using System;

namespace BE
{
    // Franquicia de equipaje de un pasajero en el check-in: la de su clase más los kilos que le da
    // el "Equipaje extra" comprado en la reserva (cada unidad suma KgPorUnidad).
    public class FranquiciaEquipaje_GV42
    {
        #region Propiedades

        public decimal FranquiciaClaseKg { get; set; }
        public decimal KgPorUnidadExtra { get; set; }

        // Unidades compradas para este tramo que todavía no usaron otros pasajeros de la reserva.
        public int UnidadesExtraDisponibles { get; set; }

        // Tope de unidades que puede usar un pasajero (MaxPorPasajero del servicio).
        public int UnidadesExtraMaxPasajero { get; set; }

        public int UnidadesExtraAplicables { get { return Math.Max(0, Math.Min(UnidadesExtraDisponibles, UnidadesExtraMaxPasajero)); } }

        public decimal FranquiciaMaximaKg { get { return FranquiciaClaseKg + UnidadesExtraAplicables * KgPorUnidadExtra; } }

        public decimal CostoKiloExceso { get; set; }

        #endregion
    }
}
