using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Servicios
{
    // Representación de un usuario para la serialización XML.
    // No se serializa Usuario_GV42 directamente porque:
    //  - tiene la contraseña (hash) y datos internos de seguridad que no deben salir del sistema;
    //  - su Rol es un Composite (lista de IComponentePermiso_GV42) y XmlSerializer no puede
    //    serializar interfaces.
    // Por eso se guardan solo los datos visibles en la matriz de la pantalla.
    [Serializable]
    [XmlType("Usuario")]
    public class UsuarioXml_GV42
    {
        #region Constructor

        public UsuarioXml_GV42() { }

        #endregion

        #region Propiedades

        [XmlElement("DNI")]
        public string DNI { get; set; }

        [XmlElement("Apellido")]
        public string Apellido { get; set; }

        [XmlElement("Nombre")]
        public string Nombre { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("UserName")]
        public string Login { get; set; }

        [XmlElement("Rol")]
        public string RolNombre { get; set; }

        [XmlElement("Bloqueado")]
        public bool Bloqueo { get; set; }

        [XmlElement("Activo")]
        public bool Activo { get; set; }

        #endregion

        #region Métodos públicos

        public static UsuarioXml_GV42 DesdeUsuario(Usuario_GV42 u)
        {
            return new UsuarioXml_GV42
            {
                DNI = u.DNI,
                Apellido = u.Apellido,
                Nombre = u.Nombre,
                Email = u.Email,
                Login = u.Login,
                RolNombre = u.RolNombre,
                Bloqueo = u.Bloqueo,
                Activo = u.Activo
            };
        }

        public Usuario_GV42 AUsuario()
        {
            return new Usuario_GV42
            {
                DNI = DNI,
                Apellido = Apellido,
                Nombre = Nombre,
                Email = Email,
                Login = Login,
                Rol = string.IsNullOrEmpty(RolNombre) ? null : new Rol_GV42 { Nombre = RolNombre },
                Bloqueo = Bloqueo,
                Activo = Activo
            };
        }

        #endregion
    }

    // Raíz del archivo XML: la lista de usuarios más algunos datos de control
    // (quién y cuándo lo generó).
    [Serializable]
    [XmlRoot("MaestroUsuarios")]
    public class ListaUsuariosXml_GV42
    {
        #region Propiedades

        [XmlAttribute("FechaGeneracion")]
        public DateTime FechaGeneracion { get; set; }

        [XmlAttribute("GeneradoPor")]
        public string GeneradoPor { get; set; }

        [XmlAttribute("Cantidad")]
        public int Cantidad { get; set; }

        [XmlElement("Usuario")]
        public List<UsuarioXml_GV42> Usuarios { get; set; } = new List<UsuarioXml_GV42>();

        #endregion
    }
}
