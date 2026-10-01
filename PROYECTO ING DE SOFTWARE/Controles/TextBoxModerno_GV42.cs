using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // TextBox con borde celeste que se pone azul al tener el foco (y rojo si el campo quedó marcado
    // como inválido por Tema_GV42.MostrarError). Se usa desde el Form Designer como un TextBox común.
    [ToolboxItem(true)]
    [Description("Caja de texto con borde de color (celeste / azul con foco / rojo si es inválida).")]
    public class TextBoxModerno_GV42 : TextBox
    {
        #region Campos

        private const int WM_NCPAINT = 0x85;
        private static readonly bool EsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        #endregion

        #region Constructor

        public TextBoxModerno_GV42()
        {
            BorderStyle = BorderStyle.FixedSingle;
            Font = new Font("Segoe UI", 10.5F);
            ForeColor = Color.FromArgb(33, 33, 33);
        }

        #endregion

        #region Eventos

        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); RedibujarBorde(); }

        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); RedibujarBorde(); }

        protected override void OnBackColorChanged(EventArgs e) { base.OnBackColorChanged(e); RedibujarBorde(); }

        #endregion

        #region Dibujo del borde

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT && BorderStyle == BorderStyle.FixedSingle) PintarBorde();
        }

        private void RedibujarBorde()
        {
            if (!EsWindows || !IsHandleCreated) return;
            try { SendMessage(Handle, WM_NCPAINT, (IntPtr)1, IntPtr.Zero); } catch { }
        }

        private void PintarBorde()
        {
            if (!EsWindows) return;
            IntPtr hdc = IntPtr.Zero;
            try
            {
                hdc = GetWindowDC(Handle);
                if (hdc == IntPtr.Zero) return;
                using (Graphics g = Graphics.FromHdc(hdc))
                {
                    Color color = BackColor == Tema_GV42.FondoError ? Tema_GV42.Error
                                : (Focused ? Tema_GV42.Primario : Color.FromArgb(144, 202, 249));
                    using (var lapiz = new Pen(color)) g.DrawRectangle(lapiz, 0, 0, Width - 1, Height - 1);
                }
            }
            catch { }
            finally
            {
                if (hdc != IntPtr.Zero) ReleaseDC(Handle, hdc);
            }
        }

        [DllImport("user32.dll")] private static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
