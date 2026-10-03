using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        // ¿Hay otro aeropuerto con ese código IATA? (idExcluir: el que se está modificando; 0 en el alta).
        public bool ExisteCodigo(string codigoIata, int idExcluir)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Aeropuerto WHERE CodigoIata = @Cod AND Id <> @Id",
                new[] { new SqlParameter("@Cod", codigoIata), new SqlParameter("@Id", idExcluir) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        // Vuelos (vigentes o del historial de cambios) que salen de ese aeropuerto o llegan a él.
        public int ContarVuelos(int id)
        {
            object r = _acceso.leerEscalar(
                "SELECT (SELECT COUNT(1) FROM Vuelo WHERE IdOrigen = @Id OR IdDestino = @Id) + " +
                "       (SELECT COUNT(1) FROM Vuelo_C WHERE IdOrigen = @Id OR IdDestino = @Id)",
                new[] { new SqlParameter("@Id", id) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        public void Insertar(Aeropuerto_GV42 a)
        {
            _acceso.escribir(
                "INSERT INTO Aeropuerto (CodigoIata, Nombre, Ciudad, Pais) VALUES (@Cod, @Nombre, @Ciudad, @Pais)",
                new[] {
                    new SqlParameter("@Cod", a.CodigoIata),
                    new SqlParameter("@Nombre", a.Nombre),
                    new SqlParameter("@Ciudad", a.Ciudad),
                    new SqlParameter("@Pais", a.Pais)
                });
        }

        // El código IATA identifica al aeropuerto: no se modifica.
        public void Modificar(Aeropuerto_GV42 a)
        {
            _acceso.escribir(
                "UPDATE Aeropuerto SET Nombre = @Nombre, Ciudad = @Ciudad, Pais = @Pais WHERE Id = @Id",
                new[] {
                    new SqlParameter("@Id", a.Id),
                    new SqlParameter("@Nombre", a.Nombre),
                    new SqlParameter("@Ciudad", a.Ciudad),
                    new SqlParameter("@Pais", a.Pais)
                });
        }

        public void Eliminar(int id)
        {
            _acceso.escribir("DELETE FROM Aeropuerto WHERE Id = @Id", new[] { new SqlParameter("@Id", id) });
        }

        #endregion
    }
}
