using System;

namespace BE
{
    public class Asiento_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdVuelo { get; set; }

        // Ej: 12A
        public string NumeroAsiento { get; set; }
        public ClaseVuelo_GV42 Clase { get; set; }

        // Ventana / Central / Pasillo
        public string Ubicacion { get; set; }

        public string ClaseTexto { get { return Clase.Texto(); } }

        #endregion

        #region Métodos públicos

        public override string ToString()
        {
            return NumeroAsiento + " - " + Ubicacion;
        }

        #endregion
    }
}
