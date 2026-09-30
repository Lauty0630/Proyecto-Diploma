using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BLL
{
    // Reporte asociado al RFN 1 (Reserva de vuelo). Lo usa el rol Gerente: consolida todas las
    // reservas con filtros por fecha, vuelo, clase, estado y pasajero, y se exporta a PDF igual
    // que la bitácora de eventos.
    public class BLLReporteReserva_GV42
    {
        public const string PATENTE_VER = "Reportes.Reservas";
        public const string PATENTE_EXPORTAR = "Reportes.ReservasExportarPDF";

        private const string EVENTO_CONSULTA = "Reporte de reservas consultado";
        private const string EVENTO_PDF = "Reporte de reservas exportado a PDF";

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private readonly DALReporteReserva_GV42 _dal = new DALReporteReserva_GV42();

        public bool PuedeVer() => BLLNegocioUtil_GV42.TienePatente(PATENTE_VER);
        public bool PuedeExportar() => BLLNegocioUtil_GV42.TienePatente(PATENTE_EXPORTAR);

        public List<ReporteReserva_GV42> Generar(FiltroReporteReservas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, "No tiene permiso para ver el reporte de reservas.");
            filtro = Normalizar(filtro);

            List<ReporteReserva_GV42> filas = _dal.Listar(filtro);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, EVENTO_CONSULTA,
                Recortar(filtro.Descripcion() + " | Resultados: " + filas.Count), "Baja");
            return filas;
        }

        public void ExportarPdf(string ruta, List<ReporteReserva_GV42> filas, FiltroReporteReservas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_EXPORTAR, "No tiene permiso para exportar el reporte de reservas.");
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42("Indique dónde guardar el PDF.");
            if (filas == null || filas.Count == 0)
                throw new NegocioException_GV42("No hay reservas para exportar con los filtros aplicados.");

            filtro = filtro ?? new FiltroReporteReservas_GV42();

            string[] headers =
            {
                "Reserva / Fecha", "Pasajero / DNI", "Contacto", "Vuelo", "Origen / Destino",
                "Salida / Llegada", "Clase", "Pax", "Adicionales (tipo x cant. = costo)",
                "Importe base ($)", "Impuestos ($)", "Total final ($)", "Estado"
            };
            float[] anchos = { 0.062f, 0.09f, 0.105f, 0.063f, 0.1f, 0.082f, 0.055f, 0.028f, 0.154f, 0.068f, 0.063f, 0.07f, 0.06f };
            bool[] derecha = { false, false, false, false, false, false, false, true, false, true, true, true, false };

            var datos = filas.Select(f => new[]
            {
                f.NumeroReserva + "\n" + f.FechaRealizacion.ToString("dd/MM/yyyy HH:mm"),
                f.PasajeroApellido + ", " + f.PasajeroNombre + "\nDNI " + f.PasajeroDni,
                (f.Email ?? "") + "\n" + (string.IsNullOrWhiteSpace(f.Telefono) ? "Sin teléfono" : "Tel. " + f.Telefono),
                f.CodigoVuelo,
                f.OrigenIata + " " + f.OrigenCiudad + "\n" + f.DestinoIata + " " + f.DestinoCiudad,
                f.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm") + "\n" + f.FechaHoraLlegada.ToString("dd/MM/yyyy HH:mm"),
                f.ClaseTexto,
                f.CantidadPasajeros.ToString(),
                AdicionalesPdf(f),
                Numero(f.ImporteBase),
                Numero(f.Impuestos),
                Numero(f.ImporteTotal),
                f.EstadoTexto
            }).ToList();

            string[] subtitulos =
            {
                "Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " por " + LoginSeguro() +
                " - Total de registros: " + filas.Count,
                "Filtros: " + filtro.Descripcion()
            };

            try
            {
                new GeneradorPdf_GV42().GenerarTablaMultilinea(ruta, "Reporte de Reservas (RFN 1)", subtitulos,
                    headers, anchos, datos, 7f, derecha, Resumen(filas).ToArray());
            }
            catch (IOException ex)
            {
                throw new NegocioException_GV42("No se pudo guardar el PDF (¿está abierto en otro programa?). " + ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                throw new NegocioException_GV42("No hay permiso para escribir en la carpeta elegida.");
            }

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, EVENTO_PDF,
                Recortar(Path.GetFileName(ruta) + " | Registros: " + filas.Count), "Media");
        }

        // Totales para el pie del reporte: cantidades por estado, pasajeros, adicionales y montos.
        // Las reservas canceladas se cuentan pero no suman al importe.
        public List<string> Resumen(List<ReporteReserva_GV42> filas)
        {
            filas = filas ?? new List<ReporteReserva_GV42>();
            var vigentes = filas.Where(f => f.Estado != EstadoReserva_GV42.Cancelada).ToList();

            return new List<string>
            {
                "Reservas: " + filas.Count +
                "  |  Confirmadas: " + filas.Count(f => f.Estado == EstadoReserva_GV42.Confirmada) +
                "  |  Pendientes de pago: " + filas.Count(f => f.Estado == EstadoReserva_GV42.PendienteDePago) +
                "  |  Canceladas: " + filas.Count(f => f.Estado == EstadoReserva_GV42.Cancelada),
                "Pasajeros (sin canceladas): " + vigentes.Sum(f => f.CantidadPasajeros) +
                "  |  Adicionales: " + Dinero(vigentes.Sum(f => f.SubtotalAdicionales)) +
                "  |  Importe total (sin canceladas): " + Dinero(vigentes.Sum(f => f.ImporteTotal))
            };
        }

        public static string TextoAdicionales(ReporteReserva_GV42 f, string separador)
        {
            if (f.Adicionales == null || f.Adicionales.Count == 0) return "Sin adicionales";
            return string.Join(separador, f.Adicionales.Select(a =>
                a.Tipo + " x" + a.Cantidad + " = " + Dinero(a.Subtotal)));
        }

        private static FiltroReporteReservas_GV42 Normalizar(FiltroReporteReservas_GV42 f)
        {
            f = f ?? new FiltroReporteReservas_GV42();
            f.CodigoVuelo = string.IsNullOrWhiteSpace(f.CodigoVuelo) ? null : f.CodigoVuelo.Trim().ToUpperInvariant();
            f.Pasajero = string.IsNullOrWhiteSpace(f.Pasajero) ? null : Servicios.Validaciones_GV42.NormalizarEspacios(f.Pasajero);

            if (f.FechaDesde.HasValue) f.FechaDesde = f.FechaDesde.Value.Date;
            if (f.FechaHasta.HasValue) f.FechaHasta = f.FechaHasta.Value.Date;
            if (f.FechaDesde.HasValue && f.FechaHasta.HasValue && f.FechaHasta < f.FechaDesde)
                throw new NegocioException_GV42("La fecha 'hasta' no puede ser anterior a la fecha 'desde'.");
            if (f.CodigoVuelo != null && f.CodigoVuelo.Length > 20)
                throw new NegocioException_GV42("El código de vuelo es demasiado largo.");
            if (f.Pasajero != null && f.Pasajero.Length > 60)
                throw new NegocioException_GV42("El texto de búsqueda del pasajero es demasiado largo (máximo 60).");
            return f;
        }

        // En el PDF "x2 = $ 50.000,00" no se parte en renglones (espacios duros): si el nombre del
        // adicional es largo, el corte cae entre el nombre y la cantidad.
        private static string AdicionalesPdf(ReporteReserva_GV42 f)
        {
            const string D = "\u00A0";
            if (f.Adicionales == null || f.Adicionales.Count == 0) return "Sin adicionales";
            return string.Join("\n", f.Adicionales.Select(a =>
                a.Tipo + " x" + a.Cantidad + D + "=" + D + "$" + D + Numero(a.Subtotal)));
        }

        private static string Numero(decimal importe) => importe.ToString("N2", Cultura);

        private static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);

        private static string LoginSeguro()
        {
            try { return BLLNegocioUtil_GV42.LoginActual(); } catch { return "sistema"; }
        }

        // La columna Detalle de la bitácora tiene un largo acotado.
        private static string Recortar(string s) => s != null && s.Length > 250 ? s.Substring(0, 247) + "..." : s;
    }
}
