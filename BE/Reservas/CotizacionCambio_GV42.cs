using System;

namespace BE
{
    // Lo que cuesta cambiar el vuelo de un tramo de la reserva: la penalidad de la tarifa más la
    // diferencia de tarifa (con impuestos) si el vuelo nuevo es más caro. Si es más barato no se devuelve.
    public class CotizacionCambio_GV42
    {
        #region Propiedades

        public decimal TarifaActual { get; set; }      // tarifa del tramo actual, todos los pasajeros
        public decimal TarifaNueva { get; set; }       // tarifa del vuelo nuevo, todos los pasajeros
        public decimal PorcentajePenalidad { get; set; }
        public decimal Penalidad { get; set; }
        public decimal DiferenciaTarifa { get; set; }  // 0 si el vuelo nuevo es igual o más barato
        public decimal ImpuestosDiferencia { get; set; }

        public decimal Total { get { return Penalidad + DiferenciaTarifa + ImpuestosDiferencia; } }

        #endregion
    }
}
