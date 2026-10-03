using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Gestión de usuarios del sistema (ABM, desbloqueo, activación).
    // El diseño está en FRMGestionUsuariosAdmin.Designer.cs (Form Designer); acá va solo la lógica.
    public partial class FRMGestionUsuariosAdmin : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLUsuario_GV42 _bll;

        private string _modo = "Consulta";

        private Usuario_GV42 _usuarioSeleccionado = null;

        #endregion

        #region Constructor

        public FRMGestionUsuariosAdmin()
        {
            InitializeComponent();
            _bll = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();
            AplicarPermisos();

            // Largos máximos iguales a los de la base / reglas de negocio (el DNI solo acepta dígitos: txtDni_KeyPress).
            txtDni.MaxLength = 8;
            txtNombre.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtApellido.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtEmail.MaxLength = Validaciones_GV42.MAX_EMAIL;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("usuarios.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("usuarios.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("usuarios.subtitulo");

            lblTituloDatos.Text = IdiomaManager_GV42.T("usuarios.tituloDatos");
            label1.Text = IdiomaManager_GV42.T("usuarios.dni");
            label2.Text = IdiomaManager_GV42.T("usuarios.apellido");
            label3.Text = IdiomaManager_GV42.T("usuarios.nombre");
            label4.Text = IdiomaManager_GV42.T("usuarios.email");
            label5.Text = IdiomaManager_GV42.T("usuarios.rol");
            label6.Text = IdiomaManager_GV42.T("usuarios.userName");
            label7.Text = IdiomaManager_GV42.T("usuarios.bloqueado");
            label8.Text = IdiomaManager_GV42.T("usuarios.activo");
            lblMensaje.Text = TraducirModo(_modo);

            lblTituloGrilla.Text = IdiomaManager_GV42.T("usuarios.tituloGrilla");
            rbActivos.Text = IdiomaManager_GV42.T("usuarios.activos");
            rbTodos.Text = IdiomaManager_GV42.T("usuarios.todos");

            btnCrear.Text = IdiomaManager_GV42.T("usuarios.crear");
            btnModificar.Text = IdiomaManager_GV42.T("usuarios.modificar");
            btnDesbloquear.Text = IdiomaManager_GV42.T("usuarios.desbloquear");
            btnActivarDesactivar.Text = IdiomaManager_GV42.T("usuarios.activarDesactivar");
            btnAplicar.Text = IdiomaManager_GV42.T("usuarios.aplicar");
            btnCancelar.Text = IdiomaManager_GV42.T("usuarios.cancelar");
            btnSalir.Text = IdiomaManager_GV42.T("usuarios.salir");

            // Datos ya mostrados: encabezados de la grilla y Sí/No del usuario elegido.
            ConfigurarColumnasGrilla();
            if (_usuarioSeleccionado != null)
            {
                if (txtBloqueado.Text.Length > 0) txtBloqueado.Text = TextoSiNo(_usuarioSeleccionado.Bloqueo);
                if (txtActivo.Text.Length > 0) txtActivo.Text = TextoSiNo(_usuarioSeleccionado.Activo);
            }
        }

        private string TraducirModo(string modo)
        {
            string etiquetaModo = IdiomaManager_GV42.T("usuarios.modo");
            return etiquetaModo + ": " + IdiomaManager_GV42.TConDefecto("usuarios.nombreModo" + modo, modo);
        }

        private static string TextoSiNo(bool valor)
        {
            return IdiomaManager_GV42.T(valor ? "general.si" : "general.no");
        }

        #endregion

        #region Permisos

        private void AplicarPermisos()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            var bllPermisos = new BLLPermisos_GV42();
            Rol_GV42 rolCompleto = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
            if (rolCompleto == null) return;

            btnCrear.Visible = rolCompleto.TienePermiso("Usuarios.Crear");
            btnModificar.Visible = rolCompleto.TienePermiso("Usuarios.Modificar");
            btnDesbloquear.Visible = rolCompleto.TienePermiso("Usuarios.Desbloquear");
            btnActivarDesactivar.Visible = rolCompleto.TienePermiso("Usuarios.Activar");
        }

        #endregion

        #region Carga de datos

        private void CargarRoles()
        {
            comboBox1.DataSource = _bll.ListarRoles();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Id";
        }

        private void CargarGrilla(bool soloActivos)
        {
            List<Usuario_GV42> lista = soloActivos ? _bll.ListarActivos() : _bll.ListarTodos();
            MostrarEnGrilla(lista);
        }

        private void MostrarEnGrilla(List<Usuario_GV42> lista)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = lista ?? new List<Usuario_GV42>();

            ConfigurarColumnasGrilla();

            // Los usuarios inactivos se resaltan (depende de cada fila, por eso se hace en código).
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                if (row.DataBoundItem is Usuario_GV42 u && !u.Activo)
                {
                    row.DefaultCellStyle.BackColor = Tema_GV42.FondoError;
                    row.DefaultCellStyle.ForeColor = Tema_GV42.Error;
                }
            }

            if (dgvUsuarios.Rows.Count == 0)
            {
                _usuarioSeleccionado = null;
                LimpiarCampos();
            }
        }

        private void ConfigurarColumnasGrilla()
        {
            if (dgvUsuarios.Columns.Count == 0) return;

            string[] aOcultar = {
                "Contrasena", "IntentosFallidos", "UltimoIntentoFallido",
                "Rol", "DebeCambiarContrasena", "Idioma"
            };
            foreach (string col in aOcultar)
                if (dgvUsuarios.Columns.Contains(col))
                    dgvUsuarios.Columns[col].Visible = false;

            ConfigurarColumna("DNI", "usuarios.dni", 80, 84);
            ConfigurarColumna("Apellido", "usuarios.apellido", 95, 60);
            ConfigurarColumna("Nombre", "usuarios.nombre", 90, 60);
            ConfigurarColumna("Login", "usuarios.login", 85, 60);
            ConfigurarColumna("RolNombre", "usuarios.rol", 90, 60);
            ConfigurarColumna("Email", "usuarios.email", 150, 80);
            ConfigurarColumna("Bloqueo", "usuarios.bloqueado", 75, 60);
            ConfigurarColumna("Activo", "usuarios.activo", 60, 50);
        }

        // Encabezado traducido y ancho relativo de cada columna (las columnas se generan al enlazar los datos).
        private void ConfigurarColumna(string nombre, string clave, float pesoAncho, int anchoMinimo)
        {
            if (!dgvUsuarios.Columns.Contains(nombre)) return;
            DataGridViewColumn col = dgvUsuarios.Columns[nombre];
            col.HeaderText = IdiomaManager_GV42.T(clave);
            col.FillWeight = pesoAncho;
            col.MinimumWidth = anchoMinimo;
        }

        private void SeleccionarRolEnCombo(Rol_GV42 rol)
        {
            if (rol == null) { comboBox1.SelectedIndex = -1; return; }

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                Rol_GV42 r = comboBox1.Items[i] as Rol_GV42;
                bool mismoRol = rol.Id > 0
                    ? r != null && r.Id == rol.Id
                    : r != null && string.Equals(r.Nombre, rol.Nombre, StringComparison.OrdinalIgnoreCase);
                if (mismoRol)
                {
                    comboBox1.SelectedIndex = i;
                    return;
                }
            }
            comboBox1.SelectedIndex = -1;
        }

        #endregion

        #region Modos de pantalla

        private void ModoConsulta()
        {
            _modo = "Consulta";
            lblMensaje.Text = TraducirModo(_modo);
            LimpiarCampos();
            HabilitarCampos(false);
            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnDesbloquear.Enabled = true;
            btnActivarDesactivar.Enabled = true;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            rbActivos.Enabled = true;
            rbTodos.Enabled = true;
            dgvUsuarios.Enabled = true;
        }

        private void ModoOperacion(string modo)
        {
            _modo = modo;
            lblMensaje.Text = TraducirModo(modo);
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnDesbloquear.Enabled = false;
            btnActivarDesactivar.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
            rbActivos.Enabled = false;
            rbTodos.Enabled = false;
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtDni.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            txtEmail.Enabled = habilitar;
            comboBox1.Enabled = habilitar;
            txtUser.Enabled = false;
            txtBloqueado.Enabled = false;
            txtActivo.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtDni.Text = txtApellido.Text = txtNombre.Text =
            txtEmail.Text = txtUser.Text =
            txtBloqueado.Text = txtActivo.Text = "";
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        #endregion

        #region Operaciones (Crear / Modificar / Desbloquear / Activar)

        private void Crear()
        {
            string dni = txtDni.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            Rol_GV42 rol = comboBox1.SelectedItem as Rol_GV42;

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || rol == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("general.completarCampos"), IdiomaManager_GV42.T("general.advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string advertencia = IdiomaManager_GV42.T("general.advertencia");
            if (!Validaciones_GV42.EsDniValido(dni))
            {
                Tema_GV42.MostrarError(txtDni, Validaciones_GV42.MENSAJE_DNI, advertencia);
                return;
            }
            if (!Validaciones_GV42.EsApellidoValido(Validaciones_GV42.NormalizarEspacios(apellido)))
            {
                Tema_GV42.MostrarError(txtApellido, Validaciones_GV42.MENSAJE_APELLIDO, advertencia);
                return;
            }
            if (!Validaciones_GV42.EsNombreValido(Validaciones_GV42.NormalizarEspacios(nombre)))
            {
                Tema_GV42.MostrarError(txtNombre, Validaciones_GV42.MENSAJE_NOMBRE, advertencia);
                return;
            }
            if (!Validaciones_GV42.EsEmailValido(email))
            {
                Tema_GV42.MostrarError(txtEmail, Validaciones_GV42.MENSAJE_EMAIL, advertencia);
                return;
            }
            if (_bll.ExisteDNI(dni))
            {
                Tema_GV42.MostrarError(txtDni, $"{IdiomaManager_GV42.T("usuarios.dniDuplicado")} '{dni}'.", advertencia);
                return;
            }

            try
            {
                string loginCreado = _bll.CrearUsuario(dni, apellido, nombre, email, rol);

                // El administrador necesita saber con qué credenciales entra la persona la primera vez.
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.confirmarCreado") + "\n\n" +
                                IdiomaManager_GV42.T("usuarios.credencialesIniciales", loginCreado, loginCreado),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ModoConsulta();
                CargarGrilla(rbActivos.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.errorCrear") + "\n\n" + ex.Message,
                                IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Modificar()
        {
            string email = txtEmail.Text.Trim();
            Rol_GV42 rol = comboBox1.SelectedItem as Rol_GV42;

            if (!Validaciones_GV42.EsEmailValido(email))
            {
                Tema_GV42.MostrarError(txtEmail, Validaciones_GV42.MENSAJE_EMAIL, IdiomaManager_GV42.T("general.advertencia"));
                return;
            }
            if (rol == null)
            {
                Tema_GV42.MostrarError(comboBox1, IdiomaManager_GV42.T("usuarios.rolVacio"), IdiomaManager_GV42.T("general.advertencia"));
                return;
            }

            if (email != _usuarioSeleccionado.Email)
                _bll.ModificarEmail(_usuarioSeleccionado.DNI, email);

            if (_usuarioSeleccionado.Rol == null || rol.Id != _usuarioSeleccionado.Rol.Id)
                _bll.ModificarRol(_usuarioSeleccionado.DNI, rol);

            MessageBox.Show(IdiomaManager_GV42.T("usuarios.confirmarModificado"),
                            IdiomaManager_GV42.T("general.exito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
        }

        private void Desbloquear()
        {
            if (_usuarioSeleccionado.Bloqueo == false)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.usuarioYaDesbloqueado"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ModoConsulta();
                CargarGrilla(rbActivos.Checked);
                return;
            }

            string temporal = _bll.Desbloquear(_usuarioSeleccionado.DNI, _usuarioSeleccionado.Login);
            MessageBox.Show(IdiomaManager_GV42.T("usuarios.usuarioDesbloqueado", _usuarioSeleccionado.Login) + "\n\n" +
                            IdiomaManager_GV42.T("usuarios.contrasenaTemporal", temporal),
                            IdiomaManager_GV42.T("general.exito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
        }

        private void ActivarDesactivar()
        {
            bool nuevoEstado = !_usuarioSeleccionado.Activo;

            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual != null && _usuarioSeleccionado.Login == actual.Login)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.noAutoDesactivar"),
                                IdiomaManager_GV42.T("general.accionNoPermitida"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ModoConsulta();
                CargarGrilla(rbActivos.Checked);
                return;
            }

            _bll.ActivarDesactivar(_usuarioSeleccionado.DNI, nuevoEstado);
            string claveMsj = nuevoEstado ? "usuarios.usuarioActivado" : "usuarios.usuarioDesactivado";
            MessageBox.Show(IdiomaManager_GV42.T(claveMsj, _usuarioSeleccionado.Login),
                            IdiomaManager_GV42.T("general.exito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
        }

        #endregion

        #region Eventos

        private void FRMPrincipalAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
                ModoConsulta();
                // Al marcar rbActivos se dispara rbActivos_CheckedChanged, que ya carga la grilla
                // (antes la grilla se cargaba dos veces al abrir el formulario).
                if (rbActivos.Checked) CargarGrilla(soloActivos: true);
                else rbActivos.Checked = true;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("usuarios.accionCargar"), ex);
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El DNI solo admite dígitos.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            _usuarioSeleccionado = dgvUsuarios.CurrentRow.DataBoundItem as Usuario_GV42;
            if (_usuarioSeleccionado == null) return;

            txtDni.Text = _usuarioSeleccionado.DNI;
            txtApellido.Text = _usuarioSeleccionado.Apellido;
            txtNombre.Text = _usuarioSeleccionado.Nombre;
            txtEmail.Text = _usuarioSeleccionado.Email;
            SeleccionarRolEnCombo(_usuarioSeleccionado.Rol);
            txtUser.Text = _usuarioSeleccionado.Login;
            txtBloqueado.Text = TextoSiNo(_usuarioSeleccionado.Bloqueo);
            txtActivo.Text = TextoSiNo(_usuarioSeleccionado.Activo);
        }

        private void rbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked) CargarGrilla(soloActivos: true);
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked) CargarGrilla(soloActivos: false);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(true);
            dgvUsuarios.Enabled = false;
            ModoOperacion("Crear");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_usuarioSeleccionado == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.seleccionarUsuario"), IdiomaManager_GV42.T("general.advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HabilitarCampos(false);
            txtEmail.Enabled = true;
            comboBox1.Enabled = true;
            dgvUsuarios.Enabled = false;
            ModoOperacion("Modificar");
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (_usuarioSeleccionado == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.seleccionarUsuario"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HabilitarCampos(false);
            dgvUsuarios.Enabled = false;
            ModoOperacion("Desbloquear");
        }

        private void btnActivarDesactivar_Click(object sender, EventArgs e)
        {
            if (_usuarioSeleccionado == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.seleccionarUsuario"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HabilitarCampos(false);
            dgvUsuarios.Enabled = false;
            ModoOperacion("ActivarDesactivar");
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Las reglas de la BLL (email repetido, último administrador, permisos, etc.) llegan como
            // excepción con el motivo: se muestran como aviso en lugar del diálogo de error de .NET.
            try
            {
                switch (_modo)
                {
                    case "Crear":
                        Crear();
                        break;
                    case "Modificar":
                        Modificar();
                        break;
                    case "Desbloquear":
                        Desbloquear();
                        break;
                    case "ActivarDesactivar":
                        ActivarDesactivar();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try { ModoConsulta(); CargarGrilla(rbActivos.Checked); } catch { }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
