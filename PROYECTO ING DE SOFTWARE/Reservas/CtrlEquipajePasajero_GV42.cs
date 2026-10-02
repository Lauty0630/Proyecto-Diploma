using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Una fila de "Equipaje extra" por pasajero (RFN 1, paso "Adicionales"). La valija extra se compra
    // a nombre de quien la va a despachar: en el check-in (RFN 2) cada pasajero solo puede usar las
    // suyas. FRMReservarVuelo_GV42 crea una fila por pasajero.
    // El diseño está en CtrlEquipajePasajero_GV42.Designer.cs.
    public partial class CtrlEquipajePasajero_GV42 : UserControl
    {
        #region Campos

        private string _pasajero = string.Empty;
        private decimal _precio;
        private bool _porTramo;

        #endregion

        #region Constructor

        public CtrlEquipajePasajero_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        public int Cantidad
        {
            get { return (int)numCantidad.Value; }
            set { numCantidad.Value = Math.Max(numCantidad.Minimum, Math.Min(numCantidad.Maximum, value)); }
        }

        public string Pasajero => _pasajero;

        #endregion

        #region Configuración

        // maximo: valijas extra que puede llevar un pasajero (TipoAdicional.MaxPorPasajero).
        // porTramo: en ida y vuelta la valija se cobra una vez por tramo.
        public void Configurar(string pasajero, int maximo, decimal precio, bool porTramo)
        {
            _pasajero = pasajero ?? string.Empty;
            _precio = precio;
            _porTramo = porTramo;
            numCantidad.Maximum = Math.Max(1, maximo);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma

        public void ActualizarIdioma()
        {
            lblPasajero.Text = IdiomaManager_GV42.T("reservar.equipaje.pasajero", _pasajero);
            lblCantidad.Text = IdiomaManager_GV42.T("reservar.equipaje.cantidad");
            lblDetalle.Text = IdiomaManager_GV42.T(_porTramo ? "reservar.equipaje.detalleTramo" : "reservar.equipaje.detalle",
                                                   (int)numCantidad.Maximum, _precio.ToString("C2"));
        }

        #endregion
    }
}
