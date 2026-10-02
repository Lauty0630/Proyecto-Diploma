using BE;
using Servicios;
using System;
using System.Globalization;

namespace BLL
{
    // Diseño visual del boleto electrónico (en puntos). Se dibuja sobre cualquier ILienzo_GV42:
    // pantalla, impresora o PDF, así los tres muestran exactamente el mismo boleto.
    // Todos los textos fijos salen del archivo de idioma (claves "boleto.") y se traducen cada vez
    // que se dibuja: si el usuario cambia de idioma, el próximo repintado ya sale en el idioma nuevo.
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
        #region Constantes

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

        #endregion

        #region Campos

        // Importes y números siempre con formato argentino ("$ 1.234,56"): la moneda es el peso.
        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private static readonly CultureInfo CulturaIngles = new CultureInfo("en-US");

        #endregion

        #region Propiedades

        // Cultura para los nombres de días y meses de las fechas del boleto.
        private static CultureInfo CulturaFechas => IdiomaManager_GV42.Instancia.EsIngles ? CulturaIngles : Cultura;

        #endregion

        #region Dibujo del boleto

        public static void Dibujar(ILienzo_GV42 l, BoletoElectronico_GV42 b, float x0, float y0)
        {
            // Textos que dependen del idioma: si el boleto trae los valores sin traducir, se traducen ahora.
            string clase = b.ClaseValor.HasValue ? b.ClaseValor.Value.Texto() : b.Clase;
            string tipoViaje = b.TipoViajeValor.HasValue ? b.TipoViajeValor.Value.Texto() : b.TipoViaje;
            // Ida y vuelta: cada boleto aclara si es el de la ida o el de la vuelta.
            if (b.EsIdaYVuelta)
                tipoViaje += " · " + IdiomaManager_GV42.T(b.Tramo == 2 ? "tramo.vuelta" : "tramo.ida");
            string formaPago = b.MedioPagoValor.HasValue ? b.MedioPagoValor.Value.Texto() : b.FormaPago;
            string estado = TextoEstado(b);
            string servicios = b.Adicionales != null ? BLLBoleto_GV42.TextoServicios(b.Adicionales) : b.ServiciosAdicionales;
            string ubicacion = string.IsNullOrEmpty(b.UbicacionAsiento)
                ? b.UbicacionAsiento
                : IdiomaManager_GV42.TConDefecto("ubicacion." + b.UbicacionAsiento, b.UbicacionAsiento);

            // ---- Marco, talón y encabezado ----
            l.Rectangulo(x0, y0, ANCHO, ALTO, BLANCO, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO, CELESTE, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO_ENCABEZADO, AZUL, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO_ENCABEZADO, AZUL_OSCURO, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO, null, GRIS_CLARO, 0.8f);
            l.Linea(x0 + ANCHO_PRINCIPAL, y0 + ALTO_ENCABEZADO, x0 + ANCHO_PRINCIPAL, y0 + ALTO, GRIS, 0.8f, true);

            l.Texto(x0 + 16, y0 + 27, "FLY SAFE", 18, true, BLANCO);
            l.Texto(x0 + 118, y0 + 18, IdiomaManager_GV42.T("boleto.titulo"), 9, true, BLANCO);
            l.Texto(x0 + 118, y0 + 30, IdiomaManager_GV42.T("boleto.subtitulo",
                (tipoViaje ?? "").ToLower(CulturaFechas), b.NumeroPasajero, b.TotalPasajeros), 7.5f, false, BLANCO);
            TextoDerecha(l, x0 + ANCHO_PRINCIPAL - 14, y0 + 26, (b.Aerolinea ?? "").ToUpper(Cultura), 12, true, BLANCO, 220);
            TextoAjustado(l, x0 + ANCHO_PRINCIPAL + 14, y0 + 18, IdiomaManager_GV42.T("boleto.talon"), 8, true, BLANCO,
                          ANCHO - ANCHO_PRINCIPAL - 28);
            l.Texto(x0 + ANCHO_PRINCIPAL + 14, y0 + 30, b.NumeroBoleto, 7.5f, false, BLANCO);

            // ---- Pasajero ----
            float x = x0 + 16, y = y0 + 58;
            Campo(l, x, y, IdiomaManager_GV42.T("boleto.pasajero"), b.PasajeroParaBoleto, 13, 290);
            Campo(l, x0 + 320, y, IdiomaManager_GV42.T("boleto.dni"), b.PasajeroDni, 11, 100);
            Campo(l, x0 + 430, y, IdiomaManager_GV42.T("boleto.numeroBoleto"), b.NumeroBoleto, 11, 105);

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
            TextoCentrado(l, (xa + xb) / 2, ym + 19, IdiomaManager_GV42.T("boleto.directo"), 7, false, GRIS);

            // Horarios
            float xs = x0 + 360;
            l.Texto(xs, y0 + 96, IdiomaManager_GV42.T("boleto.salida"), 6.5f, true, GRIS);
            l.Texto(xs, y0 + 118, b.FechaHoraSalida.ToString("HH:mm"), 20, true, TEXTO);
            l.Texto(xs, y0 + 131, Fecha(b.FechaHoraSalida), 7.5f, false, TEXTO);
            float xll = x0 + 452;
            l.Texto(xll, y0 + 96, IdiomaManager_GV42.T("boleto.llegada"), 6.5f, true, GRIS);
            l.Texto(xll, y0 + 118, b.FechaHoraLlegada.ToString("HH:mm"), 20, true, TEXTO);
            l.Texto(xll, y0 + 131, Fecha(b.FechaHoraLlegada), 7.5f, false, TEXTO);
            l.Texto(xs, y0 + 146, IdiomaManager_GV42.T("boleto.horariosLocales"), 6, false, GRIS);

            // ---- Datos del vuelo ----
            float yFila = y0 + 172, anchoCelda = (ANCHO_PRINCIPAL - 32) / 6f;
            l.Linea(x0 + 16, yFila - 14, x0 + ANCHO_PRINCIPAL - 16, yFila - 14, GRIS_CLARO, 0.6f);
            string[] etiquetas =
            {
                IdiomaManager_GV42.T("boleto.vuelo"), IdiomaManager_GV42.T("boleto.clase"),
                IdiomaManager_GV42.T("boleto.asiento"), IdiomaManager_GV42.T("boleto.puerta"),
                IdiomaManager_GV42.T("boleto.embarque"), IdiomaManager_GV42.T("boleto.equipaje")
            };
            string[] valores =
            {
                b.CodigoVuelo, clase,
                string.IsNullOrEmpty(b.Asiento) ? IdiomaManager_GV42.T("boleto.asientoAAsignar") : b.Asiento,
                string.IsNullOrWhiteSpace(b.PuertaEmbarque) ? IdiomaManager_GV42.T("boleto.puertaAConfirmar") : b.PuertaEmbarque,
                b.HoraEmbarque.ToString("HH:mm"),
                b.FranquiciaEquipajeKg.ToString("0.#", Cultura) + " kg"
            };
            for (int i = 0; i < 6; i++)
                Campo(l, x0 + 16 + i * anchoCelda, yFila, etiquetas[i], valores[i], 11, anchoCelda - 6);
            if (!string.IsNullOrEmpty(ubicacion))
                TextoAjustado(l, x0 + 16 + 2 * anchoCelda, yFila + 26, ubicacion, 6.5f, false, GRIS, anchoCelda - 6);
            TextoAjustado(l, x0 + 16 + 4 * anchoCelda, yFila + 26,
                          IdiomaManager_GV42.T("boleto.cierra", b.CierreEmbarque.ToString("HH:mm")), 6.5f, false, GRIS, anchoCelda - 6);
            TextoAjustado(l, x0 + 16 + 5 * anchoCelda, yFila + 26, IdiomaManager_GV42.T("boleto.enBodega"), 6.5f, false, GRIS,
                          anchoCelda - 6);

            // ---- Datos comerciales ----
            float yCom = y0 + 214;
            l.Linea(x0 + 16, yCom - 12, x0 + ANCHO_PRINCIPAL - 16, yCom - 12, GRIS_CLARO, 0.6f);
            string[] etiquetas2 =
            {
                IdiomaManager_GV42.T("boleto.reserva"), IdiomaManager_GV42.T("boleto.emision"),
                IdiomaManager_GV42.T("boleto.tarifa"), IdiomaManager_GV42.T("boleto.impuestos"),
                IdiomaManager_GV42.T("boleto.formaPago"), IdiomaManager_GV42.T("boleto.estado")
            };
            string[] valores2 =
            {
                b.NumeroReserva, b.FechaEmision.ToString("dd/MM/yyyy"),
                Dinero(b.TarifaPasajero), Dinero(b.ImpuestosPasajero), formaPago, estado
            };
            for (int i = 0; i < 6; i++)
                Campo(l, x0 + 16 + i * anchoCelda, yCom, etiquetas2[i], valores2[i], 8.5f, anchoCelda - 6,
                      i == 5 ? VERDE : TEXTO);

            // ---- Código de barras y avisos ----
            Code128_GV42.Dibujar(l, b.CodigoBarras, x0 + 8, y0 + 232, 250, 24);
            TextoCentrado(l, x0 + 133, y0 + 264, b.CodigoBarras, 6, false, GRIS);

            float xn = x0 + 270, anchoAviso = ANCHO_PRINCIPAL - 290;
            TextoAjustado(l, xn, y0 + 238, IdiomaManager_GV42.T("boleto.avisoPresentarse", BLLBoleto_GV42.MINUTOS_CIERRE_EMBARQUE),
                          6.3f, false, GRIS, anchoAviso);
            TextoAjustado(l, xn, y0 + 248, IdiomaManager_GV42.T("boleto.avisoServicios", servicios), 6.3f, false, GRIS, anchoAviso);
            TextoAjustado(l, xn, y0 + 258, IdiomaManager_GV42.T("boleto.avisoTotal", Dinero(b.TotalReserva)),
                          6.3f, false, GRIS, anchoAviso);

            // ---- Talón ----
            float xt = x0 + ANCHO_PRINCIPAL + 14, anchoT = ANCHO - ANCHO_PRINCIPAL - 28;
            Campo(l, xt, y0 + 58, IdiomaManager_GV42.T("boleto.pasajero"), b.PasajeroParaBoleto, 9, anchoT);

            l.Texto(xt, y0 + 104, b.OrigenIata, 18, true, AZUL_OSCURO);
            float anchoOrigen = l.AnchoTexto(b.OrigenIata, 18, true);
            Avion(l, xt + anchoOrigen + 18, y0 + 97, AZUL, 0.7f);
            l.Texto(xt + anchoOrigen + 34, y0 + 104, b.DestinoIata, 18, true, AZUL_OSCURO);

            float mitad = anchoT / 2;
            Campo(l, xt, y0 + 124, IdiomaManager_GV42.T("boleto.vuelo"), b.CodigoVuelo, 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 124, IdiomaManager_GV42.T("boleto.fecha"), b.FechaHoraSalida.ToString("dd/MM/yyyy"), 9, mitad - 4);
            Campo(l, xt, y0 + 154, IdiomaManager_GV42.T("boleto.asiento"), valores[2], 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 154, IdiomaManager_GV42.T("boleto.embarque"), b.HoraEmbarque.ToString("HH:mm"), 9, mitad - 4);
            Campo(l, xt, y0 + 184, IdiomaManager_GV42.T("boleto.clase"), clase, 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 184, IdiomaManager_GV42.T("boleto.puerta"), valores[3], 9, mitad - 4);
            Campo(l, xt, y0 + 214, IdiomaManager_GV42.T("boleto.reserva"), b.NumeroReserva, 8.5f, anchoT);

            Code128_GV42.Dibujar(l, b.CodigoBarras, xt - 6, y0 + 238, anchoT + 12, 20);
        }

        #endregion

        #region Métodos privados

        // Etiqueta chica gris arriba y valor en negrita abajo.
        private static void Campo(ILienzo_GV42 l, float x, float y, string etiqueta, string valor,
                                  float tamValor, float anchoMax, int color = TEXTO)
        {
            TextoAjustado(l, x, y, etiqueta, 6.5f, true, GRIS, anchoMax);
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

        // "Sáb 04 oct 2026" en español / "Sat 04 Oct 2026" en inglés.
        private static string Fecha(DateTime f)
        {
            CultureInfo c = CulturaFechas;
            string dia = f.ToString("ddd", c).TrimEnd('.');
            return char.ToUpper(dia[0]) + dia.Substring(1) + " " + f.ToString("dd MMM yyyy", c).Replace(".", "");
        }

        private static string Duracion(TimeSpan d)
        {
            if (d.TotalMinutes <= 0) return "";
            int h = (int)d.TotalHours;
            return h > 0 ? h + " h " + d.Minutes.ToString("00") + " min" : d.Minutes + " min";
        }

        // Los boletos solo se emiten con la reserva confirmada: "CONFIRMADO" / "CONFIRMED".
        private static string TextoEstado(BoletoElectronico_GV42 b)
        {
            if (!b.EstadoValor.HasValue) return b.Estado;
            return b.EstadoValor.Value == EstadoReserva_GV42.Confirmada
                ? IdiomaManager_GV42.T("boleto.estadoConfirmado")
                : b.EstadoValor.Value.Texto().ToUpper(CulturaFechas);
        }

        private static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);

        #endregion
    }
}
