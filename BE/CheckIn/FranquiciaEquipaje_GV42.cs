using System;

namespace BE
{
    // Franquicia de equipaje de un pasajero en el check-in. Se cuenta por valija, como en las
    // aerolíneas: la clase incluye una cantidad de valijas de hasta KgPorBultoIncluido cada una y
    // cada "Equipaje extra" comprado en la reserva agrega una valija más de hasta KgPorUnidadExtra.
    // No se pueden despachar más valijas que las permitidas y ninguna puede superar PesoMaximoPorBulto.
    public class FranquiciaEquipaje_GV42
    {
        #region Propiedades

        // Valijas que incluye la tarifa de la clase y kilos permitidos por cada una.
        public int BultosIncluidos { get; set; }
        public decimal KgPorBultoIncluido { get; set; }

        // Kilos permitidos por cada valija de equipaje extra.
        public decimal KgPorUnidadExtra { get; set; }

        // Unidades compradas para este tramo que todavía no usaron otros pasajeros de la reserva.
        public int UnidadesExtraDisponibles { get; set; }

        // Tope de unidades que puede usar un pasajero (MaxPorPasajero del servicio).
        public int UnidadesExtraMaxPasajero { get; set; }

        // Ninguna valija se acepta por encima de este peso (seguridad de quienes las cargan).
        public decimal PesoMaximoPorBulto { get; set; }

        public decimal CostoKiloExceso { get; set; }

        public int UnidadesExtraAplicables { get { return Math.Max(0, Math.Min(UnidadesExtraDisponibles, UnidadesExtraMaxPasajero)); } }

        // Valijas que puede despachar el pasajero.
        public int BultosPermitidos { get { return BultosIncluidos + UnidadesExtraAplicables; } }

        // Kilos incluidos en la clase (todas sus valijas).
        public decimal FranquiciaClaseKg { get { return BultosIncluidos * KgPorBultoIncluido; } }

        // Kilos sin cargo si despacha todas las valijas permitidas.
        public decimal FranquiciaMaximaKg { get { return FranquiciaClaseKg + UnidadesExtraAplicables * KgPorUnidadExtra; } }

        #endregion
    }
}
