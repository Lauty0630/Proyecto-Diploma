using BE;
using BLL;
using Microsoft.Win32;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Instalador
{
    // Qué hacer con la base de datos durante la instalación.
    public enum OpcionBase_GV42
    {
        Preparar = 0,        // crearla si no existe, o actualizarla conservando los datos
        RestaurarBackup = 1, // dejarla como estaba en un archivo .bak
        ReinstalarLimpia = 2 // borrarla y crearla de nuevo con los datos iniciales
    }

    // Datos que carga el operador en la pantalla del instalador.
    public class PedidoInstalacion_GV42
    {
        public string CarpetaDestino { get; set; }
        public string Instancia { get; set; }
        public OpcionBase_GV42 OpcionBase { get; set; }
        public string RutaBackup { get; set; }
        public bool CrearAccesoDirecto { get; set; }
        public string LoginAdministrador { get; set; }
        public string ContrasenaAdministrador { get; set; }
    }

    // Lógica del instalador: copia los archivos del sistema, prepara la base de datos, guarda la
    // configuración, crea el acceso directo y registra la instalación en Windows.
    // Para la base de datos reutiliza las mismas clases que usa el sistema (BLLInstalador_GV42 y
    // BLLReinstalador_GV42): el instalador no tiene SQL propio.
    public class InstaladorSistema_GV42
    {
        #region Constantes

        public const string NOMBRE_SISTEMA = "FLY SAFE";
        public const string CARPETA_ORIGEN = "Sistema";
        public const string EJECUTABLE = "PROYECTO ING DE SOFTWARE.exe";

        // Dónde queda anotado que el sistema está instalado (por usuario de Windows: no pide
        // permisos de administrador del equipo).
        private const string CLAVE_REGISTRO = @"Software\FLYSAFE_GV42";
        private const string VALOR_CARPETA = "CarpetaInstalacion";
        private const string VALOR_FECHA = "FechaInstalacion";
        private const string VALOR_INSTANCIA = "Instancia";

        #endregion

        #region Estado de la instalación

        // Carpeta de los archivos a instalar: está junto al instalador.
        public string CarpetaOrigen
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CARPETA_ORIGEN); }
        }

        public string CarpetaDestinoSugerida
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                    "Programs", NOMBRE_SISTEMA);
            }
        }

        // Carpeta donde está instalado el sistema, o null si no está instalado. Se considera
        // instalado si quedó anotado en el registro y el ejecutable sigue en esa carpeta.
        public string CarpetaInstalada()
        {
            using (RegistryKey clave = Registry.CurrentUser.OpenSubKey(CLAVE_REGISTRO))
            {
                string carpeta = clave != null ? clave.GetValue(VALOR_CARPETA) as string : null;
                if (string.IsNullOrWhiteSpace(carpeta)) return null;
                return File.Exists(Path.Combine(carpeta, EJECUTABLE)) ? carpeta : null;
            }
        }

        public bool EstaInstalado() { return CarpetaInstalada() != null; }

        public string InstanciaInstalada()
        {
            using (RegistryKey clave = Registry.CurrentUser.OpenSubKey(CLAVE_REGISTRO))
            {
                string instancia = clave != null ? clave.GetValue(VALOR_INSTANCIA) as string : null;
                return string.IsNullOrWhiteSpace(instancia) ? ConfiguracionBD_GV42.LeerInstanciaGuardada() : instancia;
            }
        }

        public List<string> DetectarInstancias()
        {
            return DetectorInstancias_GV42.DetectarInstancias();
        }

        #endregion

        #region Validaciones

        // Lanza NegocioException_GV42 con el motivo si el pedido no se puede ejecutar.
        public void Validar(PedidoInstalacion_GV42 p)
        {
            if (p == null)
                throw new NegocioException_GV42("Faltan los datos de la instalación.");
            if (!Directory.Exists(CarpetaOrigen) || !File.Exists(Path.Combine(CarpetaOrigen, EJECUTABLE)))
                throw new NegocioException_GV42("No se encontraron los archivos del sistema. La carpeta \"" + CARPETA_ORIGEN +
                                                "\" tiene que estar junto al instalador.");
            if (string.IsNullOrWhiteSpace(p.CarpetaDestino) || !Path.IsPathRooted(p.CarpetaDestino))
                throw new NegocioException_GV42("Indicá la carpeta donde se va a instalar el sistema.");
            if (MismaCarpeta(p.CarpetaDestino, CarpetaOrigen) || EstaDentro(p.CarpetaDestino, CarpetaOrigen))
                throw new NegocioException_GV42("La carpeta de destino no puede ser la carpeta del instalador.");
            if (string.IsNullOrWhiteSpace(p.Instancia))
                throw new NegocioException_GV42("Elegí la instancia de SQL Server donde va a estar la base de datos.");

            if (p.OpcionBase == OpcionBase_GV42.RestaurarBackup)
            {
                if (string.IsNullOrWhiteSpace(p.RutaBackup) || !File.Exists(p.RutaBackup))
                    throw new NegocioException_GV42("Elegí el archivo de backup (.bak) que se va a restaurar.");
                if (!string.Equals(Path.GetExtension(p.RutaBackup), ".bak", StringComparison.OrdinalIgnoreCase))
                    throw new NegocioException_GV42("El archivo de backup tiene que ser un .bak.");
            }
        }

        // Restaurar un backup o reinstalar la base limpia borra datos: si ya hay una base que se
        // puede leer, lo tiene que autorizar un usuario administrador del sistema.
        public bool RequiereAdministrador(PedidoInstalacion_GV42 p)
        {
            if (p.OpcionBase == OpcionBase_GV42.Preparar) return false;
            BLLInstalador_GV42.ConfigurarConexion(p.Instancia);
            return new BLLReinstalador_GV42().BaseAccesible();
        }

        #endregion

        #region Instalación

        // Ejecuta la instalación completa. 'avisar' recibe el texto de cada paso para mostrarlo.
        public void Instalar(PedidoInstalacion_GV42 p, Action<string> avisar)
        {
            avisar = avisar ?? (t => { });
            Validar(p);

            // 1) Autorización (solo si se van a perder datos de una base que existe).
            if (RequiereAdministrador(p))
            {
                avisar("Verificando el usuario administrador...");
                new BLLReinstalador_GV42().ValidarAdministrador(p.LoginAdministrador, p.ContrasenaAdministrador);
            }

            // 2) Archivos del sistema.
            avisar("Copiando los archivos del sistema...");
            int copiados = CopiarCarpeta(CarpetaOrigen, p.CarpetaDestino);
            avisar("Archivos copiados: " + copiados + ".");

            // 3) Base de datos.
            PrepararBase(p, avisar);

            // 4) Configuración: el sistema arranca directamente contra esta instancia.
            ConfiguracionBD_GV42.GuardarInstancia(p.Instancia);

            // 5) Acceso directo en el escritorio.
            if (p.CrearAccesoDirecto)
            {
                avisar("Creando el acceso directo en el escritorio...");
                CrearAccesoDirecto(p.CarpetaDestino);
            }

            // 6) Registro de la instalación (lo que después permite detectar que ya está instalado).
            using (RegistryKey clave = Registry.CurrentUser.CreateSubKey(CLAVE_REGISTRO))
            {
                clave.SetValue(VALOR_CARPETA, p.CarpetaDestino);
                clave.SetValue(VALOR_INSTANCIA, p.Instancia);
                clave.SetValue(VALOR_FECHA, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            avisar("Listo. " + NOMBRE_SISTEMA + " quedó instalado en " + p.CarpetaDestino);
        }

        private void PrepararBase(PedidoInstalacion_GV42 p, Action<string> avisar)
        {
            string instancia = p.Instancia;

            if (p.OpcionBase == OpcionBase_GV42.RestaurarBackup)
            {
                avisar("Restaurando el backup (puede tardar)...");
                BLLInstalador_GV42.ConfigurarConexion(instancia);
                new BLLReinstalador_GV42().RestaurarBackup(p.RutaBackup);
                return;
            }

            if (p.OpcionBase == OpcionBase_GV42.ReinstalarLimpia)
            {
                avisar("Reinstalando la base de datos con los datos iniciales (puede tardar varios minutos)...");
                BLLInstalador_GV42.ConfigurarConexion(instancia);
                new BLLReinstalador_GV42().ReinstalarLimpia();
                return;
            }

            // Preparar: crear la base si no existe, o actualizarla conservando los datos.
            if (!BLLInstalador_GV42.ExisteBaseDatos(instancia))
            {
                avisar("Creando la base de datos (puede tardar varios minutos)...");
                BLLInstalador_GV42.InstalarBaseDatos(instancia);
            }
            else
            {
                List<string> faltan = BLLInstalador_GV42.ObjetosFaltantes(instancia);
                if (faltan.Count > 0)
                    throw new NegocioException_GV42("La base de datos existente es de una versión muy anterior y no se puede " +
                                                    "actualizar. Elegí \"Reinstalar la base limpia\" o \"Restaurar un backup\".");
            }

            if (BLLInstalador_GV42.NecesitaActualizacion(instancia))
            {
                avisar("Actualizando la base de datos a esta versión...");
                BLLInstalador_GV42.ActualizarBaseDatos(instancia);
            }

            avisar("Aplicando las tareas pendientes de la base...");
            BLLInstalador_GV42.EjecutarTareasPendientes(instancia);

            // Si falla no se interrumpe la instalación: el sistema lo vuelve a intentar al arrancar.
            try
            {
                avisar("Generando los vuelos disponibles...");
                BLLInstalador_GV42.AsegurarVuelosDisponibles(instancia);
            }
            catch (Exception ex)
            {
                avisar("No se pudieron generar los vuelos ahora (" + ex.Message + "). Se generan al abrir el sistema.");
            }
        }

        #endregion

        #region Archivos, acceso directo

        // Copia la carpeta completa (con subcarpetas), pisando los archivos que ya existan.
        // Devuelve cuántos archivos copió.
        private static int CopiarCarpeta(string origen, string destino)
        {
            Directory.CreateDirectory(destino);
            int cantidad = 0;

            foreach (string archivo in Directory.GetFiles(origen))
            {
                File.Copy(archivo, Path.Combine(destino, Path.GetFileName(archivo)), true);
                cantidad++;
            }
            foreach (string subcarpeta in Directory.GetDirectories(origen))
                cantidad += CopiarCarpeta(subcarpeta, Path.Combine(destino, Path.GetFileName(subcarpeta)));

            return cantidad;
        }

        // El acceso directo (.lnk) se crea con el componente de Windows "WScript.Shell".
        // Se usa por reflexión para no agregar una referencia COM al proyecto.
        private static void CrearAccesoDirecto(string carpetaInstalacion)
        {
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string rutaLnk = Path.Combine(escritorio, NOMBRE_SISTEMA + ".lnk");

            Type tipo = Type.GetTypeFromProgID("WScript.Shell");
            if (tipo == null) return;

            object shell = Activator.CreateInstance(tipo);
            object acceso = tipo.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, shell, new object[] { rutaLnk });
            Type tipoAcceso = acceso.GetType();
            tipoAcceso.InvokeMember("TargetPath", BindingFlags.SetProperty, null, acceso,
                                    new object[] { Path.Combine(carpetaInstalacion, EJECUTABLE) });
            tipoAcceso.InvokeMember("WorkingDirectory", BindingFlags.SetProperty, null, acceso, new object[] { carpetaInstalacion });
            tipoAcceso.InvokeMember("Description", BindingFlags.SetProperty, null, acceso, new object[] { NOMBRE_SISTEMA });
            tipoAcceso.InvokeMember("Save", BindingFlags.InvokeMethod, null, acceso, null);
        }

        private static string Normalizar(string ruta)
        {
            return Path.GetFullPath(ruta).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        private static bool MismaCarpeta(string a, string b)
        {
            return string.Equals(Normalizar(a), Normalizar(b), StringComparison.OrdinalIgnoreCase);
        }

        // ¿'ruta' está dentro de 'carpeta'?
        private static bool EstaDentro(string ruta, string carpeta)
        {
            return Normalizar(ruta).StartsWith(Normalizar(carpeta) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
