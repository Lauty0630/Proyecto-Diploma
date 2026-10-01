using System.Collections.Generic;

namespace Servicios
{

    public class Patente_GV42 : IComponentePermiso_GV42
    {
        #region Constructor

        public Patente_GV42() { }

        public Patente_GV42(int id, string nombre, string dataKey)
        {
            Id = id;
            Nombre = nombre;
            DataKey = dataKey;
        }

        #endregion

        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }

        public string DataKey { get; set; }

        #endregion

        #region Métodos públicos

        public IEnumerable<Patente_GV42> ObtenerPatentes()
        {
            yield return this;
        }

        public override string ToString() => Nombre ?? string.Empty;

        #endregion
    }
}
