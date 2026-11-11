using BE;
using BLL;
using Microsoft.Win32;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Instalador
{
    // Qué hacer con la base de datos durante la instalación (lo que se elige en la pantalla "Datos iniciales").
    public enum OpcionBase_GV42
    {
        CrearSoloUsuarios = 0,       // no hay base: crearla solo con la gestión de usuarios
        CrearCompleta = 1,           // no hay base: crearla con todo (gestión de usuarios + negocio)
        AgregarNegocio = 2,          // hay una base solo de usuarios: agrega el negocio sin tocar los datos existentes
        Mantener = 3,                // hay una base completa: se conserva (se actualiza si hace falta)
        ReinstalarSoloUsuarios = 4,  // borrarla y crearla de nuevo solo con la gestión de usuarios
        ReinstalarCompleta = 5,      // borrarla y crearla de nuevo con todo
        RestaurarBackup = 6          // dejarla como estaba en un archivo .bak
    }

    // Qué hay en la instancia elegida.
    public enum EstadoBase_GV42
    {
        SinBase,       // todavía no existe la base "Gestion Usuario"
        SoloUsuarios,  // existe y tiene solo la gestión de usuarios
        Completa,      // existe y tiene también el negocio
        Antigua        // existe pero es de una versión muy anterior: no se puede actualizar
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

        // Nombre de la base del sistema (el mismo que usan los scripts).
        private const string NOMBRE_BD = "Gestion Usuario";

        // Usuario administrador con el que se puede ingresar apenas se instala (se muestra en la pantalla
        // "Datos iniciales"). Es uno de los administradores que ya vienen en EsquemaBase.sql y EsquemaCompleto.sql.
        public const string USUARIO_INICIAL = "lautaro212";
        public const string CLAVE_INICIAL = "lautaro212";

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

        // Qué hay en la instancia: lo usa la pantalla para ofrecer solo las opciones que corresponden.
        // Si no se puede conectar a la instancia lanza una excepción con el motivo.
        public EstadoBase_GV42 EstadoBase(string instancia)
        {
            if (!BLLInstalador_GV42.ExisteBaseDatos(instancia)) return EstadoBase_GV42.SinBase;
            if (BLLInstalador_GV42.EsBaseUsuarios(instancia)) return EstadoBase_GV42.SoloUsuarios;
            return BLLInstalador_GV42.ObjetosFaltantes(instancia).Count > 0 ? EstadoBase_GV42.Antigua : EstadoBase_GV42.Completa;
        }

        // Botón "Probar conexión": devuelve un texto para mostrar (o lanza una excepción si no conecta).
        public string ProbarConexion(string instancia)
        {
            switch (EstadoBase(instancia))
            {
                case EstadoBase_GV42.SinBase:
                    return "Conexión correcta. Todavía no existe la base de datos del sistema: se va a crear.";
                case EstadoBase_GV42.SoloUsuarios:
                    return "Conexión correcta. Ya existe la base con la gestión de usuarios (sin el negocio).";
                case EstadoBase_GV42.Completa:
                    return "Conexión correcta. Ya existe la base completa del sistema.";
                default:
                    return "Conexión correcta. Existe una base de una versión muy anterior.";
            }
        }

        // Texto corto de cada opción, para el resumen de "Listo para instalar".
        public static string TextoOpcion(OpcionBase_GV42 opcion)
        {
            switch (opcion)
            {
                case OpcionBase_GV42.CrearSoloUsuarios: return "Base solo con gestión de usuarios";
                case OpcionBase_GV42.CrearCompleta: return "Base completa (gestión de usuarios + negocio)";
                case OpcionBase_GV42.AgregarNegocio: return "Actualizar: agregar el negocio conservando los datos existentes";
                case OpcionBase_GV42.Mantener: return "Mantener la base actual";
                case OpcionBase_GV42.ReinstalarSoloUsuarios: return "Reinstalar solo con gestión de usuarios (se borran los datos)";
                case OpcionBase_GV42.ReinstalarCompleta: return "Reinstalar completa (se borran los datos)";
                default: return "Restaurar un backup";
            }
        }

        // Las opciones que borran la base actual necesitan la autorización de un administrador.
        public static bool BorraDatos(OpcionBase_GV42 opcion)
        {
            return opcion == OpcionBase_GV42.ReinstalarSoloUsuarios ||
                   opcion == OpcionBase_GV42.ReinstalarCompleta ||
                   opcion == OpcionBase_GV42.RestaurarBackup;
        }

        // Espacio que ocupan los archivos del sistema (en MB), para el texto de la primera pantalla.
        public double TamanoOrigenMB()
        {
            try
            {
                long bytes = 0;
                if (Directory.Exists(CarpetaOrigen))
                    foreach (string archivo in Directory.GetFiles(CarpetaOrigen, "*", SearchOption.AllDirectories))
                        bytes += new FileInfo(archivo).Length;
                return bytes / 1048576.0;
            }
            catch { return 0; }
        }

        #endregion

        #region Validaciones

        // Pantalla "Carpeta de destino".
        public void ValidarCarpeta(string carpetaDestino)
        {
            if (!Directory.Exists(CarpetaOrigen) || !File.Exists(Path.Combine(CarpetaOrigen, EJECUTABLE)))
                throw new NegocioException_GV42("No se encontraron los archivos del sistema. La carpeta \"" + CARPETA_ORIGEN +
                                                "\" tiene que estar junto al instalador.");
            if (string.IsNullOrWhiteSpace(carpetaDestino) || !Path.IsPathRooted(carpetaDestino))
                throw new NegocioException_GV42("Indicá la carpeta donde se va a instalar el sistema.");
            if (MismaCarpeta(carpetaDestino, CarpetaOrigen) || EstaDentro(carpetaDestino, CarpetaOrigen))
                throw new NegocioException_GV42("La carpeta de destino no puede ser la carpeta del instalador.");
        }

        // Pantalla "Base de datos".
        public void ValidarInstancia(string instancia)
        {
            if (string.IsNullOrWhiteSpace(instancia))
                throw new NegocioException_GV42("Elegí la instancia de SQL Server donde va a estar la base de datos.");
        }

        // Lanza NegocioException_GV42 con el motivo si el pedido no se puede ejecutar.
        public void Validar(PedidoInstalacion_GV42 p)
        {
            if (p == null)
                throw new NegocioException_GV42("Faltan los datos de la instalación.");
            ValidarCarpeta(p.CarpetaDestino);
            ValidarInstancia(p.Instancia);

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
            // Solo las opciones que borran la base actual piden autorización (crear, mantener y agregar el negocio no).
            if (!BorraDatos(p.OpcionBase)) return false;
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

            switch (p.OpcionBase)
            {
                case OpcionBase_GV42.RestaurarBackup:
                    avisar("Restaurando el backup (puede tardar)...");
                    BLLInstalador_GV42.ConfigurarConexion(instancia);
                    new BLLReinstalador_GV42().RestaurarBackup(p.RutaBackup);
                    return;

                case OpcionBase_GV42.ReinstalarCompleta:
                    avisar("Reinstalando la base de datos completa con los datos iniciales (puede tardar varios minutos)...");
                    BLLInstalador_GV42.ConfigurarConexion(instancia);
                    new BLLReinstalador_GV42().ReinstalarLimpia();
                    return;

                case OpcionBase_GV42.ReinstalarSoloUsuarios:
                    avisar("Borrando la base de datos actual...");
                    BorrarBase(instancia);
                    avisar("Creando la base de datos con la gestión de usuarios (puede tardar unos minutos)...");
                    BLLInstalador_GV42.InstalarBaseUsuarios(instancia);
                    break;

                case OpcionBase_GV42.CrearSoloUsuarios:
                    avisar("Creando la base de datos con la gestión de usuarios (puede tardar unos minutos)...");
                    BLLInstalador_GV42.InstalarBaseUsuarios(instancia);
                    break;

                case OpcionBase_GV42.CrearCompleta:
                    avisar("Creando la base de datos completa (puede tardar varios minutos)...");
                    BLLInstalador_GV42.InstalarBaseDatos(instancia);
                    break;

                case OpcionBase_GV42.AgregarNegocio:
                    // Actualización: la base tiene que existir. Agrega las tablas y datos del negocio
                    // en una sola transacción, sin modificar lo que ya hay.
                    if (!BLLInstalador_GV42.ExisteBaseDatos(instancia))
                        throw new NegocioException_GV42("No hay una base de datos en esa instancia para actualizar.");
                    avisar("Agregando el negocio a la base (conserva los datos existentes)...");
                    BLLInstalador_GV42.AgregarNegocio(instancia);
                    break;

                default: // Mantener
                    if (BLLInstalador_GV42.ObjetosFaltantes(instancia).Count > 0)
                        throw new NegocioException_GV42("La base de datos existente es de una versión muy anterior y no se puede " +
                                                        "actualizar. Elegí reinstalarla o restaurar un backup.");
                    break;
            }

            if (BLLInstalador_GV42.NecesitaActualizacion(instancia))
            {
                avisar("Actualizando la base de datos a esta versión...");
                BLLInstalador_GV42.ActualizarBaseDatos(instancia);
            }

            avisar("Aplicando las tareas pendientes de la base...");
            BLLInstalador_GV42.EjecutarTareasPendientes(instancia);

            // Los vuelos son parte del negocio: en una base solo de usuarios no se generan.
            if (BLLInstalador_GV42.EsBaseUsuarios(instancia)) return;

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

        // Borra la base del sistema para crearla de nuevo (solo después de la autorización del administrador).
        // Es lo único de SQL que tiene el instalador: la creación de la base la hacen los scripts.
        private static void BorrarBase(string instancia)
        {
            string conexion = "Data Source=" + instancia + ";Initial Catalog=master;Integrated Security=True;Connect Timeout=10";
            using (var conn = new SqlConnection(conexion))
            {
                conn.Open();
                string sql = "IF DB_ID(N'" + NOMBRE_BD + "') IS NOT NULL BEGIN " +
                             "ALTER DATABASE [" + NOMBRE_BD + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                             "DROP DATABASE [" + NOMBRE_BD + "]; END";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
            SqlConnection.ClearAllPools();
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
