using System;
using System.Collections.Generic;

namespace BE
{
    // Check-in de un pasajero para una reserva (RFN 2).
    // Id vale 0 si la reserva todavía no generó el registro de check-in (reserva sin pago confirmado).
    public class CheckIn_GV42
    {
        public int Id { get; set; }
        public int IdReserva { get; set; }
        public string NumeroReserva { get; set; }
        public Pasajero_GV42 Pasajero { get; set; }
        public VueloClase_GV42 VueloClase { get; set; }
        public EstadoReserva_GV42 EstadoReserva { get; set; }
        public EstadoCheckIn_GV42 Estado { get; set; } = EstadoCheckIn_GV42.Pendiente;
        public DateTime? FechaHoraCheckIn { get; set; }
        public string LoginEncargado { get; set; }

        public List<AdicionalReserva_GV42> ServiciosAdicionales { get; set; } = new List<AdicionalReserva_GV42>();

        public Asiento_GV42 Asiento { get; set; }
        public Equipaje_GV42 Equipaje { get; set; }
        public TarjetaEmbarque_GV42 TarjetaEmbarque { get; set; }

        public Vuelo_GV42 Vuelo { get { return VueloClase != null ? VueloClase.Vuelo : null; } }
        public string EstadoReservaTexto { get { return EstadoReserva.Texto(); } }
        public string EstadoTexto { get { return Estado.Texto(); } }
    }
}
