using System;
using System.Collections.Generic;

namespace BE
{
    // Período que analiza el reporte de millas (hacia atrás desde hoy).
    public enum PeriodoMillas_GV42 { Mensual = 1, Trimestral = 3, Anual = 12 }

    // Categoría del pasajero en el programa de millas (según las millas de los últimos 12 meses).
    public enum CategoriaMillas_GV42 { Bronce = 0, Plata = 1, Oro = 2, Diamante = 3 }

    public class FiltroReporteMillas_GV42
    {
        #region Propiedades

        public PeriodoMillas_GV42 Periodo { get; set; } = PeriodoMillas_GV42.Trimestral;
        public CategoriaMillas_GV42? Categoria { get; set; }

        // Nombre, apellido o DNI (contiene).
        public string Pasajero { get; set; }

        #endregion
    }

    // Un vuelo que el pasajero ya voló (check-in realizado): la base para calcular sus millas.
    public class VueloVolado_GV42
    {
        #region Propiedades

        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public string CodigoVuelo { get; set; }
        public DateTime Fecha { get; set; }
        public string OrigenIata { get; set; }
        public string OrigenCiudad { get; set; }
        public string OrigenPais { get; set; }
        public string DestinoIata { get; set; }
        public string DestinoCiudad { get; set; }
        public string DestinoPais { get; set; }
        public ClaseVuelo_GV42 Clase { get; set; }

        // Las calcula la BLL (regla de acumulación).
        public int Millas { get; set; }

        public string Ruta { get { return OrigenCiudad + " - " + DestinoCiudad; } }

        #endregion
    }

    // Perfil de fidelización de un pasajero: una fila del reporte inteligente de millas.
    public class ReporteMillas_GV42
    {
        #region Datos del pasajero

        public string NumeroCuenta { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public CategoriaMillas_GV42 Categoria { get; set; }
        public DateTime FechaIngreso { get; set; }

        public string NombreCompleto { get { return (Apellido + ", " + Nombre).Trim(' ', ','); } }

        #endregion

        #region Acumulación de millas

        public int MillasTotales { get; set; }
        public int MillasUltimoVuelo { get; set; }
        public int MillasPeriodo { get; set; }
        public int MillasUltimos12Meses { get; set; }

        // Null cuando ya está en la categoría más alta.
        public CategoriaMillas_GV42? CategoriaSiguiente { get; set; }
        public int MillasParaSiguiente { get; set; }

        #endregion

        #region Historial y comportamiento

        public int VuelosPeriodo { get; set; }
        public int VuelosTotales { get; set; }
        public List<string> RutasFrecuentes { get; set; } = new List<string>();
        public ClaseVuelo_GV42 ClaseHabitual { get; set; }
        public decimal VuelosPorMes { get; set; }
        public VueloVolado_GV42 UltimoVuelo { get; set; }

        #endregion

        #region Proyección y oferta

        public int ProyeccionMillas90Dias { get; set; }

        // Null si no se puede estimar (sin ritmo de viaje o ya en la categoría más alta).
        public DateTime? FechaEstimadaSiguiente { get; set; }
        public int MillasVencen30Dias { get; set; }
        public int MillasVencen60Dias { get; set; }

        // Texto armado por la BLL en el idioma actual.
        public string Recomendacion { get; set; }

        #endregion
    }
}
