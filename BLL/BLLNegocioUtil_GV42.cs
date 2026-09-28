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
        public const string MODULO_RESERVAS = "Reservas";
        public const string MODULO_CHECKIN = "CheckIn";

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        // Login del usuario que tiene la sesión iniciada (vendedor o encargado de check-in).
        public static string LoginActual()
        {
            Usuario_GV42 u = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (u == null || string.IsNullOrWhiteSpace(u.Login))
                throw new NegocioException_GV42("Debe iniciar sesión para realizar esta operación.");
            return u.Login;
        }

        // Registra en la bitácora. Nunca interrumpe la operación de negocio si la bitácora falla
        // (por ejemplo, si todavía no se cargaron los módulos/eventos del negocio en el catálogo).
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

        public static string Dinero(decimal importe)
        {
            return "$ " + importe.ToString("N2", Cultura);
        }

        // Valida los datos de una persona (cliente o pasajero) y les quita los espacios sobrantes.
        public static void ValidarPersona(Persona_GV42 p, string rol)
        {
            if (p == null)
                throw new NegocioException_GV42("Faltan los datos del " + rol + ".");

            p.DNI = (p.DNI ?? string.Empty).Trim();
            p.Nombre = (p.Nombre ?? string.Empty).Trim();
            p.Apellido = (p.Apellido ?? string.Empty).Trim();
            p.Email = (p.Email ?? string.Empty).Trim();
            p.Telefono = (p.Telefono ?? string.Empty).Trim();

            if (!Validaciones_GV42.EsDniValido(p.DNI))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_DNI);
            if (!Validaciones_GV42.EsNombreValido(p.Nombre))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_NOMBRE);
            if (!Validaciones_GV42.EsApellidoValido(p.Apellido))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_APELLIDO);
            if (!Validaciones_GV42.EsEmailValido(p.Email))
                throw new NegocioException_GV42(rol + ": " + Validaciones_GV42.MENSAJE_EMAIL);
            if (!Regex.IsMatch(p.Telefono, @"^[0-9+\-\s()]{6,20}$"))
                throw new NegocioException_GV42(rol + ": el teléfono debe tener entre 6 y 20 caracteres (números, espacios, +, -, paréntesis).");
        }
    }
}
