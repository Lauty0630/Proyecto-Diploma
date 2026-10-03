using System;

namespace BE
{
    // Cliente del negocio: la persona registrada en el sistema que contrata reservas y/o viaja.
    // Se persiste en la tabla Pasajero (la tabla de personas del negocio: Reserva.DniCliente y
    // ReservaPasajero.DniPasajero apuntan a ella). Es el maestro que mantiene "Maestro de clientes".
    public class Cliente_GV42 : Persona_GV42
    {
        #region Constructor

        public Cliente_GV42() { }

        public Cliente_GV42(string dni, string nombre, string apellido, string email, string telefono)
            : base(dni, nombre, apellido, email, telefono) { }

        #endregion
    }
}
