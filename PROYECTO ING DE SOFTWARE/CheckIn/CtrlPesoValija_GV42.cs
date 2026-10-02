using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Peso de UNA valija en el paso de equipaje del check-in. Se crea una instancia por valija
    // (la cantidad depende de lo que despacha el pasajero); el diseño está en el .Designer.cs.
    public partial class CtrlPesoValija_GV42 : UserControl
    {
        #region Eventos públicos

        public event EventHandler PesoCambiado;

        #endregion

        #region Constructor

        public CtrlPesoValija_GV42()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        // Texto de la etiqueta (ej.: "Valija 1"). Lo traduce el formulario que contiene el control.
        [Category("FLY SAFE")]
        [DefaultValue("Valija 1")]
        public string Titulo
        {
            get { return lblValija.Text; }
            set { lblValija.Text = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal Peso
        {
            get { return numPeso.Value; }
            set { numPeso.Value = Math.Max(numPeso.Minimum, Math.Min(numPeso.Maximum, value)); }
        }

        // Control a marcar cuando el peso de esta valija no es válido.
        [Browsable(false)]
        public Control CampoPeso { get { return numPeso; } }

        #endregion

        #region Eventos

        private void numPeso_ValueChanged(object sender, EventArgs e)
        {
            PesoCambiado?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
