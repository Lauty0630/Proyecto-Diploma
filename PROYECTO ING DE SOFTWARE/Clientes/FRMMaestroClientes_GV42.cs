using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Maestro de clientes: ABM (añadir, modificar, eliminar) y serialización XML del maestro
    // (serializar la matriz a un archivo y des-serializar un archivo a la matriz).
    // El diseño está en FRMMaestroClientes_GV42.Designer.cs (Form Designer); acá va solo la lógica.
    public partial class FRMMaestroClientes_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLCliente_GV42 _bll = new BLLCliente_GV42();

        private string _modo = "Consulta";

        private Cliente_GV42 _clienteSeleccionado = null;

        // true mientras la matriz muestra clientes cargados desde un archivo XML
        // (no desde la base): en ese estado no se permite el ABM sobre esas filas.
        private bool _mostrandoXml = false;

        private bool _puedeGestionar = true;

        // Último mensaje de la caja "Mensaje": se guarda cómo armarlo para regenerarlo al cambiar el idioma.
        private Func<string> _generadorMensaje;
        private bool _mensajeEsError;

        #endregion

        #region Constructor

        public FRMMaestroClientes_GV42()
        {
            InitializeComponent();
            dgvClientes.AutoGenerateColumns = false;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();

            _puedeGestionar = _bll.PuedeGestionar();
            btnCrear.Visible = _puedeGestionar;
            btnModificar.Visible = _puedeGestionar;
            btnEliminar.Visible = _puedeGestionar;

            // Largos máximos iguales a los de la base / reglas de negocio (el DNI solo acepta dígitos: txtDni_KeyPress).
            txtDni.MaxLength = 8;
            txtNombre.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtApellido.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            txtEmail.MaxLength = Validaciones_GV42.MAX_EMAIL;
            txtTelefono.MaxLength = Validaciones_GV42.MAX_TELEFONO;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("clientes.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("clientes.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("clientes.subtitulo");

            lblTituloDatos.Text = IdiomaManager_GV42.T("clientes.tituloDatos");
            label1.Text = IdiomaManager_GV42.T("usuarios.dni");
            label2.Text = IdiomaManager_GV42.T("usuarios.apellido");
            label3.Text = IdiomaManager_GV42.T("usuarios.nombre");
            label4.Text = IdiomaManager_GV42.T("usuarios.email");
            label5.Text = IdiomaManager_GV42.T("clientes.telefono");
            lblTituloMensaje.Text = IdiomaManager_GV42.T("usuarios.mensaje");
            lblMensaje.Text = TraducirModo(_modo);

            lblTituloGrilla.Text = IdiomaManager_GV42.T("clientes.tituloGrilla");

            btnCrear.Text = IdiomaManager_GV42.T("clientes.anadir");
            btnModificar.Text = IdiomaManager_GV42.T("usuarios.modificar");
            btnEliminar.Text = IdiomaManager_GV42.T("clientes.eliminar");
            btnAplicar.Text = IdiomaManager_GV42.T("usuarios.aplicar");
            btnCancelar.Text = IdiomaManager_GV42.T("usuarios.cancelar");
            btnSalir.Text = IdiomaManager_GV42.T("usuarios.salir");

            lblTituloSerializacion.Text = IdiomaManager_GV42.T("serializacion.titulo");
            btnActualizar.Text = IdiomaManager_GV42.T("usuarios.actualizar");
            btnLimpiar.Text = IdiomaManager_GV42.T("usuarios.limpiar");
            btnSerializar.Text = IdiomaManager_GV42.T("serializacion.serializar");
            btnDeserializar.Text = IdiomaManager_GV42.T("serializacion.deserializar");
            ConfigurarAyudas();

            ConfigurarColumnasGrilla();
            RefrescarMensaje();
        }

        // Descripción de cada botón al ubicar el mouse encima.
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

        private string TraducirModo(string modo)
        {
            string etiquetaModo = IdiomaManager_GV42.T("usuarios.modo");
            if (_mostrandoXml) return etiquetaModo + ": " + IdiomaManager_GV42.T("serializacion.modoVistaXml");
            return etiquetaModo + ": " + IdiomaManager_GV42.TConDefecto("clientes.nombreModo" + modo, modo);
        }

        #endregion

        #region Carga de datos

        private void CargarGrilla()
        {
            MostrarEnGrilla(_bll.Listar());
        }

        private void MostrarEnGrilla(List<Cliente_GV42> lista)
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = lista ?? new List<Cliente_GV42>();

            if (dgvClientes.Rows.Count == 0)
            {
                _clienteSeleccionado = null;
                LimpiarCampos();
            }
        }

        // Encabezados traducidos de las columnas (definidas en el Designer).
        private void ConfigurarColumnasGrilla()
        {
            colDni.HeaderText = IdiomaManager_GV42.T("usuarios.dni");
            colApellido.HeaderText = IdiomaManager_GV42.T("usuarios.apellido");
            colNombre.HeaderText = IdiomaManager_GV42.T("usuarios.nombre");
            colEmail.HeaderText = IdiomaManager_GV42.T("usuarios.email");
            colTelefono.HeaderText = IdiomaManager_GV42.T("clientes.telefono");
        }

        #endregion

        #region Modos de pantalla

        private void ModoConsulta()
        {
            _modo = "Consulta";
            _mostrandoXml = false;
            lblMensaje.Text = TraducirModo(_modo);
            LimpiarCampos();
            HabilitarCampos(false);
            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            dgvClientes.Enabled = true;
            HabilitarSerializacion(true);
        }

        private void ModoOperacion(string modo)
        {
            _modo = modo;
            lblMensaje.Text = TraducirModo(modo);
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
            dgvClientes.Enabled = false;
            HabilitarSerializacion(false);
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
            btnEliminar.Enabled = false;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            dgvClientes.Enabled = true;
            HabilitarSerializacion(true);
            lblMensaje.Text = TraducirModo(_modo);
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

        private void HabilitarCampos(bool habilitar)
        {
            txtDni.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            txtEmail.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
        }

        private void LimpiarCampos()
        {
            txtDni.Text = txtApellido.Text = txtNombre.Text = txtEmail.Text = txtTelefono.Text = "";
        }

        private void MostrarSeleccionado()
        {
            if (_clienteSeleccionado == null) { LimpiarCampos(); return; }
            txtDni.Text = _clienteSeleccionado.DNI;
            txtApellido.Text = _clienteSeleccionado.Apellido;
            txtNombre.Text = _clienteSeleccionado.Nombre;
            txtEmail.Text = _clienteSeleccionado.Email;
            txtTelefono.Text = _clienteSeleccionado.Telefono;
        }

        #endregion

        #region Operaciones (Añadir / Modificar / Eliminar)

        private Cliente_GV42 ClienteDeLosCampos()
        {
            return new Cliente_GV42(txtDni.Text.Trim(), txtNombre.Text.Trim(), txtApellido.Text.Trim(),
                                    txtEmail.Text.Trim(), txtTelefono.Text.Trim());
        }

        private void Crear()
        {
            Cliente_GV42 cliente = ClienteDeLosCampos();
            _bll.Crear(cliente);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("clientes.okCreado", cliente.NombreCompleto));
        }

        private void Modificar()
        {
            Cliente_GV42 cliente = ClienteDeLosCampos();
            _bll.Modificar(cliente);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("clientes.okModificado", cliente.NombreCompleto));
        }

        private void Eliminar()
        {
            Cliente_GV42 cliente = _clienteSeleccionado;
            _bll.Eliminar(cliente.DNI);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("clientes.okEliminado", cliente.NombreCompleto));
        }

        #endregion

        #region Serialización XML

        // Caso de uso compartido: SERIALIZAR / DES-SERIALIZAR el maestro de clientes.

        // Muestra un texto en la caja "Mensaje". Se guarda la forma de armarlo para poder
        // regenerarlo en el otro idioma cuando cambia el idioma (ActualizarIdioma).
        private void MostrarMensaje(Func<string> generador, bool esError = false)
        {
            _generadorMensaje = generador;
            _mensajeEsError = esError;
            RefrescarMensaje();
        }

        private void RefrescarMensaje()
        {
            if (_generadorMensaje == null) return;
            // El color depende del resultado (error / normal), por eso se asigna en tiempo de ejecución.
            txtMensaje.ForeColor = _mensajeEsError ? Tema_GV42.Error : Tema_GV42.Texto;
            txtMensaje.Text = _generadorMensaje();
        }

        private string FiltroXml()
        {
            return IdiomaManager_GV42.T("serializacion.filtroXml") + " (*.xml)|*.xml";
        }

        #endregion

        #region Eventos

        private void FRMMaestroClientes_GV42_Load(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El DNI solo admite dígitos.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;

            _clienteSeleccionado = dgvClientes.CurrentRow.DataBoundItem as Cliente_GV42;
            MostrarSeleccionado();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ModoOperacion("Crear");
            LimpiarCampos();
            HabilitarCampos(true);
            txtDni.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("clientes.seleccionar"), true);
                return;
            }

            ModoOperacion("Modificar");
            MostrarSeleccionado();
            HabilitarCampos(true);
            txtDni.Enabled = false;   // el DNI es la clave del cliente
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("clientes.seleccionar"), true);
                return;
            }

            ModoOperacion("Eliminar");
            MostrarSeleccionado();
            HabilitarCampos(false);
            MostrarMensaje(() => IdiomaManager_GV42.T("clientes.confirmarEliminar"));
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Las reglas de la BLL (DNI repetido, datos inválidos, cliente con reservas, permisos) llegan
            // como excepción con el motivo: se muestran en la caja "Mensaje" y la pantalla queda como estaba.
            try
            {
                switch (_modo)
                {
                    case "Crear": Crear(); break;
                    case "Modificar": Modificar(); break;
                    case "Eliminar": Eliminar(); break;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla();
                MostrarMensaje(() => IdiomaManager_GV42.T("clientes.cancelado"));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUbicacionSerializar_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = IdiomaManager_GV42.T("serializacion.ayudaUbicacion");
                dlg.Filter = FiltroXml();
                dlg.DefaultExt = "xml";
                dlg.AddExtension = true;
                dlg.OverwritePrompt = true;
                dlg.FileName = "Clientes_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xml";
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
                    string ruta = dlg.FileName;
                    txtRutaSerializar.Text = ruta;
                    MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.ubicacionElegida") + Environment.NewLine + ruta);
                }
            }
        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaSerializar.Text))
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.sinUbicacion"), true);
                return;
            }

            // Se serializa lo que se ve en la matriz.
            List<Cliente_GV42> visibles = dgvClientes.DataSource as List<Cliente_GV42>;
            string ruta = txtRutaSerializar.Text;
            try
            {
                int cantidad = _bll.Serializar(visibles, ruta);
                MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.okSerializar", cantidad, ruta));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
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
                    string ruta = dlg.FileName;
                    txtRutaDeserializar.Text = ruta;
                    MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.archivoElegido") + Environment.NewLine + ruta);
                }
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaDeserializar.Text))
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.sinArchivo"), true);
                return;
            }

            try
            {
                List<Cliente_GV42> clientes = _bll.Deserializar(txtRutaDeserializar.Text);
                MostrarEnGrilla(clientes);
                ModoVistaXml();
                int cantidad = clientes.Count;
                string archivo = System.IO.Path.GetFileName(txtRutaDeserializar.Text);
                MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.okDeserializar", cantidad, archivo));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        // Vuelve a consultar la base, tal como al entrar a la pantalla.
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla();
                MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.actualizado"));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        // Deja la matriz en blanco (no borra nada de la base ni de los archivos).
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ModoConsulta();
            MostrarEnGrilla(new List<Cliente_GV42>());
            _clienteSeleccionado = null;
            LimpiarCampos();
            txtRutaSerializar.Text = string.Empty;
            txtRutaDeserializar.Text = string.Empty;
            MostrarMensaje(() => IdiomaManager_GV42.T("serializacion.limpiado"));
        }

        #endregion
    }
}
