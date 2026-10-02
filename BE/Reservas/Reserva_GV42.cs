using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class Reserva_GV42
    {
        #region Propiedades

        public int Id { get; set; }

        // Autogenerado por la base de datos (ej: RES-000001).
        public string NumeroReserva { get; set; }

        // Quien contrata la reserva. Es una fila de Pasajero (misma tabla que los que viajan).
        public Pasajero_GV42 Cliente { get; set; }
        // Vuelo y clase de la ida (tramo 1).
        public VueloClase_GV42 VueloClase { get; set; }

        // Vuelo y clase de la vuelta (tramo 2). Null si el viaje es solo de ida (o en reservas de ida y
        // vuelta anteriores a esta versión, que solo guardaban la fecha de regreso).
        public VueloClase_GV42 VueloClaseVuelta { get; set; }
        // Familia tarifaria (Light / Plus / Top) con la que se vendió toda la reserva.
        public TarifaFamilia_GV42 Tarifa { get; set; }

        public TipoViaje_GV42 TipoViaje { get; set; }
        public DateTime? FechaRegreso { get; set; }

        public List<Pasajero_GV42> Pasajeros { get; set; } = new List<Pasajero_GV42>();

        // Un elemento por pasajero, con el asiento que eligió (selección estilo "cine").
        public List<AsientoPasajero_GV42> AsientosPorPasajero { get; set; } = new List<AsientoPasajero_GV42>();
        public List<AdicionalReserva_GV42> Adicionales { get; set; } = new List<AdicionalReserva_GV42>();

        public DateTime FechaRealizacion { get; set; }
        public decimal ImporteBase { get; set; }
        public decimal SubtotalAdicionales { get; set; }
        public decimal Impuestos { get; set; }
        public decimal ImporteTotal { get; set; }
        public EstadoReserva_GV42 Estado { get; set; } = EstadoReserva_GV42.PendienteDePago;
        public string LoginVendedor { get; set; }
        public CanalVenta_GV42 CanalVenta { get; set; } = CanalVenta_GV42.Presencial;

        // Solo tienen valor si Estado es Cancelada.
        public DateTime? FechaCancelacion { get; set; }
        public decimal? MontoPenalidadCancelacion { get; set; }

        // Solo tiene valor una vez registrado el pago.
        public Pago_GV42 Pago { get; set; }

        // Hasta cuándo se puede pagar mientras está pendiente. Pasada esa hora la reserva vence:
        // se cancela sola (VencidaSinPago) y libera sus asientos.
        public DateTime? FechaVencimiento { get; set; }
        public bool VencidaSinPago { get; set; }

        // Devolución generada al cancelar una reserva paga (null si no corresponde).
        public Reembolso_GV42 Reembolso { get; set; }

        // Lugares que ocupa la reserva en cada vuelo: los infantes viajan en brazos y no ocupan asiento.
        public int CantidadAsientos
        {
            get { return Pasajeros != null ? Pasajeros.Count(p => p != null && !p.EsInfante) : 0; }
        }

        public Vuelo_GV42 Vuelo { get { return VueloClase != null ? VueloClase.Vuelo : null; } }
        public ClaseVuelo_GV42 Clase { get { return VueloClase != null ? VueloClase.Clase : ClaseVuelo_GV42.Economica; } }
        public int CantidadPasajeros { get { return Pasajeros != null ? Pasajeros.Count : 0; } }
        public string EstadoTexto { get { return Estado.Texto(); } }

        public const int TRAMO_IDA = 1;
        public const int TRAMO_VUELTA = 2;

        public bool TieneVuelta { get { return VueloClaseVuelta != null && VueloClaseVuelta.Vuelo != null; } }
        public int CantidadTramos { get { return TieneVuelta ? 2 : 1; } }

        // Vuelo y clase del tramo indicado (1 = ida, 2 = vuelta). Null si la reserva no tiene ese tramo.
        public VueloClase_GV42 VueloClaseDeTramo(int tramo)
        {
            return tramo == TRAMO_VUELTA ? VueloClaseVuelta : VueloClase;
        }

        #endregion
    }
}
