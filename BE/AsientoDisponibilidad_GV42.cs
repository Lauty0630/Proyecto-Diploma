using System;

namespace BE
{
    // Un asiento del mapa de selección (estilo cine): además de sus datos, si ya está ocupado.
    // Pensado para pintar la grilla Fila x Letra en la pantalla de selección de asiento.
    public class AsientoDisponibilidad_GV42
    {
        public Asiento_GV42 Asiento { get; set; }
        public bool Ocupado { get; set; }

        public int Fila { get; set; }
        public string Letra { get; set; }

        public string NumeroAsiento { get { return Asiento != null ? Asiento.NumeroAsiento : string.Empty; } }
        public string Ubicacion { get { return Asiento != null ? Asiento.Ubicacion : string.Empty; } }
    }
}
