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
    // RFN 2 - Check-in. Un solo formulario (asistente de 5 pasos) para los dos canales:
    //  - Encargado de Check-in en el mostrador (patente CheckIn.Realizar): busca la reserva, elige al
    //    pasajero, verifica los datos, despacha el equipaje (cobrando el exceso), valida o cambia el
    //    asiento y confirma. También puede despachar después el equipaje de quien hizo el check-in online.
    //  - Cliente autogestionado (patente CheckIn.RealizarPropio): lo mismo para los pasajeros de sus
    //    reservas, pero sin despachar valijas (el paso de equipaje es solo un aviso).
    // Toda la lógica de negocio está en BLLCheckIn_GV42 (vuelve a validar permisos, ventana y estado).
    //
    // El diseño está en FRMCheckIn_GV42.Designer.cs (Form Designer): cada paso es una tarjeta
    // (PanelTarjeta_GV42) con Dock Fill dentro de pnlContenido y solo se muestra la del paso actual.
    // Lo único que se arma en código es lo que depende de los datos (filas de la grilla, textos del
    // resumen y el mapa de butacas de CtrlButacas_GV42).
    public partial class FRMCheckIn_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private const int PASO_BUSCAR = 0;
        private const int PASO_VERIFICAR = 1;
        private const int PASO_EQUIPAJE = 2;
        private const int PASO_ASIENTO = 3;
        private const int PASO_CONFIRMAR = 4;
        private const int PASO_RESULTADO = 5;

        // Valores que devuelve Validaciones_GV42.MarcaTarjeta.
        private const string MARCA_AMEX = "American Express";
        private const string MARCA_GENERICA = "Tarjeta";

        private readonly BLLCheckIn_GV42 _bll = new BLLCheckIn_GV42();

        // Modo según las patentes del usuario (no según el nombre del rol).
        private bool _esMostrador;
        private bool _esOnline;

        // Asistente: tarjetas de cada paso (la última es el resultado), su chip y su clave de texto.
        private List<Control> _pasos;
        private List<Label> _chips;
        private readonly string[] _clavesPaso = { "buscar", "verificar", "equipaje", "asiento", "confirmar" };
        private int _pasoActual;

        // Reserva con la que se abrió el formulario (desde "Mis reservas").
        private readonly string _numeroReservaInicial;

        // Paso 1: pasajeros de la reserva buscada.
        private List<CheckIn_GV42> _pasajeros;
        private List<FilaPasajero> _filas = new List<FilaPasajero>();

        // Check-in del pasajero que se está atendiendo.
        private CheckIn_GV42 _ci;

        // true: el pasajero ya hizo el check-in online y solo se despacha su equipaje en el mostrador.
        private bool _despachoPosterior;

        // Paso 3: franquicia y cálculo del exceso en vivo.
        private FranquiciaEquipaje_GV42 _franquicia;
        private CargoExcesoEquipaje_GV42 _cargo;
        private string _errorCalculo;

        // Paso 4: mapa de butacas (solo se muestra si el pasajero quiere cambiar el asiento).
        private List<AsientoDisponibilidad_GV42> _mapa;
        private bool _mapaVisible;

        // Resultado: check-in confirmado (con la tarjeta de embarque emitida).
        private CheckIn_GV42 _ciConfirmado;

        // Evita que el formateo del número de tarjeta (grupos de 4) se dispare a sí mismo.
        private bool _formateandoNumero;

        // Mientras se re-arman combos o valores en código no se recalcula el exceso.
        private bool _refrescando;

        // Medios con los que se cobra el exceso de equipaje en el mostrador.
        private readonly MedioPago_GV42[] _medios =
        {
            MedioPago_GV42.Efectivo, MedioPago_GV42.TarjetaDebito, MedioPago_GV42.TarjetaCredito, MedioPago_GV42.Transferencia
        };

        #endregion

        #region Constructor

        public FRMCheckIn_GV42(string numeroReserva = null)
        {
            InitializeComponent();

            _numeroReservaInicial = numeroReserva;
            _esMostrador = _bll.PuedeAtenderMostrador();
            _esOnline = _bll.PuedeHacerCheckInOnline();

            _pasos = new List<Control> { pnlPasoBuscar, pnlPasoVerificar, pnlPasoEquipaje, pnlPasoAsiento, pnlPasoConfirmar, pnlPasoResultado };
            _chips = new List<Label> { lblChipBuscar, lblChipVerificar, lblChipEquipaje, lblChipAsiento, lblChipConfirmar };
            ConfigurarControles();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            IrAPaso(PASO_BUSCAR);
        }

        // Estado inicial que depende de constantes de otras clases (no se puede fijar en el diseñador).
        private void ConfigurarControles()
        {
            dgvPasajeros.AutoGenerateColumns = false;

            // El máximo real (valijas permitidas) se fija al conocer la franquicia del pasajero.
            numBultos.Maximum = 0;

            txtNumeroOperacion.MaxLength = Validaciones_GV42.MAX_NUMERO_TRANSACCION;
            // Dígitos más un espacio cada 4 (el número se muestra agrupado: "4509 9535 6623 3704").
            txtNumeroTarjeta.MaxLength = Validaciones_GV42.MAX_DIGITOS_TARJETA + (Validaciones_GV42.MAX_DIGITOS_TARJETA - 1) / 4;
            txtTitular.MaxLength = Validaciones_GV42.MAX_NOMBRE;

            int anio = DateTime.Today.Year;
            for (int a = anio; a <= anio + Validaciones_GV42.MAX_ANIOS_VENCIMIENTO; a++)
                cmbAnio.Items.Add(a.ToString());
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            _refrescando = true;
            try
            {
                Text = IdiomaManager_GV42.T("checkin.tituloVentana");
                lblTitulo.Text = IdiomaManager_GV42.T("checkin.titulo");

                btnAtras.Text = IdiomaManager_GV42.T("checkin.atras");

                // Paso 1: buscar
                lblTituloBuscar.Text = IdiomaManager_GV42.T("checkin.buscar.titulo");
                lblAyudaBuscar.Text = IdiomaManager_GV42.T(_esMostrador ? "checkin.buscar.ayudaMostrador" : "checkin.buscar.ayudaOnline");
                lblNumeroReserva.Text = IdiomaManager_GV42.T("checkin.numeroReserva");
                lblDni.Text = IdiomaManager_GV42.T("checkin.dniOpcional");
                btnBuscar.Text = IdiomaManager_GV42.T("checkin.buscar");
                lblAyudaPasajero.Text = IdiomaManager_GV42.T("checkin.ayudaPasajero");
                btnDespachar.Text = IdiomaManager_GV42.T("checkin.despacharEquipaje");
                btnVerTarjeta.Text = IdiomaManager_GV42.T("checkin.verTarjeta");
                colDni.HeaderText = IdiomaManager_GV42.T("checkin.col.dni");
                colPasajero.HeaderText = IdiomaManager_GV42.T("checkin.col.pasajero");
                colTramo.HeaderText = IdiomaManager_GV42.T("checkin.col.tramo");
                colAsiento.HeaderText = IdiomaManager_GV42.T("checkin.col.asiento");
                colEstado.HeaderText = IdiomaManager_GV42.T("checkin.col.estado");
                colCanal.HeaderText = IdiomaManager_GV42.T("checkin.col.canal");
                colSituacion.HeaderText = IdiomaManager_GV42.T("checkin.col.situacion");
                MostrarPasajeros();

                // Paso 2: verificar
                lblTituloVerificar.Text = IdiomaManager_GV42.T("checkin.verificar.titulo");
                lblSecPasajero.Text = IdiomaManager_GV42.T("checkin.sec.pasajero");
                lblNombre.Text = IdiomaManager_GV42.T("checkin.nombre");
                lblDniVer.Text = IdiomaManager_GV42.T("checkin.dni");
                lblEmail.Text = IdiomaManager_GV42.T("checkin.email");
                lblSecReserva.Text = IdiomaManager_GV42.T("checkin.sec.reserva");
                lblReserva.Text = IdiomaManager_GV42.T("checkin.numeroReserva");
                lblEstadoReserva.Text = IdiomaManager_GV42.T("checkin.estadoReserva");
                lblEstadoCheckIn.Text = IdiomaManager_GV42.T("checkin.estadoCheckIn");
                lblTipoViaje.Text = IdiomaManager_GV42.T("checkin.tipoViaje");
                lblSecVuelo.Text = IdiomaManager_GV42.T("checkin.sec.vuelo");
                lblVuelo.Text = IdiomaManager_GV42.T("checkin.vuelo");
                lblRuta.Text = IdiomaManager_GV42.T("checkin.ruta");
                lblSalida.Text = IdiomaManager_GV42.T("checkin.salida");
                lblLlegada.Text = IdiomaManager_GV42.T("checkin.llegada");
                lblClase.Text = IdiomaManager_GV42.T("checkin.clase");
                lblPuerta.Text = IdiomaManager_GV42.T("checkin.puerta");
                lblSecServicios.Text = IdiomaManager_GV42.T("checkin.sec.servicios");
                MostrarVerificacion();

                // Paso 3: equipaje
                lblTituloEquipaje.Text = IdiomaManager_GV42.T(_despachoPosterior ? "checkin.equipaje.tituloPosterior" : "checkin.equipaje.titulo");
                lblAyudaEquipaje.Text = IdiomaManager_GV42.T(_esMostrador ? "checkin.equipaje.ayuda" : "checkin.equipaje.ayudaOnline");
                lblBultos.Text = IdiomaManager_GV42.T("checkin.bultos");
                lblPeso.Text = IdiomaManager_GV42.T("checkin.peso");
                TraducirValijas();
                lblSecFranquicia.Text = IdiomaManager_GV42.T("checkin.sec.franquicia");
                lblFranqClase.Text = IdiomaManager_GV42.T("checkin.franqClase");
                lblFranqExtra.Text = IdiomaManager_GV42.T("checkin.franqExtra");
                lblFranqMaxima.Text = IdiomaManager_GV42.T("checkin.franqMaxima");
                lblSecCalculo.Text = IdiomaManager_GV42.T("checkin.sec.calculo");
                lblCalcFranquicia.Text = IdiomaManager_GV42.T("checkin.calcFranquicia");
                lblCalcExtra.Text = IdiomaManager_GV42.T("checkin.calcExtra");
                lblCalcExceso.Text = IdiomaManager_GV42.T("checkin.calcExceso");
                lblCalcCosto.Text = IdiomaManager_GV42.T("checkin.calcCosto");
                lblCalcImporte.Text = IdiomaManager_GV42.T("checkin.calcImporte");
                // Datos de la tarjeta y transferencia: mismos textos que "Registrar pago".
                lblMedioCobro.Text = IdiomaManager_GV42.T("pago.medioPago");
                lblNumeroTarjeta.Text = IdiomaManager_GV42.T("pago.numeroTarjeta");
                lblTitular.Text = IdiomaManager_GV42.T("pago.titular");
                lblVencimiento.Text = IdiomaManager_GV42.T("pago.vencimiento");
                lblCodigoSeguridad.Text = IdiomaManager_GV42.T("pago.codigoSeguridad");
                lblAyudaCodigo.Text = IdiomaManager_GV42.T("pago.ayudaCodigo");
                lblNumeroOperacion.Text = IdiomaManager_GV42.T("pago.numeroOperacion");
                lblAyudaOperacion.Text = IdiomaManager_GV42.T("pago.ayudaOperacion");
                lblEfectivo.Text = IdiomaManager_GV42.T("checkin.efectivoAutomatico");
                ctrlVistaTarjeta.EtiquetaTitular = IdiomaManager_GV42.T("pago.vistaTitular");
                ctrlVistaTarjeta.EtiquetaVence = IdiomaManager_GV42.T("pago.vistaVence");
                ctrlVistaTarjeta.TextoTitularVacio = IdiomaManager_GV42.T("pago.vistaTitularVacio");
                ctrlVistaTarjeta.FormatoVencimiento = IdiomaManager_GV42.T("pago.vistaFormatoVencimiento");
                CargarMediosDePago();
                MostrarAvisoMostrador();
                MostrarFranquicia();
                MostrarCargo();
                ActualizarIndicadorTarjeta();

                // Paso 4: asiento
                lblTituloAsiento.Text = IdiomaManager_GV42.T("checkin.asiento.titulo");
                lblAyudaAsiento.Text = IdiomaManager_GV42.T("checkin.asiento.ayuda");
                lblAsientoNumero.Text = IdiomaManager_GV42.T("checkin.asiento");
                lblAsientoClase.Text = IdiomaManager_GV42.T("checkin.clase");
                lblAsientoUbicacion.Text = IdiomaManager_GV42.T("checkin.ubicacion");
                lblAsientoPreferencial.Text = IdiomaManager_GV42.T("checkin.preferencial");
                ctrlButacas.ActualizarIdioma();
                MostrarAsientoActual();

                // Paso 5: confirmar
                lblTituloConfirmar.Text = IdiomaManager_GV42.T("checkin.confirmar.titulo");
                lblNotaConfirmar.Text = IdiomaManager_GV42.T("checkin.confirmar.nota");
                lblSecConfPasajero.Text = IdiomaManager_GV42.T("checkin.sec.pasajero");
                lblSecConfVuelo.Text = IdiomaManager_GV42.T("checkin.sec.vueloAsiento");
                lblSecConfEquipaje.Text = IdiomaManager_GV42.T("checkin.sec.equipaje");
                ArmarResumen();

                // Resultado
                lblResultadoTitulo.Text = IdiomaManager_GV42.T("checkin.resultado.titulo");
                lblResTarjeta.Text = IdiomaManager_GV42.T("checkin.resultado.tarjeta");
                lblResAsiento.Text = IdiomaManager_GV42.T("checkin.asiento");
                lblResPuerta.Text = IdiomaManager_GV42.T("checkin.puerta");
                lblResEmbarque.Text = IdiomaManager_GV42.T("checkin.resultado.embarque");
                btnOtroPasajero.Text = IdiomaManager_GV42.T("checkin.otroPasajero");
                btnVerTarjetaResultado.Text = IdiomaManager_GV42.T("checkin.verTarjeta");
                MostrarResultado();

                // Encabezado, indicador y botonera según el paso actual.
                if (_pasos != null) MostrarPasoActual();
            }
            finally
            {
                _refrescando = false;
            }
        }

        #endregion

        #region Navegación del asistente

        private void IrAPaso(int indice)
        {
            if (indice < 0 || indice >= _pasos.Count) return;
            _pasoActual = indice;

            if (indice == PASO_VERIFICAR) MostrarVerificacion();
            else if (indice == PASO_EQUIPAJE) PrepararPasoEquipaje();
            else if (indice == PASO_ASIENTO) PrepararPasoAsiento();
            else if (indice == PASO_CONFIRMAR) ArmarResumen();
            else if (indice == PASO_RESULTADO) MostrarResultado();

            MostrarPasoActual();
        }

        // Muestra solo la tarjeta del paso actual y actualiza encabezado, indicador y botonera.
        private void MostrarPasoActual()
        {
            for (int i = 0; i < _pasos.Count; i++)
                _pasos[i].Visible = i == _pasoActual;

            if (_pasoActual == PASO_RESULTADO)
                lblSubtitulo.Text = IdiomaManager_GV42.T("checkin.resultadoSubtitulo");
            else if (_despachoPosterior)
                lblSubtitulo.Text = IdiomaManager_GV42.T("checkin.subtituloDespacho");
            else
                lblSubtitulo.Text = IdiomaManager_GV42.T("checkin.pasoDe", _pasoActual + 1, _chips.Count,
                                                         IdiomaManager_GV42.T("checkin.paso." + _clavesPaso[_pasoActual]));
            ActualizarIndicador();

            btnAtras.Visible = _pasoActual > PASO_BUSCAR && _pasoActual < PASO_RESULTADO;
            btnSiguiente.Visible = _pasoActual < PASO_RESULTADO;
            ActualizarTextoSiguiente();
        }

        private void ActualizarTextoSiguiente()
        {
            string clave = "checkin.siguiente";
            if (_pasoActual == PASO_CONFIRMAR) clave = "checkin.confirmar";
            else if (_pasoActual == PASO_EQUIPAJE && _esMostrador && _ci != null && _ci.Equipaje == null && numBultos.Value > 0)
                clave = _despachoPosterior ? "checkin.despachar" : "checkin.despacharSeguir";
            btnSiguiente.Text = IdiomaManager_GV42.T(clave);
        }

        // Chips: hecho (celeste con tilde), actual (azul) y pendiente (gris claro). En el despacho
        // posterior solo se ven "Reserva" y "Equipaje".
        private void ActualizarIndicador()
        {
            int numero = 0;
            for (int i = 0; i < _chips.Count; i++)
            {
                Label chip = _chips[i];
                chip.Visible = !_despachoPosterior || i == PASO_BUSCAR || i == PASO_EQUIPAJE;
                if (!chip.Visible) continue;
                numero++;

                string nombre = IdiomaManager_GV42.T("checkin.chip." + _clavesPaso[i]);
                if (i < _pasoActual)
                {
                    chip.Text = "✓  " + nombre;
                    chip.BackColor = Tema_GV42.BordeGrilla;
                    chip.ForeColor = Tema_GV42.Acento;
                }
                else if (i == _pasoActual)
                {
                    chip.Text = numero + "  " + nombre;
                    chip.BackColor = Tema_GV42.Primario;
                    chip.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    chip.Text = numero + "  " + nombre;
                    chip.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
                    chip.ForeColor = Tema_GV42.TextoSecundario;
                }
            }
        }

        // Al empezar con otro pasajero se descarta todo lo del anterior.
        private void ReiniciarPasajero()
        {
            _franquicia = null;
            _cargo = null;
            _errorCalculo = null;
            _mapa = null;
            _mapaVisible = false;
            _ciConfirmado = null;

            _refrescando = true;
            try
            {
                numBultos.Value = 0;
                flpPesos.Controls.Clear();
                cmbMedioCobro.SelectedIndex = -1;
            }
            finally { _refrescando = false; }
            LimpiarDatosDePago();
        }

        #endregion

        #region Búsqueda de reserva y pasajero (paso 1)

        // Lista los pasajeros de la reserva con el estado de su check-in. Si se ingresó un DNI,
        // queda seleccionado ese pasajero.
        private void BuscarPasajeros(bool avisarDniInexistente)
        {
            _pasajeros = null;
            MostrarPasajeros();

            string numero = txtNumeroReserva.Text.Trim();
            string dni = txtDni.Text.Trim();
            if (numero.Length == 0)
            {
                Tema_GV42.MostrarError(txtNumeroReserva, IdiomaManager_GV42.T("checkin.errNumeroReserva"));
                return;
            }
            if (dni.Length > 0 && !Validaciones_GV42.EsDniValido(dni))
            {
                Tema_GV42.MostrarError(txtDni, Validaciones_GV42.MENSAJE_DNI);
                return;
            }

            try
            {
                _pasajeros = _bll.ListarPasajeros(numero);
                MostrarPasajeros();

                if (dni.Length > 0 && !SeleccionarPorDni(dni) && avisarDniInexistente)
                    MessageBox.Show(IdiomaManager_GV42.T("checkin.dniNoEnReserva", dni, numero.ToUpper()),
                                    IdiomaManager_GV42.T("checkin.pasajeroNoEncontrado"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                _pasajeros = null;
                MostrarPasajeros();
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                _pasajeros = null;
                MostrarPasajeros();
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionBuscar"), ex);
            }
        }

        // Arma las filas de la grilla (se regeneran al cambiar el idioma) conservando la selección.
        private void MostrarPasajeros()
        {
            CheckIn_GV42 anterior = PasajeroSeleccionado();

            _filas = (_pasajeros ?? new List<CheckIn_GV42>()).Select(CrearFila).ToList();
            dgvPasajeros.DataSource = null;
            dgvPasajeros.DataSource = _filas;
            dgvPasajeros.ClearSelection();
            // Ida y vuelta: cada pasajero aparece una vez por tramo (cada vuelo tiene su check-in).
            colTramo.Visible = _filas.Any(f => f.CheckIn.ReservaConVuelta);

            // Que el usuario elija al pasajero (salvo que hubiera uno elegido antes o haya uno solo).
            if (anterior != null) SeleccionarPorDni(anterior.Pasajero.DNI, anterior.Tramo);
            else if (_filas.Count == 1) dgvPasajeros.Rows[0].Selected = true;
            ActualizarBotonesPasajero();
        }

        private FilaPasajero CrearFila(CheckIn_GV42 ci)
        {
            var fila = new FilaPasajero
            {
                Dni = ci.Pasajero != null ? ci.Pasajero.DNI : string.Empty,
                Pasajero = ci.Pasajero != null ? ci.Pasajero.NombreCompleto : string.Empty,
                Tramo = IdiomaManager_GV42.T(ci.Tramo == Reserva_GV42.TRAMO_VUELTA ? "tramo.vuelta" : "tramo.ida")
                        + (ci.Vuelo != null ? " · " + ci.Vuelo.CodigoVuelo : string.Empty),
                Asiento = ci.Asiento != null ? ci.Asiento.NumeroAsiento : "—",
                Estado = ci.EstadoTexto,
                Canal = ci.Estado == EstadoCheckIn_GV42.Realizado ? ci.CanalTexto : "—",
                CheckIn = ci
            };

            if (ci.Estado == EstadoCheckIn_GV42.Realizado)
            {
                // Solo el encargado del mostrador puede despachar después el equipaje del check-in online.
                fila.PuedeDespachar = _esMostrador && _bll.PuedeDespacharPosterior(ci);
                fila.Situacion = IdiomaManager_GV42.T(fila.PuedeDespachar ? "checkin.sit.realizadoSinEquipaje" : "checkin.sit.realizado");
                fila.Tipo = TipoSituacion.Realizado;
            }
            else
            {
                string motivo = _bll.MotivoNoDisponible(ci);
                fila.Situacion = motivo ?? IdiomaManager_GV42.T("checkin.sit.disponible");
                fila.Tipo = motivo == null ? TipoSituacion.Disponible : TipoSituacion.NoDisponible;
            }
            return fila;
        }

        // tramo 0 = cualquiera: en ida y vuelta se prefiere el tramo que tiene el check-in disponible.
        private bool SeleccionarPorDni(string dni, int tramo = 0)
        {
            if (tramo == 0)
            {
                FilaPasajero preferida = _filas.FirstOrDefault(f => f.Dni == dni && f.Tipo == TipoSituacion.Disponible)
                                         ?? _filas.FirstOrDefault(f => f.Dni == dni);
                if (preferida == null) return false;
                tramo = preferida.CheckIn.Tramo;
            }

            foreach (DataGridViewRow r in dgvPasajeros.Rows)
            {
                var fila = (FilaPasajero)r.DataBoundItem;
                if (fila.Dni == dni && fila.CheckIn.Tramo == tramo)
                {
                    r.Selected = true;
                    dgvPasajeros.CurrentCell = r.Cells[0];
                    return true;
                }
            }
            return false;
        }

        private FilaPasajero FilaSeleccionada()
        {
            if (dgvPasajeros.SelectedRows.Count == 0) return null;
            return dgvPasajeros.SelectedRows[0].DataBoundItem as FilaPasajero;
        }

        private CheckIn_GV42 PasajeroSeleccionado()
        {
            FilaPasajero f = FilaSeleccionada();
            return f != null ? f.CheckIn : null;
        }

        // "Ver tarjeta de embarque" solo con el check-in hecho; "Despachar equipaje" solo en el mostrador
        // y para quien hizo el check-in online y todavía no despachó.
        private void ActualizarBotonesPasajero()
        {
            FilaPasajero f = FilaSeleccionada();
            btnVerTarjeta.Enabled = f != null && f.Tipo == TipoSituacion.Realizado;
            btnDespachar.Visible = _esMostrador;
            btnDespachar.Enabled = f != null && f.PuedeDespachar;
        }

        private void ValidarYAvanzarBuscar()
        {
            CheckIn_GV42 sel = PasajeroSeleccionado();
            if (sel == null)
                throw new NegocioException_GV42(IdiomaManager_GV42.T(_pasajeros == null ? "checkin.buscarPrimero" : "checkin.elegiPasajero"));

            // La BLL valida permisos, estado de la reserva, check-in ya hecho y ventana horaria.
            CheckIn_GV42 ci = _bll.IniciarCheckIn(sel.NumeroReserva, sel.Pasajero.DNI, sel.Tramo);
            _ci = ci;
            _despachoPosterior = false;
            ReiniciarPasajero();
            IrAPaso(PASO_VERIFICAR);
        }

        #endregion

        #region Verificación de datos (paso 2)

        private void MostrarVerificacion()
        {
            Label[] valores = { lblNombreValor, lblDniVerValor, lblEmailValor, lblReservaValor, lblEstadoReservaValor, lblEstadoCheckInValor,
                                lblTipoViajeValor, lblVueloValor, lblRutaValor, lblSalidaValor, lblLlegadaValor, lblClaseValor, lblPuertaValor };
            if (_ci == null)
            {
                foreach (Label l in valores) l.Text = "—";
                lblServiciosValor.Text = "—";
                lblVentana.Text = string.Empty;
                return;
            }

            Vuelo_GV42 v = _ci.Vuelo;
            lblNombreValor.Text = _ci.Pasajero.NombreCompleto;
            lblDniVerValor.Text = _ci.Pasajero.DNI;
            lblEmailValor.Text = string.IsNullOrWhiteSpace(_ci.Pasajero.Email) ? "—" : _ci.Pasajero.Email;
            lblReservaValor.Text = _ci.NumeroReserva;
            lblEstadoReservaValor.Text = _ci.EstadoReservaTexto;
            lblEstadoReservaValor.ForeColor = _ci.EstadoReserva == EstadoReserva_GV42.Confirmada ? Tema_GV42.Exito : Tema_GV42.Error;
            lblEstadoCheckInValor.Text = _ci.EstadoTexto;
            lblTipoViajeValor.Text = _ci.TipoViaje.Texto() + (_ci.ReservaConVuelta
                ? " · " + IdiomaManager_GV42.T(_ci.Tramo == Reserva_GV42.TRAMO_VUELTA ? "tramo.vuelta" : "tramo.ida")
                : string.Empty);

            lblVueloValor.Text = v.CodigoVuelo + (v.Aerolinea != null ? " · " + v.Aerolinea.Nombre : string.Empty);
            lblRutaValor.Text = _ci.VueloClase.OrigenDescripcion + " -> " + _ci.VueloClase.DestinoDescripcion;
            lblSalidaValor.Text = v.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm");
            lblLlegadaValor.Text = v.FechaHoraLlegada.ToString("dd/MM/yyyy HH:mm");
            lblClaseValor.Text = _ci.VueloClase.ClaseTexto;
            lblPuertaValor.Text = string.IsNullOrWhiteSpace(v.PuertaEmbarque) ? "—" : v.PuertaEmbarque;
            lblServiciosValor.Text = TextoServicios();

            // Ventana de check-in: de 48 hs a 60 minutos antes de la salida.
            DateTime apertura = v.FechaHoraSalida.AddHours(-BLLCheckIn_GV42.HORAS_APERTURA_CHECKIN);
            DateTime cierre = v.FechaHoraSalida.AddMinutes(-BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN);
            bool abierta = DateTime.Now >= apertura && DateTime.Now <= cierre;
            lblVentana.Text = (abierta ? "✓ " : "✗ ") + IdiomaManager_GV42.T(abierta ? "checkin.ventanaAbierta" : "checkin.ventanaCerrada",
                apertura.ToString("dd/MM/yyyy HH:mm"), cierre.ToString("dd/MM/yyyy HH:mm"));
            lblVentana.ForeColor = abierta ? Tema_GV42.Exito : Tema_GV42.Error;
            pnlVentana.BackColor = abierta ? Tema_GV42.Fondo : Tema_GV42.FondoError;
        }

        // Servicios adicionales de la reserva (nombres del catálogo traducidos).
        private string TextoServicios()
        {
            var servicios = (_ci.ServiciosAdicionales ?? new List<AdicionalReserva_GV42>()).Where(a => a.Cantidad > 0).ToList();
            if (servicios.Count == 0) return IdiomaManager_GV42.T("checkin.sinServicios");
            return string.Join(Environment.NewLine, servicios.Select(a =>
                "• " + IdiomaManager_GV42.TConDefecto("adicional." + a.TipoNombre, a.TipoNombre) + "  x" + a.Cantidad));
        }

        #endregion

        #region Equipaje (paso 3)

        private void PrepararPasoEquipaje()
        {
            lblTituloEquipaje.Text = IdiomaManager_GV42.T(_despachoPosterior ? "checkin.equipaje.tituloPosterior" : "checkin.equipaje.titulo");

            // Cliente online: no despacha valijas, solo se le avisa que las entrega en el mostrador.
            tlpEquipaje.Visible = _esMostrador;
            pnlAvisoMostrador.Visible = !_esMostrador;

            if (_ci != null && _franquicia == null)
            {
                try { _franquicia = _bll.ObtenerFranquicia(_ci.Id); }
                catch (NegocioException_GV42 ex)
                {
                    if (_esMostrador)
                        MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    if (_esMostrador) Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionFranquicia"), ex);
                }
            }

            // No se pueden despachar más valijas que las incluidas en la clase más el equipaje extra comprado.
            _refrescando = true;
            try
            {
                int permitidas = _franquicia != null ? _franquicia.BultosPermitidos : 0;
                if (numBultos.Value > permitidas) numBultos.Value = permitidas;
                numBultos.Maximum = permitidas;
                SincronizarValijas();
            }
            finally { _refrescando = false; }

            MostrarAvisoMostrador();
            MostrarFranquicia();
            if (_esMostrador) Recalcular();
        }

        private static string Kg(decimal kilos)
        {
            return kilos.ToString("0.##") + " kg";
        }

        private void MostrarAvisoMostrador()
        {
            string texto = IdiomaManager_GV42.T("checkin.avisoMostrador", BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN);
            if (_franquicia != null)
                texto += Environment.NewLine + Environment.NewLine + IdiomaManager_GV42.T("checkin.avisoFranquicia",
                    _franquicia.BultosPermitidos, _franquicia.BultosIncluidos, Kg(_franquicia.KgPorBultoIncluido),
                    _franquicia.UnidadesExtraAplicables, Kg(_franquicia.KgPorUnidadExtra), Kg(_franquicia.PesoMaximoPorBulto));
            lblAvisoMostrador.Text = texto;
        }

        // Desglose de la franquicia: la de la clase, el equipaje extra comprado y el máximo posible.
        private void MostrarFranquicia()
        {
            FranquiciaEquipaje_GV42 f = _franquicia;
            if (f == null)
            {
                lblFranqClaseValor.Text = lblFranqExtraValor.Text = lblFranqMaximaValor.Text = "—";
                lblBultosPermitidos.Text = string.Empty;
                return;
            }
            lblFranqClaseValor.Text = IdiomaManager_GV42.T("checkin.unidadesExtra", f.BultosIncluidos, Kg(f.KgPorBultoIncluido));
            lblFranqExtraValor.Text = IdiomaManager_GV42.T("checkin.unidadesExtra", f.UnidadesExtraAplicables, Kg(f.KgPorUnidadExtra));
            lblFranqMaximaValor.Text = IdiomaManager_GV42.T("checkin.valijasPermitidasValor", f.BultosPermitidos, Kg(f.PesoMaximoPorBulto));
            lblBultosPermitidos.Text = IdiomaManager_GV42.T("checkin.dePermitidas", f.BultosPermitidos);
        }

        // Cálculo del exceso en vivo (la BLL no guarda nada hasta despachar).
        private void Recalcular()
        {
            if (_refrescando || _ci == null || !_esMostrador) return;
            if (_ci.Equipaje != null) { MostrarCargo(); return; }

            _cargo = null;
            _errorCalculo = null;
            List<decimal> pesos = PesosIngresados();
            if (pesos.Count > 0 && pesos.All(p => p > 0))
            {
                try
                {
                    _cargo = _bll.CalcularCargoExceso(_ci.Id, pesos);
                }
                catch (NegocioException_GV42 ex)
                {
                    _errorCalculo = ex.Message;
                }
                catch (Exception ex)
                {
                    _errorCalculo = IdiomaManager_GV42.T("general.noSePudo", IdiomaManager_GV42.T("checkin.accionCalcular")) +
                                    " " + (ex.InnerException ?? ex).Message;
                }
            }
            MostrarCargo();
        }

        // Pinta el cálculo y decide qué se ve en la columna de cobro: el medio de pago (si hay exceso),
        // el aviso "sin cargo" o el equipaje ya registrado.
        private void MostrarCargo()
        {
            ActualizarTextoSiguiente();

            Equipaje_GV42 registrado = _ci != null ? _ci.Equipaje : null;
            bool hayRegistro = registrado != null;
            numBultos.Enabled = !hayRegistro;
            flpPesos.Enabled = !hayRegistro;
            lblEquipajeRegistrado.Visible = hayRegistro;
            lblSeccionCobro.Text = IdiomaManager_GV42.T(hayRegistro ? "checkin.sec.despachado" : "checkin.sec.cobro");

            CargoExcesoEquipaje_GV42 c = hayRegistro ? registrado.CargoExceso : _cargo;
            if (hayRegistro)
            {
                lblCalcFranquiciaValor.Text = Kg(registrado.FranquiciaKg);
                lblCalcExtraValor.Text = TextoUnidadesUsadas(registrado.UnidadesExtra);
                lblCalcExcesoValor.Text = Kg(c != null ? c.KilosExceso : 0m);
                lblCalcCostoValor.Text = c != null ? c.CostoPorKilo.ToString("C2") : "—";
                lblCalcImporteValor.Text = (c != null ? c.ImporteCargo : 0m).ToString("C2");
                lblCalcImporteValor.ForeColor = c != null && c.TieneExceso ? Tema_GV42.Advertencia : Tema_GV42.Exito;
                lblEquipajeRegistrado.Text = TextoEquipajeRegistrado(registrado);
            }
            else if (c != null)
            {
                lblCalcFranquiciaValor.Text = Kg(c.FranquiciaKg);
                lblCalcExtraValor.Text = TextoUnidadesUsadas(c.UnidadesExtraUsadas);
                lblCalcExcesoValor.Text = Kg(c.KilosExceso);
                lblCalcCostoValor.Text = c.CostoPorKilo.ToString("C2");
                lblCalcImporteValor.Text = c.ImporteCargo.ToString("C2");
                lblCalcImporteValor.ForeColor = c.TieneExceso ? Tema_GV42.Advertencia : Tema_GV42.Exito;
            }
            else
            {
                foreach (Label l in new[] { lblCalcFranquiciaValor, lblCalcExtraValor, lblCalcExcesoValor, lblCalcCostoValor, lblCalcImporteValor })
                    l.Text = "—";
                lblCalcImporteValor.ForeColor = Tema_GV42.Acento;
            }

            bool cobrar = !hayRegistro && c != null && c.TieneExceso;
            lblMedioCobro.Visible = cobrar;
            cmbMedioCobro.Visible = cobrar;
            lblSinExceso.Visible = !hayRegistro && !cobrar;
            if (!hayRegistro && !cobrar)
            {
                if (_errorCalculo != null)
                {
                    lblSinExceso.Text = "✗ " + _errorCalculo;
                    lblSinExceso.ForeColor = Tema_GV42.Error;
                }
                else if (c != null)
                {
                    lblSinExceso.Text = "✓ " + IdiomaManager_GV42.T("checkin.sinExceso");
                    lblSinExceso.ForeColor = Tema_GV42.Exito;
                }
                else
                {
                    lblSinExceso.Text = IdiomaManager_GV42.T("checkin.ingresePeso");
                    lblSinExceso.ForeColor = Tema_GV42.TextoSecundario;
                }
            }
            MostrarSeccionMedio(cobrar);
        }

        private string TextoUnidadesUsadas(int unidades)
        {
            return unidades <= 0
                ? IdiomaManager_GV42.T("checkin.ninguno")
                : IdiomaManager_GV42.T("checkin.unidadesUsadas", unidades, Kg(BLLCheckIn_GV42.KG_POR_EQUIPAJE_EXTRA));
        }

        private string TextoEquipajeRegistrado(Equipaje_GV42 eq)
        {
            var sb = new StringBuilder();
            sb.AppendLine("✓ " + IdiomaManager_GV42.T("checkin.equipajeRegistrado", eq.CantidadBultos, Kg(eq.PesoTotalKg)));
            sb.AppendLine();
            sb.AppendLine(IdiomaManager_GV42.T("checkin.etiquetas"));
            foreach (string etiqueta in eq.Etiquetas ?? new List<string>())
                sb.AppendLine("   " + etiqueta);
            if (eq.CargoExceso != null && eq.CargoExceso.TieneExceso)
            {
                sb.AppendLine();
                sb.AppendLine(IdiomaManager_GV42.T("checkin.cargoCobrado", eq.CargoExceso.ImporteCargo.ToString("C2"),
                    eq.CargoExceso.MedioPago.HasValue ? eq.CargoExceso.MedioPago.Value.Texto() : "—",
                    eq.CargoExceso.NumeroTransaccion ?? "—"));
            }
            return sb.ToString();
        }

        // Despacha el equipaje (y cobra el exceso, si lo hay) o, si no hay bultos, sigue sin despachar.
        // Devuelve true si se puede pasar al paso siguiente.
        private bool DespacharEquipaje()
        {
            if (!_esMostrador || _ci.Equipaje != null) return true;

            int bultos = (int)numBultos.Value;
            List<decimal> pesos = PesosIngresados();
            decimal peso = pesos.Sum();
            if (bultos == 0)
            {
                if (_despachoPosterior)
                {
                    Tema_GV42.MostrarError(numBultos, IdiomaManager_GV42.T("checkin.errBultos"));
                    return false;
                }
                return true;   // el pasajero no despacha equipaje
            }
            // Cada valija con su peso y ninguna por encima del máximo (la BLL lo vuelve a controlar).
            var valijas = flpPesos.Controls.OfType<CtrlPesoValija_GV42>().ToList();
            for (int i = 0; i < valijas.Count; i++)
            {
                if (valijas[i].Peso <= 0)
                {
                    Tema_GV42.MostrarError(valijas[i].CampoPeso, IdiomaManager_GV42.T("checkin.errPeso", i + 1));
                    return false;
                }
                if (_franquicia != null && valijas[i].Peso > _franquicia.PesoMaximoPorBulto)
                {
                    Tema_GV42.MostrarError(valijas[i].CampoPeso, IdiomaManager_GV42.T("neg.checkin.pesoBultoMaximo",
                        i + 1, valijas[i].Peso.ToString("0.##"), _franquicia.PesoMaximoPorBulto.ToString("0.##")));
                    return false;
                }
            }

            CargoExcesoEquipaje_GV42 cargo;
            try
            {
                cargo = _bll.CalcularCargoExceso(_ci.Id, pesos);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _cargo = cargo;
            MostrarCargo();

            MedioPago_GV42? medio = null;
            string numeroOperacion = null;
            DatosTarjeta_GV42 tarjeta = null;
            if (cargo.TieneExceso)
            {
                medio = MedioSeleccionado;
                if (medio == null)
                {
                    Tema_GV42.MostrarError(cmbMedioCobro, IdiomaManager_GV42.T("checkin.elegirMedio"), IdiomaManager_GV42.T("pago.faltaDato"));
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
            }

            string mensaje = IdiomaManager_GV42.T("checkin.confirmarDespacho", bultos, Kg(peso), _ci.Pasajero.NombreCompleto);
            if (cargo.TieneExceso)
                mensaje += Environment.NewLine + Environment.NewLine + IdiomaManager_GV42.T("checkin.confirmarCobro",
                    Kg(cargo.KilosExceso), cargo.ImporteCargo.ToString("C2"), medio.Value.Texto());
            if (MessageBox.Show(mensaje, IdiomaManager_GV42.T("checkin.confirmarDespachoTitulo"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;

            Equipaje_GV42 registrado;
            try
            {
                registrado = _bll.RegistrarEquipaje(_ci.Id, pesos, medio, numeroOperacion, tarjeta);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("checkin.noSePudoDespachar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Recalcular();   // por ejemplo, otro pasajero usó el equipaje extra mientras tanto
                return false;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionDespachar"), ex);
                return false;
            }

            // Ya quedó registrado: no queda ningún dato de la tarjeta en pantalla.
            _ci.Equipaje = registrado;
            LimpiarDatosDePago();
            MostrarCargo();
            return true;
        }

        // Una caja de peso por valija: la cantidad sigue al valor de "Valijas a despachar".
        private void SincronizarValijas()
        {
            int cantidad = (int)numBultos.Value;
            while (flpPesos.Controls.Count > cantidad)
            {
                Control ultimo = flpPesos.Controls[flpPesos.Controls.Count - 1];
                flpPesos.Controls.Remove(ultimo);
                ultimo.Dispose();
            }
            while (flpPesos.Controls.Count < cantidad)
            {
                var valija = new CtrlPesoValija_GV42();
                valija.PesoCambiado += (s, e) => Recalcular();
                flpPesos.Controls.Add(valija);
            }
            TraducirValijas();
        }

        private void TraducirValijas()
        {
            int n = 1;
            foreach (CtrlPesoValija_GV42 v in flpPesos.Controls.OfType<CtrlPesoValija_GV42>())
                v.Titulo = IdiomaManager_GV42.T("checkin.valijaN", n++);
        }

        private List<decimal> PesosIngresados()
        {
            return flpPesos.Controls.OfType<CtrlPesoValija_GV42>().Select(v => v.Peso).ToList();
        }

        // Despacho posterior terminado: se muestran las etiquetas y se vuelve a la lista de pasajeros.
        private void TerminarDespachoPosterior()
        {
            try { _ci = _bll.Obtener(_ci.Id); } catch { }
            MessageBox.Show(IdiomaManager_GV42.T("checkin.despachoListo"), IdiomaManager_GV42.T("checkin.listo"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            FRMTarjetaEmbarque_GV42.Mostrar(this, _ci);
            VolverALaLista();
        }

        #endregion

        #region Cobro del exceso (tarjeta, transferencia o efectivo)

        // Vuelve a cargar los medios conservando el elegido (el texto lo pone cmbMedioCobro_Format).
        private void CargarMediosDePago()
        {
            object seleccionado = cmbMedioCobro.SelectedItem;
            cmbMedioCobro.BeginUpdate();
            cmbMedioCobro.Items.Clear();
            foreach (MedioPago_GV42 m in _medios) cmbMedioCobro.Items.Add(m);
            cmbMedioCobro.EndUpdate();
            if (seleccionado != null && cmbMedioCobro.Items.Contains(seleccionado))
                cmbMedioCobro.SelectedItem = seleccionado;
        }

        private MedioPago_GV42? MedioSeleccionado =>
            cmbMedioCobro.SelectedItem is MedioPago_GV42 ? (MedioPago_GV42?)(MedioPago_GV42)cmbMedioCobro.SelectedItem : null;

        private static bool EsTarjeta(MedioPago_GV42? medio) =>
            medio == MedioPago_GV42.TarjetaCredito || medio == MedioPago_GV42.TarjetaDebito;

        private string DigitosTarjeta => Validaciones_GV42.SoloDigitos(txtNumeroTarjeta.Text);

        private static string MarcaParaMostrar(string marca) =>
            marca == MARCA_GENERICA ? IdiomaManager_GV42.T("pago.marcaGenerica") : marca;

        // Muestra solo la sección del medio elegido (y nada si no hay exceso que cobrar).
        private void MostrarSeccionMedio(bool cobrar)
        {
            MedioPago_GV42? medio = MedioSeleccionado;
            pnlSeccionTarjeta.Visible = cobrar && EsTarjeta(medio);
            pnlSeccionTransferencia.Visible = cobrar && medio == MedioPago_GV42.Transferencia;
            pnlSeccionEfectivo.Visible = cobrar && medio == MedioPago_GV42.Efectivo;
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

        // Indicador en vivo debajo del número: marca y validez (Luhn).
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

        private void ActualizarVistaPrevia()
        {
            string d = DigitosTarjeta;
            string marca = d.Length >= 2 ? Validaciones_GV42.MarcaTarjeta(d) : MARCA_GENERICA;
            ctrlVistaTarjeta.Numero = d;
            ctrlVistaTarjeta.EsAmex = marca == MARCA_AMEX;
            ctrlVistaTarjeta.Marca = marca == MARCA_GENERICA ? string.Empty : marca;
            ctrlVistaTarjeta.Titular = Validaciones_GV42.NormalizarEspacios(txtTitular.Text);

            string mes = cmbMes.SelectedItem != null ? cmbMes.SelectedItem.ToString() : "--";
            string anio = cmbAnio.SelectedItem != null ? cmbAnio.SelectedItem.ToString().Substring(2) : "--";
            ctrlVistaTarjeta.Vencimiento = cmbMes.SelectedItem == null && cmbAnio.SelectedItem == null ? string.Empty : mes + "/" + anio;
        }

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
            ActualizarVistaPrevia();
        }

        // Prevalidación campo por campo, marcando el campo exacto (la BLL vuelve a validar todo).
        private bool ValidarTarjeta(MedioPago_GV42 medio, out DatosTarjeta_GV42 tarjeta)
        {
            tarjeta = null;
            string numero = DigitosTarjeta;

            if (!Validaciones_GV42.EsNumeroTarjetaValido(txtNumeroTarjeta.Text))
            { Tema_GV42.MostrarError(txtNumeroTarjeta, Validaciones_GV42.MENSAJE_TARJETA); return false; }

            bool amex = Validaciones_GV42.EsAmex(numero);
            if (medio == MedioPago_GV42.TarjetaDebito && amex)
            { Tema_GV42.MostrarError(cmbMedioCobro, IdiomaManager_GV42.T("pago.errAmexDebito")); return false; }

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

        #endregion

        #region Asiento (paso 4: validar o cambiar)

        private void PrepararPasoAsiento()
        {
            MostrarAsientoActual();
            if (_mapaVisible) CargarMapa();
        }

        // Asiento elegido al reservar: número, clase, ubicación y si es preferencial.
        private void MostrarAsientoActual()
        {
            Asiento_GV42 a = _ci != null ? _ci.Asiento : null;
            lblAsientoNumeroValor.Text = a != null ? a.NumeroAsiento : "—";
            lblAsientoClaseValor.Text = a != null ? a.ClaseTexto : (_ci != null ? _ci.VueloClase.ClaseTexto : "—");
            lblAsientoUbicacionValor.Text = a != null ? TextoUbicacion(a.Ubicacion) : "—";
            lblAsientoPreferencialValor.Text = a == null ? "—" : IdiomaManager_GV42.T(a.EsPreferencial ? "general.si" : "general.no");

            bool puedePreferencial = _ci != null && _bll.PuedeElegirPreferencial(_ci);
            lblAvisoPreferencial.Text = IdiomaManager_GV42.T(puedePreferencial ? "checkin.prefPermitido" : "checkin.prefNoPermitido");

            lblAsientoConforme.Text = IdiomaManager_GV42.T(a == null ? "checkin.sinAsiento" : "checkin.asientoConforme");
            lblAsientoConforme.Visible = !_mapaVisible;
            ctrlButacas.Visible = _mapaVisible;
            btnCambiarAsiento.Text = IdiomaManager_GV42.T(_mapaVisible ? "checkin.ocultarMapa" : "checkin.cambiarAsiento");
        }

        // La ubicación viene de la base en español (Ventana / Central / Pasillo).
        private static string TextoUbicacion(string ubicacion)
        {
            if (string.IsNullOrWhiteSpace(ubicacion)) return "—";
            return IdiomaManager_GV42.TConDefecto("checkin.ubicacion." + ubicacion.Trim(), ubicacion);
        }

        private void CargarMapa()
        {
            try
            {
                _mapa = _bll.ObtenerMapaAsientos(_ci.Id);
                PintarMapa();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionMapa"), ex);
            }
        }

        // El asiento actual figura ocupado en el mapa: se pasa como selección para que se vea "Tu selección".
        // Las preferenciales solo se pueden elegir si el pasajero ya pagó una al reservar.
        private void PintarMapa()
        {
            ctrlButacas.PermitirPreferenciales = _bll.PuedeElegirPreferencial(_ci);
            ctrlButacas.RecargoPreferencial = 0m;
            ctrlButacas.CargarMapa(_mapa, null, _ci.Asiento != null ? (int?)_ci.Asiento.Id : null);
        }

        private void ValidarYAvanzarAsiento()
        {
            // Confirma el asiento elegido (si el pasajero no tenía, la BLL le asigna uno libre).
            _ci.Asiento = _bll.ValidarAsiento(_ci.Id);
            IrAPaso(PASO_CONFIRMAR);
        }

        #endregion

        #region Confirmación y resultado (paso 5)

        private void ArmarResumen()
        {
            if (_ci == null)
            {
                lblConfPasajero.Text = lblConfVuelo.Text = lblConfEquipaje.Text = "-";
                return;
            }

            Vuelo_GV42 v = _ci.Vuelo;
            var pasajero = new StringBuilder();
            pasajero.AppendLine(_ci.Pasajero.NombreCompleto);
            pasajero.AppendLine(IdiomaManager_GV42.T("checkin.res.dni", _ci.Pasajero.DNI));
            pasajero.AppendLine(IdiomaManager_GV42.T("checkin.res.reserva", _ci.NumeroReserva));
            lblConfPasajero.Text = pasajero.ToString();

            var vuelo = new StringBuilder();
            vuelo.AppendLine(v.CodigoVuelo + (v.Aerolinea != null ? " · " + v.Aerolinea.Nombre : string.Empty));
            vuelo.AppendLine(_ci.VueloClase.OrigenDescripcion + " -> " + _ci.VueloClase.DestinoDescripcion);
            vuelo.AppendLine();
            vuelo.AppendLine(IdiomaManager_GV42.T("checkin.res.salida", v.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm")));
            vuelo.AppendLine(IdiomaManager_GV42.T("checkin.res.clase", _ci.VueloClase.ClaseTexto));
            vuelo.AppendLine(IdiomaManager_GV42.T("checkin.res.asiento", _ci.Asiento != null
                ? _ci.Asiento.NumeroAsiento + " (" + TextoUbicacion(_ci.Asiento.Ubicacion) + ")" : "—"));
            vuelo.AppendLine(IdiomaManager_GV42.T("checkin.res.puerta", string.IsNullOrWhiteSpace(v.PuertaEmbarque) ? "—" : v.PuertaEmbarque));
            vuelo.AppendLine(IdiomaManager_GV42.T("checkin.res.embarque",
                v.FechaHoraSalida.AddMinutes(-BLLCheckIn_GV42.MINUTOS_LIMITE_EMBARQUE).ToString("HH:mm")));
            lblConfVuelo.Text = vuelo.ToString();

            if (!_esMostrador)
                lblConfEquipaje.Text = IdiomaManager_GV42.T("checkin.res.equipajeMostrador");
            else if (_ci.Equipaje == null)
                lblConfEquipaje.Text = IdiomaManager_GV42.T("checkin.res.sinEquipaje");
            else
                lblConfEquipaje.Text = TextoEquipajeRegistrado(_ci.Equipaje).Replace("✓ ", string.Empty);
        }

        private void ConfirmarCheckIn()
        {
            CheckIn_GV42 confirmado = _bll.ConfirmarCheckIn(_ci.Id);
            _ciConfirmado = confirmado;
            _ci = confirmado;
            IrAPaso(PASO_RESULTADO);

            // La tarjeta de embarque (y las etiquetas, si despachó equipaje) se muestra enseguida.
            FRMTarjetaEmbarque_GV42.Mostrar(this, confirmado);
        }

        private void MostrarResultado()
        {
            CheckIn_GV42 ci = _ciConfirmado;
            if (ci == null)
            {
                lblResultadoDetalle.Text = string.Empty;
                lblResTarjetaValor.Text = lblResAsientoValor.Text = lblResPuertaValor.Text = lblResEmbarqueValor.Text = "-";
                return;
            }

            TarjetaEmbarque_GV42 t = ci.TarjetaEmbarque;
            lblResultadoDetalle.Text = IdiomaManager_GV42.T("checkin.resultado.detalle", ci.Pasajero.NombreCompleto, ci.Vuelo.CodigoVuelo);
            lblResTarjetaValor.Text = t != null ? t.NumeroTarjeta : "-";
            lblResAsientoValor.Text = t != null ? t.NumeroAsiento : (ci.Asiento != null ? ci.Asiento.NumeroAsiento : "-");
            lblResPuertaValor.Text = t != null && !string.IsNullOrWhiteSpace(t.PuertaEmbarque) ? t.PuertaEmbarque : "-";
            lblResEmbarqueValor.Text = t != null ? t.HoraLimiteEmbarque.ToString("dd/MM HH:mm") : "-";
        }

        // Vuelve al paso 1 con la misma reserva (la lista se actualiza con los estados nuevos).
        private void VolverALaLista()
        {
            _ci = null;
            _despachoPosterior = false;
            ReiniciarPasajero();
            IrAPaso(PASO_BUSCAR);
            if (txtNumeroReserva.Text.Trim().Length > 0) BuscarPasajeros(false);
        }

        #endregion

        #region Eventos

        private void FRMCheckIn_GV42_Load(object sender, EventArgs e)
        {
            if (!_esMostrador && !_esOnline)
            {
                btnBuscar.Enabled = false;
                btnSiguiente.Enabled = false;
                // Se avisa cuando el formulario ya está dibujado.
                BeginInvoke((Action)(() => MessageBox.Show(IdiomaManager_GV42.T("neg.checkin.sinPermiso"),
                    IdiomaManager_GV42.T("general.accesoDenegado"), MessageBoxButtons.OK, MessageBoxIcon.Warning)));
                return;
            }

            // Viene de "Mis reservas" con la reserva elegida.
            if (!string.IsNullOrWhiteSpace(_numeroReservaInicial))
            {
                txtNumeroReserva.Text = _numeroReservaInicial;
                BuscarPasajeros(false);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            // En el despacho posterior el único paso es el de equipaje: Atrás vuelve a la lista.
            if (_despachoPosterior || _pasoActual == PASO_VERIFICAR) { VolverALaLista(); return; }
            IrAPaso(_pasoActual - 1);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pasoActual == PASO_BUSCAR) ValidarYAvanzarBuscar();
                else if (_pasoActual == PASO_VERIFICAR) IrAPaso(PASO_EQUIPAJE);
                else if (_pasoActual == PASO_EQUIPAJE)
                {
                    if (!DespacharEquipaje()) return;
                    if (_despachoPosterior) TerminarDespachoPosterior();
                    else IrAPaso(PASO_ASIENTO);
                }
                else if (_pasoActual == PASO_ASIENTO) ValidarYAvanzarAsiento();
                else if (_pasoActual == PASO_CONFIRMAR) ConfirmarCheckIn();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionContinuar"), ex);
            }
        }

        // ---- Paso 1

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarPasajeros(true);
            }
        }

        // Si se cambia el número después de buscar, la lista deja de corresponder: se limpia.
        private void txtNumeroReserva_TextChanged(object sender, EventArgs e)
        {
            if (_pasajeros == null) return;
            _pasajeros = null;
            MostrarPasajeros();
        }

        private void SoloDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPasajeros(true);
        }

        private void dgvPasajeros_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonesPasajero();
        }

        private void dgvPasajeros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnSiguiente_Click(sender, e);
        }

        // Situación pintada con la paleta: disponible (verde), realizado (azul) y no disponible (rojo).
        private void dgvPasajeros_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != colSituacion.Index && e.ColumnIndex != colEstado.Index)) return;
            var fila = dgvPasajeros.Rows[e.RowIndex].DataBoundItem as FilaPasajero;
            if (fila == null) return;
            e.CellStyle.ForeColor = fila.Tipo == TipoSituacion.Disponible ? Tema_GV42.Exito
                                  : fila.Tipo == TipoSituacion.Realizado ? Tema_GV42.Primario
                                  : Tema_GV42.Error;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        // Reimpresión de la tarjeta de embarque de un check-in ya hecho.
        private void btnVerTarjeta_Click(object sender, EventArgs e)
        {
            CheckIn_GV42 sel = PasajeroSeleccionado();
            if (sel == null) return;
            try
            {
                CheckIn_GV42 realizado = _bll.BuscarRealizado(sel.NumeroReserva, sel.Pasajero.DNI, sel.Tramo);
                FRMTarjetaEmbarque_GV42.Mostrar(this, realizado);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionTarjeta"), ex);
            }
        }

        // Despacho posterior: el pasajero hizo el check-in online y entrega las valijas en el mostrador.
        private void btnDespachar_Click(object sender, EventArgs e)
        {
            FilaPasajero fila = FilaSeleccionada();
            if (fila == null || !fila.PuedeDespachar) return;
            try
            {
                _ci = _bll.Obtener(fila.CheckIn.Id);
                _despachoPosterior = true;
                ReiniciarPasajero();
                IrAPaso(PASO_EQUIPAJE);
            }
            catch (NegocioException_GV42 ex)
            {
                _despachoPosterior = false;
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                _despachoPosterior = false;
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionContinuar"), ex);
            }
        }

        // ---- Paso 3

        private void datosEquipaje_ValueChanged(object sender, EventArgs e)
        {
            if (sender == numBultos && !_refrescando) SincronizarValijas();
            Recalcular();
        }

        private void cmbMedioCobro_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is MedioPago_GV42) e.Value = ((MedioPago_GV42)e.ListItem).Texto();
        }

        private void cmbMedioCobro_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarSeccionMedio(cmbMedioCobro.Visible);
        }

        private void txtNumeroTarjeta_TextChanged(object sender, EventArgs e)
        {
            if (_formateandoNumero) return;
            FormatearNumeroTarjeta();
            AjustarLargoCodigo();
            ActualizarIndicadorTarjeta();
            ActualizarVistaPrevia();
        }

        private void txtNumeroTarjeta_Leave(object sender, EventArgs e)
        {
            ActualizarIndicadorTarjeta();
        }

        private void DatosTarjeta_Changed(object sender, EventArgs e)
        {
            ActualizarVistaPrevia();
        }

        // ---- Paso 4

        private void btnCambiarAsiento_Click(object sender, EventArgs e)
        {
            _mapaVisible = !_mapaVisible;
            MostrarAsientoActual();
            if (_mapaVisible) CargarMapa();
        }

        // El cambio se guarda en el momento (la BLL valida clase, ocupación y butaca preferencial).
        private void ctrlButacas_AsientoClickeado(object sender, Asiento_GV42 asiento)
        {
            if (asiento == null || (_ci.Asiento != null && _ci.Asiento.Id == asiento.Id)) return;
            try
            {
                _ci.Asiento = _bll.CambiarAsiento(_ci.Id, asiento.Id);
                MostrarAsientoActual();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("checkin.asientoNoDisponible"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("checkin.accionCambiarAsiento"), ex);
            }
            CargarMapa();   // el asiento anterior queda libre y se ven las ocupaciones al día
        }

        // ---- Resultado

        private void btnVerTarjetaResultado_Click(object sender, EventArgs e)
        {
            if (_ciConfirmado != null) FRMTarjetaEmbarque_GV42.Mostrar(this, _ciConfirmado);
        }

        private void btnOtroPasajero_Click(object sender, EventArgs e)
        {
            txtDni.Clear();
            VolverALaLista();
        }

        #endregion

        #region Tipos anidados

        private enum TipoSituacion { Disponible, NoDisponible, Realizado }

        // Fila de la grilla de pasajeros: los textos ya vienen armados (y traducidos) para mostrar.
        private class FilaPasajero
        {
            public string Dni { get; set; }
            public string Pasajero { get; set; }
            public string Tramo { get; set; }
            public string Asiento { get; set; }
            public string Estado { get; set; }
            public string Canal { get; set; }
            public string Situacion { get; set; }
            public TipoSituacion Tipo { get; set; }
            public bool PuedeDespachar { get; set; }
            public CheckIn_GV42 CheckIn { get; set; }
        }

        #endregion
    }
}
