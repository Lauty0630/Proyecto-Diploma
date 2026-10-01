using BE;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class DALAeropuerto_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALAeropuerto_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<Aeropuerto_GV42> ListarTodos()
        {
            string query = "SELECT Id, CodigoIata, Nombre, Ciudad, Pais FROM Aeropuerto ORDER BY Ciudad, Nombre";
            DataTable dt = _acceso.leer(query, null);

            var lista = new List<Aeropuerto_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                lista.Add(new Aeropuerto_GV42
                {
                    Id = DALUtil_GV42.Int(r, "Id"),
                    CodigoIata = DALUtil_GV42.Str(r, "CodigoIata"),
                    Nombre = DALUtil_GV42.Str(r, "Nombre"),
                    Ciudad = DALUtil_GV42.Str(r, "Ciudad"),
                    Pais = DALUtil_GV42.Str(r, "Pais")
                });
            }
            return lista;
        }

        #endregion
    }
}
