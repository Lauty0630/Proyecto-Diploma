using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Consultar reservas. Un mismo formulario con dos modos, decididos por las patentes del rol:
    //  - Vendedor (Reservas.Consultar): busca entre todas las reservas y ve de quién es cada una.
    //    Solo ve el botón Cancelar si además tiene Reservas.Cancelar.
    //  - Pasajero (Reservas.ConsultarPropia): "Mis reservas", sin buscador; ve y cancela solo las suyas
    //    (Reservas.CancelarPropia).
    // "Hacer check-in" (RFN 2) se ve solo con alguna patente de check-in (CheckIn.Realizar o
    // CheckIn.RealizarPropio) y abre FRMCheckIn_GV42 con la reserva seleccionada.
    // El diseño está en FRMConsultarReservas_GV42.Designer.cs (Form Designer).
    public partial class FRMConsultarReservas_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private readonly bool _esVendedor;
        private readonly bool _puedeCancelar;
        private readonly bool _puedeHacerCheckIn;
        private List<Reserva_GV42> _reservas = new List<Reserva_GV42>();

        #endregion

        #region Constructor

        public FRMConsultarReservas_GV42()
        {
            InitializeComponent();

            _esVendedor = _bll.PuedeConsultarTodas();
            _puedeCancelar = _bll.PuedeCancelar();
            var bllCheckIn = new BLLCheckIn_GV42();
            _puedeHacerCheckIn = bllCheckIn.PuedeAtenderMostrador() || bllCheckIn.PuedeHacerCheckInOnline();

            dgvReservas.AutoGenerateColumns = false;
            ConfigurarSegunPermisos();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
            // Las reservas se cargan en Load (FRMConsultarReservas_GV42_Load).
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T(_esVendedor ? "consulta.tituloVendedor" : "consulta.tituloPropias");
            lblTitulo.Text = Text;
            lblSubtitulo.Text = IdiomaManager_GV42.T(_esVendedor ? "consulta.subtituloVendedor" : "consulta.subtituloPropias");

            lblBuscar.Text = IdiomaManager_GV42.T("consulta.buscarPor");
            btnBuscar.Text = IdiomaManager_GV42.T("consulta.buscar");
            lblAyudaBusqueda.Text = IdiomaManager_GV42.T("consulta.ayudaBusqueda");
            lblAyudaAcciones.Text = IdiomaManager_GV42.T("consulta.ayudaAcciones");
            btnVerBoletos.Text = IdiomaManager_GV42.T("consulta.verBoletos");
            btnCancelar.Text = IdiomaManager_GV42.T(_esVendedor ? "consulta.cancelarSeleccionada" : "consulta.cancelarMia");
            btnCheckIn.Text = IdiomaManager_GV42.T("consulta.hacerCheckIn");
            toolTip.SetToolTip(btnCheckIn, IdiomaManager_GV42.T("consulta.ayudaCheckIn",
                BLLCheckIn_GV42.HORAS_APERTURA_CHECKIN, BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN));

            colReserva.HeaderText = IdiomaManager_GV42.T("consulta.colReserva");
            colDni.HeaderText = IdiomaManager_GV42.T("consulta.colDni");
            colCliente.HeaderText = IdiomaManager_GV42.T("consulta.colCliente");
            colVuelo.HeaderText = IdiomaManager_GV42.T("consulta.colVuelo");
            colRuta.HeaderText = IdiomaManager_GV42.T("consulta.colRuta");
            colSalida.HeaderText = IdiomaManager_GV42.T("consulta.colSalida");
            colClase.HeaderText = IdiomaManager_GV42.T("consulta.colClase");
            colEstado.HeaderText = IdiomaManager_GV42.T("consulta.colEstado");
            colImporte.HeaderText = IdiomaManager_GV42.T("consulta.colImporte");

            // Clase y estado de cada fila se traducen: se vuelven a armar las filas (sin ir a la base).
            MostrarReservas();
        }

        #endregion

        #region Carga de datos

        // Vendedor: buscador y columnas del cliente. Pasajero: solo sus reservas, sin buscador.
        private void ConfigurarSegunPermisos()
        {
            pnlBusqueda.Visible = _esVendedor;
            pnlSeparadorArriba.Visible = _esVendedor;
            colDni.Visible = _esVendedor;
            colCliente.Visible = _esVendedor;
            btnCancelar.Visible = _puedeCancelar;
            btnCheckIn.Visible = _puedeHacerCheckIn;
        }

        private void CargarReservas()
        {
            try
            {
                _reservas = _esVendedor
                    ? _bll.BuscarReservas(txtBusqueda.Text)
                    : _bll.ListarMisReservas();
                MostrarReservas();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("consulta.accionCargar"), ex);
            }
        }

        // Arma las filas de la grilla a partir de las reservas ya cargadas, conservando la selección.
        private void MostrarReservas()
        {
            Reserva_GV42 anterior = ObtenerSeleccionada();

            var filas = (_reservas ?? new List<Reserva_GV42>()).Select(r => new FilaReserva
            {
                NumeroReserva = r.NumeroReserva,
                Cliente = r.Cliente.NombreCompleto,
                DniCliente = r.Cliente.DNI,
                Vuelo = r.VueloClase.CodigoVuelo,
                Ruta = r.VueloClase.OrigenDescripcion + " -> " + r.VueloClase.DestinoDescripcion,
                Salida = r.VueloClase.FechaHoraSalida,
                Clase = r.VueloClase.ClaseTexto,
                Estado = r.EstadoTexto,
                ImporteTotal = r.ImporteTotal,
                Reserva = r
            }).ToList();

            dgvReservas.DataSource = null;
            dgvReservas.DataSource = filas;

            if (anterior != null)
            {
                foreach (DataGridViewRow fila in dgvReservas.Rows)
                {
                    if (((FilaReserva)fila.DataBoundItem).NumeroReserva == anterior.NumeroReserva)
                    {
                        fila.Selected = true;
                        break;
                    }
                }
            }
            ActualizarBotones();
        }

        private Reserva_GV42 ObtenerSeleccionada()
        {
            if (dgvReservas.SelectedRows.Count == 0) return null;
            var fila = dgvReservas.SelectedRows[0].DataBoundItem as FilaReserva;
            return fila != null ? fila.Reserva : null;
        }

        // Boletos: solo para reservas confirmadas (pagas). Cancelar: no si ya está cancelada
        // o si el vuelo ya salió (la BLL igual lo controla).
        private void ActualizarBotones()
        {
            Reserva_GV42 sel = ObtenerSeleccionada();
            btnVerBoletos.Enabled = sel != null && sel.Estado == EstadoReserva_GV42.Confirmada;
            btnCheckIn.Enabled = _puedeHacerCheckIn && sel != null && PuedeHacerCheckIn(sel);
            if (!_puedeCancelar) { btnCancelar.Visible = false; return; }
            btnCancelar.Enabled = sel != null && sel.Estado != EstadoReserva_GV42.Cancelada &&
                                  sel.VueloClase.FechaHoraSalida > DateTime.Now;
        }

        // Check-in: reserva confirmada y vuelo dentro de la ventana (desde 48 hs hasta 60 minutos antes
        // de la salida). Se usan las mismas constantes que la BLL, que igual lo vuelve a validar.
        private static bool PuedeHacerCheckIn(Reserva_GV42 r)
        {
            if (r.Estado != EstadoReserva_GV42.Confirmada) return false;
            DateTime salida = r.VueloClase.FechaHoraSalida;
            DateTime ahora = DateTime.Now;
            return ahora >= salida.AddHours(-BLLCheckIn_GV42.HORAS_APERTURA_CHECKIN) &&
                   ahora <= salida.AddMinutes(-BLLCheckIn_GV42.MINUTOS_CIERRE_CHECKIN);
        }

        // Si este formulario está embebido en el menú principal, el check-in se abre en el mismo panel.
        private FRMMenuPrincipalAdmin MenuContenedor()
        {
            Control c = Parent;
            while (c != null && !(c is FRMMenuPrincipalAdmin)) c = c.Parent;
            return c as FRMMenuPrincipalAdmin;
        }

        #endregion

        #region Eventos

        private void FRMConsultarReservas_GV42_Load(object sender, EventArgs e)
        {
            CargarReservas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarReservas();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CargarReservas();
            }
        }

        private void dgvReservas_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        // El estado se pinta con el color de la paleta: confirmada (verde), pendiente (naranja), cancelada (rojo).
        private void dgvReservas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colEstado.Index) return;
            var fila = dgvReservas.Rows[e.RowIndex].DataBoundItem as FilaReserva;
            if (fila == null) return;
            e.CellStyle.ForeColor = fila.Reserva.Estado == EstadoReserva_GV42.Confirmada ? Tema_GV42.Exito
                                  : fila.Reserva.Estado == EstadoReserva_GV42.Cancelada ? Tema_GV42.Error
                                  : Tema_GV42.Advertencia;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        private void btnVerBoletos_Click(object sender, EventArgs e)
        {
            Reserva_GV42 sel = ObtenerSeleccionada();
            if (sel != null) FRMBoletos_GV42.Mostrar(this, sel.NumeroReserva);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            Reserva_GV42 sel = ObtenerSeleccionada();
            if (sel == null || !PuedeHacerCheckIn(sel)) return;

            var frmCheckIn = new FRMCheckIn_GV42(sel.NumeroReserva);
            FRMMenuPrincipalAdmin menu = MenuContenedor();
            if (menu != null)
                // Se abre después de terminar este evento: el menú cierra este formulario al abrir el nuevo.
                menu.BeginInvoke((Action)(() => menu.AbrirFormularioHijo(frmCheckIn)));
            else
                using (frmCheckIn) frmCheckIn.ShowDialog(this);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Reserva_GV42 sel = ObtenerSeleccionada();
            if (sel == null) return;

            // Pendiente de pago: no se cobró nada, así que no hay penalidad (igual que en la BLL).
            decimal porcentaje = sel.Estado == EstadoReserva_GV42.PendienteDePago
                ? 0m : _bll.CalcularPorcentajePenalidad(sel.VueloClase.FechaHoraSalida);
            decimal estimado = Math.Round(sel.ImporteTotal * porcentaje, 2);

            string mensaje = IdiomaManager_GV42.T("consulta.confirmarCancelar", sel.NumeroReserva);
            mensaje += "\n\n" + (porcentaje > 0
                ? IdiomaManager_GV42.T("consulta.conPenalidad", estimado.ToString("C2"), porcentaje * 100)
                : (sel.Estado == EstadoReserva_GV42.PendienteDePago
                    ? IdiomaManager_GV42.T("consulta.sinPenalidadImpaga")
                    : IdiomaManager_GV42.T("consulta.sinPenalidad")));

            if (MessageBox.Show(mensaje, IdiomaManager_GV42.T("consulta.confirmarTitulo"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _bll.CancelarReserva(sel.NumeroReserva);
                MessageBox.Show(IdiomaManager_GV42.T("consulta.cancelada"), IdiomaManager_GV42.T("consulta.listo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarReservas();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("consulta.noSePudoCancelar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("consulta.accionCancelar"), ex);
            }
        }

        #endregion

        #region Tipos anidados

        // Fila de la grilla: los textos (clase, estado, ruta) ya vienen armados para mostrar.
        private class FilaReserva
        {
            public string NumeroReserva { get; set; }
            public string Cliente { get; set; }
            public string DniCliente { get; set; }
            public string Vuelo { get; set; }
            public string Ruta { get; set; }
            public DateTime Salida { get; set; }
            public string Clase { get; set; }
            public string Estado { get; set; }
            public decimal ImporteTotal { get; set; }
            public Reserva_GV42 Reserva { get; set; }
        }

        #endregion
    }
}
