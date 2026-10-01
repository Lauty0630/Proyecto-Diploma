using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // El cliente crea su propia cuenta para poder reservar sin pasar por un vendedor (RFN 1,
    // canal Autogestión). El diseño está en FRMRegistroCliente_GV42.Designer.cs (Form Designer).
    public partial class FRMRegistroCliente_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();

        #endregion

        #region Constructor

        public FRMRegistroCliente_GV42()
        {
            InitializeComponent();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("registro.tituloVentana");
            lblTitulo.Text = IdiomaManager_GV42.T("registro.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("registro.subtitulo");
            lblDni.Text = IdiomaManager_GV42.T("registro.dni");
            lblTelefono.Text = IdiomaManager_GV42.T("registro.telefono");
            lblNombre.Text = IdiomaManager_GV42.T("registro.nombre");
            lblApellido.Text = IdiomaManager_GV42.T("registro.apellido");
            lblEmail.Text = IdiomaManager_GV42.T("registro.email");
            lblLogin.Text = IdiomaManager_GV42.T("registro.login");
            lblContrasena.Text = IdiomaManager_GV42.T("registro.contrasena");
            lblConfirmar.Text = IdiomaManager_GV42.T("registro.confirmar");
            lblAyudaLogin.Text = IdiomaManager_GV42.T("registro.ayudaLogin");
            lblAyudaContrasena.Text = IdiomaManager_GV42.T("registro.ayudaContrasena");
            btnRegistrarme.Text = IdiomaManager_GV42.T("registro.crear");
            btnCancelar.Text = IdiomaManager_GV42.T("general.cancelar");
        }

        #endregion

        #region Validaciones

        // Valida en pantalla campo por campo y marca el que está mal (la BLL vuelve a validar todo).
        private bool ValidarCampos()
        {
            if (!Validaciones_GV42.EsDniValido(txtDni.Text.Trim()))
            { Tema_GV42.MostrarError(txtDni, Validaciones_GV42.MENSAJE_DNI); return false; }
            if (!Validaciones_GV42.EsTelefonoValido(txtTelefono.Text.Trim()))
            { Tema_GV42.MostrarError(txtTelefono, Validaciones_GV42.MENSAJE_TELEFONO); return false; }
            if (!Validaciones_GV42.EsNombreValido(Validaciones_GV42.NormalizarEspacios(txtNombre.Text)))
            { Tema_GV42.MostrarError(txtNombre, Validaciones_GV42.MENSAJE_NOMBRE); return false; }
            if (!Validaciones_GV42.EsApellidoValido(Validaciones_GV42.NormalizarEspacios(txtApellido.Text)))
            { Tema_GV42.MostrarError(txtApellido, Validaciones_GV42.MENSAJE_APELLIDO); return false; }
            if (!Validaciones_GV42.EsEmailValido(txtEmail.Text.Trim()))
            { Tema_GV42.MostrarError(txtEmail, Validaciones_GV42.MENSAJE_EMAIL); return false; }
            if (!Validaciones_GV42.EsLoginValido(txtLogin.Text.Trim()))
            { Tema_GV42.MostrarError(txtLogin, Validaciones_GV42.MENSAJE_LOGIN); return false; }
            if (!Validaciones_GV42.EsContrasenaValida(txtContrasena.Text))
            { Tema_GV42.MostrarError(txtContrasena, Validaciones_GV42.MENSAJE_CONTRASENA); return false; }
            if (txtContrasena.Text != txtConfirmar.Text)
            { Tema_GV42.MostrarError(txtConfirmar, IdiomaManager_GV42.T("registro.noCoinciden")); return false; }
            return true;
        }

        #endregion

        #region Eventos

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                var cliente = new Pasajero_GV42
                {
                    DNI = txtDni.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                _bll.RegistrarClienteAutogestionado(cliente, txtLogin.Text.Trim(), txtContrasena.Text, txtConfirmar.Text);

                MessageBox.Show(IdiomaManager_GV42.T("registro.creada"), IdiomaManager_GV42.T("registro.creadaTitulo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("registro.noSePudo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("registro.accionCrear"), ex);
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
