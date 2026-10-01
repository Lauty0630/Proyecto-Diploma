using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public enum EstiloBoton_GV42 { Primario, Secundario, Peligro, Exito, Advertencia }

    // Botón plano con esquinas redondeadas y estados hover / presionado / deshabilitado.
    // Se usa desde el Form Designer (aparece en el Cuadro de herramientas al compilar):
    // alcanza con elegir el Estilo y el Radio en la ventana Propiedades.
    [ToolboxItem(true)]
    [Description("Botón redondeado con los colores del sistema FLY SAFE.")]
    public class BotonModerno_GV42 : Button
    {
        #region Campos

        private EstiloBoton_GV42 _estilo = EstiloBoton_GV42.Primario;
        private int _radio = 8;
        private bool _sobre;
        private bool _presionado;

        #endregion

        #region Constructor

        public BotonModerno_GV42()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Size = new Size(140, 40);
            UseVisualStyleBackColor = false;
        }

        #endregion

        #region Propiedades

        [Category("FLY SAFE")]
        [Description("Colores del botón: Primario (acción principal), Secundario (borde), Peligro, Éxito o Advertencia.")]
        [DefaultValue(EstiloBoton_GV42.Primario)]
        public EstiloBoton_GV42 Estilo
        {
            get { return _estilo; }
            set { _estilo = value; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [Description("Radio de las esquinas redondeadas, en píxeles.")]
        [DefaultValue(8)]
        public int Radio
        {
            get { return _radio; }
            set { _radio = Math.Max(0, Math.Min(30, value)); Invalidate(); }
        }

        #endregion

        #region Eventos del mouse

        protected override void OnMouseEnter(EventArgs e) { _sobre = true; Invalidate(); base.OnMouseEnter(e); }

        protected override void OnMouseLeave(EventArgs e) { _sobre = false; _presionado = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) { _presionado = true; Invalidate(); } base.OnMouseDown(e); }

        protected override void OnMouseUp(MouseEventArgs e) { _presionado = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        #endregion

        #region Dibujo

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color fondoPadre = Parent != null ? Parent.BackColor : SystemColors.Control;
            if (fondoPadre.A < 255) fondoPadre = Tema_GV42.Fondo;
            g.Clear(fondoPadre);

            Color relleno, borde, texto;
            ObtenerColores(out relleno, out borde, out texto);

            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using (GraphicsPath forma = Tema_GV42.RectanguloRedondeado(rect, _radio))
            {
                using (var pincel = new SolidBrush(relleno)) g.FillPath(pincel, forma);
                using (var lapiz = new Pen(borde, 1f)) g.DrawPath(lapiz, forma);
            }

            if (Focused && ShowFocusCues && Enabled)
            {
                var interior = new RectangleF(3f, 3f, Width - 7f, Height - 7f);
                using (GraphicsPath foco = Tema_GV42.RectanguloRedondeado(interior, Math.Max(0, _radio - 3)))
                using (var lapiz = new Pen(Color.FromArgb(150, texto), 1f) { DashStyle = DashStyle.Dot })
                    g.DrawPath(lapiz, foco);
            }

            Rectangle areaTexto = ClientRectangle;
            if (Image != null)
            {
                int x = Padding.Left + 10;
                int y = (Height - Image.Height) / 2;
                g.DrawImage(Image, x, y, Image.Width, Image.Height);
                areaTexto = new Rectangle(x + Image.Width + 4, 0, Width - x - Image.Width - 14, Height);
            }

            TextRenderer.DrawText(g, Text, Font, areaTexto, texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
        }

        private void ObtenerColores(out Color relleno, out Color borde, out Color texto)
        {
            if (!Enabled)
            {
                relleno = Color.FromArgb(224, 230, 237);
                borde = relleno;
                texto = Color.FromArgb(144, 155, 168);
                return;
            }

            Color baseColor, hover, presionado;
            switch (_estilo)
            {
                case EstiloBoton_GV42.Secundario:
                    relleno = _presionado ? Tema_GV42.BordeGrilla : (_sobre ? Tema_GV42.Fondo : Color.White);
                    borde = Tema_GV42.Primario;
                    texto = Tema_GV42.Acento;
                    return;
                case EstiloBoton_GV42.Peligro:
                    baseColor = Tema_GV42.Error; hover = Color.FromArgb(229, 57, 53); presionado = Color.FromArgb(160, 30, 30);
                    break;
                case EstiloBoton_GV42.Exito:
                    baseColor = Tema_GV42.Exito; hover = Color.FromArgb(67, 160, 71); presionado = Color.FromArgb(27, 94, 32);
                    break;
                case EstiloBoton_GV42.Advertencia:
                    baseColor = Tema_GV42.Advertencia; hover = Color.FromArgb(245, 145, 30); presionado = Color.FromArgb(190, 100, 10);
                    break;
                default:
                    baseColor = Tema_GV42.Primario; hover = Tema_GV42.PrimarioHover; presionado = Tema_GV42.PrimarioPresionado;
                    break;
            }
            relleno = _presionado ? presionado : (_sobre ? hover : baseColor);
            borde = relleno;
            texto = Color.White;
        }

        #endregion
    }
}
