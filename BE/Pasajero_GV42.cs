using System;

namespace BE
{
    // Persona que viaja. Puede coincidir o no con el cliente que reserva.
    public class Pasajero_GV42 : Persona_GV42
    {
        public Pasajero_GV42() { }

        public Pasajero_GV42(string dni, string nombre, string apellido, string email, string telefono)
            : base(dni, nombre, apellido, email, telefono) { }
    }
}
