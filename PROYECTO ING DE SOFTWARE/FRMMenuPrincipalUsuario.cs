using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMMenuPrincipalUsuario : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private Form _formularioActual = null;
        private readonly BLLUsuario_GV42 _bllUsuario;

        #endregion

        #region Constructor

        public FRMMenuPrincipalUsuario()
        {
            InitializeComponent();
            _bllUsuario = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            Program.CerrarAplicacionAlSerUltimaVentana(this);

            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("menu.tituloUsuario");

            usuarioToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuario");
            cambiarClaveToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.cambiarClave");
            logOutToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.logout");

            idiomaToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.idioma");
            espanolToolStripMenuItem.Text = IdiomaManager_GV42.T("general.espanol");
            inglesToolStripMenuItem.Text = IdiomaManager_GV42.T("general.ingles");
            espanolToolStripMenuItem.Checked = !IdiomaManager_GV42.Instancia.EsIngles;
            inglesToolStripMenuItem.Checked = IdiomaManager_GV42.Instancia.EsIngles;

            lblAyudaBienvenida.Text = IdiomaManager_GV42.T("menu.bienvenidaAyuda");
            lblAccesos.Text = IdiomaManager_GV42.T("menu.accesosRapidos");
            btnAccesoCambiarClave.Text = IdiomaManager_GV42.T("menu.cambiarClave");
            btnAccesoCerrarSesion.Text = IdiomaManager_GV42.T("menu.tituloLogout");

            ActualizarBarraEstado();
            ActualizarBienvenida();
        }

        #endregion

        #region Barra de estado y bienvenida

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
        }

        // La tarjeta de bienvenida se ve solo cuando no hay ningún formulario hijo abierto.
        private void MostrarBienvenidaSiNoHayHijo(Form cerrado)
        {
            if (IsDisposed || Disposing) return;
            bool hayHijo = pnlContenido.Controls.OfType<Form>().Any(f => f != cerrado && !f.IsDisposed);
            pnlBienvenida.Visible = !hayHijo;
        }

        #endregion

        #region Navegación

        private void AbrirFormularioHijo(Form f)
        {
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

        private void FRMMenuPrincipalUsuario_Load(object sender, EventArgs e)
        {
            ActualizarBarraEstado();
            ActualizarBienvenida();
        }

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bllUsuario.CambiarIdioma(IdiomaManager_GV42.ES);
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bllUsuario.CambiarIdioma(IdiomaManager_GV42.EN);
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
                FRMIniciarSesion frm = new FRMIniciarSesion();
                frm.Show();
                this.Close();
            }
        }

        #endregion
    }
}
