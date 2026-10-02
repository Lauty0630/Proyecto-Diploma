using System;

namespace BE
{
    // Búsqueda con fechas flexibles: precio más barato de un día cercano al buscado
    // (HayVuelos = false si ese día no hay vuelos disponibles para la ruta).
    public class PrecioFecha_GV42
    {
        #region Propiedades

        public DateTime Fecha { get; set; }
        public bool HayVuelos { get; set; }
        public decimal PrecioMinimo { get; set; }

        #endregion
    }
}
