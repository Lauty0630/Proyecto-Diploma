using System;
using System.Collections.Generic;

namespace BE
{
    // Check-in de un pasajero para una reserva (RFN 2).
    // Id vale 0 si la reserva todavía no generó el registro de check-in (reserva sin pago confirmado).
    public class CheckIn_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdReserva { get; set; }
        public string NumeroReserva { get; set; }
        // Tramo del viaje: 1 = ida, 2 = vuelta. Cada tramo tiene su propio check-in (son vuelos distintos).
        public int Tramo { get; set; } = 1;
        // La reserva tiene vuelo de regreso (para aclarar el tramo en pantalla y en la tarjeta).
        public bool ReservaConVuelta { get; set; }
        public Pasajero_GV42 Pasajero { get; set; }
        public VueloClase_GV42 VueloClase { get; set; }
        public EstadoReserva_GV42 EstadoReserva { get; set; }
        public EstadoCheckIn_GV42 Estado { get; set; } = EstadoCheckIn_GV42.Pendiente;
        public DateTime? FechaHoraCheckIn { get; set; }
        public string LoginEncargado { get; set; }

        // Presencial (mostrador) u Autogestion (check-in online del cliente). Null mientras está pendiente.
        public CanalVenta_GV42? Canal { get; set; }

        // DNI del titular de la reserva (para que el cliente solo opere sobre sus reservas).
        public string DniTitular { get; set; }

        // Tipo de viaje de la reserva (el equipaje extra comprado se reparte entre los tramos).
        public TipoViaje_GV42 TipoViaje { get; set; }

        // Valijas de equipaje extra compradas para ESTE pasajero al reservar.
        public int EquipajeExtraComprado { get; set; }

        // Valijas despachadas que incluye la tarifa de la reserva (Light 0, Plus 1, Top 2).
        public int ValijasIncluidas { get; set; }
        public string TarifaNombre { get; set; }
        // La tarifa incluye elegir butaca preferencial sin recargo.
        public bool TarifaIncluyePreferencial { get; set; }

        public TipoPasajero_GV42 TipoPasajero { get; set; } = TipoPasajero_GV42.Adulto;
        public AsistenciaEspecial_GV42 Asistencia { get; set; } = AsistenciaEspecial_GV42.Ninguna;
        // Infantes de la reserva que viajan en brazos (se informan en el check-in de los adultos).
        public List<string> Infantes { get; set; } = new List<string>();

        public bool EsOnline { get { return Canal == CanalVenta_GV42.Autogestion; } }
        public string CanalTexto { get { return Canal.HasValue ? Canal.Value.Texto() : string.Empty; } }

        public List<AdicionalReserva_GV42> ServiciosAdicionales { get; set; } = new List<AdicionalReserva_GV42>();

        public Asiento_GV42 Asiento { get; set; }
        public Equipaje_GV42 Equipaje { get; set; }
        public TarjetaEmbarque_GV42 TarjetaEmbarque { get; set; }

        public Vuelo_GV42 Vuelo { get { return VueloClase != null ? VueloClase.Vuelo : null; } }
        public string EstadoReservaTexto { get { return EstadoReserva.Texto(); } }
        public string EstadoTexto { get { return Estado.Texto(); } }

        #endregion
    }
}
