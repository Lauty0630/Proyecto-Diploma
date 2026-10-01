using System;
using System.Collections.Generic;

namespace BE
{
    public class Equipaje_GV42
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdCheckIn { get; set; }
        public int CantidadBultos { get; set; }
        public decimal PesoTotalKg { get; set; }
        // Franquicia total aplicada: la de la clase más los kilos de equipaje extra usados.
        public decimal FranquiciaKg { get; set; }

        // Unidades de "Equipaje extra" (compradas en la reserva) que usó este pasajero.
        public int UnidadesExtra { get; set; }

        // Un código por bulto despachado.
        public List<string> Etiquetas { get; set; } = new List<string>();

        // Null cuando el peso no supera la franquicia.
        public CargoExcesoEquipaje_GV42 CargoExceso { get; set; }

        #endregion
    }
}
