using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BLL
{
    // Implementación PDF de ILienzo_GV42: traduce cada primitiva a operadores PDF
    // (el PDF tiene el origen abajo a la izquierda, por eso se invierte la coordenada y).
    public class LienzoPdf_GV42 : ILienzo_GV42
    {
        private static readonly CultureInfo INV = CultureInfo.InvariantCulture;
        private readonly List<StringBuilder> _paginas = new List<StringBuilder>();
        private StringBuilder _actual;

        public float AnchoPagina => GeneradorPdf_GV42.ANCHO_PAGINA_PT;
        public float AltoPagina => GeneradorPdf_GV42.ALTO_PAGINA_PT;

        public LienzoPdf_GV42()
        {
            NuevaPagina();
        }

        public void NuevaPagina()
        {
            _actual = new StringBuilder();
            _paginas.Add(_actual);
        }

        public void Guardar(string ruta)
        {
            var contenidos = new List<string>();
            foreach (StringBuilder sb in _paginas) contenidos.Add(sb.ToString());
            new GeneradorPdf_GV42().EscribirDocumento(ruta, contenidos);
        }

        private float Y(float y) => AltoPagina - y;

        private static string Color(int rgb)
        {
            return string.Format(INV, "{0:0.###} {1:0.###} {2:0.###}",
                ((rgb >> 16) & 0xFF) / 255f, ((rgb >> 8) & 0xFF) / 255f, (rgb & 0xFF) / 255f);
        }

        public void Rectangulo(float x, float y, float ancho, float alto, int? relleno, int? borde, float grosor = 0.8f)
        {
            if (relleno.HasValue)
                _actual.AppendFormat(INV, "{0} rg {1:0.##} {2:0.##} {3:0.##} {4:0.##} re f\n",
                    Color(relleno.Value), x, Y(y + alto), ancho, alto);
            if (borde.HasValue)
                _actual.AppendFormat(INV, "{0} RG {1:0.##} w {2:0.##} {3:0.##} {4:0.##} {5:0.##} re S\n",
                    Color(borde.Value), grosor, x, Y(y + alto), ancho, alto);
        }

        public void Linea(float x1, float y1, float x2, float y2, int color, float grosor, bool punteada = false)
        {
            if (punteada) _actual.Append("[3 2] 0 d\n");
            _actual.AppendFormat(INV, "{0} RG {1:0.##} w {2:0.##} {3:0.##} m {4:0.##} {5:0.##} l S\n",
                Color(color), grosor, x1, Y(y1), x2, Y(y2));
            if (punteada) _actual.Append("[] 0 d\n");
        }

        public void Poligono(float[] xs, float[] ys, int relleno)
        {
            if (xs == null || xs.Length < 3) return;
            _actual.Append(Color(relleno)).Append(" rg ");
            for (int i = 0; i < xs.Length; i++)
                _actual.AppendFormat(INV, "{0:0.##} {1:0.##} {2} ", xs[i], Y(ys[i]), i == 0 ? "m" : "l");
            _actual.Append("h f\n");
        }

        public void Texto(float x, float y, string texto, float tamanio, bool negrita, int color)
        {
            if (string.IsNullOrEmpty(texto)) return;
            _actual.AppendFormat(INV, "BT /{0} {1:0.##} Tf {2} rg 1 0 0 1 {3:0.##} {4:0.##} Tm ({5}) Tj ET\n",
                negrita ? "F2" : "F1", tamanio, Color(color), x, Y(y), GeneradorPdf_GV42.EscaparPdf(texto));
        }

        public float AnchoTexto(string texto, float tamanio, bool negrita)
        {
            return GeneradorPdf_GV42.MedirHelvetica(texto, tamanio, negrita);
        }
    }
}
