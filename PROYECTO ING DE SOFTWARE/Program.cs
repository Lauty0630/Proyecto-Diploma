using BLL;
using Servicios;
using Servicios.Instalacion;
using System;
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
                        BLLInstalador_GV42.ConfigurarConexion(instancia);
                        return true;
                    }

#if DEBUG
                    try
                    {
                        BLLInstalador_GV42.InstalarBaseDatos(instancia);
                        BLLInstalador_GV42.ConfigurarConexion(instancia);
                        return true;
                    }
                    catch { }
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

                BLLInstalador_GV42.ConfigurarConexion(frm.InstanciaElegida);
                return true;
            }
        }
    }
}
