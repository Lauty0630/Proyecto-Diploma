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
    // Bitácora de cambios de vuelos (tabla Vuelo_C). Muestra todas las versiones por las que pasó cada
    // vuelo, filtrable por código, nombre (ruta) y rango de fechas. El registro con Act = 1 es el que
    // hoy está vigente en la tabla Vuelo; ACTIVAR permite volver a una versión anterior.
    public class FRMBitacoraVuelos_GV42 : Form
    {
        private class FilaCambio
        {
            public string CodigoVuelo { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Act { get; set; }
            public VueloCambio_GV42 Cambio { get; set; }
        }

        private const string TODOS = "(Todos)";

        private readonly BLLVueloHistorial_GV42 _bll = new BLLVueloHistorial_GV42();

        private DataGridView dgv;
        private ComboBox cmbCodigo, cmbNombre;
        private DateTimePicker dtIni, dtFin;
        private Button btnAplicar, btnLimpiar, btnActivar, btnSalir;

        public FRMBitacoraVuelos_GV42()
        {
            ConstruirUI();
            CargarCombos();
            Aplicar();
        }

        private void ConstruirUI()
        {
            Text = "Bitácora de cambios de vuelos";
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
                Text = "BITÁCORA DE CAMBIOS  ·  Vuelo_C",
                Font = Tema_GV42.FuenteTitulo, ForeColor = Tema_GV42.Acento,
                AutoSize = true, Location = new Point(0, 0)
            };
            card.Controls.Add(lblTitulo);

            dgv = new DataGridView
            {
                Location = new Point(0, 45),
                Size = new Size(800, 290),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Tema_GV42.EstilizarGrilla(dgv);
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CodigoVuelo", HeaderText = "Cod. vuelo", FillWeight = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha", FillWeight = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Hora", HeaderText = "Hora", FillWeight = 45 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", FillWeight = 75 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Desc.", FillWeight = 260 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Act", HeaderText = "Act.", FillWeight = 35, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.CellFormatting += dgv_CellFormatting;
            card.Controls.Add(dgv);

            // ---- Filtros
            int y = 350;
            var lblCod = Tema_GV42.CrearLabel("Cod. vuelo");
            lblCod.Location = new Point(0, y);
            cmbCodigo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 20), Size = new Size(150, 24), Anchor = AnchorStyles.Bottom | AnchorStyles.Left };

            var lblNom = Tema_GV42.CrearLabel("Nombre (ruta)");
            lblNom.Location = new Point(170, y);
            cmbNombre = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(170, y + 20), Size = new Size(180, 24), Anchor = AnchorStyles.Bottom | AnchorStyles.Left };

            var lblIni = Tema_GV42.CrearLabel("Fecha ini.");
            lblIni.Location = new Point(370, y);
            dtIni = new DateTimePicker { Location = new Point(370, y + 20), Size = new Size(130, 24), Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };

            var lblFin = Tema_GV42.CrearLabel("Fecha fin");
            lblFin.Location = new Point(520, y);
            dtFin = new DateTimePicker { Location = new Point(520, y + 20), Size = new Size(130, 24), Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };

            foreach (var l in new[] { lblCod, lblNom, lblIni, lblFin }) l.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // ---- Botones
            int yb = 410;
            btnAplicar = CrearBoton("APLICAR", 0, yb, true);
            btnAplicar.Click += (s, e) => Aplicar();

            btnLimpiar = CrearBoton("LIMPIAR", 140, yb, false);
            btnLimpiar.Click += (s, e) => Limpiar();

            btnActivar = CrearBoton("ACTIVAR", 280, yb, false);
            btnActivar.Click += btnActivar_Click;
            btnActivar.Visible = _bll.PuedeActivar();

            btnSalir = CrearBoton("SALIR", 420, yb, false);
            btnSalir.Click += (s, e) => Close();

            card.Controls.AddRange(new Control[] {
                lblCod, cmbCodigo, lblNom, cmbNombre, lblIni, dtIni, lblFin, dtFin,
                btnAplicar, btnLimpiar, btnActivar, btnSalir
            });
        }

        private Button CrearBoton(string texto, int x, int y, bool primario)
        {
            var b = new Button { Text = texto, Location = new Point(x, y), Size = new Size(130, 34), Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            if (primario) Tema_GV42.EstilizarBotonPrimario(b); else Tema_GV42.EstilizarBotonSecundario(b);
            return b;
        }

        // El registro activo (Act = 1) se resalta; los demás son el historial.
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgv.Rows[e.RowIndex].DataBoundItem as FilaCambio;
            if (fila != null && fila.Act == 1)
            {
                e.CellStyle.BackColor = Color.FromArgb(232, 245, 233);
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void CargarCombos()
        {
            try
            {
                string codSel = cmbCodigo.SelectedItem as string;
                string nomSel = cmbNombre.SelectedItem as string;

                cmbCodigo.Items.Clear();
                cmbCodigo.Items.Add(TODOS);
                foreach (string c in _bll.ListarCodigos()) cmbCodigo.Items.Add(c);
                cmbCodigo.SelectedItem = codSel != null && cmbCodigo.Items.Contains(codSel) ? codSel : TODOS;

                cmbNombre.Items.Clear();
                cmbNombre.Items.Add(TODOS);
                foreach (string n in _bll.ListarNombres()) cmbNombre.Items.Add(n);
                cmbNombre.SelectedItem = nomSel != null && cmbNombre.Items.Contains(nomSel) ? nomSel : TODOS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudieron cargar los filtros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Aplicar()
        {
            try
            {
                string codigo = cmbCodigo.SelectedIndex > 0 ? (string)cmbCodigo.SelectedItem : null;
                string nombre = cmbNombre.SelectedIndex > 0 ? (string)cmbNombre.SelectedItem : null;
                DateTime? ini = dtIni.Checked ? (DateTime?)dtIni.Value.Date : null;
                DateTime? fin = dtFin.Checked ? (DateTime?)dtFin.Value.Date : null;

                List<VueloCambio_GV42> cambios = _bll.Consultar(codigo, nombre, ini, fin);

                var filas = cambios.Select(c => new FilaCambio
                {
                    CodigoVuelo = c.CodigoVuelo,
                    Fecha = c.Fecha,
                    Hora = c.Hora.ToString(@"hh\:mm"),
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    Act = c.Act ? 1 : 0,
                    Cambio = c
                }).ToList();

                dgv.DataSource = null;
                dgv.DataSource = filas;
                dgv.ClearSelection();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo consultar la bitácora", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            cmbCodigo.SelectedIndex = 0;
            cmbNombre.SelectedIndex = 0;
            dtIni.Checked = false;
            dtFin.Checked = false;
            Aplicar();
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná el registro que querés activar.", "Activar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            VueloCambio_GV42 sel = ((FilaCambio)dgv.SelectedRows[0].DataBoundItem).Cambio;
            if (sel.Act)
            {
                MessageBox.Show("Ese registro ya es el activo del vuelo " + sel.CodigoVuelo + ".", "Activar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string mensaje = "¿Activar el registro del " + sel.Fecha.ToString("dd/MM/yyyy") + " " + sel.Hora.ToString(@"hh\:mm") +
                             " del vuelo " + sel.CodigoVuelo + "?\n\n" + sel.Descripcion +
                             "\n\nLa tabla de vuelos va a quedar con esos datos y el registro activo actual dejará de serlo.";
            if (MessageBox.Show(mensaje, "Confirmar activación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _bll.ActivarVersion(sel);
                MessageBox.Show("Registro activado.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCombos();
                Aplicar();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "No se pudo activar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo activar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
