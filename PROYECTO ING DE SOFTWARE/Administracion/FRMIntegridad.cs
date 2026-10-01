using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMIntegridad : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLIntegridad_GV42 _bll;
        private readonly ResultadoIntegridad _resultado;

        #endregion

        #region Propiedades

        public bool SeRestauroBackup { get; private set; }
        public bool SeRecalcularon { get; private set; }

        #endregion

        #region Constructor

        public FRMIntegridad(ResultadoIntegridad resultado)
            : this(resultado, puedeRecalcular: true, puedeRestaurar: true)
        {
        }

        public FRMIntegridad(ResultadoIntegridad resultado, bool puedeRecalcular, bool puedeRestaurar)
        {
            InitializeComponent();
            _bll = new BLLIntegridad_GV42();
            _resultado = resultado;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            // ActualizarIdioma también arma la lista de tablas / registros afectados.
            ActualizarIdioma();

            btnRestore.Visible = puedeRecalcular;
            btnBackup.Visible = puedeRestaurar;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("integridad.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("integridad.tituloAlerta");
            lblSubtitulo.Text = IdiomaManager_GV42.T("integridad.titulo");
            lblMensaje.Text = IdiomaManager_GV42.T("integridad.mensaje");
            lblTablas.Text = (_resultado?.Detalles != null && _resultado.Detalles.Count > 0)
                ? IdiomaManager_GV42.T("integridad.detallesTitulo")
                : IdiomaManager_GV42.T("integridad.tablasAfectadas");
            btnRestore.Text = IdiomaManager_GV42.T("integridad.botonRestore");
            btnBackup.Text = IdiomaManager_GV42.T("integridad.botonBackup");
            btnCancelar.Text = IdiomaManager_GV42.T("general.cancelar");

            CargarTablas();
        }

        #endregion

        #region Carga de datos

        private void CargarTablas()
        {
            lstTablas.Items.Clear();
            if (_resultado == null) return;

            if (_resultado.Detalles != null && _resultado.Detalles.Count > 0)
            {
                foreach (var d in _resultado.Detalles)
                {
                    string accion = TraducirTipoTampering(d.Tipo);
                    lstTablas.Items.Add(IdiomaManager_GV42.T("integridad.lineaDetalle", accion, d.Tabla, d.IdRegistro));
                }
            }
            else
            {
                foreach (var t in _resultado.TablasComprometidas)
                    lstTablas.Items.Add("• " + t);
            }
        }

        private string TraducirTipoTampering(TipoTampering tipo)
        {
            switch (tipo)
            {
                case TipoTampering.Insertado: return IdiomaManager_GV42.T("integridad.tipoInsertado");
                case TipoTampering.Modificado: return IdiomaManager_GV42.T("integridad.tipoModificado");
                case TipoTampering.Eliminado: return IdiomaManager_GV42.T("integridad.tipoEliminado");
                default: return tipo.ToString();
            }
        }

        #endregion

        #region Eventos

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                IdiomaManager_GV42.T("integridad.confirmRestore"),
                IdiomaManager_GV42.T("integridad.titulo"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                _bll.Recalcular();
                SeRecalcularon = true;
                MessageBox.Show(IdiomaManager_GV42.T("integridad.restoreOk"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string rutaSeleccionada;

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

                if (ofd.ShowDialog(this) != DialogResult.OK) return;
                rutaSeleccionada = ofd.FileName;
            }

            string mensaje = IdiomaManager_GV42.T("integridad.confirmBackup") +
                             "\n\n" + IdiomaManager_GV42.T("integridad.archivoARestaurar") + "\n" + rutaSeleccionada;

            DialogResult r = MessageBox.Show(mensaje,
                                             IdiomaManager_GV42.T("integridad.titulo"),
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r != DialogResult.Yes) return;

            try
            {
                _bll.RestaurarBackupDesdeRuta(rutaSeleccionada);
                SeRestauroBackup = true;
                MessageBox.Show(IdiomaManager_GV42.T("integridad.backupOk"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("integridad.backupError") + "\n\n" + ex.Message,
                                IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
