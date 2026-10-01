using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMBackupManual : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLIntegridad_GV42 _bll;

        #endregion

        #region Constructor

        public FRMBackupManual()
        {
            InitializeComponent();
            _bll = new BLLIntegridad_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            AplicarPermisos();
            ActualizarIdioma();
        }

        #endregion

        #region Permisos

        private void AplicarPermisos()
        {
            var actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            var bllPermisos = new BLLPermisos_GV42();
            var rol = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
            if (rol == null) return;

            var dataKeys = rol.ObtenerPatentes()
                .Select(p => p.DataKey ?? string.Empty)
                .ToList();

            bool puedeCrear = dataKeys.Contains("Backup.Crear");
            bool puedeRestaurar = dataKeys.Contains("Integridad.Restore");

            btnCrear.Visible = puedeCrear;
            btnRestaurar.Visible = puedeRestaurar;

            // La tarjeta de cada acción se oculta junto con su botón.
            pnlCrear.Visible = puedeCrear;
            pnlRestaurar.Visible = puedeRestaurar;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("backup.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("backup.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("backup.encabezado");
            lblDescripcion.Text = IdiomaManager_GV42.T("backup.subtitulo");
            lblCrearTitulo.Text = IdiomaManager_GV42.T("backup.crearTitulo");
            lblCrearDesc.Text = IdiomaManager_GV42.T("backup.crearDesc");
            lblRestaurarTitulo.Text = IdiomaManager_GV42.T("backup.restaurarTitulo");
            lblRestaurarDesc.Text = IdiomaManager_GV42.T("backup.restaurarDesc");
            btnCrear.Text = IdiomaManager_GV42.T("backup.btnCrear");
            btnRestaurar.Text = IdiomaManager_GV42.T("backup.btnRestaurar");
            btnCerrar.Text = IdiomaManager_GV42.T("backup.btnCerrar");
        }

        #endregion

        #region Eventos

        private void btnCrear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string ruta = _bll.HacerBackupManual();
                MessageBox.Show(
                    string.Format(IdiomaManager_GV42.T("backup.crearExito"), ruta),
                    IdiomaManager_GV42.T("general.exito"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    IdiomaManager_GV42.T("general.error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = IdiomaManager_GV42.T("backup.ofdTitulo");
                ofd.Filter = IdiomaManager_GV42.T("backup.ofdFiltro");
                ofd.CheckFileExists = true;

                string ultimo = _bll.ObtenerUltimoBackup();
                if (!string.IsNullOrEmpty(ultimo))
                {
                    ofd.InitialDirectory = System.IO.Path.GetDirectoryName(ultimo);
                    ofd.FileName = System.IO.Path.GetFileName(ultimo);
                }

                if (ofd.ShowDialog() != DialogResult.OK) return;

                DialogResult r = MessageBox.Show(
                    IdiomaManager_GV42.T("backup.confirmRestaurarMensaje"),
                    IdiomaManager_GV42.T("backup.confirmRestaurarTitulo"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes) return;

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    _bll.RestaurarBackupManual(ofd.FileName);
                    MessageBox.Show(
                        IdiomaManager_GV42.T("backup.restaurarExito"),
                        IdiomaManager_GV42.T("general.exito"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        IdiomaManager_GV42.T("general.error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
