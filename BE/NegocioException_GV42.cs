using System;

namespace BE
{
    // Se lanza cuando se viola una regla de negocio (dato inválido, estado incorrecto, sin disponibilidad, etc.).
    // La UI puede mostrar ex.Message directamente al usuario.
    public class NegocioException_GV42 : Exception
    {
        public NegocioException_GV42(string mensaje) : base(mensaje) { }
        public NegocioException_GV42(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
