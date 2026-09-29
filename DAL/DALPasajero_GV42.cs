using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Acceso a la tabla Pasajero: personas que reservan (cliente de la reserva) y/o viajan.
    public class DALPasajero_GV42
    {
        private readonly Acceso _acceso;

        public DALPasajero_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        public bool ExisteDni(string dni)
        {
            string query = "SELECT COUNT(1) FROM Pasajero WHERE DNI = @DNI";
            object r = _acceso.leerEscalar(query, new[] { new SqlParameter("@DNI", dni) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        public Pasajero_GV42 BuscarPorDni(string dni)
        {
            string query = "SELECT DNI, Nombre, Apellido, Email, Telefono FROM Pasajero WHERE DNI = @DNI";
            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@DNI", dni) });
            if (dt.Rows.Count == 0) return null;

            var p = new Pasajero_GV42();
            DALUtil_GV42.LlenarPersona(p, dt.Rows[0], "");
            return p;
        }

        // Datos de una cuenta de Usuario para precargar el alta de un pasajero. Usuario no guarda
        // teléfono, así que ese campo vuelve vacío. Devuelve null si el DNI no tiene cuenta.
        public Pasajero_GV42 BuscarDatosEnUsuario(string dni)
        {
            string query = "SELECT DNI, Nombre, Apellido, Email FROM Usuario WHERE DNI = @DNI";
            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@DNI", dni) });
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new Pasajero_GV42
            {
                DNI = DALUtil_GV42.Str(r, "DNI"),
                Nombre = DALUtil_GV42.Str(r, "Nombre"),
                Apellido = DALUtil_GV42.Str(r, "Apellido"),
                Email = DALUtil_GV42.Descifrar(DALUtil_GV42.Str(r, "Email")),
                Telefono = string.Empty
            };
        }

        // Solo para compensar el alta autogestionada si falla la creación del usuario asociado
        // (ver BLLReserva_GV42.RegistrarClienteAutogestionado). No se usa en el alta normal.
        public void Eliminar(string dni)
        {
            _acceso.escribir("DELETE FROM Pasajero WHERE DNI = @DNI", new[] { new SqlParameter("@DNI", dni) });
        }

        public void Insertar(Pasajero_GV42 p)
        {
            string query =
                "INSERT INTO Pasajero (DNI, Nombre, Apellido, Email, Telefono) " +
                "VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono)";

            SqlParameter[] prm = {
                new SqlParameter("@DNI",      p.DNI),
                new SqlParameter("@Nombre",   p.Nombre),
                new SqlParameter("@Apellido", p.Apellido),
                new SqlParameter("@Email",    DALUtil_GV42.Cifrar(p.Email)),
                new SqlParameter("@Telefono", p.Telefono)
            };
            _acceso.escribir(query, prm);
        }
    }
}
