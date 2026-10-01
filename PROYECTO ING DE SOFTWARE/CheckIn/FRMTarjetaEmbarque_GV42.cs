using BE;
using BLL;
using Servicios;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Visor de la tarjeta de embarque y las etiquetas de equipaje (RFN 2, pasos 9 y 10), con opción
    // de guardarlas en PDF o imprimirlas. Se abre al confirmar el check-in.
    // El diseño está en FRMTarjetaEmbarque_GV42.Designer.cs (Form Designer); las etiquetas se agregan
    // en tiempo de ejecución (un CtrlEtiquetaEquipaje_GV42 por bulto) dentro de flpEtiquetas.
    public partial class FRMTarjetaEmbarque_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLDocumentosCheckIn_GV42 _bll = new BLLDocumentosCheckIn_GV42();
        private readonly CheckIn_GV42 _checkIn;
        private int _paginaAImprimir;

        #endregion

        #region Constructor

        // Muestra la tarjeta de embarque (y las etiquetas, si despachó equipaje) del check-in indicado.
        public static void Mostrar(IWin32Window owner, CheckIn_GV42 checkIn)
        {
            if (checkIn == null || checkIn.TarjetaEmbarque == null)
            {
                MessageBox.Show(owner, IdiomaManager_GV42.T("tarjetaEmb.sinTarjeta"), IdiomaManager_GV42.T("tarjetaEmb.encabezado"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var f = new FRMTarjetaEmbarque_GV42(checkIn))
                f.ShowDialog(owner);
        }

        public FRMTarjetaEmbarque_GV42(CheckIn_GV42 checkIn)
        {
            InitializeComponent();

            _checkIn = checkIn;
            CargarDocumentos();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        // Los textos de la tarjeta y de las etiquetas se traducen al dibujarse: alcanza con repintarlas.
        public void ActualizarIdioma()
        {
            TarjetaEmbarque_GV42 t = _checkIn != null ? _checkIn.TarjetaEmbarque : null;
            int bultos = CantidadEtiquetas();

            Text = IdiomaManager_GV42.T("tarjetaEmb.tituloVentana", t != null ? t.NumeroReserva : "");
            lblTitulo.Text = IdiomaManager_GV42.T("tarjetaEmb.encabezado");
            lblSubtitulo.Text = t == null ? "" : IdiomaManager_GV42.T("tarjetaEmb.resumen", t.NombreApellido, t.CodigoVuelo,
                DisenioTarjetaEmbarque_GV42.CodigoOrigen(_checkIn), DisenioTarjetaEmbarque_GV42.CodigoDestino(_checkIn),
                t.NumeroAsiento, DisenioTarjetaEmbarque_GV42.TextoCanal(_checkIn));

            lblSeccionEtiquetas.Text = IdiomaManager_GV42.T("tarjetaEmb.seccionEtiquetas", bultos);
            lblAyudaEtiquetas.Text = IdiomaManager_GV42.T(bultos > 0 ? "tarjetaEmb.ayudaEtiquetas" : "tarjetaEmb.ayudaSinEquipaje");
            lblAyuda.Text = IdiomaManager_GV42.T("tarjetaEmb.ayudaImpresion");
            btnPdf.Text = IdiomaManager_GV42.T("tarjetaEmb.guardarPdf");
            btnImprimir.Text = IdiomaManager_GV42.T("tarjetaEmb.imprimir");
            btnCerrar.Text = IdiomaManager_GV42.T("tarjetaEmb.cerrar");

            ctrlTarjeta.Invalidate();
            foreach (Control c in flpEtiquetas.Controls) c.Invalidate();
        }

        #endregion

        #region Carga de datos

        // La tarjeta va en el control del diseñador; las etiquetas, una por bulto (dependen de los datos).
        private void CargarDocumentos()
        {
            ctrlTarjeta.CheckIn = _checkIn;

            flpEtiquetas.SuspendLayout();
            foreach (Control c in flpEtiquetas.Controls.Cast<Control>().ToList())
            {
                flpEtiquetas.Controls.Remove(c);
                c.Dispose();
            }
            for (int i = 0; i < CantidadEtiquetas(); i++)
                flpEtiquetas.Controls.Add(new CtrlEtiquetaEquipaje_GV42(_checkIn, i));
            flpEtiquetas.ResumeLayout();

            flpEtiquetas.Visible = CantidadEtiquetas() > 0;
            btnPdf.Enabled = btnImprimir.Enabled = _checkIn != null && _checkIn.TarjetaEmbarque != null;
        }

        private int CantidadEtiquetas()
        {
            return _checkIn != null && _checkIn.Equipaje != null && _checkIn.Equipaje.Etiquetas != null
                ? _checkIn.Equipaje.Etiquetas.Count : 0;
        }

        #endregion

        #region Impresión

        // Cada hoja es igual a la del PDF (tarjeta arriba y etiquetas abajo), escalada al área imprimible.
        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Point;

            // MarginBounds viene en centésimas de pulgada; 1 pulgada = 72 puntos.
            float x = e.MarginBounds.Left * 0.72f, y = e.MarginBounds.Top * 0.72f;
            float ancho = e.MarginBounds.Width * 0.72f, alto = e.MarginBounds.Height * 0.72f;
            float escala = Math.Min(1f, Math.Min(ancho / BLLDocumentosCheckIn_GV42.ANCHO_PAGINA,
                                                 alto / BLLDocumentosCheckIn_GV42.ALTO_PAGINA));

            using (var lienzo = new LienzoGdi_GV42(g))
            {
                var estado = g.Save();
                g.TranslateTransform(x + (ancho - BLLDocumentosCheckIn_GV42.ANCHO_PAGINA * escala) / 2, y);
                g.ScaleTransform(escala, escala);
                BLLDocumentosCheckIn_GV42.DibujarPagina(lienzo, _checkIn, _paginaAImprimir, 0, 0);
                g.Restore(estado);
            }
            _paginaAImprimir++;
            e.HasMorePages = _paginaAImprimir < BLLDocumentosCheckIn_GV42.CantidadPaginas(_checkIn);
        }

        #endregion

        #region Eventos

        // Sin equipaje no hay etiquetas: el diálogo se acorta para no dejar el espacio vacío.
        // En pantallas bajas, el diálogo se ajusta al área de trabajo y el contenido se desplaza.
        private void FRMTarjetaEmbarque_GV42_Load(object sender, EventArgs e)
        {
            if (CantidadEtiquetas() == 0)
                Height -= flpEtiquetas.Height;

            Rectangle area = Screen.FromControl(this).WorkingArea;
            if (Height > area.Height)
                Height = area.Height;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            TarjetaEmbarque_GV42 t = _checkIn.TarjetaEmbarque;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("tarjetaEmb.filtroPdf");
                sfd.Title = IdiomaManager_GV42.T("tarjetaEmb.guardarTitulo");
                sfd.FileName = IdiomaManager_GV42.T("tarjetaEmb.nombreArchivo", t.NumeroReserva, t.DNI);
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    _bll.ExportarPdf(sfd.FileName, _checkIn);
                    if (MessageBox.Show(this, IdiomaManager_GV42.T("tarjetaEmb.pdfGenerado", sfd.FileName),
                                        IdiomaManager_GV42.T("general.exito"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(this, ex.Message, IdiomaManager_GV42.T("tarjetaEmb.noSePudoGuardar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, IdiomaManager_GV42.T("tarjetaEmb.errorPdf") + "\n\n" + ex.Message, IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Impresión con vista previa: hoja horizontal con la tarjeta y las etiquetas.
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (var doc = new PrintDocument())
            using (var vista = new PrintPreviewDialog())
            {
                doc.DocumentName = IdiomaManager_GV42.T("tarjetaEmb.documento", _checkIn.TarjetaEmbarque.NumeroTarjeta);
                doc.DefaultPageSettings.Landscape = true;
                doc.BeginPrint += (s, ev) => _paginaAImprimir = 0;
                doc.PrintPage += ImprimirPagina;

                vista.Document = doc;
                vista.Text = IdiomaManager_GV42.T("tarjetaEmb.vistaPrevia");
                vista.Width = 1000;
                vista.Height = 720;
                vista.StartPosition = FormStartPosition.CenterParent;
                try
                {
                    vista.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, IdiomaManager_GV42.T("tarjetaEmb.errorImpresion") + "\n\n" + ex.Message,
                                    IdiomaManager_GV42.T("tarjetaEmb.imprimir"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion
    }
}
