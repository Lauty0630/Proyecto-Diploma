using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
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

        public bool ExisteDni(string dni)
        {
            string query = "SELECT COUNT(1) FROM Cliente WHERE DNI = @DNI";
            object r = _acceso.leerEscalar(query, new[] { new SqlParameter("@DNI", dni) });
            return r != null && Convert.ToInt32(r) > 0;
        }

        public Cliente_GV42 BuscarPorDni(string dni)
        {
            string query = "SELECT DNI, Nombre, Apellido, Email, Telefono FROM Cliente WHERE DNI = @DNI";
            DataTable dt = _acceso.leer(query, new[] { new SqlParameter("@DNI", dni) });
            if (dt.Rows.Count == 0) return null;

            var c = new Cliente_GV42();
            DALUtil_GV42.LlenarPersona(c, dt.Rows[0], "");
            return c;
        }

        // Solo para compensar el alta autogestionada si falla la creación del usuario asociado
        // (ver BLLReserva_GV42.RegistrarClienteAutogestionado). No se usa en el alta normal.
        public void Eliminar(string dni)
        {
            _acceso.escribir("DELETE FROM Cliente WHERE DNI = @DNI", new[] { new SqlParameter("@DNI", dni) });
        }

        public void Insertar(Cliente_GV42 c)
        {
            string query =
                "INSERT INTO Cliente (DNI, Nombre, Apellido, Email, Telefono) " +
                "VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono)";

            SqlParameter[] p = {
                new SqlParameter("@DNI",      c.DNI),
                new SqlParameter("@Nombre",   c.Nombre),
                new SqlParameter("@Apellido", c.Apellido),
                new SqlParameter("@Email",    DALUtil_GV42.Cifrar(c.Email)),
                new SqlParameter("@Telefono", c.Telefono)
            };
            _acceso.escribir(query, p);
        }

        #endregion
    }
}
