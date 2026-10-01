using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Visor de los boletos electrónicos de una reserva (uno por pasajero), con opción de
    // guardarlos en PDF o imprimirlos. Se abre desde "Registrar pago" y desde "Consultar reservas".
    // El diseño está en FRMBoletos_GV42.Designer.cs (Form Designer); los boletos se agregan en
    // tiempo de ejecución (un CtrlBoleto_GV42 por pasajero) dentro de flpBoletos.
    public partial class FRMBoletos_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLBoleto_GV42 _bll = new BLLBoleto_GV42();
        private readonly string _numeroReserva;
        private List<BoletoElectronico_GV42> _boletos;
        private int _siguienteAImprimir;

        // Idioma en el que la BLL armó los boletos que se están mostrando.
        private string _idiomaBoletos;

        #endregion

        #region Constructor

        // Busca los boletos y abre el visor; si no se pueden mostrar, avisa el motivo.
        public static void Mostrar(IWin32Window owner, string numeroReserva)
        {
            List<BoletoElectronico_GV42> boletos;
            try
            {
                boletos = new BLLBoleto_GV42().ObtenerBoletosElectronicos(numeroReserva);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(owner, ex.Message, IdiomaManager_GV42.T("boletos.titulo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, IdiomaManager_GV42.T("boletos.errorObtener") + "\n\n" + ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var frm = new FRMBoletos_GV42(boletos))
                frm.ShowDialog(owner);
        }

        private FRMBoletos_GV42(List<BoletoElectronico_GV42> boletos)
        {
            InitializeComponent();

            _boletos = boletos;
            _numeroReserva = boletos[0].NumeroReserva;
            _idiomaBoletos = IdiomaManager_GV42.Instancia.IdiomaActual;
            CargarBoletos();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            // Los datos del boleto que arma la BLL (clase, forma de pago, estado, servicios) quedan en el
            // idioma en que se generaron: si cambió el idioma se vuelven a generar. Si no se pueden
            // obtener, se redibujan los que ya están (las etiquetas del diseño se traducen solas).
            if (_idiomaBoletos != IdiomaManager_GV42.Instancia.IdiomaActual)
            {
                try
                {
                    List<BoletoElectronico_GV42> nuevos = _bll.ObtenerBoletosElectronicos(_numeroReserva);
                    if (nuevos != null && nuevos.Count > 0)
                    {
                        _boletos = nuevos;
                        _idiomaBoletos = IdiomaManager_GV42.Instancia.IdiomaActual;
                    }
                }
                catch { }
                CargarBoletos();
            }

            BoletoElectronico_GV42 primero = _boletos[0];
            Text = IdiomaManager_GV42.T("boletos.tituloVentana", _numeroReserva);
            lblTitulo.Text = IdiomaManager_GV42.T("boletos.encabezado", _numeroReserva);
            string cantidad = IdiomaManager_GV42.T(_boletos.Count == 1 ? "boletos.cantidadUno" : "boletos.cantidadVarios", _boletos.Count);
            lblSubtitulo.Text = IdiomaManager_GV42.T("boletos.resumen", cantidad, primero.CodigoVuelo, primero.OrigenIata,
                                                     primero.DestinoIata, primero.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm"));
            lblAyuda.Text = IdiomaManager_GV42.T("boletos.ayudaImpresion");
            btnPdf.Text = IdiomaManager_GV42.T("boletos.guardarPdf");
            btnImprimir.Text = IdiomaManager_GV42.T("boletos.imprimir");
            btnCerrar.Text = IdiomaManager_GV42.T("boletos.cerrar");
        }

        #endregion

        #region Carga de datos

        // Un CtrlBoleto_GV42 por pasajero (depende de los datos: se crean en tiempo de ejecución).
        private void CargarBoletos()
        {
            flpBoletos.SuspendLayout();
            foreach (Control c in flpBoletos.Controls.Cast<Control>().ToList())
            {
                flpBoletos.Controls.Remove(c);
                c.Dispose();
            }
            foreach (BoletoElectronico_GV42 b in _boletos)
                flpBoletos.Controls.Add(new CtrlBoleto_GV42(b));
            flpBoletos.ResumeLayout();
            AjustarBoletos();
        }

        // Cada boleto ocupa el ancho disponible (sin barra horizontal).
        private void AjustarBoletos()
        {
            int ancho = flpBoletos.ClientSize.Width - flpBoletos.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth;
            foreach (CtrlBoleto_GV42 c in flpBoletos.Controls.OfType<CtrlBoleto_GV42>())
                c.AjustarAncho(Math.Min(ancho, 1400));
        }

        #endregion

        #region Impresión

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Point;

            // MarginBounds viene en centésimas de pulgada; 1 pulgada = 72 puntos.
            float x = e.MarginBounds.Left * 0.72f, y = e.MarginBounds.Top * 0.72f;
            float ancho = e.MarginBounds.Width * 0.72f, alto = e.MarginBounds.Height * 0.72f;
            const float separacion = 16f;

            float escala = Math.Min(1f, Math.Min(ancho / DisenioBoleto_GV42.ANCHO,
                                                 (alto - separacion) / (2 * DisenioBoleto_GV42.ALTO)));
            float xBoleto = x + (ancho - DisenioBoleto_GV42.ANCHO * escala) / 2;

            using (var lienzo = new LienzoGdi_GV42(g))
            {
                for (int i = 0; i < 2 && _siguienteAImprimir < _boletos.Count; i++, _siguienteAImprimir++)
                {
                    var estado = g.Save();
                    g.TranslateTransform(xBoleto, y + i * (DisenioBoleto_GV42.ALTO * escala + separacion));
                    g.ScaleTransform(escala, escala);
                    DisenioBoleto_GV42.Dibujar(lienzo, _boletos[_siguienteAImprimir], 0, 0);
                    g.Restore(estado);
                }
            }
            e.HasMorePages = _siguienteAImprimir < _boletos.Count;
        }

        #endregion

        #region Eventos

        private void FRMBoletos_GV42_Load(object sender, EventArgs e)
        {
            AjustarBoletos();
        }

        private void flpBoletos_Resize(object sender, EventArgs e)
        {
            AjustarBoletos();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("boletos.filtroPdf");
                sfd.Title = IdiomaManager_GV42.T("boletos.guardarTitulo");
                sfd.FileName = IdiomaManager_GV42.T("boletos.nombreArchivo", _numeroReserva);
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    _bll.ExportarPdf(sfd.FileName, _boletos);
                    if (MessageBox.Show(this, IdiomaManager_GV42.T("boletos.pdfGenerado", sfd.FileName),
                                        IdiomaManager_GV42.T("general.exito"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(this, ex.Message, IdiomaManager_GV42.T("boletos.noSePudoGuardar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, IdiomaManager_GV42.T("boletos.errorPdf") + "\n\n" + ex.Message, IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Impresión con vista previa: dos boletos por hoja, en horizontal.
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (var doc = new PrintDocument())
            using (var vista = new PrintPreviewDialog())
            {
                doc.DocumentName = IdiomaManager_GV42.T("boletos.documento", _numeroReserva);
                doc.DefaultPageSettings.Landscape = true;
                doc.BeginPrint += (s, ev) => _siguienteAImprimir = 0;
                doc.PrintPage += ImprimirPagina;

                vista.Document = doc;
                vista.Text = IdiomaManager_GV42.T("boletos.vistaPrevia");
                vista.Width = 1000;
                vista.Height = 720;
                vista.StartPosition = FormStartPosition.CenterParent;
                try
                {
                    vista.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, IdiomaManager_GV42.T("boletos.errorImpresion") + "\n\n" + ex.Message,
                                    IdiomaManager_GV42.T("boletos.imprimir"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion
    }
}
