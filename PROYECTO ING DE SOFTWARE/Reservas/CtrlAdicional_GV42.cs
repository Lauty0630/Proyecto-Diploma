using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Una fila de servicio adicional (RFN 1, paso "Adicionales"): tilde, cantidad con su tope y
    // costo unitario. FRMReservarVuelo_GV42 crea una por cada tipo activo que viene de la base.
    // El diseño está en CtrlAdicional_GV42.Designer.cs.
    public partial class CtrlAdicional_GV42 : UserControl
    {
        #region Campos

        private TipoAdicional_GV42 _tipo;
        private bool _costoEditable;
        private int _maximo = 1;
        private bool _porTramo;

        #endregion

        #region Constructor

        public CtrlAdicional_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        public TipoAdicional_GV42 Tipo => _tipo;
        public bool Seleccionado => chkSeleccionado.Checked;
        public int Cantidad => (int)numCantidad.Value;
        public decimal CostoUnitario => numCosto.Value;

        // Nombre del servicio en el idioma actual (si no hay traducción, el de la base).
        public string NombreTraducido
        {
            get
            {
                if (_tipo == null) return string.Empty;
                return IdiomaManager_GV42.TConDefecto("adicional." + _tipo.Nombre, _tipo.Nombre);
            }
        }

        #endregion

        #region Configuración

        // costoEditable: hoy siempre false. El precio es el del catálogo para todos (la BLL lo fuerza).
        public void Configurar(TipoAdicional_GV42 tipo, bool costoEditable)
        {
            _tipo = tipo;
            _costoEditable = costoEditable;
            numCantidad.Maximum = BLLReserva_GV42.MAX_CANTIDAD_ADICIONAL;
            numCosto.Maximum = BLLReserva_GV42.MAX_COSTO_ADICIONAL;
            numCosto.Value = Math.Min(numCosto.Maximum, Math.Max(numCosto.Minimum, tipo.PrecioUnitario));
            ActualizarIdioma();
        }

        // Pone como máximo del NumericUpDown el tope que calcula la BLL (MaxPorPasajero x pasajeros
        // x tramos). Devuelve true si el servicio estaba tildado con una cantidad mayor y se ajustó.
        public bool AplicarTope(int maximo, bool porTramo)
        {
            if (maximo < 1) maximo = 1;
            bool superaba = numCantidad.Value > maximo;

            _maximo = maximo;
            _porTramo = porTramo;
            numCantidad.Maximum = maximo;   // si el valor era mayor, el control lo baja al máximo
            ActualizarTextoTope();

            return superaba && chkSeleccionado.Checked;
        }

        #endregion

        #region Idioma

        public void ActualizarIdioma()
        {
            chkSeleccionado.Text = NombreTraducido;
            lblCantidad.Text = IdiomaManager_GV42.T("reservar.adicionalCantidad");
            lblCosto.Text = IdiomaManager_GV42.T("reservar.adicionalCosto");
            ActualizarTextoTope();
        }

        private void ActualizarTextoTope()
        {
            int porPasajero = Math.Max(1, _tipo != null ? _tipo.MaxPorPasajero : 1);
            lblTope.Text = IdiomaManager_GV42.T(_porTramo ? "reservar.adicionalTopeTramo" : "reservar.adicionalTope",
                                                _maximo, porPasajero);
        }

        #endregion

        #region Eventos

        private void chkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            numCantidad.Enabled = chkSeleccionado.Checked;
            numCosto.Enabled = chkSeleccionado.Checked && _costoEditable;
        }

        #endregion
    }
}
