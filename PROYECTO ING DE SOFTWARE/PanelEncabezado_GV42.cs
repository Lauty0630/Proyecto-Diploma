using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Banda de título de cada pantalla: degradé azul (los colores del menú) con un avión y rutas
    // punteadas de fondo. Los textos van en Labels comunes con BackColor = Transparent, así el
    // título se sigue traduciendo con el Observer de idiomas.
    [ToolboxItem(true)]
    [Description("Encabezado con degradé azul para el título de la pantalla.")]
    public class PanelEncabezado_GV42 : Panel
    {
        #region Campos

        private Color _colorInicio = Color.FromArgb(13, 71, 161);
        private Color _colorFin = Color.FromArgb(30, 136, 229);
        private bool _mostrarDecoracion = true;

        #endregion

        #region Constructor

        public PanelEncabezado_GV42()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.FromArgb(13, 71, 161);
            ForeColor = Color.White;
            Height = 76;
        }

        #endregion

        #region Propiedades

        [Category("FLY SAFE")]
        [Description("Color del degradé a la izquierda.")]
        [DefaultValue(typeof(Color), "13, 71, 161")]
        public Color ColorInicio
        {
            get { return _colorInicio; }
            set { _colorInicio = value; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [Description("Color del degradé a la derecha.")]
        [DefaultValue(typeof(Color), "30, 136, 229")]
        public Color ColorFin
        {
            get { return _colorFin; }
            set { _colorFin = value; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [Description("Dibuja el avión y las rutas punteadas a la derecha.")]
        [DefaultValue(true)]
        public bool MostrarDecoracion
        {
            get { return _mostrarDecoracion; }
            set { _mostrarDecoracion = value; Invalidate(); }
        }

        #endregion

        #region Dibujo

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (Width <= 0 || Height <= 0) return;

            using (var pincel = new LinearGradientBrush(ClientRectangle, _colorInicio, _colorFin, LinearGradientMode.Horizontal))
                g.FillRectangle(pincel, ClientRectangle);

            if (_mostrarDecoracion && Width > 360)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int x0 = Width - 300;
                using (var lapiz = new Pen(Color.FromArgb(55, Color.White), 1.6f) { DashStyle = DashStyle.Dash })
                {
                    g.DrawBezier(lapiz, x0, Height - 10, x0 + 80, 6, x0 + 170, Height + 10, Width - 70, 20);
                    g.DrawBezier(lapiz, x0 + 60, Height, x0 + 120, Height - 40, x0 + 200, 0, Width - 20, Height - 18);
                }
                using (var pincel = new SolidBrush(Color.FromArgb(40, Color.White)))
                {
                    g.FillEllipse(pincel, Width - 130, -40, 150, 150);
                    g.FillEllipse(pincel, Width - 60, Height - 30, 90, 90);
                }
                using (var fuente = new Font("Segoe UI Symbol", 26F))
                    TextRenderer.DrawText(g, "✈", fuente, new Point(Width - 86, (Height - 46) / 2), Color.FromArgb(215, Color.White));
            }

            // Línea inferior celeste que separa el encabezado del contenido.
            using (var pincel = new SolidBrush(Color.FromArgb(100, 181, 246)))
                g.FillRectangle(pincel, 0, Height - 3, Width, 3);
        }

        #endregion
    }
}
