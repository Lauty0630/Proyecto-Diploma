using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Maestro de aeropuertos: ABM (añadir, modificar, eliminar) del catálogo que usan los vuelos
    // como origen y destino. Las rutas y los horarios se siguen definiendo en Gestión de vuelos.
    // El diseño está en FRMMaestroAeropuertos_GV42.Designer.cs (Form Designer); acá va solo la lógica.
    public partial class FRMMaestroAeropuertos_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLAeropuerto_GV42 _bll = new BLLAeropuerto_GV42();

        private string _modo = "Consulta";

        private Aeropuerto_GV42 _seleccionado = null;

        // Último mensaje de la caja "Mensaje": se guarda cómo armarlo para regenerarlo al cambiar el idioma.
        private Func<string> _generadorMensaje;
        private bool _mensajeEsError;

        #endregion

        #region Constructor

        public FRMMaestroAeropuertos_GV42()
        {
            InitializeComponent();
            dgvAeropuertos.AutoGenerateColumns = false;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();

            // Largos máximos iguales a los de la base (el código solo acepta letras: txtCodigo_KeyPress).
            txtCodigo.MaxLength = BLLAeropuerto_GV42.LARGO_CODIGO;
            txtNombre.MaxLength = BLLAeropuerto_GV42.MAX_NOMBRE;
            txtCiudad.MaxLength = BLLAeropuerto_GV42.MAX_CIUDAD;
            txtPais.MaxLength = BLLAeropuerto_GV42.MAX_PAIS;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("aeropuertos.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("aeropuertos.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("aeropuertos.subtitulo");

            lblTituloDatos.Text = IdiomaManager_GV42.T("aeropuertos.tituloDatos");
            label1.Text = IdiomaManager_GV42.T("aeropuertos.codigo");
            label2.Text = IdiomaManager_GV42.T("aeropuertos.nombre");
            label3.Text = IdiomaManager_GV42.T("aeropuertos.ciudad");
            label4.Text = IdiomaManager_GV42.T("aeropuertos.pais");
            lblTituloMensaje.Text = IdiomaManager_GV42.T("usuarios.mensaje");
            lblMensaje.Text = TraducirModo(_modo);

            lblTituloGrilla.Text = IdiomaManager_GV42.T("aeropuertos.tituloGrilla");
            colCodigo.HeaderText = IdiomaManager_GV42.T("aeropuertos.codigo");
            colNombre.HeaderText = IdiomaManager_GV42.T("aeropuertos.nombre");
            colCiudad.HeaderText = IdiomaManager_GV42.T("aeropuertos.ciudad");
            colPais.HeaderText = IdiomaManager_GV42.T("aeropuertos.pais");

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
            dgvAeropuertos.DataSource = null;
            dgvAeropuertos.DataSource = _bll.Listar();

            if (dgvAeropuertos.Rows.Count == 0)
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
            dgvAeropuertos.Enabled = true;
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
            dgvAeropuertos.Enabled = false;
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtCodigo.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            txtCiudad.Enabled = habilitar;
            txtPais.Enabled = habilitar;
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = txtNombre.Text = txtCiudad.Text = txtPais.Text = "";
        }

        private void MostrarSeleccionado()
        {
            if (_seleccionado == null) { LimpiarCampos(); return; }
            txtCodigo.Text = _seleccionado.CodigoIata;
            txtNombre.Text = _seleccionado.Nombre;
            txtCiudad.Text = _seleccionado.Ciudad;
            txtPais.Text = _seleccionado.Pais;
        }

        #endregion

        #region Operaciones (Añadir / Modificar / Eliminar)

        private Aeropuerto_GV42 AeropuertoDeLosCampos()
        {
            return new Aeropuerto_GV42
            {
                Id = _modo == "Modificar" && _seleccionado != null ? _seleccionado.Id : 0,
                CodigoIata = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Pais = txtPais.Text.Trim()
            };
        }

        private void Crear()
        {
            Aeropuerto_GV42 a = AeropuertoDeLosCampos();
            _bll.Crear(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.okCreado", a.CodigoIata));
        }

        private void Modificar()
        {
            Aeropuerto_GV42 a = AeropuertoDeLosCampos();
            _bll.Modificar(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.okModificado", a.CodigoIata));
        }

        private void Eliminar()
        {
            Aeropuerto_GV42 a = _seleccionado;
            _bll.Eliminar(a);
            ModoConsulta();
            CargarGrilla();
            MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.okEliminado", a.CodigoIata));
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

        private void FRMMaestroAeropuertos_GV42_Load(object sender, EventArgs e)
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El código IATA solo admite letras.
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar)) e.Handled = true;
        }

        private void dgvAeropuertos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAeropuertos.CurrentRow == null) return;

            _seleccionado = dgvAeropuertos.CurrentRow.DataBoundItem as Aeropuerto_GV42;
            MostrarSeleccionado();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ModoOperacion("Crear");
            LimpiarCampos();
            HabilitarCampos(true);
            txtCodigo.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.seleccionar"), true);
                return;
            }

            ModoOperacion("Modificar");
            MostrarSeleccionado();
            HabilitarCampos(true);
            txtCodigo.Enabled = false;   // el código IATA identifica al aeropuerto
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null)
            {
                MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.seleccionar"), true);
                return;
            }

            ModoOperacion("Eliminar");
            MostrarSeleccionado();
            HabilitarCampos(false);
            MostrarMensaje(() => IdiomaManager_GV42.T("aeropuertos.confirmarEliminar"));
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Las reglas de la BLL (código repetido, datos inválidos, aeropuerto con vuelos, permisos) llegan
            // como excepción con el motivo: se muestran en la caja "Mensaje" y la pantalla queda como estaba.
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
