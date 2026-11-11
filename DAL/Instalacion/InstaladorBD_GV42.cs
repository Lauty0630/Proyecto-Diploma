using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace DAL
{
    // Instala, actualiza y mantiene la base "Gestion Usuario" para que el sistema funcione en
    // cualquier computadora al bajar el proyecto:
    //  - Sin base: EsquemaCompleto.sql (estructura + datos iniciales) y luego ActualizacionBD.sql.
    //  - Con base de una versión anterior: ActualizacionBD.sql (idempotente, conserva los datos).
    //  - Con base demasiado vieja (le faltan tablas del negocio): la UI ofrece reinstalarla.
    //  - Siempre: genera los vuelos que falten para los próximos días (usp_GenerarVuelos_GV42).
    public static class InstaladorBD_GV42
    {
        #region Campos

        public const string NOMBRE_BD = "Gestion Usuario";
        private const string NOMBRE_SCRIPT = "EsquemaCompleto.sql";
        private const string NOMBRE_SCRIPT_ACTUALIZACION = "ActualizacionBD.sql";

        // Instalación por etapas: primero la gestión de usuarios y después, con "Actualizar", el negocio.
        //  - EsquemaBase.sql crea solo la gestión de usuarios y deja la marca "versión 0" en VersionBD_GV42.
        //  - ActualizacionNegocio.sql agrega las tablas y datos del negocio sin tocar lo que ya había.
        private const string NOMBRE_SCRIPT_BASE = "EsquemaBase.sql";
        private const string NOMBRE_SCRIPT_NEGOCIO = "ActualizacionNegocio.sql";

        // Subir este número cuando se agregue un bloque nuevo al final de ActualizacionBD.sql.
        public const int VERSION_ACTUAL = 14;

        // Lo que tiene que existir en una base de gestión de usuarios (sin negocio).
        private static readonly string[] OBJETOS_BASE =
        {
            "Usuario", "Roles", "Patente", "RolPatente", "Familia", "FamiliaPatente", "FamiliaIntegrada",
            "RolFamilia", "Modulo", "TipoEvento", "EVENTOS", "IntegridadDVH", "IntegridadDVV"
        };

        // Tablas y columnas que necesita esta versión del sistema. Si falta alguna, la base es de
        // una versión muy anterior y no se puede actualizar conservando los datos.
        private static readonly string[] OBJETOS_REQUERIDOS =
        {
            "Usuario", "Roles", "Patente", "RolPatente", "EVENTOS", "TipoEvento", "Modulo",
            "IntegridadDVH", "IntegridadDVV", "Aerolinea", "Aeropuerto", "Vuelo", "VueloClase", "Asiento",
            "Vuelo_C", "Pasajero", "Reserva", "ReservaPasajero", "ReservaAdicional", "TipoAdicional",
            "Pago", "Boleto", "CheckIn",
            "Reserva.FechaCancelacion", "Reserva.MontoPenalidadCancelacion", "Reserva.IdCanalVenta",
            "Vuelo.BorradoLogico", "Vuelo.PuertaEmbarque"
        };

        #endregion

        #region Métodos públicos

        public static bool ExisteBaseDatos(string instancia)
        {
            using (var conn = new SqlConnection(ConexionMaster(instancia, 3)))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM sys.databases WHERE name = @n", conn))
                {
                    cmd.CommandTimeout = 5;
                    cmd.Parameters.AddWithValue("@n", NOMBRE_BD);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Instalación desde cero. Si algo falla a mitad de camino se borra la base incompleta,
        // así el próximo intento vuelve a instalar en lugar de encontrar una base "a medias".
        public static void InstalarBaseDatos(string instancia)
        {
            try
            {
                EjecutarScript(instancia, NOMBRE_SCRIPT);
                EjecutarScript(instancia, NOMBRE_SCRIPT_ACTUALIZACION);
            }
            catch
            {
                try { EliminarBaseDatos(instancia); } catch { }
                throw;
            }
        }

        // Instalación inicial por etapas: crea solo la gestión de usuarios (sin el negocio). Igual que
        // InstalarBaseDatos, si falla a mitad de camino borra la base incompleta.
        public static void InstalarBaseUsuarios(string instancia)
        {
            try
            {
                EjecutarScript(instancia, NOMBRE_SCRIPT_BASE);
            }
            catch
            {
                try { EliminarBaseDatos(instancia); } catch { }
                throw;
            }
        }

        // True si la base es una instalación de gestión de usuarios a la que todavía no se le agregó el
        // negocio: tiene la marca "versión 0" y ninguna versión posterior.
        public static bool EsBaseUsuarios(string instancia)
        {
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "IF OBJECT_ID('dbo.VersionBD_GV42', 'U') IS NULL SELECT 0 " +
                    "ELSE SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.VersionBD_GV42 WHERE Version = 0) " +
                    "AND NOT EXISTS (SELECT 1 FROM dbo.VersionBD_GV42 WHERE Version > 0) THEN 1 ELSE 0 END", conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                }
            }
        }

        // Agrega el negocio a una base de gestión de usuarios SIN borrar ni modificar lo que ya tiene
        // (usuarios, roles, permisos, bitácora). Se puede repetir: si algo falló a mitad de camino, vuelve
        // a ejecutarse y completa lo que faltaba.
        //  1) ActualizacionNegocio.sql: tablas y datos del negocio (en una sola transacción: o entra todo o nada).
        //  2) ActualizacionBD.sql: versiones 1 a 14 (completa el negocio y asigna los permisos de los roles).
        public static void AgregarNegocio(string instancia)
        {
            if (EsBaseUsuarios(instancia))
                EjecutarScript(instancia, NOMBRE_SCRIPT_NEGOCIO, true);
            EjecutarScript(instancia, NOMBRE_SCRIPT_ACTUALIZACION);
        }

        // True si la base a la que está conectado el sistema ya tiene las tablas del negocio. El sistema lo
        // usa para mostrar u ocultar lo que depende del negocio (por ejemplo "Registrarse" en el login).
        public static bool NegocioInstaladoEnConexionActual()
        {
            object r = Acceso.Instancia.leerEscalar(
                "SELECT CASE WHEN OBJECT_ID('dbo.Reserva', 'U') IS NULL THEN 0 ELSE 1 END", null);
            return r != null && r != DBNull.Value && Convert.ToInt32(r) == 1;
        }

        // Versión registrada en dbo.VersionBD_GV42 (0 si la base es anterior a esa tabla).
        public static int ObtenerVersion(string instancia)
        {
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "IF OBJECT_ID('dbo.VersionBD_GV42', 'U') IS NULL SELECT 0 " +
                    "ELSE SELECT ISNULL(MAX(Version), 0) FROM dbo.VersionBD_GV42", conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Tablas o columnas requeridas que no existen en la base (lista vacía = compatible).
        public static List<string> ObjetosFaltantes(string instancia)
        {
            var faltan = new List<string>();
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();

                // Una base de gestión de usuarios (sin negocio) solo necesita las tablas de usuarios.
                bool soloUsuarios = EsBaseUsuarios(instancia);
                foreach (string objeto in soloUsuarios ? OBJETOS_BASE : OBJETOS_REQUERIDOS)
                {
                    string[] partes = objeto.Split('.');
                    string sql = partes.Length == 1
                        ? "SELECT CASE WHEN OBJECT_ID(@t, 'U') IS NULL THEN 0 ELSE 1 END"
                        : "SELECT CASE WHEN COL_LENGTH(@t, @c) IS NULL THEN 0 ELSE 1 END";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@t", "dbo." + partes[0]);
                        if (partes.Length > 1) cmd.Parameters.AddWithValue("@c", partes[1]);
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0) faltan.Add(objeto);
                    }
                }
            }
            return faltan;
        }

        // Aplica las actualizaciones pendientes sin borrar datos (el script es idempotente).
        public static void ActualizarBaseDatos(string instancia)
        {
            EjecutarScript(instancia, NOMBRE_SCRIPT_ACTUALIZACION);
        }

        // Borra la base (cierra antes las conexiones abiertas) y la instala de nuevo.
        public static void ReinstalarBaseDatos(string instancia)
        {
            EliminarBaseDatos(instancia);
            InstalarBaseDatos(instancia);
        }

        // Genera los vuelos (con clases y asientos) que falten para los próximos días.
        // Devuelve cuántos vuelos se crearon (0 si ya estaban todos).
        public static int GenerarVuelosFaltantes(string instancia, int dias)
        {
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();
                using (var existe = new SqlCommand("SELECT OBJECT_ID('dbo.usp_GenerarVuelos_GV42', 'P')", conn))
                    if (existe.ExecuteScalar() == DBNull.Value) return 0;

                using (var cmd = new SqlCommand("dbo.usp_GenerarVuelos_GV42", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 900;
                    cmd.Parameters.AddWithValue("@Dias", dias);
                    object r = cmd.ExecuteScalar();
                    return r == null || r == DBNull.Value ? 0 : Convert.ToInt32(r);
                }
            }
        }

        // Tareas que dejó ActualizacionBD.sql para que las haga el sistema (por ejemplo, recalcular el
        // dígito verificador de una tabla cuya fórmula cambió).
        public static List<string> TareasPendientes(string instancia)
        {
            var tareas = new List<string>();
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "IF OBJECT_ID('dbo.TareaPendiente_GV42', 'U') IS NOT NULL SELECT Nombre FROM dbo.TareaPendiente_GV42", conn))
                using (var rd = cmd.ExecuteReader())
                    while (rd.Read()) tareas.Add(rd.GetString(0));
            }
            return tareas;
        }

        public static void QuitarTarea(string instancia, string nombre)
        {
            using (var conn = new SqlConnection(ConexionBase(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM dbo.TareaPendiente_GV42 WHERE Nombre = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Métodos privados

        private static string ConexionMaster(string instancia, int timeout = 15) =>
            $"Data Source={instancia};Initial Catalog=master;Integrated Security=True;Connect Timeout={timeout}";

        private static string ConexionBase(string instancia) =>
            $"Data Source={instancia};Initial Catalog={NOMBRE_BD};Integrated Security=True;Connect Timeout=15";

        private static void EliminarBaseDatos(string instancia)
        {
            SqlConnection.ClearAllPools();
            using (var conn = new SqlConnection(ConexionMaster(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "IF DB_ID(@n) IS NOT NULL BEGIN " +
                    "  ALTER DATABASE [" + NOMBRE_BD + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                    "  DROP DATABASE [" + NOMBRE_BD + "]; " +
                    "END", conn))
                {
                    cmd.CommandTimeout = 120;
                    cmd.Parameters.AddWithValue("@n", NOMBRE_BD);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ejecuta un script .sql que está junto al .exe, lote por lote (separados por GO).
        // Con transaccional = true todos los lotes van en una sola transacción: si uno falla se deshace
        // todo y la base queda como estaba.
        private static void EjecutarScript(string instancia, string nombreArchivo, bool transaccional = false)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);
            if (!File.Exists(ruta))
                throw new Exception($"No se encontró el script {nombreArchivo}: {ruta}");

            string script = File.ReadAllText(ruta);
            using (var conn = new SqlConnection(ConexionMaster(instancia)))
            {
                conn.Open();
                SqlTransaction tx = transaccional ? conn.BeginTransaction() : null;
                try
                {
                    string[] lotes = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (string lote in lotes)
                    {
                        if (string.IsNullOrWhiteSpace(lote)) continue;
                        using (var cmd = new SqlCommand(lote, conn, tx))
                        {
                            // El lote que genera los vuelos (con sus asientos) puede tardar varios minutos.
                            cmd.CommandTimeout = 900;
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                throw new Exception($"Error al ejecutar {nombreArchivo}: {ex.Message}", ex);
                            }
                        }
                    }
                    if (tx != null) tx.Commit();
                }
                catch
                {
                    if (tx != null)
                    {
                        try { tx.Rollback(); } catch { }
                    }
                    throw;
                }
            }
        }

        #endregion
    }
}
