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
    // vuelo, con cada atributo en su columna, filtrable por código, nombre (ruta) y rango de fechas.
    // El registro con Act = 1 es el que hoy está vigente en la tabla Vuelo; ACTIVAR asigna Act = 1 al
    // registro elegido y el trigger de la base deja la tabla Vuelo con esos datos.
    // El diseño está en FRMBitacoraVuelos_GV42.Designer.cs (Form Designer).
    public partial class FRMBitacoraVuelos_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLVueloHistorial_GV42 _bll = new BLLVueloHistorial_GV42();

        // Fuentes y colores del resaltado del registro activo (se crean una sola vez, no por celda).
        private readonly Font _fuenteActivo = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Color _fondoActivo = Color.FromArgb(232, 245, 233);

        #endregion

        #region Constructor

        public FRMBitacoraVuelos_GV42()
        {
            InitializeComponent();
            dgvCambios.AutoGenerateColumns = false;
            btnActivar.Visible = _bll.PuedeActivar();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            CargarCombos();
            FiltrosDeArranque();
            Aplicar();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("bitVuelos.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("bitVuelos.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("bitVuelos.subtitulo");
            lblSeccionLista.Text = IdiomaManager_GV42.T("bitVuelos.seccionLista");

            lblCodigo.Text = IdiomaManager_GV42.T("bitVuelos.codigo");
            lblNombre.Text = IdiomaManager_GV42.T("bitVuelos.nombreRuta");
            lblFechaIni.Text = IdiomaManager_GV42.T("bitVuelos.fechaIni");
            lblFechaFin.Text = IdiomaManager_GV42.T("bitVuelos.fechaFin");
            chkSoloCambios.Text = IdiomaManager_GV42.T("bitVuelos.soloCambios");
            lblAyuda.Text = IdiomaManager_GV42.T("bitVuelos.ayuda");

            btnAplicar.Text = IdiomaManager_GV42.T("bitVuelos.aplicar");
            btnLimpiar.Text = IdiomaManager_GV42.T("bitVuelos.limpiar");
            btnActivar.Text = IdiomaManager_GV42.T("bitVuelos.activar");
            btnSalir.Text = IdiomaManager_GV42.T("general.salir");

            colCodigoVuelo.HeaderText = IdiomaManager_GV42.T("bitVuelos.codigo");
            colFecha.HeaderText = IdiomaManager_GV42.T("bitVuelos.colFecha");
            colHora.HeaderText = IdiomaManager_GV42.T("bitVuelos.colHora");
            colNombre.HeaderText = IdiomaManager_GV42.T("bitVuelos.colNombre");
            colAerolinea.HeaderText = IdiomaManager_GV42.T("vuelos.aerolinea");
            colSalida.HeaderText = IdiomaManager_GV42.T("vuelos.salida");
            colLlegada.HeaderText = IdiomaManager_GV42.T("vuelos.llegada");
            colPuerta.HeaderText = IdiomaManager_GV42.T("vuelos.puerta");
            colCostoKilo.HeaderText = IdiomaManager_GV42.T("vuelos.colCostoKilo");
            colBaja.HeaderText = IdiomaManager_GV42.T("bitVuelos.colBaja");
            colAct.HeaderText = IdiomaManager_GV42.T("bitVuelos.colAct");

            // Con datos cargados se traduce el Sí/No de cada fila sin volver a consultar la base.
            var filas = dgvCambios.DataSource as List<FilaCambio>;
            if (filas != null)
                foreach (FilaCambio fila in filas) fila.Baja = TextoBaja(fila.Cambio);
            dgvCambios.Invalidate();

            // El primer ítem de cada combo es "(Todos)": se traduce sin perder lo elegido.
            TraducirOpcionTodos(cmbCodigo);
            TraducirOpcionTodos(cmbNombre);
        }

        private static void TraducirOpcionTodos(ComboBox combo)
        {
            if (combo.Items.Count == 0) return;
            int seleccionado = combo.SelectedIndex;
            combo.Items[0] = IdiomaManager_GV42.T("bitVuelos.todos");
            combo.SelectedIndex = seleccionado;
        }

        #endregion

        #region Carga de datos

        private void CargarCombos()
        {
            try
            {
                // El índice 0 es siempre "(Todos)"; se recuerda lo elegido para volver a seleccionarlo.
                string codSel = cmbCodigo.SelectedIndex > 0 ? cmbCodigo.SelectedItem as string : null;
                string nomSel = cmbNombre.SelectedIndex > 0 ? cmbNombre.SelectedItem as string : null;

                cmbCodigo.Items.Clear();
                cmbCodigo.Items.Add(IdiomaManager_GV42.T("bitVuelos.todos"));
                cmbCodigo.SelectedIndex = 0;
                foreach (string c in _bll.ListarCodigos()) cmbCodigo.Items.Add(c);
                int iCod = codSel != null ? cmbCodigo.Items.IndexOf(codSel) : -1;
                cmbCodigo.SelectedIndex = iCod > 0 ? iCod : 0;

                cmbNombre.Items.Clear();
                cmbNombre.Items.Add(IdiomaManager_GV42.T("bitVuelos.todos"));
                cmbNombre.SelectedIndex = 0;
                foreach (string n in _bll.ListarNombres()) cmbNombre.Items.Add(n);
                int iNom = nomSel != null ? cmbNombre.Items.IndexOf(nomSel) : -1;
                cmbNombre.SelectedIndex = iNom > 0 ? iNom : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("bitVuelos.errorFiltros"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Búsqueda

        private void Aplicar()
        {
            try
            {
                string codigo = cmbCodigo.SelectedIndex > 0 ? (string)cmbCodigo.SelectedItem : null;
                string nombre = cmbNombre.SelectedIndex > 0 ? (string)cmbNombre.SelectedItem : null;
                DateTime? ini = dtIni.Checked ? (DateTime?)dtIni.Value.Date : null;
                DateTime? fin = dtFin.Checked ? (DateTime?)dtFin.Value.Date : null;

                List<VueloCambio_GV42> cambios = _bll.Consultar(codigo, nombre, ini, fin, chkSoloCambios.Checked);

                var filas = cambios.Select(c => new FilaCambio
                {
                    CodigoVuelo = c.CodigoVuelo,
                    Fecha = c.Fecha,
                    Hora = c.Hora.ToString(@"hh\:mm"),
                    Nombre = c.Nombre,
                    Aerolinea = c.Aerolinea,
                    Salida = c.FechaHoraSalida,
                    Llegada = c.FechaHoraLlegada,
                    Puerta = c.PuertaEmbarque,
                    CostoKilo = c.CostoKiloExceso,
                    Baja = TextoBaja(c),
                    Act = c.Act ? 1 : 0,
                    Cambio = c
                }).ToList();

                dgvCambios.DataSource = null;
                dgvCambios.DataSource = filas;
                dgvCambios.ClearSelection();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("bitVuelos.errorConsultar"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Borrado lógico de esa versión del vuelo.
        private static string TextoBaja(VueloCambio_GV42 c)
        {
            return IdiomaManager_GV42.T(c.BorradoLogico ? "general.si" : "general.no");
        }

        private void Limpiar()
        {
            FiltrosDeArranque();
            Aplicar();
        }

        // Estado inicial de la pantalla: sin filtro de código ni de nombre y con los cambios
        // del último mes. Es el mismo estado al que vuelve el botón LIMPIAR.
        private void FiltrosDeArranque()
        {
            if (cmbCodigo.Items.Count > 0) cmbCodigo.SelectedIndex = 0;
            if (cmbNombre.Items.Count > 0) cmbNombre.SelectedIndex = 0;
            dtIni.Value = DateTime.Today.AddMonths(-1);
            dtIni.Checked = true;
            dtFin.Value = DateTime.Today;
            dtFin.Checked = true;
            chkSoloCambios.Checked = false;
        }

        #endregion

        #region Eventos

        // El registro activo (Act = 1) se resalta; los demás son el historial.
        private void dgvCambios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgvCambios.Rows[e.RowIndex].DataBoundItem as FilaCambio;
            if (fila != null && fila.Act == 1)
            {
                e.CellStyle.BackColor = _fondoActivo;
                e.CellStyle.Font = _fuenteActivo;
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Aplicar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvCambios.SelectedRows.Count == 0)
            {
                MessageBox.Show(IdiomaManager_GV42.T("bitVuelos.seleccione"), IdiomaManager_GV42.T("bitVuelos.activar"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            VueloCambio_GV42 sel = ((FilaCambio)dgvCambios.SelectedRows[0].DataBoundItem).Cambio;
            if (sel.Act)
            {
                MessageBox.Show(IdiomaManager_GV42.T("bitVuelos.yaActivo", sel.CodigoVuelo), IdiomaManager_GV42.T("bitVuelos.activar"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string mensaje = IdiomaManager_GV42.T("bitVuelos.confirmarActivar",
                sel.Fecha.ToString("dd/MM/yyyy"), sel.Hora.ToString(@"hh\:mm"), sel.CodigoVuelo, sel.Descripcion);
            if (MessageBox.Show(mensaje, IdiomaManager_GV42.T("bitVuelos.confirmarActivarTitulo"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _bll.ActivarVersion(sel);
                MessageBox.Show(IdiomaManager_GV42.T("bitVuelos.activado"), IdiomaManager_GV42.T("bitVuelos.listo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCombos();
                Aplicar();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("bitVuelos.errorActivar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("bitVuelos.errorActivar"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Tipos anidados

        private class FilaCambio
        {
            public string CodigoVuelo { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Nombre { get; set; }
            public string Aerolinea { get; set; }
            public DateTime Salida { get; set; }
            public DateTime Llegada { get; set; }
            public string Puerta { get; set; }
            public decimal CostoKilo { get; set; }
            public string Baja { get; set; }
            public int Act { get; set; }
            public VueloCambio_GV42 Cambio { get; set; }
        }

        #endregion
    }
}
