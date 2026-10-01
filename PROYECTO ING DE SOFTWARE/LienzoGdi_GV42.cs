using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Implementación GDI+ de ILienzo_GV42 (pantalla e impresora). Las fuentes se crean en
    // unidades "World", así el tamaño sigue la misma escala que el resto del dibujo.
    public sealed class LienzoGdi_GV42 : ILienzo_GV42, IDisposable
    {
        #region Campos

        private const string FAMILIA = "Arial";   // métricas equivalentes a Helvetica (la del PDF)
        private readonly Graphics _g;
        private readonly Dictionary<string, Font> _fuentes = new Dictionary<string, Font>();
        private readonly StringFormat _formato;

        #endregion

        #region Constructor

        public LienzoGdi_GV42(Graphics g)
        {
            _g = g;
            _g.SmoothingMode = SmoothingMode.AntiAlias;
            _g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            _formato = (StringFormat)StringFormat.GenericTypographic.Clone();
            _formato.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap;
        }

        #endregion

        #region Métodos públicos

        public void Rectangulo(float x, float y, float ancho, float alto, int? relleno, int? borde, float grosor = 0.8f)
        {
            if (relleno.HasValue)
                using (var b = new SolidBrush(C(relleno.Value)))
                {
                    // Sin antialias para que las barras del código queden nítidas.
                    var modo = _g.SmoothingMode;
                    _g.SmoothingMode = SmoothingMode.None;
                    _g.FillRectangle(b, x, y, ancho, alto);
                    _g.SmoothingMode = modo;
                }
            if (borde.HasValue)
                using (var p = new Pen(C(borde.Value), grosor))
                    _g.DrawRectangle(p, x, y, ancho, alto);
        }

        public void Linea(float x1, float y1, float x2, float y2, int color, float grosor, bool punteada = false)
        {
            using (var p = new Pen(C(color), grosor))
            {
                if (punteada) p.DashPattern = new[] { 3f / Math.Max(grosor, 0.1f), 2f / Math.Max(grosor, 0.1f) };
                _g.DrawLine(p, x1, y1, x2, y2);
            }
        }

        public void Poligono(float[] xs, float[] ys, int relleno)
        {
            var puntos = new PointF[xs.Length];
            for (int i = 0; i < xs.Length; i++) puntos[i] = new PointF(xs[i], ys[i]);
            using (var b = new SolidBrush(C(relleno))) _g.FillPolygon(b, puntos);
        }

        public void Texto(float x, float y, string texto, float tamanio, bool negrita, int color)
        {
            if (string.IsNullOrEmpty(texto)) return;
            Font f = Fuente(tamanio, negrita);
            // y es la línea base: se resta el ascendente de la fuente para obtener el borde superior.
            FontFamily fam = f.FontFamily;
            float ascendente = f.Size * fam.GetCellAscent(f.Style) / fam.GetEmHeight(f.Style);
            using (var b = new SolidBrush(C(color)))
                _g.DrawString(texto, f, b, x, y - ascendente, _formato);
        }

        public float AnchoTexto(string texto, float tamanio, bool negrita)
        {
            if (string.IsNullOrEmpty(texto)) return 0;
            return _g.MeasureString(texto, Fuente(tamanio, negrita), PointF.Empty, _formato).Width;
        }

        public void Dispose()
        {
            foreach (Font f in _fuentes.Values) f.Dispose();
            _fuentes.Clear();
            _formato.Dispose();
        }

        #endregion

        #region Métodos privados

        private static Color C(int rgb) => Color.FromArgb((rgb >> 16) & 0xFF, (rgb >> 8) & 0xFF, rgb & 0xFF);

        private Font Fuente(float tam, bool negrita)
        {
            string clave = tam.ToString("0.##") + (negrita ? "b" : "");
            Font f;
            if (!_fuentes.TryGetValue(clave, out f))
            {
                f = new Font(FAMILIA, tam, negrita ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.World);
                _fuentes[clave] = f;
            }
            return f;
        }

        #endregion
    }
}
