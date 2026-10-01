using BE;
using BLL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Muestra la etiqueta de un bulto despachado (mismo diseño que el PDF y la impresión).
    // Se crea una por bulto en tiempo de ejecución dentro del FlowLayoutPanel de la tarjeta de embarque.
    [ToolboxItem(false)]
    public class CtrlEtiquetaEquipaje_GV42 : Control
    {
        #region Constantes

        // Tamaño en pantalla: la etiqueta en puntos, a escala 1:1 (se lee bien y entran 6 por fila).
        public const float ESCALA = 1f;

        #endregion

        #region Campos

        private readonly CheckIn_GV42 _checkIn;
        private readonly int _indice;

        #endregion

        #region Constructor

        public CtrlEtiquetaEquipaje_GV42(CheckIn_GV42 checkIn, int indice)
        {
            _checkIn = checkIn;
            _indice = indice;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackColor = Tema_GV42.Fondo;
            Margin = new Padding(0, 0, 12, 0);
            Size = new Size((int)Math.Ceiling(DisenioEtiquetaEquipaje_GV42.ANCHO * ESCALA) + 1,
                            (int)Math.Ceiling(DisenioEtiquetaEquipaje_GV42.ALTO * ESCALA) + 1);
        }

        #endregion

        #region Propiedades

        public int Indice => _indice;

        #endregion

        #region Eventos

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_checkIn == null) return;
            float escala = Math.Min((Width - 1) / DisenioEtiquetaEquipaje_GV42.ANCHO,
                                    (Height - 1) / DisenioEtiquetaEquipaje_GV42.ALTO);
            e.Graphics.ScaleTransform(escala, escala);
            using (var lienzo = new LienzoGdi_GV42(e.Graphics))
                DisenioEtiquetaEquipaje_GV42.Dibujar(lienzo, _checkIn, _indice, 0, 0);
        }

        #endregion
    }
}
