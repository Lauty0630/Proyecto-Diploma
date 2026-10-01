using System.Collections.Generic;
using System.Linq;

namespace Servicios
{

    public class Rol_GV42 : IComponentePermiso_GV42
    {
        #region Campos

        private int _Id;

        private string _Nombre;

        #endregion

        #region Constructor

        public Rol_GV42() { }

        public Rol_GV42(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        #endregion

        #region Propiedades

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

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

        public bool TienePermiso(string dataKey)
        {
            if (string.IsNullOrEmpty(dataKey)) return false;
            return ObtenerPatentes().Any(p => p.DataKey == dataKey);
        }

        public override string ToString()
        {
            return Nombre ?? string.Empty;
        }

        #endregion
    }
}
