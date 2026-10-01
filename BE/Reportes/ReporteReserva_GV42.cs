using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Fila del reporte asociado al RFN 1 (Reserva de vuelo): consolida en un solo objeto los datos
    // de la reserva, su pasajero titular, el vuelo, los adicionales y los importes.
    public class ReporteReserva_GV42
    {
        #region Propiedades

        public int IdReserva { get; set; }
        public string NumeroReserva { get; set; }
        public DateTime FechaRealizacion { get; set; }

        // Pasajero titular de la reserva (el cliente que la realizó).
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroDni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public string CodigoVuelo { get; set; }
        public string OrigenIata { get; set; }
        public string OrigenCiudad { get; set; }
        public string DestinoIata { get; set; }
        public string DestinoCiudad { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }

        public ClaseVuelo_GV42 Clase { get; set; }
        public int CantidadPasajeros { get; set; }

        public List<AdicionalReporte_GV42> Adicionales { get; set; } = new List<AdicionalReporte_GV42>();

        public decimal ImporteBase { get; set; }
        public decimal SubtotalAdicionales { get; set; }
        public decimal Impuestos { get; set; }
        public decimal ImporteTotal { get; set; }
        public EstadoReserva_GV42 Estado { get; set; }

        // Penalidad retenida si la reserva se canceló (0 si no corresponde).
        public decimal MontoPenalidad { get; set; }

        public string PasajeroNombreCompleto => (PasajeroNombre + " " + PasajeroApellido).Trim();
        public string Origen => OrigenIata + " - " + OrigenCiudad;
        public string Destino => DestinoIata + " - " + DestinoCiudad;
        public string ClaseTexto => Clase.Texto();
        public string EstadoTexto => Estado.Texto();

        #endregion
    }

    // Servicio adicional incluido en una reserva (tipo, cantidad y costo).
    public class AdicionalReporte_GV42
    {
        #region Propiedades

        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Subtotal { get; set; }

        #endregion
    }

    // Por qué fecha se filtra el reporte.
    public enum FechaReporte_GV42 { Realizacion = 1, Salida = 2 }

    // Filtros del reporte. Todos son opcionales: sin filtros se listan todas las reservas.
    public class FiltroReporteReservas_GV42
    {
        #region Propiedades

        public FechaReporte_GV42 TipoFecha { get; set; } = FechaReporte_GV42.Realizacion;
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string CodigoVuelo { get; set; }
        public ClaseVuelo_GV42? Clase { get; set; }
        public EstadoReserva_GV42? Estado { get; set; }

        // DNI, nombre o apellido de cualquiera de los pasajeros de la reserva.
        public string Pasajero { get; set; }

        public bool FiltraPorFecha => FechaDesde.HasValue || FechaHasta.HasValue;

        #endregion

        #region Métodos públicos

        // Texto legible de los filtros aplicados (para el subtítulo del PDF y la bitácora).
        public string Descripcion()
        {
            var partes = new List<string>();
            if (FiltraPorFecha)
            {
                string cual = TipoFecha == FechaReporte_GV42.Salida ? "Salida" : "Fecha de reserva";
                string desde = FechaDesde.HasValue ? FechaDesde.Value.ToString("dd/MM/yyyy") : "...";
                string hasta = FechaHasta.HasValue ? FechaHasta.Value.ToString("dd/MM/yyyy") : "...";
                partes.Add(cual + ": " + desde + " al " + hasta);
            }
            if (!string.IsNullOrWhiteSpace(CodigoVuelo)) partes.Add("Vuelo: " + CodigoVuelo.Trim());
            if (Clase.HasValue) partes.Add("Clase: " + Clase.Value.Texto());
            if (Estado.HasValue) partes.Add("Estado: " + Estado.Value.Texto());
            if (!string.IsNullOrWhiteSpace(Pasajero)) partes.Add("Pasajero: " + Pasajero.Trim());
            return partes.Count == 0 ? "Sin filtros" : string.Join(" | ", partes);
        }

        #endregion
    }
}
