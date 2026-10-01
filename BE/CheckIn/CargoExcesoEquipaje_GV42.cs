using System;

namespace BE
{
    public class CargoExcesoEquipaje_GV42
    {
        #region Propiedades

        // Franquicia con la que se calculó el exceso y unidades de equipaje extra que se usaron.
        public decimal FranquiciaKg { get; set; }
        public int UnidadesExtraUsadas { get; set; }

        public decimal KilosExceso { get; set; }
        public decimal CostoPorKilo { get; set; }
        public decimal ImporteCargo { get; set; }

        // Datos del cobro. Quedan vacíos mientras solo se está calculando el cargo.
        public MedioPago_GV42? MedioPago { get; set; }
        public string NumeroTransaccion { get; set; }
        public DateTime? FechaHoraCobro { get; set; }

        public bool TieneExceso { get { return KilosExceso > 0; } }

        #endregion
    }
}
