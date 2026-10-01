using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMMenuPrincipalAdmin : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private Form _formularioActual = null;
        private readonly BLLUsuario_GV42 _bllUsuario;

        // true cuando el propio menú abre el login (logout / re-login) antes de cerrarse.
        private bool _cerrandoSesion = false;

        // true cuando el rol solo puede ver sus propias reservas (el acceso rápido dice "Mis reservas").
        private bool _soloReservasPropias = false;

        #endregion

        #region Constructor

        public FRMMenuPrincipalAdmin()
        {
            InitializeComponent();
            _bllUsuario = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            this.FormClosing += FRMMenuPrincipalAdmin_FormClosing;
            Program.CerrarAplicacionAlSerUltimaVentana(this);

            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("menu.tituloAdmin");

            adminToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.admin");
            usuariosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuarios");
            bitacoraToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.bitacora");
            gestionDePermisosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.gestionPermisos");
            backupToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.backup");

            reservasToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.reservas");
            nuevaReservaToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.nuevaReserva");
            registrarPagoToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.registrarPago");
            consultarReservasToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.consultarReservas");
            misReservasToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.misReservas");

            vuelosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.vuelos");
            gestionVuelosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.gestionVuelos");
            bitacoraVuelosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.bitacoraVuelos");

            reportesToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.reportes");
            reporteReservasToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.reporteReservas");

            usuarioToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuario");
            reLoginToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.relogin");
            cambiarClaveToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.cambiarClave");
            logOutToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.logout");

            idiomaToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.idioma");
            espanolToolStripMenuItem.Text = IdiomaManager_GV42.T("general.espanol");
            inglesToolStripMenuItem.Text = IdiomaManager_GV42.T("general.ingles");
            espanolToolStripMenuItem.Checked = !IdiomaManager_GV42.Instancia.EsIngles;
            inglesToolStripMenuItem.Checked = IdiomaManager_GV42.Instancia.EsIngles;

            lblAyudaBienvenida.Text = IdiomaManager_GV42.T("menu.bienvenidaAyuda");
            lblAccesos.Text = IdiomaManager_GV42.T("menu.accesosRapidos");
            btnAccesoNuevaReserva.Text = IdiomaManager_GV42.T("menu.nuevaReserva");
            btnAccesoCambiarClave.Text = IdiomaManager_GV42.T("menu.cambiarClave");

            ActualizarBarraEstado();
            ActualizarBienvenida();
        }

        #endregion

        #region Barra de estado y bienvenida

        // Textos que dependen del usuario en sesión: se regeneran al cargar y al cambiar el idioma.
        private void ActualizarBarraEstado()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();

            lblUsuarioActual.Text = actual != null
                ? IdiomaManager_GV42.T("menu.estadoSesion", actual.Nombre, actual.Apellido, actual.Login)
                : IdiomaManager_GV42.T("general.sesionNoIniciada");

            bool tieneRol = actual != null && !string.IsNullOrEmpty(actual.RolNombre);
            lblRolActual.Visible = tieneRol;
            if (tieneRol) lblRolActual.Text = IdiomaManager_GV42.T("menu.estadoRol", actual.RolNombre);

            lblIdiomaActual.Text = IdiomaManager_GV42.T("menu.estadoIdioma",
                IdiomaManager_GV42.Instancia.EsIngles ? IdiomaManager_GV42.T("general.ingles") : IdiomaManager_GV42.T("general.espanol"));
            lblFechaActual.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void ActualizarBienvenida()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();

            lblSaludo.Text = actual != null && !string.IsNullOrEmpty(actual.Nombre)
                ? IdiomaManager_GV42.T("menu.bienvenidaSaludo", actual.Nombre)
                : IdiomaManager_GV42.T("menu.bienvenida");

            bool tieneRol = actual != null && !string.IsNullOrEmpty(actual.RolNombre);
            lblRolBienvenida.Text = tieneRol ? IdiomaManager_GV42.T("menu.bienvenidaRol", actual.RolNombre) : "";

            btnAccesoReservas.Text = _soloReservasPropias
                ? IdiomaManager_GV42.T("menu.misReservas")
                : IdiomaManager_GV42.T("menu.consultarReservas");
        }

        // La tarjeta de bienvenida se ve solo cuando no hay ningún formulario hijo abierto.
        private void MostrarBienvenidaSiNoHayHijo(Form cerrado)
        {
            if (IsDisposed || Disposing) return;
            bool hayHijo = pnlContenido.Controls.OfType<Form>().Any(f => f != cerrado && !f.IsDisposed);
            pnlBienvenida.Visible = !hayHijo;
        }

        #endregion

        #region Permisos

        private void AplicarPermisosMenu()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            var bllPermisos = new BLLPermisos_GV42();
            Rol_GV42 rolCompleto = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
            if (rolCompleto == null) return;

            var dataKeys = rolCompleto.ObtenerPatentes()
                .Select(p => p.DataKey ?? string.Empty)
                .ToList();

            usuariosToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Usuarios."));
            bitacoraToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Bitacora."));
            gestionDePermisosToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Permisos."));
            backupToolStripMenuItem.Visible =
                dataKeys.Contains("Backup.Crear") ||
                dataKeys.Contains("Integridad.Restore");

            bool puedeCambiarClave = dataKeys.Contains("Sesion.CambiarClave");
            bool puedeReLogin = dataKeys.Contains("Sesion.ReLogin");
            bool puedeLogout = dataKeys.Contains("Sesion.Logout");

            cambiarClaveToolStripMenuItem.Visible = puedeCambiarClave;
            cambiarClaveToolStripMenuItem.Available = puedeCambiarClave;

            reLoginToolStripMenuItem.Visible = puedeReLogin;
            reLoginToolStripMenuItem.Available = puedeReLogin;

            logOutToolStripMenuItem.Visible = puedeLogout;
            logOutToolStripMenuItem.Available = puedeLogout;

            usuarioToolStripMenuItem.Visible = puedeCambiarClave || puedeReLogin || puedeLogout;

            bool puedeGenerarReserva = dataKeys.Contains("Reservas.Generar") || dataKeys.Contains("Reservas.GenerarPropia");
            bool puedeRegistrarPago = dataKeys.Contains("Pagos.Registrar") || dataKeys.Contains("Pagos.RegistrarPropio");

            bool puedeConsultarTodas = dataKeys.Contains("Reservas.Consultar");
            bool puedeConsultarPropias = dataKeys.Contains("Reservas.ConsultarPropia");

            nuevaReservaToolStripMenuItem.Visible = puedeGenerarReserva;
            registrarPagoToolStripMenuItem.Visible = puedeRegistrarPago;
            consultarReservasToolStripMenuItem.Visible = puedeConsultarTodas;
            misReservasToolStripMenuItem.Visible = puedeConsultarPropias && !puedeConsultarTodas;
            reservasToolStripMenuItem.Visible = puedeGenerarReserva || puedeRegistrarPago || puedeConsultarTodas || puedeConsultarPropias;

            bool puedeGestionarVuelos = dataKeys.Contains("Vuelos.Gestionar");
            bool puedeVerBitacoraVuelos = dataKeys.Contains("Vuelos.Bitacora");
            gestionVuelosToolStripMenuItem.Visible = puedeGestionarVuelos;
            bitacoraVuelosToolStripMenuItem.Visible = puedeVerBitacoraVuelos;
            vuelosToolStripMenuItem.Visible = puedeGestionarVuelos || puedeVerBitacoraVuelos;

            bool puedeVerReporteReservas = dataKeys.Contains("Reportes.Reservas");
            reporteReservasToolStripMenuItem.Visible = puedeVerReporteReservas;
            reportesToolStripMenuItem.Visible = puedeVerReporteReservas;

            bool puedeCambiarIdioma = dataKeys.Contains("Sesion.CambiarIdioma");
            idiomaToolStripMenuItem.Visible = puedeCambiarIdioma;

            // Accesos rápidos de la tarjeta de bienvenida: mismas patentes que el menú.
            btnAccesoNuevaReserva.Visible = puedeGenerarReserva;
            btnAccesoReservas.Visible = puedeConsultarTodas || puedeConsultarPropias;
            btnAccesoCambiarClave.Visible = puedeCambiarClave;
            _soloReservasPropias = puedeConsultarPropias && !puedeConsultarTodas;
            lblAccesos.Visible = puedeGenerarReserva || puedeConsultarTodas || puedeConsultarPropias || puedeCambiarClave;
            ActualizarBienvenida();
        }

        #endregion

        #region Navegación

        public void AbrirFormularioHijo(Form f)
        {
            if (_formularioActual != null && _formularioActual.IsDisposed)
                _formularioActual = null;

            if (_formularioActual != null && _formularioActual.GetType() == f.GetType())
            {
                _formularioActual.Close();
                _formularioActual = null;
                return;
            }

            if (_formularioActual != null)
            {
                _formularioActual.Close();
                _formularioActual = null;
            }

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;

            // Se quitan los hijos anteriores (la tarjeta de bienvenida queda, solo se oculta).
            foreach (Form anterior in pnlContenido.Controls.OfType<Form>().ToList())
                pnlContenido.Controls.Remove(anterior);

            pnlBienvenida.Visible = false;
            f.FormClosed += (s, e) => MostrarBienvenidaSiNoHayHijo(f);
            pnlContenido.Controls.Add(f);
            f.Show();
            _formularioActual = f;
        }

        #endregion

        #region Eventos

        private void FRMMenuPrincipalAdmin_Load(object sender, EventArgs e)
        {
            ActualizarBarraEstado();
            ActualizarBienvenida();
            AplicarPermisosMenu();
        }

        // Si se cierra el menú con la X, se registra el logout en la bitácora y se cierra la sesión.
        private void FRMMenuPrincipalAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cerrandoSesion) return;
            if (e.CloseReason != CloseReason.UserClosing) return;
            if (!SessionManager_GV42.Instancia.HaySesionActiva()) return;
            try { BLLUsuario_GV42.CerrarSesión(); } catch { }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMGestionUsuariosAdmin());
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBitacoraDeEventos());
        }

        private void gestionDePermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMGestionPermisos());
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBackupManual());
        }

        // Negocio (RFN 1): un solo formulario para vendedor y cliente (ver FRMReservarVuelo_GV42);
        // la visibilidad de cada opción depende de las patentes del rol, igual que el resto del menú.
        private void nuevaReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMReservarVuelo_GV42());
        }

        private void registrarPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMPagoReserva_GV42());
        }

        // Antes se podía asignar la patente Reservas.Consultar / Reservas.ConsultarPropia
        // pero no existía ninguna pantalla que las usara. Estos dos ítems las cubren.
        private void consultarReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMConsultarReservas_GV42());
        }

        private void misReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMConsultarReservas_GV42());
        }

        // Vuelos: gestión (modificar / baja lógica) y bitácora de cambios (tabla Vuelo_C).
        // Cada opción se muestra según su patente (Vuelos.Gestionar / Vuelos.Bitacora).
        private void gestionVuelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMGestionVuelos_GV42());
        }

        private void bitacoraVuelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBitacoraVuelos_GV42());
        }

        // Reportes de gestión (rol Gerente). Por ahora: reporte de reservas del RFN 1.
        private void reporteReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMReporteReservas_GV42());
        }

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bllUsuario.CambiarIdioma(IdiomaManager_GV42.ES);
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bllUsuario.CambiarIdioma(IdiomaManager_GV42.EN);
        }

        // Re-Login: cierra la sesión actual y vuelve al login para entrar con otro usuario.
        // Antes abría el login embebido en el panel con la sesión todavía activa, por lo que
        // cualquier intento terminaba en "Ya hay una sesión activa".
        private void reLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { BLLUsuario_GV42.CerrarSesión(); } catch { }
            _cerrandoSesion = true;
            new FRMIniciarSesion().Show();
            this.Close();
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMCambiarContrasenia());
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                IdiomaManager_GV42.T("menu.confirmarLogout"),
                IdiomaManager_GV42.T("menu.tituloLogout"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BLLUsuario_GV42.CerrarSesión();
                _cerrandoSesion = true;
                FRMIniciarSesion frm = new FRMIniciarSesion();
                frm.Show();
                this.Close();
            }
        }

        #endregion
    }
}
