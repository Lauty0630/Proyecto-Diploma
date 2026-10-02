using System;

namespace BE
{
    // Devolución al cliente por una reserva paga que se canceló: lo abonado menos la penalidad,
    // por el mismo medio con el que pagó. Nace Pendiente y un vendedor lo marca como Procesado.
    public class Reembolso_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdReserva { get; set; }
        public decimal Importe { get; set; }
        public MedioPago_GV42 MedioPago { get; set; }
        public EstadoReembolso_GV42 Estado { get; set; } = EstadoReembolso_GV42.Pendiente;
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string LoginProceso { get; set; }

        public string EstadoTexto { get { return Estado.Texto(); } }

        #endregion
    }
}
