using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Servicios
{

    public static class Validaciones_GV42
    {
        // DNI argentino: 7 u 8 dígitos (los DNI viejos tienen 7), sin puntos y sin empezar con 0.
        public const string REGEX_DNI = @"^[1-9]\d{6,7}\z";

        public const string REGEX_EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}\z";

        // Letras (con tildes, ñ y ü) separadas por un espacio, apóstrofo o guion: "María José", "D'Angelo", "Pérez-Gil".
        public const string REGEX_SOLO_LETRAS = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+([ '\-][a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]+)*\z";

        // 6 a 50 caracteres, al menos una letra y un número, sin espacios al principio ni al final
        // (antes el login recortaba espacios y el registro no: una clave con espacio final no servía).
        public const string REGEX_CONTRASENA = @"^(?=.*[A-Za-zÁÉÍÓÚáéíóúÑñ])(?=.*\d)(?!\s)(?!.*\s\z).{6,50}\z";

        // Letras, números y puntos; sin empezar ni terminar con punto y sin dos puntos seguidos.
        public const string REGEX_LOGIN = @"^(?!.*\.\.)[a-zA-Z0-9](?:[a-zA-Z0-9.]{1,48})[a-zA-Z0-9]\z";

        public const string REGEX_TELEFONO = @"^[0-9+\-\s()]{6,20}\z";

        // Largos máximos: coinciden con las columnas de la base (Pasajero.Nombre/Apellido = 60) y
        // dejan margen para el cifrado del email (Usuario.Email = 250 caracteres ya cifrado).
        public const int MAX_NOMBRE = 60;
        public const int MAX_EMAIL = 100;
        public const int MAX_LOGIN = 50;
        public const int MAX_CONTRASENA = 50;
        public const int MAX_TELEFONO = 20;
        public const int MAX_NOMBRE_ROL = 50;       // Roles.Nombre nvarchar(50)
        public const int MAX_NOMBRE_FAMILIA = 100;  // Familia.Nombre nvarchar(100)
        public const int MAX_NUMERO_TRANSACCION = 40; // Pago.NumeroTransaccion nvarchar(40)

        // Nombre de rol o familia: letras, números, espacios, punto, guion y guion bajo.
        public const string REGEX_NOMBRE_PERFIL = @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ]([a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ ._\-]*[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ])?\z";

        // Número de transacción según el medio de pago.
        public const string REGEX_TX_TARJETA = @"^\d{6,20}\z";                 // código de autorización / cupón
        public const string REGEX_TX_TRANSFERENCIA = @"^[A-Za-z0-9\-]{6,40}\z"; // número de operación bancaria

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

        // Nombre de rol (máx. 50) o familia (máx. 100), ya normalizado con NormalizarEspacios.
        public static bool EsNombrePerfilValido(string nombre, int maximo) =>
            !string.IsNullOrWhiteSpace(nombre) && nombre.Length >= 3 && nombre.Length <= maximo &&
            Regex.IsMatch(nombre, REGEX_NOMBRE_PERFIL);

        // Texto para usar dentro de un LIKE: los comodines que escribe el usuario se buscan literalmente.
        public static string EscaparLike(string texto) =>
            (texto ?? string.Empty).Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

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

        public const string MENSAJE_CONTRASENA = "La contraseña debe tener entre 6 y 50 caracteres, al menos una letra y un número, y no puede empezar ni terminar con espacios.";

        public const string MENSAJE_LOGIN = "El usuario solo puede contener letras, números y puntos (entre 3 y 50 caracteres, sin empezar ni terminar con punto).";

        public const string MENSAJE_NOMBRE_PERFIL = "El nombre solo puede tener letras, números, espacios, punto, guion y guion bajo (mínimo 3 caracteres).";

        public const string MENSAJE_TELEFONO = "El teléfono debe tener entre 6 y 15 dígitos (se permiten espacios, +, - y paréntesis). Ej: 11 4567-8901";
    }
}
