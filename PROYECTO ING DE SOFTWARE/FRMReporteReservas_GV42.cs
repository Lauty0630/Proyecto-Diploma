using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Reporte asociado al RFN 1 – Reserva de vuelo (rol Gerente).
    // Consolida todas las reservas y permite filtrarlas por fecha (de reserva o de salida), vuelo,
    // clase, estado y pasajero. Se exporta a PDF igual que la bitácora de eventos.
    //  - Reportes.Reservas: ver el reporte.
    //  - Reportes.ReservasExportarPDF: botón "Exportar PDF".
    public class FRMReporteReservas_GV42 : Form
    {
        private class FilaReporte
        {
            public string NumeroReserva { get; set; }
            public DateTime FechaReserva { get; set; }
            public string Pasajero { get; set; }
            public string Dni { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Vuelo { get; set; }
            public string Origen { get; set; }
            public string Destino { get; set; }
            public DateTime Salida { get; set; }
            public DateTime Llegada { get; set; }
            public string Clase { get; set; }
            public int Pasajeros { get; set; }
            public string Adicionales { get; set; }
            public decimal ImporteBase { get; set; }
            public decimal Impuestos { get; set; }
            public decimal ImporteTotal { get; set; }
            public string Estado { get; set; }
            public EstadoReserva_GV42 EstadoValor { get; set; }
        }

        private class Opcion<T>
        {
            public string Texto { get; set; }
            public T Valor { get; set; }
            public override string ToString() => Texto;
        }

        private readonly BLLReporteReserva_GV42 _bll = new BLLReporteReserva_GV42();
        private readonly bool _puedeExportar;

        private CheckBox chkFecha;
        private ComboBox cboTipoFecha;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private TextBox txtVuelo;
        private ComboBox cboClase;
        private ComboBox cboEstado;
        private TextBox txtPasajero;
        private Button btnAplicar;
        private Button btnLimpiar;
        private Button btnExportar;
        private DataGridView dgv;
        private Label lblResumen;
        private readonly Font _fuenteEstado = new Font("Segoe UI", 9F, FontStyle.Bold);

        private List<ReporteReserva_GV42> _resultado = new List<ReporteReserva_GV42>();
        private FiltroReporteReservas_GV42 _filtroAplicado = new FiltroReporteReservas_GV42();

        public FRMReporteReservas_GV42()
        {
            _puedeExportar = _bll.PuedeExportar();
            ConstruirUI();
            Load += (s, e) => Aplicar();
        }

        private void ConstruirUI()
        {
            Text = "Reporte de reservas";
            BackColor = Tema_GV42.Fondo;
            ClientSize = new Size(1100, 640);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);

            var card = Tema_GV42.CrearCard();
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(20);
            var contenedor = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            contenedor.Controls.Add(card);
            Controls.Add(contenedor);

            // ---- Filtros (arriba) ----
            var pnlArriba = new Panel { Dock = DockStyle.Top, Height = 190 };

            var lblTitulo = new Label
            {
                Text = "Reporte de reservas",
                Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento,
                AutoSize = true, Location = new Point(0, 0)
            };
            var lblSub = new Label
            {
                Text = "RFN 1 – Reserva de vuelo: seguimiento de reservas, ocupación y servicios adicionales.",
                Font = Tema_GV42.FuenteSubtitulo, ForeColor = Color.DimGray,
                AutoSize = true, Location = new Point(2, 34)
            };

            chkFecha = new CheckBox
            {
                Text = "Filtrar por", AutoSize = true, Location = new Point(0, 70),
                Font = Tema_GV42.FuenteLabel, ForeColor = Tema_GV42.Acento
            };
            cboTipoFecha = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(100, 67), Width = 150 };
            cboTipoFecha.Items.Add(new Opcion<FechaReporte_GV42> { Texto = "Fecha de reserva", Valor = FechaReporte_GV42.Realizacion });
            cboTipoFecha.Items.Add(new Opcion<FechaReporte_GV42> { Texto = "Fecha de salida", Valor = FechaReporte_GV42.Salida });

            var lblDesde = Tema_GV42.CrearLabel("Desde");
            lblDesde.Location = new Point(270, 70);
            dtpDesde = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(320, 67), Width = 110 };
            var lblHasta = Tema_GV42.CrearLabel("Hasta");
            lblHasta.Location = new Point(450, 70);
            dtpHasta = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(498, 67), Width = 110 };
            chkFecha.CheckedChanged += (s, e) => HabilitarFechas();

            var lblVuelo = Tema_GV42.CrearLabel("Vuelo");
            lblVuelo.Location = new Point(0, 104);
            txtVuelo = Tema_GV42.CrearTextBox();
            txtVuelo.Location = new Point(0, 124);
            txtVuelo.Size = new Size(130, 24);
            txtVuelo.MaxLength = 20;
            txtVuelo.CharacterCasing = CharacterCasing.Upper;

            var lblClase = Tema_GV42.CrearLabel("Clase");
            lblClase.Location = new Point(150, 104);
            cboClase = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(150, 124), Width = 150 };
            cboClase.Items.Add(new Opcion<ClaseVuelo_GV42?> { Texto = "(Todas)", Valor = null });
            foreach (ClaseVuelo_GV42 c in Enum.GetValues(typeof(ClaseVuelo_GV42)))
                cboClase.Items.Add(new Opcion<ClaseVuelo_GV42?> { Texto = c.Texto(), Valor = c });

            var lblEstado = Tema_GV42.CrearLabel("Estado");
            lblEstado.Location = new Point(320, 104);
            cboEstado = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(320, 124), Width = 160 };
            cboEstado.Items.Add(new Opcion<EstadoReserva_GV42?> { Texto = "(Todos)", Valor = null });
            foreach (EstadoReserva_GV42 e in Enum.GetValues(typeof(EstadoReserva_GV42)))
                cboEstado.Items.Add(new Opcion<EstadoReserva_GV42?> { Texto = e.Texto(), Valor = e });

            var lblPasajero = Tema_GV42.CrearLabel("Pasajero (DNI, nombre o apellido)");
            lblPasajero.Location = new Point(500, 104);
            txtPasajero = Tema_GV42.CrearTextBox();
            txtPasajero.Location = new Point(500, 124);
            txtPasajero.Size = new Size(240, 24);
            txtPasajero.MaxLength = 60;

            btnAplicar = new Button { Text = "Aplicar", Location = new Point(0, 156), Size = new Size(110, 30) };
            Tema_GV42.EstilizarBotonPrimario(btnAplicar);
            btnAplicar.Click += (s, e) => Aplicar();

            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(120, 156), Size = new Size(110, 30) };
            Tema_GV42.EstilizarBotonSecundario(btnLimpiar);
            btnLimpiar.Click += (s, e) => { LimpiarFiltros(); Aplicar(); };

            btnExportar = new Button
            {
                Text = "Exportar PDF", Location = new Point(760, 120), Size = new Size(140, 30),
                Visible = _puedeExportar
            };
            Tema_GV42.EstilizarBotonPrimario(btnExportar);
            btnExportar.Click += btnExportar_Click;

            var tips = new ToolTip();
            tips.SetToolTip(btnAplicar, "Aplica los filtros y actualiza el reporte.");
            tips.SetToolTip(btnLimpiar, "Quita todos los filtros y muestra todas las reservas.");
            tips.SetToolTip(btnExportar, "Guarda en PDF las reservas que se ven en la grilla.");
            tips.SetToolTip(txtPasajero, "Busca en el titular y en todos los pasajeros de la reserva.");

            // Enter en los cuadros de texto aplica los filtros.
            txtVuelo.KeyDown += EnterAplica;
            txtPasajero.KeyDown += EnterAplica;

            pnlArriba.Controls.AddRange(new Control[]
            {
                lblTitulo, lblSub, chkFecha, cboTipoFecha, lblDesde, dtpDesde, lblHasta, dtpHasta,
                lblVuelo, txtVuelo, lblClase, cboClase, lblEstado, cboEstado, lblPasajero, txtPasajero,
                btnAplicar, btnLimpiar, btnExportar
            });

            // ---- Resumen (abajo) ----
            var pnlAbajo = new Panel { Dock = DockStyle.Bottom, Height = 48 };
            lblResumen = new Label
            {
                Dock = DockStyle.Fill, Font = Tema_GV42.FuenteTexto, ForeColor = Tema_GV42.Acento,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlAbajo.Controls.Add(lblResumen);

            // ---- Grilla (resto) ----
            dgv = new DataGridView { Dock = DockStyle.Fill };
            Tema_GV42.EstilizarGrilla(dgv);
            dgv.AutoGenerateColumns = false;
            // Son muchas columnas: se ajustan al contenido y la grilla se desplaza horizontalmente.
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.ScrollBars = ScrollBars.Both;

            Columna("NumeroReserva", "N° reserva");
            Columna("FechaReserva", "Fecha reserva", "dd/MM/yyyy HH:mm");
            Columna("Pasajero", "Pasajero");
            Columna("Dni", "DNI");
            Columna("Email", "Email");
            Columna("Telefono", "Teléfono");
            Columna("Vuelo", "Vuelo");
            Columna("Origen", "Origen");
            Columna("Destino", "Destino");
            Columna("Salida", "Salida", "dd/MM/yyyy HH:mm");
            Columna("Llegada", "Llegada", "dd/MM/yyyy HH:mm");
            Columna("Clase", "Clase");
            Columna("Pasajeros", "Pax", null, true);
            Columna("Adicionales", "Adicionales (tipo, cant., costo)");
            Columna("ImporteBase", "Importe base", "C2", true);
            Columna("Impuestos", "Impuestos", "C2", true);
            Columna("ImporteTotal", "Total final", "C2", true);
            Columna("Estado", "Estado");
            dgv.CellFormatting += dgv_CellFormatting;

            // Orden de acoplamiento: Fill primero, después Bottom y Top.
            card.Controls.Add(dgv);
            card.Controls.Add(pnlAbajo);
            card.Controls.Add(pnlArriba);

            LimpiarFiltros();
        }

        private void Columna(string propiedad, string titulo, string formato = null, bool derecha = false)
        {
            var col = new DataGridViewTextBoxColumn { DataPropertyName = propiedad, Name = propiedad, HeaderText = titulo };
            if (formato != null) col.DefaultCellStyle.Format = formato;
            if (derecha) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns.Add(col);
        }

        private void EnterAplica(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            Aplicar();
        }

        private void HabilitarFechas()
        {
            cboTipoFecha.Enabled = dtpDesde.Enabled = dtpHasta.Enabled = chkFecha.Checked;
        }

        private void LimpiarFiltros()
        {
            chkFecha.Checked = false;
            cboTipoFecha.SelectedIndex = 0;
            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            txtVuelo.Clear();
            cboClase.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            txtPasajero.Clear();
            HabilitarFechas();
        }

        private FiltroReporteReservas_GV42 LeerFiltro()
        {
            var f = new FiltroReporteReservas_GV42
            {
                CodigoVuelo = txtVuelo.Text,
                Pasajero = txtPasajero.Text,
                Clase = ((Opcion<ClaseVuelo_GV42?>)cboClase.SelectedItem).Valor,
                Estado = ((Opcion<EstadoReserva_GV42?>)cboEstado.SelectedItem).Valor
            };
            if (chkFecha.Checked)
            {
                f.TipoFecha = ((Opcion<FechaReporte_GV42>)cboTipoFecha.SelectedItem).Valor;
                f.FechaDesde = dtpDesde.Value.Date;
                f.FechaHasta = dtpHasta.Value.Date;
            }
            return f;
        }

        private void Aplicar()
        {
            try
            {
                FiltroReporteReservas_GV42 filtro = LeerFiltro();
                _resultado = _bll.Generar(filtro);
                _filtroAplicado = filtro;
                MostrarResultado();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los filtros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el reporte.\n\n" + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResultado()
        {
            var filas = _resultado.Select(r => new FilaReporte
            {
                NumeroReserva = r.NumeroReserva,
                FechaReserva = r.FechaRealizacion,
                Pasajero = r.PasajeroNombreCompleto,
                Dni = r.PasajeroDni,
                Email = r.Email,
                Telefono = r.Telefono,
                Vuelo = r.CodigoVuelo,
                Origen = r.Origen,
                Destino = r.Destino,
                Salida = r.FechaHoraSalida,
                Llegada = r.FechaHoraLlegada,
                Clase = r.ClaseTexto,
                Pasajeros = r.CantidadPasajeros,
                Adicionales = BLLReporteReserva_GV42.TextoAdicionales(r, "; "),
                ImporteBase = r.ImporteBase,
                Impuestos = r.Impuestos,
                ImporteTotal = r.ImporteTotal,
                Estado = r.EstadoTexto,
                EstadoValor = r.Estado
            }).ToList();

            dgv.DataSource = null;
            dgv.DataSource = filas;

            lblResumen.Text = string.Join(Environment.NewLine, _bll.Resumen(_resultado));
            btnExportar.Enabled = _resultado.Count > 0;
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Columns[e.ColumnIndex].Name != "Estado") return;
            var fila = dgv.Rows[e.RowIndex].DataBoundItem as FilaReporte;
            if (fila == null) return;

            e.CellStyle.Font = _fuenteEstado;
            switch (fila.EstadoValor)
            {
                case EstadoReserva_GV42.Confirmada: e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); break;
                case EstadoReserva_GV42.PendienteDePago: e.CellStyle.ForeColor = Tema_GV42.Advertencia; break;
                default: e.CellStyle.ForeColor = Tema_GV42.Error; break;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_resultado.Count == 0)
            {
                MessageBox.Show("No hay reservas para exportar con los filtros aplicados.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.Title = "Guardar reporte de reservas";
                sfd.FileName = $"ReporteReservas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // Se exporta exactamente lo que se ve (los filtros aplicados con el último "Aplicar").
                    _bll.ExportarPdf(sfd.FileName, _resultado, _filtroAplicado);

                    if (MessageBox.Show("PDF generado correctamente:\n" + sfd.FileName + "\n\n¿Desea abrirlo ahora?",
                                        "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(ex.Message, "No se pudo exportar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF.\n\n" + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
