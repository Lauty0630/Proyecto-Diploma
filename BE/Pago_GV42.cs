using System;

namespace BE
{
    public class Pago_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdReserva { get; set; }
        public string NumeroReserva { get; set; }
        public decimal ImporteTotalAbonado { get; set; }
        public MedioPago_GV42 MedioPago { get; set; }
        public string NumeroTransaccion { get; set; }
        public DateTime FechaHoraPago { get; set; }
        public string LoginVendedor { get; set; }

        public string MedioPagoTexto { get { return MedioPago.Texto(); } }

        #endregion
    }
}
