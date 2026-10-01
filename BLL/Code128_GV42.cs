using System;
using System.Collections.Generic;

namespace BLL
{
    // Código de barras Code 128 (juego B: letras, números y signos ASCII 32-126).
    // Devuelve el ancho de cada barra/espacio en módulos, empezando por una barra.
    public static class Code128_GV42
    {
        #region Campos

        private static readonly string[] PATRONES =
        {
            "212222","222122","222221","121223","121322","131222","122213","122312","132212","221213",
            "221312","231212","112232","122132","122231","113222","123122","123221","223211","221132",
            "221231","213212","223112","312131","311222","321122","321221","312212","322112","322211",
            "212123","212321","232121","111323","131123","131321","112313","132113","132311","211313",
            "231113","231311","112133","112331","132131","113123","113321","133121","313121","211331",
            "231131","213113","213311","213131","311123","311321","331121","312113","312311","332111",
            "314111","221411","431111","111224","111422","121124","121421","141122","141221","112214",
            "112412","122114","122411","142112","142211","241211","221114","413111","241112","134111",
            "111242","121142","121241","114212","124112","124211","411212","421112","421211","212141",
            "214121","412121","111143","111341","131141","114113","114311","411113","411311","113141",
            "114131","311141","411131","211412","211214","211232","2331112"
        };
        private const int INICIO_B = 104;
        private const int PARADA = 106;

        #endregion

        #region Métodos públicos

        public static List<int> Codificar(string texto)
        {
            texto = texto ?? string.Empty;
            var valores = new List<int> { INICIO_B };
            foreach (char c in texto)
            {
                if (c < 32 || c > 126) throw new ArgumentException("Carácter no soportado en el código de barras: " + c);
                valores.Add(c - 32);
            }

            int suma = INICIO_B;
            for (int i = 1; i < valores.Count; i++) suma += valores[i] * i;
            valores.Add(suma % 103);
            valores.Add(PARADA);

            var modulos = new List<int>();
            foreach (int v in valores)
                foreach (char d in PATRONES[v]) modulos.Add(d - '0');
            return modulos;
        }

        // Dibuja el código ocupando el ancho indicado (deja 10 módulos de zona blanca a cada lado).
        public static void Dibujar(ILienzo_GV42 lienzo, string texto, float x, float y, float ancho, float alto)
        {
            List<int> modulos = Codificar(texto);
            int total = 20;
            foreach (int m in modulos) total += m;
            float modulo = ancho / total;

            float xx = x + 10 * modulo;
            bool barra = true;
            foreach (int m in modulos)
            {
                if (barra) lienzo.Rectangulo(xx, y, m * modulo, alto, 0x000000, null);
                xx += m * modulo;
                barra = !barra;
            }
        }

        #endregion
    }
}
