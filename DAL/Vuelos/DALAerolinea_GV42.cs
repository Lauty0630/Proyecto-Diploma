using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALAerolinea_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALAerolinea_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<Aerolinea_GV42> ListarTodos()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM Aerolinea ORDER BY Nombre", null);

            var lista = new List<Aerolinea_GV42>();
            foreach (DataRow r in dt.Rows)
                lista.Add(new Aerolinea_GV42 { Id = DALUtil_GV42.Int(r, "Id"), Nombre = DALUtil_GV42.Str(r, "Nombre") });
            return lista;
        }

        // Vuelos (vigentes o del historial de cambios) que opera esa aerolínea.
        public int ContarVuelos(int id)
        {
            object r = _acceso.leerEscalar(
                "SELECT (SELECT COUNT(1) FROM Vuelo WHERE IdAerolinea = @Id) + " +
                "       (SELECT COUNT(1) FROM Vuelo_C WHERE IdAerolinea = @Id)",
                new[] { new SqlParameter("@Id", id) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        public void Insertar(Aerolinea_GV42 a)
        {
            _acceso.escribir("INSERT INTO Aerolinea (Nombre) VALUES (@Nombre)",
                new[] { new SqlParameter("@Nombre", a.Nombre) });
        }

        public void Modificar(Aerolinea_GV42 a)
        {
            _acceso.escribir("UPDATE Aerolinea SET Nombre = @Nombre WHERE Id = @Id",
                new[] { new SqlParameter("@Id", a.Id), new SqlParameter("@Nombre", a.Nombre) });
        }

        public void Eliminar(int id)
        {
            _acceso.escribir("DELETE FROM Aerolinea WHERE Id = @Id", new[] { new SqlParameter("@Id", id) });
        }

        #endregion
    }
}
