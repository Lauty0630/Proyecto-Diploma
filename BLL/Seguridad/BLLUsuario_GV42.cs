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
        #region Constantes

        public const int MAX_INTENTOS = 3;

        #endregion

        #region Campos

        private readonly DALUsuario_GV42 _DALUsuario;
        private readonly BLLIntegridad_GV42 _bllIntegridad;
        private static readonly TimeSpan VENTANA_INTENTOS = TimeSpan.FromHours(1);

        #endregion

        #region Constructor

        public BLLUsuario_GV42()
        {
            _DALUsuario = new DALUsuario_GV42();
            _bllIntegridad = new BLLIntegridad_GV42();
        }

        #endregion

        #region Login

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

        #endregion

        #region Consultas

        public List<Usuario_GV42> ListarActivos() => _DALUsuario.ListarActivos();
        public List<Usuario_GV42> ListarTodos() => _DALUsuario.ListarTodos();

        public List<Rol_GV42> ListarRoles() => _DALUsuario.ListarRoles();

        public Usuario_GV42 BuscarPorLogin(string login) => _DALUsuario.BuscarPorLogin(login);

        public bool ExisteDNI(string dni) => _DALUsuario.ExisteDNI(dni);

        #endregion

        #region Idioma

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

        #endregion

        #region Administración de usuarios

        // Devuelve la contraseña temporal con la que el usuario vuelve a entrar.
        public string Desbloquear(string dni, string login)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Usuarios.Desbloquear", IdiomaManager_GV42.T("neg.usuario.sinPermisoDesbloquear"));
            Usuario_GV42 usuario = _DALUsuario.BuscarPorLogin(login);
            if (usuario == null || usuario.DNI != dni)
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.noExiste"));
            if (!usuario.Bloqueo)
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.noBloqueado", login));

            string contrasenaPlana = CredencialInicial(usuario.Nombre, dni);
            string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);
            _DALUsuario.Desbloquear(dni, contrasenaCifrada);
            Auditar(LoginSesion(), "Admin", "Usuario desbloqueado", $"Usuario {login} desbloqueado y contraseña reseteada", "Media");
            RecalcularUsuario();
            return contrasenaPlana;
        }

        public void ActivarDesactivar(string dni, bool activo)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Usuarios.Activar", IdiomaManager_GV42.T("neg.usuario.sinPermisoActivar"));
            if (!activo)
            {
                Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
                if (actual != null && actual.DNI == dni)
                    throw new Exception(IdiomaManager_GV42.T("neg.usuario.desactivarPropio"));
                if (!new BLLPermisos_GV42().QuedaAlgunAdministrador(dniExcluido: dni))
                    throw new Exception(IdiomaManager_GV42.T("neg.usuario.ultimoAdminDesactivar"));
            }
            _DALUsuario.ActivarDesactivar(dni, activo);
            string accion = activo ? "Usuario activado" : "Usuario desactivado";
            Auditar(LoginSesion(), "Admin", accion, $"DNI: {dni}", "Media");
            RecalcularUsuario();
        }

        public void ModificarEmail(string dni, string email)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Usuarios.Modificar", IdiomaManager_GV42.T("neg.usuario.sinPermisoModificar"));
            email = (email ?? string.Empty).Trim();
            if (!Validaciones_GV42.EsEmailValido(email))
                throw new Exception(Validaciones_GV42.MENSAJE_EMAIL);
            if (_DALUsuario.ExisteEmail(email, dni))
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.emailEnUso", email));
            _DALUsuario.ModificarEmail(dni, email);
            Auditar(LoginSesion(), "Admin", "Email modificado", $"DNI: {dni}", "Media");
            RecalcularUsuario();
        }

        public void ModificarRol(string dni, Rol_GV42 rol)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Usuarios.Modificar", IdiomaManager_GV42.T("neg.usuario.sinPermisoModificar"));
            if (rol == null) throw new Exception(IdiomaManager_GV42.T("err.rolValido"));
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual != null && actual.DNI == dni && (actual.Rol == null || actual.Rol.Id != rol.Id))
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.cambiarPropioRol"));
            if (!new BLLPermisos_GV42().QuedaAlgunAdministrador(dniConNuevoRol: dni, nuevoRolId: rol.Id))
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.ultimoAdminRol"));
            _DALUsuario.ModificarRol(dni, rol.Id);
            Auditar(LoginSesion(), "Admin", "Rol modificado", $"DNI {dni} -> rol {rol.Nombre}", "Media");
            RecalcularUsuario();
        }

        // Devuelve el login generado (la contraseña inicial es la misma y se pide cambiarla al entrar).
        public string CrearUsuario(string dni, string apellido, string nombre, string email, Rol_GV42 rol)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Usuarios.Crear", IdiomaManager_GV42.T("neg.usuario.sinPermisoCrear"));
            if (rol == null)
                throw new Exception(IdiomaManager_GV42.T("err.rolSeleccionar"));

            ValidarDatosPersonales(ref dni, ref nombre, ref apellido, ref email);

            if (_DALUsuario.ExisteDNI(dni))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.dniDuplicado"), dni));
            if (_DALUsuario.ExisteEmail(email, dni))
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.emailEnUso", email));

            VerificarMismaPersonaQuePasajero(dni, nombre, apellido);

            string contrasenaPlana = CredencialInicial(nombre, dni);
            string contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);

            // Si dos personas generan el mismo login (mismo nombre y mismos 3 últimos dígitos del DNI),
            // antes el alta fallaba sin remedio; ahora se agrega un número: juan678, juan6782, juan6783...
            string login = contrasenaPlana;
            for (int n = 2; _DALUsuario.BuscarPorLogin(login) != null; n++)
            {
                if (n > 99) throw new Exception(string.Format(IdiomaManager_GV42.T("err.usuarioLoginDuplicado"), contrasenaPlana));
                login = contrasenaPlana + n;
            }
            // La contraseña inicial es igual al login (también cuando se le agregó un número).
            contrasenaPlana = login;
            contrasenaCifrada = Encriptador_GV42.Instancia.EncriptarContrasena(contrasenaPlana);

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
            Auditar(LoginSesion(), "Admin", "Usuario creado", $"Login: {login}", "Baja");
            RecalcularUsuario();
            return login;
        }

        // Alta de la cuenta de un cliente autogestionado (RFN 1: el cliente se registra y reserva
        // por sí mismo, sin empleado). El rol "Cliente" debe existir en la tabla Roles (lo crea el
        // script de negocio). Devuelve el usuario creado, ya con su Rol completo.
        public Usuario_GV42 CrearUsuarioAutogestionado(string dni, string nombre, string apellido, string email,
                                                       string login, string contrasenaPlana)
        {
            Rol_GV42 rolCliente = _DALUsuario.BuscarPorNombre("Cliente");
            if (rolCliente == null)
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.sinRolCliente"));

            login = (login ?? string.Empty).Trim();
            if (!Validaciones_GV42.EsLoginValido(login))
                throw new Exception(Validaciones_GV42.MENSAJE_LOGIN);
            if (!Validaciones_GV42.EsContrasenaValida(contrasenaPlana))
                throw new Exception(Validaciones_GV42.MENSAJE_CONTRASENA);
            ValidarDatosPersonales(ref dni, ref nombre, ref apellido, ref email);
            if (_DALUsuario.ExisteEmail(email, dni))
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.emailEnUso", email));

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

        #endregion

        #region Contraseña

        public ResultadoCambioContrasena CambiarContrasena(string login, string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            try
            {
                if (nuevaContrasena != confirmarContrasena)
                    return ResultadoCambioContrasena.ContrasenasNoCoinciden;
                if (nuevaContrasena == contrasenaActual)
                    return ResultadoCambioContrasena.NuevaIgualActual;
                if (!Validaciones_GV42.EsContrasenaValida(nuevaContrasena))
                    return ResultadoCambioContrasena.NoCumplePolitica;

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
            catch (Exception ex)
            {
                // Se conserva el motivo real (antes solo decía "Error").
                throw new Exception(IdiomaManager_GV42.T("err.errorGenerico") + " " + ex.Message, ex);
            }
        }

        #endregion

        #region Sesión

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

        #endregion

        #region Métodos privados

        private void RecalcularUsuario()
        {
            try { _bllIntegridad.RecalcularTabla("Usuario"); } catch { }
        }

        // La bitácora nunca interrumpe la operación: antes, si fallaba al registrar (por ejemplo un login
        // inexistente), el login devolvía "Error" o el alta decía "error" aunque el usuario se había creado.
        private void Auditar(string login, string modulo, string tipoEvento, string detalle, string criticidad)
        {
            try
            {
                BLLBitacora_GV42.Instancia.RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad);
            }
            catch { }
        }

        private static string LoginSesion()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            return actual != null ? actual.Login : "sistema";
        }

        // Valida y normaliza los datos personales de un usuario (también se validan en la pantalla,
        // pero la regla vive acá para que ninguna otra pantalla pueda saltearla).
        private static void ValidarDatosPersonales(ref string dni, ref string nombre, ref string apellido, ref string email)
        {
            dni = (dni ?? string.Empty).Trim();
            nombre = Validaciones_GV42.NormalizarEspacios(nombre);
            apellido = Validaciones_GV42.NormalizarEspacios(apellido);
            email = (email ?? string.Empty).Trim();

            if (!Validaciones_GV42.EsDniValido(dni)) throw new Exception(Validaciones_GV42.MENSAJE_DNI);
            if (!Validaciones_GV42.EsNombreValido(nombre)) throw new Exception(Validaciones_GV42.MENSAJE_NOMBRE);
            if (!Validaciones_GV42.EsApellidoValido(apellido)) throw new Exception(Validaciones_GV42.MENSAJE_APELLIDO);
            if (!Validaciones_GV42.EsEmailValido(email)) throw new Exception(Validaciones_GV42.MENSAJE_EMAIL);
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
                throw new Exception(IdiomaManager_GV42.T("neg.usuario.dniComoPasajero", dni, pasajero.Nombre, pasajero.Apellido));
        }

        #endregion

        #region Tipos anidados

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

        public enum ResultadoCambioContrasena
        {
            Exitoso,
            ContrasenaActualIncorrecta,
            ContrasenasNoCoinciden,
            UsuarioInexistente,
            NuevaIgualActual,
            NoCumplePolitica
        }

        #endregion
    }
}
