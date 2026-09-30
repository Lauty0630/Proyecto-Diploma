using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Paleta y estilos comunes del sistema (los mismos colores que ya usan FRMIniciarSesion,
    // FRMCambiarContrasenia, FRMGestionUsuariosAdmin, etc.). Centralizarlos acá evita repetir
    // los mismos Color.FromArgb en cada pantalla nueva y que se desalineen con el resto.
    internal static class Tema_GV42
    {
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

        public static readonly Font FuenteTitulo = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        public static readonly Font FuenteSubtitulo = new Font("Segoe UI", 9.5F);
        public static readonly Font FuenteLabel = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        public static readonly Font FuenteTexto = new Font("Segoe UI", 9.5F);
        public static readonly Font FuenteBoton = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

        public static void EstilizarBotonPrimario(Button b)
        {
            b.BackColor = Primario;
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = PrimarioHover;
            b.FlatAppearance.MouseDownBackColor = PrimarioPresionado;
            b.Font = FuenteBoton;
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        public static void EstilizarBotonSecundario(Button b)
        {
            b.BackColor = Color.White;
            b.ForeColor = Acento;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Primario;
            b.FlatAppearance.MouseOverBackColor = Fondo;
            b.Font = FuenteBoton;
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        public static TextBox CrearTextBox()
        {
            return new TextBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = FuenteTexto,
                ForeColor = Texto
            };
        }

        public static Label CrearLabel(string texto)
        {
            return new Label { AutoSize = true, Font = FuenteLabel, ForeColor = Acento, Text = texto };
        }

        public static Panel CrearCard()
        {
            return new Panel { BackColor = Card, BorderStyle = BorderStyle.FixedSingle };
        }

        // Deja una DataGridView con el mismo look (encabezado azul, filas alternadas celestes)
        // que ya usa FRMGestionUsuariosAdmin.
        public static void EstilizarGrilla(DataGridView g)
        {
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.GridColor = BordeGrilla;
            g.RowHeadersVisible = false;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            g.AlternatingRowsDefaultCellStyle.BackColor = Fondo;

            g.ColumnHeadersDefaultCellStyle.BackColor = Acento;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Acento;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Texto;
            g.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            g.DefaultCellStyle.SelectionBackColor = BordeGrilla;
            g.DefaultCellStyle.SelectionForeColor = Acento;
            g.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    
        // Marca un campo inválido: lo pinta de rojo claro, le da el foco y muestra el motivo.
        // El color se restaura solo apenas el usuario corrige el valor.
        public static void MostrarError(Control campo, string mensaje, string titulo = "Revisá los datos")
        {
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
            MessageBox.Show("No se pudo " + accion + ".\n\n" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
