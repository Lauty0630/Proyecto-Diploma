using DAL;
using Servicios.Instalacion;
using System.Collections.Generic;

namespace BLL
{
    public static class BLLInstalador_GV42
    {
        // Días hacia adelante para los que siempre tiene que haber vuelos a la venta.
        public const int DIAS_VUELOS_DISPONIBLES = 30;

        public static string NombreBD
        {
            get { return InstaladorBD_GV42.NOMBRE_BD; }
        }

        public static bool ExisteBaseDatos(string instancia)
        {
            return InstaladorBD_GV42.ExisteBaseDatos(instancia);
        }

        public static void InstalarBaseDatos(string instancia)
        {
            InstaladorBD_GV42.InstalarBaseDatos(instancia);
        }

        // Tablas/columnas que faltan: si hay alguna, la base es demasiado vieja para actualizarla.
        public static List<string> ObjetosFaltantes(string instancia)
        {
            return InstaladorBD_GV42.ObjetosFaltantes(instancia);
        }

        public static bool NecesitaActualizacion(string instancia)
        {
            return InstaladorBD_GV42.ObtenerVersion(instancia) < InstaladorBD_GV42.VERSION_ACTUAL;
        }

        public static void ActualizarBaseDatos(string instancia)
        {
            InstaladorBD_GV42.ActualizarBaseDatos(instancia);
        }

        public static void ReinstalarBaseDatos(string instancia)
        {
            InstaladorBD_GV42.ReinstalarBaseDatos(instancia);
        }

        // Crea los vuelos que falten para los próximos días (no hace nada si ya están).
        public static int AsegurarVuelosDisponibles(string instancia)
        {
            return InstaladorBD_GV42.GenerarVuelosFaltantes(instancia, DIAS_VUELOS_DISPONIBLES);
        }

        public static void ConfigurarConexion(string instancia)
        {
            Acceso.InstanciaActual = instancia;
            Acceso.ConnectionString = ConfiguracionBD_GV42.ArmarConnectionString(
                instancia, InstaladorBD_GV42.NOMBRE_BD);
        }
    }
}
