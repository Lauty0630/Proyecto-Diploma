using System;
using System.Collections.Generic;

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
        public VueloClase_GV42 VueloClase { get; set; }
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

        public Vuelo_GV42 Vuelo { get { return VueloClase != null ? VueloClase.Vuelo : null; } }
        public ClaseVuelo_GV42 Clase { get { return VueloClase != null ? VueloClase.Clase : ClaseVuelo_GV42.Economica; } }
        public int CantidadPasajeros { get { return Pasajeros != null ? Pasajeros.Count : 0; } }
        public string EstadoTexto { get { return Estado.Texto(); } }

        #endregion
    }
}
