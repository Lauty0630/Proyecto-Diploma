using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Usuario_GV42
    {
        #region Campos

        private string _DNI;

        private string _Apellido;

        private string _Nombre;

        private string _Login;

        private string _Contrasena;

        private Rol_GV42 _Rol;

        private string _Email;

        private bool _Bloqueo;

        private bool _Activo;

        #endregion

        #region Constructor

        public Usuario_GV42(string dni, string apellidos, string nombre, string login, string password, Rol_GV42 rol, string email)
        {
            DNI = dni;
            Apellido = apellidos;
            Nombre = nombre;
            Login = login;
            Contrasena = password;
            Rol = rol;
            Email = email;
            Bloqueo = false;
        }

        public Usuario_GV42()
        {

        }

        #endregion

        #region Propiedades

        public string DNI
        {
            get { return _DNI; }
            set { _DNI = value; }
        }

        public string Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }

        public string Contrasena
        {
            get { return _Contrasena; }
            set { _Contrasena = value; }
        }

        public Rol_GV42 Rol
        {
            get { return _Rol; }
            set { _Rol = value; }
        }

        public string RolNombre
        {
            get { return _Rol != null ? _Rol.Nombre : string.Empty; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public bool Bloqueo
        {
            get { return _Bloqueo; }
            set { _Bloqueo = value; }
        }

        public bool Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public int IntentosFallidos { get; set; }
        public DateTime? UltimoIntentoFallido { get; set; }

        public bool DebeCambiarContrasena { get; set; }

        public string Idioma { get; set; } = "es";

        #endregion
    }
}
