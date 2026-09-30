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
using BLL;

namespace PROYECTO_ING_DE_SOFTWARE
{

    public partial class FRMGestionUsuariosAdmin : Form, IObservadorIdioma_GV42
    {
        private readonly BLLUsuario_GV42 _bll;

        private string _modo = "Consulta";

        private Usuario_GV42 _usuarioSeleccionado = null;

        // true mientras la matriz muestra usuarios cargados desde un archivo XML
        // (no desde la base): en ese estado no se permite el ABM sobre esas filas.
        private bool _mostrandoXml = false;

        public FRMGestionUsuariosAdmin()
        {
            InitializeComponent();
            _bll = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();
            AplicarPermisos();

            // Largos máximos iguales a los de la base / reglas de negocio, y DNI solo con dígitos.
            txtDni.MaxLength = 8;
            txtNombre.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtApellido.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtEmail.MaxLength = Validaciones_GV42.MAX_EMAIL;
            txtDni.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
        }

        private void AplicarPermisos()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            var bllPermisos = new BLLPermisos_GV42();
            Rol_GV42 rolCompleto = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
            if (rolCompleto == null) return;

            if (btnCrear != null)
                btnCrear.Visible = rolCompleto.TienePermiso("Usuarios.Crear");
            if (btnModificar != null)
                btnModificar.Visible = rolCompleto.TienePermiso("Usuarios.Modificar");
            if (btnDesbloquear != null)
                btnDesbloquear.Visible = rolCompleto.TienePermiso("Usuarios.Desbloquear");
            if (btnActivarDesactivar != null)
                btnActivarDesactivar.Visible = rolCompleto.TienePermiso("Usuarios.Activar");

            // Serializar / des-serializar el maestro exige poder ver usuarios (también lo controla la BLL).
            bool puedeVer = rolCompleto.TienePermiso("Usuarios.Ver");
            if (btnSerializar != null) btnSerializar.Enabled = puedeVer;
            if (btnDeserializar != null) btnDeserializar.Enabled = puedeVer;
        }

        private void ConfigurarAyudas()
        {
            toolTipAyuda.ToolTipTitle = IdiomaManager_GV42.T("serializacion.tituloAyuda");
            toolTipAyuda.SetToolTip(btnActualizar, IdiomaManager_GV42.T("serializacion.ayudaActualizar"));
            toolTipAyuda.SetToolTip(btnLimpiar, IdiomaManager_GV42.T("serializacion.ayudaLimpiar"));
            toolTipAyuda.SetToolTip(btnSerializar, IdiomaManager_GV42.T("serializacion.ayudaSerializar"));
            toolTipAyuda.SetToolTip(btnDeserializar, IdiomaManager_GV42.T("serializacion.ayudaDeserializar"));
            toolTipAyuda.SetToolTip(btnUbicacionSerializar, IdiomaManager_GV42.T("serializacion.ayudaUbicacion"));
            toolTipAyuda.SetToolTip(btnUbicacionDeserializar, IdiomaManager_GV42.T("serializacion.ayudaArchivo"));
        }

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("usuarios.titulo");

            if (label1 != null) label1.Text = IdiomaManager_GV42.T("usuarios.dni");
            if (label2 != null) label2.Text = IdiomaManager_GV42.T("usuarios.apellido");
            if (label3 != null) label3.Text = IdiomaManager_GV42.T("usuarios.nombre");
            if (label4 != null) label4.Text = IdiomaManager_GV42.T("usuarios.email");
            if (label5 != null) label5.Text = IdiomaManager_GV42.T("usuarios.rol");
            if (label6 != null) label6.Text = IdiomaManager_GV42.T("usuarios.userName");
            if (label7 != null) label7.Text = IdiomaManager_GV42.T("usuarios.bloqueado");
            if (label8 != null) label8.Text = IdiomaManager_GV42.T("usuarios.activo");

            if (btnCrear != null) btnCrear.Text = IdiomaManager_GV42.T("usuarios.crear");
            if (btnModificar != null) btnModificar.Text = IdiomaManager_GV42.T("usuarios.modificar");
            if (btnDesbloquear != null) btnDesbloquear.Text = IdiomaManager_GV42.T("usuarios.desbloquear");
            if (btnActivarDesactivar != null) btnActivarDesactivar.Text = IdiomaManager_GV42.T("usuarios.activarDesactivar");
            if (btnAplicar != null) btnAplicar.Text = IdiomaManager_GV42.T("usuarios.aplicar");
            if (btnCancelar != null) btnCancelar.Text = IdiomaManager_GV42.T("usuarios.cancelar");
            if (btnSalir != null) btnSalir.Text = IdiomaManager_GV42.T("usuarios.salir");

            if (rbActivos != null) rbActivos.Text = IdiomaManager_GV42.T("usuarios.activos");
            if (rbTodos != null) rbTodos.Text = IdiomaManager_GV42.T("usuarios.todos");

            if (lblMensaje != null) lblMensaje.Text = TraducirModo(_modo);

            if (btnActualizar != null) btnActualizar.Text = IdiomaManager_GV42.T("usuarios.actualizar");
            if (btnLimpiar != null) btnLimpiar.Text = IdiomaManager_GV42.T("usuarios.limpiar");
            if (lblTituloMensaje != null) lblTituloMensaje.Text = IdiomaManager_GV42.T("usuarios.mensaje");
            if (lblTituloSerializacion != null) lblTituloSerializacion.Text = IdiomaManager_GV42.T("serializacion.titulo");
            if (btnSerializar != null) btnSerializar.Text = IdiomaManager_GV42.T("serializacion.serializar");
            if (btnDeserializar != null) btnDeserializar.Text = IdiomaManager_GV42.T("serializacion.deserializar");
            if (toolTipAyuda != null) ConfigurarAyudas();

            ConfigurarColumnasGrilla();
        }

        private string TraducirModo(string modo)
        {
            string etiquetaModo = IdiomaManager_GV42.T("usuarios.modo");
            if (_mostrandoXml) return etiquetaModo + " " + IdiomaManager_GV42.T("serializacion.modoVistaXml");
            return etiquetaModo + " " + modo;
        }

        private void FRMPrincipalAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillaSoloLectura();
                CargarRoles();
                ModoConsulta();
                // Al marcar rbActivos se dispara rbActivos_CheckedChanged, que ya carga la grilla
                // (antes la grilla se cargaba dos veces al abrir el formulario).
                if (rbActivos.Checked) CargarGrilla(soloActivos: true);
                else rbActivos.Checked = true;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado("cargar los usuarios", ex);
            }
        }

        private void ConfigurarGrillaSoloLectura()
        {
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToOrderColumns = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsuarios.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void CargarRoles()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
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

            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                if (row.DataBoundItem is Usuario_GV42 u && !u.Activo)
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
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

            if (dgvUsuarios.Columns.Contains("DNI"))
                dgvUsuarios.Columns["DNI"].HeaderText = IdiomaManager_GV42.T("usuarios.dni");
            if (dgvUsuarios.Columns.Contains("Apellido"))
                dgvUsuarios.Columns["Apellido"].HeaderText = IdiomaManager_GV42.T("usuarios.apellido");
            if (dgvUsuarios.Columns.Contains("Nombre"))
                dgvUsuarios.Columns["Nombre"].HeaderText = IdiomaManager_GV42.T("usuarios.nombre");
            if (dgvUsuarios.Columns.Contains("Login"))
                dgvUsuarios.Columns["Login"].HeaderText = IdiomaManager_GV42.T("usuarios.login");
            if (dgvUsuarios.Columns.Contains("RolNombre"))
                dgvUsuarios.Columns["RolNombre"].HeaderText = IdiomaManager_GV42.T("usuarios.rol");
            if (dgvUsuarios.Columns.Contains("Email"))
                dgvUsuarios.Columns["Email"].HeaderText = IdiomaManager_GV42.T("usuarios.email");
            if (dgvUsuarios.Columns.Contains("Bloqueo"))
                dgvUsuarios.Columns["Bloqueo"].HeaderText = IdiomaManager_GV42.T("usuarios.bloqueado");
            if (dgvUsuarios.Columns.Contains("Activo"))
                dgvUsuarios.Columns["Activo"].HeaderText = IdiomaManager_GV42.T("usuarios.activo");
        }

        private void ModoConsulta()
        {
            _modo = "Consulta";
            _mostrandoXml = false;
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
            HabilitarSerializacion(true);
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
            HabilitarSerializacion(false);
        }

        private void HabilitarSerializacion(bool habilitar)
        {
            btnActualizar.Enabled = habilitar;
            btnLimpiar.Enabled = habilitar;
            btnSerializar.Enabled = habilitar;
            btnDeserializar.Enabled = habilitar;
            btnUbicacionSerializar.Enabled = habilitar;
            btnUbicacionDeserializar.Enabled = habilitar;
        }

        // La matriz muestra datos de un XML: se puede navegar, volver a serializar o limpiar,
        // pero no hacer ABM, porque esas filas no son necesariamente las de la base.
        private void ModoVistaXml()
        {
            _mostrandoXml = true;
            _modo = "Consulta";
            HabilitarCampos(false);
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnDesbloquear.Enabled = false;
            btnActivarDesactivar.Enabled = false;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            rbActivos.Enabled = false;
            rbTodos.Enabled = false;
            dgvUsuarios.Enabled = true;
            HabilitarSerializacion(true);
            lblMensaje.Text = TraducirModo(_modo);
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

        private void btnCrear_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(true);
            dgvUsuarios.Enabled = false;
            ModoOperacion("Crear");
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
            MessageBox.Show(string.Format(IdiomaManager_GV42.T(claveMsj), _usuarioSeleccionado.Login),
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
            MessageBox.Show(string.Format(IdiomaManager_GV42.T("usuarios.usuarioDesbloqueado"), _usuarioSeleccionado.Login) +
                            "\n\nContraseña temporal: " + temporal + "\n(se le pedirá cambiarla al ingresar)",
                            IdiomaManager_GV42.T("general.exito"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
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

        private void Crear()
        {
            string dni = txtDni.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            Rol_GV42 rol = comboBox1.SelectedItem as Rol_GV42;

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(apellido) ||string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) ||rol == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("general.completarCampos"),IdiomaManager_GV42.T("general.advertencia"),MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                "Usuario: " + loginCreado + "\nContraseña inicial: " + loginCreado +
                                "\n(se le pedirá cambiarla en el primer ingreso)",
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
            txtBloqueado.Text = _usuarioSeleccionado.Bloqueo ? "Sí" : "No";
            txtActivo.Text = _usuarioSeleccionado.Activo ? "Sí" : "No";
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

        private void rbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked) CargarGrilla(soloActivos: true);
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked) CargarGrilla(soloActivos: false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoConsulta();
            CargarGrilla(rbActivos.Checked);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_usuarioSeleccionado == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("usuarios.seleccionarUsuario"),IdiomaManager_GV42.T("general.advertencia"),MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HabilitarCampos(false);
            txtEmail.Enabled = true;
            comboBox1.Enabled = true;
            dgvUsuarios.Enabled = false;
            ModoOperacion("Modificar");
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

        // ---------------------------------------------------------------------------------
        // Serialización XML (Caso de uso compartido: SERIALIZAR / DES-SERIALIZAR)
        // ---------------------------------------------------------------------------------

        private void MostrarMensaje(string texto, bool esError = false)
        {
            txtMensaje.ForeColor = esError ? Color.FromArgb(198, 40, 40) : Color.FromArgb(33, 33, 33);
            txtMensaje.Text = texto;
        }

        private string FiltroXml() => IdiomaManager_GV42.T("serializacion.filtroXml") + " (*.xml)|*.xml";

        private void btnUbicacionSerializar_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = IdiomaManager_GV42.T("serializacion.ayudaUbicacion");
                dlg.Filter = FiltroXml();
                dlg.DefaultExt = "xml";
                dlg.AddExtension = true;
                dlg.OverwritePrompt = true;
                dlg.FileName = "Usuarios_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xml";
                if (!string.IsNullOrWhiteSpace(txtRutaSerializar.Text))
                {
                    try
                    {
                        dlg.InitialDirectory = System.IO.Path.GetDirectoryName(txtRutaSerializar.Text);
                        dlg.FileName = System.IO.Path.GetFileName(txtRutaSerializar.Text);
                    }
                    catch { }
                }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtRutaSerializar.Text = dlg.FileName;
                    MostrarMensaje(IdiomaManager_GV42.T("serializacion.ubicacionElegida") + Environment.NewLine + dlg.FileName);
                }
            }
        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaSerializar.Text))
            {
                MostrarMensaje(IdiomaManager_GV42.T("serializacion.sinUbicacion"), true);
                return;
            }

            List<Usuario_GV42> visibles = dgvUsuarios.DataSource as List<Usuario_GV42>;
            try
            {
                int cantidad = _bll.SerializarUsuarios(visibles, txtRutaSerializar.Text);
                MostrarMensaje(string.Format(IdiomaManager_GV42.T("serializacion.okSerializar"),
                                             cantidad, txtRutaSerializar.Text));
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnUbicacionDeserializar_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = IdiomaManager_GV42.T("serializacion.ayudaArchivo");
                dlg.Filter = FiltroXml();
                dlg.CheckFileExists = true;
                dlg.Multiselect = false;
                if (!string.IsNullOrWhiteSpace(txtRutaSerializar.Text))
                {
                    try { dlg.InitialDirectory = System.IO.Path.GetDirectoryName(txtRutaSerializar.Text); } catch { }
                }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtRutaDeserializar.Text = dlg.FileName;
                    MostrarMensaje(IdiomaManager_GV42.T("serializacion.archivoElegido") + Environment.NewLine + dlg.FileName);
                }
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaDeserializar.Text))
            {
                MostrarMensaje(IdiomaManager_GV42.T("serializacion.sinArchivo"), true);
                return;
            }

            try
            {
                List<Usuario_GV42> usuarios = _bll.DeserializarUsuarios(txtRutaDeserializar.Text);
                MostrarEnGrilla(usuarios);
                ModoVistaXml();
                MostrarMensaje(string.Format(IdiomaManager_GV42.T("serializacion.okDeserializar"),
                                             usuarios.Count, System.IO.Path.GetFileName(txtRutaDeserializar.Text)));
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla(rbActivos.Checked);
                MostrarMensaje(IdiomaManager_GV42.T("serializacion.actualizado"));
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ModoConsulta();
            MostrarEnGrilla(new List<Usuario_GV42>());
            _usuarioSeleccionado = null;
            LimpiarCampos();
            txtRutaSerializar.Text = string.Empty;
            txtRutaDeserializar.Text = string.Empty;
            MostrarMensaje(IdiomaManager_GV42.T("serializacion.limpiado"));
        }
    }
}
