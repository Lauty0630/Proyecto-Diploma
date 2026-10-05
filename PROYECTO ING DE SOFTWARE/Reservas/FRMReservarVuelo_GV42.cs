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
        // En ida y vuelta se agregan pasos: elegir el vuelo de regreso y, después de completar la ida
        // (asientos y adicionales), repetir asientos y adicionales para la vuelta. Esos pasos usan las
        // mismas tarjetas que la ida, mostrando los datos del tramo activo (_tramo).
        private List<Control> _pasos;
        private List<string> _clavesPaso;
        private List<Label> _chipDePaso;      // chip del indicador que corresponde a cada paso
        private List<int> _tramoDePaso;       // tramo (IDA / VUELTA) sobre el que trabaja cada paso
        private List<Label> _chips;           // chips visibles, en orden
        private readonly Dictionary<Label, string> _claveChip = new Dictionary<Label, string>();
        private int _pasoActual;

        // Tramos del viaje: la ida siempre; la vuelta solo en ida y vuelta.
        private const int IDA = 0;
        private const int VUELTA = 1;
        private int _tramo = IDA;
        private bool _idaYVuelta;

        // Paso búsqueda
        private List<VueloClase_GV42> _resultados = new List<VueloClase_GV42>();
        // Vuelo elegido para cada tramo ([IDA] y [VUELTA]).
        private readonly VueloClase_GV42[] _vuelos = new VueloClase_GV42[2];
        private List<VueloClase_GV42> _resultadosVuelta = new List<VueloClase_GV42>();

        // Paso tarifa: familias tarifarias (Light / Plus / Top) y la elegida para toda la reserva.
        private List<TarifaFamilia_GV42> _tarifas = new List<TarifaFamilia_GV42>();
        private readonly List<CtrlTarifa_GV42> _filasTarifa = new List<CtrlTarifa_GV42>();
        private TarifaFamilia_GV42 _tarifaElegida;
        // Servicio que cobra elegir asiento cuando la tarifa no lo incluye.
        private TipoAdicional_GV42 _servicioSeleccion;

        // Mientras se cambia la fecha de regreso desde las fechas flexibles no se invalida la búsqueda de la ida.
        private bool _cambiandoFechaRegreso;

        // Paso cliente (solo vendedor)
        private Pasajero_GV42 _clienteElegido;

        // Paso pasajeros
        private readonly List<DatosPasajero> _pasajeros = new List<DatosPasajero>();
        // Pasajeros ya validados por la BLL al pasar el paso (con su tipo: adulto, niño o infante).
        private List<Pasajero_GV42> _pasajerosValidados = new List<Pasajero_GV42>();

        // Paso asientos
        // Paso asientos: mapa y asiento de cada pasajero, por tramo.
        private readonly List<AsientoDisponibilidad_GV42>[] _mapas = new List<AsientoDisponibilidad_GV42>[2];
        private readonly Dictionary<int, Asiento_GV42>[] _asientos =
            { new Dictionary<int, Asiento_GV42>(), new Dictionary<int, Asiento_GV42>() };
        private int _indicePasajeroActivo;
        // Tarifa donde elegir asiento es pago: por tramo, si se dejó para que se asignen en el check-in.
        private readonly bool[] _sinAsientos = new bool[2];

        // Paso adicionales
        // Paso adicionales: un juego de filas por tramo (se muestran las del tramo activo).
        private readonly List<CtrlAdicional_GV42>[] _filasAdicionales =
            { new List<CtrlAdicional_GV42>(), new List<CtrlAdicional_GV42>() };
        // Equipaje extra: una fila por pasajero y por tramo (la valija queda a nombre de quien la despacha).
        private readonly List<CtrlEquipajePasajero_GV42>[] _filasEquipaje =
            { new List<CtrlEquipajePasajero_GV42>(), new List<CtrlEquipajePasajero_GV42>() };
        private TipoAdicional_GV42 _servicioEquipaje;

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

            ConfigurarPasos(false);
            ConfigurarControles();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            IrAPaso(0);
        }

        // Arma la lista de pasos según el modo y el tipo de viaje:
        //  - el paso "Cliente" solo existe para el vendedor;
        //  - en ida y vuelta se elige el vuelo de regreso y, después de completar la ida (asientos y
        //    adicionales), se piden los asientos y adicionales de la vuelta. Los pasajeros se cargan una vez.
        private void ConfigurarPasos(bool idaYVuelta)
        {
            _idaYVuelta = idaYVuelta;
            _pasos = new List<Control>();
            _clavesPaso = new List<string>();
            _chipDePaso = new List<Label>();
            _tramoDePaso = new List<int>();
            _claveChip.Clear();

            AgregarPaso(pnlPasoBusqueda, idaYVuelta ? "busquedaIda" : "busqueda", lblChipBusqueda, idaYVuelta ? "busquedaIda" : "busqueda", IDA);
            if (idaYVuelta) AgregarPaso(pnlPasoVuelta, "vuelta", lblChipVuelta, "vuelta", VUELTA);
            AgregarPaso(pnlPasoTarifa, "tarifa", lblChipTarifa, "tarifa", IDA);
            if (_esVendedor) AgregarPaso(pnlPasoCliente, "cliente", lblChipCliente, "cliente", IDA);
            AgregarPaso(pnlPasoPasajeros, "pasajeros", lblChipPasajeros, "pasajeros", IDA);
            if (idaYVuelta)
            {
                // Los dos chips del medio pasan a representar cada tramo completo (asientos + adicionales).
                AgregarPaso(pnlPasoAsientos, "asientosIda", lblChipAsientos, "tramoIda", IDA);
                AgregarPaso(pnlPasoAdicionales, "adicionalesIda", lblChipAsientos, "tramoIda", IDA);
                AgregarPaso(pnlPasoAsientos, "asientosVuelta", lblChipAdicionales, "tramoVuelta", VUELTA);
                AgregarPaso(pnlPasoAdicionales, "adicionalesVuelta", lblChipAdicionales, "tramoVuelta", VUELTA);
            }
            else
            {
                AgregarPaso(pnlPasoAsientos, "asientos", lblChipAsientos, "asientos", IDA);
                AgregarPaso(pnlPasoAdicionales, "adicionales", lblChipAdicionales, "adicionales", IDA);
            }
            AgregarPaso(pnlPasoResumen, "resumen", lblChipResumen, "resumen", IDA);
            AgregarPaso(pnlPasoResultado, "resultado", null, null, IDA);

            _chips = _chipDePaso.Where(c => c != null).Distinct().ToList();
            lblChipCliente.Visible = _esVendedor;
            lblChipVuelta.Visible = idaYVuelta;
        }

        private void AgregarPaso(Control tarjeta, string clave, Label chip, string claveChip, int tramo)
        {
            _pasos.Add(tarjeta);
            _clavesPaso.Add(clave);
            _chipDePaso.Add(chip);
            _tramoDePaso.Add(tramo);
            if (chip != null) _claveChip[chip] = claveChip;
        }

        private string ClavePasoActual => _clavesPaso[_pasoActual];

        // Asientos de cada pasajero en el tramo activo (índice del pasajero -> asiento).
        private Dictionary<int, Asiento_GV42> AsientosTramo => _asientos[_tramo];

        // "  ·  Ida (AEP -> COR)": aclara de qué tramo es el paso. Vacío si el viaje es solo de ida.
        private string SufijoTramo()
        {
            if (!_idaYVuelta || _vuelos[_tramo] == null) return string.Empty;
            Vuelo_GV42 v = _vuelos[_tramo].Vuelo;
            return "   ·   " + IdiomaManager_GV42.T(_tramo == VUELTA ? "tramo.vuelta" : "tramo.ida") +
                   " (" + v.Origen.CodigoIata + " -> " + v.Destino.CodigoIata + ")";
        }

        // Estado inicial que no se puede fijar con literales en el diseñador.
        private void ConfigurarControles()
        {
            dgvVuelos.AutoGenerateColumns = false;
            dgvVuelosVuelta.AutoGenerateColumns = false;

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

                // Paso vuelo de regreso (ida y vuelta)
                lblTituloVuelta.Text = IdiomaManager_GV42.T("reservar.vuelta.titulo");
                lblAyudaVuelta.Text = IdiomaManager_GV42.T("reservar.vuelta.ayuda");
                colVueltaVuelo.HeaderText = colVuelo.HeaderText;
                colVueltaAerolinea.HeaderText = colAerolinea.HeaderText;
                colVueltaOrigen.HeaderText = colOrigen.HeaderText;
                colVueltaDestino.HeaderText = colDestino.HeaderText;
                colVueltaSalida.HeaderText = colSalida.HeaderText;
                colVueltaLlegada.HeaderText = colLlegada.HeaderText;
                colVueltaClase.HeaderText = colClase.HeaderText;
                colVueltaPrecio.HeaderText = colPrecio.HeaderText;
                colVueltaDisponibles.HeaderText = colDisponibles.HeaderText;
                dgvVuelosVuelta.Invalidate();
                ctrlFechasIda.ActualizarIdioma();
                ctrlFechasVuelta.ActualizarIdioma();

                // Paso tarifa
                lblTituloTarifa.Text = IdiomaManager_GV42.T("reservar.tarifa.titulo");
                lblAyudaTarifa.Text = IdiomaManager_GV42.T("reservar.tarifa.ayuda");
                foreach (CtrlTarifa_GV42 fila in _filasTarifa) fila.ActualizarIdioma();

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
                ActualizarTextoAsientoAutomatico();

                // Paso adicionales
                lblTituloAdicionales.Text = IdiomaManager_GV42.T("reservar.adicionales.titulo") + SufijoTramo();
                lblAyudaAdicionales.Text = IdiomaManager_GV42.T(_esVendedor ? "reservar.adicionales.ayudaVendedor" : "reservar.adicionales.ayudaCliente");
                foreach (CtrlAdicional_GV42 fila in _filasAdicionales.SelectMany(f => f)) fila.ActualizarIdioma();
                foreach (CtrlEquipajePasajero_GV42 fila in _filasEquipaje.SelectMany(f => f)) fila.ActualizarIdioma();

                // Paso resumen
                lblTituloResumen.Text = IdiomaManager_GV42.T("reservar.resumen.titulo");
                lblSeccionVuelo.Text = IdiomaManager_GV42.T("reservar.resumen.vuelo");
                lblSeccionPasajeros.Text = IdiomaManager_GV42.T("reservar.resumen.pasajeros");
                lblSeccionAdicionales.Text = IdiomaManager_GV42.T("reservar.resumen.adicionales");
                lblNotaImporte.Text = IdiomaManager_GV42.T("reservar.resumen.nota");
                if (_resumenArmado && _vuelos[IDA] != null) ArmarResumen();

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
            _tramo = _tramoDePaso[indice];
            MostrarPasoActual();

            if (ClavePasoActual == "tarifa") MostrarTarifas();
            else if (EsPasoAsientos()) PrepararPasoAsientos();
            else if (EsPasoAdicionales()) MostrarAdicionalesDelTramo();
        }

        // Muestra solo la tarjeta del paso actual y actualiza encabezado, indicador y botonera.
        private void MostrarPasoActual()
        {
            // Una misma tarjeta puede servir a dos pasos (asientos y adicionales de ida y de vuelta), y
            // la del vuelo de regreso no participa en un viaje de ida: se recorren todas las tarjetas.
            Control actual = _pasos[_pasoActual];
            foreach (Control tarjeta in new Control[] { pnlPasoBusqueda, pnlPasoVuelta, pnlPasoTarifa, pnlPasoCliente, pnlPasoPasajeros,
                                                         pnlPasoAsientos, pnlPasoAdicionales, pnlPasoResumen, pnlPasoResultado })
                tarjeta.Visible = tarjeta == actual;

            bool esResultado = _pasoActual == PasoResultado();
            lblSubtitulo.Text = esResultado
                ? IdiomaManager_GV42.T("reservar.resultadoSubtitulo")
                : IdiomaManager_GV42.T("reservar.pasoDe", _pasoActual + 1, _pasos.Count - 1,
                                       IdiomaManager_GV42.T("reservar.paso." + ClavePasoActual));
            ActualizarIndicador();

            btnAtras.Visible = _pasoActual > 0 && _pasoActual < _pasos.Count - 1;
            bool esUltimo = _pasoActual == _pasos.Count - 1;
            btnSiguiente.Visible = !esUltimo;
            btnSiguiente.Text = (_pasoActual == _pasos.Count - 2)
                ? IdiomaManager_GV42.T("reservar.confirmar")
                : IdiomaManager_GV42.T("reservar.siguiente");
        }

        // Chips del indicador: hecho (celeste con tilde), actual (azul) y pendiente (gris claro).
        // Un chip puede abarcar más de un paso (en ida y vuelta, "Ida" y "Vuelta" abarcan asientos y adicionales).
        private void ActualizarIndicador()
        {
            Label chipActual = _chipDePaso[_pasoActual];
            int indiceActual = chipActual != null ? _chips.IndexOf(chipActual) : _chips.Count;

            for (int i = 0; i < _chips.Count; i++)
            {
                Label chip = _chips[i];
                string nombre = IdiomaManager_GV42.T("reservar.chip." + _claveChip[chip]);
                if (i < indiceActual)
                {
                    chip.Text = "✓  " + nombre;
                    chip.BackColor = Tema_GV42.BordeGrilla;
                    chip.ForeColor = Tema_GV42.Acento;
                }
                else if (i == indiceActual)
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

        private int PasoResultado() => _pasos.Count - 1;
        private bool EsPasoAsientos() => ClavePasoActual.StartsWith("asientos");
        private bool EsPasoAdicionales() => ClavePasoActual.StartsWith("adicionales");

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
            if (_refrescandoIdioma || _cambiandoFechaRegreso) return;
            ctrlFechasIda.Visible = false;
            if (_resultados == null || _resultados.Count == 0) return;
            _resultados = new List<VueloClase_GV42>();
            dgvVuelos.DataSource = null;
            _vuelos[IDA] = null;
            _vuelos[VUELTA] = null;
        }

        private void ValidarYAvanzarBusqueda()
        {
            if (dgvVuelos.SelectedRows.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.elegiVuelo"));

            var ida = (VueloClase_GV42)dgvVuelos.SelectedRows[0].DataBoundItem;
            bool idaYVuelta = TipoViajeElegido() == TipoViaje_GV42.IdaYVuelta;

            // Ida y vuelta: se buscan los vuelos de regreso (misma ruta invertida, en la fecha de regreso
            // y que salgan después de que llegue la ida). Si no hay ninguno ese día pero sí en los días
            // cercanos, se avanza igual para elegir otra fecha desde las fechas flexibles.
            if (idaYVuelta && CargarVuelosDeRegreso(ida) == 0 && !ctrlFechasVuelta.Visible)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.sinVuelosRegreso", dtRegreso.Value.ToString("dd/MM/yyyy")));

            // Si cambió el vuelo, la tarifa se vuelve a elegir (el precio depende del vuelo).
            if (_vuelos[IDA] == null || _vuelos[IDA].Vuelo.Id != ida.Vuelo.Id || _vuelos[IDA].Clase != ida.Clase)
                _tarifaElegida = null;

            _vuelos[IDA] = ida;
            _vuelos[VUELTA] = null;
            _resumenArmado = false;
            // Los asientos elegidos antes eran de otros vuelos.
            _asientos[IDA].Clear();
            _asientos[VUELTA].Clear();

            ConfigurarPasos(idaYVuelta);
            ReconstruirFilasPasajeros();
            IrAPaso(_pasoActual + 1);
        }

        // Carga la grilla de vuelos de regreso y las fechas flexibles de la vuelta. Devuelve cuántos vuelos
        // hay en la fecha de regreso elegida.
        private int CargarVuelosDeRegreso(VueloClase_GV42 ida)
        {
            ClaseVuelo_GV42? clase = cmbClaseFiltro.SelectedIndex > 0 ? (ClaseVuelo_GV42?)cmbClaseFiltro.SelectedItem : null;
            int pasajeros = (int)numPasajeros.Value;

            _resultadosVuelta = _bll.BuscarVuelosDeRegreso(ida, dtRegreso.Value.Date, pasajeros, clase);
            dgvVuelosVuelta.DataSource = null;
            dgvVuelosVuelta.DataSource = _resultadosVuelta;
            // Igual que en la ida: el vuelo de regreso lo tiene que elegir el usuario.
            dgvVuelosVuelta.ClearSelection();
            dgvVuelosVuelta.CurrentCell = null;

            // Fechas flexibles de la vuelta: desde el día en que llega la ida.
            try
            {
                var criterio = new CriterioBusquedaVuelo_GV42
                {
                    IdOrigen = ida.Vuelo.Destino.Id,
                    IdDestino = ida.Vuelo.Origen.Id,
                    FechaSalida = dtRegreso.Value.Date,
                    CantidadPasajeros = pasajeros,
                    TipoViaje = TipoViaje_GV42.Ida,
                    Clase = clase
                };
                List<PrecioFecha_GV42> dias = _bll.PreciosPorFecha(criterio, ida.Vuelo.FechaHoraLlegada.Date);
                ctrlFechasVuelta.Cargar(dias, dtRegreso.Value.Date);
                ctrlFechasVuelta.Visible = dias.Any(d => d.HayVuelos);
            }
            catch (Exception)
            {
                ctrlFechasVuelta.Visible = false;   // las fechas flexibles son una ayuda: sin ellas se sigue igual
            }
            return _resultadosVuelta.Count;
        }

        // Paso "Vuelo de regreso" (solo ida y vuelta).
        private void ValidarYAvanzarVuelta()
        {
            if (dgvVuelosVuelta.SelectedRows.Count == 0)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.elegiVueloVuelta"));

            var vuelta = (VueloClase_GV42)dgvVuelosVuelta.SelectedRows[0].DataBoundItem;
            if (_vuelos[VUELTA] == null || _vuelos[VUELTA].Vuelo.Id != vuelta.Vuelo.Id || _vuelos[VUELTA].Clase != vuelta.Clase)
            {
                _asientos[VUELTA].Clear();
                _tarifaElegida = null;   // el precio de la tarifa depende de los dos vuelos
            }
            _vuelos[VUELTA] = vuelta;
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Tarifa

        // Una tarjeta por familia tarifaria (dependen de la base, por eso se crean en código), con el
        // precio por adulto para los vuelos elegidos.
        private void MostrarTarifas()
        {
            foreach (CtrlTarifa_GV42 vieja in _filasTarifa) vieja.Dispose();
            _filasTarifa.Clear();
            flpTarifas.Controls.Clear();

            decimal precioSeleccion = _servicioSeleccion != null ? _servicioSeleccion.PrecioUnitario : 0m;
            flpTarifas.SuspendLayout();
            foreach (TarifaFamilia_GV42 tarifa in _tarifas)
            {
                decimal precioAdulto = tarifa.PrecioPara(_vuelos[IDA].PrecioBase)
                                     + (_idaYVuelta && _vuelos[VUELTA] != null ? tarifa.PrecioPara(_vuelos[VUELTA].PrecioBase) : 0m);
                var fila = new CtrlTarifa_GV42();
                fila.Configurar(tarifa, precioAdulto, precioSeleccion);
                fila.Seleccionada = _tarifaElegida != null && _tarifaElegida.Id == tarifa.Id;
                fila.Elegida += ctrlTarifa_Elegida;
                flpTarifas.Controls.Add(fila);
                _filasTarifa.Add(fila);
            }
            flpTarifas.ResumeLayout(true);
        }

        private void ctrlTarifa_Elegida(object sender, EventArgs e)
        {
            var elegida = (CtrlTarifa_GV42)sender;
            bool cambio = _tarifaElegida == null || _tarifaElegida.Id != elegida.Tarifa.Id;
            _tarifaElegida = elegida.Tarifa;
            foreach (CtrlTarifa_GV42 fila in _filasTarifa) fila.Seleccionada = fila == elegida;

            if (cambio)
            {
                // Con una tarifa donde elegir asiento es pago, por defecto no se eligen (se asignan en el check-in).
                _sinAsientos[IDA] = _sinAsientos[VUELTA] = _tarifaElegida.ElegirAsientoEsPago;
                _asientos[IDA].Clear();
                _asientos[VUELTA].Clear();
            }
        }

        private void ValidarYAvanzarTarifa()
        {
            if (_tarifaElegida == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.tarifa.falta"));
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
                // La edad se toma el día del vuelo de ida: define si viaja como adulto, niño o infante.
                dp.Control.FechaVuelo = _vuelos[IDA].Vuelo.FechaHoraSalida;
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
                    // La fecha de nacimiento tampoco está en Usuario: se pide la primera vez que reserva.
                    dp.Control.FechaNacimiento = titular.FechaNacimiento;
                    dp.Control.NacimientoBloqueado = titular.FechaNacimiento.HasValue;
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
                dp.Control.FechaNacimiento = null;
                dp.Control.NacimientoBloqueado = false;
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
                // Si ya tiene fecha de nacimiento registrada se usa esa; si no, se completa acá.
                if (registrado.FechaNacimiento.HasValue)
                {
                    dp.Control.FechaNacimiento = registrado.FechaNacimiento;
                    dp.Control.NacimientoBloqueado = true;
                }
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
                Telefono = p.Telefono.Text.Trim(),
                FechaNacimiento = p.Control.FechaNacimiento,
                Asistencia = p.Control.Asistencia
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
                if (!p.Control.FechaNacimiento.HasValue)
                    throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.pasajeroSinNacimiento", i + 1));
            }

            // Formato de cada campo, DNI repetido entre pasajeros y DNI ya registrado a nombre de otra persona.
            if (_esVendedor)
                foreach (var p in _pasajeros) AutocompletarPasajero(p);
            // La BLL deja en cada pasajero su tipo (adulto, niño o infante) según la edad el día del vuelo.
            List<Pasajero_GV42> validados = PasajerosEnPantalla();
            _bll.ValidarPasajerosParaReserva(validados, _vuelos[IDA].Vuelo.FechaHoraSalida);
            _pasajerosValidados = validados;

            _asientos[IDA].Clear();
            _asientos[VUELTA].Clear();
            _indicePasajeroActivo = 0;
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Asientos

        // Los infantes viajan en brazos: no eligen asiento ni llevan equipaje propio.
        private bool OcupaAsiento(int indice)
        {
            return indice >= _pasajerosValidados.Count || !_pasajerosValidados[indice].EsInfante;
        }

        private List<int> IndicesConAsiento()
        {
            return Enumerable.Range(0, _pasajeros.Count).Where(OcupaAsiento).ToList();
        }

        private bool ElegirAsientoEsPago => _tarifaElegida != null && _tarifaElegida.ElegirAsientoEsPago;

        private void PrepararPasoAsientos()
        {
            if (_vuelos[_tramo] == null) return;
            _mapas[_tramo] = _bll.ObtenerMapaAsientos(_vuelos[_tramo].Vuelo.Id, _vuelos[_tramo].Clase);
            List<int> conAsiento = IndicesConAsiento();
            _indicePasajeroActivo = conAsiento.Count > 0 ? conAsiento[0] : 0;

            // Butacas preferenciales: se pueden elegir; suman el recargo del catálogo salvo que la tarifa las incluya.
            _servicioPreferencial = _bll.ObtenerServicioAsientoPreferencial();
            bool incluidas = _tarifaElegida != null && _tarifaElegida.IncluyePreferencial;
            ctrlButacas.PermitirPreferenciales = incluidas || (_servicioPreferencial != null && _servicioPreferencial.PrecioUnitario > 0);
            ctrlButacas.RecargoPreferencial = incluidas || _servicioPreferencial == null ? 0m : _servicioPreferencial.PrecioUnitario;

            // Tarifa donde elegir asiento es pago: se puede dejar que se asignen gratis en el check-in.
            chkAsientoAutomatico.Visible = ElegirAsientoEsPago;
            chkAsientoAutomatico.Checked = ElegirAsientoEsPago && _sinAsientos[_tramo];
            ActualizarTextoAsientoAutomatico();
            ctrlButacas.Enabled = !chkAsientoAutomatico.Checked;

            // Si mientras tanto otra reserva tomó un asiento que ya se había elegido acá, se libera
            // y se avisa (antes se seguía pintando como "tu selección" aunque estuviera ocupado).
            var ocupados = new HashSet<int>(_mapas[_tramo].Where(m => m.Ocupado).Select(m => m.Asiento.Id));
            var perdidos = AsientosTramo.Where(kv => ocupados.Contains(kv.Value.Id)).ToList();
            foreach (var kv in perdidos) AsientosTramo.Remove(kv.Key);
            if (perdidos.Count > 0)
                MessageBox.Show(IdiomaManager_GV42.T(perdidos.Count == 1 ? "reservar.asientoPerdido" : "reservar.asientosPerdidos",
                                                     string.Join(", ", perdidos.Select(kv => kv.Value.NumeroAsiento))),
                                IdiomaManager_GV42.T("reservar.asientoNoDisponible"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarButacas();
        }

        private void ActualizarTextoAsientoAutomatico()
        {
            chkAsientoAutomatico.Text = IdiomaManager_GV42.T("reservar.asientoAutomatico",
                (_servicioSeleccion != null ? _servicioSeleccion.PrecioUnitario : 0m).ToString("C0"));
        }

        private void RefrescarButacas()
        {
            if (_mapas[_tramo] == null) return;

            List<int> conAsiento = IndicesConAsiento();
            int posicion = conAsiento.IndexOf(_indicePasajeroActivo);
            ActualizarEtiquetaPasajeroActivo();
            btnPasajeroAnterior.Enabled = posicion > 0;
            btnPasajeroSiguiente.Enabled = posicion >= 0 && posicion < conAsiento.Count - 1;

            var ocupadosLocalmente = new HashSet<int>(
                AsientosTramo.Where(kv => kv.Key != _indicePasajeroActivo).Select(kv => kv.Value.Id));
            int? miAsiento = AsientosTramo.TryGetValue(_indicePasajeroActivo, out Asiento_GV42 a) ? a.Id : (int?)null;

            ctrlButacas.CargarMapa(_mapas[_tramo], ocupadosLocalmente, miAsiento);
        }

        private void ActualizarEtiquetaPasajeroActivo()
        {
            lblPasajeroActual.Text = IdiomaManager_GV42.T("reservar.asientoPara", _indicePasajeroActivo + 1, Math.Max(1, _pasajeros.Count))
                                     + SufijoTramo();
            bool cobraPreferencial = _servicioPreferencial != null && (_tarifaElegida == null || !_tarifaElegida.IncluyePreferencial);
            if (AsientosTramo.TryGetValue(_indicePasajeroActivo, out Asiento_GV42 elegido) && elegido.EsPreferencial && cobraPreferencial)
                lblPasajeroActual.Text += "   ·   " + IdiomaManager_GV42.T("reservar.elegidoPreferencial",
                    elegido.NumeroAsiento, _servicioPreferencial.PrecioUnitario.ToString("C2"));
        }

        // Pasa al pasajero anterior / siguiente que ocupa asiento (los infantes se saltean).
        private void CambiarPasajeroActivo(int delta)
        {
            List<int> conAsiento = IndicesConAsiento();
            int posicion = conAsiento.IndexOf(_indicePasajeroActivo) + delta;
            if (posicion < 0 || posicion >= conAsiento.Count) return;
            _indicePasajeroActivo = conAsiento[posicion];
            RefrescarButacas();
        }

        private void ValidarYAvanzarAsientos()
        {
            // Con tarifa donde elegir asiento es pago se puede seguir sin elegir (se asignan en el check-in).
            bool sinElegir = ElegirAsientoEsPago && _sinAsientos[_tramo];
            if (sinElegir) AsientosTramo.Clear();
            else if (AsientosTramo.Count != IndicesConAsiento().Count)
                throw new NegocioException_GV42(IdiomaManager_GV42.T("reservar.elegiAsientos"));
            CrearFilasEquipaje(_tramo);
            IrAPaso(_pasoActual + 1);
        }

        #endregion

        #region Servicios adicionales

        private void CargarAdicionales()
        {
            _servicioEquipaje = _bll.ObtenerServicioEquipajeExtra();
            _servicioSeleccion = _bll.ObtenerServicioSeleccionAsiento();
            _tarifas = _bll.ListarTarifas();
            CrearFilasAdicionales(_bll.ListarTiposAdicional());
        }

        // Equipaje extra por pasajero: una fila por cada uno, arriba de los demás servicios. Se rearma
        // al entrar al paso (pueden haber cambiado los pasajeros) conservando lo ya elegido en ese tramo.
        private void CrearFilasEquipaje(int tramo)
        {
            List<CtrlEquipajePasajero_GV42> filas = _filasEquipaje[tramo];
            // Lo ya elegido se conserva por pasajero (Tag = índice del pasajero).
            Dictionary<int, int> elegidas = filas.ToDictionary(f => (int)f.Tag, f => f.Cantidad);
            foreach (CtrlEquipajePasajero_GV42 vieja in filas)
            {
                pnlListaAdicionales.Controls.Remove(vieja);
                vieja.Dispose();
            }
            filas.Clear();
            if (_servicioEquipaje == null || _servicioEquipaje.PrecioUnitario <= 0) return;

            pnlListaAdicionales.SuspendLayout();
            for (int i = 0; i < _pasajeros.Count; i++)
            {
                if (!OcupaAsiento(i)) continue;   // los infantes no despachan equipaje propio
                var fila = new CtrlEquipajePasajero_GV42 { Tag = i };
                fila.Configurar((_pasajeros[i].Nombre.Text.Trim() + " " + _pasajeros[i].Apellido.Text.Trim()).Trim(),
                                _servicioEquipaje.MaxPorPasajero, _servicioEquipaje.PrecioUnitario, false);
                if (elegidas.ContainsKey(i)) fila.Cantidad = elegidas[i];
                fila.Dock = DockStyle.Top;
                pnlListaAdicionales.Controls.Add(fila);
                filas.Add(fila);
            }
            // Con Dock Top, el control que está más al fondo queda más arriba: se mandan al fondo de
            // atrás para adelante, así el primer pasajero queda primero.
            for (int i = filas.Count - 1; i >= 0; i--) filas[i].SendToBack();
            pnlListaAdicionales.ResumeLayout(true);
        }

        // Una fila por tipo de servicio activo y por tramo (depende de la base, por eso se crea en código).
        private void CrearFilasAdicionales(List<TipoAdicional_GV42> tipos)
        {
            foreach (CtrlAdicional_GV42 vieja in _filasAdicionales.SelectMany(f => f)) vieja.Dispose();
            foreach (CtrlEquipajePasajero_GV42 vieja in _filasEquipaje.SelectMany(f => f)) vieja.Dispose();
            pnlListaAdicionales.Controls.Clear();

            pnlListaAdicionales.SuspendLayout();
            for (int tramo = IDA; tramo <= VUELTA; tramo++)
            {
                _filasAdicionales[tramo].Clear();
                _filasEquipaje[tramo].Clear();
                foreach (var tipo in tipos)
                {
                    var fila = new CtrlAdicional_GV42();
                    // El cliente autogestionado no elige el precio (la BLL igual lo fuerza al de lista).
                    fila.Configurar(tipo, false);   // el precio es el del catálogo para todos
                    fila.Dock = DockStyle.Top;
                    fila.Visible = tramo == _tramo;
                    pnlListaAdicionales.Controls.Add(fila);
                    fila.BringToFront();
                    _filasAdicionales[tramo].Add(fila);
                }
            }
            pnlListaAdicionales.ResumeLayout(true);

            ActualizarTopesAdicionales(false);
        }

        // Deja a la vista solo los servicios y el equipaje del tramo activo (ida o vuelta).
        private void MostrarAdicionalesDelTramo()
        {
            pnlListaAdicionales.SuspendLayout();
            for (int tramo = IDA; tramo <= VUELTA; tramo++)
            {
                foreach (CtrlAdicional_GV42 fila in _filasAdicionales[tramo]) fila.Visible = tramo == _tramo;
                foreach (CtrlEquipajePasajero_GV42 fila in _filasEquipaje[tramo]) fila.Visible = tramo == _tramo;
            }
            pnlListaAdicionales.ResumeLayout(true);
            pnlListaAdicionales.AutoScrollPosition = new System.Drawing.Point(0, 0);
            lblTituloAdicionales.Text = IdiomaManager_GV42.T("reservar.adicionales.titulo") + SufijoTramo();
        }

        // Tope de cada servicio en un tramo según la cantidad de pasajeros (lo calcula la BLL:
        // MaxPorPasajero x pasajeros). Antes se podían pedir 20 comidas especiales para 1 pasajero.
        // Si una cantidad ya elegida supera el nuevo tope, el control la baja y se avisa.
        private void ActualizarTopesAdicionales(bool avisar)
        {
            if (_refrescandoIdioma || _filasAdicionales[IDA].Count == 0) return;

            int cantidadPasajeros = (int)numPasajeros.Value;
            var ajustados = new List<string>();

            foreach (CtrlAdicional_GV42 fila in _filasAdicionales.SelectMany(f => f))
            {
                int maximo = _bll.MaximoAdicional(fila.Tipo, cantidadPasajeros);
                if (fila.AplicarTope(maximo, false))
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
            int tramos = _idaYVuelta ? 2 : 1;

            // Vuelos: el de ida y, si corresponde, el de regreso.
            var vuelo = new StringBuilder();
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.tipoViaje", TipoViajeElegido().Texto()));
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.tarifa", _tarifaElegida.Nombre));
            for (int t = 0; t < tramos; t++)
            {
                VueloClase_GV42 vc = _vuelos[t];
                vuelo.AppendLine();
                if (_idaYVuelta)
                    vuelo.AppendLine(IdiomaManager_GV42.T(t == VUELTA ? "tramo.vuelta" : "tramo.ida").ToUpper());
                vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.lineaVuelo", vc.CodigoVuelo, vc.AerolineaNombre));
                vuelo.AppendLine(vc.OrigenDescripcion + " -> " + vc.DestinoDescripcion);
                vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.salida", vc.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm")));
                vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.clase", vc.ClaseTexto));
            }
            // Tarifa de todos los pasajeros (adulto completa, niño con descuento, infante una fracción).
            decimal importeTarifa = 0m;
            for (int t = 0; t < tramos; t++)
                importeTarifa += BLLReserva_GV42.CalcularTarifaTramo(_tarifaElegida, _vuelos[t], _pasajerosValidados);
            vuelo.AppendLine();
            vuelo.AppendLine(IdiomaManager_GV42.T("reservar.resumen.importeTarifa", importeTarifa.ToString("C2")));
            lblResumenVuelo.Text = vuelo.ToString();

            // Pasajeros con su asiento en cada tramo.
            var pasajeros = new StringBuilder();
            for (int i = 0; i < _pasajeros.Count; i++)
            {
                Pasajero_GV42 p = _pasajerosValidados[i];
                string asiento;
                if (p.EsInfante)
                    asiento = IdiomaManager_GV42.T("reservar.resumen.enBrazos");
                else
                {
                    asiento = TextoAsientoResumen(IDA, i);
                    if (_idaYVuelta)
                        asiento = IdiomaManager_GV42.T("reservar.resumen.asientosIdaVuelta", asiento, TextoAsientoResumen(VUELTA, i));
                }
                string linea = IdiomaManager_GV42.T("reservar.resumen.lineaPasajero",
                    _pasajeros[i].Nombre.Text, _pasajeros[i].Apellido.Text, asiento);
                if (p.Tipo != TipoPasajero_GV42.Adulto) linea += " (" + p.Tipo.Texto() + ")";
                pasajeros.AppendLine(linea);
                if (p.Asistencia != AsistenciaEspecial_GV42.Ninguna)
                    pasajeros.AppendLine("   " + IdiomaManager_GV42.T("reservar.resumen.asistencia", p.Asistencia.Texto()));
            }
            lblResumenPasajeros.Text = pasajeros.ToString();

            // Adicionales de cada tramo: servicios elegidos, recargo por butacas preferenciales (lo vuelve
            // a calcular la BLL al confirmar) y equipaje extra por pasajero.
            var adicionales = new StringBuilder();
            for (int t = 0; t < tramos; t++)
            {
                var lineas = new List<string>();
                foreach (CtrlAdicional_GV42 f in _filasAdicionales[t].Where(f => f.Seleccionado))
                    lineas.Add(IdiomaManager_GV42.T("reservar.resumen.lineaAdicional", f.NombreTraducido, f.Cantidad));

                // Elegir asiento, cuando la tarifa no lo incluye.
                if (ElegirAsientoEsPago && _asientos[t].Count > 0 && _servicioSeleccion != null)
                    lineas.Add(IdiomaManager_GV42.T("reservar.resumen.lineaSeleccion", _asientos[t].Count,
                        (_servicioSeleccion.PrecioUnitario * _asientos[t].Count).ToString("C2")));

                int preferenciales = _asientos[t].Values.Count(x => x != null && x.EsPreferencial);
                if (preferenciales > 0 && _servicioPreferencial != null && !_tarifaElegida.IncluyePreferencial)
                    lineas.Add(IdiomaManager_GV42.T("reservar.resumen.lineaPreferencial", preferenciales,
                        (_servicioPreferencial.PrecioUnitario * preferenciales).ToString("C2")));

                foreach (CtrlEquipajePasajero_GV42 f in _filasEquipaje[t].Where(f => f.Cantidad > 0))
                    lineas.Add(IdiomaManager_GV42.T("reservar.resumen.lineaEquipaje", f.Pasajero, f.Cantidad,
                        (_servicioEquipaje.PrecioUnitario * f.Cantidad).ToString("C2")));

                if (_idaYVuelta)
                {
                    if (t > 0) adicionales.AppendLine();
                    adicionales.AppendLine(IdiomaManager_GV42.T(t == VUELTA ? "tramo.vuelta" : "tramo.ida").ToUpper());
                }
                if (lineas.Count == 0) adicionales.AppendLine(IdiomaManager_GV42.T("reservar.resumen.sinAdicionales"));
                foreach (string linea in lineas) adicionales.AppendLine(linea);
            }
            lblResumenAdicionales.Text = adicionales.ToString();

            _resumenArmado = true;
        }

        // "12A", o "se asigna en el check-in" si la tarifa cobra la elección y no se eligió.
        private string TextoAsientoResumen(int tramo, int indicePasajero)
        {
            return _asientos[tramo].TryGetValue(indicePasajero, out Asiento_GV42 a)
                ? a.NumeroAsiento
                : IdiomaManager_GV42.T("reservar.resumen.asientoEnCheckIn");
        }

        private void ConfirmarReserva()
        {
            var borrador = new Reserva_GV42
            {
                Cliente = _clienteElegido,
                VueloClase = _vuelos[IDA],
                VueloClaseVuelta = _idaYVuelta ? _vuelos[VUELTA] : null,
                Tarifa = _tarifaElegida,
                TipoViaje = _idaYVuelta ? TipoViaje_GV42.IdaYVuelta : TipoViaje_GV42.Ida
            };

            borrador.Pasajeros.AddRange(PasajerosEnPantalla());

            // Por cada tramo: el asiento y las valijas extra de cada pasajero, y los servicios elegidos.
            int tramos = _idaYVuelta ? 2 : 1;
            for (int t = 0; t < tramos; t++)
            {
                int numeroTramo = t == VUELTA ? Reserva_GV42.TRAMO_VUELTA : Reserva_GV42.TRAMO_IDA;

                for (int i = 0; i < _pasajeros.Count; i++)
                {
                    // Sin asiento: infantes (viajan en brazos) y tarifas donde se deja para el check-in.
                    _asientos[t].TryGetValue(i, out Asiento_GV42 asiento);
                    var asientoPasajero = new AsientoPasajero_GV42(_pasajeros[i].Dni.Text.Trim(), asiento) { Tramo = numeroTramo };
                    // Valijas extra a nombre de este pasajero: son las que va a poder despachar en el check-in de ese tramo.
                    CtrlEquipajePasajero_GV42 filaEquipaje = _filasEquipaje[t].FirstOrDefault(f => (int)f.Tag == i);
                    if (filaEquipaje != null) asientoPasajero.EquipajeExtra = filaEquipaje.Cantidad;
                    borrador.AsientosPorPasajero.Add(asientoPasajero);
                }

                foreach (var f in _filasAdicionales[t].Where(f => f.Seleccionado))
                {
                    borrador.Adicionales.Add(new AdicionalReserva_GV42
                    {
                        TipoAdicional = f.Tipo,
                        Tramo = numeroTramo,
                        Cantidad = f.Cantidad,
                        CostoUnitario = f.CostoUnitario
                    });
                }
            }

            _reservaGenerada = _bll.GenerarReserva(borrador);
            MostrarResultado();
            IrAPaso(PasoResultado());
        }

        private void MostrarResultado()
        {
            lblResultadoEstado.Text = IdiomaManager_GV42.T("reservar.resultado.estado", _reservaGenerada.EstadoTexto);
            // Plazo para pagar: pasado ese momento la reserva vence y libera los asientos.
            if (_reservaGenerada.FechaVencimiento.HasValue)
                lblResultadoEstado.Text += "  ·  " + IdiomaManager_GV42.T("reservar.resultado.vence",
                    _reservaGenerada.FechaVencimiento.Value.ToString("dd/MM HH:mm"));
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
                string clave = ClavePasoActual;
                if (_pasoActual == 0) ValidarYAvanzarBusqueda();
                else if (clave == "vuelta") ValidarYAvanzarVuelta();
                else if (clave == "tarifa") ValidarYAvanzarTarifa();
                else if (clave == "cliente") ValidarYAvanzarCliente();
                else if (clave == "pasajeros") ValidarYAvanzarPasajeros();
                else if (EsPasoAsientos()) ValidarYAvanzarAsientos();
                else if (EsPasoAdicionales())
                {
                    // Después de los adicionales de la ida vienen los asientos de la vuelta; después del
                    // último tramo, el resumen.
                    if (_clavesPaso[_pasoActual + 1] == "resumen") ArmarResumen();
                    IrAPaso(_pasoActual + 1);
                }
                else if (clave == "resumen") ConfirmarReserva();
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
            // Ida y vuelta tiene más pasos (vuelo de regreso, asientos y adicionales de la vuelta).
            if (!_refrescandoIdioma && _pasos != null && _pasoActual == 0)
            {
                ConfigurarPasos(TipoViajeElegido() == TipoViaje_GV42.IdaYVuelta);
                MostrarPasoActual();
            }
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

                // Fechas flexibles: días cercanos con su precio más barato, para cambiar de día con un clic.
                try
                {
                    List<PrecioFecha_GV42> dias = _bll.PreciosPorFecha(criterio);
                    ctrlFechasIda.Cargar(dias, criterio.FechaSalida);
                    ctrlFechasIda.Visible = dias.Any(d => d.HayVuelos);
                }
                catch (Exception)
                {
                    ctrlFechasIda.Visible = false;   // son una ayuda: si fallan, la búsqueda sigue igual
                }

                if (_resultados.Count == 0)
                    MessageBox.Show(IdiomaManager_GV42.T(ctrlFechasIda.Visible ? "reservar.sinResultadosOtroDia" : "reservar.sinResultados"), IdiomaManager_GV42.T("reservar.sinResultadosTitulo"),
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

        // Fechas flexibles de la ida: se cambia la fecha de salida y se vuelve a buscar.
        private void ctrlFechasIda_FechaElegida(object sender, DateTime fecha)
        {
            dtSalida.Value = fecha;
            btnBuscarVuelos_Click(sender, EventArgs.Empty);
        }

        // Fechas flexibles de la vuelta: se cambia la fecha de regreso sin tocar la ida ya elegida.
        private void ctrlFechasVuelta_FechaElegida(object sender, DateTime fecha)
        {
            if (_vuelos[IDA] == null) return;
            try
            {
                _cambiandoFechaRegreso = true;
                try { dtRegreso.Value = fecha; }
                finally { _cambiandoFechaRegreso = false; }

                _vuelos[VUELTA] = null;
                CargarVuelosDeRegreso(_vuelos[IDA]);
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

        // Tarifa donde elegir asiento es pago: tildado = no se eligen (se asignan gratis en el check-in).
        private void chkAsientoAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAsientoAutomatico.Visible) return;
            _sinAsientos[_tramo] = chkAsientoAutomatico.Checked;
            ctrlButacas.Enabled = !chkAsientoAutomatico.Checked;
            if (chkAsientoAutomatico.Checked) AsientosTramo.Clear();
            RefrescarButacas();
        }

        private void ctrlButacas_AsientoClickeado(object sender, Asiento_GV42 asiento)
        {
            AsientosTramo[_indicePasajeroActivo] = asiento;
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
