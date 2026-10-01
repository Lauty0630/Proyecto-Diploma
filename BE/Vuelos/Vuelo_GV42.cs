using System;

namespace BE
{
    public class Vuelo_GV42
    {
        #region Propiedades

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

        // Borrado lógico: un vuelo dado de baja no se ofrece para nuevas reservas, pero no se elimina.
        public bool BorradoLogico { get; set; }

        public string Descripcion
        {
            get { return CodigoVuelo + " - " + (Origen != null ? Origen.CodigoIata : "?") + " -> " + (Destino != null ? Destino.CodigoIata : "?"); }
        }

        #endregion
    }
}
