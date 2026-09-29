using BE;
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
    public class CtrlButacas_GV42 : UserControl
    {
        private const int ANCHO_BOTON = 40;
        private const int ALTO_BOTON = 32;
        private const int ESPACIO = 4;
        private const int ANCHO_PASILLO = 20;

        private readonly Panel _pnlGrilla;
        private readonly FlowLayoutPanel _pnlReferencias;
        private readonly Dictionary<int, Button> _botonesPorIdAsiento = new Dictionary<int, Button>();
        private List<AsientoDisponibilidad_GV42> _asientos = new List<AsientoDisponibilidad_GV42>();

        public event EventHandler<Asiento_GV42> AsientoClickeado;

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
            _pnlReferencias.Controls.Add(CrearReferencia("Libre", Color.White, Tema_GV42.Primario));
            _pnlReferencias.Controls.Add(CrearReferencia("Tu selección", Tema_GV42.Primario, Tema_GV42.Primario));
            _pnlReferencias.Controls.Add(CrearReferencia("Asignado (otro pasajero)", Tema_GV42.AsignadoOtroPasajero, Tema_GV42.AsignadoOtroPasajero));
            _pnlReferencias.Controls.Add(CrearReferencia("Ocupado", Tema_GV42.Ocupado, Tema_GV42.Ocupado));

            _pnlGrilla = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.White };

            Controls.Add(_pnlGrilla);
            Controls.Add(_pnlReferencias);
        }

        private Panel CrearReferencia(string texto, Color relleno, Color borde)
        {
            var cont = new Panel { Size = new Size(160, 24), Margin = new Padding(4, 2, 4, 2) };
            var cuadro = new Panel
            {
                Size = new Size(16, 16),
                Location = new Point(0, 4),
                BackColor = relleno,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lbl = new Label
            {
                Text = texto,
                Location = new Point(22, 3),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Tema_GV42.Texto
            };
            cont.Controls.Add(cuadro);
            cont.Controls.Add(lbl);
            return cont;
        }

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
    }
}
