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
    // Gestión de vuelos: modificar los datos de un vuelo existente y darlo de baja / reactivarlo
    // (borrado lógico). Cada cambio queda registrado solo en Vuelo_C por el trigger de la base;
    // se consulta desde "Bitácora de cambios".
    public class FRMGestionVuelos_GV42 : Form
    {
        private class FilaVuelo
        {
            public string Codigo { get; set; }
            public string Aerolinea { get; set; }
            public string Ruta { get; set; }
            public DateTime Salida { get; set; }
            public DateTime Llegada { get; set; }
            public string Puerta { get; set; }
            public decimal CostoKilo { get; set; }
            public string Estado { get; set; }
            public Vuelo_GV42 Vuelo { get; set; }
        }

        private readonly BLLVuelo_GV42 _bll = new BLLVuelo_GV42();

        private DataGridView dgv;
        private TextBox txtCodigo, txtPuerta;
        private ComboBox cmbAerolinea, cmbOrigen, cmbDestino;
        private DateTimePicker dtSalida, dtLlegada;
        private NumericUpDown numCosto;
        private Button btnGuardar, btnBaja, btnReactivar;
        private Panel pnlEditor;

        private Vuelo_GV42 _seleccionado;

        public FRMGestionVuelos_GV42()
        {
            ConstruirUI();
            CargarCatalogos();
            CargarVuelos(0);
        }

        private void ConstruirUI()
        {
            Text = "Gestión de vuelos";
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
                Text = "Gestión de vuelos",
                Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento,
                AutoSize = true, Location = new Point(0, 0)
            };
            card.Controls.Add(lblTitulo);

            dgv = new DataGridView
            {
                Location = new Point(0, 45),
                Size = new Size(800, 220),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Tema_GV42.EstilizarGrilla(dgv);
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Codigo", HeaderText = "Vuelo", FillWeight = 60 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Aerolinea", HeaderText = "Aerolínea", FillWeight = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ruta", HeaderText = "Ruta", FillWeight = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Salida", HeaderText = "Salida", FillWeight = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Llegada", HeaderText = "Llegada", FillWeight = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Puerta", HeaderText = "Puerta", FillWeight = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CostoKilo", HeaderText = "$/kg exceso", FillWeight = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado", FillWeight = 70 });
            dgv.CellFormatting += dgv_CellFormatting;
            dgv.SelectionChanged += (s, e) => MostrarSeleccionado();
            card.Controls.Add(dgv);

            // ---- Editor (queda deshabilitado hasta elegir un vuelo)
            pnlEditor = new Panel { Location = new Point(0, 275), Size = new Size(800, 200), Anchor = AnchorStyles.Bottom | AnchorStyles.Left, Enabled = false };

            var lblCodigo = Tema_GV42.CrearLabel("Código");
            lblCodigo.Location = new Point(0, 0);
            txtCodigo = Tema_GV42.CrearTextBox(); txtCodigo.Location = new Point(0, 20); txtCodigo.Size = new Size(100, 24); txtCodigo.MaxLength = 10;

            var lblAero = Tema_GV42.CrearLabel("Aerolínea");
            lblAero.Location = new Point(120, 0);
            cmbAerolinea = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(120, 20), Size = new Size(180, 24) };

            var lblOrigen = Tema_GV42.CrearLabel("Origen");
            lblOrigen.Location = new Point(320, 0);
            cmbOrigen = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(320, 20), Size = new Size(210, 24) };

            var lblDestino = Tema_GV42.CrearLabel("Destino");
            lblDestino.Location = new Point(550, 0);
            cmbDestino = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(550, 20), Size = new Size(210, 24) };

            var lblSalida = Tema_GV42.CrearLabel("Salida");
            lblSalida.Location = new Point(0, 60);
            dtSalida = new DateTimePicker { Location = new Point(0, 80), Size = new Size(170, 24), Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };

            var lblLlegada = Tema_GV42.CrearLabel("Llegada");
            lblLlegada.Location = new Point(190, 60);
            dtLlegada = new DateTimePicker { Location = new Point(190, 80), Size = new Size(170, 24), Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };

            var lblPuerta = Tema_GV42.CrearLabel("Puerta");
            lblPuerta.Location = new Point(380, 60);
            txtPuerta = Tema_GV42.CrearTextBox(); txtPuerta.Location = new Point(380, 80); txtPuerta.Size = new Size(80, 24); txtPuerta.MaxLength = 10;

            var lblCosto = Tema_GV42.CrearLabel("Costo por kilo de exceso ($)");
            lblCosto.Location = new Point(480, 60);
            numCosto = new NumericUpDown { Location = new Point(480, 80), Size = new Size(140, 24), DecimalPlaces = 2, Minimum = 0, Maximum = 99999999, ThousandsSeparator = true };

            btnGuardar = new Button { Text = "Guardar cambios", Location = new Point(0, 130), Size = new Size(160, 36) };
            Tema_GV42.EstilizarBotonPrimario(btnGuardar);
            btnGuardar.Click += btnGuardar_Click;

            btnBaja = new Button { Text = "Dar de baja", Location = new Point(170, 130), Size = new Size(140, 36) };
            Tema_GV42.EstilizarBotonSecundario(btnBaja);
            btnBaja.Click += btnBaja_Click;

            btnReactivar = new Button { Text = "Reactivar", Location = new Point(320, 130), Size = new Size(140, 36) };
            Tema_GV42.EstilizarBotonSecundario(btnReactivar);
            btnReactivar.Click += btnReactivar_Click;

            var lblAyuda = new Label
            {
                Text = "Cada cambio queda registrado en la bitácora de vuelos (Vuelo_C).\nLa baja es lógica: el vuelo no se elimina, solo deja de ofrecerse.",
                AutoSize = true, ForeColor = Tema_GV42.Texto, Font = Tema_GV42.FuenteSubtitulo, Location = new Point(480, 130)
            };

            pnlEditor.Controls.AddRange(new Control[] {
                lblCodigo, txtCodigo, lblAero, cmbAerolinea, lblOrigen, cmbOrigen, lblDestino, cmbDestino,
                lblSalida, dtSalida, lblLlegada, dtLlegada, lblPuerta, txtPuerta, lblCosto, numCosto,
                btnGuardar, btnBaja, btnReactivar, lblAyuda
            });
            card.Controls.Add(pnlEditor);
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgv.Rows[e.RowIndex].DataBoundItem as FilaVuelo;
            if (fila != null && fila.Vuelo.BorradoLogico)
                e.CellStyle.ForeColor = Tema_GV42.Ocupado;
        }

        private void CargarCatalogos()
        {
            try
            {
                cmbAerolinea.DataSource = _bll.ListarAerolineas();
                cmbAerolinea.DisplayMember = "Nombre";
                cmbAerolinea.ValueMember = "Id";

                // Listas separadas: si origen y destino compartieran la lista, se moverían juntos.
                List<Aeropuerto_GV42> aeropuertos = _bll.ListarAeropuertos();
                cmbOrigen.DataSource = new List<Aeropuerto_GV42>(aeropuertos);
                cmbOrigen.DisplayMember = "Descripcion";
                cmbOrigen.ValueMember = "Id";
                cmbDestino.DataSource = new List<Aeropuerto_GV42>(aeropuertos);
                cmbDestino.DisplayMember = "Descripcion";
                cmbDestino.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron cargar los catálogos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Recarga la lista y vuelve a seleccionar el vuelo con ese Id (0 = el primero).
        private void CargarVuelos(int idSeleccionar)
        {
            try
            {
                var filas = _bll.Listar().Select(v => new FilaVuelo
                {
                    Codigo = v.CodigoVuelo,
                    Aerolinea = v.Aerolinea.Nombre,
                    Ruta = v.Origen.CodigoIata + " -> " + v.Destino.CodigoIata,
                    Salida = v.FechaHoraSalida,
                    Llegada = v.FechaHoraLlegada,
                    Puerta = v.PuertaEmbarque,
                    CostoKilo = v.CostoKiloExceso,
                    Estado = v.BorradoLogico ? "Dado de baja" : "Activo",
                    Vuelo = v
                }).ToList();

                dgv.DataSource = null;
                dgv.DataSource = filas;

                dgv.ClearSelection();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var fila = (FilaVuelo)row.DataBoundItem;
                    if (idSeleccionar == 0 || fila.Vuelo.Id == idSeleccionar)
                    {
                        row.Selected = true;
                        dgv.CurrentCell = row.Cells[0];
                        break;
                    }
                }
                MostrarSeleccionado();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Gestión de vuelos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron cargar los vuelos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSeleccionado()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                _seleccionado = null;
                pnlEditor.Enabled = false;
                return;
            }

            _seleccionado = ((FilaVuelo)dgv.SelectedRows[0].DataBoundItem).Vuelo;
            pnlEditor.Enabled = true;

            txtCodigo.Text = _seleccionado.CodigoVuelo;
            cmbAerolinea.SelectedValue = _seleccionado.Aerolinea.Id;
            cmbOrigen.SelectedValue = _seleccionado.Origen.Id;
            cmbDestino.SelectedValue = _seleccionado.Destino.Id;
            dtSalida.Value = _seleccionado.FechaHoraSalida;
            dtLlegada.Value = _seleccionado.FechaHoraLlegada;
            txtPuerta.Text = _seleccionado.PuertaEmbarque;
            numCosto.Value = _seleccionado.CostoKiloExceso;

            btnBaja.Enabled = !_seleccionado.BorradoLogico;
            btnReactivar.Enabled = _seleccionado.BorradoLogico;
        }

        private static DateTime SinSegundos(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            try
            {
                var v = new Vuelo_GV42
                {
                    Id = _seleccionado.Id,
                    CodigoVuelo = txtCodigo.Text,
                    Aerolinea = (Aerolinea_GV42)cmbAerolinea.SelectedItem,
                    Origen = (Aeropuerto_GV42)cmbOrigen.SelectedItem,
                    Destino = (Aeropuerto_GV42)cmbDestino.SelectedItem,
                    FechaHoraSalida = SinSegundos(dtSalida.Value),
                    FechaHoraLlegada = SinSegundos(dtLlegada.Value),
                    PuertaEmbarque = txtPuerta.Text,
                    CostoKiloExceso = numCosto.Value
                };

                _bll.Modificar(v);
                MessageBox.Show("Vuelo guardado. Si hubo cambios, quedaron registrados en la bitácora de vuelos.",
                    "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarVuelos(v.Id);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            if (MessageBox.Show("¿Dar de baja el vuelo " + _seleccionado.CodigoVuelo + "?\n\nDeja de ofrecerse para nuevas reservas. No se elimina y podés reactivarlo cuando quieras.",
                    "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            CambiarEstado(() => _bll.DarDeBaja(_seleccionado), "No se pudo dar de baja");
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            CambiarEstado(() => _bll.Reactivar(_seleccionado), "No se pudo reactivar");
        }

        private void CambiarEstado(Action accion, string tituloError)
        {
            int id = _seleccionado.Id;
            try
            {
                accion();
                CargarVuelos(id);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, tituloError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
