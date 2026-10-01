using BE;
using Servicios;
using System;
using System.Globalization;

namespace BLL
{
    // Piezas de dibujo comunes a la tarjeta de embarque y a la etiqueta de equipaje (mismo lenguaje
    // visual que el boleto electrónico): paleta, textos que se achican si no entran, el avión y los
    // datos del vuelo que salen del check-in.
    internal static class DibujoDocumento_GV42
    {
        #region Constantes

        internal const int AZUL = 0x1976D2;
        internal const int AZUL_OSCURO = 0x0D47A1;
        internal const int CELESTE = 0xE3F2FD;
        internal const int CELESTE_BORDE = 0xBBDEFB;
        internal const int TEXTO = 0x212121;
        internal const int GRIS = 0x757575;
        internal const int GRIS_CLARO = 0xBDBDBD;
        internal const int BLANCO = 0xFFFFFF;
        internal const int VERDE = 0x2E7D32;
        internal const int NARANJA = 0xE67E0F;

        #endregion

        #region Campos

        // Importes y pesos con formato argentino ("$ 1.234,56"): la moneda es el peso.
        internal static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private static readonly CultureInfo CulturaIngles = new CultureInfo("en-US");

        #endregion

        #region Propiedades

        // Cultura para los nombres de días y meses de las fechas.
        internal static CultureInfo CulturaFechas => IdiomaManager_GV42.Instancia.EsIngles ? CulturaIngles : Cultura;

        #endregion

        #region Textos

        // Etiqueta chica gris arriba y valor en negrita abajo.
        internal static void Campo(ILienzo_GV42 l, float x, float y, string etiqueta, string valor,
                                   float tamValor, float anchoMax, int color = TEXTO, int colorEtiqueta = GRIS)
        {
            TextoAjustado(l, x, y, etiqueta, 6.5f, true, colorEtiqueta, anchoMax);
            TextoAjustado(l, x, y + tamValor + 4, valor ?? "", tamValor, true, color, anchoMax);
        }

        // Si el texto no entra, achica la letra (hasta 6 pt) y, si aún no entra, lo recorta.
        internal static void TextoAjustado(ILienzo_GV42 l, float x, float y, string texto, float tam,
                                           bool negrita, int color, float anchoMax)
        {
            texto = texto ?? "";
            float t = AjustarTamanio(l, texto, tam, negrita, anchoMax);
            if (l.AnchoTexto(texto, t, negrita) > anchoMax)
            {
                while (texto.Length > 1 && l.AnchoTexto(texto + "...", t, negrita) > anchoMax)
                    texto = texto.Substring(0, texto.Length - 1);
                texto = texto.TrimEnd() + "...";
            }
            l.Texto(x, y, texto, t, negrita, color);
        }

        internal static void TextoDerecha(ILienzo_GV42 l, float xDer, float y, string texto, float tam,
                                          bool negrita, int color, float anchoMax)
        {
            texto = texto ?? "";
            float t = AjustarTamanio(l, texto, tam, negrita, anchoMax);
            l.Texto(xDer - l.AnchoTexto(texto, t, negrita), y, texto, t, negrita, color);
        }

        // Centrado en xCentro; si no entra en anchoMax, se achica.
        internal static void TextoCentrado(ILienzo_GV42 l, float xCentro, float y, string texto, float tam,
                                           bool negrita, int color, float anchoMax = float.MaxValue)
        {
            texto = texto ?? "";
            float t = AjustarTamanio(l, texto, tam, negrita, anchoMax);
            l.Texto(xCentro - l.AnchoTexto(texto, t, negrita) / 2, y, texto, t, negrita, color);
        }

        // Rótulo con fondo de color (ej: "ASIENTO PREFERENCIAL"). Devuelve el ancho ocupado.
        internal static float Pastilla(ILienzo_GV42 l, float x, float y, string texto, float tam, int fondo, int color)
        {
            float ancho = l.AnchoTexto(texto, tam, true) + 8;
            l.Rectangulo(x, y, ancho, tam + 5, fondo, null);
            l.Texto(x + 4, y + tam + 1.5f, texto, tam, true, color);
            return ancho;
        }

        private static float AjustarTamanio(ILienzo_GV42 l, string texto, float tam, bool negrita, float anchoMax)
        {
            float t = tam;
            while (t > 6f && l.AnchoTexto(texto, t, negrita) > anchoMax) t -= 0.5f;
            return t;
        }

        #endregion

        #region Figuras

        // Silueta simple de avión mirando a la derecha, centrada en (cx, cy).
        internal static void Avion(ILienzo_GV42 l, float cx, float cy, int color, float escala = 1f)
        {
            float s = escala;
            // fuselaje
            l.Poligono(new[] { cx - 11 * s, cx + 9 * s, cx + 12 * s, cx + 9 * s, cx - 11 * s },
                       new[] { cy - 1.6f * s, cy - 1.6f * s, cy, cy + 1.6f * s, cy + 1.6f * s }, color);
            // alas
            l.Poligono(new[] { cx - 2 * s, cx + 3 * s, cx - 4 * s, cx - 7 * s },
                       new[] { cy - 1 * s, cy - 1 * s, cy - 10 * s, cy - 10 * s }, color);
            l.Poligono(new[] { cx - 2 * s, cx + 3 * s, cx - 4 * s, cx - 7 * s },
                       new[] { cy + 1 * s, cy + 1 * s, cy + 10 * s, cy + 10 * s }, color);
            // cola
            l.Poligono(new[] { cx - 9 * s, cx - 6 * s, cx - 9 * s, cx - 11 * s },
                       new[] { cy - 1 * s, cy - 1 * s, cy - 5 * s, cy - 5 * s }, color);
            l.Poligono(new[] { cx - 9 * s, cx - 6 * s, cx - 9 * s, cx - 11 * s },
                       new[] { cy + 1 * s, cy + 1 * s, cy + 5 * s, cy + 5 * s }, color);
        }

        #endregion

        #region Datos del vuelo

        // Código IATA de origen/destino: del vuelo cargado o, si no está, del texto "Ciudad (IATA)" de la tarjeta.
        internal static string OrigenIata(CheckIn_GV42 ci)
        {
            Vuelo_GV42 v = ci.Vuelo;
            if (v != null && v.Origen != null && !string.IsNullOrEmpty(v.Origen.CodigoIata)) return v.Origen.CodigoIata;
            return IataDeTexto(ci.TarjetaEmbarque != null ? ci.TarjetaEmbarque.Origen : null);
        }

        internal static string DestinoIata(CheckIn_GV42 ci)
        {
            Vuelo_GV42 v = ci.Vuelo;
            if (v != null && v.Destino != null && !string.IsNullOrEmpty(v.Destino.CodigoIata)) return v.Destino.CodigoIata;
            return IataDeTexto(ci.TarjetaEmbarque != null ? ci.TarjetaEmbarque.Destino : null);
        }

        internal static string OrigenCiudad(CheckIn_GV42 ci)
        {
            Vuelo_GV42 v = ci.Vuelo;
            if (v != null && v.Origen != null && !string.IsNullOrEmpty(v.Origen.Ciudad)) return v.Origen.Ciudad;
            return CiudadDeTexto(ci.TarjetaEmbarque != null ? ci.TarjetaEmbarque.Origen : null);
        }

        internal static string DestinoCiudad(CheckIn_GV42 ci)
        {
            Vuelo_GV42 v = ci.Vuelo;
            if (v != null && v.Destino != null && !string.IsNullOrEmpty(v.Destino.Ciudad)) return v.Destino.Ciudad;
            return CiudadDeTexto(ci.TarjetaEmbarque != null ? ci.TarjetaEmbarque.Destino : null);
        }

        internal static string Aerolinea(CheckIn_GV42 ci)
        {
            Vuelo_GV42 v = ci.Vuelo;
            return v != null && v.Aerolinea != null ? v.Aerolinea.Nombre ?? "" : "";
        }

        // "Sáb 04 oct 2026" en español / "Sat 04 Oct 2026" en inglés.
        internal static string Fecha(DateTime f)
        {
            CultureInfo c = CulturaFechas;
            string dia = f.ToString("ddd", c).TrimEnd('.');
            if (dia.Length == 0) return f.ToString("dd/MM/yyyy");
            return char.ToUpper(dia[0]) + dia.Substring(1) + " " + f.ToString("dd MMM yyyy", c).Replace(".", "");
        }

        // "04OCT" / "04 OCT": fecha corta en mayúsculas para la etiqueta de equipaje.
        internal static string FechaCorta(DateTime f)
        {
            return f.ToString("dd MMM", CulturaFechas).Replace(".", "").ToUpper(CulturaFechas);
        }

        internal static string Kilos(decimal kg) => kg.ToString("0.#", Cultura) + " kg";

        internal static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);

        private static string IataDeTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            int a = texto.LastIndexOf('('), c = texto.LastIndexOf(')');
            return a >= 0 && c > a ? texto.Substring(a + 1, c - a - 1).Trim() : texto.Trim();
        }

        private static string CiudadDeTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            int a = texto.LastIndexOf('(');
            return a > 0 ? texto.Substring(0, a).Trim() : "";
        }

        #endregion
    }
}
