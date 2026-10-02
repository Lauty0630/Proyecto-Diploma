using System;

namespace BE
{
    // Vincula un pasajero de la reserva con el asiento que eligió (selección estilo "cine").
    public class AsientoPasajero_GV42
    {
        #region Constructor

        public AsientoPasajero_GV42() { }

        public AsientoPasajero_GV42(string dniPasajero, Asiento_GV42 asiento)
        {
            DniPasajero = dniPasajero;
            Asiento = asiento;
        }

        #endregion

        #region Propiedades

        public string DniPasajero { get; set; }
        public Asiento_GV42 Asiento { get; set; }

        // Valijas de "Equipaje extra" compradas para este pasajero (por tramo). En el check-in
        // el pasajero puede despachar las valijas de su clase más estas.
        public int EquipajeExtra { get; set; }

        // Tramo del viaje al que corresponde el asiento: 1 = ida, 2 = vuelta. En ida y vuelta cada
        // pasajero tiene un asiento (y su equipaje extra) por tramo, porque son vuelos distintos.
        public int Tramo { get; set; } = 1;

        #endregion
    }
}
