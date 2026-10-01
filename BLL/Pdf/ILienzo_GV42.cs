namespace BLL
{
    // Superficie de dibujo mínima (en puntos, origen arriba a la izquierda). El diseño del boleto
    // se escribe una sola vez contra esta interfaz y se dibuja igual en pantalla/impresora
    // (implementación GDI+ en la UI) y en PDF (LienzoPdf_GV42).
    // Los colores son RGB en un entero: 0xRRGGBB.
    public interface ILienzo_GV42
    {
        void Rectangulo(float x, float y, float ancho, float alto, int? relleno, int? borde, float grosor = 0.8f);
        void Linea(float x1, float y1, float x2, float y2, int color, float grosor, bool punteada = false);
        void Poligono(float[] xs, float[] ys, int relleno);
        // y es la línea base del texto.
        void Texto(float x, float y, string texto, float tamanio, bool negrita, int color);
        float AnchoTexto(string texto, float tamanio, bool negrita);
    }
}
