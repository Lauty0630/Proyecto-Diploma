using BE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Instalador
{
    // Asistente de instalación (varias pantallas en una sola ventana). Solo toma lo que elige el operador
    // y muestra el avance: todo el trabajo lo hace InstaladorSistema_GV42.
    // Pantallas: carpeta de destino -> base de datos -> datos iniciales -> tareas adicionales ->
    // listo para instalar -> instalando. Es a la vez instalador (primera vez) y reinstalador (ya instalado):
    // en "Datos iniciales" se ofrecen solo las opciones que corresponden a lo que haya en la instancia.
    public partial class FRMInstalador_GV42 : Form
    {
        #region Campos

        private const int PAG_CARPETA = 0;
        private const int PAG_BASE = 1;
        private const int PAG_DATOS = 2;
        private const int PAG_TAREAS = 3;
        private const int PAG_LISTO = 4;
        private const int PAG_PROGRESO = 5;

        // Una opción de la pantalla "Datos iniciales".
        private class Opcion
        {
            public OpcionBase_GV42 Valor;
            public string Titulo;
            public string Detalle;
            public RadioButton Radio;
            public Label Descripcion;
        }

        private readonly InstaladorSistema_GV42 _instalador = new InstaladorSistema_GV42();
        private readonly List<Opcion> _opciones = new List<Opcion>();
        private EstadoBase_GV42 _estado;
        private bool _yaInstalado;
        private bool _ejecutando;
        private bool _terminado;
        private int _pagina;

        #endregion

        #region Constructor y carga

        public FRMInstalador_GV42()
        {
            InitializeComponent();
        }

        private void FRMInstalador_GV42_Load(object sender, EventArgs e)
        {
            string instalada = _instalador.CarpetaInstalada();
            _yaInstalado = instalada != null;

            if (_yaInstalado)
            {
                Text = "Reinstalar - " + InstaladorSistema_GV42.NOMBRE_SISTEMA;
                lblCarpetaInfo.Text = "El sistema ya está instalado en esta carpeta. Se va a reinstalar en:";
                txtCarpeta.Text = instalada;
            }
            else
            {
                Text = "Instalar - " + InstaladorSistema_GV42.NOMBRE_SISTEMA;
                txtCarpeta.Text = _instalador.CarpetaDestinoSugerida;
            }

            double mb = _instalador.TamanoOrigenMB();
            lblEspacio.Text = mb > 0
                ? string.Format("Se requieren al menos {0:0.#} MB de espacio libre en el disco.", mb)
                : string.Empty;

            CargarInstancias();
            IrA(PAG_CARPETA);
        }

        private void CargarInstancias()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                cmbInstancia.Items.Clear();
                foreach (string instancia in _instalador.DetectarInstancias())
                    cmbInstancia.Items.Add(instancia);

                string anterior = _yaInstalado ? _instalador.InstanciaInstalada() : null;
                if (!string.IsNullOrEmpty(anterior) && !cmbInstancia.Items.Contains(anterior))
                    cmbInstancia.Items.Add(anterior);

                if (!string.IsNullOrEmpty(anterior)) cmbInstancia.SelectedItem = anterior;
                else if (cmbInstancia.Items.Count > 0) cmbInstancia.SelectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Navegación entre pantallas

        private void IrA(int pagina)
        {
            _pagina = pagina;
            Panel[] paginas = { pagCarpeta, pagBase, pagDatos, pagTareas, pagListo, pagProgreso };
            for (int i = 0; i < paginas.Length; i++) paginas[i].Visible = (i == pagina);

            string titulo, subtitulo;
            switch (pagina)
            {
                case PAG_CARPETA:
                    titulo = "Seleccione la Carpeta de Destino";
                    subtitulo = "¿Dónde debe instalarse " + InstaladorSistema_GV42.NOMBRE_SISTEMA + "?";
                    break;
                case PAG_BASE:
                    titulo = "Base de datos";
                    subtitulo = "Elegí el servidor SQL Server donde se creará la base del sistema.";
                    break;
                case PAG_DATOS:
                    titulo = "Datos iniciales";
                    subtitulo = _estado == EstadoBase_GV42.SinBase
                        ? "Elegí con qué datos se crea la base."
                        : "Ya existe una base de datos en esa instancia: elegí qué hacer con ella.";
                    break;
                case PAG_TAREAS:
                    titulo = "Seleccione las Tareas Adicionales";
                    subtitulo = "¿Qué tareas adicionales deben realizarse?";
                    break;
                case PAG_LISTO:
                    titulo = "Listo para Instalar";
                    subtitulo = "Ahora el programa está listo para iniciar la instalación de " +
                                InstaladorSistema_GV42.NOMBRE_SISTEMA + " en su sistema.";
                    break;
                default:
                    titulo = "Instalando";
                    subtitulo = "Por favor espere mientras se instala " + InstaladorSistema_GV42.NOMBRE_SISTEMA + ".";
                    break;
            }
            lblTituloPagina.Text = titulo;
            lblSubtituloPagina.Text = subtitulo;

            btnAtras.Enabled = pagina > PAG_CARPETA && pagina < PAG_PROGRESO;
            btnSiguiente.Text = pagina == PAG_LISTO ? "Instalar" : pagina == PAG_PROGRESO ? "Finalizar" : "Siguiente >";
            btnSiguiente.Enabled = pagina != PAG_PROGRESO;
            btnCancelar.Enabled = pagina != PAG_PROGRESO;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_pagina)
                {
                    case PAG_CARPETA:
                        _instalador.ValidarCarpeta(txtCarpeta.Text.Trim());
                        IrA(PAG_BASE);
                        break;
                    case PAG_BASE:
                        AvanzarDesdeBase();
                        break;
                    case PAG_DATOS:
                        AvanzarDesdeDatos();
                        break;
                    case PAG_TAREAS:
                        txtResumen.Text = ArmarResumen();
                        IrA(PAG_LISTO);
                        break;
                    case PAG_LISTO:
                        Instalar();
                        break;
                    default: // Finalizar
                        Close();
                        break;
                }
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            IrA(_pagina - 1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FRMInstalador_GV42_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ejecutando)
            {
                e.Cancel = true; // no se puede cerrar a mitad de la instalación
                return;
            }
            if (!_terminado && e.CloseReason == CloseReason.UserClosing &&
                MessageBox.Show(this, "¿Querés salir sin instalar?", Text, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                e.Cancel = true;
        }

        // Al salir de "Base de datos" se mira qué hay en la instancia, para ofrecer solo lo que corresponde.
        private void AvanzarDesdeBase()
        {
            string instancia = (cmbInstancia.Text ?? string.Empty).Trim();
            _instalador.ValidarInstancia(instancia);

            Cursor = Cursors.WaitCursor;
            try
            {
                _estado = _instalador.EstadoBase(instancia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo conectar con la instancia \"" + instancia + "\".\n\n" + ex.Message, Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MostrarOpciones();
            IrA(PAG_DATOS);
        }

        private void AvanzarDesdeDatos()
        {
            PedidoInstalacion_GV42 pedido = CrearPedido();
            _instalador.Validar(pedido);

            if (InstaladorSistema_GV42.BorraDatos(pedido.OpcionBase))
            {
                bool requiere;
                Cursor = Cursors.WaitCursor;
                try { requiere = _instalador.RequiereAdministrador(pedido); }
                finally { Cursor = Cursors.Default; }

                if (requiere && (pedido.LoginAdministrador.Length == 0 || pedido.ContrasenaAdministrador.Length == 0))
                    throw new NegocioException_GV42("Ingresá el usuario y la contraseña de un administrador del sistema " +
                                                    "para autorizar que se borren los datos.");
            }
            IrA(PAG_TAREAS);
        }

        #endregion

        #region Pantalla "Datos iniciales"

        // Crea las opciones (botones de opción con su descripción) según lo que haya en la instancia.
        private void MostrarOpciones()
        {
            foreach (Opcion o in _opciones)
            {
                pagDatos.Controls.Remove(o.Radio);
                pagDatos.Controls.Remove(o.Descripcion);
                o.Radio.Dispose();
                o.Descripcion.Dispose();
            }
            _opciones.Clear();

            string credenciales = "Usuario inicial: " + InstaladorSistema_GV42.USUARIO_INICIAL +
                                  "  -  Clave: " + InstaladorSistema_GV42.CLAVE_INICIAL + ".";
            const string DETALLE_NEGOCIO = "clientes, aeropuertos, vuelos, reservas, pagos, check-in y reportes";

            var lista = new List<Opcion>();
            switch (_estado)
            {
                case EstadoBase_GV42.SinBase:
                    lista.Add(Nueva(OpcionBase_GV42.CrearSoloUsuarios, "Base solo con gestión de usuarios (recomendado)",
                        "Login, usuarios, roles y permisos, bitácora, backup/restore, integridad e idioma.\n" + credenciales));
                    lista.Add(Nueva(OpcionBase_GV42.CrearCompleta, "Base completa con todo el negocio",
                        "Incluye lo anterior más " + DETALLE_NEGOCIO + ".\n" + credenciales));
                    break;

                case EstadoBase_GV42.SoloUsuarios:
                    lista.Add(Nueva(OpcionBase_GV42.AgregarNegocio, "Actualizar: agregar el negocio conservando los datos (recomendado)",
                        "Agrega " + DETALLE_NEGOCIO + ". No modifica los usuarios, roles ni la bitácora ya cargados."));
                    AgregarReinstalaciones(lista);
                    break;

                case EstadoBase_GV42.Completa:
                    lista.Add(Nueva(OpcionBase_GV42.Mantener, "Mantener la base actual (recomendado)",
                        "No se borra nada. Si la base es de una versión anterior, se actualiza conservando los datos."));
                    AgregarReinstalaciones(lista);
                    break;

                default: // Antigua
                    AgregarReinstalaciones(lista);
                    break;
            }

            int y = 8;
            foreach (Opcion o in lista)
            {
                o.Radio = new RadioButton { AutoSize = true, Location = new Point(30, y), Text = o.Titulo };
                o.Radio.CheckedChanged += rbOpcion_CheckedChanged;

                int alto = TextRenderer.MeasureText(o.Detalle, Font, new Size(560, 0), TextFormatFlags.WordBreak).Height + 2;
                o.Descripcion = new Label { Location = new Point(50, y + 22), Size = new Size(560, alto), Text = o.Detalle };
                pagDatos.Controls.Add(o.Radio);
                pagDatos.Controls.Add(o.Descripcion);
                _opciones.Add(o);
                y += 22 + alto + 8;

                if (o.Valor == OpcionBase_GV42.RestaurarBackup)
                {
                    // El archivo del backup se elige justo debajo de la opción "Restaurar".
                    txtBackup.Location = new Point(50, y - 2);
                    btnBackup.Location = new Point(510, y - 4);
                    y += 32;
                }
            }

            txtBackup.Text = string.Empty;
            txtUsuario.Clear();
            txtContrasena.Clear();
            _opciones[0].Radio.Checked = true;
            ActualizarOpciones();
        }

        private static Opcion Nueva(OpcionBase_GV42 valor, string titulo, string detalle)
        {
            return new Opcion { Valor = valor, Titulo = titulo, Detalle = detalle };
        }

        // Reinstalar (con cualquiera de las dos bases) y restaurar un backup: las tres borran los datos actuales.
        private static void AgregarReinstalaciones(List<Opcion> lista)
        {
            lista.Add(Nueva(OpcionBase_GV42.ReinstalarSoloUsuarios, "Reinstalar solo con gestión de usuarios",
                "Borra la base y la crea de nuevo, solo con la gestión de usuarios. Se pierden todos los datos cargados."));
            lista.Add(Nueva(OpcionBase_GV42.ReinstalarCompleta, "Reinstalar la base completa con todo el negocio",
                "Borra la base y la crea de nuevo, con la gestión de usuarios y el negocio. Se pierden todos los datos cargados."));
            lista.Add(Nueva(OpcionBase_GV42.RestaurarBackup, "Restaurar un backup (.bak)",
                "La base vuelve al estado del archivo que elijas. Se pierde lo cargado después de ese backup."));
        }

        private OpcionBase_GV42 OpcionElegida()
        {
            foreach (Opcion o in _opciones)
                if (o.Radio.Checked) return o.Valor;
            return _opciones[0].Valor;
        }

        // Habilita lo que corresponde a la opción elegida.
        private void ActualizarOpciones()
        {
            if (_opciones.Count == 0) return;
            OpcionBase_GV42 elegida = OpcionElegida();
            bool restaurar = elegida == OpcionBase_GV42.RestaurarBackup;

            txtBackup.Visible = HayOpcion(OpcionBase_GV42.RestaurarBackup);
            btnBackup.Visible = txtBackup.Visible;
            txtBackup.Enabled = restaurar;
            btnBackup.Enabled = restaurar;
            grpAdministrador.Visible = InstaladorSistema_GV42.BorraDatos(elegida);
        }

        private bool HayOpcion(OpcionBase_GV42 valor)
        {
            foreach (Opcion o in _opciones)
                if (o.Valor == valor) return true;
            return false;
        }

        private void rbOpcion_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarOpciones();
        }

        #endregion

        #region Eventos de las demás pantallas

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (var dialogo = new FolderBrowserDialog())
            {
                dialogo.Description = "Carpeta donde se va a instalar el sistema";
                dialogo.SelectedPath = txtCarpeta.Text;
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                    txtCarpeta.Text = dialogo.SelectedPath;
            }
        }

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            CargarInstancias();
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            string instancia = (cmbInstancia.Text ?? string.Empty).Trim();
            lblPrueba.ForeColor = SystemColors.ControlText;
            if (instancia.Length == 0)
            {
                lblPrueba.Text = "Elegí o escribí la instancia de SQL Server.";
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                lblPrueba.Text = _instalador.ProbarConexion(instancia);
                lblPrueba.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                lblPrueba.Text = "No se pudo conectar: " + ex.Message;
                lblPrueba.ForeColor = Color.Firebrick;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Backup a restaurar";
                dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak";
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                    txtBackup.Text = dialogo.FileName;
            }
        }

        #endregion

        #region Instalación

        private PedidoInstalacion_GV42 CrearPedido()
        {
            return new PedidoInstalacion_GV42
            {
                CarpetaDestino = txtCarpeta.Text.Trim(),
                Instancia = (cmbInstancia.Text ?? string.Empty).Trim(),
                OpcionBase = OpcionElegida(),
                RutaBackup = txtBackup.Text.Trim(),
                CrearAccesoDirecto = chkAccesoDirecto.Checked,
                LoginAdministrador = txtUsuario.Text.Trim(),
                ContrasenaAdministrador = txtContrasena.Text
            };
        }

        // Lo que se muestra en "Listo para instalar".
        private string ArmarResumen()
        {
            PedidoInstalacion_GV42 p = CrearPedido();
            string nl = Environment.NewLine;
            string resumen = "Carpeta de destino:" + nl + "      " + p.CarpetaDestino + nl + nl +
                             "Base de datos:" + nl +
                             "      Servidor: " + p.Instancia + nl +
                             "      Base: Gestion Usuario" + nl +
                             "      Datos: " + InstaladorSistema_GV42.TextoOpcion(p.OpcionBase) + nl;
            if (p.OpcionBase == OpcionBase_GV42.RestaurarBackup)
                resumen += "      Backup: " + p.RutaBackup + nl;
            resumen += nl + "Tareas adicionales:" + nl +
                       "      " + (p.CrearAccesoDirecto ? "Crear un acceso directo en el escritorio" : "Ninguna");
            return resumen;
        }

        private void Instalar()
        {
            PedidoInstalacion_GV42 pedido = CrearPedido();

            if (InstaladorSistema_GV42.BorraDatos(pedido.OpcionBase))
            {
                string aviso = pedido.OpcionBase == OpcionBase_GV42.RestaurarBackup
                    ? "La base de datos va a volver al estado del backup elegido. Se pierde todo lo cargado después de ese backup."
                    : "La base de datos se va a borrar y crear de nuevo. Se pierden todos los datos cargados.";
                if (MessageBox.Show(this, aviso + "\n\n¿Continuar?", Text, MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
            }

            txtProgreso.Clear();
            IrA(PAG_PROGRESO);
            pbProgreso.Style = ProgressBarStyle.Marquee;
            btnCancelar.Enabled = false;
            _ejecutando = true;
            Cursor = Cursors.WaitCursor;

            try
            {
                _instalador.Instalar(pedido, Avisar);

                _terminado = true;
                lblTituloPagina.Text = "Instalación completada";
                lblSubtituloPagina.Text = InstaladorSistema_GV42.NOMBRE_SISTEMA + " quedó instalado correctamente.";
                lblProgreso.Text = "La instalación terminó. Haga clic en Finalizar para salir.";
                pbProgreso.Style = ProgressBarStyle.Continuous;
                pbProgreso.Value = pbProgreso.Maximum;
                btnSiguiente.Enabled = true; // "Finalizar"
            }
            catch (Exception ex)
            {
                string motivo = ex is NegocioException_GV42 ? "No se pudo continuar: " : "Error: ";
                Avisar(motivo + ex.Message);
                lblTituloPagina.Text = "La instalación no se completó";
                lblSubtituloPagina.Text = "Revisá el detalle, volvé atrás y corregí lo que haga falta.";
                lblProgreso.Text = "La instalación se detuvo.";
                pbProgreso.Style = ProgressBarStyle.Continuous;
                pbProgreso.Value = 0;
                btnAtras.Enabled = true;
                btnCancelar.Enabled = true;
                MessageBox.Show(this, ex is NegocioException_GV42 ? ex.Message : "La instalación no se completó.\n\n" + ex.Message,
                                Text, MessageBoxButtons.OK,
                                ex is NegocioException_GV42 ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
            }
            finally
            {
                _ejecutando = false;
                Cursor = Cursors.Default;
            }
        }

        // Agrega una línea al cuadro de avance y refresca la pantalla (la instalación corre en el
        // mismo hilo que la ventana).
        private void Avisar(string texto)
        {
            txtProgreso.AppendText(DateTime.Now.ToString("HH:mm:ss") + "  " + texto + Environment.NewLine);
            Application.DoEvents();
        }

        #endregion
    }
}
