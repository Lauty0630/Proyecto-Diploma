using System;

namespace BE
{
    public class Persona_GV42
    {
        #region Constructor

        public Persona_GV42() { }

        public Persona_GV42(string dni, string nombre, string apellido, string email, string telefono)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
        }

        #endregion

        #region Propiedades

        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public string NombreCompleto
        {
            get { return (Nombre + " " + Apellido).Trim(); }
        }

        #endregion
    }
}
