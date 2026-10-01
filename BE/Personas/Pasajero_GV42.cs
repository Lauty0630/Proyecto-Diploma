using System;

namespace BE
{
    // Persona registrada en el sistema para reservar y/o viajar (tabla Pasajero, PK = DNI).
    // El cliente que contrata una reserva y los que viajan son la misma entidad: si el cliente
    // también viaja, se guarda una sola vez.
    public class Pasajero_GV42 : Persona_GV42
    {
        #region Constructor

        public Pasajero_GV42() { }

        public Pasajero_GV42(string dni, string nombre, string apellido, string email, string telefono)
            : base(dni, nombre, apellido, email, telefono) { }

        #endregion
    }
}
