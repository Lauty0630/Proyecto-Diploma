using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{

    // Acceso a la base. Cada operación abre su propia SqlConnection y la cierra al terminar
    // (el pool de conexiones de ADO.NET reutiliza las conexiones físicas, así que no es más lento).
    //
    // Antes había UNA sola SqlConnection compartida por toda la aplicación. El backup programado corre
    // en otro hilo (System.Threading.Timer) y usaba esa misma conexión al mismo tiempo que la pantalla:
    // uno podía cerrarle la conexión al otro en medio de una transacción. Además los errores de
    // conexión y de COMMIT se escribían en la consola y se seguía como si nada (escribir devolvía las
    // filas afectadas aunque el commit hubiera fallado). Ahora todos esos errores se relanzan.
    public class Acceso
    {
        #region Campos

        private static readonly Acceso _instancia = new Acceso();

        private const string CADENA_POR_DEFECTO =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=""Gestion Usuario"";Integrated Security=True";

        #endregion

        #region Constructor

        private Acceso() { }

        #endregion

        #region Propiedades

        public static string ConnectionString { get; set; }
        public static string InstanciaActual { get; set; }

        public static Acceso Instancia
        {
            get { return _instancia; }
        }

        #endregion

        #region Métodos públicos

        // Abre una transacción sobre una conexión nueva. Se termina con ConfirmarTransaccion o
        // CancelarTransaccion, que además cierran la conexión.
        public SqlTransaction IniciarTransaccion()
        {
            SqlConnection conexion = AbrirConexion();
            try
            {
                return conexion.BeginTransaction();
            }
            catch
            {
                conexion.Dispose();
                throw;
            }
        }

        public void ConfirmarTransaccion(SqlTransaction tx)
        {
            if (tx == null) return;
            SqlConnection conexion = tx.Connection;   // después del Commit tx.Connection queda en null
            try
            {
                tx.Commit();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al confirmar la transacción: " + ex.Message, ex);
            }
            finally
            {
                conexion?.Dispose();
            }
        }

        public void CancelarTransaccion(SqlTransaction tx)
        {
            if (tx == null) return;
            SqlConnection conexion = tx.Connection;
            try
            {
                tx.Rollback();
            }
            catch (Exception)
            {
                // Si la transacción ya se había deshecho (por ejemplo, la conexión se cortó), no hay
                // nada más que hacer: el error que importa es el original, que relanza quien llamó.
            }
            finally
            {
                conexion?.Dispose();
            }
        }

        public int escribir(string query, SqlParameter[] parametro)
        {
            SqlTransaction tx = null;
            try
            {
                tx = IniciarTransaccion();
                int filasAfectadas;
                using (SqlCommand comando = CrearComando(tx.Connection, tx, query, parametro))
                    filasAfectadas = comando.ExecuteNonQuery();
                ConfirmarTransaccion(tx);
                tx = null;
                return filasAfectadas;
            }
            catch (Exception ex)
            {
                CancelarTransaccion(tx);
                throw new Exception("Error en Escribir: " + ex.Message, ex);
            }
        }

        public DataTable leer(string query, SqlParameter[] parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = AbrirConexion())
                using (SqlCommand comando = CrearComando(conexion, null, query, parametro))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en Leer: " + ex.Message, ex);
            }
            return dt;
        }

        public object leerEscalar(string query, SqlParameter[] parametro)
        {
            try
            {
                using (SqlConnection conexion = AbrirConexion())
                using (SqlCommand comando = CrearComando(conexion, null, query, parametro))
                {
                    return comando.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                // Antes se tragaba el error y se devolvía null: por ejemplo, "¿existe el DNI?" daba
                // false ante un error de base y se salteaba el control de duplicados.
                throw new Exception("Error en LeerEscalar: " + ex.Message, ex);
            }
        }

        // Soporte para operaciones de negocio que escriben en varias tablas.

        // Ejecuta 'trabajo' dentro de una única transacción. Si algo falla hace rollback
        // y relanza la excepción; si termina bien hace commit.
        public T EjecutarEnTransaccion<T>(Func<SqlTransaction, T> trabajo)
        {
            using (SqlConnection conexion = AbrirConexion())
            using (SqlTransaction tx = conexion.BeginTransaction())
            {
                try
                {
                    T resultado = trabajo(tx);
                    tx.Commit();
                    return resultado;
                }
                catch
                {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
            }
        }

        // Las tres sobrecargas siguientes usan la transacción recibida y NO cierran la conexión.
        public int escribir(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            using (SqlCommand comando = CrearComando(tx.Connection, tx, query, parametro))
            {
                return comando.ExecuteNonQuery();
            }
        }

        public object leerEscalar(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            using (SqlCommand comando = CrearComando(tx.Connection, tx, query, parametro))
            {
                return comando.ExecuteScalar();
            }
        }

        public DataTable leer(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            DataTable dt = new DataTable();
            using (SqlCommand comando = CrearComando(tx.Connection, tx, query, parametro))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                adaptador.Fill(dt);
            }
            return dt;
        }

        #endregion

        #region Métodos privados

        private static SqlConnection AbrirConexion()
        {
            string cs = string.IsNullOrEmpty(ConnectionString) ? CADENA_POR_DEFECTO : ConnectionString;
            var conexion = new SqlConnection(cs);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (SqlException ex)
            {
                conexion.Dispose();
                // Antes el error se escribía en la consola y después fallaba la consulta con un mensaje
                // confuso ("la conexión no está abierta"). Ahora se informa el problema real.
                throw new Exception("No se pudo conectar con la base de datos: " + ex.Message, ex);
            }
        }

        // Los parámetros se clonan: así el mismo SqlParameter[] se puede reutilizar en otra consulta
        // (un SqlParameter no puede pertenecer a dos comandos) y se conserva el tipo si se indicó.
        private static SqlCommand CrearComando(SqlConnection conexion, SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            SqlCommand comando = new SqlCommand(query, conexion, tx);
            if (parametro != null)
            {
                foreach (SqlParameter param in parametro)
                {
                    var copia = (SqlParameter)((ICloneable)param).Clone();
                    if (copia.Value == null) copia.Value = DBNull.Value;
                    comando.Parameters.Add(copia);
                }
            }
            return comando;
        }

        #endregion
    }
}
