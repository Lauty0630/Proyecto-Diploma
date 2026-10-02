namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class CtrlTarifa_GV42
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
            this.pnlTarjeta = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.btnElegir = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.lblPorAdulto = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.pnlTarjeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.Controls.Add(this.lblDetalle);
            this.pnlTarjeta.Controls.Add(this.btnElegir);
            this.pnlTarjeta.Controls.Add(this.lblPorAdulto);
            this.pnlTarjeta.Controls.Add(this.lblPrecio);
            this.pnlTarjeta.Controls.Add(this.lblNombre);
            this.pnlTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjeta.Location = new System.Drawing.Point(0, 0);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(18, 14, 18, 16);
            this.pnlTarjeta.Radio = 10;
            this.pnlTarjeta.Size = new System.Drawing.Size(290, 300);
            this.pnlTarjeta.TabIndex = 0;
            // 
            // lblDetalle
            // 
            this.lblDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetalle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDetalle.Location = new System.Drawing.Point(18, 98);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblDetalle.Size = new System.Drawing.Size(254, 148);
            this.lblDetalle.TabIndex = 3;
            this.lblDetalle.Text = "•  1 valija despachada incluida";
            // 
            // btnElegir
            // 
            this.btnElegir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnElegir.FlatAppearance.BorderSize = 0;
            this.btnElegir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElegir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnElegir.Location = new System.Drawing.Point(18, 246);
            this.btnElegir.Name = "btnElegir";
            this.btnElegir.Size = new System.Drawing.Size(254, 38);
            this.btnElegir.TabIndex = 4;
            this.btnElegir.Text = "Elegir";
            this.btnElegir.UseVisualStyleBackColor = false;
            this.btnElegir.Click += new System.EventHandler(this.btnElegir_Click);
            // 
            // lblPorAdulto
            // 
            this.lblPorAdulto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPorAdulto.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblPorAdulto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblPorAdulto.Location = new System.Drawing.Point(18, 78);
            this.lblPorAdulto.Name = "lblPorAdulto";
            this.lblPorAdulto.Size = new System.Drawing.Size(254, 20);
            this.lblPorAdulto.TabIndex = 2;
            this.lblPorAdulto.Text = "por adulto, sin impuestos";
            // 
            // lblPrecio
            // 
            this.lblPrecio.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblPrecio.Location = new System.Drawing.Point(18, 44);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(254, 34);
            this.lblPrecio.TabIndex = 1;
            this.lblPrecio.Text = "$ 0,00";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblNombre.Location = new System.Drawing.Point(18, 14);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(254, 30);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Tarifa";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CtrlTarifa_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlTarjeta);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.Name = "CtrlTarifa_GV42";
            this.Size = new System.Drawing.Size(290, 300);
            this.pnlTarjeta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjeta;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblPorAdulto;
        private System.Windows.Forms.Label lblDetalle;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnElegir;
    }
}
