using System;
using System.Collections.Generic;

namespace BE
{
    // Datos de un boleto electrónico listo para mostrar o imprimir (uno por pasajero).
    // Reúne lo que figura en un boleto real: número de boleto y de reserva, pasajero, itinerario,
    // asiento, puerta, horario de embarque, franquicia de equipaje, tarifa y forma de pago.
    public class BoletoElectronico_GV42
    {
        #region Propiedades

        public string NumeroBoleto { get; set; }
        public string NumeroReserva { get; set; }
        public DateTime FechaEmision { get; set; }
        public int NumeroPasajero { get; set; }
        public int TotalPasajeros { get; set; }

        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroDni { get; set; }

        public string Aerolinea { get; set; }
        public string CodigoVuelo { get; set; }
        public string OrigenIata { get; set; }
        public string OrigenCiudad { get; set; }
        public string OrigenAeropuerto { get; set; }
        public string DestinoIata { get; set; }
        public string DestinoCiudad { get; set; }
        public string DestinoAeropuerto { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public DateTime HoraEmbarque { get; set; }
        public DateTime CierreEmbarque { get; set; }
        public string PuertaEmbarque { get; set; }

        public string Clase { get; set; }
        public string Asiento { get; set; }
        public string UbicacionAsiento { get; set; }
        public decimal FranquiciaEquipajeKg { get; set; }
        public string TipoViaje { get; set; }

        // Importes del pasajero (la reserva se reparte en partes iguales entre sus pasajeros).
        public decimal TarifaPasajero { get; set; }
        public decimal ImpuestosPasajero { get; set; }
        public decimal TotalReserva { get; set; }
        public string ServiciosAdicionales { get; set; }
        public string FormaPago { get; set; }
        public string Estado { get; set; }

        // Texto codificado en el código de barras (Code 128).
        public string CodigoBarras { get; set; }

        #endregion

        #region Valores sin traducir

        // Los textos de arriba (Clase, TipoViaje, FormaPago, Estado, ServiciosAdicionales) quedan en el
        // idioma que había al armar el boleto. Con estos valores el diseño del boleto los vuelve a
        // traducir cada vez que se dibuja, así cambian de idioma en caliente (Observer).
        // Son opcionales: si vienen en null se usan los textos.
        public ClaseVuelo_GV42? ClaseValor { get; set; }
        public TipoViaje_GV42? TipoViajeValor { get; set; }
        public MedioPago_GV42? MedioPagoValor { get; set; }
        public EstadoReserva_GV42? EstadoValor { get; set; }
        public List<AdicionalReserva_GV42> Adicionales { get; set; }

        #endregion

        #region Propiedades calculadas

        // Formato de los boletos reales: APELLIDO / NOMBRE.
        public string PasajeroParaBoleto =>
            ((PasajeroApellido ?? "").Trim() + " / " + (PasajeroNombre ?? "").Trim()).ToUpperInvariant();

        public TimeSpan Duracion => FechaHoraLlegada - FechaHoraSalida;

        #endregion
    }
}
