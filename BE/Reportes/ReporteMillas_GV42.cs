using System;
using System.Collections.Generic;

namespace BE
{
    // Período que analiza el reporte de millas (hacia atrás desde hoy).
    public enum PeriodoMillas_GV42 { Mensual = 1, Trimestral = 3, Anual = 12 }

    // Categoría del pasajero en el programa de millas (según las millas de los últimos 12 meses).
    public enum CategoriaMillas_GV42 { Bronce = 0, Plata = 1, Oro = 2, Diamante = 3 }

    // Cómo viene viajando el pasajero: últimos 3 meses comparados con los 9 meses anteriores.
    public enum TendenciaMillas_GV42 { SinActividad = 0, EnBaja = 1, Estable = 2, EnAumento = 3 }

    // Probabilidad de que el pasajero deje de volar con la aerolínea (según su propio ritmo de viaje).
    public enum RiesgoAbandono_GV42 { Bajo = 0, Medio = 1, Alto = 2 }

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

        // Millas vigentes: las de los vuelos que todavía no vencieron (MESES_VIGENCIA).
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

        // Si no vuelve a volar: fecha en que baja de categoría porque sus vuelos salen de la ventana de
        // 12 meses, y la categoría a la que baja. Null si ya está en Bronce.
        public DateTime? FechaBajaCategoria { get; set; }
        public CategoriaMillas_GV42? CategoriaTrasBaja { get; set; }

        #endregion

        #region Predicción de comportamiento

        public TendenciaMillas_GV42 Tendencia { get; set; }

        // Viajes (ida y vuelta cuentan como uno) y días promedio entre viaje y viaje. 0 si viajó una sola vez.
        public int Viajes { get; set; }
        public int IntervaloPromedioDias { get; set; }
        public int DiasSinVolar { get; set; }

        // Último viaje + intervalo promedio. Null si viajó una sola vez.
        public DateTime? FechaProximoViaje { get; set; }
        public RiesgoAbandono_GV42 Riesgo { get; set; }

        // Texto armado por la BLL en el idioma actual.
        public string Recomendacion { get; set; }

        #endregion
    }
}
