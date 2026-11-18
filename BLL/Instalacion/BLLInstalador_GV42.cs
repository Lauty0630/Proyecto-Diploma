using DAL;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // Sin datos de demostración: solo roles, permisos y el administrador. Con datos de demostración:
        // también aerolíneas y aeropuertos, y vuelos de los próximos días para poder reservar enseguida.
        public static void InstalarBaseDatos(string instancia, bool datosDemostracion = false)
        {
            InstaladorBD_GV42.InstalarBaseDatos(instancia, datosDemostracion);
        }

        // Sistema ejecutado desde el código (zip de Git): crea la base completa y la llena con los datos de
        // DatosDemostracion.sql, para poder probar todo apenas se abre. Si los datos no se pueden cargar,
        // la base queda creada (vacía) y se devuelve el motivo para avisarlo; si todo salió bien, null.
        public static string InstalarBaseDatosConDatosDePrueba(string instancia)
        {
            InstalarBaseDatos(instancia);
            try
            {
                InstaladorBD_GV42.CargarDatosDemostracion(instancia);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Instalación por etapas: crea solo la gestión de usuarios (sin el negocio).
        public static void InstalarBaseUsuarios(string instancia)
        {
            InstaladorBD_GV42.InstalarBaseUsuarios(instancia);
        }

        // True si la base es de gestión de usuarios y todavía no tiene el negocio.
        public static bool EsBaseUsuarios(string instancia)
        {
            return InstaladorBD_GV42.EsBaseUsuarios(instancia);
        }

        // Agrega el negocio a una base de gestión de usuarios sin borrar ni modificar lo que ya tiene.
        public static void AgregarNegocio(string instancia)
        {
            InstaladorBD_GV42.AgregarNegocio(instancia);
        }

        // True si la base a la que está conectado el sistema tiene el negocio instalado.
        public static bool NegocioInstalado()
        {
            return InstaladorBD_GV42.NegocioInstaladoEnConexionActual();
        }

        // Tablas/columnas que faltan: si hay alguna, la base es demasiado vieja para actualizarla.
        public static List<string> ObjetosFaltantes(string instancia)
        {
            return InstaladorBD_GV42.ObjetosFaltantes(instancia);
        }

        // Una base de gestión de usuarios (sin negocio) no se actualiza sola: el negocio se agrega a pedido,
        // con la opción "Actualizar" del instalador.
        public static bool NecesitaActualizacion(string instancia)
        {
            return !InstaladorBD_GV42.EsBaseUsuarios(instancia) &&
                   InstaladorBD_GV42.ObtenerVersion(instancia) < InstaladorBD_GV42.VERSION_ACTUAL;
        }

        public static void ActualizarBaseDatos(string instancia)
        {
            InstaladorBD_GV42.ActualizarBaseDatos(instancia);
        }

        public static void ReinstalarBaseDatos(string instancia, bool datosDemostracion = false)
        {
            InstaladorBD_GV42.ReinstalarBaseDatos(instancia, datosDemostracion);
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
            const string RECALCULAR = "RecalcularDV:";
            // Primero las tareas que cambian datos y al final los recálculos de dígitos verificadores
            // (si no, el DVH quedaría calculado con los datos de antes del cambio).
            foreach (string tarea in tareas.OrderBy(t => t.StartsWith(RECALCULAR) ? 1 : 0))
            {
                if (tarea.StartsWith(RECALCULAR))
                    integridad.RecalcularTabla(tarea.Substring(RECALCULAR.Length));
                else if (tarea == "CifrarEmailsPlanos")
                    InstaladorBD_GV42.CifrarEmailsPlanos(instancia);
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
