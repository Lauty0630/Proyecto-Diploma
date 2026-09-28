using System;
using System.Collections.Generic;

namespace BE
{
    public class Equipaje_GV42
    {
        public int Id { get; set; }
        public int IdCheckIn { get; set; }
        public int CantidadBultos { get; set; }
        public decimal PesoTotalKg { get; set; }
        public decimal FranquiciaKg { get; set; }

        // Un código por bulto despachado.
        public List<string> Etiquetas { get; set; } = new List<string>();

        // Null cuando el peso no supera la franquicia.
        public CargoExcesoEquipaje_GV42 CargoExceso { get; set; }
    }
}
