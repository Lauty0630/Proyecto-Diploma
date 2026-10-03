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
    // Reporte asociado al RFN 2 – Check-in / Tarjeta de embarque (personal de mostrador y Gerente).
    // Muestra, vuelo por vuelo, qué pasajeros hicieron el check-in, cuáles siguen pendientes y cuáles
    // quedaron ausentes, con su asiento y su tarjeta de embarque. Se filtra por vuelo, fecha de salida,
    // estado del check-in, asistencia y pasajero, y se exporta a PDF igual que el reporte de reservas.
    //  - Reportes.CheckIn: ver el reporte.
    //  - Reportes.CheckInExportarPDF: botón "Exportar PDF".
    // El diseño está en FRMReporteCheckIn_GV42.Designer.cs (Form Designer).
    public partial class FRMReporteCheckIn_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReporteCheckIn_GV42 _bll = new BLLReporteCheckIn_GV42();
        private readonly bool _puedeExportar;
        private readonly Font _fuenteEstado = new Font("Segoe UI", 9F, FontStyle.Bold);

        private List<ReporteCheckIn_GV42> _resultado = new List<ReporteCheckIn_GV42>();
        private FiltroReporteCheckIn_GV42 _filtroAplicado = new FiltroReporteCheckIn_GV42();

        #endregion

        #region Constructor

        public FRMReporteCheckIn_GV42()
        {
            _puedeExportar = _bll.PuedeExportar();

            InitializeComponent();
            // Son muchas columnas: se ajustan al contenido y la grilla se desplaza horizontalmente.
            dgvReporte.AutoGenerateColumns = false;
            // Los estados van en negrita: la fuente se define en la columna para que el ancho automático la tenga en cuenta.
            colAsistencia.DefaultCellStyle.Font = _fuenteEstado;
            colEstado.DefaultCellStyle.Font = _fuenteEstado;
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
            Text = IdiomaManager_GV42.T("repCheckin.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("repCheckin.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("repCheckin.subtitulo");
            lblSeccionLista.Text = IdiomaManager_GV42.T("repCheckin.seccionLista");
            lblResumenTitulo.Text = IdiomaManager_GV42.T("reporte.resumen");
            lblAyuda.Text = IdiomaManager_GV42.T("repCheckin.ayuda");

            chkFecha.Text = IdiomaManager_GV42.T("reporte.fechaSalida");
            lblDesde.Text = IdiomaManager_GV42.T("reporte.desde");
            lblHasta.Text = IdiomaManager_GV42.T("reporte.hasta");
            lblVuelo.Text = IdiomaManager_GV42.T("reporte.vuelo");
            lblEstado.Text = IdiomaManager_GV42.T("repCheckin.checkIn");
            lblAsistencia.Text = IdiomaManager_GV42.T("repCheckin.asistencia");
            lblPasajero.Text = IdiomaManager_GV42.T("reporte.pasajero");

            btnAplicar.Text = IdiomaManager_GV42.T("reporte.aplicar");
            btnLimpiar.Text = IdiomaManager_GV42.T("reporte.limpiar");
            btnExportar.Text = IdiomaManager_GV42.T("reporte.exportar");

            toolTip1.SetToolTip(btnAplicar, IdiomaManager_GV42.T("reporte.tipAplicar"));
            toolTip1.SetToolTip(btnLimpiar, IdiomaManager_GV42.T("repCheckin.tipLimpiar"));
            toolTip1.SetToolTip(btnExportar, IdiomaManager_GV42.T("repCheckin.tipExportar"));
            toolTip1.SetToolTip(txtPasajero, IdiomaManager_GV42.T("repCheckin.tipPasajero"));

            colVuelo.HeaderText = IdiomaManager_GV42.T("reporte.vuelo");
            colOrigen.HeaderText = IdiomaManager_GV42.T("reporte.colOrigen");
            colDestino.HeaderText = IdiomaManager_GV42.T("reporte.colDestino");
            colSalida.HeaderText = IdiomaManager_GV42.T("reporte.colSalida");
            colNumeroReserva.HeaderText = IdiomaManager_GV42.T("reporte.colNumero");
            colPasajero.HeaderText = IdiomaManager_GV42.T("repCheckin.colPasajero");
            colDni.HeaderText = IdiomaManager_GV42.T("reporte.colDni");
            colAsistencia.HeaderText = IdiomaManager_GV42.T("repCheckin.asistencia");
            colEstado.HeaderText = IdiomaManager_GV42.T("repCheckin.checkIn");
            colFechaCheckIn.HeaderText = IdiomaManager_GV42.T("repCheckin.colFechaCheckIn");
            colAsiento.HeaderText = IdiomaManager_GV42.T("repCheckin.colAsiento");
            colClase.HeaderText = IdiomaManager_GV42.T("reporte.clase");
            colUbicacion.HeaderText = IdiomaManager_GV42.T("repCheckin.colUbicacion");
            colTarjeta.HeaderText = IdiomaManager_GV42.T("repCheckin.colTarjeta");
            colPuerta.HeaderText = IdiomaManager_GV42.T("repCheckin.colPuerta");
            colHoraLimite.HeaderText = IdiomaManager_GV42.T("repCheckin.colHoraLimite");

            CargarOpcionesFiltros();

            // Con datos cargados se regeneran la grilla (clase, estados) y el resumen en el idioma nuevo,
            // sin volver a consultar la base.
            if (dgvReporte.DataSource != null) MostrarResultado();
        }

        // Arma (o vuelve a armar, al cambiar el idioma) los ítems de los combos de filtros
        // conservando la opción elegida.
        private void CargarOpcionesFiltros()
        {
            var estados = new List<object> { new Opcion<EstadoCheckIn_GV42?> { Texto = IdiomaManager_GV42.T("reporte.todos"), Valor = null } };
            foreach (EstadoCheckIn_GV42 e in Enum.GetValues(typeof(EstadoCheckIn_GV42)))
                estados.Add(new Opcion<EstadoCheckIn_GV42?> { Texto = e.Texto(), Valor = e });
            ReemplazarItems(cboEstado, estados.ToArray());

            ReemplazarItems(cboAsistencia, new object[]
            {
                new Opcion<EstadoAsistencia_GV42?> { Texto = IdiomaManager_GV42.T("reporte.todas"), Valor = null },
                new Opcion<EstadoAsistencia_GV42?> { Texto = EstadoAsistencia_GV42.Presente.Texto(), Valor = EstadoAsistencia_GV42.Presente },
                new Opcion<EstadoAsistencia_GV42?> { Texto = EstadoAsistencia_GV42.Ausente.Texto(), Valor = EstadoAsistencia_GV42.Ausente },
                new Opcion<EstadoAsistencia_GV42?> { Texto = EstadoAsistencia_GV42.AConfirmar.Texto(), Valor = EstadoAsistencia_GV42.AConfirmar }
            });
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
            dtpDesde.Enabled = dtpHasta.Enabled = chkFecha.Checked;
        }

        private void LimpiarFiltros()
        {
            chkFecha.Checked = false;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today.AddDays(2);
            txtVuelo.Clear();
            cboEstado.SelectedIndex = 0;
            cboAsistencia.SelectedIndex = 0;
            txtPasajero.Clear();
            HabilitarFechas();
        }

        private FiltroReporteCheckIn_GV42 LeerFiltro()
        {
            var f = new FiltroReporteCheckIn_GV42
            {
                CodigoVuelo = txtVuelo.Text,
                Pasajero = txtPasajero.Text,
                Estado = ((Opcion<EstadoCheckIn_GV42?>)cboEstado.SelectedItem).Valor,
                Asistencia = ((Opcion<EstadoAsistencia_GV42?>)cboAsistencia.SelectedItem).Valor
            };
            if (chkFecha.Checked)
            {
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
                FiltroReporteCheckIn_GV42 filtro = LeerFiltro();
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
                Vuelo = r.CodigoVuelo,
                Origen = r.Origen,
                Destino = r.Destino,
                Salida = r.FechaHoraSalida,
                NumeroReserva = r.NumeroReserva,
                Pasajero = r.PasajeroNombreCompleto,
                Dni = r.PasajeroDni,
                Asistencia = r.AsistenciaTexto,
                AsistenciaValor = r.Asistencia,
                Estado = r.EstadoTexto,
                EstadoValor = r.Estado,
                FechaCheckIn = r.FechaHoraCheckIn,
                Asiento = string.IsNullOrWhiteSpace(r.NumeroAsiento) ? "—" : r.NumeroAsiento,
                Clase = r.ClaseTexto,
                Ubicacion = string.IsNullOrWhiteSpace(r.Ubicacion) ? "—" : BLLReporteCheckIn_GV42.TextoUbicacion(r.Ubicacion),
                Tarjeta = string.IsNullOrWhiteSpace(r.NumeroTarjeta) ? "—" : r.NumeroTarjeta,
                Puerta = string.IsNullOrWhiteSpace(r.PuertaEmbarque) ? "—" : r.PuertaEmbarque,
                HoraLimite = r.HoraLimiteEmbarque
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

        private void FRMReporteCheckIn_GV42_Load(object sender, EventArgs e)
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

        // Asistencia y estado del check-in en color: verde lo resuelto, naranja lo pendiente, rojo el ausente.
        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewColumn columna = dgvReporte.Columns[e.ColumnIndex];
            if (columna != colAsistencia && columna != colEstado) return;
            var fila = dgvReporte.Rows[e.RowIndex].DataBoundItem as FilaReporte;
            if (fila == null) return;

            if (columna == colEstado)
                e.CellStyle.ForeColor = fila.EstadoValor == EstadoCheckIn_GV42.Realizado ? Tema_GV42.Exito : Tema_GV42.Advertencia;
            else
                e.CellStyle.ForeColor = fila.AsistenciaValor == EstadoAsistencia_GV42.Presente ? Tema_GV42.Exito
                                      : fila.AsistenciaValor == EstadoAsistencia_GV42.Ausente ? Tema_GV42.Error : Tema_GV42.Advertencia;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_resultado.Count == 0)
            {
                MessageBox.Show(IdiomaManager_GV42.T("neg.repCheckin.sinDatos"), IdiomaManager_GV42.T("general.informacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("reporte.sfdFiltro");
                sfd.Title = IdiomaManager_GV42.T("repCheckin.sfdTitulo");
                sfd.FileName = $"{IdiomaManager_GV42.T("repCheckin.archivo")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // Se exporta exactamente lo que se ve (los filtros aplicados con el último "Aplicar").
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
            public string Vuelo { get; set; }
            public string Origen { get; set; }
            public string Destino { get; set; }
            public DateTime Salida { get; set; }
            public string NumeroReserva { get; set; }
            public string Pasajero { get; set; }
            public string Dni { get; set; }
            public string Asistencia { get; set; }
            public EstadoAsistencia_GV42 AsistenciaValor { get; set; }
            public string Estado { get; set; }
            public EstadoCheckIn_GV42 EstadoValor { get; set; }
            public DateTime? FechaCheckIn { get; set; }
            public string Asiento { get; set; }
            public string Clase { get; set; }
            public string Ubicacion { get; set; }
            public string Tarjeta { get; set; }
            public string Puerta { get; set; }
            public DateTime HoraLimite { get; set; }
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
