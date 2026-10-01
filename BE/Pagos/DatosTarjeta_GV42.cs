using System;

namespace BE
{
    // Datos que se tipean para pagar con tarjeta. Solo viajan hasta la BLL para validarlos
    // (Luhn, vencimiento, código de seguridad): nunca se guardan en la base. Del pago queda
    // registrada solo la marca, los últimos 4 dígitos y el código de autorización.
    public class DatosTarjeta_GV42
    {
        #region Propiedades

        public string Numero { get; set; }
        public string Titular { get; set; }
        public int MesVencimiento { get; set; }
        public int AnioVencimiento { get; set; }
        public string CodigoSeguridad { get; set; }

        #endregion
    }
}
