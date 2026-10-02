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
    // Cambio de fecha / vuelo de UN tramo (ida o vuelta) de una reserva confirmada.
    // Las reglas están en BLLReserva_GV42 (región "Cambio de vuelo"): misma ruta, hasta 24 hs antes
    // de la salida y sin check-in hecho en ese tramo. Se cobra la penalidad de la tarifa más la
    // diferencia de tarifa con impuestos si el vuelo nuevo es más caro (si es más barato no se
    // devuelve nada) y se vuelven a elegir los asientos en el vuelo nuevo.
    //
    // La pantalla es un asistente de dos pasos para que no quede amontonada:
    //   1. Elegir el vuelo: tramo, vuelo actual, búsqueda del vuelo nuevo y costo del cambio.
    //   2. Asientos y pago: mapa de butacas del vuelo nuevo, resumen y medio de pago (si hay algo que cobrar).
    // El diseño está en FRMCambiarVuelo_GV42.Designer.cs (Form Designer).
    public partial class FRMCambiarVuelo_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private const int PASO_VUELO = 0;
        private const int PASO_ASIENTOS_Y_PAGO = 1;
        private const string FORMATO_FECHA_HORA = "dd/MM/yyyy HH:mm";
        private const string SIN_DATO = "—";

        // Valores que devuelve Validaciones_GV42.MarcaTarjeta.
        private const string MARCA_AMEX = "American Express";
        private const string MARCA_GENERICA = "Tarjeta";

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private readonly string _numeroReserva;

        private Reserva_GV42 _reserva;

        // Si la reserva no se pudo cargar, el error se muestra recién cuando la ventana ya está a la vista.
        private Exception _errorCarga;

        // Tramo que se está cambiando y, si no se puede cambiar, el motivo (ya traducido por la BLL).
        private int _tramo = Reserva_GV42.TRAMO_IDA;
        private string _motivo;

        // Paso 1: resultado de la búsqueda, vuelo elegido y lo que cuesta cambiar a ese vuelo.
        private int _paso = PASO_VUELO;
        private List<VueloClase_GV42> _resultados;
        private DateTime _fechaBuscada;
        private VueloClase_GV42 _vueloNuevo;
        private CotizacionCambio_GV42 _cotizacion;

        // Mientras se enlaza la grilla, el DataGridView selecciona filas solo: esas selecciones no cuentan.
        private bool _cargandoGrilla;

        // Paso 2: asientos del vuelo nuevo, por DNI del pasajero (los infantes no ocupan asiento).
        private bool _requiereAsientos;
        private List<AsientoDisponibilidad_GV42> _mapa;
        private readonly Dictionary<string, Asiento_GV42> _asientos = new Dictionary<string, Asiento_GV42>();
        private int _indicePasajeroActivo;

        // Evita que el formateo del número de tarjeta (grupos de 4) se dispare a sí mismo.
        private bool _formateandoNumero;

        // Medios de pago que se ofrecen (sin efectivo si el usuario no es vendedor).
        private readonly List<MedioPago_GV42> _medios = new List<MedioPago_GV42>
        {
            MedioPago_GV42.TarjetaDebito, MedioPago_GV42.TarjetaCredito, MedioPago_GV42.Transferencia, MedioPago_GV42.Efectivo
        };

        #endregion

        #region Constructor

        public FRMCambiarVuelo_GV42(string numeroReserva)
        {
            InitializeComponent();
            _numeroReserva = numeroReserva;

            dgvVuelos.AutoGenerateColumns = false;
            dtpFecha.MinDate = DateTime.Today;

            // Solo un vendedor cobra en efectivo (el cliente autogestionado paga con tarjeta o transferencia).
            try
            {
                if (!_bll.PuedeRegistrarPagoDeTerceros())
                    _medios.Remove(MedioPago_GV42.Efectivo);
            }
            catch { _medios.Remove(MedioPago_GV42.Efectivo); }

            CargarMediosDePago();
            CargarAniosVencimiento();

            txtNumeroOperacion.MaxLength = Validaciones_GV42.MAX_NUMERO_TRANSACCION;
            // Dígitos más un espacio cada 4 (el número se muestra agrupado: "4509 9535 6623 3704").
            txtNumeroTarjeta.MaxLength = Validaciones_GV42.MAX_DIGITOS_TARJETA + (Validaciones_GV42.MAX_DIGITOS_TARJETA - 1) / 4;
            txtTitular.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            MostrarSeccionMedio();

            CargarReserva();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
            IrAPaso(PASO_VUELO);
        }

        #endregion

        #region Propiedades

        private VueloClase_GV42 VueloActual => _reserva != null ? _reserva.VueloClaseDeTramo(_tramo) : null;

        // Pasajeros que ocupan asiento: los infantes viajan en brazos y no eligen butaca.
        private List<Pasajero_GV42> PasajerosConAsiento =>
            _reserva != null && _reserva.Pasajeros != null
                ? _reserva.Pasajeros.Where(p => p != null && !p.EsInfante).ToList()
                : new List<Pasajero_GV42>();

        private bool HayQueCobrar => _cotizacion != null && _cotizacion.Total > 0;

        private MedioPago_GV42? MedioSeleccionado =>
            cmbMedioPago.SelectedItem is MedioPago_GV42 ? (MedioPago_GV42?)(MedioPago_GV42)cmbMedioPago.SelectedItem : null;

        private string DigitosTarjeta => Validaciones_GV42.SoloDigitos(txtNumeroTarjeta.Text);

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("cambio.tituloVentana");
            lblTitulo.Text = IdiomaManager_GV42.T("cambio.titulo");

            // Paso 1
            lblSeccionActual.Text = IdiomaManager_GV42.T("cambio.seccionActual");
            rbIda.Text = IdiomaManager_GV42.T("tramo.ida");
            rbVuelta.Text = IdiomaManager_GV42.T("tramo.vuelta");
            lblVueloEt.Text = IdiomaManager_GV42.T("pago.vuelo");
            lblRutaEt.Text = IdiomaManager_GV42.T("pago.ruta");
            lblSalidaEt.Text = IdiomaManager_GV42.T("pago.salida");
            lblClaseEt.Text = IdiomaManager_GV42.T("pago.clase");

            lblSeccionNuevo.Text = IdiomaManager_GV42.T("cambio.seccionNuevo");
            lblFecha.Text = IdiomaManager_GV42.T("cambio.fecha");
            btnBuscar.Text = IdiomaManager_GV42.T("cambio.buscar");
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

            lblSeccionCosto.Text = IdiomaManager_GV42.T("cambio.seccionCosto");
            lblTarifaActualEt.Text = IdiomaManager_GV42.T("cambio.tarifaActual");
            lblTarifaNuevaEt.Text = IdiomaManager_GV42.T("cambio.tarifaNueva");
            lblDiferenciaEt.Text = IdiomaManager_GV42.T("cambio.diferencia");
            lblImpuestosEt.Text = IdiomaManager_GV42.T("cambio.impuestos");
            lblTotalCostoEt.Text = IdiomaManager_GV42.T("cambio.total");

            // Paso 2
            lblSeccionAsientos.Text = IdiomaManager_GV42.T("cambio.seccionAsientos");
            btnPasajeroAnterior.Text = IdiomaManager_GV42.T("reservar.pasajeroAnterior");
            btnPasajeroSiguiente.Text = IdiomaManager_GV42.T("reservar.pasajeroSiguiente");
            lblSinAsientos.Text = IdiomaManager_GV42.T("cambio.asientosEnCheckIn");
            ctrlButacas.ActualizarIdioma();

            lblSeccionResumen.Text = IdiomaManager_GV42.T("cambio.seccionResumen");
            lblResumenActualEt.Text = IdiomaManager_GV42.T("cambio.seccionActual");
            lblResumenNuevoEt.Text = IdiomaManager_GV42.T("cambio.seccionNuevo");
            lblTotalEt.Text = IdiomaManager_GV42.T("cambio.total");

            lblSeccionPago.Text = IdiomaManager_GV42.T("cambio.seccionPago");
            lblMedioPago.Text = IdiomaManager_GV42.T("pago.medioPago");
            lblNumeroTarjeta.Text = IdiomaManager_GV42.T("pago.numeroTarjeta");
            lblTitular.Text = IdiomaManager_GV42.T("pago.titular");
            lblVencimiento.Text = IdiomaManager_GV42.T("pago.vencimiento");
            lblCodigoSeguridad.Text = IdiomaManager_GV42.T("pago.codigoSeguridad");
            lblAyudaCodigo.Text = IdiomaManager_GV42.T("pago.ayudaCodigo");
            lblNumeroOperacion.Text = IdiomaManager_GV42.T("pago.numeroOperacion");
            lblAyudaOperacion.Text = IdiomaManager_GV42.T("pago.ayudaOperacion");
            lblEfectivo.Text = IdiomaManager_GV42.T("pago.efectivoAutomatico");
            CargarMediosDePago();
            ActualizarIndicadorTarjeta();

            // Botonera
            btnAtras.Text = IdiomaManager_GV42.T("reservar.atras");
            btnSiguiente.Text = IdiomaManager_GV42.T("reservar.siguiente");
            btnConfirmar.Text = IdiomaManager_GV42.T("cambio.confirmar");
            btnCerrar.Text = IdiomaManager_GV42.T("cambio.cerrar");
            MostrarTextoPaso();

            // Lo que se arma con datos se regenera para que cambie de idioma en caliente. El motivo lo
            // traduce la BLL: solo hace falta volver a pedirlo si había uno a la vista.
            if (_motivo != null) EvaluarTramo();
            MostrarSubtitulo();
            MostrarVueloActual();
            MostrarAyudaVuelos();
            MostrarCosto();
            MostrarResumen();
            MostrarPasajeroActivo();
        }

        // Vuelve a cargar los medios de pago conservando el elegido (el texto lo pone cmbMedioPago_Format).
        private void CargarMediosDePago()
        {
            object seleccionado = cmbMedioPago.SelectedItem;
            cmbMedioPago.BeginUpdate();
            cmbMedioPago.Items.Clear();
            foreach (MedioPago_GV42 m in _medios) cmbMedioPago.Items.Add(m);
            cmbMedioPago.EndUpdate();
            if (seleccionado != null && cmbMedioPago.Items.Contains(seleccionado))
                cmbMedioPago.SelectedItem = seleccionado;
        }

        private void MostrarTextoPaso()
        {
            lblPaso.Text = IdiomaManager_GV42.T(_paso == PASO_VUELO ? "cambio.paso1" : "cambio.paso2");
        }

        #endregion

        #region Carga de datos

        // La reserva se busca una sola vez al abrir. Si falla (sin conexión, reserva inexistente) la
        // ventana se abre igual, con todo deshabilitado, y el error se avisa en el evento Shown.
        private void CargarReserva()
        {
            try
            {
                _reserva = _bll.BuscarReserva(_numeroReserva);
                if (_reserva == null)
                    _errorCarga = new NegocioException_GV42(IdiomaManager_GV42.T("pago.noExiste"));
            }
            catch (Exception ex)
            {
                _reserva = null;
                _errorCarga = ex;
            }

            rbVuelta.Visible = _reserva != null && _reserva.TieneVuelta;
            SeleccionarTramo(Reserva_GV42.TRAMO_IDA);
        }

        // Años de vencimiento: desde el actual hasta el máximo que acepta la validación (+15).
        private void CargarAniosVencimiento()
        {
            int actual = DateTime.Today.Year;
            cmbAnio.Items.Clear();
            for (int a = actual; a <= actual + Validaciones_GV42.MAX_ANIOS_VENCIMIENTO; a++)
                cmbAnio.Items.Add(a.ToString());
        }

        private void MostrarSubtitulo()
        {
            if (_reserva != null && _reserva.Tarifa != null && !string.IsNullOrEmpty(_reserva.Tarifa.Nombre))
                lblSubtitulo.Text = IdiomaManager_GV42.T("cambio.subtitulo", _reserva.NumeroReserva, _reserva.Tarifa.Nombre);
            else
                lblSubtitulo.Text = IdiomaManager_GV42.T("cambio.subtituloSinTarifa", _reserva != null ? _reserva.NumeroReserva : _numeroReserva);
        }

        // Texto corto de un vuelo para el resumen y los mensajes: "AR1500 · AEP -> COR · 15/10/2026 08:00 · Económica".
        private static string DescribirVuelo(VueloClase_GV42 vc)
        {
            if (vc == null || vc.Vuelo == null) return SIN_DATO;
            return vc.CodigoVuelo + " · " + Ruta(vc.Vuelo, true) + " · "
                 + vc.FechaHoraSalida.ToString(FORMATO_FECHA_HORA) + " · " + vc.Clase.Texto();
        }

        // En el resumen del paso 2 no hace falta la ruta: es la misma en los dos vuelos.
        private static string DescribirVueloCorto(VueloClase_GV42 vc)
        {
            if (vc == null || vc.Vuelo == null) return SIN_DATO;
            return vc.CodigoVuelo + " · " + vc.FechaHoraSalida.ToString(FORMATO_FECHA_HORA) + " · " + vc.Clase.Texto();
        }

        private static string Ruta(Vuelo_GV42 vuelo, bool soloCodigos)
        {
            if (vuelo == null) return SIN_DATO;
            Func<Aeropuerto_GV42, string> nombre = a =>
                a == null ? "?" : (soloCodigos || string.IsNullOrEmpty(a.Descripcion) ? a.CodigoIata : a.Descripcion);
            return nombre(vuelo.Origen) + " -> " + nombre(vuelo.Destino);
        }

        #endregion

        #region Tramo y vuelo actual

        // Cambiar de tramo descarta lo que se había elegido: la búsqueda, el costo y los asientos son de otro vuelo.
        private void SeleccionarTramo(int tramo)
        {
            _tramo = tramo;
            LimpiarBusqueda();
            EvaluarTramo();

            // La fecha arranca en la del vuelo actual (si ya pasó, hoy).
            VueloClase_GV42 actual = VueloActual;
            DateTime sugerida = actual != null && actual.Vuelo != null ? actual.FechaHoraSalida.Date : DateTime.Today;
            dtpFecha.Value = sugerida < dtpFecha.MinDate ? dtpFecha.MinDate : sugerida;

            MostrarVueloActual();
            MostrarAyudaVuelos();
            MostrarCosto();
        }

        // Le pregunta a la BLL si ese tramo se puede cambiar. Si la consulta falla, tampoco se deja seguir.
        private void EvaluarTramo()
        {
            if (_reserva == null) { _motivo = null; return; }
            try { _motivo = _bll.MotivoNoSePuedeCambiar(_reserva, _tramo); }
            catch (Exception ex) { _motivo = ex.Message; }
        }

        private void MostrarVueloActual()
        {
            VueloClase_GV42 actual = VueloActual;
            bool hay = actual != null && actual.Vuelo != null;
            lblVueloValor.Text = hay ? actual.CodigoVuelo : SIN_DATO;
            lblRutaValor.Text = hay ? Ruta(actual.Vuelo, false) : SIN_DATO;
            lblSalidaValor.Text = hay ? actual.FechaHoraSalida.ToString(FORMATO_FECHA_HORA) : SIN_DATO;
            lblClaseValor.Text = hay ? actual.Clase.Texto() : SIN_DATO;

            // Debajo de los datos: en rojo el motivo por el que no se puede cambiar o, si se puede, el plazo.
            if (_reserva == null)
            {
                lblMotivo.ForeColor = Tema_GV42.Error;
                lblMotivo.Text = IdiomaManager_GV42.T("cambio.noSePudoCargar");
            }
            else if (_motivo != null)
            {
                lblMotivo.ForeColor = Tema_GV42.Error;
                lblMotivo.Text = _motivo;
            }
            else
            {
                lblMotivo.ForeColor = Tema_GV42.TextoSecundario;
                lblMotivo.Text = IdiomaManager_GV42.T("cambio.plazo", BLLReserva_GV42.HORAS_LIMITE_CAMBIO);
            }
            ActualizarEstado();
        }

        // Habilita cada parte según lo que ya se cargó: sin reserva o con un tramo que no se puede
        // cambiar no se busca; sin vuelo elegido y cotizado no se pasa al paso 2.
        private void ActualizarEstado()
        {
            bool hayReserva = _reserva != null;
            bool sePuede = hayReserva && _motivo == null;
            rbIda.Enabled = hayReserva;
            rbVuelta.Enabled = hayReserva;
            pnlVueloNuevo.Enabled = sePuede;
            btnSiguiente.Enabled = sePuede && _vueloNuevo != null && _cotizacion != null;
        }

        #endregion

        #region Búsqueda del vuelo nuevo

        private void LimpiarBusqueda()
        {
            _resultados = null;
            _vueloNuevo = null;
            _cotizacion = null;
            _mapa = null;
            _asientos.Clear();
            _cargandoGrilla = true;
            try { dgvVuelos.DataSource = null; }
            finally { _cargandoGrilla = false; }
        }

        private void BuscarVuelos()
        {
            LimpiarBusqueda();
            _fechaBuscada = dtpFecha.Value.Date;
            try
            {
                // Sin filtro de clase: se puede cambiar a cualquier clase que ofrezca el vuelo.
                _resultados = _bll.BuscarVuelosParaCambio(_numeroReserva, _tramo, _fechaBuscada, null);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambio.accionBuscar"), ex);
            }
            MostrarResultados();
        }

        private void MostrarResultados()
        {
            _cargandoGrilla = true;
            try
            {
                dgvVuelos.DataSource = null;
                dgvVuelos.DataSource = _resultados;
                // El vuelo lo tiene que elegir el usuario: no se deja la primera fila seleccionada sola.
                dgvVuelos.ClearSelection();
                dgvVuelos.CurrentCell = null;
            }
            finally { _cargandoGrilla = false; }

            MostrarAyudaVuelos();
            MostrarCosto();
            ActualizarEstado();
        }

        private void MostrarAyudaVuelos()
        {
            if (_resultados == null)
                lblAyudaVuelos.Text = IdiomaManager_GV42.T("cambio.ayudaVuelos");
            else if (_resultados.Count == 0)
                lblAyudaVuelos.Text = IdiomaManager_GV42.T("cambio.sinVuelos", _fechaBuscada.ToString("dd/MM/yyyy"));
            else
                lblAyudaVuelos.Text = IdiomaManager_GV42.T("cambio.vuelosEncontrados", _resultados.Count, _fechaBuscada.ToString("dd/MM/yyyy"));
        }

        // Al elegir un vuelo se cotiza el cambio. Los asientos elegidos antes eran de otro vuelo: se descartan.
        private void ElegirVuelo(VueloClase_GV42 elegido)
        {
            _vueloNuevo = null;
            _cotizacion = null;
            _mapa = null;
            _asientos.Clear();

            if (elegido != null && elegido.Vuelo != null)
            {
                try
                {
                    _cotizacion = _bll.CotizarCambio(_numeroReserva, _tramo, elegido.Vuelo.Id, elegido.Clase);
                    _vueloNuevo = elegido;
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambio.accionCotizar"), ex);
                }
            }
            MostrarCosto();
            ActualizarEstado();
        }

        #endregion

        #region Costo del cambio

        // Detalle de lo que se cobra (se regenera al cambiar el idioma). Sin vuelo elegido queda en blanco.
        private void MostrarCosto()
        {
            bool hay = _cotizacion != null;
            lblAyudaCosto.Visible = !hay;
            lblAyudaCosto.Text = IdiomaManager_GV42.T("cambio.ayudaCosto");
            lblPenalidadEt.Text = IdiomaManager_GV42.T("cambio.penalidad", (hay ? _cotizacion.PorcentajePenalidad : PorcentajeDeLaTarifa()).ToString("0.##"));

            lblTarifaActualValor.Text = hay ? _cotizacion.TarifaActual.ToString("C2") : SIN_DATO;
            lblTarifaNuevaValor.Text = hay ? _cotizacion.TarifaNueva.ToString("C2") : SIN_DATO;
            lblPenalidadValor.Text = hay ? _cotizacion.Penalidad.ToString("C2") : SIN_DATO;
            lblDiferenciaValor.Text = hay ? _cotizacion.DiferenciaTarifa.ToString("C2") : SIN_DATO;
            lblImpuestosValor.Text = hay ? _cotizacion.ImpuestosDiferencia.ToString("C2") : SIN_DATO;
            lblTotalCostoValor.Text = TextoTotal();
            lblTotalCostoValor.ForeColor = hay && !HayQueCobrar ? Tema_GV42.Exito : Tema_GV42.Acento;
        }

        private decimal PorcentajeDeLaTarifa()
        {
            return _reserva != null && _reserva.Tarifa != null ? _reserva.Tarifa.PorcentajePenalidadCambio : 0m;
        }

        // "Sin costo" cuando no hay penalidad ni diferencia que cobrar.
        private string TextoTotal()
        {
            if (_cotizacion == null) return SIN_DATO;
            return HayQueCobrar ? _cotizacion.Total.ToString("C2") : IdiomaManager_GV42.T("cambio.sinCosto");
        }

        // Resumen del paso 2: de qué vuelo a qué vuelo y cuánto se paga.
        private void MostrarResumen()
        {
            lblResumenActual.Text = DescribirVueloCorto(VueloActual);
            lblResumenNuevo.Text = DescribirVueloCorto(_vueloNuevo);
            lblTotalValor.Text = TextoTotal();
            lblTotalValor.ForeColor = _cotizacion != null && !HayQueCobrar ? Tema_GV42.Exito : Tema_GV42.Acento;
            // El pago solo se pide si hay algo que cobrar.
            pnlPago.Visible = HayQueCobrar;
        }

        #endregion

        #region Pasos

        private void IrAPaso(int paso)
        {
            _paso = paso;
            bool pasoVuelo = paso == PASO_VUELO;
            pnlPaso1.Visible = pasoVuelo;
            pnlPaso2.Visible = !pasoVuelo;
            btnSiguiente.Visible = pasoVuelo;
            btnAtras.Visible = !pasoVuelo;
            btnConfirmar.Visible = !pasoVuelo;
            MostrarTextoPaso();

            if (!pasoVuelo)
            {
                MostrarResumen();
                MostrarAsientos();
            }
        }

        // Antes de pasar al paso 2 se averigua si hay que elegir asientos y se trae el mapa del vuelo nuevo.
        private bool PrepararAsientos()
        {
            try
            {
                _requiereAsientos = _bll.CambioRequiereAsientos(_reserva, _tramo);
                if (_requiereAsientos && _mapa == null)
                {
                    _mapa = _bll.ObtenerMapaAsientos(_vueloNuevo.Vuelo.Id, _vueloNuevo.Clase);
                    _indicePasajeroActivo = 0;
                }
                return true;
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambio.accionMapa"), ex);
            }
            return false;
        }

        #endregion

        #region Asientos

        // Con tarifa Light sin asientos comprados no se elige nada: se asignan en el check-in y no se muestra el mapa.
        private void MostrarAsientos()
        {
            lblSinAsientos.Visible = !_requiereAsientos;
            lblPasajeroActual.Visible = _requiereAsientos;
            btnPasajeroAnterior.Visible = _requiereAsientos;
            btnPasajeroSiguiente.Visible = _requiereAsientos;
            lblAsientosElegidos.Visible = _requiereAsientos;
            ctrlButacas.Visible = _requiereAsientos;
            if (!_requiereAsientos) return;

            // Las butacas preferenciales llevan recargo: en un cambio solo se ofrecen si la tarifa las incluye.
            ctrlButacas.PermitirPreferenciales = _reserva != null && _reserva.Tarifa != null && _reserva.Tarifa.IncluyePreferencial;
            ctrlButacas.RecargoPreferencial = 0m;
            RefrescarButacas();
        }

        private void RefrescarButacas()
        {
            if (_mapa == null) return;
            List<Pasajero_GV42> conAsiento = PasajerosConAsiento;
            if (conAsiento.Count == 0) return;
            if (_indicePasajeroActivo >= conAsiento.Count) _indicePasajeroActivo = 0;
            string dniActivo = conAsiento[_indicePasajeroActivo].DNI;

            MostrarPasajeroActivo();
            btnPasajeroAnterior.Enabled = _indicePasajeroActivo > 0;
            btnPasajeroSiguiente.Enabled = _indicePasajeroActivo < conAsiento.Count - 1;

            // Los asientos que ya eligieron los otros pasajeros de esta reserva se pintan como asignados.
            var ocupadosLocalmente = new HashSet<int>(_asientos.Where(kv => kv.Key != dniActivo).Select(kv => kv.Value.Id));
            int? miAsiento = _asientos.TryGetValue(dniActivo, out Asiento_GV42 a) ? a.Id : (int?)null;
            ctrlButacas.CargarMapa(_mapa, ocupadosLocalmente, miAsiento);
        }

        // Nombre del pasajero que está eligiendo y su asiento (se regenera al cambiar el idioma).
        private void MostrarPasajeroActivo()
        {
            List<Pasajero_GV42> conAsiento = PasajerosConAsiento;
            if (!_requiereAsientos || conAsiento.Count == 0 || _indicePasajeroActivo >= conAsiento.Count)
            {
                lblPasajeroActual.Text = string.Empty;
                lblAsientosElegidos.Text = string.Empty;
                return;
            }
            Pasajero_GV42 activo = conAsiento[_indicePasajeroActivo];
            string asiento = _asientos.TryGetValue(activo.DNI, out Asiento_GV42 a)
                ? IdiomaManager_GV42.T("cambio.asientoElegido", a.NumeroAsiento)
                : IdiomaManager_GV42.T("cambio.asientoSinElegir");
            lblPasajeroActual.Text = IdiomaManager_GV42.T("cambio.asientoPara", activo.NombreCompleto, _indicePasajeroActivo + 1, conAsiento.Count)
                                     + "   ·   " + asiento;
            lblAsientosElegidos.Text = IdiomaManager_GV42.T("cambio.asientosElegidos", _asientos.Count, conAsiento.Count);
        }

        private void CambiarPasajeroActivo(int delta)
        {
            int posicion = _indicePasajeroActivo + delta;
            if (posicion < 0 || posicion >= PasajerosConAsiento.Count) return;
            _indicePasajeroActivo = posicion;
            RefrescarButacas();
        }

        // Si la BLL rechaza el cambio porque otro pasajero tomó una butaca, se vuelve a traer el mapa y
        // se sueltan los asientos elegidos que ya no están libres.
        private void RecargarMapa()
        {
            if (!_requiereAsientos || _vueloNuevo == null) return;
            try
            {
                _mapa = _bll.ObtenerMapaAsientos(_vueloNuevo.Vuelo.Id, _vueloNuevo.Clase);
                var ocupados = new HashSet<int>(_mapa.Where(m => m.Ocupado && m.Asiento != null).Select(m => m.Asiento.Id));
                foreach (string dni in _asientos.Where(kv => ocupados.Contains(kv.Value.Id)).Select(kv => kv.Key).ToList())
                    _asientos.Remove(dni);
                RefrescarButacas();
            }
            catch { }   // el mapa viejo sigue a la vista; el error ya se mostró
        }

        #endregion

        #region Tarjeta

        private static bool EsTarjeta(MedioPago_GV42? medio) =>
            medio == MedioPago_GV42.TarjetaCredito || medio == MedioPago_GV42.TarjetaDebito;

        // Nombre de la marca para mostrar ("Tarjeta" cuando no se reconoce el prefijo, traducido).
        private static string MarcaParaMostrar(string marca) =>
            marca == MARCA_GENERICA ? IdiomaManager_GV42.T("pago.marcaGenerica") : marca;

        // Muestra solo la sección del medio elegido: tarjeta, transferencia o el aviso de efectivo.
        private void MostrarSeccionMedio()
        {
            MedioPago_GV42? medio = MedioSeleccionado;
            pnlSeccionTarjeta.Visible = EsTarjeta(medio);
            pnlSeccionTransferencia.Visible = medio == MedioPago_GV42.Transferencia;
            pnlSeccionEfectivo.Visible = medio == MedioPago_GV42.Efectivo;
            ActualizarIndicadorTarjeta();
        }

        // Agrupa el número de a 4 mientras se tipea (o se pega) y conserva la posición del cursor.
        private void FormatearNumeroTarjeta()
        {
            string texto = txtNumeroTarjeta.Text;
            int cursor = Math.Min(txtNumeroTarjeta.SelectionStart, texto.Length);
            int digitosAntesDelCursor = Validaciones_GV42.SoloDigitos(texto.Substring(0, cursor)).Length;

            string digitos = Validaciones_GV42.SoloDigitos(texto);
            if (digitos.Length > Validaciones_GV42.MAX_DIGITOS_TARJETA)
                digitos = digitos.Substring(0, Validaciones_GV42.MAX_DIGITOS_TARJETA);

            var sb = new StringBuilder();
            for (int i = 0; i < digitos.Length; i++)
            {
                if (i > 0 && i % 4 == 0) sb.Append(' ');
                sb.Append(digitos[i]);
            }
            string formateado = sb.ToString();
            if (formateado == texto) return;

            // Posición del cursor en el texto nuevo: después de la misma cantidad de dígitos.
            int pos = 0, vistos = 0;
            while (pos < formateado.Length && vistos < digitosAntesDelCursor)
            {
                if (char.IsDigit(formateado[pos])) vistos++;
                pos++;
            }

            _formateandoNumero = true;
            try
            {
                txtNumeroTarjeta.Text = formateado;
                txtNumeroTarjeta.SelectionStart = Math.Min(pos, formateado.Length);
            }
            finally { _formateandoNumero = false; }
        }

        // Indicador en vivo debajo del número: marca y validez (Luhn). Mientras faltan dígitos
        // solo muestra la marca reconocida; con el número completo dice si es válido o no.
        private void ActualizarIndicadorTarjeta()
        {
            string d = DigitosTarjeta;
            if (d.Length < 2)
            {
                lblEstadoTarjeta.Text = string.Empty;
                return;
            }

            string marca = Validaciones_GV42.MarcaTarjeta(d);
            bool amex = marca == MARCA_AMEX;
            int largoEsperado = amex ? 15 : 16;
            bool completo = d.Length >= largoEsperado ||
                            (d.Length >= Validaciones_GV42.MIN_DIGITOS_TARJETA && !txtNumeroTarjeta.Focused);

            if (amex && MedioSeleccionado == MedioPago_GV42.TarjetaDebito)
            {
                lblEstadoTarjeta.ForeColor = Tema_GV42.Error;
                lblEstadoTarjeta.Text = "✗ " + IdiomaManager_GV42.T("pago.amexDebito");
            }
            else if (!completo)
            {
                lblEstadoTarjeta.ForeColor = Tema_GV42.TextoSecundario;
                lblEstadoTarjeta.Text = marca == MARCA_GENERICA ? string.Empty : MarcaParaMostrar(marca);
            }
            else if (Validaciones_GV42.EsNumeroTarjetaValido(d))
            {
                lblEstadoTarjeta.ForeColor = Tema_GV42.Exito;
                lblEstadoTarjeta.Text = "✓ " + IdiomaManager_GV42.T("pago.tarjetaValida", MarcaParaMostrar(marca));
            }
            else
            {
                lblEstadoTarjeta.ForeColor = Tema_GV42.Error;
                lblEstadoTarjeta.Text = "✗ " + IdiomaManager_GV42.T("pago.tarjetaInvalida");
            }
        }

        // Código de seguridad: 4 dígitos para American Express, 3 para el resto.
        private void AjustarLargoCodigo()
        {
            int largo = Validaciones_GV42.EsAmex(DigitosTarjeta) ? 4 : 3;
            txtCodigoSeguridad.MaxLength = largo;
            if (txtCodigoSeguridad.Text.Length > largo)
                txtCodigoSeguridad.Text = txtCodigoSeguridad.Text.Substring(0, largo);
        }

        // Los datos de la tarjeta nunca quedan en pantalla después de usarlos.
        private void LimpiarDatosDePago()
        {
            txtNumeroTarjeta.Clear();
            txtTitular.Clear();
            cmbMes.SelectedIndex = -1;
            cmbAnio.SelectedIndex = -1;
            txtCodigoSeguridad.Clear();
            txtCodigoSeguridad.MaxLength = 3;
            txtNumeroOperacion.Clear();
            ActualizarIndicadorTarjeta();
        }

        #endregion

        #region Validaciones

        // Prevalidación en pantalla, campo por campo y marcando el campo exacto (la BLL vuelve a validar todo).
        private bool ValidarTarjeta(MedioPago_GV42 medio, out DatosTarjeta_GV42 tarjeta)
        {
            tarjeta = null;
            string numero = DigitosTarjeta;

            if (!Validaciones_GV42.EsNumeroTarjetaValido(txtNumeroTarjeta.Text))
            { Tema_GV42.MostrarError(txtNumeroTarjeta, Validaciones_GV42.MENSAJE_TARJETA); return false; }

            // American Express no emite tarjetas de débito (la BLL también lo rechaza).
            bool amex = Validaciones_GV42.EsAmex(numero);
            if (medio == MedioPago_GV42.TarjetaDebito && amex)
            { Tema_GV42.MostrarError(cmbMedioPago, IdiomaManager_GV42.T("pago.errAmexDebito")); return false; }

            if (!Validaciones_GV42.EsTitularTarjetaValido(txtTitular.Text))
            { Tema_GV42.MostrarError(txtTitular, IdiomaManager_GV42.T("pago.errTitular")); return false; }

            if (cmbMes.SelectedIndex < 0)
            { Tema_GV42.MostrarError(cmbMes, IdiomaManager_GV42.T("pago.errMes")); return false; }
            if (cmbAnio.SelectedIndex < 0)
            { Tema_GV42.MostrarError(cmbAnio, IdiomaManager_GV42.T("pago.errAnio")); return false; }

            int mes = cmbMes.SelectedIndex + 1;
            int anio = int.Parse(cmbAnio.SelectedItem.ToString());
            if (!Validaciones_GV42.EsVencimientoValido(mes, anio, DateTime.Today))
            { Tema_GV42.MostrarError(cmbMes, IdiomaManager_GV42.T("pago.errVencida")); return false; }

            if (!Validaciones_GV42.EsCodigoSeguridadValido(txtCodigoSeguridad.Text, numero))
            {
                Tema_GV42.MostrarError(txtCodigoSeguridad, IdiomaManager_GV42.T(amex ? "pago.errCodigoAmex" : "pago.errCodigo"));
                return false;
            }

            tarjeta = new DatosTarjeta_GV42
            {
                Numero = numero,
                Titular = Validaciones_GV42.NormalizarEspacios(txtTitular.Text),
                MesVencimiento = mes,
                AnioVencimiento = anio,
                CodigoSeguridad = txtCodigoSeguridad.Text
            };
            return true;
        }

        // Datos del pago según el medio elegido. Si Total = 0 no se pide nada.
        private bool ValidarPago(out MedioPago_GV42? medio, out string numeroOperacion, out DatosTarjeta_GV42 tarjeta)
        {
            medio = null;
            numeroOperacion = null;
            tarjeta = null;
            if (!HayQueCobrar) return true;

            medio = MedioSeleccionado;
            if (medio == null)
            {
                Tema_GV42.MostrarError(cmbMedioPago, IdiomaManager_GV42.T("pago.elegirMedio"), IdiomaManager_GV42.T("pago.faltaDato"));
                return false;
            }

            if (medio == MedioPago_GV42.Transferencia)
            {
                numeroOperacion = txtNumeroOperacion.Text.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(numeroOperacion, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                {
                    Tema_GV42.MostrarError(txtNumeroOperacion, IdiomaManager_GV42.T("pago.errOperacion"));
                    return false;
                }
            }
            else if (EsTarjeta(medio))
            {
                // Con tarjeta el número de transacción lo genera la BLL (marca, **** últimos 4 y autorización).
                if (!ValidarTarjeta(medio.Value, out tarjeta)) return false;
            }
            // En efectivo el número lo genera el sistema.
            return true;
        }

        #endregion

        #region Confirmación del cambio

        private void ConfirmarCambio()
        {
            if (_reserva == null || _vueloNuevo == null || _cotizacion == null) return;

            // Un asiento por cada pasajero que ocupa lugar (la BLL también lo exige).
            List<AsientoPasajero_GV42> asientos = null;
            if (_requiereAsientos)
            {
                List<Pasajero_GV42> conAsiento = PasajerosConAsiento;
                int sinAsiento = conAsiento.FindIndex(p => !_asientos.ContainsKey(p.DNI));
                if (sinAsiento >= 0)
                {
                    // Se deja a la vista el pasajero al que le falta elegir.
                    _indicePasajeroActivo = sinAsiento;
                    RefrescarButacas();
                    MessageBox.Show(IdiomaManager_GV42.T("reservar.elegiAsientos"), IdiomaManager_GV42.T("general.revisarDatos"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                asientos = conAsiento.Select(p => new AsientoPasajero_GV42(p.DNI, _asientos[p.DNI])).ToList();
            }

            MedioPago_GV42? medio;
            string numeroOperacion;
            DatosTarjeta_GV42 tarjeta;
            if (!ValidarPago(out medio, out numeroOperacion, out tarjeta)) return;

            string pregunta = IdiomaManager_GV42.T("cambio.confirmarPregunta",
                DescribirVuelo(VueloActual), DescribirVuelo(_vueloNuevo), TextoTotal());
            if (MessageBox.Show(pregunta, IdiomaManager_GV42.T("cambio.confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            CotizacionCambio_GV42 cobrado;
            try
            {
                cobrado = _bll.CambiarVuelo(_numeroReserva, _tramo, _vueloNuevo.Vuelo.Id, _vueloNuevo.Clase,
                                            asientos, medio, numeroOperacion, tarjeta);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RecargarMapa();
                return;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambio.accionCambiar"), ex);
                return;
            }

            LimpiarDatosDePago();
            decimal total = cobrado != null ? cobrado.Total : _cotizacion.Total;
            string mensaje = IdiomaManager_GV42.T("cambio.exito", DescribirVuelo(_vueloNuevo));
            if (total > 0) mensaje += "\n" + IdiomaManager_GV42.T("cambio.exitoCobro", total.ToString("C2"));
            MessageBox.Show(mensaje, IdiomaManager_GV42.T("general.exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            // DialogResult.OK le avisa a la consulta de reservas que tiene que refrescar la grilla.
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Eventos

        private void FRMCambiarVuelo_GV42_Shown(object sender, EventArgs e)
        {
            if (_errorCarga == null) return;
            if (_errorCarga is NegocioException_GV42)
                MessageBox.Show(_errorCarga.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("cambio.accionCargar"), _errorCarga);
        }

        private void rbTramo_CheckedChanged(object sender, EventArgs e)
        {
            // El evento llega dos veces (el que se destilda y el que se tilda): solo interesa el tildado.
            var rb = sender as RadioButton;
            if (rb == null || !rb.Checked) return;
            SeleccionarTramo(rb == rbVuelta ? Reserva_GV42.TRAMO_VUELTA : Reserva_GV42.TRAMO_IDA);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarVuelos();
        }

        private void dgvVuelos_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargandoGrilla) return;
            VueloClase_GV42 elegido = dgvVuelos.SelectedRows.Count > 0
                ? dgvVuelos.SelectedRows[0].DataBoundItem as VueloClase_GV42
                : null;
            if (ReferenceEquals(elegido, _vueloNuevo)) return;
            ElegirVuelo(elegido);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_vueloNuevo == null || _cotizacion == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("reservar.elegiVuelo"), IdiomaManager_GV42.T("general.revisarDatos"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (PrepararAsientos()) IrAPaso(PASO_ASIENTOS_Y_PAGO);
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            IrAPaso(PASO_VUELO);
        }

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
            List<Pasajero_GV42> conAsiento = PasajerosConAsiento;
            if (asiento == null || _indicePasajeroActivo >= conAsiento.Count) return;
            _asientos[conAsiento[_indicePasajeroActivo].DNI] = asiento;

            // Se pasa solo al próximo pasajero que todavía no eligió, para no tener que tocar "siguiente".
            int pendiente = conAsiento.FindIndex(p => !_asientos.ContainsKey(p.DNI));
            if (pendiente >= 0) _indicePasajeroActivo = pendiente;
            RefrescarButacas();
        }

        private void cmbMedioPago_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is MedioPago_GV42) e.Value = ((MedioPago_GV42)e.ListItem).Texto();
        }

        private void cmbMedioPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarSeccionMedio();
        }

        private void SoloDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtNumeroTarjeta_TextChanged(object sender, EventArgs e)
        {
            if (_formateandoNumero) return;
            FormatearNumeroTarjeta();
            AjustarLargoCodigo();
            ActualizarIndicadorTarjeta();
        }

        private void txtNumeroTarjeta_Leave(object sender, EventArgs e)
        {
            // Al salir del campo se informa la validez aunque tenga menos de 16 dígitos.
            ActualizarIndicadorTarjeta();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ConfirmarCambio();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
