using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class DALBitacora_GV42
    {
        private readonly Acceso _acceso;
        private const string SELECT_BASE =
            "SELECT E.UserName, " +
            "       E.IdModulo,     M.Nombre AS ModuloNombre, " +
            "       E.IdTipoEvento, T.Nombre AS TipoEventoNombre, " +
            "       E.Detalle, E.Criticidad, E.FechaHora " +
            "FROM EVENTOS E " +
            "INNER JOIN Modulo M     ON M.Id = E.IdModulo " +
            "INNER JOIN TipoEvento T ON T.Id = E.IdTipoEvento";

        public DALBitacora_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        public void Guardar(Bitacora_GV42 registro)
        {
            int idModulo = ResolverIdModulo(registro.Modulo);
            int idTipoEvento = ResolverIdTipoEvento(registro.TipoEvento);

            // EVENTOS.Detalle es NVARCHAR(500): se recorta para que un detalle largo
            // (por ejemplo, muchos registros alterados) no haga fallar el INSERT.
            const int MAX_DETALLE = 500;
            const string SUFIJO = "... (truncado)";

            string detalle = registro.Detalle;
            if (detalle != null && detalle.Length > MAX_DETALLE)
                detalle = detalle.Substring(0, MAX_DETALLE - SUFIJO.Length) + SUFIJO;

            string query =
                "INSERT INTO EVENTOS (UserName, IdModulo, IdTipoEvento, Detalle, Criticidad, FechaHora) " +
                "VALUES (@UserName, @IdModulo, @IdTipoEvento, @Detalle, @Criticidad, @FechaHora)";

            SqlParameter[] parametros = {
                new SqlParameter("@UserName",     registro.Login),
                new SqlParameter("@IdModulo",     idModulo),
                new SqlParameter("@IdTipoEvento", idTipoEvento),
                new SqlParameter("@Detalle",      (object)detalle ?? DBNull.Value),
                new SqlParameter("@Criticidad",   registro.Criticidad),
                new SqlParameter("@FechaHora",    registro.FechaHora)
            };

            _acceso.escribir(query, parametros);
        }

        public List<Bitacora_GV42> Listar()
        {
            string query = SELECT_BASE + " ORDER BY E.FechaHora DESC";
            return MapearLista(_acceso.leer(query, null));
        }

        public List<Bitacora_GV42> Filtrar(string login, string modulo, string tipoEvento, string criticidad, DateTime fechaInicio, DateTime fechaFin)
        {
            StringBuilder sb = new StringBuilder(SELECT_BASE +
                " WHERE E.FechaHora BETWEEN @FechaInicio AND @FechaFin");

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@FechaInicio", fechaInicio),
                new SqlParameter("@FechaFin", fechaFin)
            };

            if (!string.IsNullOrWhiteSpace(login))
            {
                sb.Append(" AND E.UserName LIKE @Login");
                parametros.Add(new SqlParameter("@Login", "%" + login + "%"));
            }

            if (!string.IsNullOrWhiteSpace(modulo))
            {
                sb.Append(" AND M.Nombre = @Modulo");
                parametros.Add(new SqlParameter("@Modulo", modulo));
            }

            if (!string.IsNullOrWhiteSpace(tipoEvento))
            {
                sb.Append(" AND T.Nombre LIKE @TipoEvento");
                parametros.Add(new SqlParameter("@TipoEvento", tipoEvento + "%"));
            }

            if (!string.IsNullOrWhiteSpace(criticidad))
            {
                sb.Append(" AND E.Criticidad = @Criticidad");
                parametros.Add(new SqlParameter("@Criticidad", criticidad));
            }

            sb.Append(" ORDER BY E.FechaHora DESC");

            return MapearLista(_acceso.leer(sb.ToString(), parametros.ToArray()));
        }

        private List<Bitacora_GV42> MapearLista(DataTable dt)
        {
            List<Bitacora_GV42> lista = new List<Bitacora_GV42>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Bitacora_GV42
                {
                    Login = row["UserName"].ToString(),
                    Modulo = new Modulo_GV42
                    {
                        Id = Convert.ToInt32(row["IdModulo"]),
                        Nombre = row["ModuloNombre"].ToString()
                    },
                    TipoEvento = new TipoEvento_GV42
                    {
                        Id = Convert.ToInt32(row["IdTipoEvento"]),
                        Nombre = row["TipoEventoNombre"].ToString()
                    },
                    Detalle = row["Detalle"] == DBNull.Value ? "" : row["Detalle"].ToString(),
                    Criticidad = row["Criticidad"].ToString(),
                    FechaHora = Convert.ToDateTime(row["FechaHora"])
                });
            }
            return lista;
        }

        public List<string> ListarTiposEvento()
        {
            List<TipoEvento_GV42> entidades = ListarEventos();
            List<string> tipos = new List<string>();
            foreach (TipoEvento_GV42 t in entidades) tipos.Add(t.Nombre);
            return tipos;
        }

        public List<string> ListarModulos()
        {
            List<Modulo_GV42> entidades = ListarModulo();
            List<string> modulos = new List<string>();
            foreach (Modulo_GV42 m in entidades) modulos.Add(m.Nombre);
            return modulos;
        }

        private int ResolverIdModulo(Modulo_GV42 m)
        {
            if (m == null)
                throw new Exception(IdiomaManager_GV42.T("err.bitacoraSinModulo"));

            if (m.Id > 0) return m.Id;

            Modulo_GV42 enBase = BuscarModulo(m.Nombre);
            if (enBase == null)
                throw new Exception($"El módulo '{m.Nombre}' no existe en la tabla Modulo. " +
                                    "Agregalo al catálogo antes de registrar el evento.");
            return enBase.Id;
        }

        private int ResolverIdTipoEvento(TipoEvento_GV42 t)
        {
            if (t == null)
                throw new Exception(IdiomaManager_GV42.T("err.bitacoraSinTipoEvento"));

            if (t.Id > 0) return t.Id;

            TipoEvento_GV42 enBase = BuscarEvento(t.Nombre);
            if (enBase == null)
                throw new Exception($"El tipo de evento '{t.Nombre}' no existe en la tabla TipoEvento. " +
                                    "Agregalo al catálogo antes de registrar el evento.");
            return enBase.Id;
        }

        public List<TipoEvento_GV42> ListarEventos()
        {
            string query = "SELECT Id, Nombre FROM TipoEvento ORDER BY Nombre";
            DataTable dt = _acceso.leer(query, null);

            List<TipoEvento_GV42> lista = new List<TipoEvento_GV42>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new TipoEvento_GV42
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }
            return lista;
        }

        public TipoEvento_GV42 BuscarEvento(string nombre)
        {
            string query = "SELECT Id, Nombre FROM TipoEvento WHERE Nombre = @Nombre";
            SqlParameter[] p = { new SqlParameter("@Nombre", nombre) };
            DataTable dt = _acceso.leer(query, p);

            if (dt.Rows.Count == 0) return null;

            return new TipoEvento_GV42
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Nombre = dt.Rows[0]["Nombre"].ToString()
            };
        }

        public List<Modulo_GV42> ListarModulo()
        {
            string query = "SELECT Id, Nombre FROM Modulo ORDER BY Nombre";
            DataTable dt = _acceso.leer(query, null);

            List<Modulo_GV42> lista = new List<Modulo_GV42>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Modulo_GV42
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }
            return lista;
        }

        public Modulo_GV42 BuscarModulo(string nombre)
        {
            string query = "SELECT Id, Nombre FROM Modulo WHERE Nombre = @Nombre";
            SqlParameter[] p = { new SqlParameter("@Nombre", nombre) };
            DataTable dt = _acceso.leer(query, p);

            if (dt.Rows.Count == 0) return null;

            return new Modulo_GV42
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Nombre = dt.Rows[0]["Nombre"].ToString()
            };
        }
    }
}
