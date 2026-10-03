using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BE
{
    // Representación de un cliente para la serialización XML: los datos que se ven en la matriz
    // de la pantalla "Maestro de clientes".
    [Serializable]
    [XmlType("Cliente")]
    public class ClienteXml_GV42
    {
        #region Constructor

        public ClienteXml_GV42() { }

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

        [XmlElement("Telefono")]
        public string Telefono { get; set; }

        #endregion

        #region Métodos públicos

        public static ClienteXml_GV42 DesdeCliente(Cliente_GV42 c)
        {
            return new ClienteXml_GV42
            {
                DNI = c.DNI,
                Apellido = c.Apellido,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono
            };
        }

        public Cliente_GV42 ACliente()
        {
            return new Cliente_GV42(DNI, Nombre, Apellido, Email, Telefono);
        }

        #endregion
    }

    // Raíz del archivo XML: la lista de clientes más algunos datos de control
    // (quién y cuándo lo generó).
    [Serializable]
    [XmlRoot("MaestroClientes")]
    public class ListaClientesXml_GV42
    {
        #region Propiedades

        [XmlAttribute("FechaGeneracion")]
        public DateTime FechaGeneracion { get; set; }

        [XmlAttribute("GeneradoPor")]
        public string GeneradoPor { get; set; }

        [XmlAttribute("Cantidad")]
        public int Cantidad { get; set; }

        [XmlElement("Cliente")]
        public List<ClienteXml_GV42> Clientes { get; set; } = new List<ClienteXml_GV42>();

        #endregion
    }
}
