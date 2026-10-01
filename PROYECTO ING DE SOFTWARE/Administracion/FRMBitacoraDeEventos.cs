using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    public partial class FRMBitacoraDeEventos : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLBitacora_GV42 _bllBitacora;

        private readonly BLLUsuario_GV42 _bllUsuario;

        // Listas de los combos de filtro: la posición 0 es siempre la opción "(Todos)" (sin filtro),
        // que se traduce y se regenera al cambiar el idioma.
        private List<string> _modulos;
        private List<string> _eventos;
        private List<string> _criticidades;

        #endregion

        #region Constructor

        public FRMBitacoraDeEventos()
        {
            InitializeComponent();
            _bllBitacora = BLLBitacora_GV42.Instancia;
            _bllUsuario = new BLLUsuario_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            this.FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);

            ActualizarIdioma();
            AplicarPermisos();
        }

        #endregion

        #region Permisos

        private void AplicarPermisos()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            var bllPermisos = new BLLPermisos_GV42();
            Rol_GV42 rolCompleto = bllPermisos.ObtenerArbolRol(actual.Rol.Id);
            if (rolCompleto == null) return;

            bool puedeVer = rolCompleto.TienePermiso("Bitacora.Ver");
            bool puedeExportar = rolCompleto.TienePermiso("Bitacora.ExportarPDF");

            lblLogin.Visible = puedeVer;
            txtLogin.Visible = puedeVer;
            lblModulo.Visible = puedeVer;
            cboModulo.Visible = puedeVer;
            lblEvento.Visible = puedeVer;
            cboEvento.Visible = puedeVer;
            lblCriticidad.Visible = puedeVer;
            cboCriticidad.Visible = puedeVer;
            lblFechaInicio.Visible = puedeVer;
            dtpFechaInicio.Visible = puedeVer;
            lblFechaFin.Visible = puedeVer;
            dtpFechaFin.Visible = puedeVer;
            btnAplicar.Visible = puedeVer;
            btnLimpiar.Visible = puedeVer;

            lblNombre.Visible = puedeVer;
            txtNombreUsuario.Visible = puedeVer;
            lblApellido.Visible = puedeVer;
            txtApellidoUsuario.Visible = puedeVer;

            btnImprimir.Visible = puedeExportar;
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            this.Text = IdiomaManager_GV42.T("bitacora.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("bitacora.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("bitacora.subtitulo");

            lblLogin.Text = IdiomaManager_GV42.T("bitacora.usuario");
            lblModulo.Text = IdiomaManager_GV42.T("bitacora.modulo");
            lblEvento.Text = IdiomaManager_GV42.T("bitacora.evento");
            lblFechaInicio.Text = IdiomaManager_GV42.T("bitacora.fechaInicio");
            lblFechaFin.Text = IdiomaManager_GV42.T("bitacora.fechaFin");
            lblCriticidad.Text = IdiomaManager_GV42.T("bitacora.criticidad");
            lblNombre.Text = IdiomaManager_GV42.T("bitacora.nombre");
            lblApellido.Text = IdiomaManager_GV42.T("bitacora.apellido");

            btnAplicar.Text = IdiomaManager_GV42.T("bitacora.aplicar");
            btnLimpiar.Text = IdiomaManager_GV42.T("bitacora.limpiar");
            btnImprimir.Text = IdiomaManager_GV42.T("bitacora.imprimir");
            btnCancelar.Text = IdiomaManager_GV42.T("bitacora.salir");

            RefrescarOpcionTodos(cboModulo, _modulos);
            RefrescarOpcionTodos(cboEvento, _eventos);
            RefrescarOpcionTodos(cboCriticidad, _criticidades);

            ConfigurarColumnas();
        }

        private void RefrescarOpcionTodos(ComboBox cbo, List<string> lista)
        {
            if (lista == null || lista.Count == 0) return;
            int seleccion = cbo.SelectedIndex;
            lista[0] = IdiomaManager_GV42.T("bitacora.todos");
            cbo.DataSource = null;
            cbo.DataSource = lista;
            if (seleccion < cbo.Items.Count) cbo.SelectedIndex = seleccion;
        }

        #endregion

        #region Carga de datos

        private void EstablecerFechasPorDefecto()
        {
            // Por defecto se muestran los últimos 3 días, pero se puede consultar cualquier fecha
            // anterior (antes el MinDate impedía ver eventos de más de 3 días). No se permiten fechas futuras.
            DateTime hoy = DateTime.Now.Date;
            DateTime hace3Dias = hoy.AddDays(-3);

            dtpFechaInicio.MinDate = new DateTime(2000, 1, 1);
            dtpFechaInicio.MaxDate = hoy;
            dtpFechaInicio.Value = hace3Dias;
            dtpFechaFin.MinDate = new DateTime(2000, 1, 1);
            dtpFechaFin.MaxDate = hoy;
            dtpFechaFin.Value = hoy;
        }

        private void CargarCombos()
        {
            string todos = IdiomaManager_GV42.T("bitacora.todos");

            _modulos = new List<string> { todos };
            _modulos.AddRange(_bllBitacora.ListarModulos());
            cboModulo.DataSource = _modulos;

            _eventos = new List<string> { todos };
            _eventos.AddRange(_bllBitacora.ListarTiposEvento());
            cboEvento.DataSource = _eventos;

            _criticidades = new List<string> { todos };
            _criticidades.AddRange(_bllBitacora.ListarCriticidades());
            cboCriticidad.DataSource = _criticidades;
        }

        private void CargarGrillaPorDefecto()
        {
            DateTime fechaFinReal = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);
            CargarGrilla(_bllBitacora.Filtrar(
                login: null,
                modulo: null,
                tipoEvento: null,
                criticidad: null,
                fechaInicio: dtpFechaInicio.Value.Date,
                fechaFin: fechaFinReal));
        }

        private void CargarGrilla(List<Bitacora_GV42> registros)
        {
            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = registros;
            ConfigurarColumnas();
            if (dgvBitacora.Rows.Count > 0)
            {
                dgvBitacora.ClearSelection();
                dgvBitacora.Rows[0].Selected = true;
                dgvBitacora.CurrentCell = dgvBitacora.Rows[0].Cells[0];
            }
            else
            {
                txtNombreUsuario.Text = "";
                txtApellidoUsuario.Text = "";
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvBitacora.Columns.Count == 0) return;
            string[] aOcultar = { "Modulo", "TipoEvento" };
            foreach (string c in aOcultar)
                if (dgvBitacora.Columns.Contains(c)) dgvBitacora.Columns[c].Visible = false;

            if (dgvBitacora.Columns.Contains("Login")) dgvBitacora.Columns["Login"].HeaderText = IdiomaManager_GV42.T("bitacora.usuario");
            if (dgvBitacora.Columns.Contains("ModuloNombre")) dgvBitacora.Columns["ModuloNombre"].HeaderText = IdiomaManager_GV42.T("bitacora.modulo");
            if (dgvBitacora.Columns.Contains("TipoEventoNombre")) dgvBitacora.Columns["TipoEventoNombre"].HeaderText = IdiomaManager_GV42.T("bitacora.tipoEvento");
            if (dgvBitacora.Columns.Contains("Detalle")) dgvBitacora.Columns["Detalle"].HeaderText = IdiomaManager_GV42.T("bitacora.detalle");
            if (dgvBitacora.Columns.Contains("Evento")) dgvBitacora.Columns["Evento"].Visible = false;
            if (dgvBitacora.Columns.Contains("Criticidad")) dgvBitacora.Columns["Criticidad"].HeaderText = IdiomaManager_GV42.T("bitacora.criticidad");
            if (dgvBitacora.Columns.Contains("FechaHora"))
            {
                dgvBitacora.Columns["FechaHora"].HeaderText = IdiomaManager_GV42.T("bitacora.fechaHora");
                dgvBitacora.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        private string ValorCelda(DataGridViewRow fila, string columna)
        {
            if (!dgvBitacora.Columns.Contains(columna)) return "";
            object val = fila.Cells[columna].Value;
            if (val == null) return "";
            if (val is DateTime dt) return dt.ToString("dd/MM/yyyy HH:mm:ss");
            return val.ToString();
        }

        // La posición 0 de cada combo es "(Todos)": equivale a no filtrar.
        private string ValorCombo(ComboBox cbo)
        {
            if (cbo.SelectedIndex <= 0) return null;
            return cbo.SelectedItem?.ToString();
        }

        #endregion

        #region Eventos

        private void FRMBitacoraDeEventos_Load(object sender, EventArgs e)
        {
            txtLogin.MaxLength = Validaciones_GV42.MAX_LOGIN;
            try
            {
                CargarCombos();
                EstablecerFechasPorDefecto();
                CargarGrillaPorDefecto();
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("bitacora.accionCargar"), ex);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (dtpFechaFin.Value < dtpFechaInicio.Value)
            {
                MessageBox.Show(IdiomaManager_GV42.T("bitacora.fechaInvalida"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string login = txtLogin.Text.Trim();
            string modulo = ValorCombo(cboModulo);
            string evento = ValorCombo(cboEvento);
            string criticidad = ValorCombo(cboCriticidad);
            DateTime fechaFinReal = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);
            try
            {
                List<Bitacora_GV42> resultados = _bllBitacora.Filtrar(
                    login, modulo, evento, criticidad,
                    dtpFechaInicio.Value.Date, fechaFinReal);
                CargarGrilla(resultados);
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("bitacora.accionFiltrar"), ex);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtLogin.Text = "";
            cboModulo.SelectedIndex = 0;
            cboEvento.SelectedIndex = 0;
            cboCriticidad.SelectedIndex = 0;
            EstablecerFechasPorDefecto();
            try { CargarGrillaPorDefecto(); }
            catch (Exception ex) { Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("bitacora.accionCargar"), ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvBitacora.Rows.Count == 0)
            {
                MessageBox.Show(IdiomaManager_GV42.T("bitacora.sinRegistros"),
                                IdiomaManager_GV42.T("general.informacion"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("bitacora.sfdFiltro");
                sfd.Title = IdiomaManager_GV42.T("bitacora.sfdTitulo");
                sfd.FileName = $"Bitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    string[] headers = {
                        IdiomaManager_GV42.T("bitacora.usuario"),
                        IdiomaManager_GV42.T("bitacora.modulo"),
                        IdiomaManager_GV42.T("bitacora.tipoEvento"),
                        IdiomaManager_GV42.T("bitacora.detalle"),
                        IdiomaManager_GV42.T("bitacora.criticidad"),
                        IdiomaManager_GV42.T("bitacora.fechaHora")
                    };

                    float[] proporciones = { 0.12f, 0.12f, 0.22f, 0.22f, 0.10f, 0.22f };

                    List<string[]> filas = new List<string[]>();
                    foreach (DataGridViewRow row in dgvBitacora.Rows)
                    {
                        if (row.IsNewRow) continue;
                        filas.Add(new string[]
                        {
                            ValorCelda(row, "Login"),
                            ValorCelda(row, "ModuloNombre"),
                            ValorCelda(row, "TipoEventoNombre"),
                            ValorCelda(row, "Detalle"),
                            ValorCelda(row, "Criticidad"),
                            ValorCelda(row, "FechaHora")
                        });
                    }

                    string subtitulo = IdiomaManager_GV42.T("bitacora.pdfSubtitulo",
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), filas.Count);

                    GeneradorPdf_GV42 generador = new GeneradorPdf_GV42();
                    generador.Generar(sfd.FileName, IdiomaManager_GV42.T("bitacora.pdfTitulo"), subtitulo,
                                      headers, proporciones, filas);

                    string mensaje = $"{IdiomaManager_GV42.T("bitacora.pdfGenerado")}\n{sfd.FileName}\n\n{IdiomaManager_GV42.T("bitacora.pdfAbrirAhora")}";
                    DialogResult abrir = MessageBox.Show(mensaje,
                                                         IdiomaManager_GV42.T("general.exito"),
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (abrir == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager_GV42.T("bitacora.errorPdf") + "\n\n" + ex.Message,
                                    IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvBitacora_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBitacora.CurrentRow == null) return;
            Bitacora_GV42 registro = dgvBitacora.CurrentRow.DataBoundItem as Bitacora_GV42;
            if (registro == null) return;

            try
            {
                Usuario_GV42 usuario = _bllUsuario.BuscarPorLogin(registro.Login);
                if (usuario != null)
                {
                    txtNombreUsuario.Text = usuario.Nombre;
                    txtApellidoUsuario.Text = usuario.Apellido;
                }
                else
                {
                    txtNombreUsuario.Text = IdiomaManager_GV42.T("bitacora.usuarioInexistente");
                    txtApellidoUsuario.Text = "";
                }
            }
            catch
            {
                txtNombreUsuario.Text = "";
                txtApellidoUsuario.Text = "";
            }
        }

        #endregion
    }
}
