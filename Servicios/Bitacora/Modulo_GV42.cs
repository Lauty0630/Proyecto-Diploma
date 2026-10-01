using System;

namespace Servicios
{

    public class Modulo_GV42
    {
        #region Constructor

        public Modulo_GV42() { }

        public Modulo_GV42(int id, string nombre) { Id = id; Nombre = nombre; }

        #endregion

        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }

        #endregion

        #region Métodos públicos

        public override string ToString() => Nombre ?? string.Empty;

        #endregion
    }
}
