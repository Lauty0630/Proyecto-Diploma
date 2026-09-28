using System;

namespace BE
{
    public class Vuelo_GV42
    {
        public int Id { get; set; }
        public string CodigoVuelo { get; set; }
        public Aerolinea_GV42 Aerolinea { get; set; }
        public Aeropuerto_GV42 Origen { get; set; }
        public Aeropuerto_GV42 Destino { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public string PuertaEmbarque { get; set; }

        // Tarifa por cada kilo que exceda la franquicia de equipaje (RFN 2).
        public decimal CostoKiloExceso { get; set; }
    }
}
