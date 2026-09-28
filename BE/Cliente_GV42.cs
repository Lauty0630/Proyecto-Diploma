using System;

namespace BE
{
    // Persona que contrata la reserva (RFN 1).
    public class Cliente_GV42 : Persona_GV42
    {
        public Cliente_GV42() { }

        public Cliente_GV42(string dni, string nombre, string apellido, string email, string telefono)
            : base(dni, nombre, apellido, email, telefono) { }
    }
}
