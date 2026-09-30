using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // RFN 1 - Reservar vuelo. Un solo formulario para los dos canales:
    //  - Vendedor (patente Reservas.Generar): incluye el paso de buscar/registrar al cliente.
    //  - Pasajero con cuenta (solo Reservas.GenerarPropia): ese paso se omite; la reserva queda a su
    //    nombre y sus datos se toman de la sesión activa.
    // El modo se decide por patentes, no por el nombre del rol. La capa de negocio
    // (BLLReserva_GV42.GenerarReserva) vuelve a validar el canal según la sesión.
    public class FRMReservarVuelo_GV42 : Form
    {
        private class DatosPasajero
        {
            public TextBox Dni, Nombre, Apellido, Email, Telefono;
            public bool Autocompletado;          // los datos salieron de la base (DNI ya registrado)
            public string UltimoDniBuscado = string.Empty;
        }

        // Largos máximos de los campos de persona (coinciden con las columnas de la base).
        private static void LimitarCamposPersona(TextBox dni, TextBox nombre, TextBox apellido, TextBox email, TextBox telefono)
        {
            dni.MaxLength = 8;
            nombre.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            apellido.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            email.MaxLength = Validaciones_GV42.MAX_EMAIL;
            telefono.MaxLength = 20;
            // En el DNI solo se aceptan dígitos (se ignora cualquier otra tecla, salvo borrar/pegar).
            dni.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
        }

        private static void Bloquear(TextBox txt, bool bloquear)
        {
            txt.ReadOnly = bloquear;
            txt.BackColor = bloquear ? Tema_GV42.Fondo : Color.White;
        }

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private readonly bool _esVendedor;

        // Paso 1 - búsqueda
        private ComboBox cmbOrigen, cmbDestino, cmbTipoViaje, cmbClaseFiltro;
        private DateTimePicker dtSalida, dtRegreso;
        private NumericUpDown numPasajeros;
        private DataGridView dgvVuelos;
        private List<VueloClase_GV42> _resultados = new List<VueloClase_GV42>();
        private VueloClase_GV42 _vueloElegido;

        // Paso 2 - cliente (solo vendedor)
        private TextBox txtDniCliente, txtNombreCliente, txtApellidoCliente, txtEmailCliente, txtTelefonoCliente;
        private Button btnBuscarCliente, btnRegistrarCliente;
        private Pasajero_GV42 _clienteElegido;

        // Paso 3 - pasajeros
        private Panel pnlListaPasajeros;
        private List<DatosPasajero> _pasajeros = new List<DatosPasajero>();

        // Paso 4 - asientos
        private CtrlButacas_GV42 ctrlButacas;
        private Label lblPasajeroActual;
        private Button btnPasajeroAnterior, btnPasajeroSiguiente;
        private List<AsientoDisponibilidad_GV42> _mapaAsientos;
        private Dictionary<int, Asiento_GV42> _asientoPorPasajero = new Dictionary<int, Asiento_GV42>();
        private int _indicePasajeroActivo;

        // Paso 5 - adicionales
        private Panel pnlAdicionales;
        private List<(TipoAdicional_GV42 tipo, CheckBox chk, NumericUpDown cant, NumericUpDown costo)> _filasAdicionales
            = new List<(TipoAdicional_GV42, CheckBox, NumericUpDown, NumericUpDown)>();

        // Paso 6 - resumen / confirmación
        private Label lblResumen;
        private Panel pnlResultado;
        private Label lblResultado;
        private Reserva_GV42 _reservaGenerada;

        private Panel pnlContenidoPasos;
        private Label lblTituloPaso;
        private Button btnAtras, btnSiguiente;
        private List<Panel> _pasos;
        private int _pasoActual;

        public FRMReservarVuelo_GV42()
        {
            _esVendedor = _bll.PuedeGenerarParaTerceros();

            ConstruirUI();
            try
            {
                CargarAeropuertos();
                CargarAdicionales();
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado("cargar los aeropuertos y servicios", ex);
            }
            IrAPaso(0);

            // Si se cambia un filtro después de buscar, la lista deja de corresponder a lo pedido:
            // se limpia para obligar a buscar de nuevo (antes se podía avanzar con 9 pasajeros sobre
            // un vuelo buscado para 2 y el error recién aparecía al confirmar).
            EventHandler invalidar = (s, e) => InvalidarBusqueda();
            cmbOrigen.SelectedIndexChanged += invalidar;
            cmbDestino.SelectedIndexChanged += invalidar;
            dtSalida.ValueChanged += invalidar;
            cmbTipoViaje.SelectedIndexChanged += invalidar;
            dtRegreso.ValueChanged += invalidar;
            numPasajeros.ValueChanged += invalidar;
            cmbClaseFiltro.SelectedIndexChanged += invalidar;

            // El regreso no puede ser anterior a la salida.
            dtSalida.ValueChanged += (s, e) =>
            {
                if (dtRegreso.Value.Date < dtSalida.Value.Date) dtRegreso.Value = dtSalida.Value.Date;
                dtRegreso.MinDate = dtSalida.Value.Date;
            };
        }

        private void InvalidarBusqueda()
        {
            if (_resultados == null || _resultados.Count == 0) return;
            _resultados = new List<VueloClase_GV42>();
            dgvVuelos.DataSource = null;
            _vueloElegido = null;
        }

        // ------------------------------------------------------------------ UI general

        private void ConstruirUI()
        {
            Text = "Reservar vuelo";
            BackColor = Tema_GV42.Fondo;
            ClientSize = new Size(1040, 640);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);

            lblTituloPaso = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                Font = Tema_GV42.FuenteTitulo,
                ForeColor = Tema_GV42.Acento,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };

            var pnlNav = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Tema_GV42.Fondo };
            btnAtras = new Button { Text = "< Atrás", Size = new Size(120, 36), Location = new Point(20, 12) };
            Tema_GV42.EstilizarBotonSecundario(btnAtras);
            btnAtras.Click += (s, e) =>
            {
                try { IrAPaso(_pasoActual - 1); }
                catch (NegocioException_GV42 ex) { MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (Exception ex) { Tema_GV42.MostrarErrorInesperado("volver al paso anterior", ex); }
            };

            btnSiguiente = new Button { Text = "Siguiente >", Size = new Size(140, 36) };
            Tema_GV42.EstilizarBotonPrimario(btnSiguiente);
            btnSiguiente.Click += btnSiguiente_Click;
            pnlNav.Controls.Add(btnAtras);
            pnlNav.Controls.Add(btnSiguiente);
            pnlNav.Resize += (s, e) => btnSiguiente.Location = new Point(pnlNav.Width - 160, 12);
            btnSiguiente.Location = new Point(pnlNav.Width - 160, 12);
            btnSiguiente.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            pnlContenidoPasos = new Panel { Dock = DockStyle.Fill, BackColor = Tema_GV42.Fondo, Padding = new Padding(20) };

            Controls.Add(pnlContenidoPasos);
            Controls.Add(pnlNav);
            Controls.Add(lblTituloPaso);

            _pasos = new List<Panel> { ConstruirPasoBusqueda() };
            if (_esVendedor) _pasos.Add(ConstruirPasoCliente());
            _pasos.Add(ConstruirPasoPasajeros());
            _pasos.Add(ConstruirPasoAsientos());
            _pasos.Add(ConstruirPasoAdicionales());
            _pasos.Add(ConstruirPasoResumen());
            _pasos.Add(ConstruirPasoResultado());
        }

        private Panel EnvolverEnCard(Control contenido)
        {
            var card = Tema_GV42.CrearCard();
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(20);
            contenido.Dock = DockStyle.Fill;
            card.Controls.Add(contenido);
            return card;
        }

        private readonly string[] _titulosPaso = {
            "Paso 1 de 6 · Buscar vuelo",
            "Paso 2 de 6 · Cliente",
            "Paso 3 de 6 · Pasajeros",
            "Paso 4 de 6 · Elegir asiento",
            "Paso 5 de 6 · Servicios adicionales",
            "Paso 6 de 6 · Confirmar reserva",
            "Reserva generada"
        };

        private void IrAPaso(int indice)
        {
            if (indice < 0 || indice >= _pasos.Count) return;
            _pasoActual = indice;

            pnlContenidoPasos.Controls.Clear();
            pnlContenidoPasos.Controls.Add(_pasos[_pasoActual]);

            lblTituloPaso.Text = _titulosPaso[Math.Min(indice, _esVendedor ? indice : indice + 1)];
            // Si no es vendedor, el paso "Cliente" no existe: se ajusta el título usando el índice real.
            lblTituloPaso.Text = TituloParaIndice(indice);

            btnAtras.Visible = indice > 0 && indice < _pasos.Count - 1;
            bool esUltimo = indice == _pasos.Count - 1;
            btnSiguiente.Visible = !esUltimo;
            btnSiguiente.Text = (indice == _pasos.Count - 2) ? "Confirmar reserva" : "Siguiente >";

            if (EsPasoAsientos(indice)) PrepararPasoAsientos();
        }

        private string TituloParaIndice(int indice)
        {
            int pasoCliente = _esVendedor ? 1 : -1;
            if (indice == 0) return "Paso 1 · Buscar vuelo";
            if (indice == pasoCliente) return "Paso 2 · Cliente";
            if (indice == PasoPasajeros()) return "Paso · Pasajeros";
            if (indice == PasoAsientos()) return "Paso · Elegir asiento";
            if (indice == PasoAdicionales()) return "Paso · Servicios adicionales";
            if (indice == PasoResumen()) return "Paso · Confirmar reserva";
            return "Reserva generada";
        }

        private int PasoPasajeros() => _esVendedor ? 2 : 1;
        private int PasoAsientos() => PasoPasajeros() + 1;
        private int PasoAdicionales() => PasoAsientos() + 1;
        private int PasoResumen() => PasoAdicionales() + 1;
        private int PasoResultado() => PasoResumen() + 1;
        private bool EsPasoAsientos(int indice) => indice == PasoAsientos();

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pasoActual == 0) ValidarYAvanzarBusqueda();
                else if (_esVendedor && _pasoActual == 1) ValidarYAvanzarCliente();
                else if (_pasoActual == PasoPasajeros()) ValidarYAvanzarPasajeros();
                else if (_pasoActual == PasoAsientos()) ValidarYAvanzarAsientos();
                else if (_pasoActual == PasoAdicionales()) { ArmarResumen(); IrAPaso(_pasoActual + 1); }
                else if (_pasoActual == PasoResumen()) ConfirmarReserva();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------ Paso 1: búsqueda

        private Panel ConstruirPasoBusqueda()
        {
            var contenido = new Panel();

            var lblOrigen = Tema_GV42.CrearLabel("Origen");
            lblOrigen.Location = new Point(0, 0);
            cmbOrigen = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, 20), Size = new Size(200, 24) };

            var lblDestino = Tema_GV42.CrearLabel("Destino");
            lblDestino.Location = new Point(220, 0);
            cmbDestino = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(220, 20), Size = new Size(200, 24) };

            var lblFecha = Tema_GV42.CrearLabel("Fecha de salida");
            lblFecha.Location = new Point(440, 0);
            dtSalida = new DateTimePicker { Location = new Point(440, 20), Size = new Size(160, 24), Format = DateTimePickerFormat.Short, MinDate = DateTime.Today };

            var lblTipoViaje = Tema_GV42.CrearLabel("Tipo de viaje");
            lblTipoViaje.Location = new Point(0, 60);
            cmbTipoViaje = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, 80), Size = new Size(200, 24) };
            cmbTipoViaje.Items.Add("Ida");
            cmbTipoViaje.Items.Add("Ida y Vuelta");
            cmbTipoViaje.SelectedIndex = 0;
            cmbTipoViaje.SelectedIndexChanged += (s, e) => dtRegreso.Enabled = cmbTipoViaje.SelectedIndex == 1;

            var lblRegreso = Tema_GV42.CrearLabel("Fecha de regreso");
            lblRegreso.Location = new Point(220, 60);
            dtRegreso = new DateTimePicker { Location = new Point(220, 80), Size = new Size(160, 24), Format = DateTimePickerFormat.Short, MinDate = DateTime.Today, Enabled = false };

            var lblPax = Tema_GV42.CrearLabel("Cantidad de pasajeros");
            lblPax.Location = new Point(440, 60);
            numPasajeros = new NumericUpDown { Location = new Point(440, 80), Size = new Size(80, 24), Minimum = 1, Maximum = 9, Value = 1 };

            var lblClaseFiltro = Tema_GV42.CrearLabel("Clase");
            lblClaseFiltro.Location = new Point(620, 60);
            cmbClaseFiltro = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(620, 80), Size = new Size(160, 24) };
            cmbClaseFiltro.Items.Add("Todas las clases");
            cmbClaseFiltro.Items.Add(ClaseVuelo_GV42.Economica);
            cmbClaseFiltro.Items.Add(ClaseVuelo_GV42.Ejecutiva);
            cmbClaseFiltro.Items.Add(ClaseVuelo_GV42.Primera);
            cmbClaseFiltro.Format += (s, e) => { if (e.ListItem is ClaseVuelo_GV42 c) e.Value = c.Texto(); };
            cmbClaseFiltro.SelectedIndex = 0;

            var btnBuscar = new Button { Text = "Buscar vuelos", Location = new Point(0, 120), Size = new Size(160, 36) };
            Tema_GV42.EstilizarBotonPrimario(btnBuscar);
            btnBuscar.Click += btnBuscarVuelos_Click;

            // La grilla ocupa todo el espacio que queda debajo de los filtros (Dock), así se ve completa
            // con cualquier tamaño de ventana. Antes tenía tamaño fijo (800x300) con anclajes a los 4 lados
            // sobre un panel que todavía medía 200x100: al estirarse quedaba más grande que la ventana y se
            // cortaban las últimas columnas y filas.
            dgvVuelos = new DataGridView { Dock = DockStyle.Fill };
            Tema_GV42.EstilizarGrilla(dgvVuelos);
            dgvVuelos.AutoGenerateColumns = false;
            dgvVuelos.ScrollBars = ScrollBars.Both;
            // Cada columna toma el ancho de su contenido; Origen y Destino se reparten el espacio que sobra.
            // Si la ventana es angosta, aparece la barra de desplazamiento horizontal en vez de recortar.
            dgvVuelos.Columns.Add(ColumnaVuelos("CodigoVuelo", "Vuelo", null));
            dgvVuelos.Columns.Add(ColumnaVuelos("AerolineaNombre", "Aerolínea", null));
            dgvVuelos.Columns.Add(ColumnaVuelos("OrigenDescripcion", "Origen", null, relleno: true));
            dgvVuelos.Columns.Add(ColumnaVuelos("DestinoDescripcion", "Destino", null, relleno: true));
            dgvVuelos.Columns.Add(ColumnaVuelos("FechaHoraSalida", "Salida", "dd/MM HH:mm"));
            dgvVuelos.Columns.Add(ColumnaVuelos("FechaHoraLlegada", "Llegada", "HH:mm"));
            dgvVuelos.Columns.Add(ColumnaVuelos("ClaseTexto", "Clase", null));
            dgvVuelos.Columns.Add(ColumnaVuelos("PrecioBase", "Precio", "C2", DataGridViewContentAlignment.MiddleRight));
            dgvVuelos.Columns.Add(ColumnaVuelos("AsientosDisponibles", "Disp.", null, DataGridViewContentAlignment.MiddleCenter));

            var pnlFiltros = new Panel { Dock = DockStyle.Top, Height = 170 };
            pnlFiltros.Controls.AddRange(new Control[] {
                lblOrigen, cmbOrigen, lblDestino, cmbDestino, lblFecha, dtSalida,
                lblTipoViaje, cmbTipoViaje, lblRegreso, dtRegreso, lblPax, numPasajeros,
                lblClaseFiltro, cmbClaseFiltro,
                btnBuscar
            });

            // Orden de acoplamiento: primero la grilla (Fill) y después los filtros (Top).
            contenido.Controls.Add(dgvVuelos);
            contenido.Controls.Add(pnlFiltros);

            return EnvolverEnCard(contenido);
        }

        private static DataGridViewTextBoxColumn ColumnaVuelos(string propiedad, string titulo, string formato,
                                                               DataGridViewContentAlignment? alineacion = null, bool relleno = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                HeaderText = titulo,
                AutoSizeMode = relleno ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = relleno ? 120 : 45
            };
            if (formato != null) col.DefaultCellStyle.Format = formato;
            if (alineacion.HasValue) col.DefaultCellStyle.Alignment = alineacion.Value;
            return col;
        }

        private void CargarAeropuertos()
        {
            var aeropuertos = _bll.ListarAeropuertos();
            cmbOrigen.DisplayMember = "Descripcion";
            cmbDestino.DisplayMember = "Descripcion";
            cmbOrigen.DataSource = aeropuertos;
            cmbDestino.DataSource = new List<Aeropuerto_GV42>(aeropuertos);
        }

        private void btnBuscarVuelos_Click(object sender, EventArgs e)
        {
            var criterio = new CriterioBusquedaVuelo_GV42
            {
                IdOrigen = ((Aeropuerto_GV42)cmbOrigen.SelectedItem)?.Id ?? 0,
                IdDestino = ((Aeropuerto_GV42)cmbDestino.SelectedItem)?.Id ?? 0,
                FechaSalida = dtSalida.Value.Date,
                CantidadPasajeros = (int)numPasajeros.Value,
                TipoViaje = cmbTipoViaje.SelectedIndex == 1 ? TipoViaje_GV42.IdaYVuelta : TipoViaje_GV42.Ida,
                FechaRegreso = cmbTipoViaje.SelectedIndex == 1 ? (DateTime?)dtRegreso.Value.Date : null,
                Clase = cmbClaseFiltro.SelectedIndex > 0 ? (ClaseVuelo_GV42?)cmbClaseFiltro.SelectedItem : null
            };

            try
            {
                _resultados = _bll.BuscarVuelosDisponibles(criterio);
                dgvVuelos.DataSource = null;
                dgvVuelos.DataSource = _resultados;
                // Que el usuario elija el vuelo: antes quedaba seleccionada la primera fila y
                // "Siguiente" avanzaba con un vuelo que no se había elegido.
                dgvVuelos.ClearSelection();
                dgvVuelos.CurrentCell = null;

                if (_resultados.Count == 0)
                    MessageBox.Show("No hay vuelos disponibles para esa búsqueda.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado("buscar vuelos", ex);
            }
        }

        private void ValidarYAvanzarBusqueda()
        {
            if (dgvVuelos.SelectedRows.Count == 0)
                throw new NegocioException_GV42("Elegí un vuelo de la lista antes de continuar.");

            _vueloElegido = (VueloClase_GV42)dgvVuelos.SelectedRows[0].DataBoundItem;
            ReconstruirFilasPasajeros();
            IrAPaso(_pasoActual + 1);
        }

        // ------------------------------------------------------------ Paso 2: cliente (vendedor)

        private Panel ConstruirPasoCliente()
        {
            var contenido = new Panel();

            var lblDni = Tema_GV42.CrearLabel("DNI del cliente");
            lblDni.Location = new Point(0, 0);
            txtDniCliente = Tema_GV42.CrearTextBox();
            txtDniCliente.Location = new Point(0, 20);
            txtDniCliente.Size = new Size(160, 24);

            btnBuscarCliente = new Button { Text = "Buscar", Location = new Point(170, 19), Size = new Size(100, 26) };
            Tema_GV42.EstilizarBotonSecundario(btnBuscarCliente);
            btnBuscarCliente.Click += btnBuscarCliente_Click;

            var lblNombre = Tema_GV42.CrearLabel("Nombre"); lblNombre.Location = new Point(0, 60);
            txtNombreCliente = Tema_GV42.CrearTextBox(); txtNombreCliente.Location = new Point(0, 80); txtNombreCliente.Size = new Size(260, 24);

            var lblApellido = Tema_GV42.CrearLabel("Apellido"); lblApellido.Location = new Point(280, 60);
            txtApellidoCliente = Tema_GV42.CrearTextBox(); txtApellidoCliente.Location = new Point(280, 80); txtApellidoCliente.Size = new Size(260, 24);

            var lblEmail = Tema_GV42.CrearLabel("Email"); lblEmail.Location = new Point(0, 120);
            txtEmailCliente = Tema_GV42.CrearTextBox(); txtEmailCliente.Location = new Point(0, 140); txtEmailCliente.Size = new Size(260, 24);

            var lblTelefono = Tema_GV42.CrearLabel("Teléfono"); lblTelefono.Location = new Point(280, 120);
            txtTelefonoCliente = Tema_GV42.CrearTextBox(); txtTelefonoCliente.Location = new Point(280, 140); txtTelefonoCliente.Size = new Size(260, 24);

            btnRegistrarCliente = new Button { Text = "Registrar cliente nuevo", Location = new Point(0, 180), Size = new Size(220, 36), Visible = false };
            Tema_GV42.EstilizarBotonPrimario(btnRegistrarCliente);
            btnRegistrarCliente.Click += btnRegistrarCliente_Click;

            var lblAyuda = new Label
            {
                Text = "Buscá al cliente por DNI. Si no existe, completá sus datos y registralo.",
                Location = new Point(0, 230),
                Size = new Size(540, 40),
                Font = Tema_GV42.FuenteSubtitulo,
                ForeColor = Tema_GV42.Texto
            };

            contenido.Controls.AddRange(new Control[] {
                lblDni, txtDniCliente, btnBuscarCliente,
                lblNombre, txtNombreCliente, lblApellido, txtApellidoCliente,
                lblEmail, txtEmailCliente, lblTelefono, txtTelefonoCliente,
                btnRegistrarCliente, lblAyuda
            });

            LimitarCamposPersona(txtDniCliente, txtNombreCliente, txtApellidoCliente, txtEmailCliente, txtTelefonoCliente);
            // Si se cambia el DNI después de buscar, hay que volver a buscar: antes se podía buscar un DNI,
            // cambiarlo y registrar al cliente con el DNI nuevo pero con los datos precargados del anterior.
            txtDniCliente.TextChanged += (s, e) =>
            {
                if (_clienteElegido == null && !btnRegistrarCliente.Visible) return;
                _clienteElegido = null;
                btnRegistrarCliente.Visible = false;
                txtNombreCliente.Clear(); txtApellidoCliente.Clear(); txtEmailCliente.Clear(); txtTelefonoCliente.Clear();
                PonerDatosClienteSoloLectura(true);
            };
            txtDniCliente.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnBuscarCliente_Click(s, e); } };

            PonerDatosClienteSoloLectura(true);
            return EnvolverEnCard(contenido);
        }

        private void PonerDatosClienteSoloLectura(bool soloLectura)
        {
            txtNombreCliente.ReadOnly = soloLectura;
            txtApellidoCliente.ReadOnly = soloLectura;
            txtEmailCliente.ReadOnly = soloLectura;
            txtTelefonoCliente.ReadOnly = soloLectura;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string dni = txtDniCliente.Text.Trim();
            try
            {
                _clienteElegido = null;
                Pasajero_GV42 encontrado = _bll.BuscarPasajero(dni);
                if (encontrado != null)
                {
                    // Ya está registrado: se usa tal cual, sin volver a cargarlo.
                    _clienteElegido = encontrado;
                    MostrarDatosCliente(encontrado);
                    PonerDatosClienteSoloLectura(true);
                    btnRegistrarCliente.Visible = false;
                    return;
                }

                // No está en Pasajero: si tiene cuenta de usuario, se precargan sus datos.
                Pasajero_GV42 deUsuario = _bll.PrecargarDesdeUsuario(dni);
                if (deUsuario != null)
                {
                    MostrarDatosCliente(deUsuario);
                    PonerDatosClienteSoloLectura(true);
                    txtTelefonoCliente.ReadOnly = false;   // Usuario no guarda teléfono: se completa acá.
                    btnRegistrarCliente.Visible = true;
                    MessageBox.Show("Ese DNI tiene una cuenta de usuario. Se precargaron sus datos: " +
                        "completá el teléfono y registralo para poder reservar.",
                        "Datos precargados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtNombreCliente.Clear(); txtApellidoCliente.Clear(); txtEmailCliente.Clear(); txtTelefonoCliente.Clear();
                    PonerDatosClienteSoloLectura(false);
                    btnRegistrarCliente.Visible = true;
                    MessageBox.Show("No existe una persona registrada con ese DNI. Completá sus datos para registrarla.",
                        "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarDatosCliente(Pasajero_GV42 p)
        {
            txtNombreCliente.Text = p.Nombre;
            txtApellidoCliente.Text = p.Apellido;
            txtEmailCliente.Text = p.Email;
            txtTelefonoCliente.Text = p.Telefono;
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = new Pasajero_GV42
                {
                    DNI = txtDniCliente.Text.Trim(),
                    Nombre = txtNombreCliente.Text.Trim(),
                    Apellido = txtApellidoCliente.Text.Trim(),
                    Email = txtEmailCliente.Text.Trim(),
                    Telefono = txtTelefonoCliente.Text.Trim()
                };
                _bll.RegistrarPasajero(cliente);
                _clienteElegido = cliente;
                PonerDatosClienteSoloLectura(true);
                btnRegistrarCliente.Visible = false;
                MessageBox.Show("Cliente registrado.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ValidarYAvanzarCliente()
        {
            if (_clienteElegido == null)
                throw new NegocioException_GV42("Buscá o registrá al cliente antes de continuar.");
            IrAPaso(_pasoActual + 1);
        }

        // ------------------------------------------------------------ Paso 3: pasajeros

        private Panel ConstruirPasoPasajeros()
        {
            pnlListaPasajeros = new Panel { AutoScroll = true };
            return EnvolverEnCard(pnlListaPasajeros);
        }

        private void ReconstruirFilasPasajeros()
        {
            pnlListaPasajeros.Controls.Clear();
            _pasajeros.Clear();

            int cantidad = (int)numPasajeros.Value;
            int y = 0;
            for (int i = 0; i < cantidad; i++)
            {
                var grupo = new GroupBox
                {
                    Text = "Pasajero " + (i + 1),
                    Location = new Point(0, y),
                    Size = new Size(760, 90),
                    ForeColor = Tema_GV42.Acento,
                    Font = Tema_GV42.FuenteLabel
                };

                var dp = new DatosPasajero
                {
                    Dni = Tema_GV42.CrearTextBox(),
                    Nombre = Tema_GV42.CrearTextBox(),
                    Apellido = Tema_GV42.CrearTextBox(),
                    Email = Tema_GV42.CrearTextBox(),
                    Telefono = Tema_GV42.CrearTextBox()
                };

                UbicarConEtiqueta(grupo, "DNI", dp.Dni, 10, 24, 110);
                UbicarConEtiqueta(grupo, "Nombre", dp.Nombre, 130, 24, 140);
                UbicarConEtiqueta(grupo, "Apellido", dp.Apellido, 280, 24, 140);
                UbicarConEtiqueta(grupo, "Email", dp.Email, 430, 24, 180);
                UbicarConEtiqueta(grupo, "Teléfono", dp.Telefono, 620, 24, 120);
                LimitarCamposPersona(dp.Dni, dp.Nombre, dp.Apellido, dp.Email, dp.Telefono);

                // Vendedor: al salir del DNI se buscan los datos registrados de esa persona (como pasajero
                // o como usuario) y se completan bloqueados, para que no se pueda cargar un DNI existente
                // con otro nombre. El cliente autogestionado no puede consultar datos de terceros: en su
                // caso la validación se hace al pasar de paso, sin mostrar a nombre de quién está el DNI.
                if (_esVendedor)
                {
                    DatosPasajero fila = dp;
                    dp.Dni.Leave += (s, e) => AutocompletarPasajero(fila);
                }

                // El titular de la cuenta ya está identificado por su sesión: sus datos salen de ahí y no
                // se le pide que los vuelva a tipear (ni que se "registre"). Para pasajeros adicionales sí se
                // piden datos completos (pueden ser personas distintas del titular).
                if (i == 0 && !_esVendedor)
                {
                    Pasajero_GV42 titular = _bll.ObtenerTitularDeSesion();
                    dp.Dni.Text = titular.DNI; dp.Nombre.Text = titular.Nombre; dp.Apellido.Text = titular.Apellido;
                    dp.Email.Text = titular.Email; dp.Telefono.Text = titular.Telefono;

                    foreach (var txt in new[] { dp.Dni, dp.Nombre, dp.Apellido, dp.Email })
                        Bloquear(txt, true);
                    // Usuario no guarda teléfono: solo se pide si nunca reservó antes.
                    if (!string.IsNullOrWhiteSpace(titular.Telefono))
                        Bloquear(dp.Telefono, true);
                    grupo.Text = "Pasajero 1 (vos)";
                }

                _pasajeros.Add(dp);
                pnlListaPasajeros.Controls.Add(grupo);
                y += 100;
            }
        }

        private void UbicarConEtiqueta(Control padre, string etiqueta, TextBox txt, int x, int y, int ancho)
        {
            var lbl = Tema_GV42.CrearLabel(etiqueta);
            lbl.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lbl.Location = new Point(x, y);
            txt.Location = new Point(x, y + 16);
            txt.Size = new Size(ancho, 24);
            padre.Controls.Add(lbl);
            padre.Controls.Add(txt);
        }

        private void AutocompletarPasajero(DatosPasajero dp)
        {
            string dni = dp.Dni.Text.Trim();
            if (dni == dp.UltimoDniBuscado) return;
            dp.UltimoDniBuscado = dni;

            // Si antes se había autocompletado con otro DNI, esos datos ya no corresponden.
            if (dp.Autocompletado)
            {
                foreach (var txt in new[] { dp.Nombre, dp.Apellido, dp.Email, dp.Telefono })
                {
                    txt.Clear();
                    Bloquear(txt, false);
                }
                dp.Autocompletado = false;
            }

            if (!Validaciones_GV42.EsDniValido(dni)) return;

            try
            {
                Pasajero_GV42 registrado = _bll.BuscarPasajero(dni);
                bool esPasajero = registrado != null;
                if (registrado == null) registrado = _bll.PrecargarDesdeUsuario(dni);
                if (registrado == null) return;   // persona nueva: se cargan los datos a mano

                dp.Nombre.Text = registrado.Nombre;
                dp.Apellido.Text = registrado.Apellido;
                dp.Email.Text = registrado.Email;
                dp.Telefono.Text = registrado.Telefono;
                Bloquear(dp.Nombre, true);
                Bloquear(dp.Apellido, true);
                Bloquear(dp.Email, true);
                // Un usuario sin viajes previos no tiene teléfono guardado: se completa acá.
                Bloquear(dp.Telefono, esPasajero && !string.IsNullOrWhiteSpace(registrado.Telefono));
                dp.Autocompletado = true;
            }
            catch (Exception)
            {
                // Sin permiso de búsqueda, DNI inválido o error de conexión: se valida igual al pasar de paso.
            }
        }

        private List<Pasajero_GV42> PasajerosEnPantalla()
        {
            return _pasajeros.Select(p => new Pasajero_GV42
            {
                DNI = p.Dni.Text.Trim(),
                Nombre = p.Nombre.Text.Trim(),
                Apellido = p.Apellido.Text.Trim(),
                Email = p.Email.Text.Trim(),
                Telefono = p.Telefono.Text.Trim()
            }).ToList();
        }

        private void ValidarYAvanzarPasajeros()
        {
            for (int i = 0; i < _pasajeros.Count; i++)
            {
                var p = _pasajeros[i];
                if (string.IsNullOrWhiteSpace(p.Dni.Text) || string.IsNullOrWhiteSpace(p.Nombre.Text) ||
                    string.IsNullOrWhiteSpace(p.Apellido.Text) || string.IsNullOrWhiteSpace(p.Email.Text) ||
                    string.IsNullOrWhiteSpace(p.Telefono.Text))
                    throw new NegocioException_GV42("Completá todos los datos del pasajero " + (i + 1) + ".");
            }

            // Formato de cada campo, DNI repetido entre pasajeros y DNI ya registrado a nombre de otra persona.
            if (_esVendedor)
                foreach (var p in _pasajeros) AutocompletarPasajero(p);
            _bll.ValidarPasajerosParaReserva(PasajerosEnPantalla());

            _asientoPorPasajero.Clear();
            _indicePasajeroActivo = 0;
            IrAPaso(_pasoActual + 1);
        }

        // ------------------------------------------------------------ Paso 4: asientos

        private Panel ConstruirPasoAsientos()
        {
            var contenido = new Panel();

            lblPasajeroActual = new Label
            {
                Location = new Point(0, 0), Size = new Size(400, 26),
                Font = Tema_GV42.FuenteLabel, ForeColor = Tema_GV42.Acento
            };
            btnPasajeroAnterior = new Button { Text = "< Pasajero anterior", Location = new Point(410, 0), Size = new Size(150, 28) };
            Tema_GV42.EstilizarBotonSecundario(btnPasajeroAnterior);
            btnPasajeroAnterior.Click += (s, e) => CambiarPasajeroActivo(-1);

            var btnPasajeroSig = new Button { Text = "Pasajero siguiente >", Location = new Point(570, 0), Size = new Size(160, 28) };
            Tema_GV42.EstilizarBotonSecundario(btnPasajeroSig);
            btnPasajeroSig.Click += (s, e) => CambiarPasajeroActivo(1);
            btnPasajeroSiguiente = btnPasajeroSig;

            ctrlButacas = new CtrlButacas_GV42 { Location = new Point(0, 36), Size = new Size(800, 420) };
            ctrlButacas.AsientoClickeado += ctrlButacas_AsientoClickeado;

            contenido.Controls.Add(lblPasajeroActual);
            contenido.Controls.Add(btnPasajeroAnterior);
            contenido.Controls.Add(btnPasajeroSig);
            contenido.Controls.Add(ctrlButacas);

            return EnvolverEnCard(contenido);
        }

        private void PrepararPasoAsientos()
        {
            if (_vueloElegido == null) return;
            _mapaAsientos = _bll.ObtenerMapaAsientos(_vueloElegido.Vuelo.Id, _vueloElegido.Clase);
            _indicePasajeroActivo = 0;

            // Si mientras tanto otra reserva tomó un asiento que ya se había elegido acá, se libera
            // y se avisa (antes se seguía pintando como "tu selección" aunque estuviera ocupado).
            var ocupados = new HashSet<int>(_mapaAsientos.Where(m => m.Ocupado).Select(m => m.Asiento.Id));
            var perdidos = _asientoPorPasajero.Where(kv => ocupados.Contains(kv.Value.Id)).ToList();
            foreach (var kv in perdidos) _asientoPorPasajero.Remove(kv.Key);
            if (perdidos.Count > 0)
                MessageBox.Show("Otro pasajero tomó " + (perdidos.Count == 1 ? "el asiento " : "los asientos ") +
                                string.Join(", ", perdidos.Select(kv => kv.Value.NumeroAsiento)) +
                                " mientras reservabas. Elegí " + (perdidos.Count == 1 ? "otro." : "otros."),
                                "Asiento no disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarButacas();
        }

        private void RefrescarButacas()
        {
            if (_mapaAsientos == null) return;

            lblPasajeroActual.Text = "Asiento para: Pasajero " + (_indicePasajeroActivo + 1) + " de " + _pasajeros.Count;
            btnPasajeroAnterior.Enabled = _indicePasajeroActivo > 0;
            btnPasajeroSiguiente.Enabled = _indicePasajeroActivo < _pasajeros.Count - 1;

            var ocupadosLocalmente = new HashSet<int>(
                _asientoPorPasajero.Where(kv => kv.Key != _indicePasajeroActivo).Select(kv => kv.Value.Id));
            int? miAsiento = _asientoPorPasajero.TryGetValue(_indicePasajeroActivo, out Asiento_GV42 a) ? a.Id : (int?)null;

            ctrlButacas.CargarMapa(_mapaAsientos, ocupadosLocalmente, miAsiento);
        }

        private void CambiarPasajeroActivo(int delta)
        {
            int nuevo = _indicePasajeroActivo + delta;
            if (nuevo < 0 || nuevo >= _pasajeros.Count) return;
            _indicePasajeroActivo = nuevo;
            RefrescarButacas();
        }

        private void ctrlButacas_AsientoClickeado(object sender, Asiento_GV42 asiento)
        {
            _asientoPorPasajero[_indicePasajeroActivo] = asiento;
            RefrescarButacas();
        }

        private void ValidarYAvanzarAsientos()
        {
            if (_asientoPorPasajero.Count != _pasajeros.Count)
                throw new NegocioException_GV42("Elegí un asiento para cada pasajero.");
            IrAPaso(_pasoActual + 1);
        }

        // ------------------------------------------------------------ Paso 5: adicionales

        private Panel ConstruirPasoAdicionales()
        {
            pnlAdicionales = new Panel { AutoScroll = true };
            return EnvolverEnCard(pnlAdicionales);
        }

        private void CargarAdicionales()
        {
            var lblAyuda = new Label
            {
                Text = _esVendedor
                    ? "Tildá los servicios que pida el cliente e indicá la cantidad. El costo unitario viene del catálogo y se puede ajustar."
                    : "Tildá los servicios que quieras agregar e indicá la cantidad. El costo es el precio de lista de cada servicio.",
                Location = new Point(0, 0), Size = new Size(600, 20), Font = Tema_GV42.FuenteSubtitulo, ForeColor = Tema_GV42.Texto
            };
            pnlAdicionales.Controls.Add(lblAyuda);

            var tipos = _bll.ListarTiposAdicional();
            int y = 30;
            foreach (var tipo in tipos)
            {
                var chk = new CheckBox { Text = tipo.Nombre, Location = new Point(0, y + 3), Size = new Size(180, 20), Font = Tema_GV42.FuenteTexto };

                var lblCant = new Label { Text = "Cantidad", Location = new Point(190, y), AutoSize = true, Font = new Font("Segoe UI", 8F) };
                var cant = new NumericUpDown { Location = new Point(190, y + 16), Size = new Size(60, 24), Minimum = 1, Maximum = 20, Value = 1, Enabled = false };

                var lblCosto = new Label { Text = "Costo unitario", Location = new Point(270, y), AutoSize = true, Font = new Font("Segoe UI", 8F) };
                var costo = new NumericUpDown { Location = new Point(270, y + 16), Size = new Size(90, 24), Minimum = 0.01m, Maximum = 999999, DecimalPlaces = 2, Increment = 100, Enabled = false };
                costo.Value = Math.Min(costo.Maximum, Math.Max(costo.Minimum, tipo.PrecioUnitario));

                // El cliente autogestionado no elige el precio (la BLL igual lo fuerza al de lista).
                chk.CheckedChanged += (s, e) => { cant.Enabled = chk.Checked; costo.Enabled = chk.Checked && _esVendedor; };

                pnlAdicionales.Controls.Add(chk);
                pnlAdicionales.Controls.Add(lblCant);
                pnlAdicionales.Controls.Add(cant);
                pnlAdicionales.Controls.Add(lblCosto);
                pnlAdicionales.Controls.Add(costo);
                _filasAdicionales.Add((tipo, chk, cant, costo));
                y += 46;
            }
        }

        // ------------------------------------------------------------ Paso 6: resumen

        private Panel ConstruirPasoResumen()
        {
            lblResumen = new Label { Dock = DockStyle.Fill, Font = Tema_GV42.FuenteTexto, ForeColor = Tema_GV42.Texto };
            return EnvolverEnCard(lblResumen);
        }

        private void ArmarResumen()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Vuelo: " + _vueloElegido.CodigoVuelo + " (" + _vueloElegido.AerolineaNombre + ")");
            sb.AppendLine(_vueloElegido.OrigenDescripcion + " -> " + _vueloElegido.DestinoDescripcion);
            sb.AppendLine("Salida: " + _vueloElegido.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("Clase: " + _vueloElegido.ClaseTexto);
            sb.AppendLine();
            sb.AppendLine("Pasajeros y asientos:");
            for (int i = 0; i < _pasajeros.Count; i++)
            {
                string asiento = _asientoPorPasajero.TryGetValue(i, out Asiento_GV42 a) ? a.NumeroAsiento : "-";
                sb.AppendLine("  " + _pasajeros[i].Nombre.Text + " " + _pasajeros[i].Apellido.Text + " — asiento " + asiento);
            }
            var seleccionados = _filasAdicionales.Where(f => f.chk.Checked).ToList();
            if (seleccionados.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Servicios adicionales:");
                foreach (var f in seleccionados)
                    sb.AppendLine("  " + f.tipo.Nombre + " x" + f.cant.Value);
            }
            sb.AppendLine();
            sb.AppendLine("El importe final (con impuestos) se calcula al confirmar.");
            lblResumen.Text = sb.ToString();
        }

        private void ConfirmarReserva()
        {
            var borrador = new Reserva_GV42
            {
                Cliente = _clienteElegido,
                VueloClase = _vueloElegido,
                TipoViaje = cmbTipoViaje.SelectedIndex == 1 ? TipoViaje_GV42.IdaYVuelta : TipoViaje_GV42.Ida,
                FechaRegreso = cmbTipoViaje.SelectedIndex == 1 ? (DateTime?)dtRegreso.Value.Date : null
            };

            borrador.Pasajeros.AddRange(PasajerosEnPantalla());

            for (int i = 0; i < _pasajeros.Count; i++)
                borrador.AsientosPorPasajero.Add(new AsientoPasajero_GV42(_pasajeros[i].Dni.Text.Trim(), _asientoPorPasajero[i]));

            foreach (var f in _filasAdicionales.Where(f => f.chk.Checked))
            {
                borrador.Adicionales.Add(new AdicionalReserva_GV42
                {
                    TipoAdicional = f.tipo,
                    Cantidad = (int)f.cant.Value,
                    CostoUnitario = f.costo.Value
                });
            }

            _reservaGenerada = _bll.GenerarReserva(borrador);
            MostrarResultado();
            IrAPaso(PasoResultado());
        }

        // ------------------------------------------------------------ Paso final: resultado

        private Panel ConstruirPasoResultado()
        {
            pnlResultado = new Panel();
            lblResultado = new Label { Dock = DockStyle.Top, Height = 200, Font = Tema_GV42.FuenteTexto, ForeColor = Tema_GV42.Texto };

            var btnIrAPagar = new Button { Text = "Ir a pagar", Location = new Point(0, 210), Size = new Size(160, 40) };
            Tema_GV42.EstilizarBotonPrimario(btnIrAPagar);
            btnIrAPagar.Click += (s, e) =>
            {
                var frmPago = new FRMPagoReserva_GV42(_reservaGenerada.NumeroReserva);
                frmPago.ShowDialog(this);
                Close();
            };

            var btnCerrar = new Button { Text = "Cerrar", Location = new Point(170, 210), Size = new Size(120, 40) };
            Tema_GV42.EstilizarBotonSecundario(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            pnlResultado.Controls.Add(lblResultado);
            pnlResultado.Controls.Add(btnIrAPagar);
            pnlResultado.Controls.Add(btnCerrar);
            return EnvolverEnCard(pnlResultado);
        }

        private void MostrarResultado()
        {
            lblResultado.Text =
                "¡Reserva generada!\n\n" +
                "Número de reserva: " + _reservaGenerada.NumeroReserva + "\n" +
                "Importe base: " + _reservaGenerada.ImporteBase.ToString("C2") + "\n" +
                "Adicionales: " + _reservaGenerada.SubtotalAdicionales.ToString("C2") + "\n" +
                "Impuestos: " + _reservaGenerada.Impuestos.ToString("C2") + "\n" +
                "Importe total: " + _reservaGenerada.ImporteTotal.ToString("C2") + "\n\n" +
                "Estado: " + _reservaGenerada.EstadoTexto + " — quedan pendientes de pago.";
        }
    }
}
