using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Maestro de clientes. Los clientes se guardan en la tabla Pasajero, que es la tabla de
    // personas del negocio (quien contrata la reserva y quienes viajan).
    public class DALCliente_GV42
    {
        #region Campos

        private readonly Acceso _acceso;

        #endregion

        #region Constructor

        public DALCliente_GV42()
        {
            _acceso = Acceso.Instancia;
        }

        #endregion

        #region Métodos públicos

        public List<Cliente_GV42> Listar()
        {
            DataTable dt = _acceso.leer(
                "SELECT DNI, Nombre, Apellido, Email, Telefono FROM Pasajero ORDER BY Apellido, Nombre, DNI", null);
            var lista = new List<Cliente_GV42>();
            foreach (DataRow r in dt.Rows)
            {
                var c = new Cliente_GV42();
                DALUtil_GV42.LlenarPersona(c, r, "");
                lista.Add(c);
            }
            return lista;
        }

        public bool ExisteDni(string dni)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Pasajero WHERE DNI = @DNI",
                                           new[] { new SqlParameter("@DNI", dni) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        public Cliente_GV42 BuscarPorDni(string dni)
        {
            DataTable dt = _acceso.leer("SELECT DNI, Nombre, Apellido, Email, Telefono FROM Pasajero WHERE DNI = @DNI",
                                        new[] { new SqlParameter("@DNI", dni) });
            if (dt.Rows.Count == 0) return null;

            var c = new Cliente_GV42();
            DALUtil_GV42.LlenarPersona(c, dt.Rows[0], "");
            return c;
        }

        // Reservas en las que figura como titular o como pasajero (cualquier estado).
        public int ContarReservas(string dni)
        {
            object r = _acceso.leerEscalar(
                "SELECT (SELECT COUNT(1) FROM Reserva WHERE DniCliente = @DNI) + " +
                "       (SELECT COUNT(1) FROM ReservaPasajero WHERE DniPasajero = @DNI)",
                new[] { new SqlParameter("@DNI", dni) });
            return r == null ? 0 : Convert.ToInt32(r);
        }

        // True si esa persona tiene una cuenta para entrar al sistema.
        public bool TieneUsuario(string dni)
        {
            object r = _acceso.leerEscalar("SELECT COUNT(1) FROM Usuario WHERE DNI = @DNI",
                                           new[] { new SqlParameter("@DNI", dni) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        public void Insertar(Cliente_GV42 c)
        {
            string query =
                "INSERT INTO Pasajero (DNI, Nombre, Apellido, Email, Telefono) " +
                "VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono)";
            _acceso.escribir(query, Parametros(c));
        }

        // El DNI es la clave del cliente: no se modifica.
        public void Modificar(Cliente_GV42 c)
        {
            string query =
                "UPDATE Pasajero SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono " +
                "WHERE DNI = @DNI";
            _acceso.escribir(query, Parametros(c));
        }

        public void Eliminar(string dni)
        {
            _acceso.escribir("DELETE FROM Pasajero WHERE DNI = @DNI", new[] { new SqlParameter("@DNI", dni) });
        }

        #endregion

        #region Métodos privados

        private static SqlParameter[] Parametros(Cliente_GV42 c)
        {
            return new[] {
                new SqlParameter("@DNI",      c.DNI),
                new SqlParameter("@Nombre",   c.Nombre),
                new SqlParameter("@Apellido", c.Apellido),
                new SqlParameter("@Email",    DALUtil_GV42.Cifrar(c.Email)),
                new SqlParameter("@Telefono", c.Telefono)
            };
        }

        #endregion
    }
}
