using System;

namespace BE
{
    public class Aeropuerto_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public string CodigoIata { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }

        public string Descripcion
        {
            get { return Ciudad + " (" + CodigoIata + ")"; }
        }

        #endregion

        #region Métodos públicos

        public override string ToString() { return Descripcion; }

        #endregion
    }
}
