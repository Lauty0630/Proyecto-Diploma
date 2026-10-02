using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Navegador "pasajero anterior / siguiente" del check-in grupal: muestra a quién se está
    // atendiendo ("Pasajero 2 de 3 · Sofía Vergara") y avisa con eventos cuando se pide cambiar.
    // No conoce a los pasajeros: el formulario le pasa el texto y qué flechas están habilitadas.
    // El diseño está en el .Designer.cs.
    public partial class CtrlNavegadorPasajero_GV42 : UserControl
    {
        #region Eventos públicos

        public event EventHandler Anterior;
        public event EventHandler Siguiente;

        #endregion

        #region Constructor

        public CtrlNavegadorPasajero_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        // Texto del pasajero activo. Lo arma (y lo traduce) el formulario que contiene el control.
        [Category("FLY SAFE")]
        [DefaultValue("Pasajero 1 de 1")]
        public string Texto
        {
            get { return lblPasajero.Text; }
            set { lblPasajero.Text = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PuedeAnterior
        {
            get { return btnAnterior.Enabled; }
            set { btnAnterior.Enabled = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PuedeSiguiente
        {
            get { return btnSiguiente.Enabled; }
            set { btnSiguiente.Enabled = value; }
        }

        #endregion

        #region Idioma

        // Ayudas de las flechas (las traduce el formulario en su ActualizarIdioma).
        public void FijarAyudas(string anterior, string siguiente)
        {
            toolTip1.SetToolTip(btnAnterior, anterior);
            toolTip1.SetToolTip(btnSiguiente, siguiente);
        }

        #endregion

        #region Eventos

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Anterior?.Invoke(this, EventArgs.Empty);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Siguiente?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
