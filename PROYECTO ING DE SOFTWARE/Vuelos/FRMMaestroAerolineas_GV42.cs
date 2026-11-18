using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Maestro de aerolíneas: ABM (añadir, modificar, eliminar) del catálogo de aerolíneas que operan
    // los vuelos. Se usa en Gestión de vuelos al crear o modificar un vuelo.
    // El diseño está en FRMMaestroAerolineas_GV42.Designer.cs (Form Designer); acá va solo la lógica.
    public partial class FRMMaestroAerolineas_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLAerolinea_GV42 _bll = new BLLAerolinea_GV42();

        private string _modo = "Consulta";

        private Aerolinea_GV42 _seleccionado = null;

        // Último mensaje de la caja "Mensaje": se guarda cómo armarlo para regenerarlo al cambiar el idioma.
        private Func<string> _generadorMensaje;
        private bool _mensajeEsError;

        #endregion

        #region Constructor

        public FRMMaestroAerolineas_GV42()
        {
            InitializeComponent();
            dgvAerolineas.AutoGenerateColumns = false;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();

            // Largo máximo igual al de la base.
            txtNombre.MaxLength = BLLAerolinea_GV42.MAX_NOMBRE;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("aerolineas.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("aerolineas.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("aerolineas.subtitulo");

            lblTituloDatos.Text = IdiomaManager_GV42.T("aerolineas.tituloDatos");
            label2.Text = IdiomaManager_GV42.T("aerolineas.nombre");
            lblTituloMensaje.Text = IdiomaManager_GV42.T("usuarios.mensaje");
            lblMensaje.Text = TraducirModo(_modo);

            lblTituloGrilla.Text = IdiomaManager_GV42.T("aerolineas.tituloGrilla");
            colNombre.HeaderText = IdiomaManager_GV42.T("aerolineas.nombre");

            btnCrear.Text = IdiomaManager_GV42.T("clientes.anadir");
            btnModificar.Text = IdiomaManager_GV42.T("usuarios.modificar");
            btnEliminar.Text = IdiomaManager_GV42.T("clientes.eliminar");
            btnAplicar.Text = IdiomaManager_GV42.T("usuarios.aplicar");
            btnCancelar.Text = IdiomaManager_GV42.T("usuarios.cancelar");
            btnSalir.Text = IdiomaManager_GV42.T("usuarios.salir");

            RefrescarMensaje();
        }

        private string TraducirModo(string modo)
        {
            return IdiomaManager_GV42.T("usuarios.modo") + ": " + IdiomaManager_GV42.TConDefecto("clientes.nombreModo" + modo, modo);
        }

        #endregion

        #region Carga de datos

        private void CargarGrilla()
        {
            dgvAerolineas.DataSource = null;
            dgvAerolineas.DataSource = _bll.Listar();

            if (dgvAerolineas.Rows.Count == 0)
            {
                _seleccionado = null;
                LimpiarCampos();
            }
        }

        #endregion

        #region Modos de pantalla

        private void ModoConsulta()
        {
            _modo = "Consulta";
            lblMensaje.Text = TraducirModo(_modo);
            LimpiarCampos();
            HabilitarCampos(false);
            btnCrear.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAplicar.Enabled = false;
            btnCancelar.Enabled = false;
            dgvAerolineas.Enabled = true;
        }

        private void ModoOperacion(string modo)
        {
            _modo = modo;
            lblMensaje.Text = TraducirModo(modo);
            btnCrear.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAplicar.Enabled = true;
            btnCancelar.Enabled = true;
            dgvAerolineas.Enabled = false;
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
        }

        private void MostrarSeleccionado()
        {
            if (_seleccionado == null) { LimpiarCampos(); return; }
            txtNombre.Text = _seleccionado.Nombre;
        }

        #endregion

        #region Operaciones (Añadir / Modificar / Eliminar)

        private Aerolinea_GV42 AerolineaDeLosCampos()
        {
            return new Aerolinea_GV42
            {
                Id = _modo == "Modificar" && _seleccionado != null ? _seleccionado.Id : 0,
                Nombre = txtNombre.Text.Trim()
            };
        }

        private void Crear()
        {
            Aerolinea_GV42 a = AerolineaDeLosCampos();
            _bll.Crear(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.okCreada", a.Nombre));
        }

        private void Modificar()
        {
            Aerolinea_GV42 a = AerolineaDeLosCampos();
            _bll.Modificar(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.okModificada", a.Nombre));
        }

        private void Eliminar()
        {
            Aerolinea_GV42 a = _seleccionado;
            _bll.Eliminar(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.okEliminada", a.Nombre));
        }

        #endregion

        #region Mensajes

        // Muestra un texto en la caja "Mensaje". Se guarda la forma de armarlo para poder
        // regenerarlo en el otro idioma cuando cambia el idioma (ActualizarIdioma).
        private void MostrarMensaje(Func<string> generador, bool esError = false)
        {
            _generadorMensaje = generador;
            _mensajeEsError = esError;
            RefrescarMensaje();
        }

        private void RefrescarMensaje()
        {
            if (_generadorMensaje == null) return;
            // El color depende del resultado (error / normal), por eso se asigna en tiempo de ejecución.
            txtMensaje.ForeColor = _mensajeEsError ? Tema_GV42.Error : Tema_GV42.Texto;
            txtMensaje.Text = _generadorMensaje();
        }

        #endregion

        #region Eventos

        private void FRMMaestroAerolineas_GV42_Load(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void dgvAerolineas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAerolineas.CurrentRow == null) return;

            _seleccionado = dgvAerolineas.CurrentRow.DataBoundItem as Aerolinea_GV42;
            MostrarSeleccionado();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ModoOperacion("Crear");
            LimpiarCampos();
            HabilitarCampos(true);
            txtNombre.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.seleccionar"), true);
                return;
            }

            ModoOperacion("Modificar");
            MostrarSeleccionado();
            HabilitarCampos(true);
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.seleccionar"), true);
                return;
            }

            ModoOperacion("Eliminar");
            MostrarSeleccionado();
            HabilitarCampos(false);
            MostrarMensaje(() => IdiomaManager_GV42.T("aerolineas.confirmarEliminar"));
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Las reglas de la BLL (nombre repetido o inválido, aerolínea con vuelos, permisos) llegan como
            // excepción con el motivo: se muestran en la caja "Mensaje" y la pantalla queda como estaba.
            try
            {
                switch (_modo)
                {
                    case "Crear": Crear(); break;
                    case "Modificar": Modificar(); break;
                    case "Eliminar": Eliminar(); break;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ModoConsulta();
                CargarGrilla();
                MostrarMensaje(() => IdiomaManager_GV42.T("clientes.cancelado"));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MostrarMensaje(() => error, true);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
