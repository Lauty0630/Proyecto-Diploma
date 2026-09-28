using System;

namespace BE
{
    public class Aerolinea_GV42
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public override string ToString() { return Nombre ?? string.Empty; }
    }
}
