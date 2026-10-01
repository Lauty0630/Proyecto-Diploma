using BE;
using Servicios;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BLL
{
    // Utilidades compartidas por BLLReserva_GV42 y BLLCheckIn_GV42.
    internal static class BLLNegocioUtil_GV42
    {
        #region Constantes

        public const string MODULO_RESERVAS = "Reservas";
        public const string MODULO_CHECKIN = "CheckIn";
        public const string MODULO_VUELOS = "Vuelos";

        #endregion

        #region Campos

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        #endregion

        #region Sesión y permisos

        // Login del usuario que tiene la sesión iniciada (vendedor o encargado de check-in).
        public static string LoginActual()
        {
            Usuario_GV42 u = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (u == null || string.IsNullOrWhiteSpace(u.Login))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.sesion.requerida"));
            return u.Login;
        }

        // ¿El rol de la sesión tiene esa patente? Las reglas de negocio la vuelven a chequear
        // aunque la pantalla ya oculte el botón.
        public static bool TienePatente(string dataKey)
        {
            Usuario_GV42 u = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (u == null || u.Rol == null) return false;
            Rol_GV42 rol = new BLLPermisos_GV42().ObtenerArbolRol(u.Rol.Id);
            return rol != null && rol.TienePermiso(dataKey);
        }

        // 'mensaje' ya llega traducido (IdiomaManager_GV42.T("neg...")).
        public static void ExigirPatente(string dataKey, string mensaje)
        {
            LoginActual();
            if (!TienePatente(dataKey))
                throw new NegocioException_GV42(mensaje);
        }

        #endregion

        #region Bitácora

        // Registra en la bitácora. Nunca interrumpe la operación de negocio si la bitácora falla
        // (por ejemplo, si todavía no se cargaron los módulos/eventos del negocio en el catálogo).
        // Lo que se guarda es dato: el tipo de evento y el detalle quedan siempre en español.
        public static void Auditar(string modulo, string tipoEvento, string detalle, string criticidad)
        {
            try
            {
                Usuario_GV42 u = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
                string login = u != null ? u.Login : "sistema";
                BLLBitacora_GV42.Instancia.RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad);
            }
            catch
            {
            }
        }

        #endregion

        #region Formatos

        public static string Dinero(decimal importe)
        {
            return "$ " + importe.ToString("N2", Cultura);
        }

        // Nombre de un servicio adicional (viene de la base en español) en el idioma actual.
        public static string NombreAdicional(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return nombre ?? string.Empty;
            return IdiomaManager_GV42.TConDefecto("adicional." + nombre, nombre);
        }

        #endregion

        #region Validaciones

        // Valida los datos de una persona (cliente o pasajero) y les quita los espacios sobrantes.
        // 'rol' ya llega traducido (por ejemplo T("neg.rol.pasajeroN", 1) = "Pasajero 1").
        public static void ValidarPersona(Persona_GV42 p, string rol)
        {
            if (p == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.persona.faltanDatos", rol));

            p.DNI = (p.DNI ?? string.Empty).Trim();
            p.Nombre = Validaciones_GV42.NormalizarEspacios(p.Nombre);
            p.Apellido = Validaciones_GV42.NormalizarEspacios(p.Apellido);
            p.Email = (p.Email ?? string.Empty).Trim();
            p.Telefono = Validaciones_GV42.NormalizarEspacios(p.Telefono);

            if (!Validaciones_GV42.EsDniValido(p.DNI))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_DNI);
            if (!Validaciones_GV42.EsNombreValido(p.Nombre))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_NOMBRE);
            if (!Validaciones_GV42.EsApellidoValido(p.Apellido))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_APELLIDO);
            if (!Validaciones_GV42.EsEmailValido(p.Email))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_EMAIL);
            if (!Validaciones_GV42.EsTelefonoValido(p.Telefono))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_TELEFONO);
        }

        #endregion
    }
}
