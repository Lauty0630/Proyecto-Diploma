using System;

namespace BE
{
    public class Aeropuerto_GV42
    {
        public int Id { get; set; }
        public string CodigoIata { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }

        public string Descripcion
        {
            get { return Ciudad + " (" + CodigoIata + ")"; }
        }

        public override string ToString() { return Descripcion; }
    }
}
