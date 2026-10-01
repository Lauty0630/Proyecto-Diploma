using BE;
using BLL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Muestra la tarjeta de embarque de un check-in (mismo diseño que el PDF y la impresión),
    // escalada al tamaño del control y manteniendo la proporción. Se puede arrastrar desde el
    // Form Designer; el check-in se asigna en tiempo de ejecución.
    [ToolboxItem(true)]
    [Description("Tarjeta de embarque dibujada con el diseño de FLY SAFE.")]
    public class CtrlTarjetaEmbarque_GV42 : Control
    {
        #region Campos

        private CheckIn_GV42 _checkIn;

        #endregion

        #region Constructor

        public CtrlTarjetaEmbarque_GV42()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackColor = Tema_GV42.Fondo;
        }

        #endregion

        #region Propiedades

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckIn_GV42 CheckIn
        {
            get { return _checkIn; }
            set { _checkIn = value; Invalidate(); }
        }

        #endregion

        #region Eventos

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_checkIn == null || _checkIn.TarjetaEmbarque == null) return;

            float escala = Math.Min((Width - 1) / DisenioTarjetaEmbarque_GV42.ANCHO,
                                    (Height - 1) / DisenioTarjetaEmbarque_GV42.ALTO);
            float margen = (Width - 1 - DisenioTarjetaEmbarque_GV42.ANCHO * escala) / 2;
            e.Graphics.TranslateTransform(margen, 0);
            e.Graphics.ScaleTransform(escala, escala);
            using (var lienzo = new LienzoGdi_GV42(e.Graphics))
                DisenioTarjetaEmbarque_GV42.Dibujar(lienzo, _checkIn, 0, 0);
        }

        #endregion
    }
}
