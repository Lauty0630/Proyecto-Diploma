using System;

namespace BE
{
    // Vincula un pasajero de la reserva con el asiento que eligió (selección estilo "cine").
    public class AsientoPasajero_GV42
    {
        public string DniPasajero { get; set; }
        public Asiento_GV42 Asiento { get; set; }

        public AsientoPasajero_GV42() { }

        public AsientoPasajero_GV42(string dniPasajero, Asiento_GV42 asiento)
        {
            DniPasajero = dniPasajero;
            Asiento = asiento;
        }
    }
}
