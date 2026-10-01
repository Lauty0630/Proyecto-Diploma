using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // Barra de menú azul con desplegables blancos y resaltado celeste. Los ítems se cargan
    // desde el Form Designer como en cualquier MenuStrip; los colores los pone el renderer.
    [ToolboxItem(true)]
    [Description("Barra de menú con el estilo de FLY SAFE.")]
    public class MenuModerno_GV42 : MenuStrip
    {
        #region Constructor

        public MenuModerno_GV42()
        {
            Renderer = new RenderMenu_GV42();
            BackColor = Color.FromArgb(13, 71, 161);
            ForeColor = Color.White;
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Padding = new Padding(10, 6, 0, 6);
        }

        #endregion

        #region Tipos anidados

        private sealed class RenderMenu_GV42 : ToolStripProfessionalRenderer
        {
            public RenderMenu_GV42() : base(new Colores_GV42()) { RoundedEdges = false; }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip is MenuStrip)
                {
                    using (var pincel = new LinearGradientBrush(e.AffectedBounds, Tema_GV42.Acento, Tema_GV42.Primario, LinearGradientMode.Horizontal))
                        e.Graphics.FillRectangle(pincel, e.AffectedBounds);
                    return;
                }
                base.OnRenderToolStripBackground(e);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.IsOnDropDown
                    ? (e.Item.Enabled ? Tema_GV42.Acento : Color.FromArgb(150, 160, 170))
                    : Color.White;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = e.Item != null && e.Item.IsOnDropDown ? Tema_GV42.Acento : Color.White;
                base.OnRenderArrow(e);
            }
        }

        private sealed class Colores_GV42 : ProfessionalColorTable
        {
            private static readonly Color Celeste = Color.FromArgb(227, 242, 253);
            private static readonly Color Borde = Color.FromArgb(187, 222, 251);
            private static readonly Color HoverBarra = Color.FromArgb(33, 150, 243);

            public override Color MenuItemSelected => Celeste;
            public override Color MenuItemBorder => Borde;
            public override Color MenuBorder => Borde;
            public override Color MenuItemSelectedGradientBegin => HoverBarra;
            public override Color MenuItemSelectedGradientEnd => HoverBarra;
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(13, 71, 161);
            public override Color MenuItemPressedGradientMiddle => Color.FromArgb(13, 71, 161);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(13, 71, 161);
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color SeparatorDark => Borde;
            public override Color SeparatorLight => Color.White;
        }

        #endregion
    }
}
