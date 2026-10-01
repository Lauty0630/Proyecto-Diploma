using System;

namespace BE
{
    public class Aerolinea_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }

        #endregion

        #region Métodos públicos

        public override string ToString() { return Nombre ?? string.Empty; }

        #endregion
    }
}
