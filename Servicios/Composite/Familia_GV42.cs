using System.Collections.Generic;
using System.Linq;

namespace Servicios
{

    public class Familia_GV42 : IComponentePermiso_GV42
    {
        #region Constructor

        public Familia_GV42() { }

        public Familia_GV42(int id, string nombre) { Id = id; Nombre = nombre; }

        #endregion

        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<IComponentePermiso_GV42> Hijos { get; set; } = new List<IComponentePermiso_GV42>();

        #endregion

        #region Métodos públicos

        public IEnumerable<Patente_GV42> ObtenerPatentes()
        {
            var vistas = new HashSet<int>();
            foreach (var hijo in Hijos)
            {
                foreach (var p in hijo.ObtenerPatentes())
                {
                    if (vistas.Add(p.Id))
                        yield return p;
                }
            }
        }

        public override string ToString() => Nombre ?? string.Empty;

        #endregion
    }
}
