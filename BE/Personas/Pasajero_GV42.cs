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

        #region Propiedades

        // Null en pasajeros registrados antes de que se pidiera la fecha (se tratan como adultos).
        public DateTime? FechaNacimiento { get; set; }

        // Datos del pasajero DENTRO de una reserva (no son de la persona): el tipo según la edad que
        // tiene el día del vuelo de ida y la asistencia especial que pidió para ese viaje.
        public TipoPasajero_GV42 Tipo { get; set; } = TipoPasajero_GV42.Adulto;
        public AsistenciaEspecial_GV42 Asistencia { get; set; } = AsistenciaEspecial_GV42.Ninguna;

        public bool EsInfante { get { return Tipo == TipoPasajero_GV42.Infante; } }

        #endregion

        #region Métodos públicos

        public const int EDAD_MINIMA_ADULTO = 12;
        public const int EDAD_MINIMA_NINO = 2;

        // Edad en años cumplidos en la fecha indicada (null si no se conoce la fecha de nacimiento).
        public int? EdadEn(DateTime fecha)
        {
            if (!FechaNacimiento.HasValue) return null;
            DateTime nac = FechaNacimiento.Value.Date;
            int edad = fecha.Year - nac.Year;
            if (fecha.Date < nac.AddYears(edad)) edad--;
            return edad;
        }

        // Tipo de pasajero según la edad que tiene el día del vuelo. Sin fecha de nacimiento: adulto.
        public TipoPasajero_GV42 TipoEn(DateTime fechaVuelo)
        {
            int? edad = EdadEn(fechaVuelo);
            if (!edad.HasValue || edad.Value >= EDAD_MINIMA_ADULTO) return TipoPasajero_GV42.Adulto;
            return edad.Value >= EDAD_MINIMA_NINO ? TipoPasajero_GV42.Nino : TipoPasajero_GV42.Infante;
        }

        #endregion
    }
}
