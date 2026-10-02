using System;

namespace BE
{
    // Familia tarifaria (Light / Plus / Top). La reserva se vende con una tarifa, que define el precio
    // (recargo sobre el precio base del vuelo y la clase), las valijas despachadas incluidas, si elegir
    // asiento es pago y las condiciones de cambio y de reembolso. Igual que en las aerolíneas.
    public class TarifaFamilia_GV42
    {
        #region Constantes

        // AsientoIncluido
        public const int ASIENTO_PAGO = 0;              // elegir asiento se cobra; si no, se asigna en el check-in
        public const int ASIENTO_COMUN_GRATIS = 1;      // elegir asiento común es gratis (el preferencial lleva recargo)
        public const int ASIENTO_CUALQUIERA_GRATIS = 2; // incluye las butacas preferenciales

        // TipoReembolso
        public const int REEMBOLSO_NO = 0;              // no reembolsable
        public const int REEMBOLSO_CON_PENALIDAD = 1;   // penalidad según la anticipación con que se cancela
        public const int REEMBOLSO_TOTAL = 2;           // se devuelve todo

        #endregion

        #region Propiedades

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        // Porcentaje que se suma al precio base del vuelo y la clase (0 = precio base).
        public decimal PorcentajeRecargo { get; set; }
        // Valijas despachadas incluidas por pasajero y por tramo.
        public int ValijasIncluidas { get; set; }
        public int AsientoIncluido { get; set; }
        // Penalidad por cambiar de vuelo: porcentaje de la tarifa del tramo que se cambia.
        public decimal PorcentajePenalidadCambio { get; set; }
        public int TipoReembolso { get; set; }

        public bool ElegirAsientoEsPago { get { return AsientoIncluido == ASIENTO_PAGO; } }
        public bool IncluyePreferencial { get { return AsientoIncluido == ASIENTO_CUALQUIERA_GRATIS; } }

        #endregion

        #region Métodos públicos

        // Precio por pasajero adulto de un tramo con esta tarifa.
        public decimal PrecioPara(decimal precioBase)
        {
            return Math.Round(precioBase * (1 + PorcentajeRecargo / 100m), 2);
        }

        public override string ToString() { return Nombre ?? string.Empty; }

        #endregion
    }
}
