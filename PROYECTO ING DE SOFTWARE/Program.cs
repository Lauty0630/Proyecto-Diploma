using BLL;
using Servicios;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    internal static class Program
    {
        #region Campos

        private const string INSTANCIA_DEBUG_DEFAULT = @"(localdb)\MSSQLLocalDB";

        #endregion

        #region Punto de entrada

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Red de seguridad: un error no previsto en cualquier pantalla muestra un mensaje claro
            // y la aplicación sigue funcionando (antes aparecía el diálogo de .NET y se cerraba).
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => MostrarErrorNoControlado(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => MostrarErrorNoControlado(e.ExceptionObject as Exception);

            // El idioma se carga antes de cualquier mensaje o pantalla (instalación, login).
            BE.Textos_GV42.Traductor = IdiomaManager_GV42.TConDefecto;
            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.IDIOMA_POR_DEFECTO);

            if (!ConfigurarConexionBD()) return;

            Application.Run(new FRMIniciarSesion());
        }

        #endregion

        #region Ventanas

        // El login es el formulario principal de Application.Run, pero después de loguearse queda
        // oculto (Hide) y la navegación sigue en otros formularios. Si el usuario cerraba el menú
        // con la X, el proceso quedaba vivo en segundo plano con el login invisible.
        // Cada formulario "raíz" llama a esto: al cerrarse, si no queda ninguna otra ventana
        // visible, se termina la aplicación.
        internal static void CerrarAplicacionAlSerUltimaVentana(Form formulario)
        {
            formulario.FormClosed += (s, e) =>
            {
                if (!formulario.TopLevel) return; // formularios embebidos en el panel del menú

                bool quedanVentanas = Application.OpenForms
                    .Cast<Form>()
                    .Any(f => f != formulario && f.TopLevel && f.Visible && !f.IsDisposed);

                if (!quedanVentanas)
                    Application.Exit();
            };
        }

        // Ventanita de "espere" mientras se trabaja con la base (se cierra con Dispose).
        // El diseño está en FRMAviso_GV42; acá solo se indica la clave del mensaje.
        private static Form CrearAviso(string claveMensaje)
        {
            return FRMAviso_GV42.Mostrar(claveMensaje);
        }

        private static void MostrarErrorNoControlado(Exception ex)
        {
            try
            {
                // TConDefecto: si el error ocurre antes de cargar el idioma, igual se ve un texto claro.
                string detalle = ex == null
                    ? IdiomaManager_GV42.TConDefecto("instalacion.errorDesconocido", "Error desconocido.")
                    : (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                string mensaje = IdiomaManager_GV42.TConDefecto("instalacion.errorInesperado",
                    "Ocurrió un error inesperado. La operación no se completó.\n\n{0}");
                MessageBox.Show(string.Format(mensaje, detalle),
                                IdiomaManager_GV42.TConDefecto("general.error", "Error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }

        #endregion

        #region Base de datos

        private static bool ConfigurarConexionBD()
        {
            string instancia = ConfiguracionBD_GV42.LeerInstanciaGuardada();

#if DEBUG
            if (string.IsNullOrEmpty(instancia))
                instancia = INSTANCIA_DEBUG_DEFAULT;
#endif

            if (!string.IsNullOrEmpty(instancia))
            {
                try
                {
                    if (BLLInstalador_GV42.ExisteBaseDatos(instancia))
                    {
                        if (!PrepararBaseDatos(instancia)) return false;
                        BLLInstalador_GV42.ConfigurarConexion(instancia);
                        return true;
                    }

#if DEBUG
                    try
                    {
                        using (var aviso = CrearAviso("instalacion.avisoInstalando"))
                            BLLInstalador_GV42.InstalarBaseDatos(instancia);
                        if (!PrepararBaseDatos(instancia)) return false;
                        BLLInstalador_GV42.ConfigurarConexion(instancia);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(IdiomaManager_GV42.T("instalacion.errorInstalarEn", instancia, ex.Message),
                                        IdiomaManager_GV42.T("instalacion.tituloInstalacion"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
#endif
                }
                catch
                {
                }
            }

            using (var frm = new FRMSeleccionInstancia())
            {
                DialogResult r = frm.ShowDialog();
                if (r != DialogResult.OK || string.IsNullOrEmpty(frm.InstanciaElegida))
                    return false;

                if (!PrepararBaseDatos(frm.InstanciaElegida)) return false;
                BLLInstalador_GV42.ConfigurarConexion(frm.InstanciaElegida);
                return true;
            }
        }

        // Deja la base lista para esta versión del sistema, en cualquier computadora:
        //  1) Si es demasiado vieja (le faltan tablas del negocio) ofrece reinstalarla.
        //  2) Si es de una versión anterior, la actualiza conservando los datos.
        //  3) Genera los vuelos que falten para los próximos días (nunca queda sin vuelos).
        private static bool PrepararBaseDatos(string instancia)
        {
            try
            {
                List<string> faltan = BLLInstalador_GV42.ObjetosFaltantes(instancia);
                if (faltan.Count > 0)
                {
                    string listaFaltantes = string.Join(", ", faltan.Take(5)) + (faltan.Count > 5 ? "..." : "");
                    DialogResult r = MessageBox.Show(
                        IdiomaManager_GV42.T("instalacion.baseViejaMensaje", BLLInstalador_GV42.NombreBD, listaFaltantes),
                        IdiomaManager_GV42.T("instalacion.baseViejaTitulo"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (r != DialogResult.Yes) return false;

                    using (CrearAviso("instalacion.avisoReinstalando"))
                        BLLInstalador_GV42.ReinstalarBaseDatos(instancia);
                }
                else if (BLLInstalador_GV42.NecesitaActualizacion(instancia))
                {
                    try
                    {
                        using (CrearAviso("instalacion.avisoActualizando"))
                            BLLInstalador_GV42.ActualizarBaseDatos(instancia);
                    }
                    catch (Exception ex)
                    {
                        DialogResult r = MessageBox.Show(
                            IdiomaManager_GV42.T("instalacion.errorActualizar", ex.Message),
                            IdiomaManager_GV42.T("instalacion.tituloActualizacion"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (r != DialogResult.Yes) return false;
                        using (CrearAviso("instalacion.avisoReinstalando"))
                            BLLInstalador_GV42.ReinstalarBaseDatos(instancia);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("instalacion.errorPreparar", ex.Message),
                                IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Tareas que dejó la actualización (por ejemplo, recalcular dígitos verificadores cuya fórmula cambió).
            try
            {
                BLLInstalador_GV42.EjecutarTareasPendientes(instancia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("instalacion.errorTareas", ex.Message),
                                IdiomaManager_GV42.T("instalacion.tituloActualizacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Vuelos: si falla no se bloquea el ingreso (el resto del sistema funciona igual).
            try
            {
                using (CrearAviso("instalacion.avisoVuelos"))
                    BLLInstalador_GV42.AsegurarVuelosDisponibles(instancia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("instalacion.errorVuelos", ex.Message),
                                IdiomaManager_GV42.T("instalacion.tituloVuelos"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        #endregion
    }
}
