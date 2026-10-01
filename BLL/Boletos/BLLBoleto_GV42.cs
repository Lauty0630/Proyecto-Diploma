using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    // Paso 14 del RFN 1: entregar los boletos al cliente. Arma el boleto electrónico de cada
    // pasajero con los datos de la reserva (vuelo, asiento, tarifa, pago) y lo exporta a PDF.
    public class BLLBoleto_GV42
    {
        #region Constantes

        // El embarque empieza 40 minutos antes de la salida y cierra 15 minutos antes.
        public const int MINUTOS_INICIO_EMBARQUE = 40;
        public const int MINUTOS_CIERRE_EMBARQUE = 15;

        #endregion

        #region Campos

        private readonly BLLReserva_GV42 _bllReserva = new BLLReserva_GV42();
        private readonly DALBoleto_GV42 _dalBoleto = new DALBoleto_GV42();

        #endregion

        #region Armado de boletos

        public List<BoletoElectronico_GV42> ObtenerBoletosElectronicos(string numeroReserva)
        {
            // BuscarReserva ya controla que un cliente solo vea sus propias reservas.
            Reserva_GV42 reserva = _bllReserva.BuscarReserva(numeroReserva);
            if (reserva == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reserva.noExiste"));
            if (reserva.Estado == EstadoReserva_GV42.Cancelada)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.boleto.reservaCancelada", reserva.NumeroReserva));
            if (reserva.Estado != EstadoReserva_GV42.Confirmada)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.boleto.noPaga"));

            List<Boleto_GV42> boletos = _dalBoleto.ListarPorReserva(reserva.NumeroReserva);
            if (boletos.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.boleto.sinBoletos"));

            Vuelo_GV42 v = reserva.Vuelo;
            int cantidad = Math.Max(1, reserva.CantidadPasajeros);
            string servicios = TextoServicios(reserva.Adicionales);

            var lista = new List<BoletoElectronico_GV42>();
            int n = 0;
            foreach (Boleto_GV42 b in boletos)
            {
                n++;
                Asiento_GV42 asiento = reserva.AsientosPorPasajero?
                    .FirstOrDefault(a => a.DniPasajero == b.PasajeroDni)?.Asiento;

                lista.Add(new BoletoElectronico_GV42
                {
                    NumeroBoleto = b.NumeroBoleto,
                    NumeroReserva = reserva.NumeroReserva,
                    FechaEmision = b.FechaEmision,
                    NumeroPasajero = n,
                    TotalPasajeros = boletos.Count,
                    PasajeroNombre = b.Pasajero?.Nombre,
                    PasajeroApellido = b.Pasajero?.Apellido,
                    PasajeroDni = b.PasajeroDni,
                    Aerolinea = v.Aerolinea?.Nombre,
                    CodigoVuelo = v.CodigoVuelo,
                    OrigenIata = v.Origen?.CodigoIata,
                    OrigenCiudad = v.Origen?.Ciudad,
                    OrigenAeropuerto = v.Origen?.Nombre,
                    DestinoIata = v.Destino?.CodigoIata,
                    DestinoCiudad = v.Destino?.Ciudad,
                    DestinoAeropuerto = v.Destino?.Nombre,
                    FechaHoraSalida = v.FechaHoraSalida,
                    FechaHoraLlegada = v.FechaHoraLlegada,
                    HoraEmbarque = v.FechaHoraSalida.AddMinutes(-MINUTOS_INICIO_EMBARQUE),
                    CierreEmbarque = v.FechaHoraSalida.AddMinutes(-MINUTOS_CIERRE_EMBARQUE),
                    PuertaEmbarque = v.PuertaEmbarque,
                    Clase = reserva.VueloClase.ClaseTexto,
                    Asiento = asiento?.NumeroAsiento,
                    UbicacionAsiento = asiento?.Ubicacion,
                    FranquiciaEquipajeKg = reserva.VueloClase.FranquiciaEquipajeKg,
                    TipoViaje = reserva.TipoViaje.Texto(),
                    TarifaPasajero = Math.Round(reserva.ImporteBase / cantidad, 2),
                    // Impuesto de la tarifa del pasajero (los adicionales y su impuesto se ven en el total de la reserva).
                    ImpuestosPasajero = Math.Round(Math.Round(reserva.ImporteBase / cantidad, 2) * BLLReserva_GV42.TASA_IMPUESTOS, 2),
                    TotalReserva = reserva.ImporteTotal,
                    ServiciosAdicionales = servicios,
                    FormaPago = reserva.Pago != null ? reserva.Pago.MedioPagoTexto : "-",
                    Estado = IdiomaManager_GV42.T("boleto.estadoConfirmado"),
                    CodigoBarras = CodigoDeBarras(b.NumeroBoleto, v.CodigoVuelo, asiento?.NumeroAsiento),

                    // Valores sin traducir: el diseño los vuelve a traducir al dibujar (cambio de idioma en caliente).
                    ClaseValor = reserva.VueloClase.Clase,
                    TipoViajeValor = reserva.TipoViaje,
                    MedioPagoValor = reserva.Pago != null ? reserva.Pago.MedioPago : (MedioPago_GV42?)null,
                    EstadoValor = reserva.Estado,
                    Adicionales = reserva.Adicionales
                });
            }
            return lista;
        }

        // "Equipaje extra x1, Comida especial x2" en el idioma actual (o "sin servicios adicionales").
        internal static string TextoServicios(List<AdicionalReserva_GV42> adicionales)
        {
            return adicionales == null || adicionales.Count == 0
                ? IdiomaManager_GV42.T("boleto.sinServicios")
                : string.Join(", ", adicionales.Select(a => BLLNegocioUtil_GV42.NombreAdicional(a.TipoNombre) + " x" + a.Cantidad));
        }

        #endregion

        #region Exportación a PDF

        // Guarda los boletos en un PDF: dos por hoja (horizontal), igual a como se ven en pantalla.
        public void ExportarPdf(string ruta, List<BoletoElectronico_GV42> boletos)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.pdf.indiqueRuta"));
            if (boletos == null || boletos.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.boleto.nadaExportar"));

            var pdf = new LienzoPdf_GV42();
            float x = (pdf.AnchoPagina - DisenioBoleto_GV42.ANCHO) / 2;
            float separacion = 16;
            float y0 = (pdf.AltoPagina - 2 * DisenioBoleto_GV42.ALTO - separacion) / 2;

            for (int i = 0; i < boletos.Count; i++)
            {
                if (i > 0 && i % 2 == 0) pdf.NuevaPagina();
                float y = y0 + (i % 2) * (DisenioBoleto_GV42.ALTO + separacion);
                DisenioBoleto_GV42.Dibujar(pdf, boletos[i], x, y);
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

        // Texto del código de barras: boleto + vuelo + asiento (solo caracteres ASCII).
        private static string CodigoDeBarras(string boleto, string vuelo, string asiento)
        {
            return (boleto ?? "") + " " + (vuelo ?? "") + (string.IsNullOrEmpty(asiento) ? "" : " " + asiento);
        }

        #endregion
    }
}
