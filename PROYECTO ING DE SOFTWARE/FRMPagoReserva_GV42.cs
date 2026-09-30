using BE;
using BLL;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // RFN 1 - Registrar el pago de una reserva pendiente y mostrar los boletos generados.
    // Sirve tanto para el vendedor (cobra en el mostrador) como para el cliente autogestionado
    // (paga su propia reserva); BLLReserva_GV42 ya valida que la reserva exista y esté pendiente.
    public class FRMPagoReserva_GV42 : Form
    {
        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private Reserva_GV42 _reserva;

        private TextBox txtNumeroReserva;
        private Button btnBuscar;
        private Label lblDetalle;
        private ComboBox cmbMedioPago;
        private TextBox txtImporte, txtNumeroTransaccion;
        private Button btnConfirmarPago;
        private Button btnVerBoletos;
        private Label lblBoletos;

        public FRMPagoReserva_GV42(string numeroReservaInicial = null)
        {
            ConstruirUI();

            // Solo un vendedor cobra en efectivo (el cliente autogestionado paga con tarjeta o transferencia).
            try
            {
                if (!_bll.PuedeRegistrarPagoDeTerceros())
                    cmbMedioPago.Items.Remove(MedioPago_GV42.Efectivo);
            }
            catch { }
            txtNumeroReserva.MaxLength = 20;
            txtNumeroReserva.CharacterCasing = CharacterCasing.Upper;
            txtNumeroTransaccion.MaxLength = Servicios.Validaciones_GV42.MAX_NUMERO_TRANSACCION;
            btnConfirmarPago.Enabled = false;
            txtNumeroReserva.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnBuscar_Click(s, e); } };
            if (!string.IsNullOrWhiteSpace(numeroReservaInicial))
            {
                txtNumeroReserva.Text = numeroReservaInicial;
                btnBuscar_Click(this, EventArgs.Empty);
            }
        }

        private void ConstruirUI()
        {
            Text = "Registrar pago";
            BackColor = Tema_GV42.Fondo;
            ClientSize = new Size(560, 620);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 9F);

            var card = Tema_GV42.CrearCard();
            card.Location = new Point(30, 20);
            card.Size = new Size(500, 580);
            card.Padding = new Padding(20);
            Controls.Add(card);

            var lblTitulo = new Label { Text = "Registrar pago", Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento, AutoSize = true, Location = new Point(20, 15) };

            var lblNumero = Tema_GV42.CrearLabel("Número de reserva"); lblNumero.Location = new Point(20, 60);
            txtNumeroReserva = Tema_GV42.CrearTextBox(); txtNumeroReserva.Location = new Point(20, 80); txtNumeroReserva.Size = new Size(220, 24);
            btnBuscar = new Button { Text = "Buscar", Location = new Point(250, 79), Size = new Size(100, 26) };
            Tema_GV42.EstilizarBotonSecundario(btnBuscar);
            btnBuscar.Click += btnBuscar_Click;

            lblDetalle = new Label { Location = new Point(20, 120), Size = new Size(460, 160), Font = Tema_GV42.FuenteTexto, ForeColor = Tema_GV42.Texto };

            var lblMedio = Tema_GV42.CrearLabel("Medio de pago"); lblMedio.Location = new Point(20, 290);
            cmbMedioPago = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(20, 310), Size = new Size(220, 24) };
            cmbMedioPago.Items.AddRange(new object[] { MedioPago_GV42.TarjetaDebito, MedioPago_GV42.TarjetaCredito, MedioPago_GV42.Transferencia, MedioPago_GV42.Efectivo });
            cmbMedioPago.SelectedIndexChanged += (s, e) =>
            {
                bool efectivo = MedioPago_GV42.Efectivo.Equals(cmbMedioPago.SelectedItem);
                txtNumeroTransaccion.Enabled = !efectivo;
                if (efectivo) txtNumeroTransaccion.Clear();   // en efectivo el número lo genera el sistema
            };

            var lblImporte = Tema_GV42.CrearLabel("Importe a abonar"); lblImporte.Location = new Point(20, 350);
            txtImporte = Tema_GV42.CrearTextBox(); txtImporte.Location = new Point(20, 370); txtImporte.Size = new Size(220, 24); txtImporte.ReadOnly = true;

            var lblTransaccion = Tema_GV42.CrearLabel("Número de transacción"); lblTransaccion.Location = new Point(20, 400);
            txtNumeroTransaccion = Tema_GV42.CrearTextBox(); txtNumeroTransaccion.Location = new Point(20, 420); txtNumeroTransaccion.Size = new Size(220, 24);

            btnConfirmarPago = new Button { Text = "Confirmar pago", Location = new Point(20, 460), Size = new Size(220, 40) };
            Tema_GV42.EstilizarBotonPrimario(btnConfirmarPago);
            btnConfirmarPago.Click += btnConfirmarPago_Click;

            btnVerBoletos = new Button { Text = "Ver boletos", Location = new Point(260, 460), Size = new Size(220, 40), Enabled = false };
            Tema_GV42.EstilizarBotonSecundario(btnVerBoletos);
            btnVerBoletos.Click += (s, e) => { if (_reserva != null) FRMBoletos_GV42.Mostrar(this, _reserva.NumeroReserva); };

            lblBoletos = new Label { Location = new Point(20, 510), Size = new Size(460, 60), Font = Tema_GV42.FuenteSubtitulo, ForeColor = Tema_GV42.Acento };

            card.Controls.AddRange(new Control[] {
                lblTitulo, lblNumero, txtNumeroReserva, btnBuscar, lblDetalle,
                lblMedio, cmbMedioPago, lblImporte, txtImporte, lblTransaccion, txtNumeroTransaccion,
                btnConfirmarPago, btnVerBoletos, lblBoletos
            });
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Se limpia el estado antes de buscar: si la búsqueda falla, no puede quedar habilitado
            // el pago de la reserva anterior mientras la caja muestra otro número.
            _reserva = null;
            lblDetalle.Text = "";
            lblBoletos.Text = "";
            txtImporte.Clear();
            btnConfirmarPago.Enabled = false;
            if (btnVerBoletos != null) btnVerBoletos.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtNumeroReserva.Text))
            {
                Tema_GV42.MostrarError(txtNumeroReserva, "Ingresá el número de reserva (ej: RES-000123).");
                return;
            }

            try
            {
                _reserva = _bll.BuscarReserva(txtNumeroReserva.Text.Trim());
                if (_reserva == null)
                {
                    MessageBox.Show("No existe una reserva con ese número.", "No encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblDetalle.Text =
                    "Cliente: " + _reserva.Cliente.NombreCompleto + "\n" +
                    "Vuelo: " + _reserva.VueloClase.CodigoVuelo + " — " + _reserva.VueloClase.OrigenDescripcion + " -> " + _reserva.VueloClase.DestinoDescripcion + "\n" +
                    "Salida: " + _reserva.VueloClase.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm") + "\n" +
                    "Clase: " + _reserva.VueloClase.ClaseTexto + " — Pasajeros: " + _reserva.CantidadPasajeros + "\n" +
                    "Estado: " + _reserva.EstadoTexto + "\n" +
                    "Importe total: " + _reserva.ImporteTotal.ToString("C2");

                txtImporte.Text = _reserva.ImporteTotal.ToString("N2");
                bool yaSalio = _reserva.VueloClase.FechaHoraSalida <= DateTime.Now;
                btnConfirmarPago.Enabled = _reserva.Estado == EstadoReserva_GV42.PendienteDePago && !yaSalio;
                btnVerBoletos.Enabled = _reserva.Estado == EstadoReserva_GV42.Confirmada;
                lblBoletos.Text = "";

                if (_reserva.Estado != EstadoReserva_GV42.PendienteDePago)
                    MessageBox.Show("Esta reserva ya no está pendiente de pago (" + _reserva.EstadoTexto + ").",
                        "Sin acción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (yaSalio)
                    MessageBox.Show("El vuelo de esta reserva ya salió: no se puede registrar el pago.",
                        "Sin acción", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NegocioException_GV42 ex)
            {
                _reserva = null;
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                _reserva = null;
                Tema_GV42.MostrarErrorInesperado("buscar la reserva", ex);
            }
        }

        private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            if (_reserva == null) { MessageBox.Show("Buscá primero la reserva.", "Falta la reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cmbMedioPago.SelectedItem == null) { Tema_GV42.MostrarError(cmbMedioPago, "Elegí el medio de pago.", "Falta un dato"); return; }

            MedioPago_GV42 medio = (MedioPago_GV42)cmbMedioPago.SelectedItem;
            string tx = txtNumeroTransaccion.Text.Trim();
            if (medio == MedioPago_GV42.Transferencia &&
                !System.Text.RegularExpressions.Regex.IsMatch(tx, Servicios.Validaciones_GV42.REGEX_TX_TRANSFERENCIA))
            {
                Tema_GV42.MostrarError(txtNumeroTransaccion, "Ingresá el número de operación de la transferencia (6 a 40 letras, números o guiones).");
                return;
            }
            if ((medio == MedioPago_GV42.TarjetaCredito || medio == MedioPago_GV42.TarjetaDebito) &&
                !System.Text.RegularExpressions.Regex.IsMatch(tx, Servicios.Validaciones_GV42.REGEX_TX_TARJETA))
            {
                Tema_GV42.MostrarError(txtNumeroTransaccion, "Ingresá el código de autorización de la tarjeta (6 a 20 dígitos).");
                return;
            }

            try
            {
                // El importe es el total de la reserva (el campo es de solo lectura): se usa el valor
                // numérico directamente en vez de volver a parsear el texto (dependía de la cultura).
                _bll.RegistrarPago(_reserva.NumeroReserva, medio, _reserva.ImporteTotal, tx);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar el pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado("registrar el pago", ex);
                return;
            }

            // El pago ya quedó registrado: lo que sigue no puede decir "no se pudo registrar el pago".
            btnConfirmarPago.Enabled = false;
            btnVerBoletos.Enabled = true;
            try
            {
                var boletos = _bll.ObtenerBoletos(_reserva.NumeroReserva);
                lblBoletos.Text = "Pago registrado. Boletos emitidos:\n" +
                    string.Join("\n", boletos.Select(b => "  " + b.NumeroBoleto + " — " + b.PasajeroNombre));

                if (MessageBox.Show("Pago registrado y reserva confirmada.\n\n¿Desea ver los boletos emitidos?", "Listo",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    FRMBoletos_GV42.Mostrar(this, _reserva.NumeroReserva);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El pago se registró correctamente, pero no se pudieron mostrar los boletos:\n" + ex.Message,
                                "Pago registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
