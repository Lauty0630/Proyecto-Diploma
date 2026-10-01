using BE;
using Servicios;
using System;
using System.IO;

namespace BLL
{
    // Documentos que se entregan al terminar el check-in (RFN 2, pasos 9 y 10): la tarjeta de
    // embarque y una etiqueta por bulto despachado. Arma las hojas (horizontales, igual que los
    // boletos) y las exporta a PDF. La UI usa DibujarPagina para imprimir exactamente lo mismo.
    //
    //  Hoja 1: tarjeta de embarque arriba, línea de corte y hasta 5 etiquetas abajo.
    //  Hojas siguientes (si hay más de 5 bultos): 2 filas de 5 etiquetas.
    public class BLLDocumentosCheckIn_GV42
    {
        #region Constantes

        public const float ANCHO_PAGINA = GeneradorPdf_GV42.ANCHO_PAGINA_PT;
        public const float ALTO_PAGINA = GeneradorPdf_GV42.ALTO_PAGINA_PT;

        public const int ETIQUETAS_POR_FILA = 5;
        private const int FILAS_POR_PAGINA_SIGUIENTE = 2;

        private const float MARGEN_SUPERIOR = 24f;
        private const float SEPARACION_ETIQUETAS = 13f;
        private const float SEPARACION_CORTE = 20f;

        #endregion

        #region Armado de las hojas

        // Cantidad de hojas que ocupan la tarjeta y las etiquetas del check-in.
        public static int CantidadPaginas(CheckIn_GV42 ci)
        {
            int restantes = CantidadEtiquetas(ci) - ETIQUETAS_POR_FILA;
            if (restantes <= 0) return 1;
            int porHoja = ETIQUETAS_POR_FILA * FILAS_POR_PAGINA_SIGUIENTE;
            return 1 + (restantes + porHoja - 1) / porHoja;
        }

        // Dibuja la hoja indicada (0 = primera) con su esquina superior izquierda en (x0, y0).
        // La hoja mide ANCHO_PAGINA x ALTO_PAGINA puntos.
        public static void DibujarPagina(ILienzo_GV42 l, CheckIn_GV42 ci, int pagina, float x0, float y0)
        {
            int cantidad = CantidadEtiquetas(ci);

            if (pagina == 0)
            {
                float xTarjeta = x0 + (ANCHO_PAGINA - DisenioTarjetaEmbarque_GV42.ANCHO) / 2;
                float yTarjeta = y0 + MARGEN_SUPERIOR;
                DisenioTarjetaEmbarque_GV42.Dibujar(l, ci, xTarjeta, yTarjeta);
                if (cantidad == 0) return;

                // Línea de corte entre la tarjeta y las etiquetas.
                float yCorte = yTarjeta + DisenioTarjetaEmbarque_GV42.ALTO + SEPARACION_CORTE / 2;
                string aviso = IdiomaManager_GV42.T("etiqueta.recortar");
                float anchoAviso = l.AnchoTexto(aviso, 6.5f, false);
                float xc = x0 + ANCHO_PAGINA / 2;
                l.Linea(xTarjeta, yCorte, xc - anchoAviso / 2 - 8, yCorte, DibujoDocumento_GV42.GRIS_CLARO, 0.6f, true);
                l.Linea(xc + anchoAviso / 2 + 8, yCorte, xTarjeta + DisenioTarjetaEmbarque_GV42.ANCHO, yCorte,
                        DibujoDocumento_GV42.GRIS_CLARO, 0.6f, true);
                l.Texto(xc - anchoAviso / 2, yCorte + 2.5f, aviso, 6.5f, false, DibujoDocumento_GV42.GRIS);

                DibujarFila(l, ci, 0, Math.Min(ETIQUETAS_POR_FILA, cantidad), x0, yCorte + SEPARACION_CORTE / 2);
                return;
            }

            int porHoja = ETIQUETAS_POR_FILA * FILAS_POR_PAGINA_SIGUIENTE;
            int desde = ETIQUETAS_POR_FILA + (pagina - 1) * porHoja;
            float altoBloque = FILAS_POR_PAGINA_SIGUIENTE * DisenioEtiquetaEquipaje_GV42.ALTO
                               + (FILAS_POR_PAGINA_SIGUIENTE - 1) * SEPARACION_ETIQUETAS;
            float y = y0 + (ALTO_PAGINA - altoBloque) / 2;
            for (int fila = 0; fila < FILAS_POR_PAGINA_SIGUIENTE && desde < cantidad; fila++)
            {
                int enFila = Math.Min(ETIQUETAS_POR_FILA, cantidad - desde);
                DibujarFila(l, ci, desde, enFila, x0, y);
                desde += enFila;
                y += DisenioEtiquetaEquipaje_GV42.ALTO + SEPARACION_ETIQUETAS;
            }
        }

        #endregion

        #region Exportación a PDF

        // Guarda la tarjeta de embarque y las etiquetas de equipaje en un PDF.
        public void ExportarPdf(string ruta, CheckIn_GV42 ci)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));
            if (ci == null || ci.TarjetaEmbarque == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("tarjetaEmb.sinTarjeta"));

            var pdf = new LienzoPdf_GV42();
            int paginas = CantidadPaginas(ci);
            for (int p = 0; p < paginas; p++)
            {
                if (p > 0) pdf.NuevaPagina();
                DibujarPagina(pdf, ci, p, 0, 0);
            }

            try
            {
                pdf.Guardar(ruta);
            }
            catch (IOException ex)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.noSePudoGuardar", ex.Message));
            }
            catch (UnauthorizedAccessException)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.sinPermisoCarpeta"));
            }
        }

        #endregion

        #region Métodos privados

        private static int CantidadEtiquetas(CheckIn_GV42 ci)
        {
            return ci != null && ci.Equipaje != null && ci.Equipaje.Etiquetas != null ? ci.Equipaje.Etiquetas.Count : 0;
        }

        // Una fila de etiquetas centrada en la hoja.
        private static void DibujarFila(ILienzo_GV42 l, CheckIn_GV42 ci, int desde, int cantidad, float x0, float y)
        {
            float anchoFila = cantidad * DisenioEtiquetaEquipaje_GV42.ANCHO + (cantidad - 1) * SEPARACION_ETIQUETAS;
            float x = x0 + (ANCHO_PAGINA - anchoFila) / 2;
            for (int i = 0; i < cantidad; i++)
            {
                DisenioEtiquetaEquipaje_GV42.Dibujar(l, ci, desde + i, x, y);
                x += DisenioEtiquetaEquipaje_GV42.ANCHO + SEPARACION_ETIQUETAS;
            }
        }

        #endregion
    }
}
