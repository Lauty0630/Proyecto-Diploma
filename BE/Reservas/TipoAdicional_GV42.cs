using System;

namespace BE
{
    // Catálogo de servicios adicionales: equipaje extra, asiento preferencial, comida especial, etc.
    public class TipoAdicional_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }

        // Precio de lista por unidad. En autogestión se cobra siempre este precio;
        // el vendedor lo ve precargado y puede ajustarlo.
        public decimal PrecioUnitario { get; set; }

        // Cuántas unidades puede pedir cada pasajero por tramo (ida / vuelta). Ej.: 1 comida especial
        // por pasajero; 2 equipajes extra por pasajero. El tope de la reserva es
        // MaxPorPasajero x pasajeros x tramos.
        public int MaxPorPasajero { get; set; } = 1;

        // Código fijo para los servicios que el sistema usa en sus reglas (null en el resto).
        public string Codigo { get; set; }

        // false: el servicio no se elige a mano en la reserva (lo agrega el sistema, como el recargo
        // por butaca preferencial).
        public bool SeleccionManual { get; set; } = true;

        public const string CODIGO_EQUIPAJE_EXTRA = "EQUIPAJE_EXTRA";
        public const string CODIGO_ASIENTO_PREFERENCIAL = "ASIENTO_PREFERENCIAL";

        public bool EsEquipajeExtra { get { return Codigo == CODIGO_EQUIPAJE_EXTRA; } }
        public bool EsAsientoPreferencial { get { return Codigo == CODIGO_ASIENTO_PREFERENCIAL; } }

        #endregion

        #region Métodos públicos

        public override string ToString() { return Nombre ?? string.Empty; }

        #endregion
    }
}
