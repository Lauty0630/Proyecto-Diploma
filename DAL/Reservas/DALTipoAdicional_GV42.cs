using BE;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class DALTipoAdicional_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALTipoAdicional_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<TipoAdicional_GV42> ListarActivos()
        {
            string query = "SELECT Id, Nombre, PrecioUnitario, MaxPorPasajero FROM TipoAdicional WHERE Activo = 1 ORDER BY Nombre";
            DataTable dt = _acceso.leer(query, null);

            var lista = new List<TipoAdicional_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new TipoAdicional_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    Nombre = DALUtil_GV42.Str(r, "Nombre"),
                    PrecioUnitario = DALUtil_GV42.Dec(r, "PrecioUnitario"),
                    MaxPorPasajero = System.Math.Max(1, DALUtil_GV42.Int(r, "MaxPorPasajero"))
                });
            }
            return lista;
        }

        #endregion
    }
}
