namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class CtrlAdicional_GV42
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
            this.chkSeleccionado = new System.Windows.Forms.CheckBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblTope = new System.Windows.Forms.Label();
            this.lblCosto = new System.Windows.Forms.Label();
            this.numCosto = new System.Windows.Forms.NumericUpDown();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSeleccionado
            // 
            this.chkSeleccionado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkSeleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.chkSeleccionado.Location = new System.Drawing.Point(8, 22);
            this.chkSeleccionado.Name = "chkSeleccionado";
            this.chkSeleccionado.Size = new System.Drawing.Size(262, 24);
            this.chkSeleccionado.TabIndex = 0;
            this.chkSeleccionado.Text = "Servicio adicional";
            this.chkSeleccionado.UseVisualStyleBackColor = true;
            this.chkSeleccionado.CheckedChanged += new System.EventHandler(this.chkSeleccionado_CheckedChanged);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCantidad.Location = new System.Drawing.Point(284, 4);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(60, 15);
            this.lblCantidad.TabIndex = 1;
            this.lblCantidad.Text = "Cantidad";
            // 
            // numCantidad
            // 
            this.numCantidad.Enabled = false;
            this.numCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCantidad.Location = new System.Drawing.Point(284, 24);
            this.numCantidad.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(72, 25);
            this.numCantidad.TabIndex = 2;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTope
            // 
            this.lblTope.AutoSize = true;
            this.lblTope.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblTope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblTope.Location = new System.Drawing.Point(364, 29);
            this.lblTope.Name = "lblTope";
            this.lblTope.Size = new System.Drawing.Size(140, 15);
            this.lblTope.TabIndex = 3;
            this.lblTope.Text = "Máx. 1 (1 por pasajero)";
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCosto.Location = new System.Drawing.Point(644, 4);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(90, 15);
            this.lblCosto.TabIndex = 4;
            this.lblCosto.Text = "Costo unitario";
            // 
            // numCosto
            // 
            this.numCosto.DecimalPlaces = 2;
            this.numCosto.Enabled = false;
            this.numCosto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCosto.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numCosto.Location = new System.Drawing.Point(644, 24);
            this.numCosto.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numCosto.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCosto.Name = "numCosto";
            this.numCosto.Size = new System.Drawing.Size(130, 25);
            this.numCosto.TabIndex = 5;
            this.numCosto.ThousandsSeparator = true;
            this.numCosto.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparador.Location = new System.Drawing.Point(0, 63);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(900, 1);
            this.pnlSeparador.TabIndex = 6;
            // 
            // CtrlAdicional_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlSeparador);
            this.Controls.Add(this.numCosto);
            this.Controls.Add(this.lblCosto);
            this.Controls.Add(this.lblTope);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.chkSeleccionado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CtrlAdicional_GV42";
            this.Size = new System.Drawing.Size(900, 64);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSeleccionado;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblTope;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.NumericUpDown numCosto;
        private System.Windows.Forms.Panel pnlSeparador;
    }
}
