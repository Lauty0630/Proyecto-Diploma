using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMMenuPrincipalUsuario : Form, IObservadorIdioma_GV42
    {
        private Form _formularioActual = null;
        private readonly BLLUsuario_GV42 _bllUsuario;

        private ToolStripMenuItem _menuIdioma;
        private ToolStripMenuItem _itemEspanol;
        private ToolStripMenuItem _itemIngles;

        public FRMMenuPrincipalUsuario()
        {
            InitializeComponent();
            _bllUsuario = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            Program.CerrarAplicacionAlSerUltimaVentana(this);

            ConstruirMenuIdioma();
            ActualizarIdioma();
        }

        private void ConstruirMenuIdioma()
        {
            MenuStrip menu = this.Controls.OfType<MenuStrip>().FirstOrDefault();
            if (menu == null) return;

            _itemEspanol = new ToolStripMenuItem("Español");
            _itemEspanol.Click += (s, e) => _bllUsuario.CambiarIdioma(IdiomaManager_GV42.ES);

            _itemIngles = new ToolStripMenuItem("English");
            _itemIngles.Click += (s, e) => _bllUsuario.CambiarIdioma(IdiomaManager_GV42.EN);

            _menuIdioma = new ToolStripMenuItem("Idioma");
            _menuIdioma.DropDownItems.Add(_itemEspanol);
            _menuIdioma.DropDownItems.Add(_itemIngles);

            menu.Items.Add(_menuIdioma);
        }

        public void ActualizarIdioma()
        {
            if (usuarioToolStripMenuItem != null) usuarioToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.usuario");
            if (cambiarClaveToolStripMenuItem != null) cambiarClaveToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.cambiarClave");
            if (logOutToolStripMenuItem != null) logOutToolStripMenuItem.Text = IdiomaManager_GV42.T("menu.logout");

            if (_menuIdioma != null) _menuIdioma.Text = IdiomaManager_GV42.T("menu.idioma");
            if (_itemEspanol != null) _itemEspanol.Text = IdiomaManager_GV42.T("general.espanol");
            if (_itemIngles != null) _itemIngles.Text = IdiomaManager_GV42.T("general.ingles");
        }

        private void FRMMenuPrincipalUsuario_Load(object sender, EventArgs e)
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual != null)
                lblUsuarioActual.Text = $"Sesión: {actual.Nombre} {actual.Apellido} ({actual.Login})";
        }

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

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(f);
            f.Show();
            _formularioActual = f;
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
    }
}
