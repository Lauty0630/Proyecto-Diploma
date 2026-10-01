using System;

namespace BE
{
    public class TarjetaEmbarque_GV42
    {
        #region Propiedades

        public int Id { get; set; }

        // Autogenerado por la base de datos (ej: TE-000001).
        public string NumeroTarjeta { get; set; }

        public string NumeroReserva { get; set; }
        public string NombreApellido { get; set; }
        public string DNI { get; set; }
        public string CodigoVuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public string NumeroAsiento { get; set; }
        public ClaseVuelo_GV42 Clase { get; set; }
        public string PuertaEmbarque { get; set; }
        public DateTime HoraLimiteEmbarque { get; set; }
        public DateTime FechaHoraEmision { get; set; }

        public string ClaseTexto { get { return Clase.Texto(); } }

        #endregion
    }
}
