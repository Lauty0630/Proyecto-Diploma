using System;

namespace BE
{
    // Datos que el cliente le pide al vendedor para iniciar la reserva (RFN 1, pasos 1 a 3).
    public class CriterioBusquedaVuelo_GV42
    {
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadPasajeros { get; set; }
        public TipoViaje_GV42 TipoViaje { get; set; }

        // Obligatoria solo si TipoViaje es IdaYVuelta.
        public DateTime? FechaRegreso { get; set; }
    }
}
