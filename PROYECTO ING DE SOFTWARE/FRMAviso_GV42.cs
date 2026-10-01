using Servicios;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Ventanita de "espere" que se muestra mientras se instala o actualiza la base de datos
    // (antes se armaba en código dentro de Program.cs). Se cierra con Dispose (bloque using).
    public partial class FRMAviso_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        // Clave de idioma del mensaje (por ejemplo "instalacion.avisoActualizando").
        private readonly string _claveMensaje;

        #endregion

        #region Constructor

        public FRMAviso_GV42(string claveMensaje = null)
        {
            InitializeComponent();
            _claveMensaje = claveMensaje;

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            this.Disposed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("instalacion.avisoTitulo");
            lblTitulo.Text = IdiomaManager_GV42.T("instalacion.avisoEncabezado");
            lblSubtitulo.Text = IdiomaManager_GV42.T("instalacion.avisoEspere");
            lblMensaje.Text = IdiomaManager_GV42.T(string.IsNullOrEmpty(_claveMensaje) ? "instalacion.avisoTrabajando" : _claveMensaje);
        }

        #endregion

        #region Mostrar

        // Muestra el aviso sin bloquear (el trabajo pesado sigue en el mismo hilo) y lo devuelve
        // para cerrarlo con using / Dispose cuando termina la operación.
        public static FRMAviso_GV42 Mostrar(string claveMensaje)
        {
            var aviso = new FRMAviso_GV42(claveMensaje);
            aviso.Show();
            aviso.Refresh();
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            return aviso;
        }

        #endregion
    }
}
