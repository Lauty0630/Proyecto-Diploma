using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Ayuda en línea (3ra entrega): árbol de temas a la izquierda y el texto del tema a la derecha.
    // Se abre desde el menú Ayuda o con F1 (en ese caso muestra el tema de la pantalla abierta).
    // El diseño está en FRMAyuda_GV42.Designer.cs (Form Designer).
    public partial class FRMAyuda_GV42 : Form, IObservadorIdioma_GV42
    {
        #region Campos

        private readonly BLLAyuda_GV42 _bll = new BLLAyuda_GV42();
        private string _temaActual = BLLAyuda_GV42.TEMA_INICIAL;

        #endregion

        #region Constructor

        public FRMAyuda_GV42()
        {
            InitializeComponent();

            IdiomaManager_GV42.Instancia.Suscribir(this);
            FormClosed += (s, e) => IdiomaManager_GV42.Instancia.Desuscribir(this);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma (Observer)

        public void ActualizarIdioma()
        {
            Text = IdiomaManager_GV42.T("ayuda.titulo");
            lblTitulo.Text = IdiomaManager_GV42.T("ayuda.titulo");
            lblSubtitulo.Text = IdiomaManager_GV42.T("ayuda.subtitulo");
            lblTemas.Text = IdiomaManager_GV42.T("ayuda.temas");
            btnExportar.Text = IdiomaManager_GV42.T("reporte.exportar");
            btnCerrar.Text = IdiomaManager_GV42.T("ayuda.cerrar");

            CargarArbol();
            MostrarTema(_temaActual);
        }

        #endregion

        #region Temas

        // Los nodos dependen de los temas definidos en la BLL (datos), por eso se arman en código.
        private void CargarArbol()
        {
            tvTemas.BeginUpdate();
            tvTemas.Nodes.Clear();
            foreach (TemaAyuda_GV42 raiz in _bll.Hijos(null))
            {
                TreeNode nodo = tvTemas.Nodes.Add(raiz.Id, raiz.Titulo);
                foreach (TemaAyuda_GV42 hijo in _bll.Hijos(raiz.Id))
                    nodo.Nodes.Add(hijo.Id, hijo.Titulo);
            }
            tvTemas.ExpandAll();
            tvTemas.EndUpdate();
        }

        // Selecciona el tema en el árbol y muestra su texto.
        public void MostrarTema(string idTema)
        {
            TreeNode[] encontrados = tvTemas.Nodes.Find(idTema ?? BLLAyuda_GV42.TEMA_INICIAL, true);
            if (encontrados.Length == 0) encontrados = tvTemas.Nodes.Find(BLLAyuda_GV42.TEMA_INICIAL, true);
            if (encontrados.Length == 0) return;

            tvTemas.SelectedNode = encontrados[0];
            encontrados[0].EnsureVisible();
        }

        #endregion

        #region Eventos

        private void tvTemas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _temaActual = e.Node.Name;
            lblTemaTitulo.Text = e.Node.Text;
            txtTexto.Text = IdiomaManager_GV42.T("ayuda." + _temaActual + ".texto").Replace("\n", Environment.NewLine);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = IdiomaManager_GV42.T("reporte.sfdFiltro");
                sfd.Title = IdiomaManager_GV42.T("ayuda.sfdTitulo");
                sfd.FileName = IdiomaManager_GV42.T("ayuda.archivo") + ".pdf";
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    _bll.ExportarPdf(sfd.FileName);
                    if (MessageBox.Show(IdiomaManager_GV42.T("reporte.pdfGenerado", sfd.FileName), IdiomaManager_GV42.T("general.exito"),
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (NegocioException_GV42 ex)
                {
                    MessageBox.Show(ex.Message, IdiomaManager_GV42.T("reporte.errorExportar"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(IdiomaManager_GV42.T("reporte.errorPdf", ex.Message), IdiomaManager_GV42.T("general.error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
