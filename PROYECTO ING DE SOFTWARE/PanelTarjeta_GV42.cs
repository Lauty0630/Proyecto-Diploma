using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Tarjeta blanca con esquinas redondeadas, borde celeste y una sombra suave abajo.
    // Contenedor para usar desde el Form Designer: se le arrastran adentro los controles.
    [ToolboxItem(true)]
    [Description("Tarjeta blanca redondeada (contenedor) con el estilo de FLY SAFE.")]
    public class PanelTarjeta_GV42 : Panel
    {
        #region Campos

        private int _radio = 12;
        private Color _colorBorde = Color.FromArgb(187, 222, 251);
        private bool _acentoSuperior;

        #endregion

        #region Constructor

        public PanelTarjeta_GV42()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
            Padding = new Padding(16);
        }

        #endregion

        #region Propiedades

        [Category("FLY SAFE")]
        [Description("Radio de las esquinas redondeadas, en píxeles.")]
        [DefaultValue(12)]
        public int Radio
        {
            get { return _radio; }
            set { _radio = Math.Max(0, Math.Min(40, value)); Invalidate(); }
        }

        [Category("FLY SAFE")]
        [Description("Color del borde de la tarjeta.")]
        [DefaultValue(typeof(Color), "187, 222, 251")]
        public Color ColorBorde
        {
            get { return _colorBorde; }
            set { _colorBorde = value; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [Description("Dibuja una franja azul arriba de la tarjeta.")]
        [DefaultValue(false)]
        public bool AcentoSuperior
        {
            get { return _acentoSuperior; }
            set { _acentoSuperior = value; Invalidate(); }
        }

        #endregion

        #region Dibujo

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color fondoPadre = Parent != null ? Parent.BackColor : Tema_GV42.Fondo;
            if (fondoPadre.A < 255) fondoPadre = Tema_GV42.Fondo;
            g.Clear(fondoPadre);

            // Sombra: dos contornos transparentes corridos hacia abajo.
            for (int i = 2; i >= 1; i--)
            {
                var sombra = new RectangleF(0.5f, 0.5f + i, Width - 1.5f, Height - 1.5f - i);
                using (GraphicsPath p = Tema_GV42.RectanguloRedondeado(sombra, _radio))
                using (var pincel = new SolidBrush(Color.FromArgb(18 * i, 13, 71, 161)))
                    g.FillPath(pincel, p);
            }

            var rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 3.5f);
            using (GraphicsPath forma = Tema_GV42.RectanguloRedondeado(rect, _radio))
            {
                using (var pincel = new SolidBrush(BackColor)) g.FillPath(pincel, forma);

                if (_acentoSuperior)
                {
                    Region anterior = g.Clip;
                    g.SetClip(forma, CombineMode.Intersect);
                    using (var pincel = new LinearGradientBrush(new RectangleF(0, 0, Width, 5), Tema_GV42.Acento, Tema_GV42.Primario, LinearGradientMode.Horizontal))
                        g.FillRectangle(pincel, 0, 0, Width, 5);
                    g.Clip = anterior;
                }

                using (var lapiz = new Pen(_colorBorde, 1f)) g.DrawPath(lapiz, forma);
            }
        }

        #endregion
    }
}
