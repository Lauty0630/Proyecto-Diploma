using BLL;
using Servicios;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    internal static class Program
    {
        private const string INSTANCIA_DEBUG_DEFAULT = @"(localdb)\MSSQLLocalDB";

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

            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.IDIOMA_POR_DEFECTO);

            if (!ConfigurarConexionBD()) return;

            Application.Run(new FRMIniciarSesion());
        }

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
                        using (var aviso = CrearAviso("Instalando la base de datos por primera vez...\nPuede tardar unos minutos."))
                            BLLInstalador_GV42.InstalarBaseDatos(instancia);
                        if (!PrepararBaseDatos(instancia)) return false;
                        BLLInstalador_GV42.ConfigurarConexion(instancia);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo instalar la base de datos en " + instancia + ".\n\n" + ex.Message +
                                        "\n\nElegí la instancia de SQL Server en la siguiente pantalla.",
                                        "Instalación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private static void MostrarErrorNoControlado(Exception ex)
        {
            try
            {
                string detalle = ex == null ? "Error desconocido." :
                    (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                MessageBox.Show("Ocurrió un error inesperado. La operación no se completó.\n\n" + detalle,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
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
                    DialogResult r = MessageBox.Show(
                        "La base de datos \"" + BLLInstalador_GV42.NombreBD + "\" de esta computadora es de una versión " +
                        "anterior del sistema y no se puede actualizar (le falta: " + string.Join(", ", faltan.Take(5)) +
                        (faltan.Count > 5 ? "..." : "") + ").\n\n" +
                        "¿Querés reemplazarla por la base de esta versión?\n" +
                        "Se pierden los datos cargados en esta computadora (usuarios, reservas, etc.).",
                        "Base de datos desactualizada", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (r != DialogResult.Yes) return false;

                    using (CrearAviso("Reinstalando la base de datos...\nPuede tardar unos minutos."))
                        BLLInstalador_GV42.ReinstalarBaseDatos(instancia);
                }
                else if (BLLInstalador_GV42.NecesitaActualizacion(instancia))
                {
                    try
                    {
                        using (CrearAviso("Actualizando la base de datos a la versión actual...\nLos datos existentes se conservan."))
                            BLLInstalador_GV42.ActualizarBaseDatos(instancia);
                    }
                    catch (Exception ex)
                    {
                        DialogResult r = MessageBox.Show(
                            "No se pudo actualizar la base de datos:\n" + ex.Message + "\n\n" +
                            "¿Querés reinstalarla? Se pierden los datos cargados en esta computadora.",
                            "Actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (r != DialogResult.Yes) return false;
                        using (CrearAviso("Reinstalando la base de datos...\nPuede tardar unos minutos."))
                            BLLInstalador_GV42.ReinstalarBaseDatos(instancia);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo preparar la base de datos.\n\n" + ex.Message, "Error",
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
                MessageBox.Show("No se pudieron completar las tareas de actualización de la base.\n\n" + ex.Message,
                                "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Vuelos: si falla no se bloquea el ingreso (el resto del sistema funciona igual).
            try
            {
                using (CrearAviso("Actualizando los vuelos disponibles..."))
                    BLLInstalador_GV42.AsegurarVuelosDisponibles(instancia);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron generar los vuelos de los próximos días.\n\n" + ex.Message,
                                "Vuelos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        // Ventanita de "espere" mientras se trabaja con la base (se cierra con Dispose).
        private static Form CrearAviso(string texto)
        {
            var aviso = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ControlBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                ClientSize = new Size(420, 110),
                Text = "Gestión de reservas",
                BackColor = Tema_GV42.Fondo,
                ShowInTaskbar = true,
                TopMost = true
            };
            aviso.Controls.Add(new Label
            {
                Text = texto,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = Tema_GV42.FuenteTexto,
                ForeColor = Tema_GV42.Acento
            });
            aviso.Show();
            aviso.Refresh();
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            return aviso;
        }
    }
}
