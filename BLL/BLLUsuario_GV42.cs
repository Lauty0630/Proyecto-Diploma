using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class BLLUsuario_GV42
    {
        private readonly DALUsuario_GV42 _DALUsuario;
        private readonly BLLIntegridad_GV42 _bllIntegridad;
        public const int MAX_INTENTOS = 3;
        private static readonly TimeSpan VENTANA_INTENTOS = TimeSpan.FromHours(1);

        public BLLUsuario_GV42()
        {
            _DALUsuario = new DALUsuario_GV42();
            _bllIntegridad = new BLLIntegridad_GV42();
        }

        private void RecalcularUsuario()
        {
            try { _bllIntegridad.RecalcularTabla("Usuario"); } catch { }
        }

        public enum ResultadoLogin
        {
            Exitoso,
            SesionActiva,
            UsuarioInexistente,
            UsuarioBloqueado,
            UsuarioInactivo,
            ContrasenaIncorrecta,
            BloqueadoPorIntentos,
            Error
        }

        private void Auditar(string login, string modulo, string tipoEvento, string detalle, string criticidad)
        {

            BLLBitacora_GV42.Instancia.RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad);

        }

        public ResultadoLogin IntentarLogin(string login, string contrasena)
        {
            try
            {
                if (SessionManager_GV42.Instancia.HaySesionActiva())
                {
                    Auditar(login, "Usuario", "Intento de login con sesión ya activa", null, "Media");
                    return ResultadoLogin.SesionActiva;
                }

                Usuario_GV42 usuario = _DALUsuario.BuscarPorLogin(login);

                if (usuario == null)
                {
                    // Antes, si la bitácora se grababa bien, el flujo seguía de largo con usuario == null
                    // y reventaba en usuario.Bloqueo: el login mostraba "Error" en vez de "Usuario inexistente".
                    try
                    {
                        Auditar(login, "Usuario", "Usuario inexistente", "el usuario no existe", "Alta");
                    }
                    catch { }
                    return ResultadoLogin.UsuarioInexistente;
                }

                if (usuario.Bloqueo)
                {
                    Auditar(login, "Usuario", "Usuario bloqueado", "Usuario bloqueado correctamente", "Alta");
                    return ResultadoLogin.UsuarioBloqueado;
                }

                if (!usuario.Activo)
                {
                    Auditar(login, "Usuario", "Usuario inactivo", "el usuario esta inactivo", "Alta");
                    return ResultadoLogin.UsuarioInactivo;
                }

                string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasena);
                if (usuario.Contrasena != contrasenaCifrada)
                {

                    int nuevosIntentos = CalcularNuevosIntentos(usuario);
                    DateTime ahora = DateTime.Now;

                    if (nuevosIntentos >= MAX_INTENTOS)
                    {

                        _DALUsuario.ActualizarIntentosFallidos(login, nuevosIntentos, ahora);
                        _DALUsuario.Bloquear(login);
                        Auditar(login, "Usuario", "Usuario bloqueado por intentos fallidos", $"{MAX_INTENTOS} intentos fallidos consecutivos dentro de {VENTANA_INTENTOS.TotalMinutes:0} min", "Alta");
                        return ResultadoLogin.BloqueadoPorIntentos;
                    }
                    else
                    {
                        _DALUsuario.ActualizarIntentosFallidos(login, nuevosIntentos, ahora);
                        Auditar(login, "Usuario", "Contraseña incorrecta", $"Intento {nuevosIntentos}/{MAX_INTENTOS}", "Media");
                        return ResultadoLogin.ContrasenaIncorrecta;
                    }
                }

                _DALUsuario.ResetearIntentosFallidos(login);
                BLLPermisos_GV42 bllPermisos = new BLLPermisos_GV42();
                if (usuario.Rol != null)
                {
                    usuario.Rol = bllPermisos.ObtenerArbolRol(usuario.Rol.Id);
                }
                bool sesionIniciada = SessionManager_GV42.Instancia.IniciarSesion(usuario);
                if (!sesionIniciada)
                {
                    Auditar(login, "Usuario", "Intento de login con sesión ya activa", "Intento de login con sesión ya activa", "Alta");
                    return ResultadoLogin.SesionActiva;
                }
                Auditar(login, "Usuario", "Login exitoso", "Login correcto", "Baja");

                if (!string.IsNullOrWhiteSpace(usuario.Idioma))
                    IdiomaManager_GV42.Instancia.CambiarIdioma(usuario.Idioma);

                return ResultadoLogin.Exitoso;

            }
            catch
            {
                return ResultadoLogin.Error;
            }

        }

        private int CalcularNuevosIntentos(Usuario_GV42 usuario)
        {
            try
            {
                if (usuario.UltimoIntentoFallido == null)
                    return 1;

                DateTime ultimo = usuario.UltimoIntentoFallido.Value;
                bool dentroDeLaVentana = (DateTime.Now - ultimo) <= VENTANA_INTENTOS;

                if (dentroDeLaVentana)
                    return usuario.IntentosFallidos + 1;
                else
                    return 1;
            }
            catch { throw new Exception(IdiomaManager_GV42.T("err.errorGenerico")); }

        }

        public List<Usuario_GV42> ListarActivos() => _DALUsuario.ListarActivos();
        public List<Usuario_GV42> ListarTodos() => _DALUsuario.ListarTodos();

        public List<Rol_GV42> ListarRoles() => _DALUsuario.ListarRoles();

        public Usuario_GV42 BuscarPorLogin(string login) => _DALUsuario.BuscarPorLogin(login);

        public bool ExisteDNI(string dni) => _DALUsuario.ExisteDNI(dni);

        public void CambiarIdioma(string codigoIdioma)
        {

            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            string idiomaAnterior = actual != null
                ? (actual.Idioma ?? IdiomaManager_GV42.Instancia.IdiomaActual)
                : IdiomaManager_GV42.Instancia.IdiomaActual;

            IdiomaManager_GV42.Instancia.CambiarIdioma(codigoIdioma);

            if (actual != null)
            {
                _DALUsuario.GuardarIdioma(actual.Login, codigoIdioma);
                actual.Idioma = codigoIdioma;

                if (!string.Equals(idiomaAnterior, codigoIdioma, StringComparison.OrdinalIgnoreCase))
                {
                    Auditar(actual.Login, "Usuario", "Idioma cambiado",
                            $"{idiomaAnterior} -> {codigoIdioma}", "Baja");
                }
            }
        }

        public void Desbloquear(string dni, string login)
        {
            Usuario_GV42 usuario = _DALUsuario.BuscarPorLogin(login);

            string contrasenaPlana = CredencialInicial(usuario.Nombre, dni);
            string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);
            _DALUsuario.Desbloquear(dni, contrasenaCifrada);
            Auditar(SessionManager_GV42.Instancia.ObtenerUsuarioActual().Login, "Admin", "Usuario desbloqueado", $"Usuario {login} desbloqueado y contraseña reseteada", "Media");
            RecalcularUsuario();
        }

        public void ActivarDesactivar(string dni, bool activo)
        {
            _DALUsuario.ActivarDesactivar(dni, activo);
            string accion = activo ? "Usuario activado" : "Usuario desactivado";
            Auditar(SessionManager_GV42.Instancia.ObtenerUsuarioActual().Login, "Admin", accion, $"DNI: {dni}", "Media");
            RecalcularUsuario();
        }

        public void ModificarEmail(string dni, string email)
        {
            _DALUsuario.ModificarEmail(dni, email);
            Auditar(SessionManager_GV42.Instancia.ObtenerUsuarioActual().Login, "Admin", "Email modificado", $"DNI: {dni}", "Media");
            RecalcularUsuario();
        }

        public void ModificarRol(string dni, Rol_GV42 rol)
        {
            if (rol == null) throw new Exception(IdiomaManager_GV42.T("err.rolValido"));
            _DALUsuario.ModificarRol(dni, rol.Id);
            Auditar(SessionManager_GV42.Instancia.ObtenerUsuarioActual().Login, "Admin","Rol modificado", $"DNI {dni} -> rol {rol.Nombre}", "Media");
            RecalcularUsuario();
        }
        public void CrearUsuario(string dni, string apellido, string nombre, string email, Rol_GV42 rol)
        {
            if (rol == null)
                throw new Exception(IdiomaManager_GV42.T("err.rolSeleccionar"));

            if (_DALUsuario.ExisteDNI(dni))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.dniDuplicado"), dni));

            nombre = Validaciones_GV42.NormalizarEspacios(nombre);
            apellido = Validaciones_GV42.NormalizarEspacios(apellido);
            VerificarMismaPersonaQuePasajero(dni, nombre, apellido);

            string contrasenaPlana = CredencialInicial(nombre, dni);
            string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);
            string login = contrasenaPlana;

            if (_DALUsuario.BuscarPorLogin(login) != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.usuarioLoginDuplicado"), login));

            Usuario_GV42 u = new Usuario_GV42
            {
                DNI = dni,
                Apellido = apellido,
                Nombre = nombre,
                Login = login,
                Contrasena = contrasenaCifrada,
                Rol = rol,
                Email = email
            };

            int filas = _DALUsuario.AgregarUsuario(u);
            if (filas == 0)
                throw new Exception(IdiomaManager_GV42.T("err.insertFallido"));
            Auditar(SessionManager_GV42.Instancia.ObtenerUsuarioActual().Login, "Admin","Usuario creado", $"Login: {login}", "Baja");
            RecalcularUsuario();
        }

        // Alta de la cuenta de un cliente autogestionado (RFN 1: el cliente se registra y reserva
        // por sí mismo, sin empleado). El rol "Cliente" debe existir en la tabla Roles (lo crea el
        // script de negocio). Devuelve el usuario creado, ya con su Rol completo.
        public Usuario_GV42 CrearUsuarioAutogestionado(string dni, string nombre, string apellido, string email,
                                                       string login, string contrasenaPlana)
        {
            Rol_GV42 rolCliente = _DALUsuario.BuscarPorNombre("Cliente");
            if (rolCliente == null)
                throw new Exception("No existe el rol 'Cliente'. Ejecute el script de negocio antes de habilitar el autoregistro.");

            if (!Validaciones_GV42.EsLoginValido(login))
                throw new Exception(Validaciones_GV42.MENSAJE_LOGIN);
            if (!Validaciones_GV42.EsContrasenaValida(contrasenaPlana))
                throw new Exception(Validaciones_GV42.MENSAJE_CONTRASENA);

            if (_DALUsuario.ExisteDNI(dni))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.dniDuplicado"), dni));
            if (_DALUsuario.BuscarPorLogin(login) != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.usuarioLoginDuplicado"), login));

            nombre = Validaciones_GV42.NormalizarEspacios(nombre);
            apellido = Validaciones_GV42.NormalizarEspacios(apellido);
            VerificarMismaPersonaQuePasajero(dni, nombre, apellido);

            string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);

            Usuario_GV42 u = new Usuario_GV42
            {
                DNI = dni,
                Apellido = apellido,
                Nombre = nombre,
                Login = login,
                Contrasena = contrasenaCifrada,
                Rol = rolCliente,
                Email = email
            };

            int filas = _DALUsuario.AgregarUsuarioAutogestionado(u);
            if (filas == 0)
                throw new Exception(IdiomaManager_GV42.T("err.insertFallido"));

            Auditar(login, "Reservas", "Cliente autogestionado registrado", "Login: " + login, "Media");
            RecalcularUsuario();

            u.Rol = rolCliente;
            return u;
        }

        // ---------------------------------------------------------------------------------
        // Serialización XML del maestro de usuarios.
        // Se serializa lo que el operador ve en la matriz de la pantalla (no se vuelve a leer
        // la base). No hay tabla para los archivos XML: quedan en la carpeta que elija el operador.
        // ---------------------------------------------------------------------------------

        public int SerializarUsuarios(List<Usuario_GV42> usuarios, string rutaArchivo)
        {
            if (usuarios == null || usuarios.Count == 0)
                throw new Exception(IdiomaManager_GV42.T("serializacion.sinDatos"));
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new Exception(IdiomaManager_GV42.T("serializacion.sinUbicacion"));
            if (!string.Equals(System.IO.Path.GetExtension(rutaArchivo), ".xml", StringComparison.OrdinalIgnoreCase))
                rutaArchivo += ".xml";

            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            string operador = actual != null ? actual.Login : "sistema";

            DateTime ahora = DateTime.Now;
            var lista = new ListaUsuariosXml_GV42
            {
                // Sin milisegundos ni zona horaria: el atributo queda legible (2026-09-30T00:05:12).
                FechaGeneracion = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second),
                GeneradoPor = operador,
                Cantidad = usuarios.Count,
                Usuarios = usuarios.Select(UsuarioXml_GV42.DesdeUsuario).ToList()
            };

            try
            {
                // 1) Datos: XML generado por XmlSerializer, con la referencia estándar a la hoja CSS.
                SerializadorXml_GV42.Serializar(lista, rutaArchivo, EstiloXmlUsuarios_GV42.NOMBRE_ARCHIVO);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception(IdiomaManager_GV42.T("serializacion.sinPermisoEscritura"));
            }
            catch (System.IO.IOException ex)
            {
                throw new Exception(IdiomaManager_GV42.T("serializacion.errorSerializar") + " " + ex.Message);
            }

            // 2) Presentación: la hoja CSS se guarda aparte, en la misma carpeta que el XML.
            //    Si no se puede escribir, el XML sigue siendo válido y des-serializable (solo se ve sin estilo).
            try
            {
                string carpeta = System.IO.Path.GetDirectoryName(rutaArchivo) ?? string.Empty;
                System.IO.File.WriteAllText(System.IO.Path.Combine(carpeta, EstiloXmlUsuarios_GV42.NOMBRE_ARCHIVO),
                                            EstiloXmlUsuarios_GV42.CSS, new System.Text.UTF8Encoding(false));
            }
            catch { }

            try
            {
                Auditar(operador, "Admin", "Usuarios serializados a XML",
                        $"{usuarios.Count} usuario(s) -> {System.IO.Path.GetFileName(rutaArchivo)}", "Baja");
            }
            catch { }

            return usuarios.Count;
        }

        public List<Usuario_GV42> DeserializarUsuarios(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new Exception(IdiomaManager_GV42.T("serializacion.sinArchivo"));
            if (!System.IO.File.Exists(rutaArchivo))
                throw new Exception(IdiomaManager_GV42.T("serializacion.archivoNoExiste"));

            ListaUsuariosXml_GV42 lista;
            try
            {
                lista = SerializadorXml_GV42.Deserializar<ListaUsuariosXml_GV42>(rutaArchivo);
            }
            catch (Exception)
            {
                // XML mal formado, raíz distinta a <MaestroUsuarios>, tipos inválidos, etc.
                throw new Exception(IdiomaManager_GV42.T("serializacion.formatoInvalido"));
            }

            List<Usuario_GV42> usuarios = (lista?.Usuarios ?? new List<UsuarioXml_GV42>())
                .Select(x => x.AUsuario())
                .ToList();

            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            try
            {
                Auditar(actual != null ? actual.Login : "sistema", "Admin", "Usuarios deserializados desde XML",
                        $"{usuarios.Count} usuario(s) <- {System.IO.Path.GetFileName(rutaArchivo)}", "Baja");
            }
            catch { }

            return usuarios;
        }

        // Login y contraseña inicial = primer nombre en minúsculas, sin tildes ni signos, + últimos 3 del DNI.
        // Antes se usaba el nombre tal cual: "María José" generaba el login "maría josé544", que la pantalla
        // de login rechaza (solo acepta letras, números y puntos), así que ese usuario nunca podía entrar.
        private static string CredencialInicial(string nombre, string dni)
        {
            string primerNombre = Validaciones_GV42.NormalizarEspacios(nombre).Split(' ')[0];
            string sinTildes = new string(primerNombre.ToLowerInvariant()
                .Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                .Where(c => (c >= 'a' && c <= 'z') || c == 'ñ')
                .Select(c => c == 'ñ' ? 'n' : c)
                .ToArray());
            if (sinTildes.Length == 0) sinTildes = "usuario";
            string ultimos3 = dni.Length >= 3 ? dni.Substring(dni.Length - 3) : dni;
            return sinTildes + ultimos3;
        }

        // El DNI identifica a una sola persona: si ya viajó (está en Pasajero), la cuenta que se crea
        // tiene que ser a su mismo nombre y apellido.
        private static void VerificarMismaPersonaQuePasajero(string dni, string nombre, string apellido)
        {
            BE.Pasajero_GV42 pasajero = new DAL.DALPasajero_GV42().BuscarPorDni(dni);
            if (pasajero == null) return;
            if (!Validaciones_GV42.MismoTexto(nombre, pasajero.Nombre) || !Validaciones_GV42.MismoTexto(apellido, pasajero.Apellido))
                throw new Exception("El DNI " + dni + " ya está registrado como pasajero a nombre de " +
                                    pasajero.Nombre + " " + pasajero.Apellido + ". Verificá el DNI o los datos.");
        }

        public enum ResultadoCambioContrasena
        {
            Exitoso,
            ContrasenaActualIncorrecta,
            ContrasenasNoCoinciden,
            UsuarioInexistente,
            NuevaIgualActual
        }

        public ResultadoCambioContrasena CambiarContrasena(string login, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            try
            {
                if (nuevaContrasena != confirmarContrasena)
                    return ResultadoCambioContrasena.ContrasenasNoCoinciden;
                if (nuevaContrasena == contrasenaActual)
                    return ResultadoCambioContrasena.NuevaIgualActual;

                Usuario_GV42 usuario = _DALUsuario.BuscarPorLogin(login);
                if (usuario == null)
                    return ResultadoCambioContrasena.UsuarioInexistente;

                string contrasenaActualCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaActual);
                if (usuario.Contrasena != contrasenaActualCifrada)
                    return ResultadoCambioContrasena.ContrasenaActualIncorrecta;

                string nuevaContrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(nuevaContrasena);
                if (usuario.Contrasena == nuevaContrasenaCifrada)
                    return ResultadoCambioContrasena.NuevaIgualActual;

                _DALUsuario.CambiarContrasena(login, nuevaContrasenaCifrada);

                Auditar(login, "Usuario", "Contraseña cambiada exitosamente", "Cambio de contraseña", "Baja");
                return ResultadoCambioContrasena.Exitoso;
            }
            catch
            {
                throw new Exception(IdiomaManager_GV42.T("err.errorGenerico"));
            }
        }

        public static void CerrarSesión()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual != null)
            {
                // Si la bitácora falla, igual se tiene que cerrar la sesión
                // (si no, el siguiente login devuelve "sesión activa").
                try
                {
                    BLLUsuario_GV42 bll = new BLLUsuario_GV42();
                    bll.Auditar(actual.Login, "Usuario", "Logout realizado", "LogOut", "Alta");
                }
                catch { }
            }
            SessionManager_GV42.Instancia.CerrarSesion();
            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.ES);
        }
    }
}
