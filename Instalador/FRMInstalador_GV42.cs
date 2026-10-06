using BE;
using System;
using System.Windows.Forms;

namespace Instalador
{
    // Única pantalla del instalador. Solo toma lo que elige el operador y muestra el avance:
    // todo el trabajo lo hace InstaladorSistema_GV42.
    public partial class FRMInstalador_GV42 : Form
    {
        #region Campos

        private readonly InstaladorSistema_GV42 _instalador = new InstaladorSistema_GV42();
        private bool _yaInstalado;

        #endregion

        #region Constructor

        public FRMInstalador_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Carga

        private void FRMInstalador_GV42_Load(object sender, EventArgs e)
        {
            string instalada = _instalador.CarpetaInstalada();
            _yaInstalado = instalada != null;

            // Modo: instalador (primera vez) o reinstalador (ya instalado).
            if (_yaInstalado)
            {
                lblTitulo.Text = "Reinstalador de " + InstaladorSistema_GV42.NOMBRE_SISTEMA;
                lblEstado.Text = "El sistema ya está instalado en: " + instalada;
                txtCarpeta.Text = instalada;
                btnInstalar.Text = "Reinstalar";
            }
            else
            {
                lblTitulo.Text = "Instalador de " + InstaladorSistema_GV42.NOMBRE_SISTEMA;
                lblEstado.Text = "El sistema todavía no está instalado en esta computadora.";
                txtCarpeta.Text = _instalador.CarpetaDestinoSugerida;
                btnInstalar.Text = "Instalar";
            }

            // Restaurar un backup o reinstalar la base limpia son operaciones del reinstalador.
            rbRestaurar.Enabled = _yaInstalado;
            rbLimpia.Enabled = _yaInstalado;
            rbPreparar.Checked = true;

            CargarInstancias();
            ActualizarOpciones();
        }

        private void CargarInstancias()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                cmbInstancia.Items.Clear();
                foreach (string instancia in _instalador.DetectarInstancias())
                    cmbInstancia.Items.Add(instancia);

                string anterior = _yaInstalado ? _instalador.InstanciaInstalada() : null;
                if (!string.IsNullOrEmpty(anterior) && !cmbInstancia.Items.Contains(anterior))
                    cmbInstancia.Items.Add(anterior);

                if (!string.IsNullOrEmpty(anterior)) cmbInstancia.SelectedItem = anterior;
                else if (cmbInstancia.Items.Count > 0) cmbInstancia.SelectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Habilita lo que corresponde a la opción de base elegida.
        private void ActualizarOpciones()
        {
            txtBackup.Enabled = rbRestaurar.Checked;
            btnBackup.Enabled = rbRestaurar.Checked;
            grpAdministrador.Enabled = !rbPreparar.Checked;
        }

        #endregion

        #region Eventos

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (var dialogo = new FolderBrowserDialog())
            {
                dialogo.Description = "Carpeta donde se va a instalar el sistema";
                dialogo.SelectedPath = txtCarpeta.Text;
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                    txtCarpeta.Text = dialogo.SelectedPath;
            }
        }

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            CargarInstancias();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Backup a restaurar";
                dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak";
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                    txtBackup.Text = dialogo.FileName;
            }
        }

        private void rbOpcion_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarOpciones();
        }

        private void btnInstalar_Click(object sender, EventArgs e)
        {
            var pedido = new PedidoInstalacion_GV42
            {
                CarpetaDestino = txtCarpeta.Text.Trim(),
                Instancia = (cmbInstancia.Text ?? string.Empty).Trim(),
                OpcionBase = rbRestaurar.Checked ? OpcionBase_GV42.RestaurarBackup
                           : rbLimpia.Checked ? OpcionBase_GV42.ReinstalarLimpia
                           : OpcionBase_GV42.Preparar,
                RutaBackup = txtBackup.Text.Trim(),
                CrearAccesoDirecto = chkAccesoDirecto.Checked,
                LoginAdministrador = txtUsuario.Text.Trim(),
                ContrasenaAdministrador = txtContrasena.Text
            };

            try
            {
                _instalador.Validar(pedido);

                if (pedido.OpcionBase != OpcionBase_GV42.Preparar)
                {
                    string aviso = pedido.OpcionBase == OpcionBase_GV42.RestaurarBackup
                        ? "La base de datos va a volver al estado del backup elegido. Se pierde todo lo cargado después de ese backup."
                        : "La base de datos se va a borrar y crear de nuevo. Se pierden todos los datos cargados.";
                    if (MessageBox.Show(this, aviso + "\n\n¿Continuar?", Text, MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                }

                txtProgreso.Clear();
                HabilitarPantalla(false);
                _instalador.Instalar(pedido, Avisar);

                MessageBox.Show(this, InstaladorSistema_GV42.NOMBRE_SISTEMA + " quedó instalado correctamente.", Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (NegocioException_GV42 ex)
            {
                Avisar("No se pudo continuar: " + ex.Message);
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Avisar("Error: " + ex.Message);
                MessageBox.Show(this, "La instalación no se completó.\n\n" + ex.Message, Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarPantalla(true);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Métodos privados

        // Agrega una línea al cuadro de avance y refresca la pantalla (la instalación corre en el
        // mismo hilo que la ventana).
        private void Avisar(string texto)
        {
            txtProgreso.AppendText(DateTime.Now.ToString("HH:mm:ss") + "  " + texto + Environment.NewLine);
            Application.DoEvents();
        }

        private void HabilitarPantalla(bool habilitada)
        {
            grpDestino.Enabled = habilitada;
            grpBase.Enabled = habilitada;
            chkAccesoDirecto.Enabled = habilitada;
            btnInstalar.Enabled = habilitada;
            btnSalir.Enabled = habilitada;
            Cursor = habilitada ? Cursors.Default : Cursors.WaitCursor;
            if (habilitada) ActualizarOpciones();
        }

        #endregion
    }
}
