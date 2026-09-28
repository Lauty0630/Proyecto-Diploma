using System;

namespace BE
{
    // Un vuelo ofrecido en una clase concreta, con su precio y disponibilidad.
    // Es lo que devuelve la búsqueda de vuelos disponibles (RFN 1, paso 4).
    public class VueloClase_GV42
    {
        public Vuelo_GV42 Vuelo { get; set; }
        public ClaseVuelo_GV42 Clase { get; set; }
        public decimal PrecioBase { get; set; }
        public int CapacidadAsientos { get; set; }
        public int AsientosReservados { get; set; }

        // Kilos de equipaje despachado sin cargo por pasajero (RFN 2).
        public decimal FranquiciaEquipajeKg { get; set; }

        public int AsientosDisponibles
        {
            get { return CapacidadAsientos - AsientosReservados; }
        }

        // Propiedades planas, cómodas para enlazar a una grilla.
        public string CodigoVuelo { get { return Vuelo != null ? Vuelo.CodigoVuelo : string.Empty; } }
        public string AerolineaNombre { get { return Vuelo != null && Vuelo.Aerolinea != null ? Vuelo.Aerolinea.Nombre : string.Empty; } }
        public string OrigenDescripcion { get { return Vuelo != null && Vuelo.Origen != null ? Vuelo.Origen.Descripcion : string.Empty; } }
        public string DestinoDescripcion { get { return Vuelo != null && Vuelo.Destino != null ? Vuelo.Destino.Descripcion : string.Empty; } }
        public DateTime FechaHoraSalida { get { return Vuelo != null ? Vuelo.FechaHoraSalida : DateTime.MinValue; } }
        public DateTime FechaHoraLlegada { get { return Vuelo != null ? Vuelo.FechaHoraLlegada : DateTime.MinValue; } }
        public string ClaseTexto { get { return Clase.Texto(); } }
    }
}
