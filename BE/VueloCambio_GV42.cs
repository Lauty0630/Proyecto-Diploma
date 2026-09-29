using System;

namespace BE
{
    // Un registro de la bitácora de cambios de vuelos (tabla Vuelo_C): una "versión" del vuelo.
    public class VueloCambio_GV42
    {
        public int Id { get; set; }
        public int IdVuelo { get; set; }
        public string CodigoVuelo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        // Ruta de esa versión, ej: "AEP -> COR".
        public string Nombre { get; set; }
        public string Aerolinea { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public string PuertaEmbarque { get; set; }
        public decimal CostoKiloExceso { get; set; }
        public bool BorradoLogico { get; set; }

        // True en el único registro que refleja lo que hoy hay en la tabla Vuelo.
        public bool Act { get; set; }

        public string Descripcion
        {
            get
            {
                return Aerolinea + " | Sale " + FechaHoraSalida.ToString("dd/MM/yyyy HH:mm") +
                       " - llega " + FechaHoraLlegada.ToString("dd/MM/yyyy HH:mm") +
                       " | Puerta " + PuertaEmbarque +
                       " | $" + CostoKiloExceso.ToString("N2") + "/kg" +
                       (BorradoLogico ? " | DADO DE BAJA" : string.Empty);
            }
        }
    }
}
