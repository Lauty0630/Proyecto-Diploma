using BE;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Tarjeta con los datos de un pasajero de la reserva (RFN 1, paso "Pasajeros").
    // FRMReservarVuelo_GV42 crea una por pasajero; el diseño está en CtrlPasajero_GV42.Designer.cs
    // y la lógica (autocompletar, bloquear, validar) sigue estando en el formulario.
    // Además de los datos de la persona pide la fecha de nacimiento (define si viaja como adulto,
    // niño o infante) y la asistencia especial que necesita para el viaje.
    public partial class CtrlPasajero_GV42 : UserControl
    {
        #region Campos

        private DateTime _fechaVuelo = DateTime.Today;

        #endregion

        #region Constructor

        public CtrlPasajero_GV42()
        {
            InitializeComponent();
            dtNacimiento.MaxDate = DateTime.Today;
            dtNacimiento.MinDate = DateTime.Today.AddYears(-120);
            ActualizarIdioma();
        }

        #endregion

        #region Propiedades

        public TextBox Dni => txtDni;
        public TextBox Nombre => txtNombre;
        public TextBox Apellido => txtApellido;
        public TextBox Email => txtEmail;
        public TextBox Telefono => txtTelefono;

        // Fecha de nacimiento: null mientras no se la haya indicado (casilla del selector sin tildar).
        public DateTime? FechaNacimiento
        {
            get { return dtNacimiento.Checked ? (DateTime?)dtNacimiento.Value.Date : null; }
            set
            {
                if (value.HasValue && value.Value.Date >= dtNacimiento.MinDate && value.Value.Date <= dtNacimiento.MaxDate)
                {
                    dtNacimiento.Value = value.Value.Date;
                    dtNacimiento.Checked = true;
                }
                else
                {
                    dtNacimiento.Checked = false;
                }
                ActualizarTipo();
            }
        }

        // La fecha ya está registrada para esa persona: no se puede cambiar desde la reserva.
        public bool NacimientoBloqueado
        {
            get { return !dtNacimiento.Enabled; }
            set { dtNacimiento.Enabled = !value; }
        }

        public AsistenciaEspecial_GV42 Asistencia
        {
            get { return cmbAsistencia.SelectedItem is AsistenciaEspecial_GV42 a ? a : AsistenciaEspecial_GV42.Ninguna; }
            set { cmbAsistencia.SelectedItem = value; }
        }

        // Fecha del vuelo de ida: con la fecha de nacimiento define el tipo de pasajero que se muestra.
        public DateTime FechaVuelo
        {
            get { return _fechaVuelo; }
            set { _fechaVuelo = value; ActualizarTipo(); }
        }

        // Cambió la fecha de nacimiento (el formulario recalcula tarifas y pasos).
        public event EventHandler NacimientoCambiado;

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
            lblNacimiento.Text = IdiomaManager_GV42.T("reservar.nacimiento");
            lblTipo.Text = IdiomaManager_GV42.T("reservar.tipoPasajero");
            lblAsistencia.Text = IdiomaManager_GV42.T("reservar.asistencia");

            // Los ítems del combo son el enum: se vuelven a cargar para que se dibujen en el idioma nuevo.
            AsistenciaEspecial_GV42 elegida = Asistencia;
            cmbAsistencia.BeginUpdate();
            cmbAsistencia.Items.Clear();
            foreach (AsistenciaEspecial_GV42 a in Enum.GetValues(typeof(AsistenciaEspecial_GV42))) cmbAsistencia.Items.Add(a);
            cmbAsistencia.SelectedItem = elegida;
            cmbAsistencia.EndUpdate();
            ActualizarTipo();
        }

        // "Adulto", "Niño (2 a 11 años)" o "Infante (menor de 2, en brazos)" según la edad el día del vuelo.
        private void ActualizarTipo()
        {
            if (!dtNacimiento.Checked)
            {
                lblTipoValor.Text = IdiomaManager_GV42.T("reservar.tipoSinFecha");
                return;
            }
            TipoPasajero_GV42 tipo = new Pasajero_GV42 { FechaNacimiento = dtNacimiento.Value.Date }.TipoEn(_fechaVuelo);
            lblTipoValor.Text = tipo == TipoPasajero_GV42.Adulto ? tipo.Texto()
                : IdiomaManager_GV42.T(tipo == TipoPasajero_GV42.Nino ? "reservar.tipoNinoDetalle" : "reservar.tipoInfanteDetalle");
        }

        #endregion

        #region Eventos

        // En el DNI solo se aceptan dígitos (se ignora cualquier otra tecla, salvo borrar/pegar).
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void dtNacimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTipo();
            NacimientoCambiado?.Invoke(this, EventArgs.Empty);
        }

        // El enum del combo se muestra traducido.
        private void cmbAsistencia_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is AsistenciaEspecial_GV42 a) e.Value = a.Texto();
        }

        #endregion
    }
}
