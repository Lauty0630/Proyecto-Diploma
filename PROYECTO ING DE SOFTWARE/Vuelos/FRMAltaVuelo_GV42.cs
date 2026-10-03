using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Alta de un vuelo (se abre desde "Gestión de vuelos" con el botón Nuevo vuelo).
    // Se cargan los datos del vuelo y el precio base de cada clase; el mapa de asientos es el estándar
    // (lo arma la BLL). El primer registro del vuelo en la bitácora Vuelo_C lo genera el trigger de la base.
    // El diseño está en FRMAltaVuelo_GV42.Designer.cs (Form Designer).
    public partial class FRMAltaVuelo_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLVuelo_GV42 _bll = new BLLVuelo_GV42();

        #endregion

        #region Propiedades

        // Id del vuelo creado (para dejarlo seleccionado en la lista al volver).
        public int IdVueloCreado { get; private set; }

        #endregion

        #region Constructor

        public FRMAltaVuelo_GV42()
        {
            InitializeComponent();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();

            CargarCatalogos();

            // Valores iniciales razonables: sale mañana a las 8 y dura dos horas.
            dtSalida.Value = DateTime.Today.AddDays(1).AddHours(8);
            dtLlegada.Value = dtSalida.Value.AddHours(2);
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("altaVuelo.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("altaVuelo.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("altaVuelo.subtitulo");

            lblCodigo.Text = IdiomaManager_GV42.T("vuelos.codigo");
            lblAerolinea.Text = IdiomaManager_GV42.T("vuelos.aerolinea");
            lblOrigen.Text = IdiomaManager_GV42.T("vuelos.origen");
            lblDestino.Text = IdiomaManager_GV42.T("vuelos.destino");
            lblSalida.Text = IdiomaManager_GV42.T("vuelos.salida");
            lblLlegada.Text = IdiomaManager_GV42.T("vuelos.llegada");
            lblPuerta.Text = IdiomaManager_GV42.T("vuelos.puerta");
            lblCosto.Text = IdiomaManager_GV42.T("vuelos.costoKilo");

            lblSeccionPrecios.Text = IdiomaManager_GV42.T("altaVuelo.precios");
            lblPrecioEconomica.Text = ClaseVuelo_GV42.Economica.Texto();
            lblPrecioEjecutiva.Text = ClaseVuelo_GV42.Ejecutiva.Texto();
            lblPrecioPrimera.Text = ClaseVuelo_GV42.Primera.Texto();

            int total = (BLLVuelo_GV42.FILAS_PRIMERA + BLLVuelo_GV42.FILAS_EJECUTIVA + BLLVuelo_GV42.FILAS_ECONOMICA) * BLLVuelo_GV42.ASIENTOS_POR_FILA;
            lblMapa.Text = IdiomaManager_GV42.T("altaVuelo.mapa", total,
                BLLVuelo_GV42.FILAS_ECONOMICA * BLLVuelo_GV42.ASIENTOS_POR_FILA,
                BLLVuelo_GV42.FILAS_EJECUTIVA * BLLVuelo_GV42.ASIENTOS_POR_FILA,
                BLLVuelo_GV42.FILAS_PRIMERA * BLLVuelo_GV42.ASIENTOS_POR_FILA);

            btnCrear.Text = IdiomaManager_GV42.T("altaVuelo.crear");
            btnCancelar.Text = IdiomaManager_GV42.T("general.cancelar");
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
                if (cmbDestino.Items.Count > 1) cmbDestino.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("vuelos.errorCatalogos"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DateTime SinSegundos(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
        }

        #endregion

        #region Eventos

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                var v = new Vuelo_GV42
                {
                    CodigoVuelo = txtCodigo.Text,
                    Aerolinea = cmbAerolinea.SelectedItem as Aerolinea_GV42,
                    Origen = cmbOrigen.SelectedItem as Aeropuerto_GV42,
                    Destino = cmbDestino.SelectedItem as Aeropuerto_GV42,
                    FechaHoraSalida = SinSegundos(dtSalida.Value),
                    FechaHoraLlegada = SinSegundos(dtLlegada.Value),
                    PuertaEmbarque = txtPuerta.Text,
                    CostoKiloExceso = numCosto.Value
                };

                IdVueloCreado = _bll.Crear(v, numPrecioEconomica.Value, numPrecioEjecutiva.Value, numPrecioPrimera.Value);

                MessageBox.Show(IdiomaManager_GV42.T("altaVuelo.creado", v.CodigoVuelo), IdiomaManager_GV42.T("vuelos.listo"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.revisarDatos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("altaVuelo.errorCrear"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
