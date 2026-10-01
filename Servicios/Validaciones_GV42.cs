using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Servicios
{

    public static class Validaciones_GV42
    {
        #region Campos

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

        public const int MIN_DIGITOS_TARJETA = 13;
        public const int MAX_DIGITOS_TARJETA = 19;

        public const int MAX_ANIOS_VENCIMIENTO = 15;

        #endregion

        #region Propiedades

        public static string MENSAJE_DNI => IdiomaManager_GV42.TConDefecto("val.dni", "El DNI debe tener 7 u 8 dígitos numéricos, sin puntos ni espacios y sin empezar con 0. Ej: 46947544");

        public static string MENSAJE_EMAIL => IdiomaManager_GV42.TConDefecto("val.email", "El email no tiene un formato válido (máximo 100 caracteres). Debe contener @ y un dominio. Ej: jeremias@gmail.com");

        public static string MENSAJE_NOMBRE => IdiomaManager_GV42.TConDefecto("val.nombre", "El nombre solo puede contener letras, espacios, apóstrofo o guion (entre 2 y 60 caracteres).");

        public static string MENSAJE_APELLIDO => IdiomaManager_GV42.TConDefecto("val.apellido", "El apellido solo puede contener letras, espacios, apóstrofo o guion (entre 2 y 60 caracteres).");

        public static string MENSAJE_CONTRASENA => IdiomaManager_GV42.TConDefecto("val.contrasena", "La contraseña debe tener entre 6 y 50 caracteres, al menos una letra y un número, y no puede empezar ni terminar con espacios.");

        public static string MENSAJE_LOGIN => IdiomaManager_GV42.TConDefecto("val.login", "El usuario solo puede contener letras, números y puntos (entre 3 y 50 caracteres, sin empezar ni terminar con punto).");

        public static string MENSAJE_NOMBRE_PERFIL => IdiomaManager_GV42.TConDefecto("val.nombre_perfil", "El nombre solo puede tener letras, números, espacios, punto, guion y guion bajo (mínimo 3 caracteres).");

        public static string MENSAJE_TARJETA => IdiomaManager_GV42.TConDefecto("val.tarjeta", "El número de tarjeta no es válido: debe tener entre 13 y 19 dígitos y pasar la verificación del dígito (algoritmo de Luhn). Revisá que no falte ni sobre ningún número.");

        public static string MENSAJE_TELEFONO => IdiomaManager_GV42.TConDefecto("val.telefono", "El teléfono debe tener entre 6 y 15 dígitos (se permiten espacios, +, - y paréntesis). Ej: 11 4567-8901");

        #endregion

        #region Métodos públicos

        public static bool EsDniValido(string dni) => !string.IsNullOrWhiteSpace(dni) && Regex.IsMatch(dni, REGEX_DNI);

        public static bool EsEmailValido(string email) =>
            !string.IsNullOrWhiteSpace(email) && email.Length <= MAX_EMAIL &&
            !email.Contains("..") && Regex.IsMatch(email, REGEX_EMAIL);

        public static bool EsNombreValido(string nombre) => EsTextoDeLetras(nombre);

        public static bool EsApellidoValido(string apellido) => EsTextoDeLetras(apellido);

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

        // ---- Tarjetas (pago con tarjeta de crédito / débito) ----

        // Solo dígitos: se aceptan espacios o guiones al tipear ("4509 9535 6623 3704").
        public static string SoloDigitos(string texto) =>
            new string((texto ?? string.Empty).Where(char.IsDigit).ToArray());

        // Algoritmo de Luhn (módulo 10): desde el último dígito hacia la izquierda se duplica uno
        // de cada dos; si el resultado pasa de 9 se le resta 9; la suma total debe ser múltiplo de 10.
        // Detecta cualquier error de un dígito y casi todas las transposiciones de dígitos vecinos.
        public static bool CumpleLuhn(string numero)
        {
            string digitos = SoloDigitos(numero);
            if (digitos.Length == 0) return false;

            int suma = 0;
            bool duplicar = false;
            for (int i = digitos.Length - 1; i >= 0; i--)
            {
                int d = digitos[i] - '0';
                if (duplicar)
                {
                    d *= 2;
                    if (d > 9) d -= 9;
                }
                suma += d;
                duplicar = !duplicar;
            }
            return suma % 10 == 0;
        }

        // Número de tarjeta: solo dígitos (se toleran espacios y guiones), 13 a 19 dígitos,
        // no todos iguales ("0000 0000 0000 0000" cumple Luhn pero no es una tarjeta) y Luhn válido.
        public static bool EsNumeroTarjetaValido(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return false;
            if (!Regex.IsMatch(numero.Trim(), @"^[\d \-]+\z")) return false;
            string d = SoloDigitos(numero);
            if (d.Length < MIN_DIGITOS_TARJETA || d.Length > MAX_DIGITOS_TARJETA) return false;
            if (d.Distinct().Count() == 1) return false;
            return CumpleLuhn(d);
        }

        // Marca según el prefijo (IIN). Si no se reconoce, "Tarjeta".
        public static string MarcaTarjeta(string numero)
        {
            string d = SoloDigitos(numero);
            if (d.Length < 2) return "Tarjeta";
            int dos = int.Parse(d.Substring(0, 2));
            int cuatro = d.Length >= 4 ? int.Parse(d.Substring(0, 4)) : 0;
            int seis = d.Length >= 6 ? int.Parse(d.Substring(0, 6)) : 0;

            if (dos == 34 || dos == 37) return "American Express";
            if (seis >= 589562 && seis <= 589562) return "Naranja";
            if (seis >= 604201 && seis <= 604219) return "Cabal";
            if (d[0] == '4') return "Visa";
            if ((dos >= 51 && dos <= 55) || (cuatro >= 2221 && cuatro <= 2720)) return "Mastercard";
            if (dos == 50 || (dos >= 56 && dos <= 69)) return "Maestro";
            return "Tarjeta";
        }

        public static bool EsAmex(string numero) => MarcaTarjeta(numero) == "American Express";

        // Código de seguridad: 4 dígitos para American Express, 3 para el resto.
        public static bool EsCodigoSeguridadValido(string codigo, string numeroTarjeta)
        {
            if (string.IsNullOrEmpty(codigo)) return false;
            int largo = EsAmex(numeroTarjeta) ? 4 : 3;
            return Regex.IsMatch(codigo, @"^\d{" + largo + @"}\z");
        }

        // La tarjeta vence el último día del mes indicado. Se rechazan vencimientos a más de 15 años.
        public static bool EsVencimientoValido(int mes, int anio, System.DateTime hoy)
        {
            if (mes < 1 || mes > 12) return false;
            if (anio < 100) anio += 2000;
            if (anio < hoy.Year || anio > hoy.Year + MAX_ANIOS_VENCIMIENTO) return false;
            var finDeMes = new System.DateTime(anio, mes, System.DateTime.DaysInMonth(anio, mes));
            return finDeMes >= hoy.Date;
        }

        // Titular tal como figura en la tarjeta: letras y espacios, 2 a 60 caracteres.
        public static bool EsTitularTarjetaValido(string titular) => EsTextoDeLetras(NormalizarEspacios(titular));

        // "**** 3704": nunca se guarda ni se muestra el número completo.
        public static string EnmascararTarjeta(string numero)
        {
            string d = SoloDigitos(numero);
            return "**** " + (d.Length >= 4 ? d.Substring(d.Length - 4) : d);
        }

        // Saca espacios al principio/final y deja un solo espacio entre palabras.
        public static string NormalizarEspacios(string texto) =>
            Regex.Replace((texto ?? string.Empty).Trim(), @"\s+", " ");

        // Compara dos nombres sin distinguir mayúsculas, tildes ni espacios de más
        // ("José  PÉREZ" == "jose perez"). Se usa para saber si un DNI ya registrado
        // corresponde a la misma persona.
        public static bool MismoTexto(string a, string b) =>
            string.Equals(Canonico(a), Canonico(b), System.StringComparison.Ordinal);

        #endregion

        #region Métodos privados

        private static bool EsTextoDeLetras(string texto) =>
            !string.IsNullOrWhiteSpace(texto) && texto.Length >= 2 && texto.Length <= MAX_NOMBRE &&
            Regex.IsMatch(texto, REGEX_SOLO_LETRAS);

        private static string Canonico(string texto)
        {
            string normal = NormalizarEspacios(texto).ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(normal.Length);
            foreach (char c in normal)
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        #endregion
    }
}
