using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Vista previa de la tarjeta de crédito / débito que se está cargando en "Registrar pago".
    // Se dibuja con GDI+ (degradé azul, chip, número enmascarado, titular, vencimiento y marca)
    // y se actualiza en vivo a medida que el usuario tipea. Nunca muestra el número completo:
    // solo los primeros 4 dígitos (identifican la marca) y los últimos 4.
    [ToolboxItem(true)]
    [Description("Vista previa de una tarjeta de pago (se actualiza en vivo).")]
    public class CtrlVistaTarjeta_GV42 : Control
    {
        #region Campos

        private string _digitos = string.Empty;
        private string _titular = string.Empty;
        private string _vencimiento = string.Empty;
        private string _marca = string.Empty;
        private bool _esAmex;
        private string _textoTitularVacio = "NOMBRE DEL TITULAR";
        private string _etiquetaVence = "VENCE";
        private string _etiquetaTitular = "TITULAR";
        private string _formatoVencimiento = "MM/AA";

        private static readonly Color ColorInicio = Color.FromArgb(13, 71, 161);
        private static readonly Color ColorFin = Color.FromArgb(66, 165, 245);

        #endregion

        #region Constructor

        public CtrlVistaTarjeta_GV42()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.White;
            Size = new Size(300, 189);
        }

        #endregion

        #region Propiedades

        // Número tal como se tipeó (se toman solo los dígitos).
        [Category("FLY SAFE")]
        [DefaultValue("")]
        public string Numero
        {
            get { return _digitos; }
            set
            {
                var sb = new StringBuilder();
                foreach (char c in value ?? string.Empty) if (char.IsDigit(c)) sb.Append(c);
                _digitos = sb.ToString();
                Invalidate();
            }
        }

        [Category("FLY SAFE")]
        [DefaultValue("")]
        public string Titular
        {
            get { return _titular; }
            set { _titular = value ?? string.Empty; Invalidate(); }
        }

        // Texto ya armado, por ejemplo "08/29".
        [Category("FLY SAFE")]
        [DefaultValue("")]
        public string Vencimiento
        {
            get { return _vencimiento; }
            set { _vencimiento = value ?? string.Empty; Invalidate(); }
        }

        // Marca ya traducida (Visa, Mastercard, ...). Vacío si todavía no se reconoce.
        [Category("FLY SAFE")]
        [DefaultValue("")]
        public string Marca
        {
            get { return _marca; }
            set { _marca = value ?? string.Empty; Invalidate(); }
        }

        // American Express agrupa 15 dígitos (4-6-5) en vez de 16 (4-4-4-4).
        [Category("FLY SAFE")]
        [DefaultValue(false)]
        public bool EsAmex
        {
            get { return _esAmex; }
            set { _esAmex = value; Invalidate(); }
        }

        // Textos fijos de la tarjeta: los asigna el formulario en ActualizarIdioma().
        [Category("FLY SAFE")]
        [DefaultValue("NOMBRE DEL TITULAR")]
        public string TextoTitularVacio
        {
            get { return _textoTitularVacio; }
            set { _textoTitularVacio = value ?? string.Empty; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [DefaultValue("VENCE")]
        public string EtiquetaVence
        {
            get { return _etiquetaVence; }
            set { _etiquetaVence = value ?? string.Empty; Invalidate(); }
        }

        [Category("FLY SAFE")]
        [DefaultValue("TITULAR")]
        public string EtiquetaTitular
        {
            get { return _etiquetaTitular; }
            set { _etiquetaTitular = value ?? string.Empty; Invalidate(); }
        }

        // Lo que se muestra mientras no se eligió el vencimiento ("MM/AA" o "MM/YY").
        [Category("FLY SAFE")]
        [DefaultValue("MM/AA")]
        public string FormatoVencimiento
        {
            get { return _formatoVencimiento; }
            set { _formatoVencimiento = value ?? string.Empty; Invalidate(); }
        }

        #endregion

        #region Formato

        // "4509 •••• •••• 3704": muestra los primeros 4 y los últimos 4 dígitos tipeados; el resto
        // (y lo que falta tipear) va como punto.
        private string NumeroEnmascarado()
        {
            int largo = Math.Max(_esAmex ? 15 : 16, _digitos.Length);
            int[] grupos = _esAmex && largo == 15 ? new[] { 4, 6, 5 } : null;

            var sb = new StringBuilder();
            int enGrupo = 0, indiceGrupo = 0;
            for (int i = 0; i < largo; i++)
            {
                char c;
                if (i < _digitos.Length && (i < 4 || i >= _digitos.Length - 4)) c = _digitos[i];
                else c = '•';
                sb.Append(c);

                enGrupo++;
                int tamGrupo = grupos != null ? grupos[Math.Min(indiceGrupo, grupos.Length - 1)] : 4;
                if (enGrupo == tamGrupo && i < largo - 1)
                {
                    sb.Append(' ');
                    enGrupo = 0;
                    indiceGrupo++;
                }
            }
            return sb.ToString();
        }

        #endregion

        #region Dibujo

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            Color fondoPadre = Parent != null ? Parent.BackColor : Color.White;
            if (fondoPadre.A < 255) fondoPadre = Color.White;
            g.Clear(fondoPadre);

            // La tarjeta mantiene la proporción de una tarjeta real (300 x 189) y se centra en el control.
            float esc = Math.Min((Width - 1) / 300f, (Height - 4) / 189f);
            if (esc <= 0.15f) return;
            float w = 300 * esc, h = 189 * esc;
            g.TranslateTransform((float)Math.Floor((Width - 1 - w) / 2), 0);

            // Sombra suave.
            using (GraphicsPath sombra = Tema_GV42.RectanguloRedondeado(new RectangleF(2, 4, w - 2, h - 1), 14 * esc))
            using (var pincel = new SolidBrush(Color.FromArgb(40, 13, 71, 161)))
                g.FillPath(pincel, sombra);

            var rect = new RectangleF(0, 0, w - 1, h - 2);
            using (GraphicsPath forma = Tema_GV42.RectanguloRedondeado(rect, 14 * esc))
            {
                using (var degrade = new LinearGradientBrush(rect, ColorInicio, ColorFin, 35f))
                    g.FillPath(degrade, forma);

                // Brillos decorativos (dos círculos translúcidos).
                Region anterior = g.Clip;
                g.SetClip(forma, CombineMode.Intersect);
                using (var brillo = new SolidBrush(Color.FromArgb(28, 255, 255, 255)))
                {
                    g.FillEllipse(brillo, w * 0.55f, -h * 0.45f, w * 0.8f, h * 1.1f);
                    g.FillEllipse(brillo, -w * 0.25f, h * 0.55f, w * 0.6f, h * 0.8f);
                }
                g.Clip = anterior;
            }

            // Chip.
            var chip = new RectangleF(22 * esc, 58 * esc, 38 * esc, 28 * esc);
            using (GraphicsPath pChip = Tema_GV42.RectanguloRedondeado(chip, 5 * esc))
            using (var pincel = new LinearGradientBrush(chip, Color.FromArgb(255, 224, 130), Color.FromArgb(214, 170, 60), 45f))
            using (var lapiz = new Pen(Color.FromArgb(150, 120, 80, 20), 1f))
            {
                g.FillPath(pincel, pChip);
                g.DrawLine(lapiz, chip.Left, chip.Top + chip.Height / 2, chip.Right, chip.Top + chip.Height / 2);
                g.DrawLine(lapiz, chip.Left + chip.Width / 3, chip.Top, chip.Left + chip.Width / 3, chip.Bottom);
                g.DrawLine(lapiz, chip.Left + 2 * chip.Width / 3, chip.Top, chip.Left + 2 * chip.Width / 3, chip.Bottom);
            }

            // Ondas de "sin contacto".
            using (var lapiz = new Pen(Color.FromArgb(200, 255, 255, 255), 1.6f * esc))
            {
                for (int i = 0; i < 3; i++)
                {
                    float r = (7 + i * 5) * esc;
                    g.DrawArc(lapiz, 72 * esc - r, 72 * esc - r, 2 * r, 2 * r, -40, 80);
                }
            }

            // Marca (arriba a la derecha) y nombre del sistema (arriba a la izquierda).
            using (var fMarca = new Font("Segoe UI", 15f * esc, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel))
            using (var fSistema = new Font("Segoe UI Semibold", 11f * esc, FontStyle.Bold, GraphicsUnit.Pixel))
            using (var blanco = new SolidBrush(Color.White))
            using (var blancoSuave = new SolidBrush(Color.FromArgb(210, 255, 255, 255)))
            {
                g.DrawString("FLY SAFE", fSistema, blancoSuave, 22 * esc, 18 * esc);
                if (_marca.Length > 0)
                {
                    string m = _marca.ToUpperInvariant();
                    SizeF t = g.MeasureString(m, fMarca);
                    g.DrawString(m, fMarca, blanco, w - 18 * esc - t.Width, 14 * esc);
                }
            }

            // Número.
            using (var f = new Font("Consolas", 18f * esc, FontStyle.Bold, GraphicsUnit.Pixel))
            using (var blanco = new SolidBrush(Color.White))
            {
                string numero = NumeroEnmascarado();
                SizeF t = g.MeasureString(numero, f);
                float x = Math.Max(16 * esc, (w - t.Width) / 2);
                g.DrawString(numero, f, blanco, x, 100 * esc);
            }

            // Titular y vencimiento.
            using (var fEtiqueta = new Font("Segoe UI", 8f * esc, FontStyle.Regular, GraphicsUnit.Pixel))
            using (var fValor = new Font("Segoe UI Semibold", 12f * esc, FontStyle.Bold, GraphicsUnit.Pixel))
            using (var suave = new SolidBrush(Color.FromArgb(190, 227, 242, 253)))
            using (var blanco = new SolidBrush(Color.White))
            {
                float yEtiqueta = 140 * esc, yValor = 152 * esc;
                string titular = _titular.Trim().Length > 0 ? _titular.Trim().ToUpperInvariant() : _textoTitularVacio;
                if (titular.Length > 26) titular = titular.Substring(0, 26) + "…";
                g.DrawString(_etiquetaTitular.ToUpperInvariant(), fEtiqueta, suave, 22 * esc, yEtiqueta);
                g.DrawString(titular, fValor, blanco, 22 * esc, yValor);

                string venc = _vencimiento.Length > 0 ? _vencimiento : _formatoVencimiento;
                SizeF tv = g.MeasureString(venc, fValor);
                float xVenc = w - 22 * esc - Math.Max(tv.Width, 40 * esc);
                g.DrawString(_etiquetaVence.ToUpperInvariant(), fEtiqueta, suave, xVenc, yEtiqueta);
                g.DrawString(venc, fValor, blanco, xVenc, yValor);
            }
        }

        #endregion
    }
}
