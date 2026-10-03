using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    // Un tema de la ayuda en línea. Los textos están en los archivos de idioma:
    // "ayuda.<Id>.titulo" y "ayuda.<Id>.texto".
    public class TemaAyuda_GV42
    {
        #region Propiedades

        public string Id { get; set; }

        // Null en los temas de primer nivel del árbol.
        public string IdPadre { get; set; }

        // Clases de formulario que explica este tema (F1 desde esa pantalla abre este tema).
        public string[] Formularios { get; set; }

        public string Titulo { get { return IdiomaManager_GV42.T("ayuda." + Id + ".titulo"); } }
        public string Texto { get { return IdiomaManager_GV42.T("ayuda." + Id + ".texto"); } }

        #endregion
    }

    // Ayuda en línea (3ra entrega): árbol de temas, tema de cada pantalla (F1) y exportación a PDF.
    public class BLLAyuda_GV42
    {
        #region Campos

        public const string TEMA_INICIAL = "general";

        private static readonly List<TemaAyuda_GV42> _temas = new List<TemaAyuda_GV42>
        {
            Tema("general", null),

            Tema("maestros", null),
            Tema("vuelos", "maestros", "FRMGestionVuelos_GV42", "FRMAltaVuelo_GV42"),
            Tema("bitacoraVuelos", "maestros", "FRMBitacoraVuelos_GV42"),
            Tema("clientes", "maestros", "FRMMaestroClientes_GV42"),
            Tema("aeropuertos", "maestros", "FRMMaestroAeropuertos_GV42"),

            Tema("reservas", null),
            Tema("nuevaReserva", "reservas", "FRMReservarVuelo_GV42"),
            Tema("pago", "reservas", "FRMPagoReserva_GV42"),
            Tema("consultarReservas", "reservas", "FRMConsultarReservas_GV42", "FRMCambiarVuelo_GV42"),
            Tema("boletos", "reservas", "FRMBoletos_GV42"),

            Tema("checkin", null),
            Tema("hacerCheckin", "checkin", "FRMCheckIn_GV42"),
            Tema("tarjeta", "checkin", "FRMTarjetaEmbarque_GV42"),

            Tema("reportes", null),
            Tema("repReservas", "reportes", "FRMReporteReservas_GV42"),
            Tema("repCheckin", "reportes", "FRMReporteCheckIn_GV42"),
            Tema("repMillas", "reportes", "FRMReporteMillas_GV42"),

            Tema("admin", null),
            Tema("usuarios", "admin", "FRMGestionUsuariosAdmin"),
            Tema("permisos", "admin", "FRMGestionPermisos"),
            Tema("bitacora", "admin", "FRMBitacoraDeEventos"),
            Tema("backup", "admin", "FRMBackupManual", "FRMIntegridad"),

            Tema("usuario", null),
            Tema("clave", "usuario", "FRMCambiarContrasenia"),
            Tema("idioma", "usuario")
        };

        #endregion

        #region Consultas

        public List<TemaAyuda_GV42> Temas() { return _temas.ToList(); }

        public List<TemaAyuda_GV42> Hijos(string idPadre)
        {
            return _temas.Where(t => t.IdPadre == idPadre).ToList();
        }

        // Tema que explica una pantalla (por el nombre de su clase). Si no tiene, el tema general.
        public string TemaDeFormulario(string nombreClase)
        {
            TemaAyuda_GV42 tema = _temas.FirstOrDefault(t => t.Formularios.Contains(nombreClase));
            return tema != null ? tema.Id : TEMA_INICIAL;
        }

        #endregion

        #region Exportación a PDF

        // Toda la ayuda en un PDF: una fila por tema, en el orden del árbol.
        public void ExportarPdf(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new BE.NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));

            var filas = new List<string[]>();
            foreach (TemaAyuda_GV42 raiz in Hijos(null))
            {
                filas.Add(new[] { raiz.Titulo, raiz.Texto });
                foreach (TemaAyuda_GV42 hijo in Hijos(raiz.Id))
                    filas.Add(new[] { raiz.Titulo + " > " + hijo.Titulo, hijo.Texto });
            }

            string[] subtitulos = { IdiomaManager_GV42.T("ayuda.pdfSubtitulo", DateTime.Now.ToString("dd/MM/yyyy HH:mm")) };
            string[] headers = { IdiomaManager_GV42.T("ayuda.pdfColTema"), IdiomaManager_GV42.T("ayuda.pdfColTexto") };
            try
            {
                new GeneradorPdf_GV42().GenerarTablaMultilinea(ruta, IdiomaManager_GV42.T("ayuda.titulo"), subtitulos,
                    headers, new[] { 0.25f, 0.75f }, filas, 8.5f);
            }
            catch (IOException ex)
            {
                throw new BE.NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.noSePudoGuardar", ex.Message));
            }
            catch (UnauthorizedAccessException)
            {
                throw new BE.NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.sinPermisoCarpeta"));
            }
        }

        #endregion

        #region Métodos privados

        private static TemaAyuda_GV42 Tema(string id, string idPadre, params string[] formularios)
        {
            return new TemaAyuda_GV42 { Id = id, IdPadre = idPadre, Formularios = formularios ?? new string[0] };
        }

        #endregion
    }
}
