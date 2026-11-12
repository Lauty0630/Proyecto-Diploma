using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;

namespace InstaladorUnico
{
    // Instalador de un solo archivo. Adentro lleva (como recurso) todo lo que necesita el instalador:
    // el instalador en sí, sus bibliotecas, la carpeta "Sistema" y los scripts de la base.
    // Al ejecutarlo descomprime todo en una carpeta temporal, abre el instalador de ahí y, cuando
    // el instalador se cierra, borra la carpeta temporal.
    internal static class Program
    {
        private const string RECURSO = "carga.zip";
        private const string INSTALADOR = "Instalador FLY SAFE.exe";

        [STAThread]
        private static int Main()
        {
            string carpeta = Path.Combine(Path.GetTempPath(), "FLYSAFE_Instalador_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            try
            {
                Directory.CreateDirectory(carpeta);

                using (Stream recurso = Assembly.GetExecutingAssembly().GetManifestResourceStream(RECURSO))
                {
                    if (recurso == null)
                        throw new InvalidOperationException("Este instalador no incluye los archivos del sistema. Hay que recompilarlo en Release.");
                    using (var zip = new ZipArchive(recurso, ZipArchiveMode.Read))
                        zip.ExtractToDirectory(carpeta);
                }

                var inicio = new ProcessStartInfo(Path.Combine(carpeta, INSTALADOR))
                {
                    WorkingDirectory = carpeta,
                    UseShellExecute = false
                };
                using (Process instalador = Process.Start(inicio))
                {
                    instalador.WaitForExit();
                    return instalador.ExitCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar el instalador.\n\n" + ex.Message, "Instalador FLY SAFE",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            finally
            {
                try { Directory.Delete(carpeta, true); } catch { /* si algún archivo sigue en uso, Windows lo limpia después */ }
            }
        }
    }
}
