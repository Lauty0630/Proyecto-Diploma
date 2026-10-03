using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    // Maestro de clientes: ABM y serialización XML (serializar / des-serializar).
    public class BLLCliente_GV42
    {
        #region Campos

        public const string PATENTE_VER = "Clientes.Ver";
        public const string PATENTE_GESTIONAR = "Clientes.Gestionar";

        private readonly DALCliente_GV42 _dal = new DALCliente_GV42();

        #endregion

        #region Permisos

        public bool PuedeGestionar()
        {
            return BLLNegocioUtil_GV42.TienePatente(PATENTE_GESTIONAR);
        }

        #endregion

        #region Consultas

        public List<Cliente_GV42> Listar()
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.cliente.sinPermisoVer"));
            return _dal.Listar();
        }

        #endregion

        #region ABM

        public void Crear(Cliente_GV42 cliente)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.cliente.sinPermisoGestionar"));
            BLLNegocioUtil_GV42.ValidarPersona(cliente, IdiomaManager_GV42.T("neg.rol.cliente"));

            if (_dal.ExisteDni(cliente.DNI))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.dniDuplicado", cliente.DNI));

            _dal.Insertar(cliente);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Cliente registrado",
                "DNI " + cliente.DNI + " - " + cliente.NombreCompleto, "Baja");
        }

        public void Modificar(Cliente_GV42 cliente)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.cliente.sinPermisoGestionar"));
            BLLNegocioUtil_GV42.ValidarPersona(cliente, IdiomaManager_GV42.T("neg.rol.cliente"));

            Cliente_GV42 actual = _dal.BuscarPorDni(cliente.DNI);
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.noExiste", cliente.DNI));

            // Si la persona tiene cuenta para entrar al sistema, su nombre, apellido y email también
            // están en esa cuenta: se cambian desde Gestión de usuarios para que no queden distintos.
            bool cambiaIdentidad = !string.Equals(actual.Nombre, cliente.Nombre, StringComparison.Ordinal)
                                || !string.Equals(actual.Apellido, cliente.Apellido, StringComparison.Ordinal)
                                || !string.Equals(actual.Email, cliente.Email, StringComparison.OrdinalIgnoreCase);
            if (cambiaIdentidad && _dal.TieneUsuario(cliente.DNI))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.tieneUsuarioModificar"));

            _dal.Modificar(cliente);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Cliente modificado",
                "DNI " + cliente.DNI + " - " + cliente.NombreCompleto, "Media");
        }

        // Solo se puede eliminar un cliente sin reservas ni cuenta de usuario: los demás son parte
        // del historial del negocio.
        public void Eliminar(string dni)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_GESTIONAR, IdiomaManager_GV42.T("neg.cliente.sinPermisoGestionar"));

            Cliente_GV42 actual = _dal.BuscarPorDni((dni ?? string.Empty).Trim());
            if (actual == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.noExiste", dni));
            if (_dal.ContarReservas(actual.DNI) > 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.tieneReservas", actual.NombreCompleto));
            if (_dal.TieneUsuario(actual.DNI))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.cliente.tieneUsuarioEliminar", actual.NombreCompleto));

            _dal.Eliminar(actual.DNI);
            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Cliente eliminado",
                "DNI " + actual.DNI + " - " + actual.NombreCompleto, "Alta");
        }

        #endregion

        #region Serialización XML

        // Se serializa lo que el operador ve en la matriz de la pantalla (no se vuelve a leer la
        // base). No hay tabla para los archivos XML: quedan en la carpeta que elija el operador.
        public int Serializar(List<Cliente_GV42> clientes, string rutaArchivo)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.cliente.sinPermisoVer"));
            if (clientes == null || clientes.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.sinDatos"));
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.sinUbicacion"));
            if (!string.Equals(System.IO.Path.GetExtension(rutaArchivo), ".xml", StringComparison.OrdinalIgnoreCase))
                rutaArchivo += ".xml";

            DateTime ahora = DateTime.Now;
            var lista = new ListaClientesXml_GV42
            {
                // Sin milisegundos ni zona horaria: el atributo queda legible (2026-09-30T00:05:12).
                FechaGeneracion = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second),
                GeneradoPor = BLLNegocioUtil_GV42.LoginActual(),
                Cantidad = clientes.Count,
                Clientes = clientes.Select(ClienteXml_GV42.DesdeCliente).ToList()
            };

            try
            {
                // 1) Datos: XML generado por XmlSerializer, con la referencia estándar a la hoja CSS.
                SerializadorXml_GV42.Serializar(lista, rutaArchivo, EstiloXmlClientes_GV42.NOMBRE_ARCHIVO);
            }
            catch (UnauthorizedAccessException)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.sinPermisoEscritura"));
            }
            catch (System.IO.IOException ex)
            {
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.errorSerializar") + " " + ex.Message);
            }

            // 2) Presentación: la hoja CSS se guarda aparte, en la misma carpeta que el XML. Si no se
            //    puede escribir, el XML sigue siendo válido y des-serializable (solo se ve sin estilo).
            try
            {
                string carpeta = System.IO.Path.GetDirectoryName(rutaArchivo) ?? string.Empty;
                System.IO.File.WriteAllText(System.IO.Path.Combine(carpeta, EstiloXmlClientes_GV42.NOMBRE_ARCHIVO),
                                            EstiloXmlClientes_GV42.CSS, new System.Text.UTF8Encoding(false));
            }
            catch { }

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Clientes serializados a XML",
                clientes.Count + " cliente(s) -> " + System.IO.Path.GetFileName(rutaArchivo), "Baja");

            return clientes.Count;
        }

        // Devuelve los clientes del archivo para mostrarlos en la matriz. No se guardan en la base.
        public List<Cliente_GV42> Deserializar(string rutaArchivo)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_VER, IdiomaManager_GV42.T("neg.cliente.sinPermisoVer"));
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.sinArchivo"));
            if (!System.IO.File.Exists(rutaArchivo))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.archivoNoExiste"));

            ListaClientesXml_GV42 lista;
            try
            {
                lista = SerializadorXml_GV42.Deserializar<ListaClientesXml_GV42>(rutaArchivo);
            }
            catch (Exception)
            {
                // XML mal formado, raíz distinta a <MaestroClientes>, tipos inválidos, etc.
                throw new NegocioException_GV42(IdiomaManager_GV42.T("serializacion.formatoInvalido"));
            }

            List<Cliente_GV42> clientes = (lista != null && lista.Clientes != null ? lista.Clientes : new List<ClienteXml_GV42>())
                .Select(x => x.ACliente())
                .ToList();

            BLLNegocioUtil_GV42.Auditar(BLLNegocioUtil_GV42.MODULO_RESERVAS, "Clientes deserializados desde XML",
                clientes.Count + " cliente(s) <- " + System.IO.Path.GetFileName(rutaArchivo), "Baja");

            return clientes;
        }

        #endregion
    }
}
