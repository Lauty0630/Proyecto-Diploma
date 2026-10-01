using BLL;
using Servicios;
using Servicios.Instalacion;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMSeleccionInstancia : Form, IObservadorIdioma_GV42
    {
        #region Propiedades

        public string InstanciaElegida { get; private set; }

        #endregion

        #region Constructor

        public FRMSeleccionInstancia()
        {
            InitializeComponent();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();
            RecargarLista();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("instalacion.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("instalacion.encabezado");
            lblSubtitulo.Text = IdiomaManager_GV42.T("instalacion.encabezadoSub");
            lblDescripcion.Text = IdiomaManager_GV42.T("instalacion.subtitulo");
            lblInstancia.Text = IdiomaManager_GV42.T("instalacion.instancia");
            btnDetectar.Text = IdiomaManager_GV42.T("instalacion.btnDetectar");
            btnContinuar.Text = IdiomaManager_GV42.T("instalacion.btnContinuar");
            btnCancelar.Text = IdiomaManager_GV42.T("instalacion.btnCancelar");
        }

        #endregion

        #region Instancias

        private void RecargarLista()
        {
            lblEstado.Text = IdiomaManager_GV42.T("instalacion.detectando");
            Application.DoEvents();

            List<string> instancias = DetectorInstancias_GV42.DetectarInstancias();

            cboInstancias.Items.Clear();
            foreach (var i in instancias) cboInstancias.Items.Add(i);
            if (cboInstancias.Items.Count > 0) cboInstancias.SelectedIndex = 0;

            lblEstado.Text = string.Format(IdiomaManager_GV42.T("instalacion.detectadas"), instancias.Count);
        }

        #endregion

        #region Eventos

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            RecargarLista();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (cboInstancias.SelectedItem == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("instalacion.elegirInstancia"),
                    IdiomaManager_GV42.T("general.advertencia"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string instancia = cboInstancias.SelectedItem.ToString();

            this.Cursor = Cursors.WaitCursor;
            btnContinuar.Enabled = false;
            btnCancelar.Enabled = false;
            btnDetectar.Enabled = false;

            try
            {
                lblEstado.Text = IdiomaManager_GV42.T("instalacion.verificando");
                Application.DoEvents();

                bool existe = BLLInstalador_GV42.ExisteBaseDatos(instancia);

                if (!existe)
                {
                    lblEstado.Text = IdiomaManager_GV42.T("instalacion.instalando");
                    Application.DoEvents();

                    BLLInstalador_GV42.InstalarBaseDatos(instancia);

                    MessageBox.Show(
                        IdiomaManager_GV42.T("instalacion.exitoMensaje"),
                        IdiomaManager_GV42.T("instalacion.exitoTitulo"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ConfiguracionBD_GV42.GuardarInstancia(instancia);
                InstanciaElegida = instancia;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    IdiomaManager_GV42.T("instalacion.errorMensaje") + "\n\n" + ex.Message,
                    IdiomaManager_GV42.T("general.error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblEstado.Text = IdiomaManager_GV42.T("instalacion.error");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnContinuar.Enabled = true;
                btnCancelar.Enabled = true;
                btnDetectar.Enabled = true;
            }
        }

        #endregion
    }
}
