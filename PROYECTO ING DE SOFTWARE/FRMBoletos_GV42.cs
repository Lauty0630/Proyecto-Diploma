using BE;
using BLL;
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
    public class FRMBoletos_GV42 : Form
    {
        private readonly BLLBoleto_GV42 _bll = new BLLBoleto_GV42();
        private readonly List<BoletoElectronico_GV42> _boletos;
        private FlowLayoutPanel _lista;
        private int _siguienteAImprimir;

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
                MessageBox.Show(owner, ex.Message, "Boletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, "No se pudieron obtener los boletos.\n\n" + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var frm = new FRMBoletos_GV42(boletos))
                frm.ShowDialog(owner);
        }

        private FRMBoletos_GV42(List<BoletoElectronico_GV42> boletos)
        {
            _boletos = boletos;
            ConstruirUI();
        }

        private void ConstruirUI()
        {
            BoletoElectronico_GV42 primero = _boletos[0];
            Text = "Boletos de la reserva " + primero.NumeroReserva;
            BackColor = Tema_GV42.Fondo;
            ClientSize = new Size(1040, 720);
            MinimumSize = new Size(700, 480);
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("Segoe UI", 9F);
            ShowInTaskbar = false;

            var pnlArriba = new Panel { Dock = DockStyle.Top, Height = 78, Padding = new Padding(24, 14, 24, 0) };
            var lblTitulo = new Label
            {
                Text = "Boletos electrónicos - " + primero.NumeroReserva,
                Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento,
                AutoSize = true, Location = new Point(24, 10)
            };
            var lblSub = new Label
            {
                Text = _boletos.Count + (_boletos.Count == 1 ? " boleto" : " boletos") + "  |  Vuelo " +
                       primero.CodigoVuelo + " " + primero.OrigenIata + " -> " + primero.DestinoIata + "  |  Salida " +
                       primero.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm"),
                Font = Tema_GV42.FuenteSubtitulo, ForeColor = Color.DimGray,
                AutoSize = true, Location = new Point(26, 46)
            };

            var btnPdf = new Button { Text = "Guardar PDF", Size = new Size(130, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            Tema_GV42.EstilizarBotonPrimario(btnPdf);
            btnPdf.Click += btnPdf_Click;
            var btnImprimir = new Button { Text = "Imprimir", Size = new Size(110, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            Tema_GV42.EstilizarBotonSecundario(btnImprimir);
            btnImprimir.Click += btnImprimir_Click;
            var btnCerrar = new Button { Text = "Cerrar", Size = new Size(100, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            Tema_GV42.EstilizarBotonSecundario(btnCerrar);
            btnCerrar.Click += (s, e) => Close();
            CancelButton = btnCerrar;

            pnlArriba.Controls.AddRange(new Control[] { lblTitulo, lblSub, btnPdf, btnImprimir, btnCerrar });
            pnlArriba.Resize += (s, e) =>
            {
                int x = pnlArriba.ClientSize.Width - 24;
                btnCerrar.Location = new Point(x -= btnCerrar.Width, 18);
                btnImprimir.Location = new Point(x -= btnImprimir.Width + 10, 18);
                btnPdf.Location = new Point(x - btnPdf.Width - 10, 18);
            };

            _lista = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(24, 8, 24, 16)
            };
            foreach (BoletoElectronico_GV42 b in _boletos)
                _lista.Controls.Add(new CtrlBoleto_GV42(b));
            _lista.Resize += (s, e) => AjustarBoletos();

            Controls.Add(_lista);
            Controls.Add(pnlArriba);
            Load += (s, e) => AjustarBoletos();
        }

        // Cada boleto ocupa el ancho disponible (sin barra horizontal).
        private void AjustarBoletos()
        {
            int ancho = _lista.ClientSize.Width - _lista.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth;
            foreach (CtrlBoleto_GV42 c in _lista.Controls.OfType<CtrlBoleto_GV42>())
                c.AjustarAncho(Math.Min(ancho, 1400));
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.Title = "Guardar boletos";
                sfd.FileName = "Boletos_" + _boletos[0].NumeroReserva + ".pdf";
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    _bll.ExportarPdf(sfd.FileName, _boletos);
                    if (MessageBox.Show(this, "PDF generado correctamente:\n" + sfd.FileName + "\n\n¿Desea abrirlo ahora?",
                                        "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(this, ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error al generar el PDF.\n\n" + ex.Message, "Error",
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
                doc.DocumentName = "Boletos " + _boletos[0].NumeroReserva;
                doc.DefaultPageSettings.Landscape = true;
                doc.BeginPrint += (s, ev) => _siguienteAImprimir = 0;
                doc.PrintPage += ImprimirPagina;

                vista.Document = doc;
                vista.Width = 1000;
                vista.Height = 720;
                vista.StartPosition = FormStartPosition.CenterParent;
                try
                {
                    vista.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "No se pudo preparar la impresión (¿hay una impresora instalada?).\n\n" + ex.Message,
                                    "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

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
    }
}
