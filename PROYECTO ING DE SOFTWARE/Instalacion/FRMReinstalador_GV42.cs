using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Reinstalador (3ra entrega): se abre desde el login. Con la autorización de un administrador
    // restaura un backup (.bak) o reinstala la base limpia. Al terminar hay que volver a iniciar sesión.
    // El diseño está en FRMReinstalador_GV42.Designer.cs (Form Designer).
    public partial class FRMReinstalador_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReinstalador_GV42 _bll = new BLLReinstalador_GV42();

        // Si la base no se puede leer no hay usuarios contra los que validar: no se piden credenciales.
        private readonly bool _baseAccesible;

        #endregion

        #region Constructor

        public FRMReinstalador_GV42()
        {
            InitializeComponent();
            _baseAccesible = _bll.BaseAccesible();
            lblUsuario.Visible = txtUsuario.Visible = lblContrasena.Visible = txtContrasena.Visible = _baseAccesible;
            txtUsuario.MaxLength = Validaciones_GV42.MAX_LOGIN;
            txtContrasena.MaxLength = Validaciones_GV42.MAX_CONTRASENA;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
            ActualizarOpcion();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("reinstalador.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("reinstalador.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("reinstalador.subtitulo");
            lblAdmin.Text = IdiomaManager_GV42.T(_baseAccesible ? "reinstalador.pedirAdmin" : "reinstalador.baseInaccesible");
            lblUsuario.Text = IdiomaManager_GV42.T("reinstalador.usuario");
            lblContrasena.Text = IdiomaManager_GV42.T("reinstalador.contrasena");
            lblOperacion.Text = IdiomaManager_GV42.T("reinstalador.operacion");
            rbRestaurar.Text = IdiomaManager_GV42.T("reinstalador.restaurar");
            rbReinstalar.Text = IdiomaManager_GV42.T("reinstalador.reinstalar");
            btnEjecutar.Text = IdiomaManager_GV42.T("reinstalador.ejecutar");
            btnCancelar.Text = IdiomaManager_GV42.T("general.cancelar");
            ActualizarOpcion();
        }

        #endregion

        #region Operación

        private void ActualizarOpcion()
        {
            txtArchivo.Enabled = btnElegirArchivo.Enabled = rbRestaurar.Checked;
            lblAdvertencia.Text = IdiomaManager_GV42.T(rbRestaurar.Checked ? "reinstalador.avisoRestaurar" : "reinstalador.avisoReinstalar");
        }

        private void Ejecutar()
        {
            if (_baseAccesible) _bll.ValidarAdministrador(txtUsuario.Text, txtContrasena.Text);

            bool restaurar = rbRestaurar.Checked;
            if (restaurar && string.IsNullOrWhiteSpace(txtArchivo.Text))
                throw new NegocioException_GV42(IdiomaManager_GV42.T("neg.reinstalador.archivoInvalido"));

            if (MessageBox.Show(IdiomaManager_GV42.T(restaurar ? "reinstalador.confirmarRestaurar" : "reinstalador.confirmarReinstalar"),
                                IdiomaManager_GV42.T("reinstalador.titulo"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            Cursor = Cursors.WaitCursor;
            try
            {
                if (restaurar) _bll.RestaurarBackup(txtArchivo.Text);
                else _bll.ReinstalarLimpia();
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MessageBox.Show(IdiomaManager_GV42.T(restaurar ? "reinstalador.okRestaurar" : "reinstalador.okReinstalar"),
                            IdiomaManager_GV42.T("general.exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Eventos

        private void opcion_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarOpcion();
        }

        private void btnElegirArchivo_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = IdiomaManager_GV42.T("reinstalador.elegirArchivo");
                ofd.Filter = IdiomaManager_GV42.T("reinstalador.filtroBak") + " (*.bak)|*.bak";
                ofd.CheckFileExists = true;
                if (ofd.ShowDialog(this) == DialogResult.OK) txtArchivo.Text = ofd.FileName;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                Ejecutar();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("reinstalador.error", ex.Message), IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
