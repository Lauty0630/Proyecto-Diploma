using BE;
using Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Panel "Ofertas para vos" del paso de adicionales (RFN 1, reporte inteligente de la 3ra entrega).
    // Muestra lo que arma BLLOfertasCliente_GV42 para el cliente: lo que le suman sus millas, los
    // servicios que suelen ir con los que ya eligió y lo que más contrata su categoría.
    // Es solo una sugerencia: el botón "Agregar" le avisa al formulario, que tilda el servicio.
    // Se arma en código (no tiene diseñador) porque su contenido depende de los datos.
    public class CtrlOfertasCliente_GV42 : UserControl
    {
        #region Constantes

        private const int ALTO_TITULO = 24;
        private const int ALTO_RESUMEN = 22;
        private const int ALTO_LINEA_TITULO = 20;
        private const int ANCHO_BOTON = 96;
        private const int MARGEN = 12;

        #endregion

        #region Campos

        private readonly Label _lblTitulo = new Label();
        private readonly Label _lblResumen = new Label();
        private readonly Panel _pnlLista = new Panel();

        private OfertasCliente_GV42 _datos;
        private Func<OfertaCliente_GV42, bool> _puedeAgregar;
        private bool _armando;

        #endregion

        #region Eventos

        // El cliente apretó "Agregar" en una oferta.
        public event EventHandler<OfertaCliente_GV42> AgregarSolicitado;

        #endregion

        #region Constructor

        public CtrlOfertasCliente_GV42()
        {
            Dock = DockStyle.Top;
            BackColor = Color.FromArgb(240, 247, 255);
            Padding = new Padding(MARGEN, 8, MARGEN, 8);
            Height = 0;
            Visible = false;

            _lblTitulo.Dock = DockStyle.Top;
            _lblTitulo.Height = ALTO_TITULO;
            _lblTitulo.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            _lblTitulo.ForeColor = Tema_GV42.Acento;
            _lblTitulo.TextAlign = ContentAlignment.MiddleLeft;

            _lblResumen.Dock = DockStyle.Top;
            _lblResumen.Height = ALTO_RESUMEN;
            _lblResumen.Font = new Font("Segoe UI", 8.75F, FontStyle.Italic);
            _lblResumen.ForeColor = Tema_GV42.TextoSecundario;
            _lblResumen.TextAlign = ContentAlignment.MiddleLeft;

            _pnlLista.Dock = DockStyle.Fill;

            // El que se agrega primero se acomoda último: la lista (Fill) primero, el título arriba de todo.
            Controls.Add(_pnlLista);
            Controls.Add(_lblResumen);
            Controls.Add(_lblTitulo);
        }

        #endregion

        #region Métodos públicos

        // puedeAgregar: el formulario dice si esa oferta se puede agregar con un clic (si no, se muestra
        // solo como información).
        public void Mostrar(OfertasCliente_GV42 datos, string titulo, Func<OfertaCliente_GV42, bool> puedeAgregar)
        {
            _datos = datos;
            _puedeAgregar = puedeAgregar;
            _lblTitulo.Text = titulo ?? string.Empty;
            Armar();
        }

        public void Ocultar()
        {
            _datos = null;
            Visible = false;
            Height = 0;
        }

        #endregion

        #region Armado

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_datos != null && !_armando) Armar();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var lapiz = new Pen(Tema_GV42.BordeGrilla))
                e.Graphics.DrawRectangle(lapiz, 0, 0, Width - 1, Height - 1);
        }

        private void Armar()
        {
            if (_datos == null) return;
            _armando = true;
            try
            {
                SuspendLayout();
                _lblResumen.Text = _datos.Resumen ?? string.Empty;

                var viejas = new Control[_pnlLista.Controls.Count];
                _pnlLista.Controls.CopyTo(viejas, 0);
                _pnlLista.Controls.Clear();
                foreach (Control vieja in viejas) vieja.Dispose();

                int ancho = Math.Max(300, Width - Padding.Horizontal);
                int altoLista = 0;

                if (_datos.Ofertas.Count == 0)
                {
                    var vacio = new Label
                    {
                        Dock = DockStyle.Top,
                        Height = 26,
                        Text = IdiomaManager_GV42.T("ofertas.sinOfertas"),
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Tema_GV42.TextoSecundario,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    _pnlLista.Controls.Add(vacio);
                    altoLista = vacio.Height;
                }
                else
                {
                    foreach (OfertaCliente_GV42 oferta in _datos.Ofertas)
                    {
                        Panel fila = CrearFila(oferta, ancho);
                        _pnlLista.Controls.Add(fila);
                        fila.BringToFront();   // con Dock Top, la primera fila queda arriba
                        altoLista += fila.Height;
                    }
                }

                Height = Padding.Vertical + ALTO_TITULO + ALTO_RESUMEN + altoLista;
                Visible = true;
                ResumeLayout(true);
                Invalidate();
            }
            finally { _armando = false; }
        }

        private Panel CrearFila(OfertaCliente_GV42 oferta, int anchoTotal)
        {
            bool agregable = oferta.Tipo != null && _puedeAgregar != null && _puedeAgregar(oferta);
            int anchoTexto = anchoTotal - (agregable ? ANCHO_BOTON + 8 : 0) - 8;

            // Servicio que no se agrega con un clic (por ejemplo la butaca preferencial): se aclara dónde se elige.
            string textoDetalle = (oferta.Detalle ?? string.Empty) +
                (!agregable && oferta.Tipo != null ? " " + IdiomaManager_GV42.T("ofertas.seEligeAparte") : string.Empty);

            var fuenteDetalle = new Font("Segoe UI", 9F);
            Size medida = TextRenderer.MeasureText(textoDetalle, fuenteDetalle,
                new Size(anchoTexto, int.MaxValue), TextFormatFlags.WordBreak);

            var fila = new Panel { Dock = DockStyle.Top, Height = ALTO_LINEA_TITULO + medida.Height + 10 };

            var detalle = new Label
            {
                Dock = DockStyle.Fill,
                Text = textoDetalle,
                Font = fuenteDetalle,
                ForeColor = Tema_GV42.Texto,
                TextAlign = ContentAlignment.TopLeft
            };
            var titulo = new Label
            {
                Dock = DockStyle.Top,
                Height = ALTO_LINEA_TITULO,
                Text = oferta.Titulo,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = Tema_GV42.Primario,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Orden de agregado: la zona Fill primero, después lo que se acomoda a los costados.
            fila.Controls.Add(detalle);
            fila.Controls.Add(titulo);

            if (agregable)
            {
                var boton = new BotonModerno_GV42
                {
                    Dock = DockStyle.Right,
                    Width = ANCHO_BOTON,
                    Text = IdiomaManager_GV42.T("ofertas.agregar"),
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                    Estilo = EstiloBoton_GV42.Secundario
                };
                OfertaCliente_GV42 destino = oferta;
                // Se avisa después de que termine el clic: al agregar, el panel se vuelve a armar y este
                // mismo botón se destruye.
                boton.Click += (s, e) =>
                {
                    var handler = AgregarSolicitado;
                    if (handler != null) BeginInvoke((Action)(() => handler(this, destino)));
                };
                fila.Controls.Add(boton);
            }

            return fila;
        }

        #endregion
    }
}
