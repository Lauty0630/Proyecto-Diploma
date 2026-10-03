using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    // Reporte asociado al RFN 2 (Check-in / Tarjeta de embarque). Lo usan el personal de mostrador
    // (Encargado de Check-in) y el Gerente: muestra, vuelo por vuelo, qué pasajeros hicieron el
    // check-in, cuáles siguen pendientes y cuáles quedaron ausentes, con su asiento y su tarjeta de
    // embarque. Se exporta a PDF igual que el reporte de reservas.
    //
    // La asistencia se calcula por horario, porque el sistema no registra el embarque:
    //  - Presente: hizo el check-in.
    //  - Ausente: no lo hizo y el check-in del vuelo ya cerró (MINUTOS_CIERRE_CHECKIN antes de salir).
    //  - A confirmar: no lo hizo, pero el check-in del vuelo todavía no cerró.
    public class BLLReporteCheckIn_GV42
    {
        #region Constantes

        public const string PATENTE_VER = "Reportes.CheckIn";
        public const string PATENTE_EXPORTAR = "Reportes.CheckInExportarPDF";

        private const string EVENTO_CONSULTA = "Reporte de check-in consultado";
        private const string EVENTO_PDF = "Reporte de check-in exportado a PDF";

        #endregion

        #region Campos

        private readonly DALReporteCheckIn_GV42 _dal = new DALReporteCheckIn_GV42();

        #endregion

        #region Permisos

        public bool PuedeVer() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_VER); }
        public bool PuedeExportar() { return BLLNegocioUtil_GV42.TienePatente(PATENTE_EXPORTAR); }

        #endregion

        #region Consulta

        public List<ReporteCheckIn_GV42> Generar(FiltroReporteCheckIn_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.repCheckin.sinPermisoVer"));
            filtro = Normalizar(filtro);

            List<ReporteCheckIn_GV42> filas = _dal.Listar(filtro);
            Completar(filas, DateTime.Now);
            if (filtro.Asistencia.HasValue)
                filas = filas.Where(f => f.Asistencia == filtro.Asistencia.Value).ToList();

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, EVENTO_CONSULTA,
                Recortar(filtro.Descripcion() + " | Resultados: " + filas.Count), "Baja");
            return filas;
        }

        // Calcula la asistencia de cada fila y, si todavía no tiene tarjeta de embarque, la hora
        // límite de embarque que le va a corresponder. "ahora" se recibe para poder probarlo.
        public static void Completar(List<ReporteCheckIn_GV42> filas, DateTime ahora)
        {
            if (filas == null) return;
            foreach (ReporteCheckIn_GV42 f in filas)
            {
                f.Asistencia = CalcularAsistencia(f.Estado, f.FechaHoraSalida, ahora);
                if (f.HoraLimiteEmbarque == DateTime.MinValue)
                    f.HoraLimiteEmbarque = f.FechaHoraSalida.AddMinutes(-BLLCheckIn_GV42.MINUTOS_LIMITE_EMBARQUE);
            }
        }

        public static EstadoAsistencia_GV42 CalcularAsistencia(EstadoCheckIn_GV42 estado, DateTime salida, DateTime ahora)
        {
            if (estado == EstadoCheckIn_GV42.Realizado) return EstadoAsistencia_GV42.Presente;
            return ahora > salida.AddMinutes(-BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN)
                ? EstadoAsistencia_GV42.Ausente : EstadoAsistencia_GV42.AConfirmar;
        }

        #endregion

        #region Exportación a PDF

        public void ExportarPdf(string ruta, List<ReporteCheckIn_GV42> filas, FiltroReporteCheckIn_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_EXPORTAR, IdiomaManager_GV42.T("neg.repCheckin.sinPermisoExportar"));
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));
            if (filas == null || filas.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.repCheckin.sinDatos"));

            filtro = filtro ?? new FiltroReporteCheckIn_GV42();

            string[] headers =
            {
                IdiomaManager_GV42.T("pdfCheckin.colVuelo"), IdiomaManager_GV42.T("pdfCheckin.colSalida"),
                IdiomaManager_GV42.T("pdfCheckin.colReserva"), IdiomaManager_GV42.T("pdfCheckin.colPasajero"),
                IdiomaManager_GV42.T("pdfCheckin.colAsistencia"), IdiomaManager_GV42.T("pdfCheckin.colCheckIn"),
                IdiomaManager_GV42.T("pdfCheckin.colAsiento"), IdiomaManager_GV42.T("pdfCheckin.colClase"),
                IdiomaManager_GV42.T("pdfCheckin.colTarjeta"), IdiomaManager_GV42.T("pdfCheckin.colEmbarque")
            };
            float[] anchos = { 0.17f, 0.09f, 0.08f, 0.15f, 0.08f, 0.1f, 0.055f, 0.095f, 0.08f, 0.1f };

            var datos = filas.Select(f => new[]
            {
                f.CodigoVuelo + "\n" + f.OrigenIata + " " + f.OrigenCiudad + " > " + f.DestinoIata + " " + f.DestinoCiudad,
                f.FechaHoraSalida.ToString("dd/MM/yyyy") + "\n" + f.FechaHoraSalida.ToString("HH:mm"),
                f.NumeroReserva,
                f.PasajeroApellido + ", " + f.PasajeroNombre + "\n" + IdiomaManager_GV42.T("pdfReservas.dni", f.PasajeroDni),
                f.AsistenciaTexto,
                f.EstadoTexto + (f.FechaHoraCheckIn.HasValue ? "\n" + f.FechaHoraCheckIn.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty),
                Guion(f.NumeroAsiento),
                f.ClaseTexto + (string.IsNullOrWhiteSpace(f.Ubicacion) ? string.Empty : "\n" + TextoUbicacion(f.Ubicacion)),
                Guion(f.NumeroTarjeta),
                IdiomaManager_GV42.T("pdfCheckin.puerta", Guion(f.PuertaEmbarque)) + "\n" + f.HoraLimiteEmbarque.ToString("dd/MM HH:mm")
            }).ToList();

            string[] subtitulos =
            {
                IdiomaManager_GV42.T("pdfReservas.generado", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), LoginSeguro(), filas.Count),
                IdiomaManager_GV42.T("pdfReservas.filtros", DescripcionFiltro(filtro))
            };

            try
            {
                new GeneradorPdf_GV42().GenerarTablaMultilinea(ruta, IdiomaManager_GV42.T("pdfCheckin.titulo"), subtitulos,
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

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_CHECKIN, EVENTO_PDF,
                Recortar(Path.GetFileName(ruta) + " | Registros: " + filas.Count), "Media");
        }

        #endregion

        #region Resumen y textos

        // Totales del pie del reporte (los usan la pantalla y el PDF, en el idioma actual). Los tres
        // grupos no se superponen: realizados + pendientes + ausentes = total de pasajeros.
        //  - Pendientes: no hicieron el check-in pero todavía están a tiempo.
        //  - Ausentes: no lo hicieron y el check-in del vuelo ya cerró.
        public List<string> Resumen(List<ReporteCheckIn_GV42> filas)
        {
            filas = filas ?? new List<ReporteCheckIn_GV42>();
            int realizados = filas.Count(f => f.Estado == EstadoCheckIn_GV42.Realizado);
            int ausentes = filas.Count(f => f.Asistencia == EstadoAsistencia_GV42.Ausente);
            int pendientes = filas.Count - realizados - ausentes;
            int vuelos = filas.Select(f => f.CodigoVuelo + "|" + f.FechaHoraSalida.Ticks).Distinct().Count();
            decimal porcentaje = filas.Count == 0 ? 0m : Math.Round(realizados * 100m / filas.Count, 1);

            return new List<string>
            {
                IdiomaManager_GV42.T("pdfCheckin.resumenTotales", filas.Count, vuelos, realizados, porcentaje.ToString("0.#"), pendientes, ausentes),
                IdiomaManager_GV42.T("pdfCheckin.resumenCriterio", BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN)
            };
        }

        // La ubicación del asiento viene de la base en español (Ventana / Central / Pasillo).
        public static string TextoUbicacion(string ubicacion)
        {
            if (string.IsNullOrWhiteSpace(ubicacion)) return "-";
            return IdiomaManager_GV42.TConDefecto("checkin.ubicacion." + ubicacion.Trim(), ubicacion);
        }

        #endregion

        #region Métodos privados

        private static FiltroReporteCheckIn_GV42 Normalizar(FiltroReporteCheckIn_GV42 f)
        {
            f = f ?? new FiltroReporteCheckIn_GV42();
            f.CodigoVuelo = string.IsNullOrWhiteSpace(f.CodigoVuelo) ? null : f.CodigoVuelo.Trim().ToUpperInvariant();
            f.Pasajero = string.IsNullOrWhiteSpace(f.Pasajero) ? null : Validaciones_GV42.NormalizarEspacios(f.Pasajero);

            if (f.FechaDesde.HasValue) f.FechaDesde = f.FechaDesde.Value.Date;
            if (f.FechaHasta.HasValue) f.FechaHasta = f.FechaHasta.Value.Date;
            if (f.FechaDesde.HasValue && f.FechaHasta.HasValue && f.FechaHasta < f.FechaDesde)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reporte.fechasInvertidas"));
            if (f.CodigoVuelo != null && f.CodigoVuelo.Length > 20)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reporte.codigoLargo"));
            if (f.Pasajero != null && f.Pasajero.Length > 60)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reporte.pasajeroLargo"));
            return f;
        }

        // Filtros aplicados en el idioma actual, para el subtítulo del PDF.
        private static string DescripcionFiltro(FiltroReporteCheckIn_GV42 f)
        {
            var partes = new List<string>();
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroVuelo", f.CodigoVuelo.Trim()));
            if (f.FiltraPorFecha)
                partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroRango", IdiomaManager_GV42.T("pdfReservas.filtroSalida"),
                    f.FechaDesde.HasValue ? f.FechaDesde.Value.ToString("dd/MM/yyyy") : "...",
                    f.FechaHasta.HasValue ? f.FechaHasta.Value.ToString("dd/MM/yyyy") : "..."));
            if (f.Estado.HasValue) partes.Add(IdiomaManager_GV42.T("pdfCheckin.filtroCheckIn", f.Estado.Value.Texto()));
            if (f.Asistencia.HasValue) partes.Add(IdiomaManager_GV42.T("pdfCheckin.filtroAsistencia", f.Asistencia.Value.Texto()));
            if (!string.IsNullOrWhiteSpace(f.Pasajero)) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroPasajero", f.Pasajero.Trim()));
            return partes.Count == 0 ? IdiomaManager_GV42.T("pdfReservas.sinFiltros") : string.Join(" | ", partes);
        }

        private static string Guion(string texto) { return string.IsNullOrWhiteSpace(texto) ? "-" : texto; }

        private static string LoginSeguro()
        {
            try { return BLLNegocioUtil_GV42.LoginActual(); } catch { return "sistema"; }
        }

        // La columna Detalle de la bitácora tiene un largo acotado.
        private static string Recortar(string s) { return s != null && s.Length > 250 ? s.Substring(0, 247) + "..." : s; }

        #endregion
    }
}
