using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{

    public class DALRol_GV42
    {
        #region Campos

        private readonly Acceso _acceso;
        private readonly DALFamilia_GV42 _dalFamilia;
        private readonly DALPatente_GV42 _dalPatente;

        #endregion

        #region Constructor

        public DALRol_GV42()
        {
            _acceso = Acceso.Instancia;
            _dalFamilia = new DALFamilia_GV42();
            _dalPatente = new DALPatente_GV42();
        }

        #endregion

        #region Métodos públicos

        public List<Rol_GV42> ListarTodos()
        {
            string q = "SELECT Id, Nombre FROM Roles ORDER BY Nombre";
            DataTable dt = _acceso.leer(q, null);

            var lista = new List<Rol_GV42>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Rol_GV42
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }
            return lista;
        }

        public Rol_GV42 ObtenerArbol(int idRol)
        {

            string qRol = "SELECT Id, Nombre FROM Roles WHERE Id = @Id";
            DataTable dtRol = _acceso.leer(qRol, new[] { new SqlParameter("@Id", idRol) });
            if (dtRol.Rows.Count == 0) return null;

            var rol = new Rol_GV42
            {
                Id = Convert.ToInt32(dtRol.Rows[0]["Id"]),
                Nombre = dtRol.Rows[0]["Nombre"].ToString()
            };

            string qPat =
                "SELECT P.Id, P.Nombre, P.DataKey " +
                "FROM RolPatente RP " +
                "INNER JOIN Patente P ON P.Id = RP.IdPatente " +
                "WHERE RP.IdRol = @Id";
            DataTable dtPat = _acceso.leer(qPat, new[] { new SqlParameter("@Id", idRol) });
            foreach (DataRow row in dtPat.Rows)
            {
                rol.Hijos.Add(new Patente_GV42
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                    DataKey = row["DataKey"].ToString()
                });
            }

            string qFam = "SELECT IdFamilia FROM RolFamilia WHERE IdRol = @Id";
            DataTable dtFam = _acceso.leer(qFam, new[] { new SqlParameter("@Id", idRol) });
            foreach (DataRow row in dtFam.Rows)
            {
                int idFam = Convert.ToInt32(row["IdFamilia"]);
                Familia_GV42 fam = _dalFamilia.ObtenerArbol(idFam);
                if (fam != null) rol.Hijos.Add(fam);
            }

            return rol;
        }

        public int Crear(string nombre, List<int> idsPatentes, List<int> idsFamilias)
        {
            // Todo en una transacción: si falla un INSERT no queda un rol a medio crear.
            return _acceso.EjecutarEnTransaccion(tx =>
            {
                object res = _acceso.leerEscalar(tx,
                    "INSERT INTO Roles (Nombre) VALUES (@Nombre); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new[] { new SqlParameter("@Nombre", nombre) });
                if (res == null || res == DBNull.Value)
                    throw new Exception(IdiomaManager_GV42.T("err.rolSinIdCreado"));

                int idRol = Convert.ToInt32(res);
                InsertarHijos(tx, idRol, idsPatentes, idsFamilias);
                return idRol;
            });
        }

        public bool EstaEnUso(int idRol)
        {
            string q = "SELECT COUNT(1) FROM Usuario WHERE IdRol = @Id";
            object res = _acceso.leerEscalar(q, new[] { new SqlParameter("@Id", idRol) });
            return Convert.ToInt32(res) > 0;
        }

        public int CantidadUsuariosConRol(int idRol)
        {
            string q = "SELECT COUNT(1) FROM Usuario WHERE IdRol = @Id";
            object res = _acceso.leerEscalar(q, new[] { new SqlParameter("@Id", idRol) });
            return Convert.ToInt32(res);
        }

        public void Eliminar(int idRol)
        {
            _acceso.EjecutarEnTransaccion(tx =>
            {
                _acceso.escribir(tx, "DELETE FROM RolPatente WHERE IdRol = @Id", new[] { new SqlParameter("@Id", idRol) });
                _acceso.escribir(tx, "DELETE FROM RolFamilia WHERE IdRol = @Id", new[] { new SqlParameter("@Id", idRol) });
                return _acceso.escribir(tx, "DELETE FROM Roles WHERE Id = @Id", new[] { new SqlParameter("@Id", idRol) });
            });
        }

        public void Modificar(int idRol, string nombre, List<int> idsPatentes, List<int> idsFamilias)
        {
            // Antes cada sentencia iba en su propia transacción: si fallaba un INSERT, el rol
            // (por ejemplo Admin) quedaba sin patentes. Ahora es todo o nada.
            _acceso.EjecutarEnTransaccion(tx =>
            {
                _acceso.escribir(tx, "UPDATE Roles SET Nombre = @Nombre WHERE Id = @Id",
                    new[] { new SqlParameter("@Nombre", nombre), new SqlParameter("@Id", idRol) });
                _acceso.escribir(tx, "DELETE FROM RolPatente WHERE IdRol = @Id", new[] { new SqlParameter("@Id", idRol) });
                _acceso.escribir(tx, "DELETE FROM RolFamilia WHERE IdRol = @Id", new[] { new SqlParameter("@Id", idRol) });
                InsertarHijos(tx, idRol, idsPatentes, idsFamilias);
                return 0;
            });
        }

        public List<int> IdsPatentesDirectas(int idRol)
        {
            string q = "SELECT IdPatente FROM RolPatente WHERE IdRol = @Id ORDER BY IdPatente";
            DataTable dt = _acceso.leer(q, new[] { new SqlParameter("@Id", idRol) });
            return dt.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["IdPatente"])).ToList();
        }

        public List<int> IdsFamiliasDirectas(int idRol)
        {
            string q = "SELECT IdFamilia FROM RolFamilia WHERE IdRol = @Id ORDER BY IdFamilia";
            DataTable dt = _acceso.leer(q, new[] { new SqlParameter("@Id", idRol) });
            return dt.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["IdFamilia"])).ToList();
        }

        #endregion

        #region Métodos privados

        private void InsertarHijos(SqlTransaction tx, int idRol, List<int> idsPatentes, List<int> idsFamilias)
        {
            foreach (int idPat in idsPatentes ?? new List<int>())
                _acceso.escribir(tx, "INSERT INTO RolPatente (IdRol, IdPatente) VALUES (@R, @P)",
                    new[] { new SqlParameter("@R", idRol), new SqlParameter("@P", idPat) });

            foreach (int idFam in idsFamilias ?? new List<int>())
                _acceso.escribir(tx, "INSERT INTO RolFamilia (IdRol, IdFamilia) VALUES (@R, @F)",
                    new[] { new SqlParameter("@R", idRol), new SqlParameter("@F", idFam) });
        }

        #endregion
    }
}
