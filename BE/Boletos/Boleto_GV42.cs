using System;

namespace BE
{
    // Se emite un boleto por cada pasajero y por cada tramo (ida / vuelta) al confirmarse el pago.
    public class Boleto_GV42
    {
        #region Propiedades

        public int Id { get; set; }

        // Autogenerado por la base de datos (ej: BOL-000001).
        public string NumeroBoleto { get; set; }

        public string NumeroReserva { get; set; }
        public Pasajero_GV42 Pasajero { get; set; }
        public string CodigoVuelo { get; set; }
        // 1 = ida, 2 = vuelta.
        public int Tramo { get; set; } = 1;
        public DateTime FechaEmision { get; set; }

        public string PasajeroNombre { get { return Pasajero != null ? Pasajero.NombreCompleto : string.Empty; } }
        public string PasajeroDni { get { return Pasajero != null ? Pasajero.DNI : string.Empty; } }

        #endregion
    }
}
