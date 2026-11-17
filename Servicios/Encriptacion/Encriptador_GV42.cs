using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{

    // Hash de contraseñas con PBKDF2 (SHA-256, 100.000 iteraciones) y una sal aleatoria por usuario.
    // Formato guardado en Usuario.Contrasena:  PBKDF2$<iteraciones>$<sal en Base64>$<hash en Base64>
    //
    // Antes se guardaba SHA-256 sin sal: dos usuarios con la misma clave tenían el mismo hash y una
    // tabla precalculada encontraba las claves comunes al instante. Los hashes viejos (64 caracteres
    // hexadecimales) se siguen aceptando y se reemplazan por el formato nuevo en el próximo login.
    public class Encriptador_GV42
    {
        #region Campos

        private static Encriptador_GV42 _instancia;

        private const string PREFIJO = "PBKDF2";
        private const int ITERACIONES = 100000;
        private const int BYTES_SAL = 16;
        private const int BYTES_HASH = 32;

        #endregion

        #region Constructor

        private Encriptador_GV42() { }

        #endregion

        #region Propiedades

        public static Encriptador_GV42 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Encriptador_GV42();
                return _instancia;
            }
        }

        #endregion

        #region Métodos públicos

        // Genera el hash a guardar. Como la sal es aleatoria, dos llamadas con la misma clave dan
        // resultados distintos: para comparar se usa siempre VerificarContrasena, nunca "==".
        public string EncriptarContrasena(string contrasenaPlana)
        {
            if (string.IsNullOrEmpty(contrasenaPlana))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            byte[] sal = new byte[BYTES_SAL];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(sal);

            byte[] hash = Derivar(contrasenaPlana, sal, ITERACIONES);
            return PREFIJO + "$" + ITERACIONES + "$" + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        // True si la contraseña corresponde al hash guardado (formato nuevo o SHA-256 viejo).
        public bool VerificarContrasena(string contrasenaPlana, string hashGuardado)
        {
            if (string.IsNullOrEmpty(contrasenaPlana) || string.IsNullOrEmpty(hashGuardado))
                return false;

            if (EsHashLegado(hashGuardado))
                return IgualesTiempoConstante(
                    Encoding.ASCII.GetBytes(Sha256Hex(contrasenaPlana)),
                    Encoding.ASCII.GetBytes(hashGuardado.ToLowerInvariant()));

            string[] partes = hashGuardado.Split('$');
            if (partes.Length != 4 || partes[0] != PREFIJO) return false;
            if (!int.TryParse(partes[1], out int iteraciones) || iteraciones <= 0) return false;

            try
            {
                byte[] sal = Convert.FromBase64String(partes[2]);
                byte[] esperado = Convert.FromBase64String(partes[3]);
                byte[] calculado = Derivar(contrasenaPlana, sal, iteraciones, esperado.Length);
                return IgualesTiempoConstante(calculado, esperado);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        // True si el hash es del formato viejo (SHA-256 sin sal) y conviene reemplazarlo.
        public bool EsHashLegado(string hashGuardado)
        {
            return hashGuardado != null && hashGuardado.Length == 64 &&
                   hashGuardado.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
        }

        #endregion

        #region Métodos privados

        private static byte[] Derivar(string contrasena, byte[] sal, int iteraciones, int largo = BYTES_HASH)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(contrasena), sal, iteraciones, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(largo);
        }

        private static string Sha256Hex(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // Compara sin cortar en la primera diferencia (no deja medir cuántos bytes coincidieron).
        private static bool IgualesTiempoConstante(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diferencia = 0;
            for (int i = 0; i < a.Length; i++)
                diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }

        #endregion
    }
}
