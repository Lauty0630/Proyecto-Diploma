using BE;
using Servicios;
using System;
using System.Windows.Forms;

namespace Instalador
{
    // Programa instalador de FLY SAFE (3ra entrega, 2da versión del instalador).
    // Es un ejecutable aparte del sistema: se entrega junto a la carpeta "Sistema", que tiene los
    // archivos del programa ya compilados.
    //  - Si FLY SAFE no está instalado en la computadora, funciona como INSTALADOR.
    //  - Si ya está instalado, funciona como REINSTALADOR.
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Los mensajes de las reglas de negocio (BLL) salen de los archivos de idioma.
            Textos_GV42.Traductor = IdiomaManager_GV42.TConDefecto;
            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.IDIOMA_POR_DEFECTO);

            Application.Run(new FRMInstalador_GV42());
        }
    }
}
