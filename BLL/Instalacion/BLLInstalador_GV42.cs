using DAL;
using Servicios.Instalacion;
using System.Collections.Generic;

namespace BLL
{
    public static class BLLInstalador_GV42
    {
        #region Constantes

        // Días hacia adelante para los que siempre tiene que haber vuelos a la venta.
        public const int DIAS_VUELOS_DISPONIBLES = 30;

        #endregion

        #region Propiedades

        public static string NombreBD
        {
            get { return InstaladorBD_GV42.NOMBRE_BD; }
        }

        #endregion

        #region Instalación y actualización de la base

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

        #endregion

        #region Tareas de mantenimiento

        // Crea los vuelos que falten para los próximos días (no hace nada si ya están).
        public static int AsegurarVuelosDisponibles(string instancia)
        {
            return InstaladorBD_GV42.GenerarVuelosFaltantes(instancia, DIAS_VUELOS_DISPONIBLES);
        }

        // Ejecuta las tareas pendientes que dejó la actualización de la base (una sola vez).
        public static void EjecutarTareasPendientes(string instancia)
        {
            List<string> tareas = InstaladorBD_GV42.TareasPendientes(instancia);
            if (tareas.Count == 0) return;

            ConfigurarConexion(instancia);
            var integridad = new BLLIntegridad_GV42();
            foreach (string tarea in tareas)
            {
                const string RECALCULAR = "RecalcularDV:";
                if (tarea.StartsWith(RECALCULAR))
                    integridad.RecalcularTabla(tarea.Substring(RECALCULAR.Length));
                InstaladorBD_GV42.QuitarTarea(instancia, tarea);
            }
        }

        #endregion

        #region Conexión

        public static void ConfigurarConexion(string instancia)
        {
            Acceso.InstanciaActual = instancia;
            Acceso.ConnectionString = ConfiguracionBD_GV42.ArmarConnectionString(
                instancia, InstaladorBD_GV42.NOMBRE_BD);
        }

        #endregion
    }
}
