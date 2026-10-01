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
    // Reporte asociado al RFN 1 – Reserva de vuelo (rol Gerente).
    // Consolida todas las reservas y permite filtrarlas por fecha (de reserva o de salida), vuelo,
    // clase, estado y pasajero. Se exporta a PDF igual que la bitácora de eventos.
    //  - Reportes.Reservas: ver el reporte.
    //  - Reportes.ReservasExportarPDF: botón "Exportar PDF".
    // El diseño está en FRMReporteReservas_GV42.Designer.cs (Form Designer).
    public partial class FRMReporteReservas_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReporteReserva_GV42 _bll = new BLLReporteReserva_GV42();
        private readonly bool _puedeExportar;
        private readonly Font _fuenteEstado = new Font("Segoe UI", 9F, FontStyle.Bold);

        private List<ReporteReserva_GV42> _resultado = new List<ReporteReserva_GV42>();
        private FiltroReporteReservas_GV42 _filtroAplicado = new FiltroReporteReservas_GV42();

        #endregion

        #region Constructor

        public FRMReporteReservas_GV42()
        {
            _puedeExportar = _bll.PuedeExportar();

            InitializeComponent();
            // Son muchas columnas: se ajustan al contenido y la grilla se desplaza horizontalmente.
            dgvReporte.AutoGenerateColumns = false;
            btnExportar.Visible = _puedeExportar;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            LimpiarFiltros();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("reporte.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("reporte.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("reporte.subtitulo");
            lblSeccionLista.Text = IdiomaManager_GV42.T("reporte.seccionLista");
            lblResumenTitulo.Text = IdiomaManager_GV42.T("reporte.resumen");
            lblAyuda.Text = IdiomaManager_GV42.T("reporte.ayuda");

            chkFecha.Text = IdiomaManager_GV42.T("reporte.filtrarPor");
            lblDesde.Text = IdiomaManager_GV42.T("reporte.desde");
            lblHasta.Text = IdiomaManager_GV42.T("reporte.hasta");
            lblVuelo.Text = IdiomaManager_GV42.T("reporte.vuelo");
            lblClase.Text = IdiomaManager_GV42.T("reporte.clase");
            lblEstado.Text = IdiomaManager_GV42.T("reporte.estado");
            lblPasajero.Text = IdiomaManager_GV42.T("reporte.pasajero");

            btnAplicar.Text = IdiomaManager_GV42.T("reporte.aplicar");
            btnLimpiar.Text = IdiomaManager_GV42.T("reporte.limpiar");
            btnExportar.Text = IdiomaManager_GV42.T("reporte.exportar");

            toolTip1.SetToolTip(btnAplicar, IdiomaManager_GV42.T("reporte.tipAplicar"));
            toolTip1.SetToolTip(btnLimpiar, IdiomaManager_GV42.T("reporte.tipLimpiar"));
            toolTip1.SetToolTip(btnExportar, IdiomaManager_GV42.T("reporte.tipExportar"));
            toolTip1.SetToolTip(txtPasajero, IdiomaManager_GV42.T("reporte.tipPasajero"));

            colNumeroReserva.HeaderText = IdiomaManager_GV42.T("reporte.colNumero");
            colFechaReserva.HeaderText = IdiomaManager_GV42.T("reporte.colFechaReserva");
            colPasajero.HeaderText = IdiomaManager_GV42.T("reporte.colTitular");
            colDni.HeaderText = IdiomaManager_GV42.T("reporte.colDni");
            colEmail.HeaderText = IdiomaManager_GV42.T("reporte.colEmail");
            colTelefono.HeaderText = IdiomaManager_GV42.T("reporte.colTelefono");
            colVuelo.HeaderText = IdiomaManager_GV42.T("reporte.vuelo");
            colOrigen.HeaderText = IdiomaManager_GV42.T("reporte.colOrigen");
            colDestino.HeaderText = IdiomaManager_GV42.T("reporte.colDestino");
            colSalida.HeaderText = IdiomaManager_GV42.T("reporte.colSalida");
            colLlegada.HeaderText = IdiomaManager_GV42.T("reporte.colLlegada");
            colClase.HeaderText = IdiomaManager_GV42.T("reporte.clase");
            colPasajeros.HeaderText = IdiomaManager_GV42.T("reporte.colPax");
            colAdicionales.HeaderText = IdiomaManager_GV42.T("reporte.colAdicionales");
            colImporteBase.HeaderText = IdiomaManager_GV42.T("reporte.colImporteBase");
            colImpuestos.HeaderText = IdiomaManager_GV42.T("reporte.colImpuestos");
            colImporteTotal.HeaderText = IdiomaManager_GV42.T("reporte.colTotal");
            colEstado.HeaderText = IdiomaManager_GV42.T("reporte.estado");

            CargarOpcionesFiltros();

            // Con datos cargados se regeneran la grilla (clase, estado) y el resumen en el idioma nuevo,
            // sin volver a consultar la base.
            if (dgvReporte.DataSource != null) MostrarResultado();
        }

        // Arma (o vuelve a armar, al cambiar el idioma) los ítems de los combos de filtros
        // conservando la opción elegida.
        private void CargarOpcionesFiltros()
        {
            ReemplazarItems(cboTipoFecha, new object[]
            {
                new Opcion<FechaReporte_GV42> { Texto = IdiomaManager_GV42.T("reporte.fechaReserva"), Valor = FechaReporte_GV42.Realizacion },
                new Opcion<FechaReporte_GV42> { Texto = IdiomaManager_GV42.T("reporte.fechaSalida"), Valor = FechaReporte_GV42.Salida }
            });

            var clases = new List<object> { new Opcion<ClaseVuelo_GV42?> { Texto = IdiomaManager_GV42.T("reporte.todas"), Valor = null } };
            foreach (ClaseVuelo_GV42 c in Enum.GetValues(typeof(ClaseVuelo_GV42)))
                clases.Add(new Opcion<ClaseVuelo_GV42?> { Texto = c.Texto(), Valor = c });
            ReemplazarItems(cboClase, clases.ToArray());

            var estados = new List<object> { new Opcion<EstadoReserva_GV42?> { Texto = IdiomaManager_GV42.T("reporte.todos"), Valor = null } };
            foreach (EstadoReserva_GV42 e in Enum.GetValues(typeof(EstadoReserva_GV42)))
                estados.Add(new Opcion<EstadoReserva_GV42?> { Texto = e.Texto(), Valor = e });
            ReemplazarItems(cboEstado, estados.ToArray());
        }

        private static void ReemplazarItems(ComboBox combo, object[] items)
        {
            int seleccionado = combo.SelectedIndex;
            combo.BeginUpdate();
            combo.Items.Clear();
            combo.Items.AddRange(items);
            combo.SelectedIndex = seleccionado >= 0 && seleccionado < items.Length ? seleccionado : 0;
            combo.EndUpdate();
        }

        #endregion

        #region Filtros

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

        #endregion

        #region Generación del reporte

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
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("reporte.revisarFiltros"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(IdiomaManager_GV42.T("reporte.errorGenerar", ex.Message), IdiomaManager_GV42.T("general.error"),
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

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = filas;
            // Al regenerar (por ejemplo, al cambiar de idioma) se recalcula el ancho de cada columna.
            dgvReporte.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Los renglones del resumen los arma la BLL (los mismos que van al pie del PDF).
            lblResumen.Text = string.Join(Environment.NewLine, _bll.Resumen(_resultado));
            btnExportar.Enabled = _resultado.Count > 0;
        }

        #endregion

        #region Eventos

        private void FRMReporteReservas_GV42_Load(object sender, EventArgs e)
        {
            Aplicar();
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarFechas();
        }

        // Enter en los cuadros de texto aplica los filtros.
        private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            Aplicar();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Aplicar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
            Aplicar();
        }

        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvReporte.Columns[e.ColumnIndex] != colEstado) return;
            var fila = dgvReporte.Rows[e.RowIndex].DataBoundItem as FilaReporte;
            if (fila == null) return;

            e.CellStyle.Font = _fuenteEstado;
            switch (fila.EstadoValor)
            {
                case EstadoReserva_GV42.Confirmada: e.CellStyle.ForeColor = Tema_GV42.Exito; break;
                case EstadoReserva_GV42.PendienteDePago: e.CellStyle.ForeColor = Tema_GV42.Advertencia; break;
                default: e.CellStyle.ForeColor = Tema_GV42.Error; break;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_resultado.Count == 0)
            {
                MessageBox.Show(IdiomaManager_GV42.T("reporte.sinDatos"), IdiomaManager_GV42.T("general.informacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("reporte.sfdFiltro");
                sfd.Title = IdiomaManager_GV42.T("reporte.sfdTitulo");
                sfd.FileName = $"{IdiomaManager_GV42.T("reporte.archivo")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // Se exporta exactamente lo que se ve (los filtros aplicados con el último "Aplicar").
                    // Los textos del PDF (título, encabezados, resumen) los arma la BLL.
                    _bll.ExportarPdf(sfd.FileName, _resultado, _filtroAplicado);

                    if (MessageBox.Show(IdiomaManager_GV42.T("reporte.pdfGenerado", sfd.FileName), IdiomaManager_GV42.T("general.exito"),
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_GV42.T("reporte.errorExportar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager_GV42.T("reporte.errorPdf", ex.Message), IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Tipos anidados

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

        #endregion
    }
}
