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

    public class Acceso
    {
        private static Acceso _instancia;

        protected SqlConnection conexion = null;

        public static string ConnectionString { get; set; }
        public static string InstanciaActual { get; set; }

        private Acceso()
        {
            conexion = new SqlConnection();
        }

        public static Acceso Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Acceso();
                }
                return _instancia;
            }
        }

        public void conectar()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    string cs = ConnectionString;
                    if (string.IsNullOrEmpty(cs))
                        cs = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=""Gestion Usuario"";Integrated Security=True";
                    conexion.ConnectionString = cs;
                    conexion.Open();
                    Console.WriteLine("Conexión exitosa");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de conexión" + ex.Message);
            }
        }

        public void desconectar()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    Console.WriteLine("Desconexión exitosa.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al desconectar: " + ex.Message);
            }
        }

        public SqlTransaction IniciarTransaccion()
        {
            conectar();
            return conexion.BeginTransaction();
        }

        public void ConfirmarTransaccion(SqlTransaction tx)
        {
            try
            {
                if (tx != null)
                {
                    tx.Commit();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al confirmar la transacción: " + ex.Message);
            }
            finally
            {
                desconectar();
            }
        }

        public void CancelarTransaccion(SqlTransaction tx)
        {
            try
            {
                if (tx != null)
                {
                    tx.Rollback();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al cancelar la transacción: " + ex.Message);
            }
        }

        public int escribir(string query, SqlParameter[] parametro)
        {
            SqlTransaction tx = null;
            int filasAfectadas = 0;
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Parameters.Clear();
                tx = IniciarTransaccion();
                comando.Connection = tx.Connection;
                comando.Transaction = tx;
                comando.CommandText = query;
                if (parametro != null)
                {
                    foreach(SqlParameter param in parametro)
                    {
                        comando.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                }
                filasAfectadas = comando.ExecuteNonQuery();
                ConfirmarTransaccion(tx);
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
            SqlCommand comando = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            try
            {
                conectar();
                comando.Connection = conexion;
                comando.CommandText = query;
                if (parametro != null)
                {
                    foreach (SqlParameter param in parametro)
                    {
                        comando.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                }

                adaptador.SelectCommand = comando;
                adaptador.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en Leer: " + ex.Message);
            }
            finally
            {
                desconectar();
            }
            return dt;
        }

        public object leerEscalar(string query, SqlParameter[] parametro)
        {
            SqlCommand comando = new SqlCommand();
            object resultado = null;
            try
            {
                conectar();
                comando.Connection = conexion;
                comando.CommandText = query;

                if (parametro != null)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddRange(parametro);
                }
                resultado = comando.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                // Antes se tragaba el error y se devolvía null: por ejemplo, "¿existe el DNI?" daba
                // false ante un error de base y se salteaba el control de duplicados.
                throw new Exception("Error en LeerEscalar: " + ex.Message, ex);
            }
            finally
            {
                comando.Parameters.Clear();
                desconectar();
            }
            return resultado;
        }

      
        // Soporte para operaciones de negocio que escriben en varias tablas.
        

        // Ejecuta 'trabajo' dentro de una única transacción. Si algo falla hace rollback
        // y relanza la excepción; si termina bien hace commit.
        public T EjecutarEnTransaccion<T>(Func<SqlTransaction, T> trabajo)
        {
            SqlTransaction tx = null;
            try
            {
                tx = IniciarTransaccion();
                T resultado = trabajo(tx);
                tx.Commit();
                return resultado;
            }
            catch
            {
                if (tx != null)
                {
                    try { tx.Rollback(); } catch { }
                }
                throw;
            }
            finally
            {
                desconectar();
            }
        }

        // Las tres sobrecargas siguientes usan la transacción recibida y NO cierran la conexión.
        public int escribir(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            using (SqlCommand comando = CrearComando(tx, query, parametro))
            {
                return comando.ExecuteNonQuery();
            }
        }

        public object leerEscalar(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            using (SqlCommand comando = CrearComando(tx, query, parametro))
            {
                return comando.ExecuteScalar();
            }
        }

        public DataTable leer(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            DataTable dt = new DataTable();
            using (SqlCommand comando = CrearComando(tx, query, parametro))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                adaptador.Fill(dt);
            }
            return dt;
        }

        private SqlCommand CrearComando(SqlTransaction tx, string query, SqlParameter[] parametro)
        {
            SqlCommand comando = new SqlCommand(query, tx.Connection, tx);
            if (parametro != null)
            {
                foreach (SqlParameter param in parametro)
                {
                    comando.Parameters.AddWithValue(param.ParameterName, param.Value ?? DBNull.Value);
                }
            }
            return comando;
        }
    }
}
