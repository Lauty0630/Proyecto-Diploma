using BE;
using BLL;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Muestra un boleto electrónico escalado al ancho del control (mantiene la proporción).
    public class CtrlBoleto_GV42 : Control
    {
        #region Campos

        private readonly BoletoElectronico_GV42 _boleto;

        #endregion

        #region Constructor

        public CtrlBoleto_GV42(BoletoElectronico_GV42 boleto)
        {
            _boleto = boleto;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackColor = Tema_GV42.Fondo;
            Margin = new Padding(0, 0, 0, 16);
        }

        #endregion

        #region Propiedades

        public BoletoElectronico_GV42 Boleto => _boleto;

        #endregion

        #region Métodos públicos

        // Ajusta el alto a la proporción del boleto para el ancho dado.
        public void AjustarAncho(int ancho)
        {
            ancho = System.Math.Max(300, ancho);
            Size = new Size(ancho, (int)(ancho * DisenioBoleto_GV42.ALTO / DisenioBoleto_GV42.ANCHO) + 2);
        }

        #endregion

        #region Eventos

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_boleto == null) return;
            float escala = (Width - 1) / DisenioBoleto_GV42.ANCHO;
            e.Graphics.ScaleTransform(escala, escala);
            using (var lienzo = new LienzoGdi_GV42(e.Graphics))
                DisenioBoleto_GV42.Dibujar(lienzo, _boleto, 0, 0);
        }

        #endregion
    }
}
