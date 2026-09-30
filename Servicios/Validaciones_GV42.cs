using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Servicios
{

    public static class Validaciones_GV42
    {
        // DNI argentino: 7 u 8 dígitos (los DNI viejos tienen 7), sin puntos y sin empezar con 0.
        public const string REGEX_DNI = @"^[1-9]\d{6,7}$";

        public const string REGEX_EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$";

        // Letras (con tildes, ñ y ü) separadas por un espacio, apóstrofo o guion: "María José", "D'Angelo", "Pérez-Gil".
        public const string REGEX_SOLO_LETRAS = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+([ '\-][a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+)*$";

        public const string REGEX_CONTRASENA = @"^(?=.*[A-Za-zÁÉÍÓÚáéíóúÑñ])(?=.*\d).{6,}$";

        public const string REGEX_LOGIN = @"^[a-zA-Z0-9.]{3,}$";

        public const string REGEX_TELEFONO = @"^[0-9+\-\s()]{6,20}$";

        // Largos máximos: coinciden con las columnas de la base (Pasajero.Nombre/Apellido = 60) y
        // dejan margen para el cifrado del email (Usuario.Email = 250 caracteres ya cifrado).
        public const int MAX_NOMBRE = 60;
        public const int MAX_EMAIL = 100;
        public const int MAX_LOGIN = 50;

        public static bool EsDniValido(string dni) => !string.IsNullOrWhiteSpace(dni) && Regex.IsMatch(dni, REGEX_DNI);

        public static bool EsEmailValido(string email) =>
            !string.IsNullOrWhiteSpace(email) && email.Length <= MAX_EMAIL &&
            !email.Contains("..") && Regex.IsMatch(email, REGEX_EMAIL);

        public static bool EsNombreValido(string nombre) => EsTextoDeLetras(nombre);

        public static bool EsApellidoValido(string apellido) => EsTextoDeLetras(apellido);

        private static bool EsTextoDeLetras(string texto) =>
            !string.IsNullOrWhiteSpace(texto) && texto.Length >= 2 && texto.Length <= MAX_NOMBRE &&
            Regex.IsMatch(texto, REGEX_SOLO_LETRAS);

        public static bool EsContrasenaValida(string contrasena) => !string.IsNullOrWhiteSpace(contrasena) && Regex.IsMatch(contrasena, REGEX_CONTRASENA);

        public static bool EsLoginValido(string login) =>
            !string.IsNullOrWhiteSpace(login) && login.Length <= MAX_LOGIN && Regex.IsMatch(login, REGEX_LOGIN);

        // Entre 6 y 20 caracteres permitidos y, además, entre 6 y 15 dígitos reales
        // (antes "------" o "(((((( " pasaban como teléfono válido).
        public static bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, REGEX_TELEFONO)) return false;
            int digitos = telefono.Count(char.IsDigit);
            return digitos >= 6 && digitos <= 15;
        }

        // Saca espacios al principio/final y deja un solo espacio entre palabras.
        public static string NormalizarEspacios(string texto) =>
            Regex.Replace((texto ?? string.Empty).Trim(), @"\s+", " ");

        // Compara dos nombres sin distinguir mayúsculas, tildes ni espacios de más
        // ("José  PÉREZ" == "jose perez"). Se usa para saber si un DNI ya registrado
        // corresponde a la misma persona.
        public static bool MismoTexto(string a, string b) =>
            string.Equals(Canonico(a), Canonico(b), System.StringComparison.Ordinal);

        private static string Canonico(string texto)
        {
            string normal = NormalizarEspacios(texto).ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(normal.Length);
            foreach (char c in normal)
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public const string MENSAJE_DNI = "El DNI debe tener 7 u 8 dígitos numéricos, sin puntos ni espacios y sin empezar con 0. Ej: 46947544";

        public const string MENSAJE_EMAIL = "El email no tiene un formato válido (máximo 100 caracteres). Debe contener @ y un dominio. Ej: jeremias@gmail.com";

        public const string MENSAJE_NOMBRE = "El nombre solo puede contener letras, espacios, apóstrofo o guion (entre 2 y 60 caracteres).";

        public const string MENSAJE_APELLIDO = "El apellido solo puede contener letras, espacios, apóstrofo o guion (entre 2 y 60 caracteres).";

        public const string MENSAJE_CONTRASENA = "La contraseña debe tener al menos 6 caracteres, una letra y un número.";

        public const string MENSAJE_LOGIN = "El usuario solo puede contener letras, números y puntos (entre 3 y 50 caracteres).";

        public const string MENSAJE_TELEFONO = "El teléfono debe tener entre 6 y 15 dígitos (se permiten espacios, +, - y paréntesis). Ej: 11 4567-8901";
    }
}
