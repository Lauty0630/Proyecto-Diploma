using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{

    public class DALPatente_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALPatente_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<Patente_GV42> ListarTodas()
        {
            string query = "SELECT Id, Nombre, DataKey FROM Patente ORDER BY Nombre";
            DataTable dt = _acceso.leer(query, null);

            var lista = new List<Patente_GV42>();
            foreach (DataRow row in dt.Rows)
                lista.Add(MapearFila(row));
            return lista;
        }

        public Patente_GV42 BuscarPorId(int id)
        {
            string query = "SELECT Id, Nombre, DataKey FROM Patente WHERE Id = @Id";
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable dt = _acceso.leer(query, p);
            return dt.Rows.Count == 0 ? null : MapearFila(dt.Rows[0]);
        }

        #endregion

        #region Métodos privados

        private Patente_GV42 MapearFila(DataRow row)
        {
            return new Patente_GV42
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                DataKey = row["DataKey"].ToString()
            };
        }

        #endregion
    }
}
