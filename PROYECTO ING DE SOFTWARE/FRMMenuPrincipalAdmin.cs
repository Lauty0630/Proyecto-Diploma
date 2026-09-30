using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMMenuPrincipalAdmin : Form, IObservadorIdioma_GV42
    {
        private Form _formularioActual = null;
        private readonly BLLUsuario_GV42 _bllUsuario;

        private ToolStripMenuItem _menuIdioma;
        private ToolStripMenuItem _itemEspanol;
        private ToolStripMenuItem _itemIngles;

        private ToolStripMenuItem _menuReservas;
        private ToolStripMenuItem _itemNuevaReserva;
        private ToolStripMenuItem _itemRegistrarPago;
        private ToolStripMenuItem _itemConsultarReservas;
        private ToolStripMenuItem _itemMisReservas;

        // true cuando el propio menú abre el login (logout / re-login) antes de cerrarse.
        private bool _cerrandoSesion = false;

        private ToolStripMenuItem _menuVuelos;
        private ToolStripMenuItem _itemGestionVuelos;
        private ToolStripMenuItem _itemBitacoraVuelos;

        public FRMMenuPrincipalAdmin()
        {
            InitializeComponent();
            _bllUsuario = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            this.FormClosing += FRMMenuPrincipalAdmin_FormClosing;
            Program.CerrarAplicacionAlSerUltimaVentana(this);

            ConstruirMenuReservas();
            ConstruirMenuVuelos();
            ConstruirMenuIdioma();
            ActualizarIdioma();
        }

        // Negocio (RFN 1): un solo formulario para vendedor y cliente (ver FRMReservarVuelo_GV42);
        // la visibilidad de cada opción depende de las patentes del rol, igual que el resto del menú.
        private void ConstruirMenuReservas()
        {
            if (menuStrip1 == null) return;

            _itemNuevaReserva = new ToolStripMenuItem("Nueva reserva");
            _itemNuevaReserva.Click += (s, e) => AbrirFormularioHijo(new FRMReservarVuelo_GV42());

            _itemRegistrarPago = new ToolStripMenuItem("Registrar pago");
            _itemRegistrarPago.Click += (s, e) => AbrirFormularioHijo(new FRMPagoReserva_GV42());

            // Antes se podía asignar la patente Reservas.Consultar / Reservas.ConsultarPropia
            // pero no existía ninguna pantalla que las usara. Estos dos ítems las cubren.
            _itemConsultarReservas = new ToolStripMenuItem("Consultar reservas");
            _itemConsultarReservas.Click += (s, e) => AbrirFormularioHijo(new FRMConsultarReservas_GV42());

            _itemMisReservas = new ToolStripMenuItem("Mis reservas");
            _itemMisReservas.Click += (s, e) => AbrirFormularioHijo(new FRMConsultarReservas_GV42());

            _menuReservas = new ToolStripMenuItem("Reservas");
            _menuReservas.DropDownItems.Add(_itemNuevaReserva);
            _menuReservas.DropDownItems.Add(_itemRegistrarPago);
            _menuReservas.DropDownItems.Add(_itemConsultarReservas);
            _menuReservas.DropDownItems.Add(_itemMisReservas);

            menuStrip1.Items.Add(_menuReservas);
        }

        // Vuelos: gestión (modificar / baja lógica) y bitácora de cambios (tabla Vuelo_C).
        // Cada opción se muestra según su patente (Vuelos.Gestionar / Vuelos.Bitacora).
        private void ConstruirMenuVuelos()
        {
            if (menuStrip1 == null) return;

            _itemGestionVuelos = new ToolStripMenuItem("Gestión de vuelos");
            _itemGestionVuelos.Click += (s, e) => AbrirFormularioHijo(new FRMGestionVuelos_GV42());

            _itemBitacoraVuelos = new ToolStripMenuItem("Bitácora de cambios");
            _itemBitacoraVuelos.Click += (s, e) => AbrirFormularioHijo(new FRMBitacoraVuelos_GV42());

            _menuVuelos = new ToolStripMenuItem("Vuelos");
            _menuVuelos.DropDownItems.Add(_itemGestionVuelos);
            _menuVuelos.DropDownItems.Add(_itemBitacoraVuelos);

            menuStrip1.Items.Add(_menuVuelos);
        }

        private void ConstruirMenuIdioma()
        {
            if (menuStrip1 == null) return;

            _itemEspanol = new ToolStripMenuItem("Español");
            _itemEspanol.Click += (s, e) => _bllUsuario.CambiarIdioma(IdiomaManager_GV42.ES);

            _itemIngles = new ToolStripMenuItem("English");
            _itemIngles.Click += (s, e) => _bllUsuario.CambiarIdioma(IdiomaManager_GV42.EN);

            _menuIdioma = new ToolStripMenuItem("Idioma");
            _menuIdioma.DropDownItems.Add(_itemEspanol);
            _menuIdioma.DropDownItems.Add(_itemIngles);

            menuStrip1.Items.Add(_menuIdioma);
        }

        public void ActualizarIdioma()
        {
            if (adminToolStripMenuItem != null) adminToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.admin");
            if (usuariosToolStripMenuItem != null) usuariosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuarios");
            if (bitacoraToolStripMenuItem != null) bitacoraToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.bitacora");
            if (gestionDePermisosToolStripMenuItem != null) gestionDePermisosToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.gestionPermisos");
            if (backupToolStripMenuItem != null) backupToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.backup");
            if (usuarioToolStripMenuItem != null) usuarioToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuario");
            if (reLoginToolStripMenuItem != null) reLoginToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.relogin");
            if (cambiarClaveToolStripMenuItem != null) cambiarClaveToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.cambiarClave");
            if (logOutToolStripMenuItem != null) logOutToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.logout");

            if (_menuIdioma != null) _menuIdioma.Text = IdiomaManager_GV42.T("menu.idioma");
            if (_itemEspanol != null) _itemEspanol.Text = IdiomaManager_GV42.T("general.espanol");
            if (_itemIngles != null) _itemIngles.Text = IdiomaManager_GV42.T("general.ingles");
        }

        private void FRMMenuPrincipalAdmin_Load(object sender, EventArgs e)
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual != null)
                lblUsuarioActual.Text = $"Sesión: {actual.Nombre} {actual.Apellido} ({actual.Login}) — Rol: {actual.RolNombre}";

            AplicarPermisosMenu();
        }

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

            if (usuariosToolStripMenuItem != null)
                usuariosToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Usuarios."));

            if (bitacoraToolStripMenuItem != null)
                bitacoraToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Bitacora."));

            if (gestionDePermisosToolStripMenuItem != null)
                gestionDePermisosToolStripMenuItem.Visible = dataKeys.Any(k => k.StartsWith("Permisos."));

            if (backupToolStripMenuItem != null)
                backupToolStripMenuItem.Visible =
                    dataKeys.Contains("Backup.Crear") ||
                    dataKeys.Contains("Integridad.Restore");

            bool puedeCambiarClave = dataKeys.Contains("Sesion.CambiarClave");
            bool puedeReLogin      = dataKeys.Contains("Sesion.ReLogin");
            bool puedeLogout       = dataKeys.Contains("Sesion.Logout");

            if (cambiarClaveToolStripMenuItem != null)
            {
                cambiarClaveToolStripMenuItem.Visible = puedeCambiarClave;
                cambiarClaveToolStripMenuItem.Available = puedeCambiarClave;
            }

            if (reLoginToolStripMenuItem != null)
            {
                reLoginToolStripMenuItem.Visible = puedeReLogin;
                reLoginToolStripMenuItem.Available = puedeReLogin;
            }

            if (logOutToolStripMenuItem != null)
            {
                logOutToolStripMenuItem.Visible = puedeLogout;
                logOutToolStripMenuItem.Available = puedeLogout;
            }

            if (usuarioToolStripMenuItem != null)
                usuarioToolStripMenuItem.Visible = puedeCambiarClave || puedeReLogin || puedeLogout;

            bool puedeGenerarReserva = dataKeys.Contains("Reservas.Generar") || dataKeys.Contains("Reservas.GenerarPropia");
            bool puedeRegistrarPago  = dataKeys.Contains("Pagos.Registrar") || dataKeys.Contains("Pagos.RegistrarPropio");

            bool puedeConsultarTodas = dataKeys.Contains("Reservas.Consultar");
            bool puedeConsultarPropias = dataKeys.Contains("Reservas.ConsultarPropia");

            if (_itemNuevaReserva != null) _itemNuevaReserva.Visible = puedeGenerarReserva;
            if (_itemRegistrarPago != null) _itemRegistrarPago.Visible = puedeRegistrarPago;
            if (_itemConsultarReservas != null) _itemConsultarReservas.Visible = puedeConsultarTodas;
            if (_itemMisReservas != null) _itemMisReservas.Visible = puedeConsultarPropias && !puedeConsultarTodas;
            if (_menuReservas != null)
                _menuReservas.Visible = puedeGenerarReserva || puedeRegistrarPago || puedeConsultarTodas || puedeConsultarPropias;

            bool puedeGestionarVuelos = dataKeys.Contains("Vuelos.Gestionar");
            bool puedeVerBitacoraVuelos = dataKeys.Contains("Vuelos.Bitacora");
            if (_itemGestionVuelos != null) _itemGestionVuelos.Visible = puedeGestionarVuelos;
            if (_itemBitacoraVuelos != null) _itemBitacoraVuelos.Visible = puedeVerBitacoraVuelos;
            if (_menuVuelos != null) _menuVuelos.Visible = puedeGestionarVuelos || puedeVerBitacoraVuelos;

            bool puedeCambiarIdioma = dataKeys.Contains("Sesion.CambiarIdioma");
            if (_menuIdioma != null)
                _menuIdioma.Visible = puedeCambiarIdioma;
        }

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

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(f);
            f.Show();
            _formularioActual = f;
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMGestionUsuariosAdmin());
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

        // Si se cierra el menú con la X, se registra el logout en la bitácora y se cierra la sesión.
        private void FRMMenuPrincipalAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cerrandoSesion) return;
            if (e.CloseReason != CloseReason.UserClosing) return;
            if (!SessionManager_GV42.Instancia.HaySesionActiva()) return;
            try { BLLUsuario_GV42.CerrarSesión(); } catch { }
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

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMCambiarContrasenia());
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

    }
}
