using System;

namespace BE
{
    // Catálogo de servicios adicionales: equipaje extra, asiento preferencial, comida especial, etc.
    public class TipoAdicional_GV42
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Precio de lista por unidad. En autogestión se cobra siempre este precio;
        // el vendedor lo ve precargado y puede ajustarlo.
        public decimal PrecioUnitario { get; set; }

        public override string ToString() { return Nombre ?? string.Empty; }
    }
}
