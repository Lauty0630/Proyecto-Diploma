using BLL;
using Servicios;
using System;
using System.Windows.Forms;
using static BLL.BLLUsuario_GV42;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMCambiarContrasenia : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLUsuario_GV42 _bll;
        private readonly bool _primerLogin;
        private bool _cambioRealizado;

        #endregion

        #region Constructor

        public FRMCambiarContrasenia(bool primerLogin = false)
        {
            InitializeComponent();
            txtUsuario.Text = SessionManager_GV42.Instancia.ObtenerUsuarioActual()?.Login;
            _bll = new BLLUsuario_GV42();
            _primerLogin = primerLogin;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            Program.CerrarAplicacionAlSerUltimaVentana(this);

            ActualizarIdioma();

            txtContrasenia.MaxLength = Validaciones_GV42.MAX_CONTRASENA;
            txtNuevaconstrasenia.MaxLength = Validaciones_GV42.MAX_CONTRASENA;
            txtConfirmarContrasenia.MaxLength = Validaciones_GV42.MAX_CONTRASENA;

            // Primer ingreso: si se cierra sin cambiar la contraseña, se cierra la sesión (y queda en la bitácora).
            this.FormClosing += (s, e) =>
            {
                if (_primerLogin && !_cambioRealizado) BLLUsuario_GV42.CerrarSesión();
            };
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("cambiarClave.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("cambiarClave.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("cambiarClave.subtitulo");
            lblSeccion.Text = IdiomaManager_GV42.T("cambiarClave.seccion");
            label1.Text = IdiomaManager_GV42.T("cambiarClave.usuario");
            label2.Text = IdiomaManager_GV42.T("cambiarClave.actual");
            label3.Text = IdiomaManager_GV42.T("cambiarClave.nueva");
            label4.Text = IdiomaManager_GV42.T("cambiarClave.confirmar");
            lblAyudaNueva.Text = IdiomaManager_GV42.T("cambiarClave.ayudaNueva");
            btnAceptar.Text = IdiomaManager_GV42.T("cambiarClave.btnAceptar");
        }

        #endregion

        #region Navegación

        private void AbrirMenuPrincipalSegunRol()
        {
            Form menu = new FRMMenuPrincipalAdmin();
            menu.Show();
        }

        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Las contraseñas no se recortan: se comparan tal cual se escribieron (antes el login y el
            // registro las trataban distinto y una clave con espacio final nunca servía).
            string login = txtUsuario.Text.Trim();
            string contrasenaActual = txtContrasenia.Text;
            string nuevaContrasena = txtNuevaconstrasenia.Text;
            string confirmar = txtConfirmarContrasenia.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(contrasenaActual) ||
                string.IsNullOrEmpty(nuevaContrasena) || string.IsNullOrEmpty(confirmar))
            {
                MessageBox.Show(IdiomaManager_GV42.T("general.completarCampos"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones_GV42.EsContrasenaValida(nuevaContrasena))
            {
                Tema_GV42.MostrarError(txtNuevaconstrasenia, Validaciones_GV42.MENSAJE_CONTRASENA);
                return;
            }
            if (nuevaContrasena != confirmar)
            {
                Tema_GV42.MostrarError(txtConfirmarContrasenia, IdiomaManager_GV42.T("cambiarClave.noCoinciden"));
                return;
            }

            ResultadoCambioContrasena resultado;
            try
            {
                resultado = _bll.CambiarContrasena(login, contrasenaActual, nuevaContrasena, confirmar);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambiarClave.accionCambiar"), ex);
                return;
            }

            switch (resultado)
            {
                case ResultadoCambioContrasena.Exitoso:
                    _cambioRealizado = true;
                    Usuario_GV42 enSesion = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
                    if (enSesion != null) enSesion.DebeCambiarContrasena = false;
                    MessageBox.Show(IdiomaManager_GV42.T("cambiarClave.exito"),
                                    IdiomaManager_GV42.T("general.exito"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_primerLogin) AbrirMenuPrincipalSegunRol();
                    this.Close();
                    break;
                case ResultadoCambioContrasena.ContrasenaActualIncorrecta:
                    Tema_GV42.MarcarInvalido(txtContrasenia);
                    MessageBox.Show(IdiomaManager_GV42.T("cambiarClave.actualIncorrecta"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasenia.Focus();
                    break;
                case ResultadoCambioContrasena.NoCumplePolitica:
                    Tema_GV42.MostrarError(txtNuevaconstrasenia, Validaciones_GV42.MENSAJE_CONTRASENA);
                    break;
                case ResultadoCambioContrasena.ContrasenasNoCoinciden:
                    MessageBox.Show(IdiomaManager_GV42.T("cambiarClave.noCoinciden"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ResultadoCambioContrasena.NuevaIgualActual:
                    MessageBox.Show(IdiomaManager_GV42.T("cambiarClave.iguales"),
                                    IdiomaManager_GV42.T("general.advertencia"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNuevaconstrasenia.Focus();
                    break;
                case ResultadoCambioContrasena.UsuarioInexistente:
                    MessageBox.Show(IdiomaManager_GV42.T("cambiarClave.usuarioInexistente"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        #endregion
    }
}
