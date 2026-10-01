using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // RFN 1 - Reservar vuelo. Un solo formulario para los dos canales:
    //  - Vendedor (patente Reservas.Generar): incluye el paso de buscar/registrar al cliente.
    //  - Pasajero con cuenta (solo Reservas.GenerarPropia): ese paso se omite; la reserva queda a su
    //    nombre y sus datos se toman de la sesión activa.
    // El modo se decide por patentes, no por el nombre del rol. La capa de negocio
    // (BLLReserva_GV42.GenerarReserva) vuelve a validar el canal según la sesión.
    //
    // El diseño está en FRMReservarVuelo_GV42.Designer.cs (Form Designer): cada paso del asistente es
    // una tarjeta (PanelTarjeta_GV42) con Dock Fill dentro de pnlContenido, y solo se muestra la del
    // paso actual. Lo único que se crea en código es lo que depende de los datos: una tarjeta
    // CtrlPasajero_GV42 por pasajero y una fila CtrlAdicional_GV42 por servicio adicional.
    public partial class FRMReservarVuelo_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        // Servicio "Asiento preferencial" del catálogo (precio del recargo por butaca preferencial).
        private TipoAdicional_GV42 _servicioPreferencial;

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private readonly bool _esVendedor;

        // Asistente: tarjetas de cada paso (la última es el resultado), su "chip" del indicador y su clave.
        private List<Control> _pasos;
        private List<Label> _chips;
        private List<string> _clavesPaso;
        private int _pasoActual;

        // Paso búsqueda
        private List<VueloClase_GV42> _resultados = new List<VueloClase_GV42>();
        private VueloClase_GV42 _vueloElegido;

        // Paso cliente (solo vendedor)
        private Pasajero_GV42 _clienteElegido;

        // Paso pasajeros
        private readonly List<DatosPasajero> _pasajeros = new List<DatosPasajero>();

        // Paso asientos
        private List<AsientoDisponibilidad_GV42> _mapaAsientos;
        private readonly Dictionary<int, Asiento_GV42> _asientoPorPasajero = new Dictionary<int, Asiento_GV42>();
        private int _indicePasajeroActivo;

        // Paso adicionales
        private readonly List<CtrlAdicional_GV42> _filasAdicionales = new List<CtrlAdicional_GV42>();

        // Paso resumen / resultado
        private bool _resumenArmado;
        private Reserva_GV42 _reservaGenerada;

        // Mientras se re-arman los combos por el cambio de idioma no se invalida la búsqueda
        // ni se recalculan los topes (el índice pasa un instante por -1).
        private bool _refrescandoIdioma;

        #endregion

        #region Constructor

        public FRMReservarVuelo_GV42()
        {
            InitializeComponent();

            _esVendedor = _bll.PuedeGenerarParaTerceros();

            ConfigurarPasos();
            ConfigurarControles();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            IrAPaso(0);
        }

        // Arma la lista de pasos según el modo: el paso "Cliente" solo existe para el vendedor.
        private void ConfigurarPasos()
        {
            _pasos = new List<Control> { pnlPasoBusqueda };
            _chips = new List<Label> { lblChipBusqueda };
            _clavesPaso = new List<string> { "busqueda" };
            if (_esVendedor)
            {
                _pasos.Add(pnlPasoCliente);
                _chips.Add(lblChipCliente);
                _clavesPaso.Add("cliente");
            }
            _pasos.AddRange(new Control[] { pnlPasoPasajeros, pnlPasoAsientos, pnlPasoAdicionales, pnlPasoResumen, pnlPasoResultado });
            _chips.AddRange(new[] { lblChipPasajeros, lblChipAsientos, lblChipAdicionales, lblChipResumen });
            _clavesPaso.AddRange(new[] { "pasajeros", "asientos", "adicionales", "resumen" });

            lblChipCliente.Visible = _esVendedor;
            pnlPasoCliente.Visible = false;
        }

        // Estado inicial que no se puede fijar con literales en el diseñador.
        private void ConfigurarControles()
        {
            dgvVuelos.AutoGenerateColumns = false;

            dtSalida.MinDate = DateTime.Today;
            dtRegreso.MinDate = DateTime.Today;
            numPasajeros.Maximum = BLLReserva_GV42.MAX_PASAJEROS_POR_RESERVA;

            LimitarCamposPersona(txtDniCliente, txtNombreCliente, txtApellidoCliente, txtEmailCliente, txtTelefonoCliente);
            PonerDatosClienteSoloLectura(true);
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            _refrescandoIdioma = true;
            try
            {
                Text = IdiomaManager_GV42.T("reservar.tituloVentana");
                lblTitulo.Text = IdiomaManager_GV42.T("reservar.titulo");

                // Navegación
                btnAtras.Text = IdiomaManager_GV42.T("reservar.atras");

                // Paso búsqueda
                lblOrigen.Text = IdiomaManager_GV42.T("reservar.origen");
                lblDestino.Text = IdiomaManager_GV42.T("reservar.destino");
                lblFechaSalida.Text = IdiomaManager_GV42.T("reservar.fechaSalida");
                lblFechaRegreso.Text = IdiomaManager_GV42.T("reservar.fechaRegreso");
                lblTipoViaje.Text = IdiomaManager_GV42.T("reservar.tipoViaje");
                lblPasajeros.Text = IdiomaManager_GV42.T("reservar.cantidadPasajeros");
                lblClase.Text = IdiomaManager_GV42.T("reservar.clase");
                btnBuscar.Text = IdiomaManager_GV42.T("reservar.buscar");
                lblVuelosDisponibles.Text = IdiomaManager_GV42.T("reservar.vuelosDisponibles");
                lblAyudaVuelos.Text = IdiomaManager_GV42.T("reservar.ayudaVuelos");
                RellenarCombo(cmbTipoViaje, new object[] { TipoViaje_GV42.Ida, TipoViaje_GV42.IdaYVuelta });
                RellenarCombo(cmbClaseFiltro, new object[] {
                    IdiomaManager_GV42.T("reservar.todasClases"),
                    ClaseVuelo_GV42.Economica, ClaseVuelo_GV42.Ejecutiva, ClaseVuelo_GV42.Primera });

                colVuelo.HeaderText = IdiomaManager_GV42.T("reservar.col.vuelo");
                colAerolinea.HeaderText = IdiomaManager_GV42.T("reservar.col.aerolinea");
                colOrigen.HeaderText = IdiomaManager_GV42.T("reservar.col.origen");
                colDestino.HeaderText = IdiomaManager_GV42.T("reservar.col.destino");
                colSalida.HeaderText = IdiomaManager_GV42.T("reservar.col.salida");
                colLlegada.HeaderText = IdiomaManager_GV42.T("reservar.col.llegada");
                colClase.HeaderText = IdiomaManager_GV42.T("reservar.col.clase");
                colPrecio.HeaderText = IdiomaManager_GV42.T("reservar.col.precio");
                colDisponibles.HeaderText = IdiomaManager_GV42.T("reservar.col.disponibles");
                dgvVuelos.Invalidate();   // la columna Clase (ClaseTexto) se vuelve a leer ya traducida

                // Paso cliente
                lblTituloCliente.Text = IdiomaManager_GV42.T("reservar.cliente.titulo");
                lblAyudaCliente.Text = IdiomaManager_GV42.T("reservar.cliente.ayuda");
                lblDniCliente.Text = IdiomaManager_GV42.T("reservar.cliente.dni");
                btnBuscarCliente.Text = IdiomaManager_GV42.T("reservar.cliente.buscar");
                lblNombreCliente.Text = IdiomaManager_GV42.T("reservar.nombre");
                lblApellidoCliente.Text = IdiomaManager_GV42.T("reservar.apellido");
                lblEmailCliente.Text = IdiomaManager_GV42.T("reservar.email");
                lblTelefonoCliente.Text = IdiomaManager_GV42.T("reservar.telefono");
                btnRegistrarCliente.Text = IdiomaManager_GV42.T("reservar.cliente.registrar");

                // Paso pasajeros
                lblTituloPasajeros.Text = IdiomaManager_GV42.T("reservar.pasajeros.titulo");
                lblAyudaPasajeros.Text = IdiomaManager_GV42.T(_esVendedor ? "reservar.pasajeros.ayudaVendedor" : "reservar.pasajeros.ayudaCliente");
                for (int i = 0; i < _pasajeros.Count; i++)
                {
                    _pasajeros[i].Control.ActualizarIdioma();
                    _pasajeros[i].Control.Titulo = TituloPasajero(i, _pasajeros[i].EsTitular);
                }

                // Paso asientos
                btnPasajeroAnterior.Text = IdiomaManager_GV42.T("reservar.pasajeroAnterior");
                btnPasajeroSiguiente.Text = IdiomaManager_GV42.T("reservar.pasajeroSiguiente");
                ActualizarEtiquetaPasajeroActivo();
                ctrlButacas.ActualizarIdioma();

                // Paso adicionales
                lblTituloAdicionales.Text = IdiomaManager_GV42.T("reservar.adicionales.titulo");
                lblAyudaAdicionales.Text = IdiomaManager_GV42.T(_esVendedor ? "reservar.adicionales.ayudaVendedor" : "reservar.adicionales.ayudaCliente");
                foreach (CtrlAdicional_GV42 fila in _filasAdicionales) fila.ActualizarIdioma();

                // Paso resumen
                lblTituloResumen.Text = IdiomaManager_GV42.T("reservar.resumen.titulo");
                lblSeccionVuelo.Text = IdiomaManager_GV42.T("reservar.resumen.vuelo");
                lblSeccionPasajeros.Text = IdiomaManager_GV42.T("reservar.resumen.pasajeros");
                lblSeccionAdicionales.Text = IdiomaManager_GV42.T("reservar.resumen.adicionales");
                lblNotaImporte.Text = IdiomaManager_GV42.T("reservar.resumen.nota");
                if (_resumenArmado && _vueloElegido != null) ArmarResumen();

                // Resultado
                lblResultadoTitulo.Text = IdiomaManager_GV42.T("reservar.resultado.titulo");
                lblResNumero.Text = IdiomaManager_GV42.T("reservar.resultado.numero");
                lblResBase.Text = IdiomaManager_GV42.T("reservar.resultado.base");
                lblResAdicionales.Text = IdiomaManager_GV42.T("reservar.resultado.adicionales");
                lblResImpuestos.Text = IdiomaManager_GV42.T("reservar.resultado.impuestos");
                lblResTotal.Text = IdiomaManager_GV42.T("reservar.resultado.total");
                btnIrAPagar.Text = IdiomaManager_GV42.T("reservar.irAPagar");
                btnCerrar.Text = IdiomaManager_GV42.T("reservar.cerrar");
                if (_reservaGenerada != null) MostrarResultado();

                // Encabezado, indicador y botón Siguiente/Confirmar según el paso actual.
                if (_pasos != null) MostrarPasoActual();
            }
            finally
            {
                _refrescandoIdioma = false;
            }
        }

        // Vuelve a cargar los ítems de un combo conservando la selección (los enums se muestran con
        // Texto() en el evento Format; la opción "Todas las clases" es un texto).
        private void RellenarCombo(ComboBox cmb, object[] items)
        {
            int indice = cmb.SelectedIndex;
            cmb.BeginUpdate();
            cmb.Items.Clear();
            cmb.Items.AddRange(items);
            cmb.SelectedIndex = indice >= 0 && indice < items.Length ? indice : 0;
            cmb.EndUpdate();
        }

        private string TituloPasajero(int indice, bool esTitular)
        {
            return esTitular ? IdiomaManager_GV42.T("reservar.pasajeroVos")
                             : IdiomaManager_GV42.T("reservar.pasajero", indice + 1);
        }

        #endregion

        #region Navegación del asistente

        private void IrAPaso(int indice)
        {
            if (indice < 0 || indice >= _pasos.Count) return;
            _pasoActual = indice;
            MostrarPasoActual();

            if (EsPasoAsientos(indice)) PrepararPasoAsientos();
        }

        // Muestra solo la tarjeta del paso actual y actualiza encabezado, indicador y botonera.
        private void MostrarPasoActual()
        {
            for (int i = 0; i < _pasos.Count; i++)
                _pasos[i].Visible = i == _pasoActual;

            bool esResultado = _pasoActual == PasoResultado();
            lblSubtitulo.Text = esResultado
                ? IdiomaManager_GV42.T("reservar.resultadoSubtitulo")
                : IdiomaManager_GV42.T("reservar.pasoDe", _pasoActual + 1, _chips.Count,
                                       IdiomaManager_GV42.T("reservar.paso." + _clavesPaso[_pasoActual]));
            ActualizarIndicador();

            btnAtras.Visible = _pasoActual > 0 && _pasoActual < _pasos.Count - 1;
            bool esUltimo = _pasoActual == _pasos.Count - 1;
            btnSiguiente.Visible = !esUltimo;
            btnSiguiente.Text = (_pasoActual == _pasos.Count - 2)
                ? IdiomaManager_GV42.T("reservar.confirmar")
                : IdiomaManager_GV42.T("reservar.siguiente");
        }

        // Chips del indicador: hecho (celeste con tilde), actual (azul) y pendiente (gris claro).
        private void ActualizarIndicador()
        {
            for (int i = 0; i < _chips.Count; i++)
            {
                Label chip = _chips[i];
                string nombre = IdiomaManager_GV42.T("reservar.chip." + _clavesPaso[i]);
                if (i < _pasoActual)
                {
                    chip.Text = "✓  " + nombre;
                    chip.BackColor = Tema_GV42.BordeGrilla;
                    chip.ForeColor = Tema_GV42.Acento;
                }
                else if (i == _pasoActual)
                {
                    chip.Text = (i + 1) + "  " + nombre;
                    chip.BackColor = Tema_GV42.Primario;
                    chip.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    chip.Text = (i + 1) + "  " + nombre;
                    chip.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                    chip.ForeColor = Tema_GV42.TextoSecundario;
                }
            }
        }

        private int PasoCliente() => _esVendedor ? 1 : -1;
        private int PasoPasajeros() => _esVendedor ? 2 : 1;
        private int PasoAsientos() => PasoPasajeros() + 1;
        private int PasoAdicionales() => PasoAsientos() + 1;
        private int PasoResumen() => PasoAdicionales() + 1;
        private int PasoResultado() => PasoResumen() + 1;
        private bool EsPasoAsientos(int indice) => indice == PasoAsientos();

        #endregion

        #region Búsqueda de vuelos

        private void CargarAeropuertos()
        {
            var aeropuertos = _bll.ListarAeropuertos();
            cmbOrigen.DisplayMember = "Descripcion";
            cmbDestino.DisplayMember = "Descripcion";
            cmbOrigen.DataSource = aeropuertos;
            cmbDestino.DataSource = new List<Aeropuerto_GV42>(aeropuertos);
        }

        private TipoViaje_GV42 TipoViajeElegido()
        {
            return cmbTipoViaje.SelectedIndex == 1 ? TipoViaje_GV42.IdaYVuelta : TipoViaje_GV42.Ida;
        }

        // Si se cambia un filtro después de buscar, la lista deja de corresponder a lo pedido:
        // se limpia para obligar a buscar de nuevo (antes se podía avanzar con 9 pasajeros sobre
        // un vuelo buscado para 2 y el error recién aparecía al confirmar).
        private void InvalidarBusqueda()
        {
            if (_refrescandoIdioma) return;
            if (_resultados == null || _resultados.Count == 0) return;
            _resultados = new List<VueloClase_GV42>();
            dgvVuelos.DataSource = null;
            _vueloElegido = null;
        }

        private void ValidarYAvanzarBusqueda()
        {
            if (dgvVuelos.SelectedRows.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.elegiVuelo"));

            _vueloElegido = (VueloClase_GV42)dgvVuelos.SelectedRows[0].DataBoundItem;
            ReconstruirFilasPasajeros();
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Cliente (vendedor)

        // Largos máximos de los campos de persona (coinciden con las columnas de la base).
        private static void LimitarCamposPersona(TextBox dni, TextBox nombre, TextBox apellido, TextBox email, TextBox telefono)
        {
            dni.MaxLength = 8;
            nombre.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            apellido.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            email.MaxLength = Validaciones_GV42.MAX_EMAIL;
            telefono.MaxLength = Validaciones_GV42.MAX_TELEFONO;
        }

        private void PonerDatosClienteSoloLectura(bool soloLectura)
        {
            txtNombreCliente.ReadOnly = soloLectura;
            txtApellidoCliente.ReadOnly = soloLectura;
            txtEmailCliente.ReadOnly = soloLectura;
            txtTelefonoCliente.ReadOnly = soloLectura;
        }

        private void MostrarDatosCliente(Pasajero_GV42 p)
        {
            txtNombreCliente.Text = p.Nombre;
            txtApellidoCliente.Text = p.Apellido;
            txtEmailCliente.Text = p.Email;
            txtTelefonoCliente.Text = p.Telefono;
        }

        private void ValidarYAvanzarCliente()
        {
            if (_clienteElegido == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.cliente.falta"));
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Pasajeros

        private static void Bloquear(TextBox txt, bool bloquear)
        {
            txt.ReadOnly = bloquear;
            txt.BackColor = bloquear ? Tema_GV42.Fondo : System.Drawing.Color.White;
        }

        private void ReconstruirFilasPasajeros()
        {
            // Las tarjetas anteriores se liberan (no quedan suscriptas ni ocupando memoria).
            foreach (DatosPasajero viejo in _pasajeros) viejo.Control.Dispose();
            pnlListaPasajeros.Controls.Clear();
            _pasajeros.Clear();

            int cantidad = (int)numPasajeros.Value;
            pnlListaPasajeros.SuspendLayout();
            for (int i = 0; i < cantidad; i++)
            {
                var dp = new DatosPasajero(new CtrlPasajero_GV42());
                dp.Control.Titulo = TituloPasajero(i, false);
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
                    dp.EsTitular = true;
                    dp.Control.Titulo = TituloPasajero(i, true);
                }

                _pasajeros.Add(dp);
                // Dock Top apilado: cada tarjeta nueva va debajo de la anterior.
                dp.Control.Dock = DockStyle.Top;
                pnlListaPasajeros.Controls.Add(dp.Control);
                dp.Control.BringToFront();
            }
            pnlListaPasajeros.ResumeLayout(true);
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
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.pasajeroIncompleto", i + 1));
            }

            // Formato de cada campo, DNI repetido entre pasajeros y DNI ya registrado a nombre de otra persona.
            if (_esVendedor)
                foreach (var p in _pasajeros) AutocompletarPasajero(p);
            _bll.ValidarPasajerosParaReserva(PasajerosEnPantalla());

            _asientoPorPasajero.Clear();
            _indicePasajeroActivo = 0;
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Asientos

        private void PrepararPasoAsientos()
        {
            if (_vueloElegido == null) return;
            _mapaAsientos = _bll.ObtenerMapaAsientos(_vueloElegido.Vuelo.Id, _vueloElegido.Clase);
            _indicePasajeroActivo = 0;

            // Butacas preferenciales: se pueden elegir y suman el recargo del catálogo.
            _servicioPreferencial = _bll.ObtenerServicioAsientoPreferencial();
            ctrlButacas.PermitirPreferenciales = _servicioPreferencial != null && _servicioPreferencial.PrecioUnitario > 0;
            ctrlButacas.RecargoPreferencial = _servicioPreferencial != null ? _servicioPreferencial.PrecioUnitario : 0m;

            // Si mientras tanto otra reserva tomó un asiento que ya se había elegido acá, se libera
            // y se avisa (antes se seguía pintando como "tu selección" aunque estuviera ocupado).
            var ocupados = new HashSet<int>(_mapaAsientos.Where(m => m.Ocupado).Select(m => m.Asiento.Id));
            var perdidos = _asientoPorPasajero.Where(kv => ocupados.Contains(kv.Value.Id)).ToList();
            foreach (var kv in perdidos) _asientoPorPasajero.Remove(kv.Key);
            if (perdidos.Count > 0)
                MessageBox.Show(IdiomaManager_GV42.T(perdidos.Count == 1 ? "reservar.asientoPerdido" : "reservar.asientosPerdidos",
                                                     string.Join(", ", perdidos.Select(kv => kv.Value.NumeroAsiento))),
                                IdiomaManager_GV42.T("reservar.asientoNoDisponible"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarButacas();
        }

        private void RefrescarButacas()
        {
            if (_mapaAsientos == null) return;

            ActualizarEtiquetaPasajeroActivo();
            btnPasajeroAnterior.Enabled = _indicePasajeroActivo > 0;
            btnPasajeroSiguiente.Enabled = _indicePasajeroActivo < _pasajeros.Count - 1;

            var ocupadosLocalmente = new HashSet<int>(
                _asientoPorPasajero.Where(kv => kv.Key != _indicePasajeroActivo).Select(kv => kv.Value.Id));
            int? miAsiento = _asientoPorPasajero.TryGetValue(_indicePasajeroActivo, out Asiento_GV42 a) ? a.Id : (int?)null;

            ctrlButacas.CargarMapa(_mapaAsientos, ocupadosLocalmente, miAsiento);
        }

        private void ActualizarEtiquetaPasajeroActivo()
        {
            lblPasajeroActual.Text = IdiomaManager_GV42.T("reservar.asientoPara", _indicePasajeroActivo + 1, Math.Max(1, _pasajeros.Count));
            if (_asientoPorPasajero.TryGetValue(_indicePasajeroActivo, out Asiento_GV42 elegido) && elegido.EsPreferencial && _servicioPreferencial != null)
                lblPasajeroActual.Text += "   ·   " + IdiomaManager_GV42.T("reservar.elegidoPreferencial",
                    elegido.NumeroAsiento, _servicioPreferencial.PrecioUnitario.ToString("C2"));
        }

        private void CambiarPasajeroActivo(int delta)
        {
            int nuevo = _indicePasajeroActivo + delta;
            if (nuevo < 0 || nuevo >= _pasajeros.Count) return;
            _indicePasajeroActivo = nuevo;
            RefrescarButacas();
        }

        private void ValidarYAvanzarAsientos()
        {
            if (_asientoPorPasajero.Count != _pasajeros.Count)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.elegiAsientos"));
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Servicios adicionales

        private void CargarAdicionales()
        {
            CrearFilasAdicionales(_bll.ListarTiposAdicional());
        }

        // Una fila por tipo de servicio activo (depende de la base, por eso se crea en código).
        private void CrearFilasAdicionales(List<TipoAdicional_GV42> tipos)
        {
            foreach (CtrlAdicional_GV42 vieja in _filasAdicionales) vieja.Dispose();
            _filasAdicionales.Clear();
            pnlListaAdicionales.Controls.Clear();

            pnlListaAdicionales.SuspendLayout();
            foreach (var tipo in tipos)
            {
                var fila = new CtrlAdicional_GV42();
                // El cliente autogestionado no elige el precio (la BLL igual lo fuerza al de lista).
                fila.Configurar(tipo, _esVendedor);
                fila.Dock = DockStyle.Top;
                pnlListaAdicionales.Controls.Add(fila);
                fila.BringToFront();
                _filasAdicionales.Add(fila);
            }
            pnlListaAdicionales.ResumeLayout(true);

            ActualizarTopesAdicionales(false);
        }

        // Tope de cada servicio según la cantidad de pasajeros y el tipo de viaje (lo calcula la BLL:
        // MaxPorPasajero x pasajeros x tramos). Antes se podían pedir 20 comidas especiales para 1 pasajero.
        // Si una cantidad ya elegida supera el nuevo tope, el control la baja y se avisa.
        private void ActualizarTopesAdicionales(bool avisar)
        {
            if (_refrescandoIdioma || _filasAdicionales.Count == 0) return;

            int cantidadPasajeros = (int)numPasajeros.Value;
            TipoViaje_GV42 tipoViaje = TipoViajeElegido();
            var ajustados = new List<string>();

            foreach (CtrlAdicional_GV42 fila in _filasAdicionales)
            {
                int maximo = _bll.MaximoAdicional(fila.Tipo, cantidadPasajeros, tipoViaje);
                if (fila.AplicarTope(maximo, tipoViaje == TipoViaje_GV42.IdaYVuelta))
                    ajustados.Add(IdiomaManager_GV42.T("reservar.topeAjustadoItem", fila.NombreTraducido, fila.Cantidad));
            }

            if (!avisar || ajustados.Count == 0) return;

            string mensaje = IdiomaManager_GV42.T("reservar.topeAjustado", string.Join(Environment.NewLine, ajustados));
            string titulo = IdiomaManager_GV42.T("reservar.topeAjustadoTitulo");
            // Se muestra después de que termine el evento del NumericUpDown / ComboBox, para no dejar
            // "trabado" el botón de subir/bajar mientras está abierto el mensaje.
            if (IsHandleCreated)
                BeginInvoke((Action)(() => MessageBox.Show(this, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)));
            else
                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Resumen y confirmación

        private void ArmarResumen()
        {
            var vuelo = new StringBuilder();
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.lineaVuelo", _vueloElegido.CodigoVuelo, _vueloElegido.AerolineaNombre));
            vuelo.AppendLine(_vueloElegido.OrigenDescripcion + " -> " + _vueloElegido.DestinoDescripcion);
            vuelo.AppendLine();
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.salida", _vueloElegido.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm")));
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.clase", _vueloElegido.ClaseTexto));
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.tipoViaje", TipoViajeElegido().Texto()));
            if (TipoViajeElegido() == TipoViaje_GV42.IdaYVuelta)
                vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.regreso", dtRegreso.Value.ToString("dd/MM/yyyy")));
            lblResumenVuelo.Text = vuelo.ToString();

            var pasajeros = new StringBuilder();
            for (int i = 0; i < _pasajeros.Count; i++)
            {
                string asiento = _asientoPorPasajero.TryGetValue(i, out Asiento_GV42 a) ? a.NumeroAsiento : "-";
                pasajeros.AppendLine(IdiomaManager_GV42.T("reservar.resumen.lineaPasajero",
                    _pasajeros[i].Nombre.Text, _pasajeros[i].Apellido.Text, asiento));
            }
            lblResumenPasajeros.Text = pasajeros.ToString();

            var seleccionados = _filasAdicionales.Where(f => f.Seleccionado).ToList();
            var adicionales = new StringBuilder();
            if (seleccionados.Count == 0)
                adicionales.AppendLine(IdiomaManager_GV42.T("reservar.resumen.sinAdicionales"));
            foreach (var f in seleccionados)
                adicionales.AppendLine(IdiomaManager_GV42.T("reservar.resumen.lineaAdicional", f.NombreTraducido, f.Cantidad));

            // Recargo automático por butacas preferenciales (lo vuelve a calcular la BLL al confirmar).
            int preferenciales = _asientoPorPasajero.Values.Count(x => x != null && x.EsPreferencial);
            if (preferenciales > 0 && _servicioPreferencial != null)
            {
                if (seleccionados.Count == 0) adicionales.Clear();
                adicionales.AppendLine(IdiomaManager_GV42.T("reservar.resumen.lineaPreferencial", preferenciales,
                    (_servicioPreferencial.PrecioUnitario * preferenciales).ToString("C2")));
            }
            lblResumenAdicionales.Text = adicionales.ToString();

            _resumenArmado = true;
        }

        private void ConfirmarReserva()
        {
            var borrador = new Reserva_GV42
            {
                Cliente = _clienteElegido,
                VueloClase = _vueloElegido,
                TipoViaje = TipoViajeElegido(),
                FechaRegreso = cmbTipoViaje.SelectedIndex == 1 ? (DateTime?)dtRegreso.Value.Date : null
            };

            borrador.Pasajeros.AddRange(PasajerosEnPantalla());

            for (int i = 0; i < _pasajeros.Count; i++)
                borrador.AsientosPorPasajero.Add(new AsientoPasajero_GV42(_pasajeros[i].Dni.Text.Trim(), _asientoPorPasajero[i]));

            foreach (var f in _filasAdicionales.Where(f => f.Seleccionado))
            {
                borrador.Adicionales.Add(new AdicionalReserva_GV42
                {
                    TipoAdicional = f.Tipo,
                    Cantidad = f.Cantidad,
                    CostoUnitario = f.CostoUnitario
                });
            }

            _reservaGenerada = _bll.GenerarReserva(borrador);
            MostrarResultado();
            IrAPaso(PasoResultado());
        }

        private void MostrarResultado()
        {
            lblResultadoEstado.Text = IdiomaManager_GV42.T("reservar.resultado.estado", _reservaGenerada.EstadoTexto);
            lblResNumeroValor.Text = _reservaGenerada.NumeroReserva;
            lblResBaseValor.Text = _reservaGenerada.ImporteBase.ToString("C2");
            lblResAdicionalesValor.Text = _reservaGenerada.SubtotalAdicionales.ToString("C2");
            lblResImpuestosValor.Text = _reservaGenerada.Impuestos.ToString("C2");
            lblResTotalValor.Text = _reservaGenerada.ImporteTotal.ToString("C2");
        }

        #endregion

        #region Eventos

        private void FRMReservarVuelo_GV42_Load(object sender, EventArgs e)
        {
            try
            {
                CargarAeropuertos();
                CargarAdicionales();
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("reservar.accionCargar"), ex);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            try { IrAPaso(_pasoActual - 1); }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("reservar.accionVolver"), ex); }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pasoActual == 0) ValidarYAvanzarBusqueda();
                else if (_pasoActual == PasoCliente()) ValidarYAvanzarCliente();
                else if (_pasoActual == PasoPasajeros()) ValidarYAvanzarPasajeros();
                else if (_pasoActual == PasoAsientos()) ValidarYAvanzarAsientos();
                else if (_pasoActual == PasoAdicionales()) { ArmarResumen(); IrAPaso(_pasoActual + 1); }
                else if (_pasoActual == PasoResumen()) ConfirmarReserva();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---- Filtros de búsqueda

        // Origen, destino, fecha de regreso y clase: cualquier cambio invalida la búsqueda anterior.
        private void filtroBusqueda_Cambio(object sender, EventArgs e)
        {
            InvalidarBusqueda();
        }

        private void dtSalida_ValueChanged(object sender, EventArgs e)
        {
            InvalidarBusqueda();
            // El regreso no puede ser anterior a la salida.
            if (dtRegreso.Value.Date < dtSalida.Value.Date) dtRegreso.Value = dtSalida.Value.Date;
            dtRegreso.MinDate = dtSalida.Value.Date;
        }

        private void cmbTipoViaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtRegreso.Enabled = cmbTipoViaje.SelectedIndex == 1;
            InvalidarBusqueda();
            ActualizarTopesAdicionales(true);   // ida y vuelta = 2 tramos
        }

        private void numPasajeros_ValueChanged(object sender, EventArgs e)
        {
            InvalidarBusqueda();
            ActualizarTopesAdicionales(true);
        }

        // Los enums del combo se muestran traducidos (ClaseVuelo / TipoViaje).
        private void cmbEnum_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ClaseVuelo_GV42 c) e.Value = c.Texto();
            else if (e.ListItem is TipoViaje_GV42 t) e.Value = t.Texto();
        }

        private void btnBuscarVuelos_Click(object sender, EventArgs e)
        {
            var criterio = new CriterioBusquedaVuelo_GV42
            {
                IdOrigen = ((Aeropuerto_GV42)cmbOrigen.SelectedItem)?.Id ?? 0,
                IdDestino = ((Aeropuerto_GV42)cmbDestino.SelectedItem)?.Id ?? 0,
                FechaSalida = dtSalida.Value.Date,
                CantidadPasajeros = (int)numPasajeros.Value,
                TipoViaje = TipoViajeElegido(),
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
                    MessageBox.Show(IdiomaManager_GV42.T("reservar.sinResultados"), IdiomaManager_GV42.T("reservar.sinResultadosTitulo"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("reservar.accionBuscar"), ex);
            }
        }

        // ---- Cliente (vendedor)

        // En el DNI solo se aceptan dígitos (se ignora cualquier otra tecla, salvo borrar/pegar).
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        // Si se cambia el DNI después de buscar, hay que volver a buscar: antes se podía buscar un DNI,
        // cambiarlo y registrar al cliente con el DNI nuevo pero con los datos precargados del anterior.
        private void txtDniCliente_TextChanged(object sender, EventArgs e)
        {
            if (_clienteElegido == null && !btnRegistrarCliente.Visible) return;
            _clienteElegido = null;
            btnRegistrarCliente.Visible = false;
            txtNombreCliente.Clear(); txtApellidoCliente.Clear(); txtEmailCliente.Clear(); txtTelefonoCliente.Clear();
            PonerDatosClienteSoloLectura(true);
        }

        private void txtDniCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscarCliente_Click(sender, e);
            }
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
                    MessageBox.Show(IdiomaManager_GV42.T("reservar.cliente.precargado"), IdiomaManager_GV42.T("reservar.cliente.precargadoTitulo"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtNombreCliente.Clear(); txtApellidoCliente.Clear(); txtEmailCliente.Clear(); txtTelefonoCliente.Clear();
                    PonerDatosClienteSoloLectura(false);
                    btnRegistrarCliente.Visible = true;
                    MessageBox.Show(IdiomaManager_GV42.T("reservar.cliente.noEncontrado"), IdiomaManager_GV42.T("reservar.cliente.noEncontradoTitulo"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                MessageBox.Show(IdiomaManager_GV42.T("reservar.cliente.registrado"), IdiomaManager_GV42.T("reservar.cliente.registradoTitulo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---- Asientos

        private void btnPasajeroAnterior_Click(object sender, EventArgs e)
        {
            CambiarPasajeroActivo(-1);
        }

        private void btnPasajeroSiguiente_Click(object sender, EventArgs e)
        {
            CambiarPasajeroActivo(1);
        }

        private void ctrlButacas_AsientoClickeado(object sender, Asiento_GV42 asiento)
        {
            _asientoPorPasajero[_indicePasajeroActivo] = asiento;
            RefrescarButacas();
        }

        // ---- Resultado

        private void btnIrAPagar_Click(object sender, EventArgs e)
        {
            var frmPago = new FRMPagoReserva_GV42(_reservaGenerada.NumeroReserva);
            frmPago.ShowDialog(this);
            Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Tipos anidados

        // Una fila de pasajero en pantalla: su tarjeta y el estado del autocompletado.
        private class DatosPasajero
        {
            public DatosPasajero(CtrlPasajero_GV42 control) { Control = control; }

            public CtrlPasajero_GV42 Control { get; }
            public TextBox Dni => Control.Dni;
            public TextBox Nombre => Control.Nombre;
            public TextBox Apellido => Control.Apellido;
            public TextBox Email => Control.Email;
            public TextBox Telefono => Control.Telefono;

            public bool EsTitular;                // pasajero 1 del cliente autogestionado (datos de la sesión)
            public bool Autocompletado;           // los datos salieron de la base (DNI ya registrado)
            public string UltimoDniBuscado = string.Empty;
        }

        #endregion
    }
}
