using System;
using System.Collections.Generic;

namespace BE
{
    // Asistencia del pasajero al vuelo. Se calcula por horario (no se registra el embarque):
    //  - Presente: hizo el check-in.
    //  - Ausente: no hizo el check-in y el check-in del vuelo ya cerró.
    //  - AConfirmar: todavía no hizo el check-in pero el vuelo sigue abierto (puede presentarse).
    public enum EstadoAsistencia_GV42 { AConfirmar = 0, Presente = 1, Ausente = 2 }

    // Fila del reporte asociado al RFN 2 (Check-in / Tarjeta de embarque): un pasajero en un vuelo,
    // con el estado de su check-in, su asiento y su tarjeta de embarque.
    public class ReporteCheckIn_GV42
    {
        #region Propiedades

        public int IdCheckIn { get; set; }

        public string CodigoVuelo { get; set; }
        public string OrigenIata { get; set; }
        public string OrigenCiudad { get; set; }
        public string DestinoIata { get; set; }
        public string DestinoCiudad { get; set; }
        public DateTime FechaHoraSalida { get; set; }

        public string NumeroReserva { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroDni { get; set; }

        public EstadoCheckIn_GV42 Estado { get; set; }
        // Lo calcula la BLL según el estado del check-in y el horario del vuelo.
        public EstadoAsistencia_GV42 Asistencia { get; set; }
        public DateTime? FechaHoraCheckIn { get; set; }

        // Asiento: vacío si todavía no tiene (tarifa sin asiento elegido y check-in pendiente).
        public string NumeroAsiento { get; set; }
        public ClaseVuelo_GV42 Clase { get; set; }
        public string Ubicacion { get; set; }

        // Tarjeta de embarque: vacía hasta que se confirma el check-in.
        public string NumeroTarjeta { get; set; }
        public string PuertaEmbarque { get; set; }
        public DateTime HoraLimiteEmbarque { get; set; }

        public string PasajeroNombreCompleto { get { return (PasajeroNombre + " " + PasajeroApellido).Trim(); } }
        public string Origen { get { return OrigenIata + " - " + OrigenCiudad; } }
        public string Destino { get { return DestinoIata + " - " + DestinoCiudad; } }
        public string ClaseTexto { get { return Clase.Texto(); } }
        public string EstadoTexto { get { return Estado.Texto(); } }
        public string AsistenciaTexto { get { return Asistencia.Texto(); } }

        #endregion
    }

    // Filtros del reporte. Todos son opcionales: sin filtros se listan todos los vuelos.
    public class FiltroReporteCheckIn_GV42
    {
        #region Propiedades

        public string CodigoVuelo { get; set; }
        // Fecha de salida del vuelo.
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public EstadoCheckIn_GV42? Estado { get; set; }
        public EstadoAsistencia_GV42? Asistencia { get; set; }
        // DNI, nombre o apellido del pasajero.
        public string Pasajero { get; set; }

        public bool FiltraPorFecha { get { return FechaDesde.HasValue || FechaHasta.HasValue; } }

        #endregion

        #region Métodos públicos

        // Texto de los filtros aplicados, en español (para la bitácora).
        public string Descripcion()
        {
            var partes = new List<string>();
            if (!string.IsNullOrWhiteSpace(CodigoVuelo)) partes.Add("Vuelo: " + CodigoVuelo.Trim());
            if (FiltraPorFecha)
                partes.Add("Salida: " + (FechaDesde.HasValue ? FechaDesde.Value.ToString("dd/MM/yyyy") : "...") +
                           " al " + (FechaHasta.HasValue ? FechaHasta.Value.ToString("dd/MM/yyyy") : "..."));
            if (Estado.HasValue) partes.Add("Check-in: " + Estado.Value);
            if (Asistencia.HasValue) partes.Add("Asistencia: " + Asistencia.Value);
            if (!string.IsNullOrWhiteSpace(Pasajero)) partes.Add("Pasajero: " + Pasajero.Trim());
            return partes.Count == 0 ? "Sin filtros" : string.Join(" | ", partes);
        }

        #endregion
    }
}
