using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // DataGridView con el estilo del sistema ya aplicado (encabezado azul, filas alternadas
    // celestes, solo lectura, selección por fila). Se arrastra desde el Cuadro de herramientas.
    [ToolboxItem(true)]
    [Description("Grilla de solo lectura con el estilo de FLY SAFE.")]
    public class GrillaModerna_GV42 : DataGridView
    {
        #region Constructor

        public GrillaModerna_GV42()
        {
            DoubleBuffered = true;
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            EnableHeadersVisualStyles = false;
            GridColor = Color.FromArgb(227, 236, 247);
            RowHeadersVisible = false;
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersHeight = 38;
            RowTemplate.Height = 32;

            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(13, 71, 161);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(13, 71, 161);
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            DefaultCellStyle.BackColor = Color.White;
            DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
            DefaultCellStyle.SelectionForeColor = Color.FromArgb(13, 71, 161);
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 247, 255);
        }

        #endregion
    }
}
