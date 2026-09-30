using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{

    public class GeneradorPdf_GV42
    {
        private const int ANCHO_PAGINA = 792;
        private const int ALTO_PAGINA = 612;
        private const int MARGEN = 50;
        private const int ALTO_FILA = 18;
        private const int FILAS_POR_PAGINA = 22;

        private static readonly CultureInfo INV = CultureInfo.InvariantCulture;

        private MemoryStream _buffer;
        private List<long> _offsetsObjetos;

        public void Generar(string ruta, string titulo, string subtitulo,
                            string[] headers, float[] anchosProporcionales,
                            List<string[]> filas)
        {
            _buffer = new MemoryStream();
            _offsetsObjetos = new List<long>();

            int totalPaginas = Math.Max(1, (int)Math.Ceiling((double)filas.Count / FILAS_POR_PAGINA));

            int idCatalog = 1;
            int idPages = 2;
            int primerIdPagina = 3;
            int idFont = primerIdPagina + totalPaginas;
            int idFontBold = idFont + 1;
            int primerIdContents = idFontBold + 1;

            EscribirHeader();
            EscribirObjeto(idCatalog, $"<</Type/Catalog/Pages {idPages} 0 R>>");

            string kidsStr = string.Join(" ",
                Enumerable.Range(0, totalPaginas).Select(i => $"{primerIdPagina + i} 0 R"));
            EscribirObjeto(idPages, $"<</Type/Pages/Kids[{kidsStr}]/Count {totalPaginas}>>");

            for (int p = 0; p < totalPaginas; p++)
            {
                int idPage = primerIdPagina + p;
                int idContents = primerIdContents + p;
                EscribirObjeto(idPage,
                    $"<</Type/Page/Parent {idPages} 0 R" +
                    $"/MediaBox[0 0 {ANCHO_PAGINA} {ALTO_PAGINA}]" +
                    $"/Resources<</Font<</F1 {idFont} 0 R/F2 {idFontBold} 0 R>>>>" +
                    $"/Contents {idContents} 0 R>>");
            }

            EscribirObjeto(idFont,
                "<</Type/Font/Subtype/Type1/BaseFont/Helvetica/Encoding/WinAnsiEncoding>>");
            EscribirObjeto(idFontBold,
                "<</Type/Font/Subtype/Type1/BaseFont/Helvetica-Bold/Encoding/WinAnsiEncoding>>");

            float anchoUtil = ANCHO_PAGINA - 2 * MARGEN;
            float[] anchos = anchosProporcionales.Select(pct => pct * anchoUtil).ToArray();

            for (int p = 0; p < totalPaginas; p++)
            {
                int idContents = primerIdContents + p;
                int filaInicio = p * FILAS_POR_PAGINA;
                int filaFin = Math.Min(filaInicio + FILAS_POR_PAGINA, filas.Count);

                StringBuilder content = new StringBuilder();
                int yActual = ALTO_PAGINA - MARGEN;

                content.Append("BT\n");
                content.Append("0 0 0 rg\n");
                content.Append("/F2 16 Tf\n");
                content.AppendFormat(INV, "1 0 0 1 {0} {1} Tm\n", MARGEN, yActual - 16);
                content.AppendFormat(INV, "({0}) Tj\n", EscaparTexto(titulo));
                content.Append("ET\n");
                yActual -= 24;

                content.Append("BT\n");
                content.Append("0.45 0.45 0.45 rg\n");
                content.Append("/F1 9 Tf\n");
                content.AppendFormat(INV, "1 0 0 1 {0} {1} Tm\n", MARGEN, yActual);
                string subtitFinal = subtitulo + (totalPaginas > 1 ? $"   |   Pagina {p + 1} de {totalPaginas}" : "");
                content.AppendFormat(INV, "({0}) Tj\n", EscaparTexto(subtitFinal));
                content.Append("ET\n");
                yActual -= 20;

                content.Append("0.7 0.7 0.7 RG\n");
                content.Append("0.6 w\n");
                content.AppendFormat(INV, "{0} {1} m\n", MARGEN, yActual);
                content.AppendFormat(INV, "{0} {1} l\n", MARGEN + anchoUtil, yActual);
                content.Append("S\n");
                yActual -= 14;

                int yHeaderBaseline = yActual;
                content.Append("BT\n");
                content.Append("0 0 0 rg\n");
                content.Append("/F2 10 Tf\n");
                float xCol = MARGEN;
                for (int h = 0; h < headers.Length; h++)
                {
                    content.AppendFormat(INV, "1 0 0 1 {0} {1} Tm\n", xCol, yHeaderBaseline);
                    content.AppendFormat(INV, "({0}) Tj\n", EscaparTexto(headers[h]));
                    xCol += anchos[h];
                }
                content.Append("ET\n");
                yActual -= 8;

                content.Append("0.5 0.5 0.5 RG\n");
                content.Append("0.7 w\n");
                content.AppendFormat(INV, "{0} {1} m\n", MARGEN, yActual);
                content.AppendFormat(INV, "{0} {1} l\n", MARGEN + anchoUtil, yActual);
                content.Append("S\n");
                yActual -= 12;

                for (int f = filaInicio; f < filaFin; f++)
                {
                    string[] valores = filas[f];

                    content.Append("BT\n");
                    content.Append("0 0 0 rg\n");
                    content.Append("/F1 9 Tf\n");
                    xCol = MARGEN;
                    for (int c = 0; c < headers.Length; c++)
                    {
                        string val = c < valores.Length ? (valores[c] ?? "") : "";
                        int maxChars = (int)(anchos[c] / 5);
                        if (val.Length > maxChars && maxChars > 1)
                            val = val.Substring(0, maxChars - 1) + ".";

                        content.AppendFormat(INV, "1 0 0 1 {0} {1} Tm\n", xCol, yActual);
                        content.AppendFormat(INV, "({0}) Tj\n", EscaparTexto(val));
                        xCol += anchos[c];
                    }
                    content.Append("ET\n");

                    yActual -= ALTO_FILA;

                    content.Append("0.88 0.88 0.88 RG\n");
                    content.Append("0.3 w\n");
                    content.AppendFormat(INV, "{0} {1} m\n", MARGEN, yActual + 4);
                    content.AppendFormat(INV, "{0} {1} l\n", MARGEN + anchoUtil, yActual + 4);
                    content.Append("S\n");
                }

                EscribirStreamObjeto(idContents, content.ToString());
            }

            long xrefOffset = _buffer.Position;
            EscribirXref();
            EscribirTrailer(idCatalog, xrefOffset);

            File.WriteAllBytes(ruta, _buffer.ToArray());
        }

        // ------------------------------------------------------------------------------------
        // Tabla con celdas de varias líneas (reportes con muchas columnas, p. ej. reservas).
        // Mismo estilo que Generar (título, subtítulo gris, encabezados en negrita entre líneas),
        // pero cada celda admite saltos de línea ("\n") y el texto largo se parte en renglones
        // según el ancho real de la fuente Helvetica, así no se corta información.
        // alinearDerecha (opcional) indica qué columnas son importes.
        // resumen (opcional) se imprime al final, debajo de la última fila.
        // ------------------------------------------------------------------------------------
        public void GenerarTablaMultilinea(string ruta, string titulo, string[] subtitulos,
                                           string[] headers, float[] anchosProporcionales,
                                           List<string[]> filas, float tamanioFuente = 7f,
                                           bool[] alinearDerecha = null, string[] resumen = null)
        {
            const float MARGEN_M = 30f;
            const float PADDING_CELDA = 5f;
            float interlinea = tamanioFuente + 2f;
            float tamEncabezado = tamanioFuente + 0.5f;

            float anchoUtil = ANCHO_PAGINA - 2 * MARGEN_M;
            float[] anchos = anchosProporcionales.Select(pct => pct * anchoUtil).ToArray();
            subtitulos = subtitulos ?? new string[0];

            // 1) Partir cada celda en renglones.
            var encabezadoLineas = headers.Select((h, c) => Partir(h, anchos[c] - PADDING_CELDA, tamEncabezado, true)).ToList();
            int renglonesEncabezado = encabezadoLineas.Max(l => l.Count);

            var filasLineas = new List<List<List<string>>>();
            foreach (string[] fila in filas)
            {
                var celdas = new List<List<string>>();
                for (int c = 0; c < headers.Length; c++)
                {
                    string val = c < fila.Length ? (fila[c] ?? "") : "";
                    celdas.Add(Partir(val, anchos[c] - PADDING_CELDA, tamanioFuente, false));
                }
                filasLineas.Add(celdas);
            }

            // 2) Paginar según el alto de cada fila.
            float yInicioTabla = ALTO_PAGINA - MARGEN_M - 36 - 13 * subtitulos.Length - 8
                                 - renglonesEncabezado * (tamEncabezado + 2) - 10;
            float yLimite = MARGEN_M + 14;   // deja lugar al número de página
            var paginas = new List<List<int>>();
            var actual = new List<int>();
            float y = yInicioTabla;
            for (int f = 0; f < filasLineas.Count; f++)
            {
                float alto = filasLineas[f].Max(l => l.Count) * interlinea + 6;
                if (y - alto < yLimite && actual.Count > 0)
                {
                    paginas.Add(actual);
                    actual = new List<int>();
                    y = yInicioTabla;
                }
                actual.Add(f);
                y -= alto;
            }
            paginas.Add(actual);

            float altoResumen = resumen == null ? 0 : 10 + resumen.Length * (tamanioFuente + 4);
            if (resumen != null && y - altoResumen < yLimite) paginas.Add(new List<int>());
            int totalPaginas = paginas.Count;

            // 3) Estructura del PDF (igual que Generar).
            _buffer = new MemoryStream();
            _offsetsObjetos = new List<long>();
            int idCatalog = 1, idPages = 2, primerIdPagina = 3;
            int idFont = primerIdPagina + totalPaginas;
            int idFontBold = idFont + 1;
            int primerIdContents = idFontBold + 1;

            EscribirHeader();
            EscribirObjeto(idCatalog, $"<</Type/Catalog/Pages {idPages} 0 R>>");
            string kids = string.Join(" ", Enumerable.Range(0, totalPaginas).Select(i => $"{primerIdPagina + i} 0 R"));
            EscribirObjeto(idPages, $"<</Type/Pages/Kids[{kids}]/Count {totalPaginas}>>");
            for (int p = 0; p < totalPaginas; p++)
            {
                EscribirObjeto(primerIdPagina + p,
                    $"<</Type/Page/Parent {idPages} 0 R" +
                    $"/MediaBox[0 0 {ANCHO_PAGINA} {ALTO_PAGINA}]" +
                    $"/Resources<</Font<</F1 {idFont} 0 R/F2 {idFontBold} 0 R>>>>" +
                    $"/Contents {primerIdContents + p} 0 R>>");
            }
            EscribirObjeto(idFont, "<</Type/Font/Subtype/Type1/BaseFont/Helvetica/Encoding/WinAnsiEncoding>>");
            EscribirObjeto(idFontBold, "<</Type/Font/Subtype/Type1/BaseFont/Helvetica-Bold/Encoding/WinAnsiEncoding>>");

            // 4) Contenido de cada página.
            for (int p = 0; p < totalPaginas; p++)
            {
                var sb = new StringBuilder();
                float yy = ALTO_PAGINA - MARGEN_M;

                Texto(sb, "F2", 16, 0, MARGEN_M, yy - 16, titulo);
                yy -= 36;
                foreach (string sub in subtitulos)
                {
                    Texto(sb, "F1", 9, 0.45f, MARGEN_M, yy, sub);
                    yy -= 13;
                }
                yy -= 2;
                Linea(sb, 0.7f, 0.6f, MARGEN_M, MARGEN_M + anchoUtil, yy);
                yy -= tamEncabezado + 6;

                float x = MARGEN_M;
                for (int c = 0; c < headers.Length; c++)
                {
                    bool der = alinearDerecha != null && c < alinearDerecha.Length && alinearDerecha[c];
                    for (int l = 0; l < encabezadoLineas[c].Count; l++)
                        TextoCelda(sb, "F2", tamEncabezado, 0, x, anchos[c] - PADDING_CELDA, yy - l * (tamEncabezado + 2),
                                   encabezadoLineas[c][l], der, true);
                    x += anchos[c];
                }
                yy -= (renglonesEncabezado - 1) * (tamEncabezado + 2) + 6;
                Linea(sb, 0.5f, 0.7f, MARGEN_M, MARGEN_M + anchoUtil, yy);
                yy -= tamanioFuente + 4;

                foreach (int f in paginas[p])
                {
                    var celdas = filasLineas[f];
                    int renglones = celdas.Max(l => l.Count);
                    x = MARGEN_M;
                    for (int c = 0; c < headers.Length; c++)
                    {
                        bool der = alinearDerecha != null && c < alinearDerecha.Length && alinearDerecha[c];
                        for (int l = 0; l < celdas[c].Count; l++)
                            TextoCelda(sb, "F1", tamanioFuente, 0, x, anchos[c] - PADDING_CELDA, yy - l * interlinea,
                                       celdas[c][l], der, false);
                        x += anchos[c];
                    }
                    yy -= renglones * interlinea + 6;
                    Linea(sb, 0.88f, 0.3f, MARGEN_M, MARGEN_M + anchoUtil, yy + tamanioFuente + 2);
                }

                if (resumen != null && p == totalPaginas - 1)
                {
                    yy -= 6;
                    foreach (string linea in resumen)
                    {
                        Texto(sb, "F2", tamanioFuente + 1, 0, MARGEN_M, yy, linea);
                        yy -= tamanioFuente + 4;
                    }
                }

                if (totalPaginas > 1)
                {
                    string pie = $"Pagina {p + 1} de {totalPaginas}";
                    float anchoPie = AnchoTexto(pie, 8, false);
                    Texto(sb, "F1", 8, 0.45f, ANCHO_PAGINA - MARGEN_M - anchoPie, MARGEN_M - 6, pie);
                }

                EscribirStreamObjeto(primerIdContents + p, sb.ToString());
            }

            long xrefOffset = _buffer.Position;
            EscribirXref();
            EscribirTrailer(idCatalog, xrefOffset);
            File.WriteAllBytes(ruta, _buffer.ToArray());
        }

        private void Texto(StringBuilder sb, string fuente, float tam, float gris, float x, float y, string texto)
        {
            sb.Append("BT\n");
            sb.AppendFormat(INV, "{0} {0} {0} rg\n", gris);
            sb.AppendFormat(INV, "/{0} {1} Tf\n", fuente, tam);
            sb.AppendFormat(INV, "1 0 0 1 {0:0.##} {1:0.##} Tm\n", x, y);
            sb.AppendFormat(INV, "({0}) Tj\n", EscaparTexto(texto));
            sb.Append("ET\n");
        }

        private void TextoCelda(StringBuilder sb, string fuente, float tam, float gris, float x, float ancho,
                                float y, string texto, bool derecha, bool negrita)
        {
            if (string.IsNullOrEmpty(texto)) return;
            float xx = derecha ? x + ancho - AnchoTexto(texto, tam, negrita) : x;
            Texto(sb, fuente, tam, gris, xx, y, texto);
        }

        private void Linea(StringBuilder sb, float gris, float grosor, float x1, float x2, float y)
        {
            sb.AppendFormat(INV, "{0} {0} {0} RG\n{1} w\n", gris, grosor);
            sb.AppendFormat(INV, "{0:0.##} {1:0.##} m\n{2:0.##} {1:0.##} l\nS\n", x1, y, x2);
        }

        // Parte un texto en renglones que entren en el ancho indicado (respeta los "\n").
        private static List<string> Partir(string texto, float ancho, float tam, bool negrita)
        {
            var renglones = new List<string>();
            foreach (string parrafo in (texto ?? "").Replace("\r", "").Split('\n'))
            {
                string actual = "";
                foreach (string palabra in parrafo.Split(' '))
                {
                    string candidato = actual.Length == 0 ? palabra : actual + " " + palabra;
                    if (AnchoTexto(candidato, tam, negrita) <= ancho) { actual = candidato; continue; }

                    if (actual.Length > 0) renglones.Add(actual);
                    actual = palabra;
                    // Palabra más larga que la columna (p. ej. un email): se corta por caracteres.
                    while (AnchoTexto(actual, tam, negrita) > ancho && actual.Length > 1)
                    {
                        int n = actual.Length - 1;
                        while (n > 1 && AnchoTexto(actual.Substring(0, n), tam, negrita) > ancho) n--;
                        renglones.Add(actual.Substring(0, n));
                        actual = actual.Substring(n);
                    }
                }
                renglones.Add(actual);
            }
            return renglones;
        }

        // Ancho aproximado en puntos usando las métricas estándar de Helvetica (1/1000 del tamaño).
        private static float AnchoTexto(string texto, float tam, bool negrita)
        {
            if (string.IsNullOrEmpty(texto)) return 0;
            float total = 0;
            foreach (char ch in texto)
            {
                char c = SinAcento(ch);
                int w = c >= 32 && c <= 126 ? ANCHOS_HELVETICA[c - 32] : 556;
                total += w;
            }
            return total * tam / 1000f * (negrita ? 1.08f : 1f);
        }

        private static char SinAcento(char c)
        {
            if (c == '\u00A0') return ' ';   // espacio duro: mismo ancho que el espacio
            const string con = "áéíóúÁÉÍÓÚñÑüÜ";
            const string sin = "aeiouAEIOUnNuU";
            int i = con.IndexOf(c);
            return i >= 0 ? sin[i] : c;
        }

        private static readonly int[] ANCHOS_HELVETICA =
        {
            278, 278, 355, 556, 556, 889, 667, 191, 333, 333, 389, 584, 278, 333, 278, 278,
            556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 278, 278, 584, 584, 584, 556,
            1015, 667, 667, 722, 722, 667, 611, 778, 722, 278, 500, 667, 556, 833, 722, 778,
            667, 778, 722, 667, 611, 722, 667, 944, 667, 667, 611, 278, 278, 278, 469, 556,
            333, 556, 556, 500, 556, 556, 278, 556, 556, 222, 222, 500, 222, 833, 556, 556,
            556, 556, 333, 500, 278, 556, 500, 722, 500, 500, 500, 334, 260, 334, 584
        };

        // Escribe un PDF con páginas ya dibujadas (contenido de cada página en operadores PDF).
        // Lo usa LienzoPdf_GV42 para los boletos; mismo tamaño de página y fuentes que el resto.
        internal void EscribirDocumento(string ruta, IList<string> paginas)
        {
            _buffer = new MemoryStream();
            _offsetsObjetos = new List<long>();
            int total = Math.Max(1, paginas.Count);
            int idCatalog = 1, idPages = 2, primerIdPagina = 3;
            int idFont = primerIdPagina + total;
            int idFontBold = idFont + 1;
            int primerIdContents = idFontBold + 1;

            EscribirHeader();
            EscribirObjeto(idCatalog, $"<</Type/Catalog/Pages {idPages} 0 R>>");
            string kids = string.Join(" ", Enumerable.Range(0, total).Select(i => $"{primerIdPagina + i} 0 R"));
            EscribirObjeto(idPages, $"<</Type/Pages/Kids[{kids}]/Count {total}>>");
            for (int p = 0; p < total; p++)
                EscribirObjeto(primerIdPagina + p,
                    $"<</Type/Page/Parent {idPages} 0 R/MediaBox[0 0 {ANCHO_PAGINA} {ALTO_PAGINA}]" +
                    $"/Resources<</Font<</F1 {idFont} 0 R/F2 {idFontBold} 0 R>>>>/Contents {primerIdContents + p} 0 R>>");
            EscribirObjeto(idFont, "<</Type/Font/Subtype/Type1/BaseFont/Helvetica/Encoding/WinAnsiEncoding>>");
            EscribirObjeto(idFontBold, "<</Type/Font/Subtype/Type1/BaseFont/Helvetica-Bold/Encoding/WinAnsiEncoding>>");
            for (int p = 0; p < total; p++)
                EscribirStreamObjeto(primerIdContents + p, p < paginas.Count ? paginas[p] : "");

            long xrefOffset = _buffer.Position;
            EscribirXref();
            EscribirTrailer(idCatalog, xrefOffset);
            File.WriteAllBytes(ruta, _buffer.ToArray());
        }

        internal const float ANCHO_PAGINA_PT = ANCHO_PAGINA;
        internal const float ALTO_PAGINA_PT = ALTO_PAGINA;

        internal static float MedirHelvetica(string texto, float tam, bool negrita) => AnchoTexto(texto, tam, negrita);

        internal static string EscaparPdf(string s) =>
            (s ?? "").Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");

        private void EscribirHeader()
        {
            EscribirBytes("%PDF-1.4\n");
            byte[] binario = { (byte)'%', 0xE2, 0xE3, 0xCF, 0xD3, (byte)'\n' };
            _buffer.Write(binario, 0, binario.Length);
        }

        private void EscribirObjeto(int id, string contenido)
        {
            while (_offsetsObjetos.Count < id) _offsetsObjetos.Add(0);
            _offsetsObjetos[id - 1] = _buffer.Position;
            EscribirBytes($"{id} 0 obj\n{contenido}\nendobj\n");
        }

        private void EscribirStreamObjeto(int id, string contenidoStream)
        {
            byte[] streamBytes = Encoding.GetEncoding(1252).GetBytes(contenidoStream);
            while (_offsetsObjetos.Count < id) _offsetsObjetos.Add(0);
            _offsetsObjetos[id - 1] = _buffer.Position;

            EscribirBytes($"{id} 0 obj\n");
            EscribirBytes($"<</Length {streamBytes.Length}>>\nstream\n");
            _buffer.Write(streamBytes, 0, streamBytes.Length);
            EscribirBytes("\nendstream\nendobj\n");
        }

        private void EscribirXref()
        {
            int total = _offsetsObjetos.Count + 1;
            EscribirBytes("xref\n");
            EscribirBytes($"0 {total}\n");
            EscribirBytes("0000000000 65535 f \n");
            foreach (var off in _offsetsObjetos)
            {
                EscribirBytes($"{off.ToString("D10")} 00000 n \n");
            }
        }

        private void EscribirTrailer(int idCatalog, long xrefOffset)
        {
            int total = _offsetsObjetos.Count + 1;
            EscribirBytes($"trailer\n<</Size {total}/Root {idCatalog} 0 R>>\n");
            EscribirBytes($"startxref\n{xrefOffset}\n");
            EscribirBytes("%%EOF\n");
        }

        private void EscribirBytes(string s)
        {
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(s);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        private string EscaparTexto(string s)
        {
            if (s == null) return "";
            return s
                .Replace("\\", "\\\\")
                .Replace("(", "\\(")
                .Replace(")", "\\)");
        }
    }
}
