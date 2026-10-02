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
    // RFN 1 - Registrar el pago de una reserva pendiente y mostrar los boletos generados.
    // Sirve tanto para el vendedor (cobra en el mostrador) como para el cliente autogestionado
    // (paga su propia reserva); BLLReserva_GV42 ya valida que la reserva exista y esté pendiente.
    // El diseño está en FRMPagoReserva_GV42.Designer.cs (Form Designer).
    //
    // Con tarjeta (crédito o débito) se piden los datos de la tarjeta y se prevalidan acá
    // (algoritmo de Luhn, titular, vencimiento y código de seguridad); la BLL vuelve a validar
    // todo y genera el número de transacción: marca, **** últimos 4 y código de autorización.
    public partial class FRMPagoReserva_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private Reserva_GV42 _reserva;

        // Resultado del último pago registrado (se vuelve a armar al cambiar el idioma).
        private string _transaccionGuardada;
        private List<Boleto_GV42> _boletosEmitidos;

        // Evita que el formateo del número de tarjeta (grupos de 4) se dispare a sí mismo.
        private bool _formateandoNumero;

        // Medios de pago que se ofrecen (sin efectivo si el usuario no es vendedor).
        private readonly List<MedioPago_GV42> _medios = new List<MedioPago_GV42>
        {
            MedioPago_GV42.TarjetaDebito, MedioPago_GV42.TarjetaCredito, MedioPago_GV42.Transferencia, MedioPago_GV42.Efectivo
        };

        // Valores que devuelve Validaciones_GV42.MarcaTarjeta.
        private const string MARCA_AMEX = "American Express";
        private const string MARCA_GENERICA = "Tarjeta";

        #endregion

        #region Constructor

        public FRMPagoReserva_GV42(string numeroReservaInicial = null)
        {
            InitializeComponent();

            // Solo un vendedor cobra en efectivo (el cliente autogestionado paga con tarjeta o transferencia).
            try
            {
                if (!_bll.PuedeRegistrarPagoDeTerceros())
                    _medios.Remove(MedioPago_GV42.Efectivo);
            }
            catch { }

            CargarMediosDePago();
            CargarAniosVencimiento();

            txtNumeroOperacion.MaxLength = Validaciones_GV42.MAX_NUMERO_TRANSACCION;
            // Dígitos más un espacio cada 4 (el número se muestra agrupado: "4509 9535 6623 3704").
            txtNumeroTarjeta.MaxLength = Validaciones_GV42.MAX_DIGITOS_TARJETA + (Validaciones_GV42.MAX_DIGITOS_TARJETA - 1) / 4;
            txtTitular.MaxLength = Validaciones_GV42.MAX_NOMBRE;
            btnConfirmarPago.Enabled = false;
            btnVerBoletos.Enabled = false;
            MostrarSeccionMedio();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            if (!string.IsNullOrWhiteSpace(numeroReservaInicial))
            {
                txtNumeroReserva.Text = numeroReservaInicial;
                btnBuscar_Click(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("pago.tituloVentana");
            lblTitulo.Text = IdiomaManager_GV42.T("pago.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("pago.subtitulo");

            lblSeccionReserva.Text = IdiomaManager_GV42.T("pago.seccionReserva");
            lblAyudaReserva.Text = IdiomaManager_GV42.T("pago.ayudaReserva");
            lblNumeroReserva.Text = IdiomaManager_GV42.T("pago.numeroReserva");
            btnBuscar.Text = IdiomaManager_GV42.T("pago.buscar");
            lblCliente.Text = IdiomaManager_GV42.T("pago.cliente");
            lblVuelo.Text = IdiomaManager_GV42.T("pago.vuelo");
            lblRuta.Text = IdiomaManager_GV42.T("pago.ruta");
            lblSalida.Text = IdiomaManager_GV42.T("pago.salida");
            lblClase.Text = IdiomaManager_GV42.T("pago.clase");
            lblPasajeros.Text = IdiomaManager_GV42.T("pago.pasajeros");
            lblEstado.Text = IdiomaManager_GV42.T("pago.estado");
            lblImporteTitulo.Text = IdiomaManager_GV42.T("pago.importe");

            lblSeccionPago.Text = IdiomaManager_GV42.T("pago.seccionPago");
            lblAyudaPago.Text = IdiomaManager_GV42.T("pago.ayudaPago");
            lblMedioPago.Text = IdiomaManager_GV42.T("pago.medioPago");
            lblNumeroTarjeta.Text = IdiomaManager_GV42.T("pago.numeroTarjeta");
            lblTitular.Text = IdiomaManager_GV42.T("pago.titular");
            lblVencimiento.Text = IdiomaManager_GV42.T("pago.vencimiento");
            lblCodigoSeguridad.Text = IdiomaManager_GV42.T("pago.codigoSeguridad");
            lblAyudaCodigo.Text = IdiomaManager_GV42.T("pago.ayudaCodigo");
            lblNumeroOperacion.Text = IdiomaManager_GV42.T("pago.numeroOperacion");
            lblAyudaOperacion.Text = IdiomaManager_GV42.T("pago.ayudaOperacion");
            lblEfectivo.Text = IdiomaManager_GV42.T("pago.efectivoAutomatico");
            btnConfirmarPago.Text = IdiomaManager_GV42.T("pago.confirmar");
            btnVerBoletos.Text = IdiomaManager_GV42.T("pago.verBoletos");

            ctrlVistaTarjeta.EtiquetaTitular = IdiomaManager_GV42.T("pago.vistaTitular");
            ctrlVistaTarjeta.EtiquetaVence = IdiomaManager_GV42.T("pago.vistaVence");
            ctrlVistaTarjeta.TextoTitularVacio = IdiomaManager_GV42.T("pago.vistaTitularVacio");
            ctrlVistaTarjeta.FormatoVencimiento = IdiomaManager_GV42.T("pago.vistaFormatoVencimiento");

            // Datos armados en código: se regeneran para que cambien de idioma en caliente.
            CargarMediosDePago();
            MostrarDetalle();
            MostrarResultadoPago();
            ActualizarIndicadorTarjeta();
            ActualizarVistaPrevia();
        }

        #endregion

        #region Carga de datos

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

        // Años de vencimiento: desde el actual hasta el máximo que acepta la validación (+15).
        private void CargarAniosVencimiento()
        {
            int actual = DateTime.Today.Year;
            cmbAnio.Items.Clear();
            for (int a = actual; a <= actual + Validaciones_GV42.MAX_ANIOS_VENCIMIENTO; a++)
                cmbAnio.Items.Add(a.ToString());
        }

        // Detalle de la reserva buscada (se regenera al cambiar el idioma: clase y estado se traducen).
        private void MostrarDetalle()
        {
            if (_reserva == null)
            {
                foreach (Label l in new[] { lblClienteValor, lblVueloValor, lblRutaValor, lblSalidaValor,
                                            lblClaseValor, lblPasajerosValor, lblEstadoValor, lblImporteValor })
                    l.Text = "—";
                lblEstadoValor.ForeColor = Tema_GV42.Texto;
                return;
            }

            lblClienteValor.Text = _reserva.Cliente.NombreCompleto;
            lblVueloValor.Text = _reserva.VueloClase.CodigoVuelo;
            lblRutaValor.Text = _reserva.VueloClase.OrigenDescripcion + " -> " + _reserva.VueloClase.DestinoDescripcion;
            lblSalidaValor.Text = _reserva.VueloClase.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm");
            lblClaseValor.Text = _reserva.VueloClase.ClaseTexto;
            // Ida y vuelta: se paga la reserva completa (los dos vuelos).
            if (_reserva.TieneVuelta)
            {
                VueloClase_GV42 vuelta = _reserva.VueloClaseVuelta;
                lblVueloValor.Text += " / " + vuelta.CodigoVuelo;
                lblRutaValor.Text = _reserva.VueloClase.Vuelo.Origen.CodigoIata + " -> " + _reserva.VueloClase.Vuelo.Destino.CodigoIata
                                  + " -> " + vuelta.Vuelo.Destino.CodigoIata + "  (" + _reserva.TipoViaje.Texto() + ")";
                lblSalidaValor.Text = _reserva.VueloClase.FechaHoraSalida.ToString("dd/MM HH:mm") + "  ·  "
                                    + IdiomaManager_GV42.T("pago.regreso", vuelta.FechaHoraSalida.ToString("dd/MM HH:mm"));
                if (vuelta.Clase != _reserva.VueloClase.Clase) lblClaseValor.Text += " / " + vuelta.ClaseTexto;
            }
            lblPasajerosValor.Text = _reserva.CantidadPasajeros.ToString();
            lblEstadoValor.Text = _reserva.EstadoTexto;
            lblEstadoValor.ForeColor = _reserva.Estado == EstadoReserva_GV42.Confirmada ? Tema_GV42.Exito
                                     : _reserva.Estado == EstadoReserva_GV42.Cancelada ? Tema_GV42.Error
                                     : Tema_GV42.Advertencia;
            lblImporteValor.Text = _reserva.ImporteTotal.ToString("C2");
        }

        // Número de transacción guardado (la BLL ya lo guarda enmascarado) y boletos emitidos.
        private void MostrarResultadoPago()
        {
            lblTransaccionGuardada.Text = string.IsNullOrEmpty(_transaccionGuardada)
                ? string.Empty
                : "✓ " + IdiomaManager_GV42.T("pago.transaccionGuardada", _transaccionGuardada);

            if (_boletosEmitidos == null)
            {
                lblBoletos.Text = string.Empty;
                return;
            }
            lblBoletos.Text = IdiomaManager_GV42.T("pago.boletosEmitidos") + "\n" +
                string.Join("\n", _boletosEmitidos.Select(b => "  " + b.NumeroBoleto + " — " + b.PasajeroNombre));
        }

        #endregion

        #region Tarjeta

        private MedioPago_GV42? MedioSeleccionado =>
            cmbMedioPago.SelectedItem is MedioPago_GV42 ? (MedioPago_GV42?)(MedioPago_GV42)cmbMedioPago.SelectedItem : null;

        private static bool EsTarjeta(MedioPago_GV42? medio) =>
            medio == MedioPago_GV42.TarjetaCredito || medio == MedioPago_GV42.TarjetaDebito;

        private string DigitosTarjeta => Validaciones_GV42.SoloDigitos(txtNumeroTarjeta.Text);

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

        // Vista previa de la tarjeta: número enmascarado, titular, vencimiento y marca.
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
            ctrlVistaTarjeta.Vencimiento = cmbMes.SelectedItem == null && cmbAnio.SelectedItem == null
                ? string.Empty
                : mes + "/" + anio;
        }

        // Después de pagar no queda ningún dato de la tarjeta en pantalla.
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

        #endregion

        #region Eventos

        private void txtNumeroReserva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar_Click(sender, e);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Se limpia el estado antes de buscar: si la búsqueda falla, no puede quedar habilitado
            // el pago de la reserva anterior mientras la caja muestra otro número.
            _reserva = null;
            _transaccionGuardada = null;
            _boletosEmitidos = null;
            MostrarDetalle();
            MostrarResultadoPago();
            btnConfirmarPago.Enabled = false;
            btnVerBoletos.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtNumeroReserva.Text))
            {
                Tema_GV42.MostrarError(txtNumeroReserva, IdiomaManager_GV42.T("pago.errNumeroReserva"));
                return;
            }

            try
            {
                _reserva = _bll.BuscarReserva(txtNumeroReserva.Text.Trim());
                if (_reserva == null)
                {
                    MessageBox.Show(IdiomaManager_GV42.T("pago.noExiste"), IdiomaManager_GV42.T("pago.noEncontrada"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MostrarDetalle();
                bool yaSalio = _reserva.VueloClase.FechaHoraSalida <= DateTime.Now;
                btnConfirmarPago.Enabled = _reserva.Estado == EstadoReserva_GV42.PendienteDePago && !yaSalio;
                btnVerBoletos.Enabled = _reserva.Estado == EstadoReserva_GV42.Confirmada;

                if (_reserva.Estado != EstadoReserva_GV42.PendienteDePago)
                    MessageBox.Show(IdiomaManager_GV42.T("pago.noPendiente", _reserva.EstadoTexto),
                        IdiomaManager_GV42.T("pago.sinAccion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (yaSalio)
                    MessageBox.Show(IdiomaManager_GV42.T("pago.yaSalio"),
                        IdiomaManager_GV42.T("pago.sinAccion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                _reserva = null;
                MostrarDetalle();
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                _reserva = null;
                MostrarDetalle();
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("pago.accionBuscar"), ex);
            }
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
            ActualizarVistaPrevia();
        }

        private void txtNumeroTarjeta_Leave(object sender, EventArgs e)
        {
            // Al salir del campo se informa la validez aunque tenga menos de 16 dígitos.
            ActualizarIndicadorTarjeta();
        }

        private void DatosTarjeta_Changed(object sender, EventArgs e)
        {
            ActualizarVistaPrevia();
        }

        private void btnVerBoletos_Click(object sender, EventArgs e)
        {
            if (_reserva != null) FRMBoletos_GV42.Mostrar(this, _reserva.NumeroReserva);
        }

        private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("pago.buscarPrimero"), IdiomaManager_GV42.T("pago.faltaReserva"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MedioSeleccionado == null)
            {
                Tema_GV42.MostrarError(cmbMedioPago, IdiomaManager_GV42.T("pago.elegirMedio"), IdiomaManager_GV42.T("pago.faltaDato"));
                return;
            }

            MedioPago_GV42 medio = MedioSeleccionado.Value;
            string tx = null;
            DatosTarjeta_GV42 tarjeta = null;

            if (medio == MedioPago_GV42.Transferencia)
            {
                tx = txtNumeroOperacion.Text.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(tx, Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
                {
                    Tema_GV42.MostrarError(txtNumeroOperacion, IdiomaManager_GV42.T("pago.errOperacion"));
                    return;
                }
            }
            else if (EsTarjeta(medio))
            {
                // Con tarjeta el número de transacción lo genera la BLL (marca, **** últimos 4 y autorización).
                if (!ValidarTarjeta(medio, out tarjeta)) return;
            }
            // En efectivo el número lo genera el sistema.

            Pago_GV42 registrado;
            try
            {
                // El importe es el total de la reserva: se usa el valor numérico directamente
                // en vez de volver a parsear un texto (dependía de la cultura).
                registrado = _bll.RegistrarPago(_reserva.NumeroReserva, medio, _reserva.ImporteTotal, tx, tarjeta);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("pago.noSePudoRegistrar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("pago.accionRegistrar"), ex);
                return;
            }

            // El pago ya quedó registrado: lo que sigue no puede decir "no se pudo registrar el pago".
            btnConfirmarPago.Enabled = false;
            btnVerBoletos.Enabled = true;
            _transaccionGuardada = registrado != null ? registrado.NumeroTransaccion : null;
            LimpiarDatosDePago();

            // Se vuelve a leer la reserva para mostrar el estado nuevo (Confirmada); si falla, no importa.
            try
            {
                Reserva_GV42 actualizada = _bll.BuscarReserva(_reserva.NumeroReserva);
                if (actualizada != null) _reserva = actualizada;
            }
            catch { }
            MostrarDetalle();
            MostrarResultadoPago();

            try
            {
                _boletosEmitidos = _bll.ObtenerBoletos(_reserva.NumeroReserva);
                MostrarResultadoPago();

                if (MessageBox.Show(IdiomaManager_GV42.T("pago.registradoVerBoletos"), IdiomaManager_GV42.T("pago.listo"),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    FRMBoletos_GV42.Mostrar(this, _reserva.NumeroReserva);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("pago.registradoSinBoletos") + "\n" + ex.Message,
                                IdiomaManager_GV42.T("pago.registradoTitulo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
