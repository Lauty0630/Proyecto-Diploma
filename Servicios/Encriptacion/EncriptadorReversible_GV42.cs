using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Servicios
{

    public class EncriptadorReversible_GV42
    {
        #region Campos

        private static EncriptadorReversible_GV42 _instancia;

        private static readonly byte[] CLAVE = Encoding.UTF8.GetBytes("GV42_K3y_S3cr3ta_ProyectoIngSft!");

        private static readonly byte[] IV = Encoding.UTF8.GetBytes("GV42_IV_ProyMng_");

        #endregion

        #region Constructor

        private EncriptadorReversible_GV42() { }

        #endregion

        #region Propiedades

        public static EncriptadorReversible_GV42 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new EncriptadorReversible_GV42();
                return _instancia;
            }
        }

        #endregion

        #region Métodos públicos

        public string Encriptar(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano)) return textoPlano;

            using (Aes aes = Aes.Create())
            {
                aes.Key = CLAVE;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] textoBytes = Encoding.UTF8.GetBytes(textoPlano);
                    cs.Write(textoBytes, 0, textoBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Desencriptar(string textoCifrado)
        {
            if (string.IsNullOrEmpty(textoCifrado)) return textoCifrado;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = CLAVE;
                    aes.IV = IV;

                    byte[] cifradoBytes = Convert.FromBase64String(textoCifrado);
                    using (MemoryStream ms = new MemoryStream(cifradoBytes))
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return textoCifrado;
            }
        }

        #endregion
    }
}
