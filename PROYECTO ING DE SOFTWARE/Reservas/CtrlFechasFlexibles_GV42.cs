using BE;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Fechas flexibles (RFN 1, búsqueda de vuelos): tira con los días cercanos a la fecha buscada y el
    // precio más barato de cada uno. Al hacer clic en otro día se busca directamente para ese día,
    // sin volver a cargar los filtros. El diseño está en CtrlFechasFlexibles_GV42.Designer.cs.
    public partial class CtrlFechasFlexibles_GV42 : UserControl
    {
        #region Campos

        private Button[] _botones;
        private List<PrecioFecha_GV42> _dias = new List<PrecioFecha_GV42>();
        private DateTime _fechaElegida;

        #endregion

        #region Constructor

        public CtrlFechasFlexibles_GV42()
        {
            InitializeComponent();
            _botones = new[] { btnDia0, btnDia1, btnDia2, btnDia3, btnDia4, btnDia5, btnDia6 };
        }

        #endregion

        #region Propiedades

        // El usuario eligió otro día de la tira.
        public event EventHandler<DateTime> FechaElegida;

        #endregion

        #region Métodos públicos

        // dias: hasta 7 días consecutivos con su precio mínimo; fechaElegida: el día buscado (se destaca).
        public void Cargar(List<PrecioFecha_GV42> dias, DateTime fechaElegida)
        {
            _dias = dias ?? new List<PrecioFecha_GV42>();
            _fechaElegida = fechaElegida.Date;
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            var cultura = System.Globalization.CultureInfo.GetCultureInfo(IdiomaManager_GV42.Instancia.EsIngles ? "en-US" : "es-AR");
            for (int i = 0; i < _botones.Length; i++)
            {
                Button b = _botones[i];
                if (i >= _dias.Count) { b.Visible = false; continue; }

                PrecioFecha_GV42 dia = _dias[i];
                bool elegido = dia.Fecha.Date == _fechaElegida;
                b.Visible = true;
                b.Tag = dia.Fecha.Date;
                b.Text = dia.Fecha.ToString("ddd dd/MM", cultura) + Environment.NewLine +
                         (dia.HayVuelos ? IdiomaManager_GV42.T("fechas.desde", dia.PrecioMinimo.ToString("C0"))
                                        : IdiomaManager_GV42.T("fechas.sinVuelos"));
                b.Enabled = dia.HayVuelos || elegido;
                b.BackColor = elegido ? Tema_GV42.Primario : (dia.HayVuelos ? Color.White : Tema_GV42.Fondo);
                b.ForeColor = elegido ? Color.White : (dia.HayVuelos ? Tema_GV42.Acento : Tema_GV42.TextoSecundario);
                b.FlatAppearance.BorderColor = elegido ? Tema_GV42.Primario : Tema_GV42.BordeGrilla;
            }
        }

        #endregion

        #region Eventos

        private void btnDia_Click(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            if (!(boton.Tag is DateTime)) return;
            DateTime fecha = (DateTime)boton.Tag;
            if (fecha == _fechaElegida) return;
            FechaElegida?.Invoke(this, fecha);
        }

        #endregion
    }
}
