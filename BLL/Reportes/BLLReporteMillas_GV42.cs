using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    // Reporte inteligente (3ra entrega): Gestión de Millas.
    // Arma el perfil de fidelización de cada pasajero a partir de los vuelos que ya voló (check-in
    // realizado) y propone una oferta razonada. No hay tablas de millas: todo se calcula.
    //
    // Reglas del programa de millas:
    //  - Acumulación: cada vuelo volado suma MILLAS_NACIONAL (origen y destino en el mismo país) o
    //    MILLAS_INTERNACIONAL, multiplicado por la clase (Económica x1, Ejecutiva x1,5, Primera x2).
    //  - Categoría: según las millas de los últimos 12 meses (Bronce / Plata / Oro / Diamante).
    //  - Vencimiento: las millas de un vuelo vencen a los MESES_VIGENCIA meses de volado.
    //  - Ingreso al programa: la fecha del primer vuelo volado. N° de cuenta: "FS-" + DNI.
    //  - Proyección: frecuencia de los últimos 12 meses x promedio de millas por vuelo.
    //
    // Por ahora usa las patentes del reporte de reservas (Gerente). En la 3ra entrega se le pueden
    // dar patentes propias: el bloque está preparado (comentado) al final de ActualizacionBD.sql.
    public class BLLReporteMillas_GV42
    {
        #region Constantes

        public const string PATENTE_VER = "Reportes.Reservas";
        public const string PATENTE_EXPORTAR = "Reportes.ReservasExportarPDF";

        public const int MILLAS_NACIONAL = 500;
        public const int MILLAS_INTERNACIONAL = 1500;
        public const int MESES_VIGENCIA = 24;
        public const int DIAS_PROYECCION = 90;

        // Millas de los últimos 12 meses necesarias para cada categoría (Bronce, Plata, Oro, Diamante).
        private static readonly int[] UMBRALES = { 0, 2000, 5000, 10000 };

        private const string EVENTO_CONSULTA = "Reporte de millas consultado";
        private const string EVENTO_PDF = "Reporte de millas exportado a PDF";

        #endregion

        #region Campos

        private readonly DALReporteMillas_GV42 _dal = new DALReporteMillas_GV42();

        #endregion

        #region Permisos

        public bool PuedeVer() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_VER); }
        public bool PuedeExportar() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_EXPORTAR); }

        #endregion

        #region Consulta

        public List<ReporteMillas_GV42> Generar(FiltroReporteMillas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.millas.sinPermisoVer"));
            filtro = Normalizar(filtro);

            List<ReporteMillas_GV42> filas = Calcular(_dal.ListarVuelosVolados(), filtro, DateTime.Now);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, EVENTO_CONSULTA,
                "Período: " + filtro.Periodo + " | Resultados: " + filas.Count, "Baja");
            return filas;
        }

        // Arma los perfiles a partir de los vuelos volados. "ahora" se recibe para poder probarlo.
        public static List<ReporteMillas_GV42> Calcular(List<VueloVolado_GV42> vuelos, FiltroReporteMillas_GV42 filtro, DateTime ahora)
        {
            filtro = filtro ?? new FiltroReporteMillas_GV42();
            vuelos = vuelos ?? new List<VueloVolado_GV42>();
            foreach (VueloVolado_GV42 v in vuelos) v.Millas = MillasDelVuelo(v);

            var perfiles = new List<ReporteMillas_GV42>();
            foreach (var grupo in vuelos.GroupBy(v => v.Dni))
                perfiles.Add(ArmarPerfil(grupo.OrderBy(v => v.Fecha).ToList(), filtro.Periodo, ahora));

            if (filtro.Categoria.HasValue)
                perfiles = perfiles.Where(p => p.Categoria == filtro.Categoria.Value).ToList();
            if (!string.IsNullOrWhiteSpace(filtro.Pasajero))
            {
                string texto = filtro.Pasajero.Trim();
                perfiles = perfiles.Where(p =>
                    (p.Dni ?? "").IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (p.Nombre + " " + p.Apellido).IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (p.Apellido + " " + p.Nombre).IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            // Primero los que están más cerca de subir de categoría: son las oportunidades de retención.
            return perfiles
                .OrderBy(p => p.CategoriaSiguiente.HasValue ? 0 : 1)
                .ThenBy(p => p.MillasParaSiguiente)
                .ThenBy(p => p.Apellido).ThenBy(p => p.Nombre)
                .ToList();
        }

        public static int MillasDelVuelo(VueloVolado_GV42 v)
        {
            bool nacional = string.Equals((v.OrigenPais ?? "").Trim(), (v.DestinoPais ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
            decimal factor = v.Clase == ClaseVuelo_GV42.Primera ? 2m : v.Clase == ClaseVuelo_GV42.Ejecutiva ? 1.5m : 1m;
            return (int)Math.Round((nacional ? MILLAS_NACIONAL : MILLAS_INTERNACIONAL) * factor);
        }

        public static CategoriaMillas_GV42 CategoriaPara(int millasUltimos12Meses)
        {
            int c = 0;
            for (int i = 0; i < UMBRALES.Length; i++)
                if (millasUltimos12Meses >= UMBRALES[i]) c = i;
            return (CategoriaMillas_GV42)c;
        }

        #endregion

        #region Exportación a PDF

        public void ExportarPdf(string ruta, List<ReporteMillas_GV42> filas, FiltroReporteMillas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_EXPORTAR, IdiomaManager_GV42.T("neg.millas.sinPermisoExportar"));
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));
            if (filas == null || filas.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.millas.sinDatos"));
            filtro = filtro ?? new FiltroReporteMillas_GV42();

            string[] headers =
            {
                IdiomaManager_GV42.T("pdfMillas.colPasajero"), IdiomaManager_GV42.T("pdfMillas.colCategoria"),
                IdiomaManager_GV42.T("pdfMillas.colMillas"), IdiomaManager_GV42.T("pdfMillas.colHistorial"),
                IdiomaManager_GV42.T("pdfMillas.colProyeccion"), IdiomaManager_GV42.T("pdfMillas.colRecomendacion")
            };
            float[] anchos = { 0.17f, 0.11f, 0.14f, 0.18f, 0.14f, 0.26f };

            var datos = filas.Select(f => new[]
            {
                f.NombreCompleto + "\n" + f.NumeroCuenta + "\n" + IdiomaManager_GV42.T("pdfReservas.dni", f.Dni) + "\n" + f.Email + "\n" + f.Telefono,
                TextoCategoria(f.Categoria) + "\n" + IdiomaManager_GV42.T("pdfMillas.ingreso", f.FechaIngreso.ToString("dd/MM/yyyy")),
                IdiomaManager_GV42.T("pdfMillas.millas", Numero(f.MillasTotales), Numero(f.MillasPeriodo), Numero(f.MillasUltimoVuelo), TextoFaltan(f)),
                IdiomaManager_GV42.T("pdfMillas.historial", f.VuelosPeriodo, string.Join("; ", f.RutasFrecuentes), f.ClaseHabitual.Texto(),
                    f.VuelosPorMes.ToString("0.##"), TextoUltimoVuelo(f)),
                IdiomaManager_GV42.T("pdfMillas.proyeccion", Numero(f.ProyeccionMillas90Dias), TextoFechaEstimada(f),
                    Numero(f.MillasVencen30Dias), Numero(f.MillasVencen60Dias)),
                f.Recomendacion
            }).ToList();

            string[] subtitulos =
            {
                IdiomaManager_GV42.T("pdfReservas.generado", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), LoginSeguro(), filas.Count),
                IdiomaManager_GV42.T("pdfMillas.filtros", TextoPeriodo(filtro.Periodo),
                    filtro.Categoria.HasValue ? TextoCategoria(filtro.Categoria.Value) : IdiomaManager_GV42.T("reporte.todas"))
            };

            try
            {
                new GeneradorPdf_GV42().GenerarTablaMultilinea(ruta, IdiomaManager_GV42.T("pdfMillas.titulo"), subtitulos,
                    headers, anchos, datos, 7.5f, null, Resumen(filas).ToArray());
            }
            catch (IOException ex)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.noSePudoGuardar", ex.Message));
            }
            catch (UnauthorizedAccessException)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.sinPermisoCarpeta"));
            }

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, EVENTO_PDF,
                Path.GetFileName(ruta) + " | Registros: " + filas.Count, "Media");
        }

        #endregion

        #region Resumen y textos

        // Totales del pie del reporte (pantalla y PDF), en el idioma actual.
        public List<string> Resumen(List<ReporteMillas_GV42> filas)
        {
            filas = filas ?? new List<ReporteMillas_GV42>();
            var porCategoria = Enum.GetValues(typeof(CategoriaMillas_GV42)).Cast<CategoriaMillas_GV42>()
                .Select(c => TextoCategoria(c) + ": " + filas.Count(f => f.Categoria == c));
            int porVencer = filas.Count(f => f.MillasVencen60Dias > 0);
            return new List<string>
            {
                IdiomaManager_GV42.T("pdfMillas.resumenTotales", filas.Count, string.Join(" | ", porCategoria), porVencer),
                IdiomaManager_GV42.T("pdfMillas.resumenCriterio", MILLAS_NACIONAL, MILLAS_INTERNACIONAL, MESES_VIGENCIA)
            };
        }

        public static string TextoCategoria(CategoriaMillas_GV42 c)
        {
            return IdiomaManager_GV42.TConDefecto("millas.categoria." + c, c.ToString());
        }

        public static string TextoPeriodo(PeriodoMillas_GV42 p)
        {
            return IdiomaManager_GV42.TConDefecto("millas.periodo." + p, p.ToString());
        }

        public static string TextoFaltan(ReporteMillas_GV42 f)
        {
            return f.CategoriaSiguiente.HasValue
                ? IdiomaManager_GV42.T("millas.faltan", Numero(f.MillasParaSiguiente), TextoCategoria(f.CategoriaSiguiente.Value))
                : IdiomaManager_GV42.T("millas.categoriaMaxima");
        }

        public static string TextoFechaEstimada(ReporteMillas_GV42 f)
        {
            if (!f.CategoriaSiguiente.HasValue) return IdiomaManager_GV42.T("millas.categoriaMaxima");
            return f.FechaEstimadaSiguiente.HasValue
                ? f.FechaEstimadaSiguiente.Value.ToString("dd/MM/yyyy")
                : IdiomaManager_GV42.T("millas.sinEstimacion");
        }

        public static string TextoUltimoVuelo(ReporteMillas_GV42 f)
        {
            return f.UltimoVuelo == null ? "-"
                : f.UltimoVuelo.Fecha.ToString("dd/MM/yyyy") + ", " + f.UltimoVuelo.Ruta + ", " + f.UltimoVuelo.Clase.Texto();
        }

        public static string Numero(int n) { return n.ToString("N0", new System.Globalization.CultureInfo("es-AR")); }

        // Perfil completo de un pasajero en varias líneas (panel de detalle de la pantalla).
        public static string Detalle(ReporteMillas_GV42 f)
        {
            if (f == null) return string.Empty;
            return string.Join(Environment.NewLine, new[]
            {
                IdiomaManager_GV42.T("millas.detContacto", f.NumeroCuenta, f.Email, f.Telefono, f.FechaIngreso.ToString("dd/MM/yyyy")),
                IdiomaManager_GV42.T("millas.detHistorial", string.Join("; ", f.RutasFrecuentes), f.ClaseHabitual.Texto(),
                    f.VuelosPorMes.ToString("0.##"), TextoUltimoVuelo(f), Numero(f.MillasUltimoVuelo)),
                IdiomaManager_GV42.T("millas.detProyeccion", Numero(f.ProyeccionMillas90Dias), TextoFechaEstimada(f),
                    Numero(f.MillasVencen30Dias), Numero(f.MillasVencen60Dias)),
                IdiomaManager_GV42.T("millas.detRecomendacion", f.Recomendacion)
            });
        }

        #endregion

        #region Métodos privados

        private static ReporteMillas_GV42 ArmarPerfil(List<VueloVolado_GV42> vuelos, PeriodoMillas_GV42 periodo, DateTime ahora)
        {
            VueloVolado_GV42 primero = vuelos.First(), ultimo = vuelos.Last();
            DateTime desdePeriodo = ahora.AddMonths(-(int)periodo), desde12 = ahora.AddMonths(-12);
            List<VueloVolado_GV42> delPeriodo = vuelos.Where(v => v.Fecha >= desdePeriodo).ToList();
            List<VueloVolado_GV42> de12Meses = vuelos.Where(v => v.Fecha >= desde12).ToList();

            var p = new ReporteMillas_GV42
            {
                NumeroCuenta = "FS-" + primero.Dni,
                Dni = primero.Dni,
                Nombre = ultimo.Nombre,
                Apellido = ultimo.Apellido,
                Email = ultimo.Email,
                Telefono = ultimo.Telefono,
                FechaIngreso = primero.Fecha.Date,
                MillasTotales = vuelos.Sum(v => v.Millas),
                MillasUltimoVuelo = ultimo.Millas,
                MillasPeriodo = delPeriodo.Sum(v => v.Millas),
                MillasUltimos12Meses = de12Meses.Sum(v => v.Millas),
                VuelosPeriodo = delPeriodo.Count,
                VuelosTotales = vuelos.Count,
                UltimoVuelo = ultimo,
                RutasFrecuentes = vuelos.GroupBy(v => v.Ruta).OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                                        .Take(2).Select(g => g.Key + " (" + g.Count() + ")").ToList(),
                ClaseHabitual = vuelos.GroupBy(v => v.Clase).OrderByDescending(g => g.Count()).ThenBy(g => (int)g.Key).First().Key,
                VuelosPorMes = Math.Round(de12Meses.Count / 12m, 2)
            };

            p.Categoria = CategoriaPara(p.MillasUltimos12Meses);
            if ((int)p.Categoria < UMBRALES.Length - 1)
            {
                p.CategoriaSiguiente = (CategoriaMillas_GV42)((int)p.Categoria + 1);
                p.MillasParaSiguiente = UMBRALES[(int)p.Categoria + 1] - p.MillasUltimos12Meses;
            }

            // Proyección: al ritmo de los últimos 12 meses, con el promedio de millas por vuelo.
            decimal millasPorDia = p.MillasUltimos12Meses / 365m;
            p.ProyeccionMillas90Dias = (int)Math.Round(millasPorDia * DIAS_PROYECCION);
            if (p.CategoriaSiguiente.HasValue && millasPorDia > 0)
            {
                double dias = (double)Math.Ceiling(p.MillasParaSiguiente / millasPorDia);
                if (dias <= 3650) p.FechaEstimadaSiguiente = ahora.Date.AddDays(dias);
            }

            // Vencimiento: cada vuelo vence a los MESES_VIGENCIA meses.
            p.MillasVencen30Dias = vuelos.Where(v => Vence(v, ahora, 30)).Sum(v => v.Millas);
            p.MillasVencen60Dias = vuelos.Where(v => Vence(v, ahora, 60)).Sum(v => v.Millas);

            p.Recomendacion = Recomendar(p, vuelos);
            return p;
        }

        private static bool Vence(VueloVolado_GV42 v, DateTime ahora, int dias)
        {
            DateTime vencimiento = v.Fecha.AddMonths(MESES_VIGENCIA);
            return vencimiento >= ahora && vencimiento <= ahora.AddDays(dias);
        }

        // Oferta razonada: con qué vuelo (ruta más frecuente y clase habitual) le conviene seguir.
        private static string Recomendar(ReporteMillas_GV42 p, List<VueloVolado_GV42> vuelos)
        {
            VueloVolado_GV42 rutaFrecuente = vuelos.GroupBy(v => v.Ruta).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).First().Last();
            string ruta = rutaFrecuente.Ruta;
            int millasRuta = (int)Math.Round(vuelos.Where(v => v.Ruta == ruta).Average(v => (decimal)v.Millas));
            string clase = p.ClaseHabitual.Texto().ToLower();
            string texto;

            if (!p.CategoriaSiguiente.HasValue)
            {
                texto = IdiomaManager_GV42.T("millas.recMantener", Numero(p.MillasUltimos12Meses), TextoCategoria(p.Categoria), ruta);
            }
            else
            {
                int vuelosNecesarios = Math.Max(1, (int)Math.Ceiling(p.MillasParaSiguiente / (decimal)Math.Max(1, millasRuta)));
                string siguiente = TextoCategoria(p.CategoriaSiguiente.Value);
                texto = vuelosNecesarios == 1
                    ? IdiomaManager_GV42.T("millas.recUnVuelo", Numero(p.MillasUltimos12Meses), Numero(p.MillasParaSiguiente), siguiente, clase, ruta)
                    : IdiomaManager_GV42.T("millas.recVariosVuelos", Numero(p.MillasUltimos12Meses), Numero(p.MillasParaSiguiente), siguiente, vuelosNecesarios, clase, ruta);
            }

            if (p.MillasVencen60Dias > 0)
                texto += " " + IdiomaManager_GV42.T("millas.recVencen", Numero(p.MillasVencen60Dias));
            if (p.VuelosPeriodo == 0)
                texto += " " + IdiomaManager_GV42.T("millas.recInactivo");
            return texto;
        }

        private static FiltroReporteMillas_GV42 Normalizar(FiltroReporteMillas_GV42 f)
        {
            f = f ?? new FiltroReporteMillas_GV42();
            f.Pasajero = string.IsNullOrWhiteSpace(f.Pasajero) ? null : Validaciones_GV42.NormalizarEspacios(f.Pasajero);
            if (f.Pasajero != null && f.Pasajero.Length > 60)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reporte.pasajeroLargo"));
            return f;
        }

        private static string LoginSeguro()
        {
            try { return BLLNegocioUtil_GV42.LoginActual(); } catch { return "sistema"; }
        }

        #endregion
    }
}
