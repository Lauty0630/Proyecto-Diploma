using BE;
using Servicios;
using System;
using static BLL.DibujoDocumento_GV42;

namespace BLL
{
    // Diseño de la etiqueta de equipaje (RFN 2, paso 10): una por bulto despachado, en formato de
    // tira vertical como las que se pegan en las valijas. Se dibuja sobre cualquier ILienzo_GV42
    // (pantalla, impresora o PDF) y los textos fijos salen del archivo de idioma (claves "etiqueta.").
    //
    //  ┌──────────────┐
    //  │   FLY SAFE   │  encabezado azul
    //  │   DESTINO    │
    //  │     MDQ      │  IATA bien grande
    //  │ Mar del Plata│
    //  │ VUELO  FECHA │
    //  │ PASAJERO     │
    //  │ RESERVA      │
    //  │ BULTO 1 DE 2 │
    //  │ ||||||||||||| │  código de barras del bulto
    //  │ EQ000012-01  │
    //  └──────────────┘
    public static class DisenioEtiquetaEquipaje_GV42
    {
        #region Constantes

        public const float ANCHO = 136f;
        public const float ALTO = 280f;

        private const float ALTO_ENCABEZADO = 34f;
        private const float MARGEN = 8f;

        #endregion

        #region Dibujo de la etiqueta

        // indice: posición del bulto (0 = primero) dentro de ci.Equipaje.Etiquetas.
        public static void Dibujar(ILienzo_GV42 l, CheckIn_GV42 ci, int indice, float x0, float y0)
        {
            if (ci == null || ci.Equipaje == null || ci.Equipaje.Etiquetas == null) return;
            if (indice < 0 || indice >= ci.Equipaje.Etiquetas.Count) return;

            TarjetaEmbarque_GV42 t = ci.TarjetaEmbarque;
            string codigo = ci.Equipaje.Etiquetas[indice] ?? "";
            int total = ci.Equipaje.Etiquetas.Count;
            float centro = x0 + ANCHO / 2, anchoUtil = ANCHO - 2 * MARGEN;

            string vuelo = t != null ? t.CodigoVuelo : (ci.Vuelo != null ? ci.Vuelo.CodigoVuelo : "");
            DateTime salida = t != null ? t.FechaHoraSalida : (ci.Vuelo != null ? ci.Vuelo.FechaHoraSalida : DateTime.MinValue);
            string pasajero = t != null ? t.NombreApellido : (ci.Pasajero != null ? ci.Pasajero.NombreCompleto : "");
            string reserva = t != null ? t.NumeroReserva : ci.NumeroReserva;

            // ---- Marco y encabezado ----
            l.Rectangulo(x0, y0, ANCHO, ALTO, BLANCO, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO_ENCABEZADO, AZUL, null);
            l.Rectangulo(x0, y0, ANCHO, ALTO, null, GRIS_CLARO, 0.8f);
            TextoCentrado(l, centro, y0 + 17, "FLY SAFE", 12, true, BLANCO, anchoUtil);
            TextoCentrado(l, centro, y0 + 28, IdiomaManager_GV42.T("etiqueta.titulo"), 6, true, CELESTE, anchoUtil);

            // ---- Destino (bien grande) ----
            TextoCentrado(l, centro, y0 + 48, IdiomaManager_GV42.T("etiqueta.destino"), 6.5f, true, GRIS, anchoUtil);
            TextoCentrado(l, centro, y0 + 86, DestinoIata(ci), 40, true, AZUL_OSCURO, anchoUtil);
            TextoCentrado(l, centro, y0 + 99, DestinoCiudad(ci), 8.5f, true, TEXTO, anchoUtil);
            TextoCentrado(l, centro, y0 + 109, IdiomaManager_GV42.T("etiqueta.desde", OrigenIata(ci)), 6.5f, false, GRIS, anchoUtil);
            l.Linea(x0 + MARGEN, y0 + 116, x0 + ANCHO - MARGEN, y0 + 116, GRIS_CLARO, 0.6f, true);

            // ---- Vuelo, fecha, pasajero y reserva ----
            float x = x0 + MARGEN, mitad = anchoUtil / 2;
            Campo(l, x, y0 + 128, IdiomaManager_GV42.T("etiqueta.vuelo"), vuelo, 9, mitad - 4);
            Campo(l, x + mitad + 4, y0 + 128, IdiomaManager_GV42.T("etiqueta.fecha"),
                  salida == DateTime.MinValue ? "" : FechaCorta(salida), 9, mitad - 4);
            Campo(l, x, y0 + 156, IdiomaManager_GV42.T("etiqueta.pasajero"), (pasajero ?? "").ToUpper(Cultura), 9, anchoUtil);
            Campo(l, x, y0 + 184, IdiomaManager_GV42.T("etiqueta.reserva"), reserva, 8.5f, anchoUtil);

            // ---- Bulto N de M ----
            l.Rectangulo(x, y0 + 202, anchoUtil, 16, CELESTE, CELESTE_BORDE, 0.6f);
            // Con el peso de la valija si está registrado ("BULTO 1 DE 2 · 18,5 KG").
            string textoBulto = IdiomaManager_GV42.T("etiqueta.bulto", indice + 1, total);
            decimal peso = ci.Equipaje.PesosKg != null && indice < ci.Equipaje.PesosKg.Count ? ci.Equipaje.PesosKg[indice] : 0m;
            if (peso > 0) textoBulto += "  ·  " + peso.ToString("0.#", Cultura) + " KG";
            TextoCentrado(l, centro, y0 + 213.5f, textoBulto, 8, true, AZUL_OSCURO, anchoUtil - 8);

            // ---- Código de barras y código del bulto ----
            if (codigo.Length > 0)
                Code128_GV42.Dibujar(l, codigo, x0 + 2, y0 + 224, ANCHO - 4, 32);
            TextoCentrado(l, centro, y0 + 272, codigo, 13, true, TEXTO, anchoUtil);
        }

        #endregion
    }
}
