using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DALIntegridad_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        public static readonly string[] TABLAS_PROTEGIDAS = {
            "Usuario", "Roles", "Familia", "Patente",
            "FamiliaPatente", "FamiliaIntegrada",
            "RolPatente", "RolFamilia",
            "Modulo", "TipoEvento",
            // Negocio: se protegen los importes de reservas y pagos contra alteración directa en la BD.
            "Reserva", "Pago"
        };

        #endregion

        #region Constructor

        public DALIntegridad_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Propiedades

        public static bool IntegridadConocidamenteRota { get; set; }

        #endregion

        #region Métodos públicos

        public Dictionary<string, string> CalcularDVHsTabla(string nombreTabla)
        {
            switch (nombreTabla)
            {
                case "Usuario": return DVHsUsuario();
                case "Roles": return DVHsRoles();
                case "Familia": return DVHsFamilia();
                case "Patente": return DVHsPatente();
                case "FamiliaPatente": return DVHsFamiliaPatente();
                case "FamiliaIntegrada": return DVHsFamiliaIntegrada();
                case "RolPatente": return DVHsRolPatente();
                case "RolFamilia": return DVHsRolFamilia();
                case "Modulo": return DVHsModulo();
                case "TipoEvento": return DVHsTipoEvento();
                case "Reserva": return DVHsReserva();
                case "Pago": return DVHsPago();
                default: throw new Exception("Tabla protegida desconocida: " + nombreTabla);
            }
        }

        public Dictionary<string, string> ObtenerDVHsAlmacenados(string nombreTabla)
        {
            string q = "SELECT IdRegistro, DVH FROM IntegridadDVH WHERE NombreTabla = @T";
            DataTable dt = _acceso.leer(q, new[] { new SqlParameter("@T", nombreTabla) });
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
                dict[r["IdRegistro"].ToString()] = r["DVH"].ToString();
            return dict;
        }

        public string ObtenerDVVAlmacenado(string nombreTabla)
        {
            string q = "SELECT DVV FROM IntegridadDVV WHERE NombreTabla = @T";
            object res = _acceso.leerEscalar(q, new[] { new SqlParameter("@T", nombreTabla) });
            return (res == null || res == DBNull.Value) ? null : res.ToString();
        }

        public void RecalcularTabla(string nombreTabla)
        {
            Dictionary<string, string> dvhs = CalcularDVHsTabla(nombreTabla);
            GuardarDVHs(nombreTabla, dvhs);
            string dvv = CalculadorIntegridad_GV42.CalcularDVV(dvhs.Values);
            GuardarDVV(nombreTabla, dvv);
        }

        public void GuardarDVHs(string nombreTabla, Dictionary<string, string> dvhs)
        {
            _acceso.escribir(
                "DELETE FROM IntegridadDVH WHERE NombreTabla = @T",
                new[] { new SqlParameter("@T", nombreTabla) });
            foreach (var kv in dvhs)
            {
                _acceso.escribir(
                    "INSERT INTO IntegridadDVH (NombreTabla, IdRegistro, DVH) VALUES (@T, @I, @D)",
                    new[] {
                        new SqlParameter("@T", nombreTabla),
                        new SqlParameter("@I", kv.Key),
                        new SqlParameter("@D", kv.Value)
                    });
            }
        }

        public void GuardarDVV(string nombreTabla, string dvv)
        {
            string existeQ = "SELECT COUNT(1) FROM IntegridadDVV WHERE NombreTabla = @T";
            object res = _acceso.leerEscalar(existeQ, new[] { new SqlParameter("@T", nombreTabla) });
            int existe = Convert.ToInt32(res);
            if (existe > 0)
            {
                _acceso.escribir(
                    "UPDATE IntegridadDVV SET DVV = @D, FechaCalculo = GETDATE() WHERE NombreTabla = @T",
                    new[] { new SqlParameter("@D", dvv), new SqlParameter("@T", nombreTabla) });
            }
            else
            {
                _acceso.escribir(
                    "INSERT INTO IntegridadDVV (NombreTabla, DVV) VALUES (@T, @D)",
                    new[] { new SqlParameter("@T", nombreTabla), new SqlParameter("@D", dvv) });
            }
        }

        public bool ExisteAlgunDVV()
        {
            object res = _acceso.leerEscalar("SELECT COUNT(1) FROM IntegridadDVV", null);
            return Convert.ToInt32(res) > 0;
        }

        public void HacerBackupSQL(string connStringMaster, string nombreBd, string rutaArchivoBak)
        {
            using (var conn = new SqlConnection(connStringMaster))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        $"BACKUP DATABASE [{nombreBd}] TO DISK = @ruta WITH INIT, FORMAT, NAME = N'Backup automatico';";
                    cmd.Parameters.AddWithValue("@ruta", rutaArchivoBak);
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RestaurarBackupSQL(string connStringMaster, string nombreBd, string rutaArchivoBak)
        {
            SqlConnection.ClearAllPools();
            using (var conn = new SqlConnection(connStringMaster))
            {
                conn.Open();
                string sql =
                    $"ALTER DATABASE [{nombreBd}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                    $"RESTORE DATABASE [{nombreBd}] FROM DISK = @ruta WITH REPLACE; " +
                    $"ALTER DATABASE [{nombreBd}] SET MULTI_USER;";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ruta", rutaArchivoBak);
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Métodos privados

        private Dictionary<string, string> DVHsModulo()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM Modulo", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["Id"], r["Nombre"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsTipoEvento()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM TipoEvento", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["Id"], r["Nombre"]);
            }
            return dict;
        }

        // Cubre los importes y el estado de la reserva: si alguno se altera directamente en la
        // base (sin pasar por la capa de negocio), la verificación de integridad lo detecta.
        private Dictionary<string, string> DVHsReserva()
        {
            // Versión 2: también cubre fechas, canal, vendedor y la penalidad de cancelación (antes se
            // podían alterar directamente en la base sin que la verificación lo detectara).
            string q = "SELECT Id, DniCliente, IdVuelo, IdClase, CantidadPasajeros, " +
                       "       ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal, IdEstadoReserva, " +
                       "       FechaRealizacion, LoginVendedor, IdCanalVenta, FechaRegreso, FechaCancelacion, MontoPenalidadCancelacion " +
                       "FROM Reserva";
            DataTable dt = _acceso.leer(q, null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(
                    r["Id"], r["DniCliente"], r["IdVuelo"], r["IdClase"], r["CantidadPasajeros"],
                    r["ImporteBase"], r["SubtotalAdicionales"], r["Impuestos"], r["ImporteTotal"], r["IdEstadoReserva"],
                    r["FechaRealizacion"], r["LoginVendedor"], r["IdCanalVenta"], r["FechaRegreso"],
                    r["FechaCancelacion"], r["MontoPenalidadCancelacion"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsPago()
        {
            string q = "SELECT Id, IdReserva, ImporteTotalAbonado, IdMedioPago, NumeroTransaccion FROM Pago";
            DataTable dt = _acceso.leer(q, null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(
                    r["Id"], r["IdReserva"], r["ImporteTotalAbonado"], r["IdMedioPago"], r["NumeroTransaccion"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsUsuario()
        {
            // Versión 2: incluye la contraseña (hash). Antes se podía reemplazar el hash de cualquier
            // usuario en la base por el de una clave conocida y la verificación no lo detectaba.
            string q = "SELECT DNI, Apellido, Nombre, UserName, Contrasena, Email, " +
                       "       IdRol, Activo, Bloqueo, IntentosFallidos, DebeCambiarContrasena " +
                       "FROM Usuario";
            DataTable dt = _acceso.leer(q, null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["DNI"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(
                    r["DNI"], r["Apellido"], r["Nombre"], r["UserName"], r["Contrasena"], r["Email"],
                    r["IdRol"], r["Activo"], r["Bloqueo"], r["IntentosFallidos"], r["DebeCambiarContrasena"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsRoles()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM Roles", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["Id"], r["Nombre"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsFamilia()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre FROM Familia", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["Id"], r["Nombre"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsPatente()
        {
            DataTable dt = _acceso.leer("SELECT Id, Nombre, DataKey FROM Patente", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = Convert.ToString(r["Id"]);
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["Id"], r["Nombre"], r["DataKey"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsFamiliaPatente()
        {
            DataTable dt = _acceso.leer("SELECT IdFamilia, IdPatente FROM FamiliaPatente", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = $"{r["IdFamilia"]}_{r["IdPatente"]}";
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["IdFamilia"], r["IdPatente"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsFamiliaIntegrada()
        {
            DataTable dt = _acceso.leer("SELECT IdFamiliaPadre, IdFamiliaHija FROM FamiliaIntegrada", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = $"{r["IdFamiliaPadre"]}_{r["IdFamiliaHija"]}";
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["IdFamiliaPadre"], r["IdFamiliaHija"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsRolPatente()
        {
            DataTable dt = _acceso.leer("SELECT IdRol, IdPatente FROM RolPatente", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = $"{r["IdRol"]}_{r["IdPatente"]}";
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["IdRol"], r["IdPatente"]);
            }
            return dict;
        }

        private Dictionary<string, string> DVHsRolFamilia()
        {
            DataTable dt = _acceso.leer("SELECT IdRol, IdFamilia FROM RolFamilia", null);
            var dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                string id = $"{r["IdRol"]}_{r["IdFamilia"]}";
                dict[id] = CalculadorIntegridad_GV42.CalcularDVH(r["IdRol"], r["IdFamilia"]);
            }
            return dict;
        }

        #endregion
    }
}
