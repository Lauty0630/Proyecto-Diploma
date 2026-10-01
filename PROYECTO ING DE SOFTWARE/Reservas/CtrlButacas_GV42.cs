using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Selección de asiento estilo cine: una grilla de botones (Fila x Letra) con un pasillo
    // entre las columnas C y D, tal como están sembrados los asientos en la base (A/F ventana,
    // B/E central, C/D pasillo). Un solo control se reutiliza para cada pasajero de la reserva.
    // El mapa se sigue armando en código porque depende de los asientos que vienen de la base.
    // Los textos de la leyenda se traducen con ActualizarIdioma (claves "butacas.*"), que llama
    // el formulario que lo contiene desde su propio Observer de idioma.
    public class CtrlButacas_GV42 : UserControl
    {
        #region Campos

        private const int ANCHO_BOTON = 40;
        private const int ALTO_BOTON = 32;
        private const int ESPACIO = 4;
        private const int ANCHO_PASILLO = 20;

        private readonly Panel _pnlGrilla;
        private readonly FlowLayoutPanel _pnlReferencias;
        private readonly Dictionary<int, Button> _botonesPorIdAsiento = new Dictionary<int, Button>();
        private List<AsientoDisponibilidad_GV42> _asientos = new List<AsientoDisponibilidad_GV42>();

        // Etiquetas de la leyenda, para poder traducirlas en caliente.
        private readonly Label _lblLibre;
        private readonly Label _lblSeleccion;
        private readonly Label _lblAsignado;
        private readonly Label _lblOcupado;

        #endregion

        #region Eventos públicos

        public event EventHandler<Asiento_GV42> AsientoClickeado;

        #endregion

        #region Constructor

        public CtrlButacas_GV42()
        {
            AutoScroll = true;
            BackColor = Color.White;

            _pnlReferencias = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 30,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(4)
            };
            _pnlReferencias.Controls.Add(CrearReferencia(Color.White, Tema_GV42.Primario, out _lblLibre));
            _pnlReferencias.Controls.Add(CrearReferencia(Tema_GV42.Primario, Tema_GV42.Primario, out _lblSeleccion));
            _pnlReferencias.Controls.Add(CrearReferencia(Tema_GV42.AsignadoOtroPasajero, Tema_GV42.AsignadoOtroPasajero, out _lblAsignado));
            _pnlReferencias.Controls.Add(CrearReferencia(Tema_GV42.Ocupado, Tema_GV42.Ocupado, out _lblOcupado));

            _pnlGrilla = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.White };

            Controls.Add(_pnlGrilla);
            Controls.Add(_pnlReferencias);
            ActualizarIdioma();
        }

        #endregion

        #region Idioma

        // TConDefecto: en el diseñador de VS (sin archivo de idioma cargado) se ve el texto en español.
        public void ActualizarIdioma()
        {
            _lblLibre.Text = IdiomaManager_GV42.TConDefecto("butacas.libre", "Libre");
            _lblSeleccion.Text = IdiomaManager_GV42.TConDefecto("butacas.seleccion", "Tu selección");
            _lblAsignado.Text = IdiomaManager_GV42.TConDefecto("butacas.asignado", "Asignado (otro pasajero)");
            _lblOcupado.Text = IdiomaManager_GV42.TConDefecto("butacas.ocupado", "Ocupado");
        }

        #endregion

        #region Leyenda

        // El contenedor se ajusta al largo del texto (en inglés algunos textos son más largos).
        private Panel CrearReferencia(Color relleno, Color borde, out Label lbl)
        {
            var cont = new Panel
            {
                Size = new Size(160, 24),
                MinimumSize = new Size(60, 24),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(4, 2, 12, 2)
            };
            var cuadro = new Panel
            {
                Size = new Size(16, 16),
                Location = new Point(0, 4),
                BackColor = relleno,
                BorderStyle = BorderStyle.FixedSingle
            };
            lbl = new Label
            {
                Location = new Point(22, 3),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Tema_GV42.Texto
            };
            cont.Controls.Add(cuadro);
            cont.Controls.Add(lbl);
            return cont;
        }

        #endregion

        #region Mapa de asientos

        // ocupadosLocalmente: asientos ya elegidos por OTROS pasajeros de esta misma reserva (todavía
        // no persistidos en la base). seleccionActualId: el asiento del pasajero que se está editando ahora.
        public void CargarMapa(List<AsientoDisponibilidad_GV42> asientos, HashSet<int> ocupadosLocalmente, int? seleccionActualId)
        {
            _asientos = asientos ?? new List<AsientoDisponibilidad_GV42>();
            _botonesPorIdAsiento.Clear();
            _pnlGrilla.Controls.Clear();
            if (_asientos.Count == 0) return;

            int filaMin = _asientos.Min(a => a.Fila);
            int filaMax = _asientos.Max(a => a.Fila);
            var letras = _asientos.Select(a => a.Letra).Distinct().OrderBy(l => l).ToList();

            for (int fila = filaMin; fila <= filaMax; fila++)
            {
                var deLaFila = _asientos.Where(a => a.Fila == fila).ToDictionary(a => a.Letra);
                int x = 30;
                int y = (fila - filaMin) * (ALTO_BOTON + ESPACIO) + 8;

                var lblFila = new Label
                {
                    Text = fila.ToString(),
                    Location = new Point(0, y + 6),
                    Size = new Size(24, ALTO_BOTON),
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Tema_GV42.Texto
                };
                _pnlGrilla.Controls.Add(lblFila);

                foreach (string letra in letras)
                {
                    if (letra == "D") x += ANCHO_PASILLO; // pasillo entre C y D

                    if (deLaFila.TryGetValue(letra, out AsientoDisponibilidad_GV42 ad))
                    {
                        Button b = CrearBotonAsiento(ad, ocupadosLocalmente, seleccionActualId);
                        b.Location = new Point(x, y);
                        _pnlGrilla.Controls.Add(b);
                        _botonesPorIdAsiento[ad.Asiento.Id] = b;
                    }
                    x += ANCHO_BOTON + ESPACIO;
                }
            }
        }

        private Button CrearBotonAsiento(AsientoDisponibilidad_GV42 ad, HashSet<int> ocupadosLocalmente, int? seleccionActualId)
        {
            var b = new Button
            {
                Size = new Size(ANCHO_BOTON, ALTO_BOTON),
                Text = ad.NumeroAsiento,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Tag = ad.Asiento,
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 1;

            bool esMiSeleccion = seleccionActualId.HasValue && seleccionActualId.Value == ad.Asiento.Id;
            bool ocupadoOtroDeEstaReserva = !esMiSeleccion && ocupadosLocalmente != null && ocupadosLocalmente.Contains(ad.Asiento.Id);

            if (esMiSeleccion)
            {
                b.BackColor = Tema_GV42.Primario;
                b.ForeColor = Color.White;
                b.FlatAppearance.BorderColor = Tema_GV42.Primario;
            }
            else if (ad.Ocupado)
            {
                b.BackColor = Tema_GV42.Ocupado;
                b.ForeColor = Color.White;
                b.FlatAppearance.BorderColor = Tema_GV42.Ocupado;
                b.Enabled = false;
            }
            else if (ocupadoOtroDeEstaReserva)
            {
                b.BackColor = Tema_GV42.AsignadoOtroPasajero;
                b.ForeColor = Color.White;
                b.FlatAppearance.BorderColor = Tema_GV42.AsignadoOtroPasajero;
                b.Enabled = false;
            }
            else
            {
                b.BackColor = Color.White;
                b.ForeColor = Tema_GV42.Primario;
                b.FlatAppearance.BorderColor = Tema_GV42.Primario;
                b.Click += (s, e) => AsientoClickeado?.Invoke(this, (Asiento_GV42)((Button)s).Tag);
            }

            return b;
        }

        #endregion
    }
}
