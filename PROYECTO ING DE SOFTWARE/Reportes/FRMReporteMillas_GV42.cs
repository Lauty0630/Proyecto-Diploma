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
    // Reporte inteligente (3ra entrega) – Gestión de Millas.
    // Muestra el perfil de fidelización de cada pasajero (categoría, millas, historial de vuelos,
    // proyección y millas por vencer) y, al elegir un pasajero, la oferta que recomienda el sistema.
    // Se filtra por período, categoría y pasajero, y se exporta a PDF.
    // El diseño está en FRMReporteMillas_GV42.Designer.cs (Form Designer).
    public partial class FRMReporteMillas_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLReporteMillas_GV42 _bll = new BLLReporteMillas_GV42();
        private readonly bool _puedeExportar;
        private readonly Font _fuenteCategoria = new Font("Segoe UI", 9F, FontStyle.Bold);

        private List<ReporteMillas_GV42> _resultado = new List<ReporteMillas_GV42>();
        private FiltroReporteMillas_GV42 _filtroAplicado = new FiltroReporteMillas_GV42();

        #endregion

        #region Constructor

        public FRMReporteMillas_GV42()
        {
            _puedeExportar = _bll.PuedeExportar();

            InitializeComponent();
            dgvReporte.AutoGenerateColumns = false;
            colCategoria.DefaultCellStyle.Font = _fuenteCategoria;
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
            Text = IdiomaManager_GV42.T("repMillas.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("repMillas.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("repMillas.subtitulo");
            lblResumenTitulo.Text = IdiomaManager_GV42.T("repMillas.perfil");
            lblAyuda.Text = IdiomaManager_GV42.T("repMillas.ayuda");

            lblPeriodo.Text = IdiomaManager_GV42.T("repMillas.periodo");
            lblCategoria.Text = IdiomaManager_GV42.T("repMillas.categoria");
            lblPasajero.Text = IdiomaManager_GV42.T("reporte.pasajero");

            btnAplicar.Text = IdiomaManager_GV42.T("reporte.aplicar");
            btnLimpiar.Text = IdiomaManager_GV42.T("reporte.limpiar");
            btnExportar.Text = IdiomaManager_GV42.T("reporte.exportar");

            toolTip1.SetToolTip(btnAplicar, IdiomaManager_GV42.T("reporte.tipAplicar"));
            toolTip1.SetToolTip(btnLimpiar, IdiomaManager_GV42.T("repMillas.tipLimpiar"));
            toolTip1.SetToolTip(btnExportar, IdiomaManager_GV42.T("repMillas.tipExportar"));
            toolTip1.SetToolTip(txtPasajero, IdiomaManager_GV42.T("repCheckin.tipPasajero"));

            colCuenta.HeaderText = IdiomaManager_GV42.T("repMillas.colCuenta");
            colPasajero.HeaderText = IdiomaManager_GV42.T("repCheckin.colPasajero");
            colDni.HeaderText = IdiomaManager_GV42.T("reporte.colDni");
            colCategoria.HeaderText = IdiomaManager_GV42.T("repMillas.categoria");
            colMillasTotales.HeaderText = IdiomaManager_GV42.T("repMillas.colTotales");
            colMillasPeriodo.HeaderText = IdiomaManager_GV42.T("repMillas.colPeriodo");
            colVuelosPeriodo.HeaderText = IdiomaManager_GV42.T("repMillas.colVuelos");
            colFaltan.HeaderText = IdiomaManager_GV42.T("repMillas.colFaltan");
            colProyeccion.HeaderText = IdiomaManager_GV42.T("repMillas.colProyeccion");
            colFechaEstimada.HeaderText = IdiomaManager_GV42.T("repMillas.colFechaEstimada");
            colVencen30.HeaderText = IdiomaManager_GV42.T("repMillas.colVencen30");
            colVencen60.HeaderText = IdiomaManager_GV42.T("repMillas.colVencen60");

            CargarOpcionesFiltros();

            // Con datos cargados se vuelven a armar las recomendaciones y los textos en el idioma nuevo.
            if (dgvReporte.DataSource != null) Aplicar();
            else MostrarTituloLista();
        }

        // Arma (o vuelve a armar, al cambiar el idioma) los ítems de los combos conservando lo elegido.
        private void CargarOpcionesFiltros()
        {
            var periodos = new List<object>();
            foreach (PeriodoMillas_GV42 p in Enum.GetValues(typeof(PeriodoMillas_GV42)))
                periodos.Add(new Opcion<PeriodoMillas_GV42> { Texto = BLLReporteMillas_GV42.TextoPeriodo(p), Valor = p });
            ReemplazarItems(cboPeriodo, periodos.ToArray(), 1);

            var categorias = new List<object> { new Opcion<CategoriaMillas_GV42?> { Texto = IdiomaManager_GV42.T("reporte.todas"), Valor = null } };
            foreach (CategoriaMillas_GV42 c in Enum.GetValues(typeof(CategoriaMillas_GV42)))
                categorias.Add(new Opcion<CategoriaMillas_GV42?> { Texto = BLLReporteMillas_GV42.TextoCategoria(c), Valor = c });
            ReemplazarItems(cboCategoria, categorias.ToArray(), 0);
        }

        private static void ReemplazarItems(ComboBox combo, object[] items, int porDefecto)
        {
            int seleccionado = combo.SelectedIndex;
            combo.BeginUpdate();
            combo.Items.Clear();
            combo.Items.AddRange(items);
            combo.SelectedIndex = seleccionado >= 0 && seleccionado < items.Length ? seleccionado : porDefecto;
            combo.EndUpdate();
        }

        #endregion

        #region Filtros

        private void LimpiarFiltros()
        {
            cboPeriodo.SelectedIndex = 1;      // Trimestral
            cboCategoria.SelectedIndex = 0;    // Todas
            txtPasajero.Clear();
        }

        private FiltroReporteMillas_GV42 LeerFiltro()
        {
            return new FiltroReporteMillas_GV42
            {
                Periodo = ((Opcion<PeriodoMillas_GV42>)cboPeriodo.SelectedItem).Valor,
                Categoria = ((Opcion<CategoriaMillas_GV42?>)cboCategoria.SelectedItem).Valor,
                Pasajero = txtPasajero.Text
            };
        }

        #endregion

        #region Generación del reporte

        private void Aplicar()
        {
            try
            {
                FiltroReporteMillas_GV42 filtro = LeerFiltro();
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
                Cuenta = r.NumeroCuenta,
                Pasajero = r.NombreCompleto,
                Dni = r.Dni,
                Categoria = BLLReporteMillas_GV42.TextoCategoria(r.Categoria),
                MillasTotales = r.MillasTotales,
                MillasPeriodo = r.MillasPeriodo,
                VuelosPeriodo = r.VuelosPeriodo,
                Faltan = BLLReporteMillas_GV42.TextoFaltan(r),
                Proyeccion = r.ProyeccionMillas90Dias,
                FechaEstimada = BLLReporteMillas_GV42.TextoFechaEstimada(r),
                Vencen30 = r.MillasVencen30Dias,
                Vencen60 = r.MillasVencen60Dias,
                Perfil = r
            }).ToList();

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = filas;
            dgvReporte.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            MostrarTituloLista();
            MostrarPerfilSeleccionado();
            btnExportar.Enabled = _resultado.Count > 0;
        }

        // Título de la grilla con los totales (los mismos que van al pie del PDF).
        private void MostrarTituloLista()
        {
            string titulo = IdiomaManager_GV42.T("repMillas.seccionLista");
            lblSeccionLista.Text = dgvReporte.DataSource == null ? titulo : titulo + "   ·   " + _bll.Resumen(_resultado)[0];
        }

        // Perfil completo y oferta recomendada del pasajero elegido en la grilla.
        private void MostrarPerfilSeleccionado()
        {
            var fila = dgvReporte.CurrentRow != null ? dgvReporte.CurrentRow.DataBoundItem as FilaReporte : null;
            lblResumen.Text = fila != null
                ? BLLReporteMillas_GV42.Detalle(fila.Perfil)
                : IdiomaManager_GV42.T(_resultado.Count == 0 ? "repMillas.sinPasajeros" : "repMillas.elegirPasajero");
        }

        #endregion

        #region Eventos

        private void FRMReporteMillas_GV42_Load(object sender, EventArgs e)
        {
            Aplicar();
        }

        // Enter en el cuadro de texto aplica los filtros.
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

        private void dgvReporte_SelectionChanged(object sender, EventArgs e)
        {
            MostrarPerfilSeleccionado();
        }

        // La categoría en color; las millas por vencer en rojo.
        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgvReporte.Rows[e.RowIndex].DataBoundItem as FilaReporte;
            if (fila == null) return;
            DataGridViewColumn columna = dgvReporte.Columns[e.ColumnIndex];

            if (columna == colCategoria)
                e.CellStyle.ForeColor = fila.Perfil.Categoria == CategoriaMillas_GV42.Bronce ? Tema_GV42.Advertencia
                                      : fila.Perfil.Categoria == CategoriaMillas_GV42.Plata ? Tema_GV42.TextoSecundario
                                      : Tema_GV42.Exito;
            else if ((columna == colVencen30 && fila.Vencen30 > 0) || (columna == colVencen60 && fila.Vencen60 > 0))
                e.CellStyle.ForeColor = Tema_GV42.Error;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_resultado.Count == 0)
            {
                MessageBox.Show(IdiomaManager_GV42.T("neg.millas.sinDatos"), IdiomaManager_GV42.T("general.informacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("reporte.sfdFiltro");
                sfd.Title = IdiomaManager_GV42.T("repMillas.sfdTitulo");
                sfd.FileName = $"{IdiomaManager_GV42.T("repMillas.archivo")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
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
            public string Cuenta { get; set; }
            public string Pasajero { get; set; }
            public string Dni { get; set; }
            public string Categoria { get; set; }
            public int MillasTotales { get; set; }
            public int MillasPeriodo { get; set; }
            public int VuelosPeriodo { get; set; }
            public string Faltan { get; set; }
            public int Proyeccion { get; set; }
            public string FechaEstimada { get; set; }
            public int Vencen30 { get; set; }
            public int Vencen60 { get; set; }
            public ReporteMillas_GV42 Perfil { get; set; }
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
