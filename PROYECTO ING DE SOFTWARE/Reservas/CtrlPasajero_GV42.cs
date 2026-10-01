using Servicios;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Tarjeta con los datos de un pasajero de la reserva (RFN 1, paso "Pasajeros").
    // FRMReservarVuelo_GV42 crea una por pasajero; el diseño está en CtrlPasajero_GV42.Designer.cs
    // y la lógica (autocompletar, bloquear, validar) sigue estando en el formulario.
    public partial class CtrlPasajero_GV42 : UserControl
    {
        #region Constructor

        public CtrlPasajero_GV42()
        {
            InitializeComponent();
            ActualizarIdioma();
        }

        #endregion

        #region Propiedades

        public TextBox Dni => txtDni;
        public TextBox Nombre => txtNombre;
        public TextBox Apellido => txtApellido;
        public TextBox Email => txtEmail;
        public TextBox Telefono => txtTelefono;

        // "Pasajero 1", "Pasajero 1 (vos)", etc. Lo arma el formulario porque depende del modo.
        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }

        #endregion

        #region Idioma

        // La llama el formulario desde su ActualizarIdioma (Observer).
        public void ActualizarIdioma()
        {
            lblDni.Text = IdiomaManager_GV42.T("reservar.dni");
            lblNombre.Text = IdiomaManager_GV42.T("reservar.nombre");
            lblApellido.Text = IdiomaManager_GV42.T("reservar.apellido");
            lblEmail.Text = IdiomaManager_GV42.T("reservar.email");
            lblTelefono.Text = IdiomaManager_GV42.T("reservar.telefono");
        }

        #endregion

        #region Eventos

        // En el DNI solo se aceptan dígitos (se ignora cualquier otra tecla, salvo borrar/pegar).
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        #endregion
    }
}
