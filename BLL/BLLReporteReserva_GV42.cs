using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BLL
{
    // Reporte asociado al RFN 1 (Reserva de vuelo). Lo usa el rol Gerente: consolida todas las
    // reservas con filtros por fecha, vuelo, clase, estado y pasajero, y se exporta a PDF igual
    // que la bitácora de eventos. El PDF y el resumen salen en el idioma actual; lo que se guarda
    // en la bitácora queda en español.
    public class BLLReporteReserva_GV42
    {
        #region Constantes

        public const string PATENTE_VER = "Reportes.Reservas";
        public const string PATENTE_EXPORTAR = "Reportes.ReservasExportarPDF";

        private const string EVENTO_CONSULTA = "Reporte de reservas consultado";
        private const string EVENTO_PDF = "Reporte de reservas exportado a PDF";

        #endregion

        #region Campos

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private readonly DALReporteReserva_GV42 _dal = new DALReporteReserva_GV42();

        #endregion

        #region Permisos

        public bool PuedeVer() => BLLNegocioUtil_GV42.TienePatente(PATENTE_VER);
        public bool PuedeExportar() => BLLNegocioUtil_GV42.TienePatente(PATENTE_EXPORTAR);

        #endregion

        #region Consulta

        public List<ReporteReserva_GV42> Generar(FiltroReporteReservas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.reporte.sinPermisoVer"));
            filtro = Normalizar(filtro);

            List<ReporteReserva_GV42> filas = _dal.Listar(filtro);

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, EVENTO_CONSULTA,
                Recortar(filtro.Descripcion() + " | Resultados: " + filas.Count), "Baja");
            return filas;
        }

        #endregion

        #region Exportación a PDF

        public void ExportarPdf(string ruta, List<ReporteReserva_GV42> filas, FiltroReporteReservas_GV42 filtro)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_EXPORTAR, IdiomaManager_GV42.T("neg.reporte.sinPermisoExportar"));
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));
            if (filas == null || filas.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reporte.sinDatos"));

            filtro = filtro ?? new FiltroReporteReservas_GV42();

            string[] headers =
            {
                IdiomaManager_GV42.T("pdfReservas.colReserva"), IdiomaManager_GV42.T("pdfReservas.colTitular"),
                IdiomaManager_GV42.T("pdfReservas.colContacto"), IdiomaManager_GV42.T("pdfReservas.colVuelo"),
                IdiomaManager_GV42.T("pdfReservas.colRuta"), IdiomaManager_GV42.T("pdfReservas.colHorarios"),
                IdiomaManager_GV42.T("pdfReservas.colClase"), IdiomaManager_GV42.T("pdfReservas.colPax"),
                IdiomaManager_GV42.T("pdfReservas.colAdicionales"), IdiomaManager_GV42.T("pdfReservas.colBase"),
                IdiomaManager_GV42.T("pdfReservas.colImpuestos"), IdiomaManager_GV42.T("pdfReservas.colTotal"),
                IdiomaManager_GV42.T("pdfReservas.colEstado")
            };
            float[] anchos = { 0.062f, 0.09f, 0.105f, 0.063f, 0.1f, 0.082f, 0.055f, 0.028f, 0.154f, 0.068f, 0.063f, 0.07f, 0.06f };
            bool[] derecha = { false, false, false, false, false, false, false, true, false, true, true, true, false };

            var datos = filas.Select(f => new[]
            {
                f.NumeroReserva + "\n" + f.FechaRealizacion.ToString("dd/MM/yyyy HH:mm"),
                f.PasajeroApellido + ", " + f.PasajeroNombre + "\n" + IdiomaManager_GV42.T("pdfReservas.dni", f.PasajeroDni),
                (f.Email ?? "") + "\n" + (string.IsNullOrWhiteSpace(f.Telefono)
                    ? IdiomaManager_GV42.T("pdfReservas.sinTelefono")
                    : IdiomaManager_GV42.T("pdfReservas.telefono", f.Telefono)),
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
                IdiomaManager_GV42.T("pdfReservas.generado", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), LoginSeguro(), filas.Count),
                IdiomaManager_GV42.T("pdfReservas.filtros", DescripcionFiltro(filtro))
            };

            try
            {
                new GeneradorPdf_GV42().GenerarTablaMultilinea(ruta, IdiomaManager_GV42.T("pdfReservas.titulo"), subtitulos,
                    headers, anchos, datos, 7f, derecha, Resumen(filas).ToArray());
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
                Recortar(Path.GetFileName(ruta) + " | Registros: " + filas.Count), "Media");
        }

        #endregion

        #region Resumen y textos

        // Totales para el pie del reporte: cantidades por estado, pasajeros, adicionales y montos.
        // Las reservas canceladas se cuentan pero no suman al importe. Sale en el idioma actual
        // (lo usan la pantalla y el PDF).
        public List<string> Resumen(List<ReporteReserva_GV42> filas)
        {
            filas = filas ?? new List<ReporteReserva_GV42>();
            var confirmadas = filas.Where(f => f.Estado == EstadoReserva_GV42.Confirmada).ToList();
            var pendientes = filas.Where(f => f.Estado == EstadoReserva_GV42.PendienteDePago).ToList();
            var canceladas = filas.Where(f => f.Estado == EstadoReserva_GV42.Cancelada).ToList();

            // Solo lo confirmado es dinero cobrado; lo pendiente todavía no se cobró y de lo cancelado
            // queda solo la penalidad retenida (antes se sumaban pendientes como si fueran ingresos).
            return new List<string>
            {
                IdiomaManager_GV42.T("pdfReservas.resumenCantidades",
                    filas.Count, confirmadas.Count, pendientes.Count, canceladas.Count,
                    confirmadas.Sum(f => f.CantidadPasajeros) + pendientes.Sum(f => f.CantidadPasajeros)),
                IdiomaManager_GV42.T("pdfReservas.resumenImportes",
                    Dinero(confirmadas.Sum(f => f.ImporteTotal)),
                    Dinero(pendientes.Sum(f => f.ImporteTotal)),
                    Dinero(canceladas.Sum(f => f.MontoPenalidad)),
                    Dinero(confirmadas.Sum(f => f.SubtotalAdicionales)))
            };
        }

        public static string TextoAdicionales(ReporteReserva_GV42 f, string separador)
        {
            if (f.Adicionales == null || f.Adicionales.Count == 0) return IdiomaManager_GV42.T("pdfReservas.sinAdicionales");
            return string.Join(separador, f.Adicionales.Select(a =>
                BLLNegocioUtil_GV42.NombreAdicional(a.Tipo) + " x" + a.Cantidad + " = " + Dinero(a.Subtotal)));
        }

        #endregion

        #region Métodos privados

        private static FiltroReporteReservas_GV42 Normalizar(FiltroReporteReservas_GV42 f)
        {
            f = f ?? new FiltroReporteReservas_GV42();
            f.CodigoVuelo = string.IsNullOrWhiteSpace(f.CodigoVuelo) ? null : f.CodigoVuelo.Trim().ToUpperInvariant();
            f.Pasajero = string.IsNullOrWhiteSpace(f.Pasajero) ? null : Servicios.Validaciones_GV42.NormalizarEspacios(f.Pasajero);

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

        // Filtros aplicados en el idioma actual, para el subtítulo del PDF. Es el mismo texto que
        // FiltroReporteReservas_GV42.Descripcion() (que queda en español para la bitácora).
        private static string DescripcionFiltro(FiltroReporteReservas_GV42 f)
        {
            var partes = new List<string>();
            if (f.FiltraPorFecha)
            {
                string cual = IdiomaManager_GV42.T(f.TipoFecha == FechaReporte_GV42.Salida
                    ? "pdfReservas.filtroSalida" : "pdfReservas.filtroFechaReserva");
                string desde = f.FechaDesde.HasValue ? f.FechaDesde.Value.ToString("dd/MM/yyyy") : "...";
                string hasta = f.FechaHasta.HasValue ? f.FechaHasta.Value.ToString("dd/MM/yyyy") : "...";
                partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroRango", cual, desde, hasta));
            }
            if (!string.IsNullOrWhiteSpace(f.CodigoVuelo)) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroVuelo", f.CodigoVuelo.Trim()));
            if (f.Clase.HasValue) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroClase", f.Clase.Value.Texto()));
            if (f.Estado.HasValue) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroEstado", f.Estado.Value.Texto()));
            if (!string.IsNullOrWhiteSpace(f.Pasajero)) partes.Add(IdiomaManager_GV42.T("pdfReservas.filtroPasajero", f.Pasajero.Trim()));
            return partes.Count == 0 ? IdiomaManager_GV42.T("pdfReservas.sinFiltros") : string.Join(" | ", partes);
        }

        // En el PDF "x2 = $ 50.000,00" no se parte en renglones (espacios duros): si el nombre del
        // adicional es largo, el corte cae entre el nombre y la cantidad.
        private static string AdicionalesPdf(ReporteReserva_GV42 f)
        {
            const string D = " ";
            if (f.Adicionales == null || f.Adicionales.Count == 0) return IdiomaManager_GV42.T("pdfReservas.sinAdicionales");
            return string.Join("\n", f.Adicionales.Select(a =>
                BLLNegocioUtil_GV42.NombreAdicional(a.Tipo) + " x" + a.Cantidad + D + "=" + D + "$" + D + Numero(a.Subtotal)));
        }

        private static string Numero(decimal importe) => importe.ToString("N2", Cultura);

        private static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);

        private static string LoginSeguro()
        {
            try { return BLLNegocioUtil_GV42.LoginActual(); } catch { return "sistema"; }
        }

        // La columna Detalle de la bitácora tiene un largo acotado.
        private static string Recortar(string s) => s != null && s.Length > 250 ? s.Substring(0, 247) + "..." : s;

        #endregion
    }
}
