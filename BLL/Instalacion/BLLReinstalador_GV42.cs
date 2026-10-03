using BE;
using DAL;
using Servicios;
using System;
using System.IO;

namespace BLL
{
    // Reinstalador (3ra entrega): se usa desde el login cuando el sistema ya está instalado y hay que
    // recuperar la base. Ofrece dos caminos:
    //  - Restaurar un backup (.bak): la base vuelve al estado de ese backup.
    //  - Reinstalar la base limpia: se borra y se instala de nuevo con los datos iniciales.
    // Si la base se puede leer, exige un usuario con la patente Integridad.Restore (administrador).
    // Si la base no se puede leer (está dañada o no existe) no hay contra qué validar: se permite igual.
    public class BLLReinstalador_GV42
    {
        #region Constantes

        public const string PATENTE = "Integridad.Restore";

        #endregion

        #region Estado de la base

        public string Instancia { get { return Acceso.InstanciaActual; } }

        // True si la base existe, tiene las tablas de esta versión y se pueden leer los usuarios.
        public bool BaseAccesible()
        {
            try
            {
                if (string.IsNullOrEmpty(Instancia)) return false;
                if (!BLLInstalador_GV42.ExisteBaseDatos(Instancia)) return false;
                if (BLLInstalador_GV42.ObjetosFaltantes(Instancia).Count > 0) return false;
                new DALUsuario_GV42().ListarRoles();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Autorización

        // Lanza NegocioException_GV42 si las credenciales no son de un administrador habilitado.
        // No inicia sesión: solo autoriza el uso del reinstalador.
        public void ValidarAdministrador(string login, string contrasena)
        {
            login = (login ?? string.Empty).Trim();
            if (login.Length == 0 || string.IsNullOrEmpty(contrasena))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reinstalador.faltanCredenciales"));

            Usuario_GV42 usuario = new DALUsuario_GV42().BuscarPorLogin(login);
            if (usuario == null || usuario.Bloqueo || !usuario.Activo ||
                usuario.Contrasena != Encriptador_GV42.Instancia.EncriptarContrasena(contrasena))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reinstalador.credencialesInvalidas"));

            Rol_GV42 rol = usuario.Rol != null ? new BLLPermisos_GV42().ObtenerArbolRol(usuario.Rol.Id) : null;
            if (rol == null || !rol.TienePermiso(PATENTE))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reinstalador.sinPermiso"));
        }

        #endregion

        #region Operaciones

        // Restaura el backup y deja la base lista para esta versión del sistema (si el backup es de
        // una versión anterior, la actualiza conservando los datos).
        public void RestaurarBackup(string rutaArchivoBak)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivoBak) || !File.Exists(rutaArchivoBak))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reinstalador.archivoInvalido"));

            new BLLIntegridad_GV42().RestaurarBackupDesdeRuta(rutaArchivoBak);
            DejarLista();
        }

        // Borra la base y la instala de nuevo con los datos iniciales.
        public void ReinstalarLimpia()
        {
            BLLInstalador_GV42.ReinstalarBaseDatos(Instancia);
            DejarLista();
        }

        #endregion

        #region Métodos privados

        private void DejarLista()
        {
            if (BLLInstalador_GV42.NecesitaActualizacion(Instancia))
                BLLInstalador_GV42.ActualizarBaseDatos(Instancia);
            BLLInstalador_GV42.EjecutarTareasPendientes(Instancia);
            try { BLLInstalador_GV42.AsegurarVuelosDisponibles(Instancia); } catch { }
            BLLInstalador_GV42.ConfigurarConexion(Instancia);
        }

        #endregion
    }
}
