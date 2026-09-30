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
    // Consultar reservas. Un mismo formulario con dos modos, decididos por las patentes del rol:
    //  - Vendedor (Reservas.Consultar): busca entre todas las reservas y ve de quién es cada una.
    //    Solo ve el botón Cancelar si además tiene Reservas.Cancelar.
    //  - Pasajero (Reservas.ConsultarPropia): "Mis reservas", sin buscador; ve y cancela solo las suyas
    //    (Reservas.CancelarPropia).
    public class FRMConsultarReservas_GV42 : Form
    {
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

        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();
        private readonly bool _esVendedor;
        private readonly bool _puedeCancelar;

        private TextBox txtBusqueda;
        private DataGridView dgv;
        private Button btnCancelar;
        private List<Reserva_GV42> _reservas = new List<Reserva_GV42>();

        public FRMConsultarReservas_GV42()
        {
            _esVendedor = _bll.PuedeConsultarTodas();
            _puedeCancelar = _bll.PuedeCancelar();

            ConstruirUI();
            CargarReservas();
        }

        private void ConstruirUI()
        {
            Text = _esVendedor ? "Consultar reservas" : "Mis reservas";
            BackColor = Tema_GV42.Fondo;
            ClientSize = new Size(900, 560);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);

            var card = Tema_GV42.CrearCard();
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(20);
            card.Padding = new Padding(20);
            var contenedor = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            contenedor.Controls.Add(card);
            Controls.Add(contenedor);

            var lblTitulo = new Label
            {
                Text = _esVendedor ? "Consultar reservas" : "Mis reservas",
                Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento,
                AutoSize = true, Location = new Point(0, 0)
            };
            // Zonas acopladas: título/búsqueda arriba, botón abajo y la grilla ocupa el resto
            // (antes, con tamaños fijos y anclajes, la grilla y el botón quedaban cortados).
            var pnlArriba = new Panel { Dock = DockStyle.Top };
            var pnlAbajo = new Panel { Dock = DockStyle.Bottom, Height = 50, Visible = _puedeCancelar };
            pnlArriba.Controls.Add(lblTitulo);

            int yBajoTitulo = 45;

            if (_esVendedor)
            {
                var lblBuscar = Tema_GV42.CrearLabel("Buscar por número, DNI o apellido del cliente");
                lblBuscar.Location = new Point(0, yBajoTitulo);
                txtBusqueda = Tema_GV42.CrearTextBox();
                txtBusqueda.Location = new Point(0, yBajoTitulo + 20);
                txtBusqueda.Size = new Size(320, 24);
                var btnBuscar = new Button { Text = "Buscar", Location = new Point(330, yBajoTitulo + 19), Size = new Size(100, 26) };
                Tema_GV42.EstilizarBotonSecundario(btnBuscar);
                btnBuscar.Click += (s, e) => CargarReservas();
                pnlArriba.Controls.Add(lblBuscar);
                pnlArriba.Controls.Add(txtBusqueda);
                pnlArriba.Controls.Add(btnBuscar);
                yBajoTitulo += 60;
            }
            pnlArriba.Height = yBajoTitulo;

            dgv = new DataGridView { Dock = DockStyle.Fill };
            Tema_GV42.EstilizarGrilla(dgv);
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroReserva", HeaderText = "Reserva" });
            if (_esVendedor)
            {
                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DniCliente", HeaderText = "DNI cliente" });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cliente", HeaderText = "Cliente" });
            }
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Vuelo", HeaderText = "Vuelo" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ruta", HeaderText = "Ruta" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Salida", HeaderText = "Salida", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Clase", HeaderText = "Clase" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ImporteTotal", HeaderText = "Importe", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            dgv.SelectionChanged += (s, e) => ActualizarBotonCancelar();

            btnCancelar = new Button
            {
                Text = _esVendedor ? "Cancelar reserva seleccionada" : "Cancelar mi reserva",
                Location = new Point(0, 10),
                Size = new Size(240, 36),
                Visible = _puedeCancelar
            };
            Tema_GV42.EstilizarBotonSecundario(btnCancelar);
            btnCancelar.Click += btnCancelar_Click;
            pnlAbajo.Controls.Add(btnCancelar);

            // Orden de acoplamiento: Fill primero, después Bottom y Top.
            card.Controls.Add(dgv);
            card.Controls.Add(pnlAbajo);
            card.Controls.Add(pnlArriba);
        }

        private void CargarReservas()
        {
            try
            {
                _reservas = _esVendedor
                    ? _bll.BuscarReservas(txtBusqueda?.Text)
                    : _bll.ListarMisReservas();

                var filas = _reservas.Select(r => new FilaReserva
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

                dgv.DataSource = null;
                dgv.DataSource = filas;
                ActualizarBotonCancelar();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Reserva_GV42 ObtenerSeleccionada()
        {
            if (dgv.SelectedRows.Count == 0) return null;
            return ((FilaReserva)dgv.SelectedRows[0].DataBoundItem).Reserva;
        }

        private void ActualizarBotonCancelar()
        {
            if (!_puedeCancelar) { btnCancelar.Visible = false; return; }
            Reserva_GV42 sel = ObtenerSeleccionada();
            btnCancelar.Enabled = sel != null && sel.Estado != EstadoReserva_GV42.Cancelada;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Reserva_GV42 sel = ObtenerSeleccionada();
            if (sel == null) return;

            decimal porcentaje = _bll.CalcularPorcentajePenalidad(sel.VueloClase.FechaHoraSalida);
            decimal estimado = Math.Round(sel.ImporteTotal * porcentaje, 2);

            string mensaje = "¿Cancelar la reserva " + sel.NumeroReserva + "?";
            mensaje += porcentaje > 0
                ? "\n\nPor la cercanía del vuelo se aplica una penalidad estimada de " + estimado.ToString("C2") + " (" + (porcentaje * 100) + "% del total)."
                : "\n\nTodavía falta tiempo para el vuelo: no se aplica penalidad.";

            if (MessageBox.Show(mensaje, "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _bll.CancelarReserva(sel.NumeroReserva);
                MessageBox.Show("Reserva cancelada.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarReservas();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "No se pudo cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
