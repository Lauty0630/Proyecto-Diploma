namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class CtrlPesoValija_GV42
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblValija = new System.Windows.Forms.Label();
            this.numPeso = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).BeginInit();
            this.SuspendLayout();
            // 
            // lblValija
            // 
            this.lblValija.AutoSize = true;
            this.lblValija.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblValija.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblValija.Location = new System.Drawing.Point(0, 0);
            this.lblValija.Name = "lblValija";
            this.lblValija.Size = new System.Drawing.Size(45, 13);
            this.lblValija.TabIndex = 1;
            this.lblValija.Text = "Valija 1";
            // 
            // numPeso
            // 
            this.numPeso.DecimalPlaces = 1;
            this.numPeso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPeso.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numPeso.Location = new System.Drawing.Point(0, 18);
            this.numPeso.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numPeso.Name = "numPeso";
            this.numPeso.Size = new System.Drawing.Size(76, 25);
            this.numPeso.TabIndex = 0;
            this.numPeso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPeso.ValueChanged += new System.EventHandler(this.numPeso_ValueChanged);
            // 
            // CtrlPesoValija_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.numPeso);
            this.Controls.Add(this.lblValija);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 8, 4);
            this.Name = "CtrlPesoValija_GV42";
            this.Size = new System.Drawing.Size(78, 46);
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValija;
        private System.Windows.Forms.NumericUpDown numPeso;
    }
}
