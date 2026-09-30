using BE;
using System;
using System.Globalization;

namespace BLL
{
    // Diseño visual del boleto electrónico (en puntos). Se dibuja sobre cualquier ILienzo_GV42:
    // pantalla, impresora o PDF, así los tres muestran exactamente el mismo boleto.
    //
    //  ┌──────────────────────────── encabezado (marca, aerolínea) ─────────┬─ talón ─────┐
    //  │ PASAJERO / DNI / BOLETO N°                                         ┊ pasajero    │
    //  │ EZE ─────✈───── MDQ        SALIDA 08:00   LLEGADA 09:10            ┊ EZE > MDQ   │
    //  │ VUELO | CLASE | ASIENTO | PUERTA | EMBARQUE | EQUIPAJE             ┊ vuelo/fecha │
    //  │ RESERVA | EMISIÓN | TARIFA | IMPUESTOS | FORMA DE PAGO | ESTADO    ┊ asiento...  │
    //  │ ||||| código de barras |||||   avisos                              ┊ |||||||||   │
    //  └────────────────────────────────────────────────────────────────────┴─────────────┘
    public static class DisenioBoleto_GV42
    {
        public const float ANCHO = 732f;
        public const float ALTO = 270f;

        private const float ANCHO_PRINCIPAL = 548f;   // el resto es el talón
        private const float ALTO_ENCABEZADO = 40f;

        private const int AZUL = 0x1976D2;
        private const int AZUL_OSCURO = 0x0D47A1;
        private const int CELESTE = 0xE3F2FD;
        private const int TEXTO = 0x212121;
        private const int GRIS = 0x757575;
        private const int GRIS_CLARO = 0xBDBDBD;
        private const int BLANCO = 0xFFFFFF;
        private const int VERDE = 0x2E7D32;

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        public static void Dibujar(ILienzo_GV42 l, BoletoElectronico_GV42 b, float x0, float y0)
        {
            // ---- Marco, talón y encabezado ----
            l.Rectangulo(x0, y0, ANCHO, ALTO, BLANCO, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO, CELESTE, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO_ENCABEZADO, AZUL, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO_ENCABEZADO, AZUL_OSCURO, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO, null, GRIS_CLARO, 0.8f);
            l.Linea(x0 + ANCHO_PRINCIPAL, y0 + ALTO_ENCABEZADO, x0 + ANCHO_PRINCIPAL, y0 + ALTO, GRIS, 0.8f, true);

            l.Texto(x0 + 16, y0 + 27, "FLY SAFE", 18, true, BLANCO);
            l.Texto(x0 + 118, y0 + 18, "BOLETO ELECTRÓNICO", 9, true, BLANCO);
            l.Texto(x0 + 118, y0 + 30, "Pasaje " + (b.TipoViaje ?? "").ToLower(Cultura) + " - Pasajero " + b.NumeroPasajero + " de " + b.TotalPasajeros, 7.5f, false, BLANCO);
            TextoDerecha(l, x0 + ANCHO_PRINCIPAL - 14, y0 + 26, (b.Aerolinea ?? "").ToUpper(Cultura), 12, true, BLANCO, 220);
            l.Texto(x0 + ANCHO_PRINCIPAL + 14, y0 + 18, "TALÓN DEL PASAJERO", 8, true, BLANCO);
            l.Texto(x0 + ANCHO_PRINCIPAL + 14, y0 + 30, b.NumeroBoleto, 7.5f, false, BLANCO);

            // ---- Pasajero ----
            float x = x0 + 16, y = y0 + 58;
            Campo(l, x, y, "PASAJERO", b.PasajeroParaBoleto, 13, 290);
            Campo(l, x0 + 320, y, "DNI", b.PasajeroDni, 11, 100);
            Campo(l, x0 + 430, y, "BOLETO N°", b.NumeroBoleto, 11, 105);

            // ---- Ruta: origen ───✈─── destino ----
            float yRuta = y0 + 124;
            l.Texto(x, yRuta, b.OrigenIata, 30, true, AZUL_OSCURO);
            TextoAjustado(l, x, yRuta + 14, b.OrigenCiudad, 8.5f, true, TEXTO, 120);
            TextoAjustado(l, x, yRuta + 25, b.OrigenAeropuerto, 6.5f, false, GRIS, 120);

            float xDestino = x0 + 222;
            l.Texto(xDestino, yRuta, b.DestinoIata, 30, true, AZUL_OSCURO);
            TextoAjustado(l, xDestino, yRuta + 14, b.DestinoCiudad, 8.5f, true, TEXTO, 120);
            TextoAjustado(l, xDestino, yRuta + 25, b.DestinoAeropuerto, 6.5f, false, GRIS, 120);

            float xa = x + l.AnchoTexto(b.OrigenIata, 30, true) + 10, xb = xDestino - 10, ym = yRuta - 11;
            l.Linea(xa, ym, xb, ym, GRIS, 0.8f, true);
            Avion(l, (xa + xb) / 2, ym, AZUL);
            TextoCentrado(l, (xa + xb) / 2, ym - 13, Duracion(b.Duracion), 7, false, GRIS);
            TextoCentrado(l, (xa + xb) / 2, ym + 19, "Directo", 7, false, GRIS);

            // Horarios
            float xs = x0 + 360;
            l.Texto(xs, y0 + 96, "SALIDA", 6.5f, true, GRIS);
            l.Texto(xs, y0 + 118, b.FechaHoraSalida.ToString("HH:mm"), 20, true, TEXTO);
            l.Texto(xs, y0 + 131, Fecha(b.FechaHoraSalida), 7.5f, false, TEXTO);
            float xll = x0 + 452;
            l.Texto(xll, y0 + 96, "LLEGADA", 6.5f, true, GRIS);
            l.Texto(xll, y0 + 118, b.FechaHoraLlegada.ToString("HH:mm"), 20, true, TEXTO);
            l.Texto(xll, y0 + 131, Fecha(b.FechaHoraLlegada), 7.5f, false, TEXTO);
            l.Texto(xs, y0 + 146, "Horarios locales", 6, false, GRIS);

            // ---- Datos del vuelo ----
            float yFila = y0 + 172, anchoCelda = (ANCHO_PRINCIPAL - 32) / 6f;
            l.Linea(x0 + 16, yFila - 14, x0 + ANCHO_PRINCIPAL - 16, yFila - 14, GRIS_CLARO, 0.6f);
            string[] etiquetas = { "VUELO", "CLASE", "ASIENTO", "PUERTA", "EMBARQUE", "EQUIPAJE" };
            string[] valores =
            {
                b.CodigoVuelo, b.Clase,
                string.IsNullOrEmpty(b.Asiento) ? "A asignar" : b.Asiento,
                string.IsNullOrWhiteSpace(b.PuertaEmbarque) ? "A confirmar" : b.PuertaEmbarque,
                b.HoraEmbarque.ToString("HH:mm"),
                b.FranquiciaEquipajeKg.ToString("0.#", Cultura) + " kg"
            };
            for (int i = 0; i < 6; i++)
                Campo(l, x0 + 16 + i * anchoCelda, yFila, etiquetas[i], valores[i], 11, anchoCelda - 6);
            if (!string.IsNullOrEmpty(b.UbicacionAsiento))
                l.Texto(x0 + 16 + 2 * anchoCelda, yFila + 26, b.UbicacionAsiento, 6.5f, false, GRIS);
            l.Texto(x0 + 16 + 4 * anchoCelda, yFila + 26, "Cierra " + b.CierreEmbarque.ToString("HH:mm"), 6.5f, false, GRIS);
            l.Texto(x0 + 16 + 5 * anchoCelda, yFila + 26, "en bodega", 6.5f, false, GRIS);

            // ---- Datos comerciales ----
            float yCom = y0 + 214;
            l.Linea(x0 + 16, yCom - 12, x0 + ANCHO_PRINCIPAL - 16, yCom - 12, GRIS_CLARO, 0.6f);
            string[] etiquetas2 = { "RESERVA", "EMISIÓN", "TARIFA", "IMPUESTOS", "FORMA DE PAGO", "ESTADO" };
            string[] valores2 =
            {
                b.NumeroReserva, b.FechaEmision.ToString("dd/MM/yyyy"),
                Dinero(b.TarifaPasajero), Dinero(b.ImpuestosPasajero), b.FormaPago, b.Estado
            };
            for (int i = 0; i < 6; i++)
                Campo(l, x0 + 16 + i * anchoCelda, yCom, etiquetas2[i], valores2[i], 8.5f, anchoCelda - 6,
                      i == 5 ? VERDE : TEXTO);

            // ---- Código de barras y avisos ----
            Code128_GV42.Dibujar(l, b.CodigoBarras, x0 + 8, y0 + 232, 250, 24);
            TextoCentrado(l, x0 + 133, y0 + 264, b.CodigoBarras, 6, false, GRIS);

            float xn = x0 + 270;
            TextoAjustado(l, xn, y0 + 238, "Preséntese con su DNI 2 h antes de la salida. El embarque cierra " +
                                          "15 min antes.", 6.3f, false, GRIS, ANCHO_PRINCIPAL - 290);
            TextoAjustado(l, xn, y0 + 248, "Servicios de la reserva: " + b.ServiciosAdicionales, 6.3f, false, GRIS, ANCHO_PRINCIPAL - 290);
            TextoAjustado(l, xn, y0 + 258, "Total abonado por la reserva: " + Dinero(b.TotalReserva) +
                                          "  -  Boleto personal e intransferible.", 6.3f, false, GRIS, ANCHO_PRINCIPAL - 290);

            // ---- Talón ----
            float xt = x0 + ANCHO_PRINCIPAL + 14, anchoT = ANCHO - ANCHO_PRINCIPAL - 28;
            Campo(l, xt, y0 + 58, "PASAJERO", b.PasajeroParaBoleto, 9, anchoT);

            l.Texto(xt, y0 + 104, b.OrigenIata, 18, true, AZUL_OSCURO);
            float anchoOrigen = l.AnchoTexto(b.OrigenIata, 18, true);
            Avion(l, xt + anchoOrigen + 18, y0 + 97, AZUL, 0.7f);
            l.Texto(xt + anchoOrigen + 34, y0 + 104, b.DestinoIata, 18, true, AZUL_OSCURO);

            float mitad = anchoT / 2;
            Campo(l, xt, y0 + 124, "VUELO", b.CodigoVuelo, 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 124, "FECHA", b.FechaHoraSalida.ToString("dd/MM/yyyy"), 9, mitad - 4);
            Campo(l, xt, y0 + 154, "ASIENTO", valores[2], 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 154, "EMBARQUE", b.HoraEmbarque.ToString("HH:mm"), 9, mitad - 4);
            Campo(l, xt, y0 + 184, "CLASE", b.Clase, 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 184, "PUERTA", valores[3], 9, mitad - 4);
            Campo(l, xt, y0 + 214, "RESERVA", b.NumeroReserva, 8.5f, anchoT);

            Code128_GV42.Dibujar(l, b.CodigoBarras, xt - 6, y0 + 238, anchoT + 12, 20);
        }

        // Etiqueta chica gris arriba y valor en negrita abajo.
        private static void Campo(ILienzo_GV42 l, float x, float y, string etiqueta, string valor,
                                  float tamValor, float anchoMax, int color = TEXTO)
        {
            l.Texto(x, y, etiqueta, 6.5f, true, GRIS);
            TextoAjustado(l, x, y + tamValor + 4, valor ?? "", tamValor, true, color, anchoMax);
        }

        // Si el texto no entra, achica la letra (hasta 6 pt) y, si aún no entra, lo recorta.
        private static void TextoAjustado(ILienzo_GV42 l, float x, float y, string texto, float tam,
                                          bool negrita, int color, float anchoMax)
        {
            texto = texto ?? "";
            float t = tam;
            while (t > 6f && l.AnchoTexto(texto, t, negrita) > anchoMax) t -= 0.5f;
            if (l.AnchoTexto(texto, t, negrita) > anchoMax)
            {
                while (texto.Length > 1 && l.AnchoTexto(texto + "...", t, negrita) > anchoMax)
                    texto = texto.Substring(0, texto.Length - 1);
                texto = texto.TrimEnd() + "...";
            }
            l.Texto(x, y, texto, t, negrita, color);
        }

        private static void TextoDerecha(ILienzo_GV42 l, float xDer, float y, string texto, float tam,
                                         bool negrita, int color, float anchoMax)
        {
            float t = tam;
            while (t > 6f && l.AnchoTexto(texto, t, negrita) > anchoMax) t -= 0.5f;
            l.Texto(xDer - l.AnchoTexto(texto, t, negrita), y, texto, t, negrita, color);
        }

        private static void TextoCentrado(ILienzo_GV42 l, float xCentro, float y, string texto, float tam,
                                          bool negrita, int color)
        {
            l.Texto(xCentro - l.AnchoTexto(texto, tam, negrita) / 2, y, texto, tam, negrita, color);
        }

        // Silueta simple de avión mirando a la derecha, centrada en (cx, cy).
        private static void Avion(ILienzo_GV42 l, float cx, float cy, int color, float escala = 1f)
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

        private static string Fecha(DateTime f)
        {
            string dia = f.ToString("ddd", Cultura).TrimEnd('.');
            return char.ToUpper(dia[0]) + dia.Substring(1) + " " + f.ToString("dd MMM yyyy", Cultura).Replace(".", "");
        }

        private static string Duracion(TimeSpan d)
        {
            if (d.TotalMinutes <= 0) return "";
            int h = (int)d.TotalHours;
            return h > 0 ? h + " h " + d.Minutes.ToString("00") + " min" : d.Minutes + " min";
        }

        private static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);
    }
}
