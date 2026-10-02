using BE;
using Servicios;
using System;
using System.Text;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Tarjeta de una familia tarifaria (Light / Plus / Top) en el paso "Tarifa" de la reserva (RFN 1):
    // precio por adulto y lo que incluye (valijas, asiento, cambios y reembolso).
    // FRMReservarVuelo_GV42 crea una por cada tarifa activa. El diseño está en CtrlTarifa_GV42.Designer.cs.
    public partial class CtrlTarifa_GV42 : UserControl
    {
        #region Campos

        private TarifaFamilia_GV42 _tarifa;
        private decimal _precioAdulto;
        private decimal _precioSeleccionAsiento;
        private bool _seleccionada;

        #endregion

        #region Constructor

        public CtrlTarifa_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        public TarifaFamilia_GV42 Tarifa => _tarifa;

        // La tarifa elegida se destaca con el borde y el botón en verde.
        public bool Seleccionada
        {
            get { return _seleccionada; }
            set
            {
                _seleccionada = value;
                pnlTarjeta.ColorBorde = value ? Tema_GV42.Primario : Tema_GV42.BordeGrilla;
                pnlTarjeta.AcentoSuperior = value;
                btnElegir.Estilo = value ? EstiloBoton_GV42.Exito : EstiloBoton_GV42.Primario;
                ActualizarIdioma();
                pnlTarjeta.Invalidate();
            }
        }

        // El usuario eligió esta tarifa.
        public event EventHandler Elegida;

        #endregion

        #region Configuración

        // precioAdulto: tarifa por adulto para todos los tramos del viaje (sin impuestos).
        // precioSeleccionAsiento: lo que cuesta elegir asiento cuando la tarifa no lo incluye.
        public void Configurar(TarifaFamilia_GV42 tarifa, decimal precioAdulto, decimal precioSeleccionAsiento)
        {
            _tarifa = tarifa;
            _precioAdulto = precioAdulto;
            _precioSeleccionAsiento = precioSeleccionAsiento;
            ActualizarIdioma();
        }

        #endregion

        #region Idioma

        public void ActualizarIdioma()
        {
            if (_tarifa == null) return;

            lblNombre.Text = _tarifa.Nombre;
            lblPrecio.Text = _precioAdulto.ToString("C2");
            lblPorAdulto.Text = IdiomaManager_GV42.T("tarifa.porAdulto");
            btnElegir.Text = IdiomaManager_GV42.T(_seleccionada ? "tarifa.elegida" : "tarifa.elegir");

            var detalle = new StringBuilder();
            detalle.AppendLine("•  " + (_tarifa.ValijasIncluidas == 0
                ? IdiomaManager_GV42.T("tarifa.sinValija")
                : IdiomaManager_GV42.T("tarifa.valijas", _tarifa.ValijasIncluidas)));
            detalle.AppendLine();
            detalle.AppendLine("•  " + IdiomaManager_GV42.T(
                _tarifa.IncluyePreferencial ? "tarifa.asientoTodos"
                : _tarifa.ElegirAsientoEsPago ? "tarifa.asientoPago" : "tarifa.asientoComun",
                _precioSeleccionAsiento.ToString("C0")));
            detalle.AppendLine();
            detalle.AppendLine("•  " + (_tarifa.PorcentajePenalidadCambio <= 0
                ? IdiomaManager_GV42.T("tarifa.cambioSinCargo")
                : IdiomaManager_GV42.T("tarifa.cambioPenalidad", _tarifa.PorcentajePenalidadCambio.ToString("0.##"))));
            detalle.AppendLine();
            detalle.Append("•  " + IdiomaManager_GV42.T(
                _tarifa.TipoReembolso == TarifaFamilia_GV42.REEMBOLSO_TOTAL ? "tarifa.reembolsoTotal"
                : _tarifa.TipoReembolso == TarifaFamilia_GV42.REEMBOLSO_CON_PENALIDAD ? "tarifa.reembolsoPenalidad"
                : "tarifa.noReembolsable"));
            lblDetalle.Text = detalle.ToString();
        }

        #endregion

        #region Eventos

        private void btnElegir_Click(object sender, EventArgs e)
        {
            Elegida?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
