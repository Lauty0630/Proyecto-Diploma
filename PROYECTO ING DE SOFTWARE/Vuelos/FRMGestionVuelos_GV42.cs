using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Gestión de vuelos: modificar los datos de un vuelo existente y darlo de baja / reactivarlo
    // (borrado lógico). Cada cambio queda registrado solo en Vuelo_C por el trigger de la base;
    // se consulta desde "Bitácora de cambios". El diseño está en FRMGestionVuelos_GV42.Designer.cs.
    public partial class FRMGestionVuelos_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLVuelo_GV42 _bll = new BLLVuelo_GV42();

        private Vuelo_GV42 _seleccionado;

        #endregion

        #region Constructor

        public FRMGestionVuelos_GV42()
        {
            InitializeComponent();
            dgvVuelos.AutoGenerateColumns = false;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            CargarCatalogos();
            CargarVuelos(0);
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("vuelos.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("vuelos.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("vuelos.subtitulo");
            lblSeccionVuelos.Text = IdiomaManager_GV42.T("vuelos.seccionLista");
            lblSeccionEditor.Text = IdiomaManager_GV42.T("vuelos.seccionEditor");

            lblCodigo.Text = IdiomaManager_GV42.T("vuelos.codigo");
            lblAerolinea.Text = IdiomaManager_GV42.T("vuelos.aerolinea");
            lblOrigen.Text = IdiomaManager_GV42.T("vuelos.origen");
            lblDestino.Text = IdiomaManager_GV42.T("vuelos.destino");
            lblSalida.Text = IdiomaManager_GV42.T("vuelos.salida");
            lblLlegada.Text = IdiomaManager_GV42.T("vuelos.llegada");
            lblPuerta.Text = IdiomaManager_GV42.T("vuelos.puerta");
            lblCosto.Text = IdiomaManager_GV42.T("vuelos.costoKilo");
            lblAyuda.Text = IdiomaManager_GV42.T("vuelos.ayuda");

            btnGuardar.Text = IdiomaManager_GV42.T("vuelos.guardar");
            btnBaja.Text = IdiomaManager_GV42.T("vuelos.baja");
            btnReactivar.Text = IdiomaManager_GV42.T("vuelos.reactivar");

            colCodigo.HeaderText = IdiomaManager_GV42.T("vuelos.colVuelo");
            colAerolinea.HeaderText = IdiomaManager_GV42.T("vuelos.aerolinea");
            colRuta.HeaderText = IdiomaManager_GV42.T("vuelos.colRuta");
            colSalida.HeaderText = IdiomaManager_GV42.T("vuelos.salida");
            colLlegada.HeaderText = IdiomaManager_GV42.T("vuelos.llegada");
            colPuerta.HeaderText = IdiomaManager_GV42.T("vuelos.puerta");
            colCostoKilo.HeaderText = IdiomaManager_GV42.T("vuelos.colCostoKilo");
            colEstado.HeaderText = IdiomaManager_GV42.T("vuelos.colEstado");

            // Si ya hay vuelos cargados, se traduce el estado de cada fila sin volver a la base
            // (así no se pierde la selección ni lo que se esté editando).
            var filas = dgvVuelos.DataSource as List<FilaVuelo>;
            if (filas != null)
            {
                foreach (FilaVuelo fila in filas) fila.Estado = TextoEstado(fila.Vuelo);
                dgvVuelos.Invalidate();
            }
        }

        private static string TextoEstado(Vuelo_GV42 v)
        {
            return v.BorradoLogico ? IdiomaManager_GV42.T("vuelos.estadoBaja") : IdiomaManager_GV42.T("vuelos.estadoActivo");
        }

        #endregion

        #region Carga de datos

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
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("vuelos.errorCatalogos"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Estado = TextoEstado(v),
                    Vuelo = v
                }).ToList();

                dgvVuelos.DataSource = null;
                dgvVuelos.DataSource = filas;

                dgvVuelos.ClearSelection();
                foreach (DataGridViewRow row in dgvVuelos.Rows)
                {
                    var fila = (FilaVuelo)row.DataBoundItem;
                    if (idSeleccionar == 0 || fila.Vuelo.Id == idSeleccionar)
                    {
                        row.Selected = true;
                        dgvVuelos.CurrentCell = row.Cells[0];
                        break;
                    }
                }
                MostrarSeleccionado();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("vuelos.titulo"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("vuelos.errorCargar"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Edición

        private void MostrarSeleccionado()
        {
            if (dgvVuelos.SelectedRows.Count == 0)
            {
                _seleccionado = null;
                pnlEditor.Enabled = false;
                return;
            }

            _seleccionado = ((FilaVuelo)dgvVuelos.SelectedRows[0].DataBoundItem).Vuelo;
            pnlEditor.Enabled = true;

            txtCodigo.Text = _seleccionado.CodigoVuelo;
            cmbAerolinea.SelectedValue = _seleccionado.Aerolinea.Id;
            cmbOrigen.SelectedValue = _seleccionado.Origen.Id;
            cmbDestino.SelectedValue = _seleccionado.Destino.Id;
            dtSalida.Value = _seleccionado.FechaHoraSalida;
            dtLlegada.Value = _seleccionado.FechaHoraLlegada;
            txtPuerta.Text = _seleccionado.PuertaEmbarque;
            numCosto.Value = Math.Min(numCosto.Maximum, Math.Max(numCosto.Minimum, _seleccionado.CostoKiloExceso));

            // Un vuelo que ya salió o que está dado de baja no se edita (la BLL también lo rechaza).
            bool yaSalio = _seleccionado.FechaHoraSalida <= DateTime.Now;
            btnGuardar.Enabled = !_seleccionado.BorradoLogico && !yaSalio;
            btnBaja.Enabled = !_seleccionado.BorradoLogico && !yaSalio;
            btnReactivar.Enabled = _seleccionado.BorradoLogico && !yaSalio;
        }

        private static DateTime SinSegundos(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
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

        #endregion

        #region Eventos

        private void dgvVuelos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgvVuelos.Rows[e.RowIndex].DataBoundItem as FilaVuelo;
            if (fila != null && fila.Vuelo.BorradoLogico)
                e.CellStyle.ForeColor = Tema_GV42.Ocupado;
        }

        private void dgvVuelos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarSeleccionado();
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
                MessageBox.Show(IdiomaManager_GV42.T("vuelos.guardado"), IdiomaManager_GV42.T("vuelos.listo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarVuelos(v.Id);
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("vuelos.errorGuardar"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            if (MessageBox.Show(IdiomaManager_GV42.T("vuelos.confirmarBaja", _seleccionado.CodigoVuelo),
                                IdiomaManager_GV42.T("vuelos.confirmarBajaTitulo"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            CambiarEstado(() => _bll.DarDeBaja(_seleccionado), IdiomaManager_GV42.T("vuelos.errorBaja"));
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            CambiarEstado(() => _bll.Reactivar(_seleccionado), IdiomaManager_GV42.T("vuelos.errorReactivar"));
        }

        #endregion

        #region Tipos anidados

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

        #endregion
    }
}
