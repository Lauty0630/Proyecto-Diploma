using BE;
using Servicios;
using System;
using System.Collections.Generic;
using static BLL.DibujoDocumento_GV42;

namespace BLL
{
    // Diseño de la tarjeta de embarque (RFN 2, pasos 9 y 10), en puntos. Sigue el mismo lenguaje
    // visual que el boleto electrónico y se dibuja sobre cualquier ILienzo_GV42: pantalla, impresora
    // o PDF muestran exactamente la misma tarjeta. Los textos fijos salen del archivo de idioma
    // (claves "tarjetaEmb.") y se traducen cada vez que se dibuja.
    //
    //  ┌──────────────── encabezado (marca, canal del check-in, aerolínea) ─┬─ talón ─────┐
    //  │ PASAJERO / DNI / RESERVA                                           ┊ pasajero    │
    //  │ EZE ─────✈───── MDQ          SALIDA 08:00     VUELO AR1302         ┊ EZE > MDQ   │
    //  │ [ASIENTO] [PUERTA] [HORA LÍMITE DE EMBARQUE] [CLASE + tarifa]      ┊ vuelo/fecha │
    //  │ EQUIPAJE DESPACHADO: 2 bultos · 31 kg | ...   [ASISTENCIA: ...]    ┊ asiento...  │
    //  │ ||||| código de barras (N° de tarjeta) |||||   avisos              ┊ |||||||||   │
    //  └────────────────────────────────────────────────────────────────────┴─────────────┘
    public static class DisenioTarjetaEmbarque_GV42
    {
        #region Constantes

        public const float ANCHO = 732f;
        public const float ALTO = 266f;

        private const float ANCHO_PRINCIPAL = 548f;   // el resto es el talón
        private const float ALTO_ENCABEZADO = 40f;

        // Recuadros destacados (asiento, puerta, hora límite y clase).
        private const float Y_DESTACADOS = 142f;
        private const float ALTO_DESTACADO = 50f;

        #endregion

        #region Dibujo de la tarjeta

        public static void Dibujar(ILienzo_GV42 l, CheckIn_GV42 ci, float x0, float y0)
        {
            if (ci == null || ci.TarjetaEmbarque == null) return;
            TarjetaEmbarque_GV42 t = ci.TarjetaEmbarque;

            string origen = OrigenIata(ci), destino = DestinoIata(ci);
            string clase = t.Clase.Texto();
            string asiento = string.IsNullOrEmpty(t.NumeroAsiento) ? IdiomaManager_GV42.T("boleto.asientoAAsignar") : t.NumeroAsiento;
            string puerta = string.IsNullOrWhiteSpace(t.PuertaEmbarque) ? IdiomaManager_GV42.T("boleto.puertaAConfirmar") : t.PuertaEmbarque;
            string horaLimite = t.HoraLimiteEmbarque.ToString("HH:mm");
            string canal = TextoCanal(ci);
            string pasajero = (t.NombreApellido ?? "").ToUpper(Cultura);

            // ---- Marco, talón y encabezado ----
            l.Rectangulo(x0, y0, ANCHO, ALTO, BLANCO, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO, CELESTE, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO_ENCABEZADO, AZUL, null);
            l.Rectangulo(x0 + ANCHO_PRINCIPAL, y0, ANCHO - ANCHO_PRINCIPAL, ALTO_ENCABEZADO, AZUL_OSCURO, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO, null, GRIS_CLARO, 0.8f);
            l.Linea(x0 + ANCHO_PRINCIPAL, y0 + ALTO_ENCABEZADO, x0 + ANCHO_PRINCIPAL, y0 + ALTO, GRIS, 0.8f, true);

            l.Texto(x0 + 16, y0 + 27, "FLY SAFE", 18, true, BLANCO);
            TextoAjustado(l, x0 + 118, y0 + 18, IdiomaManager_GV42.T("tarjetaEmb.titulo"), 9, true, BLANCO, 200);
            TextoAjustado(l, x0 + 118, y0 + 30, canal, 7.5f, false, BLANCO, 200);
            TextoDerecha(l, x0 + ANCHO_PRINCIPAL - 14, y0 + 26, Aerolinea(ci).ToUpper(Cultura), 12, true, BLANCO, 200);

            float xt = x0 + ANCHO_PRINCIPAL + 14, anchoT = ANCHO - ANCHO_PRINCIPAL - 28;
            TextoAjustado(l, xt, y0 + 18, IdiomaManager_GV42.T("tarjetaEmb.talon"), 8, true, BLANCO, anchoT);
            TextoAjustado(l, xt, y0 + 30, t.NumeroTarjeta, 7.5f, false, BLANCO, anchoT);

            // ---- Pasajero ----
            float x = x0 + 16;
            Campo(l, x, y0 + 54, IdiomaManager_GV42.T("tarjetaEmb.pasajero"), pasajero, 13, 290);
            Campo(l, x0 + 320, y0 + 54, IdiomaManager_GV42.T("tarjetaEmb.dni"), t.DNI, 11, 100);
            Campo(l, x0 + 430, y0 + 54, IdiomaManager_GV42.T("tarjetaEmb.reserva"), t.NumeroReserva, 11, 105);

            // ---- Ruta: ORIGEN ───✈─── DESTINO ----
            float yRuta = y0 + 112;
            l.Texto(x, yRuta, origen, 30, true, AZUL_OSCURO);
            TextoAjustado(l, x, yRuta + 12, OrigenCiudad(ci), 8.5f, true, TEXTO, 120);
            TextoAjustado(l, x, yRuta + 22, NombreAeropuerto(ci, true), 6.5f, false, GRIS, 120);

            float xDestino = x0 + 222;
            l.Texto(xDestino, yRuta, destino, 30, true, AZUL_OSCURO);
            TextoAjustado(l, xDestino, yRuta + 12, DestinoCiudad(ci), 8.5f, true, TEXTO, 120);
            TextoAjustado(l, xDestino, yRuta + 22, NombreAeropuerto(ci, false), 6.5f, false, GRIS, 120);

            float xa = x + l.AnchoTexto(origen, 30, true) + 10, xb = xDestino - 10, ym = yRuta - 11;
            l.Linea(xa, ym, xb, ym, GRIS, 0.8f, true);
            Avion(l, (xa + xb) / 2, ym, AZUL);

            // Salida y vuelo
            float xs = x0 + 360;
            l.Texto(xs, y0 + 84, IdiomaManager_GV42.T("tarjetaEmb.salida"), 6.5f, true, GRIS);
            l.Texto(xs, y0 + 106, t.FechaHoraSalida.ToString("HH:mm"), 20, true, TEXTO);
            l.Texto(xs, y0 + 119, Fecha(t.FechaHoraSalida), 7.5f, false, TEXTO);
            l.Texto(xs, y0 + 130, IdiomaManager_GV42.T("boleto.horariosLocales"), 6, false, GRIS);
            float xv = x0 + 452;
            l.Texto(xv, y0 + 84, IdiomaManager_GV42.T("tarjetaEmb.vuelo"), 6.5f, true, GRIS);
            TextoAjustado(l, xv, y0 + 104, t.CodigoVuelo, 14, true, TEXTO, ANCHO_PRINCIPAL - 468);

            // ---- Destacados: asiento, puerta, hora límite y clase ----
            float anchoD = (ANCHO_PRINCIPAL - 32 - 3 * 8) / 4f;
            float yd = y0 + Y_DESTACADOS;
            bool preferencial = ci.Asiento != null && ci.Asiento.EsPreferencial;

            float xd = x;
            Destacado(l, xd, yd, anchoD, IdiomaManager_GV42.T("tarjetaEmb.asiento"), asiento, 20,
                      preferencial ? NARANJA : (int?)null, false);
            if (preferencial)
            {
                string marca = IdiomaManager_GV42.T("tarjetaEmb.asientoPreferencial").ToUpper(CulturaFechas);
                Pastilla(l, xd + 8, yd + ALTO_DESTACADO - 13, marca, 6, NARANJA, BLANCO);
            }
            else if (!string.IsNullOrEmpty(TextoUbicacion(ci)))
                TextoAjustado(l, xd + 8, yd + ALTO_DESTACADO - 6, TextoUbicacion(ci), 6.5f, false, GRIS, anchoD - 16);

            xd += anchoD + 8;
            Destacado(l, xd, yd, anchoD, IdiomaManager_GV42.T("tarjetaEmb.puerta"), puerta, 20, null, false);

            xd += anchoD + 8;
            Destacado(l, xd, yd, anchoD, IdiomaManager_GV42.T("tarjetaEmb.horaLimite"), horaLimite, 20, null, true);
            TextoAjustado(l, xd + 8, yd + ALTO_DESTACADO - 6,
                          IdiomaManager_GV42.T("tarjetaEmb.antesSalida", BLLCheckIn_GV42.MINUTOS_LIMITE_EMBARQUE),
                          6.5f, false, CELESTE, anchoD - 16);

            xd += anchoD + 8;
            Destacado(l, xd, yd, anchoD, IdiomaManager_GV42.T("tarjetaEmb.clase"), clase, 13, null, false);
            // Tarifa de la reserva (Light / Plus / Top), debajo de la clase.
            if (!string.IsNullOrWhiteSpace(ci.TarifaNombre))
                TextoAjustado(l, xd + 8, yd + ALTO_DESTACADO - 6,
                              IdiomaManager_GV42.T("checkin.tarjeta.tarifa", ci.TarifaNombre), 6.5f, false, GRIS, anchoD - 16);

            // ---- Equipaje despachado ----
            float yEq = y0 + 214;
            l.Linea(x0 + 16, yEq - 12, x0 + ANCHO_PRINCIPAL - 16, yEq - 12, GRIS_CLARO, 0.6f);
            l.Texto(x, yEq, IdiomaManager_GV42.T("tarjetaEmb.equipaje"), 6.5f, true, GRIS);

            // Asistencia especial pedida al reservar: leyenda destacada a la derecha, para que la vea
            // el personal de embarque ("ASISTENCIA: SILLA DE RUEDAS").
            if (ci.Asistencia != AsistenciaEspecial_GV42.Ninguna)
            {
                string asistencia = IdiomaManager_GV42.T("checkin.tarjeta.asistencia", ci.Asistencia.Texto()).ToUpper(CulturaFechas);
                float anchoAsistencia = l.AnchoTexto(asistencia, 7, true) + 8;
                Pastilla(l, x0 + ANCHO_PRINCIPAL - 16 - anchoAsistencia, yEq - 8, asistencia, 7, NARANJA, BLANCO);
            }
            ResumenEquipaje(l, ci.Equipaje, x, yEq + 12, ANCHO_PRINCIPAL - 32);

            // ---- Código de barras (N° de tarjeta) y avisos ----
            if (!string.IsNullOrEmpty(t.NumeroTarjeta))
            {
                Code128_GV42.Dibujar(l, t.NumeroTarjeta, x0 + 8, y0 + 234, 220, 20);
                TextoCentrado(l, x0 + 118, y0 + 261, t.NumeroTarjeta, 6, false, GRIS);
            }

            float xn = x0 + 244, anchoAviso = ANCHO_PRINCIPAL - 260;
            TextoAjustado(l, xn, y0 + 240, IdiomaManager_GV42.T("tarjetaEmb.avisoPuerta", horaLimite), 6.3f, false, GRIS, anchoAviso);
            TextoAjustado(l, xn, y0 + 250, IdiomaManager_GV42.T("tarjetaEmb.avisoEmision", t.NumeroTarjeta,
                          t.FechaHoraEmision.ToString("dd/MM/yyyy HH:mm")), 6.3f, false, GRIS, anchoAviso);
            TextoAjustado(l, xn, y0 + 260, IdiomaManager_GV42.T("tarjetaEmb.avisoPersonal"), 6.3f, false, GRIS, anchoAviso);

            // ---- Talón ----
            Campo(l, xt, y0 + 54, IdiomaManager_GV42.T("tarjetaEmb.pasajero"), pasajero, 9, anchoT);

            l.Texto(xt, y0 + 100, origen, 18, true, AZUL_OSCURO);
            float anchoOrigen = l.AnchoTexto(origen, 18, true);
            Avion(l, xt + anchoOrigen + 18, y0 + 93, AZUL, 0.7f);
            l.Texto(xt + anchoOrigen + 34, y0 + 100, destino, 18, true, AZUL_OSCURO);

            float mitad = anchoT / 2;
            Campo(l, xt, y0 + 114, IdiomaManager_GV42.T("tarjetaEmb.vuelo"), t.CodigoVuelo, 9, mitad - 4);
            Campo(l, xt + mitad, y0 + 114, IdiomaManager_GV42.T("tarjetaEmb.fecha"), t.FechaHoraSalida.ToString("dd/MM/yyyy"), 9, mitad - 4);
            Campo(l, xt, y0 + 144, IdiomaManager_GV42.T("tarjetaEmb.asiento"), asiento, 9, mitad - 4, preferencial ? NARANJA : TEXTO);
            Campo(l, xt + mitad, y0 + 144, IdiomaManager_GV42.T("tarjetaEmb.puerta"), puerta, 9, mitad - 4);
            Campo(l, xt, y0 + 174, IdiomaManager_GV42.T("tarjetaEmb.limiteCorto"), horaLimite, 9, mitad - 4, AZUL_OSCURO);
            Campo(l, xt + mitad, y0 + 174, IdiomaManager_GV42.T("tarjetaEmb.clase"), clase, 9, mitad - 4);
            Campo(l, xt, y0 + 204, IdiomaManager_GV42.T("tarjetaEmb.reserva"), t.NumeroReserva, 8.5f, mitad - 4);
            Campo(l, xt + mitad, y0 + 204, IdiomaManager_GV42.T("tarjetaEmb.canal"), CanalCorto(ci), 8.5f, mitad - 4);

            if (!string.IsNullOrEmpty(t.NumeroTarjeta))
                Code128_GV42.Dibujar(l, t.NumeroTarjeta, xt - 6, y0 + 234, anchoT + 12, 20);
        }

        #endregion

        #region Textos para la UI

        // "Check-in en mostrador" / "Check-in online" (vacío si el check-in no tiene canal).
        public static string TextoCanal(CheckIn_GV42 ci)
        {
            if (ci == null || !ci.Canal.HasValue) return "";
            return IdiomaManager_GV42.T(ci.EsOnline ? "tarjetaEmb.canalOnline" : "tarjetaEmb.canalMostrador");
        }

        // Códigos IATA de origen y destino (para el resumen del encabezado de la pantalla).
        public static string CodigoOrigen(CheckIn_GV42 ci) => ci == null ? "" : OrigenIata(ci);

        public static string CodigoDestino(CheckIn_GV42 ci) => ci == null ? "" : DestinoIata(ci);

        // "2 bultos · 31 kg" (vacío si no despachó equipaje).
        public static string TextoBultos(Equipaje_GV42 e)
        {
            if (e == null || e.CantidadBultos <= 0) return "";
            return IdiomaManager_GV42.T(e.CantidadBultos == 1 ? "tarjetaEmb.bultoPeso" : "tarjetaEmb.bultosPeso",
                                        e.CantidadBultos, Kilos(e.PesoTotalKg));
        }

        #endregion

        #region Métodos privados

        // Recuadro con etiqueta chica y valor grande. "resaltado" lo pinta de azul con letras blancas.
        private static void Destacado(ILienzo_GV42 l, float x, float y, float ancho, string etiqueta, string valor,
                                      float tamValor, int? borde, bool resaltado)
        {
            int fondo = resaltado ? AZUL_OSCURO : CELESTE;
            int colorEtiqueta = resaltado ? CELESTE_BORDE : GRIS;
            int colorValor = resaltado ? BLANCO : AZUL_OSCURO;

            l.Rectangulo(x, y, ancho, ALTO_DESTACADO, fondo, borde ?? (resaltado ? AZUL_OSCURO : CELESTE_BORDE),
                         borde.HasValue ? 1.4f : 0.8f);
            TextoAjustado(l, x + 8, y + 12, etiqueta, 6.5f, true, colorEtiqueta, ancho - 16);
            TextoAjustado(l, x + 8, y + 33, valor, tamValor, true, colorValor, ancho - 16);
        }

        // "2 bultos · 31 kg  |  Franquicia 46 kg  |  Exceso 8 kg abonado ($ 12.000,00)".
        private static void ResumenEquipaje(ILienzo_GV42 l, Equipaje_GV42 e, float x, float y, float anchoMax)
        {
            const float TAM = 8.5f;
            if (e == null || e.CantidadBultos <= 0)
            {
                TextoAjustado(l, x, y, IdiomaManager_GV42.T("tarjetaEmb.sinEquipaje"), TAM, true, GRIS, anchoMax);
                return;
            }

            var partes = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>(TextoBultos(e), TEXTO),
                new KeyValuePair<string, int>(IdiomaManager_GV42.T("tarjetaEmb.franquicia", Kilos(e.FranquiciaKg)), TEXTO)
            };

            CargoExcesoEquipaje_GV42 c = e.CargoExceso;
            if (c != null && c.TieneExceso)
            {
                bool pagado = c.FechaHoraCobro.HasValue || c.MedioPago.HasValue;
                string clave = pagado ? "tarjetaEmb.excesoPagado" : "tarjetaEmb.excesoPendiente";
                partes.Add(new KeyValuePair<string, int>(
                    IdiomaManager_GV42.T(clave, Kilos(c.KilosExceso), Dinero(c.ImporteCargo)), pagado ? VERDE : NARANJA));
            }
            else
                partes.Add(new KeyValuePair<string, int>(IdiomaManager_GV42.T("tarjetaEmb.sinExceso"), VERDE));

            // Se dibujan una al lado de la otra; si no entran todas, se achica la letra.
            const string SEPARADOR = "   |   ";
            float tam = TAM;
            while (tam > 6f && AnchoPartes(l, partes, SEPARADOR, tam) > anchoMax) tam -= 0.5f;

            float xx = x;
            for (int i = 0; i < partes.Count; i++)
            {
                if (i > 0)
                {
                    l.Texto(xx, y, SEPARADOR, tam, false, GRIS_CLARO);
                    xx += l.AnchoTexto(SEPARADOR, tam, false);
                }
                l.Texto(xx, y, partes[i].Key, tam, true, partes[i].Value);
                xx += l.AnchoTexto(partes[i].Key, tam, true);
            }
        }

        private static float AnchoPartes(ILienzo_GV42 l, List<KeyValuePair<string, int>> partes, string separador, float tam)
        {
            float total = 0;
            for (int i = 0; i < partes.Count; i++)
            {
                if (i > 0) total += l.AnchoTexto(separador, tam, false);
                total += l.AnchoTexto(partes[i].Key, tam, true);
            }
            return total;
        }

        // "Mostrador" / "Online" para el talón.
        private static string CanalCorto(CheckIn_GV42 ci)
        {
            if (!ci.Canal.HasValue) return "-";
            return IdiomaManager_GV42.T(ci.EsOnline ? "tarjetaEmb.online" : "tarjetaEmb.mostrador");
        }

        // "Ventana" / "Window" según el idioma.
        private static string TextoUbicacion(CheckIn_GV42 ci)
        {
            if (ci.Asiento == null || string.IsNullOrEmpty(ci.Asiento.Ubicacion)) return "";
            return IdiomaManager_GV42.TConDefecto("ubicacion." + ci.Asiento.Ubicacion, ci.Asiento.Ubicacion);
        }

        private static string NombreAeropuerto(CheckIn_GV42 ci, bool origen)
        {
            Vuelo_GV42 v = ci.Vuelo;
            if (v == null) return "";
            Aeropuerto_GV42 a = origen ? v.Origen : v.Destino;
            return a != null ? a.Nombre ?? "" : "";
        }

        #endregion
    }
}
