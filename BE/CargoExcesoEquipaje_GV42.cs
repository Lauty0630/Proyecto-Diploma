using System;

namespace BE
{
    public class CargoExcesoEquipaje_GV42
    {
        public decimal KilosExceso { get; set; }
        public decimal CostoPorKilo { get; set; }
        public decimal ImporteCargo { get; set; }

        // Datos del cobro. Quedan vacíos mientras solo se está calculando el cargo.
        public MedioPago_GV42? MedioPago { get; set; }
        public string NumeroTransaccion { get; set; }
        public DateTime? FechaHoraCobro { get; set; }

        public bool TieneExceso { get { return KilosExceso > 0; } }
    }
}
