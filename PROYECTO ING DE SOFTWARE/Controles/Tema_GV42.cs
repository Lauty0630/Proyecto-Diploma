using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Servicios;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Paleta y estilos comunes del sistema (los mismos colores que ya usan FRMIniciarSesion,
    // FRMCambiarContrasenia, FRMGestionUsuariosAdmin, etc.). Centralizarlos acá evita repetir
    // los mismos Color.FromArgb en cada pantalla nueva y que se desalineen con el resto.
    internal static class Tema_GV42
    {
        #region Campos

        public static readonly Color Fondo = Color.FromArgb(227, 242, 253);
        public static readonly Color Card = Color.White;
        public static readonly Color Primario = Color.FromArgb(25, 118, 210);
        public static readonly Color PrimarioHover = Color.FromArgb(33, 150, 243);
        public static readonly Color PrimarioPresionado = Color.FromArgb(13, 71, 161);
        public static readonly Color Acento = Color.FromArgb(13, 71, 161);
        public static readonly Color Texto = Color.FromArgb(33, 33, 33);
        public static readonly Color BordeGrilla = Color.FromArgb(187, 222, 251);
        public static readonly Color Error = Color.FromArgb(198, 40, 40);
        public static readonly Color Advertencia = Color.FromArgb(230, 126, 15);
        public static readonly Color Ocupado = Color.FromArgb(158, 158, 158);
        public static readonly Color AsignadoOtroPasajero = Color.FromArgb(255, 152, 0);

        public static readonly Color FondoError = Color.FromArgb(255, 235, 238);
        public static readonly Color Exito = Color.FromArgb(46, 125, 50);
        public static readonly Color TextoSecundario = Color.FromArgb(96, 125, 139);

        #endregion

        #region Métodos públicos

        // Marca un campo inválido: lo pinta de rojo claro, le da el foco y muestra el motivo.
        // El color se restaura solo apenas el usuario corrige el valor.
        public static void MostrarError(Control campo, string mensaje, string titulo = null)
        {
            if (titulo == null) titulo = IdiomaManager_GV42.T("general.revisarDatos");
            if (campo != null)
            {
                MarcarInvalido(campo);
                if (campo.CanFocus) campo.Focus();
                var tb = campo as TextBox;
                if (tb != null) tb.SelectAll();
            }
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void MarcarInvalido(Control campo)
        {
            if (campo == null || campo.BackColor == FondoError) return;
            Color original = campo.BackColor;
            campo.BackColor = FondoError;
            EventHandler limpiar = null;
            limpiar = (s, e) =>
            {
                campo.BackColor = original;
                campo.TextChanged -= limpiar;
                var nud = campo as NumericUpDown;
                if (nud != null) nud.ValueChanged -= limpiar;
                var cbo = campo as ComboBox;
                if (cbo != null) cbo.SelectedIndexChanged -= limpiar;
            };
            campo.TextChanged += limpiar;
            var n = campo as NumericUpDown;
            if (n != null) n.ValueChanged += limpiar;
            var c = campo as ComboBox;
            if (c != null) c.SelectedIndexChanged += limpiar;
        }

        // Error inesperado (base de datos, archivo, etc.): mensaje claro en vez del diálogo de .NET.
        public static void MostrarErrorInesperado(string accion, Exception ex)
        {
            MessageBox.Show(IdiomaManager_GV42.T("general.noSePudo", accion) + "\n\n" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message),
                            IdiomaManager_GV42.T("general.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Contorno redondeado usado por los controles modernos (botón, tarjeta).
        public static GraphicsPath RectanguloRedondeado(RectangleF r, float radio)
        {
            var p = new GraphicsPath();
            if (radio <= 0.5f) { p.AddRectangle(r); return p; }
            float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        #endregion
    }
}
