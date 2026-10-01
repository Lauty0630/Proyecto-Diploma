using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static BLL.BLLUsuario_GV42;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMIniciarSesion : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLUsuario_GV42 _bllUsuario;
        private ResultadoIntegridad _resultadoIntegridadPrelogin;

        #endregion

        #region Constructor

        public FRMIniciarSesion()
        {
            InitializeComponent();
            txtLogIn.MaxLength = Validaciones_GV42.MAX_LOGIN;
            txtContrasena.MaxLength = Validaciones_GV42.MAX_CONTRASENA;
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
            this.Text = IdiomaManager_GV42.T("login.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("login.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("login.subtitulo");
            lblEslogan.Text = IdiomaManager_GV42.T("login.eslogan");
            label1.Text = IdiomaManager_GV42.T("login.login");
            label2.Text = IdiomaManager_GV42.T("login.contrasena");
            btnIngresar.Text = IdiomaManager_GV42.T("login.btnIngresar");
            lnkRegistro.Text = IdiomaManager_GV42.T("login.registrarse");

            // El idioma activo se muestra resaltado (link deshabilitado); el otro queda para hacer clic.
            bool ingles = IdiomaManager_GV42.Instancia.EsIngles;
            if (lnkEspanol.Links.Count > 0) lnkEspanol.Links[0].Enabled = ingles;
            if (lnkIngles.Links.Count > 0) lnkIngles.Links[0].Enabled = !ingles;
        }

        #endregion

        #region Inicio de sesión

        private void AbrirFormularioSegunRol()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();

            if (!VerificarIntegridad(actual))
            {
                BLLUsuario_GV42.CerrarSesión();
                return;
            }

            if (actual != null && actual.DebeCambiarContrasena)
            {
                MessageBox.Show(IdiomaManager_GV42.T("login.cambioRequeridoMensaje"),
                                IdiomaManager_GV42.T("login.cambioRequeridoTitulo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                FRMCambiarContrasenia cambio = new FRMCambiarContrasenia(primerLogin: true);
                cambio.Show();
                this.Hide();
                return;
            }

            Form formulario = new FRMMenuPrincipalAdmin();
            formulario.Show();
            this.Hide();
        }

        #endregion

        #region Integridad

        private bool VerificarIntegridad(Usuario_GV42 actual)
        {
            try
            {
                var bllInt = new BLLIntegridad_GV42();
                ResultadoIntegridad res = _resultadoIntegridadPrelogin ?? bllInt.Verificar();

                if (res.EsIntegra)
                {
                    try { bllInt.IniciarBackupsProgramados(); } catch { }
                    return true;
                }

                string detalleBitacora;
                if (res.Detalles != null && res.Detalles.Count > 0)
                {
                    detalleBitacora = string.Join(" | ",
                        res.Detalles.Select(d => $"[{d.Tipo}] {d.Tabla}#{d.IdRegistro}"));
                }
                else
                {
                    detalleBitacora = string.Join(", ", res.TablasComprometidas);
                }

                BLLBitacora_GV42.Instancia.RegistrarEvento(
                    actual.Login, "Admin", "Integridad comprometida",
                    detalleBitacora, "Alta");

                var bllPermisos = new BLLPermisos_GV42();
                Rol_GV42 rolCompleto = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
                var dataKeys = rolCompleto != null
                    ? rolCompleto.ObtenerPatentes().Select(p => p.DataKey ?? string.Empty).ToList()
                    : new List<string>();

                bool puedeRecalcular = dataKeys.Contains("Integridad.Recalcular");
                bool puedeRestaurar = dataKeys.Contains("Integridad.Restore");

                if (!puedeRecalcular && !puedeRestaurar)
                {
                    MessageBox.Show(
                        IdiomaManager_GV42.T("integridad.sistemaInactivoMensaje"),
                        IdiomaManager_GV42.T("integridad.sistemaInactivoTitulo"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (var frm = new FRMIntegridad(res, puedeRecalcular, puedeRestaurar))
                {
                    DialogResult dr = frm.ShowDialog(this);

                    if (frm.SeRestauroBackup)
                    {
                        MessageBox.Show(
                            IdiomaManager_GV42.T("integridad.cerrandoAppBackup"),
                            IdiomaManager_GV42.T("integridad.titulo"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return false;
                    }

                    if (frm.SeRecalcularon)
                    {
                        BLLBitacora_GV42.Instancia.RegistrarEvento(
                            actual.Login, "Admin", "Integridad recalculada",
                            "Admin aceptó los cambios externos como válidos.", "Alta");
                        try { bllInt.IniciarBackupsProgramados(); } catch { }
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    IdiomaManager_GV42.T("integridad.errorVerificacion") + "\n\n" + ex.Message,
                    IdiomaManager_GV42.T("general.error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Eventos

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string login = txtLogIn.Text.Trim();
            string contrasena = txtContrasena.Text;   // la contraseña no se recorta: se compara tal cual

            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show(IdiomaManager_GV42.T("general.completarCampos"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Al ingresar solo se controla largo y caracteres (el formato completo se exige al crear el
            // usuario; así un usuario viejo cuyo login no cumpla la regla nueva igual puede entrar).
            if (login.Length > Validaciones_GV42.MAX_LOGIN || login.Any(c => !(char.IsLetterOrDigit(c) || c == '.')))
            {
                Tema_GV42.MostrarError(txtLogIn, Validaciones_GV42.MENSAJE_LOGIN);
                return;
            }

            try
            {
                _resultadoIntegridadPrelogin = new BLLIntegridad_GV42().Verificar();
            }
            catch
            {
                _resultadoIntegridadPrelogin = null;
            }

            ResultadoLogin resultado = _bllUsuario.IntentarLogin(login, contrasena);

            switch (resultado)
            {
                case ResultadoLogin.Exitoso:
                    AbrirFormularioSegunRol();
                    break;
                case ResultadoLogin.UsuarioBloqueado:
                case ResultadoLogin.BloqueadoPorIntentos:
                    MessageBox.Show(IdiomaManager_GV42.T("login.bloqueado"),
                                    IdiomaManager_GV42.T("general.accesoDenegado"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ResultadoLogin.UsuarioInactivo:
                    MessageBox.Show(IdiomaManager_GV42.T("login.inactivo"),
                                    IdiomaManager_GV42.T("general.accesoDenegado"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ResultadoLogin.ContrasenaIncorrecta:
                    MessageBox.Show(IdiomaManager_GV42.T("login.contrasenaIncorrecta"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ResultadoLogin.UsuarioInexistente:
                    MessageBox.Show(IdiomaManager_GV42.T("login.usuarioInexistente"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ResultadoLogin.SesionActiva:
                    MessageBox.Show(IdiomaManager_GV42.T("login.sesionActiva"),
                                    IdiomaManager_GV42.T("general.advertencia"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ResultadoLogin.Error:
                    MessageBox.Show(IdiomaManager_GV42.T("login.errorUsuario"),
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        // RFN 1: el cliente puede crearse su propia cuenta y reservar sin pasar por un vendedor.
        private void lnkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new FRMRegistroCliente_GV42())
                frm.ShowDialog(this);
        }

        // Selector de idioma del login: todavía no hay sesión, así que se cambia directo en el
        // IdiomaManager (no se usa BLLUsuario.CambiarIdioma, que además guarda la preferencia del
        // usuario logueado). Al iniciar sesión se aplica el idioma guardado de ese usuario.
        private void lnkEspanol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.ES);
        }

        private void lnkIngles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IdiomaManager_GV42.Instancia.CambiarIdioma(IdiomaManager_GV42.EN);
        }

        #endregion
    }
}
