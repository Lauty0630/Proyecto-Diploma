using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Gestión de permisos (Composite): catálogo de patentes, familias y roles.
    // El diseño está en FRMGestionPermisos.Designer.cs (Form Designer); acá va solo la lógica.
    public partial class FRMGestionPermisos : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLPermisos_GV42 _bll;

        private string _modoFamilia = "Crear";
        private int _idFamiliaEdicion = 0;
        private string _nombreFamiliaEdicion = string.Empty;

        private string _modoRol = "Crear";
        private int _idRolEdicion = 0;
        private string _nombreRolEdicion = string.Empty;

        #endregion

        #region Constructor

        public FRMGestionPermisos()
        {
            InitializeComponent();
            _bll = new BLLPermisos_GV42();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("permisos.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("permisos.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("permisos.subtitulo");

            tabPatentes.Text = IdiomaManager_GV42.T("permisos.tabPatentes");
            tabFamilias.Text = IdiomaManager_GV42.T("permisos.tabFamilias");
            tabRoles.Text = IdiomaManager_GV42.T("permisos.tabRoles");

            lblTitPatentes.Text = IdiomaManager_GV42.T("permisos.titPatentes");

            lblTitFamilias.Text = IdiomaManager_GV42.T("permisos.titFamilias");
            btnModificarFamilia.Text = IdiomaManager_GV42.T("permisos.modificarFamilia");
            btnEliminarFamilia.Text = IdiomaManager_GV42.T("permisos.eliminarFamilia");
            lblNombreFamilia.Text = IdiomaManager_GV42.T("permisos.nombre");
            lblPatentesFamilia.Text = IdiomaManager_GV42.T("permisos.patentesAIncluir");
            lblSubfamilias.Text = IdiomaManager_GV42.T("permisos.subfamilias");
            btnGuardarFamilia.Text = IdiomaManager_GV42.T("permisos.guardar");
            ActualizarTextosModoFamilia();

            lblTitRoles.Text = IdiomaManager_GV42.T("permisos.titRoles");
            btnModificarRol.Text = IdiomaManager_GV42.T("permisos.modificarRol");
            btnEliminarRol.Text = IdiomaManager_GV42.T("permisos.eliminarRol");
            lblNombreRol.Text = IdiomaManager_GV42.T("permisos.nombre");
            lblPatentesRol.Text = IdiomaManager_GV42.T("permisos.patentesIndividuales");
            lblFamiliasRol.Text = IdiomaManager_GV42.T("permisos.familias");
            btnGuardarRol.Text = IdiomaManager_GV42.T("permisos.guardar");
            ActualizarTextosModoRol();

            // Las pestañas son dibujadas a mano: se repintan para mostrar el texto nuevo.
            tabControl.Invalidate();

            // Nombres de patentes y encabezados de grillas. No se recarga en medio de una edición:
            // vaciaba los checklists que se estaban modificando.
            if (dgvPatentes.Columns.Count > 0 && _idFamiliaEdicion == 0 && _idRolEdicion == 0)
            {
                try { RecargarTodo(); }
                catch (Exception ex) { Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("permisos.accionRecargar"), ex); }
            }
            else
            {
                AplicarHeadersPatentes();
                AplicarHeadersFamilias();
                AplicarHeadersRoles();
            }
        }

        // Título de la tarjeta de alta/edición y texto del botón Limpiar/Cancelar según el modo.
        private void ActualizarTextosModoFamilia()
        {
            lblCrearFamilia.Text = _modoFamilia == "Modificar"
                ? $"{IdiomaManager_GV42.T("permisos.editandoFamilia")} {_nombreFamiliaEdicion}"
                : (_modoFamilia == "Eliminar"
                    ? IdiomaManager_GV42.T("permisos.confirmarEliminacion")
                    : IdiomaManager_GV42.T("permisos.crearFamilia"));
            btnLimpiarFamilia.Text = _modoFamilia == "Crear"
                ? IdiomaManager_GV42.T("permisos.limpiar")
                : IdiomaManager_GV42.T("general.cancelar");
        }

        private void ActualizarTextosModoRol()
        {
            lblCrearRol.Text = _modoRol == "Modificar"
                ? $"{IdiomaManager_GV42.T("permisos.editandoRol")} {_nombreRolEdicion}"
                : (_modoRol == "Eliminar"
                    ? IdiomaManager_GV42.T("permisos.confirmarEliminacion")
                    : IdiomaManager_GV42.T("permisos.crearRol"));
            btnLimpiarRol.Text = _modoRol == "Crear"
                ? IdiomaManager_GV42.T("permisos.limpiar")
                : IdiomaManager_GV42.T("general.cancelar");
        }

        private string TraducirNombrePatente(Patente_GV42 p)
        {
            if (p == null || string.IsNullOrEmpty(p.DataKey)) return p?.Nombre ?? string.Empty;
            string clave = "patente." + p.DataKey;
            string traducido = IdiomaManager_GV42.T(clave);
            return traducido == clave ? p.Nombre : traducido;
        }

        private void AplicarHeadersPatentes()
        {
            if (dgvPatentes.Columns.Contains("Nombre"))
                dgvPatentes.Columns["Nombre"].HeaderText = IdiomaManager_GV42.T("permisos.colNombre");
            if (dgvPatentes.Columns.Contains("DataKey"))
                dgvPatentes.Columns["DataKey"].HeaderText = IdiomaManager_GV42.T("permisos.colDataKey");
        }

        private void AplicarHeadersFamilias()
        {
            if (dgvFamilias.Columns.Contains("Nombre"))
                dgvFamilias.Columns["Nombre"].HeaderText = IdiomaManager_GV42.T("permisos.colNombre");
        }

        private void AplicarHeadersRoles()
        {
            if (dgvRoles.Columns.Contains("Nombre"))
                dgvRoles.Columns["Nombre"].HeaderText = IdiomaManager_GV42.T("permisos.colNombre");
        }

        #endregion

        #region Permisos

        private void AplicarPermisosTabs()
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            if (actual == null || actual.Rol == null) return;

            Rol_GV42 rolCompleto = _bll.ObtenerArbolRol(actual.Rol.Id);
            if (rolCompleto == null) return;

            var dataKeys = rolCompleto.ObtenerPatentes()
                .Select(p => p.DataKey ?? string.Empty)
                .ToList();

            bool puedePatentes = dataKeys.Contains("Permisos.Patentes");
            bool puedeFamilias = dataKeys.Contains("Permisos.Familias");
            bool puedeRoles = dataKeys.Contains("Permisos.Roles");

            if (tabControl.TabPages.Contains(tabPatentes)) tabControl.TabPages.Remove(tabPatentes);
            if (tabControl.TabPages.Contains(tabFamilias)) tabControl.TabPages.Remove(tabFamilias);
            if (tabControl.TabPages.Contains(tabRoles)) tabControl.TabPages.Remove(tabRoles);

            if (puedePatentes) tabControl.TabPages.Add(tabPatentes);
            if (puedeFamilias) tabControl.TabPages.Add(tabFamilias);
            if (puedeRoles) tabControl.TabPages.Add(tabRoles);
        }

        private bool EsRolDelUsuarioActual(int idRol)
        {
            Usuario_GV42 actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
            return actual != null && actual.Rol != null && actual.Rol.Id == idRol;
        }

        private void ForzarReloginPorCambioDePropioRol()
        {
            MessageBox.Show(
                IdiomaManager_GV42.T("permisos.mensajeRolPropio"),
                IdiomaManager_GV42.T("permisos.tituloRolPropio"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            BLLUsuario_GV42.CerrarSesión();

            var login = new FRMIniciarSesion();
            login.Show();

            var menuPrincipal = Application.OpenForms
                .OfType<FRMMenuPrincipalAdmin>()
                .FirstOrDefault();
            if (menuPrincipal != null)
                menuPrincipal.Close();
        }

        #endregion

        #region Carga de datos

        private void RecargarTodo()
        {
            CargarPatentes();
            CargarFamilias();
            CargarRoles();
        }

        private void CargarPatentes()
        {
            List<Patente_GV42> patentes = _bll.ListarPatentes();

            foreach (var p in patentes)
                p.Nombre = TraducirNombrePatente(p);

            dgvPatentes.DataSource = null;
            dgvPatentes.DataSource = patentes;
            if (dgvPatentes.Columns.Contains("Id"))
                dgvPatentes.Columns["Id"].Visible = false;
            if (dgvPatentes.Columns.Contains("DataKey"))
                dgvPatentes.Columns["DataKey"].Visible = false;
            AplicarHeadersPatentes();

            clbPatentesFamilia.Items.Clear();
            clbPatentesRol.Items.Clear();
            foreach (var p in patentes)
            {
                clbPatentesFamilia.Items.Add(p, false);
                clbPatentesRol.Items.Add(p, false);
            }
        }

        private void CargarFamilias()
        {
            List<Familia_GV42> familias = _bll.ListarFamilias();

            dgvFamilias.DataSource = null;
            dgvFamilias.DataSource = familias;
            if (dgvFamilias.Columns.Contains("Id"))
                dgvFamilias.Columns["Id"].Visible = false;
            if (dgvFamilias.Columns.Contains("Hijos"))
                dgvFamilias.Columns["Hijos"].Visible = false;
            AplicarHeadersFamilias();

            clbSubfamilias.Items.Clear();
            clbFamiliasRol.Items.Clear();
            foreach (var f in familias)
            {
                clbSubfamilias.Items.Add(f, false);
                clbFamiliasRol.Items.Add(f, false);
            }
        }

        private void CargarRoles()
        {
            List<Rol_GV42> roles = _bll.ListarRoles();
            dgvRoles.DataSource = null;
            dgvRoles.DataSource = roles;
            if (dgvRoles.Columns.Contains("Id"))
                dgvRoles.Columns["Id"].Visible = false;
            if (dgvRoles.Columns.Contains("Hijos"))
                dgvRoles.Columns["Hijos"].Visible = false;
            AplicarHeadersRoles();
        }

        #endregion

        #region Familias

        private void CrearFamilia()
        {
            string nombre = txtNombreFamilia.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.ingresaNombreFamilia"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreFamilia.Focus();
                return;
            }

            List<int> idsPatentes = clbPatentesFamilia.CheckedItems.Cast<Patente_GV42>().Select(p => p.Id).ToList();
            List<int> idsSubfamilias = clbSubfamilias.CheckedItems.Cast<Familia_GV42>().Select(f => f.Id).ToList();

            try
            {
                _bll.CrearFamilia(nombre, idsPatentes, idsSubfamilias);
                MessageBox.Show(IdiomaManager_GV42.T("permisos.familiaCreada"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearFamilia();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarFamilia()
        {
            string nombre = txtNombreFamilia.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.ingresaNombreFamilia"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreFamilia.Focus();
                return;
            }

            List<int> idsPatentes = clbPatentesFamilia.CheckedItems.Cast<Patente_GV42>().Select(p => p.Id).ToList();
            List<int> idsSubfamilias = clbSubfamilias.CheckedItems.Cast<Familia_GV42>().Select(f => f.Id).ToList();

            try
            {
                _bll.ModificarFamilia(_idFamiliaEdicion, nombre, idsPatentes, idsSubfamilias);
                MessageBox.Show(IdiomaManager_GV42.T("permisos.familiaModificada"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearFamilia();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarFamilia()
        {
            // Se usa el Id guardado al entrar en modo Eliminar (no la fila actual: si la grilla se recargó,
            // por ejemplo al cambiar el idioma, CurrentRow podía ser otra familia).
            Familia_GV42 fam = _bll.ListarFamilias().FirstOrDefault(f => f.Id == _idFamiliaEdicion);
            if (fam == null) { VolverAModoCrearFamilia(); return; }

            DialogResult r = MessageBox.Show(
                $"{IdiomaManager_GV42.T("permisos.confirmEliminarFamilia")} '{fam.Nombre}'?",
                IdiomaManager_GV42.T("permisos.tituloEliminar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) { VolverAModoCrearFamilia(); return; }

            try
            {
                _bll.EliminarFamilia(fam.Id);
                MessageBox.Show(IdiomaManager_GV42.T("permisos.familiaEliminada"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearFamilia();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("permisos.noSePuedeEliminar"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EntrarAModoFamilia(string modo, int idFamilia)
        {
            _modoFamilia = modo;
            _idFamiliaEdicion = idFamilia;
            _nombreFamiliaEdicion = txtNombreFamilia.Text;
            btnEliminarFamilia.Enabled = false;
            btnModificarFamilia.Enabled = false;
            dgvFamilias.Enabled = false;
            ActualizarTextosModoFamilia();
        }

        private void VolverAModoCrearFamilia()
        {
            _modoFamilia = "Crear";
            _idFamiliaEdicion = 0;
            _nombreFamiliaEdicion = string.Empty;
            txtNombreFamilia.Clear();
            for (int i = 0; i < clbPatentesFamilia.Items.Count; i++) clbPatentesFamilia.SetItemChecked(i, false);
            for (int i = 0; i < clbSubfamilias.Items.Count; i++) clbSubfamilias.SetItemChecked(i, false);
            btnEliminarFamilia.Enabled = true;
            btnModificarFamilia.Enabled = true;
            dgvFamilias.Enabled = true;
            ActualizarTextosModoFamilia();
        }

        #endregion

        #region Roles

        private void CrearRol()
        {
            string nombre = txtNombreRol.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.ingresaNombreRol"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreRol.Focus();
                return;
            }

            List<int> idsPatentes = clbPatentesRol.CheckedItems.Cast<Patente_GV42>().Select(p => p.Id).ToList();
            List<int> idsFamilias = clbFamiliasRol.CheckedItems.Cast<Familia_GV42>().Select(f => f.Id).ToList();

            try
            {
                _bll.CrearRol(nombre, idsPatentes, idsFamilias);
                MessageBox.Show(IdiomaManager_GV42.T("permisos.rolCreado"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearRol();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarRol()
        {
            string nombre = txtNombreRol.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.ingresaNombreRol"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreRol.Focus();
                return;
            }

            List<int> idsPatentes = clbPatentesRol.CheckedItems.Cast<Patente_GV42>().Select(p => p.Id).ToList();
            List<int> idsFamilias = clbFamiliasRol.CheckedItems.Cast<Familia_GV42>().Select(f => f.Id).ToList();

            try
            {
                _bll.ModificarRol(_idRolEdicion, nombre, idsPatentes, idsFamilias);

                if (EsRolDelUsuarioActual(_idRolEdicion))
                {
                    ForzarReloginPorCambioDePropioRol();
                    return;
                }

                MessageBox.Show(IdiomaManager_GV42.T("permisos.rolModificado"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearRol();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("general.error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarRol()
        {
            Rol_GV42 rol = _bll.ListarRoles().FirstOrDefault(x => x.Id == _idRolEdicion);
            if (rol == null) { VolverAModoCrearRol(); return; }

            DialogResult r = MessageBox.Show(
                $"{IdiomaManager_GV42.T("permisos.confirmEliminarRol")} '{rol.Nombre}'?",
                IdiomaManager_GV42.T("permisos.tituloEliminar"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) { VolverAModoCrearRol(); return; }

            try
            {
                _bll.EliminarRol(rol.Id);
                MessageBox.Show(IdiomaManager_GV42.T("permisos.rolEliminado"),
                                IdiomaManager_GV42.T("general.exito"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                VolverAModoCrearRol();
                RecargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IdiomaManager_GV42.T("permisos.noSePuedeEliminar"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EntrarAModoRol(string modo, int idRol)
        {
            _modoRol = modo;
            _idRolEdicion = idRol;
            _nombreRolEdicion = txtNombreRol.Text;
            btnEliminarRol.Enabled = false;
            btnModificarRol.Enabled = false;
            dgvRoles.Enabled = false;
            ActualizarTextosModoRol();
        }

        private void VolverAModoCrearRol()
        {
            _modoRol = "Crear";
            _idRolEdicion = 0;
            _nombreRolEdicion = string.Empty;
            txtNombreRol.Clear();
            for (int i = 0; i < clbPatentesRol.Items.Count; i++) clbPatentesRol.SetItemChecked(i, false);
            for (int i = 0; i < clbFamiliasRol.Items.Count; i++) clbFamiliasRol.SetItemChecked(i, false);
            btnEliminarRol.Enabled = true;
            btnModificarRol.Enabled = true;
            dgvRoles.Enabled = true;
            ActualizarTextosModoRol();
        }

        #endregion

        #region Eventos

        private void FRMGestionPermisos_Load(object sender, EventArgs e)
        {
            // Largo máximo = columnas Roles.Nombre (50) y Familia.Nombre (100).
            txtNombreFamilia.MaxLength = Validaciones_GV42.MAX_NOMBRE_FAMILIA;
            txtNombreRol.MaxLength = Validaciones_GV42.MAX_NOMBRE_ROL;
            try
            {
                AplicarPermisosTabs();
                // Los textos ya se tradujeron en el constructor (ActualizarIdioma); acá solo se cargan los datos.
                RecargarTodo();
            }
            catch (Exception ex)
            {
                Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("permisos.accionCargar"), ex);
            }
        }

        // Pestañas dibujadas a mano (DrawMode = OwnerDrawFixed en el diseñador):
        // la seleccionada en azul con texto blanco, las demás blancas con texto azul.
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabPage page = tc.TabPages[e.Index];
            Rectangle rect = tc.GetTabRect(e.Index);
            bool seleccionada = (e.Index == tc.SelectedIndex);

            using (SolidBrush fondo = new SolidBrush(seleccionada ? Tema_GV42.Primario : Color.White))
                e.Graphics.FillRectangle(fondo, rect);

            using (Pen borde = new Pen(seleccionada ? Tema_GV42.Primario : Tema_GV42.BordeGrilla, 1))
                e.Graphics.DrawRectangle(borde, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tc.Font,
                rect,
                seleccionada ? Color.White : Tema_GV42.Acento,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void btnGuardarFamilia_Click(object sender, EventArgs e)
        {
            switch (_modoFamilia)
            {
                case "Crear": CrearFamilia(); break;
                case "Modificar": ModificarFamilia(); break;
                case "Eliminar": EliminarFamilia(); break;
            }
        }

        private void btnModificarFamilia_Click(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.seleccioneFamilia"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Familia_GV42 fam = dgvFamilias.CurrentRow.DataBoundItem as Familia_GV42;
            if (fam == null) return;

            Familia_GV42 arbol;
            try { arbol = _bll.ObtenerArbolFamilia(fam.Id); }
            catch (Exception ex) { Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("permisos.accionLeerFamilia"), ex); return; }
            if (arbol == null) return;

            txtNombreFamilia.Text = arbol.Nombre;

            HashSet<int> idsPat = new HashSet<int>(arbol.Hijos.OfType<Patente_GV42>().Select(p => p.Id));
            HashSet<int> idsSub = new HashSet<int>(arbol.Hijos.OfType<Familia_GV42>().Select(f => f.Id));

            for (int i = 0; i < clbPatentesFamilia.Items.Count; i++)
            {
                var p = (Patente_GV42)clbPatentesFamilia.Items[i];
                clbPatentesFamilia.SetItemChecked(i, idsPat.Contains(p.Id));
            }
            for (int i = 0; i < clbSubfamilias.Items.Count; i++)
            {
                var f = (Familia_GV42)clbSubfamilias.Items[i];
                clbSubfamilias.SetItemChecked(i, idsSub.Contains(f.Id));
            }

            EntrarAModoFamilia("Modificar", fam.Id);
        }

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.seleccioneFamilia"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EntrarAModoFamilia("Eliminar", ((Familia_GV42)dgvFamilias.CurrentRow.DataBoundItem).Id);
        }

        private void btnLimpiarFamilia_Click(object sender, EventArgs e)
        {
            VolverAModoCrearFamilia();
        }

        private void btnGuardarRol_Click(object sender, EventArgs e)
        {
            switch (_modoRol)
            {
                case "Crear": CrearRol(); break;
                case "Modificar": ModificarRol(); break;
                case "Eliminar": EliminarRol(); break;
            }
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            if (dgvRoles.CurrentRow == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.seleccioneRol"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Rol_GV42 rol = dgvRoles.CurrentRow.DataBoundItem as Rol_GV42;
            if (rol == null) return;

            Rol_GV42 arbol;
            try { arbol = _bll.ObtenerArbolRol(rol.Id); }
            catch (Exception ex) { Tema_GV42.MostrarErrorInesperado(IdiomaManager_GV42.T("permisos.accionLeerRol"), ex); return; }
            if (arbol == null) return;

            txtNombreRol.Text = arbol.Nombre;

            HashSet<int> idsPat = new HashSet<int>(arbol.Hijos.OfType<Patente_GV42>().Select(p => p.Id));
            HashSet<int> idsFam = new HashSet<int>(arbol.Hijos.OfType<Familia_GV42>().Select(f => f.Id));

            for (int i = 0; i < clbPatentesRol.Items.Count; i++)
            {
                var p = (Patente_GV42)clbPatentesRol.Items[i];
                clbPatentesRol.SetItemChecked(i, idsPat.Contains(p.Id));
            }
            for (int i = 0; i < clbFamiliasRol.Items.Count; i++)
            {
                var f = (Familia_GV42)clbFamiliasRol.Items[i];
                clbFamiliasRol.SetItemChecked(i, idsFam.Contains(f.Id));
            }

            EntrarAModoRol("Modificar", rol.Id);
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (dgvRoles.CurrentRow == null)
            {
                MessageBox.Show(IdiomaManager_GV42.T("permisos.seleccioneRol"),
                                IdiomaManager_GV42.T("general.advertencia"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EntrarAModoRol("Eliminar", ((Rol_GV42)dgvRoles.CurrentRow.DataBoundItem).Id);
        }

        private void btnLimpiarRol_Click(object sender, EventArgs e)
        {
            VolverAModoCrearRol();
        }

        #endregion
    }
}
