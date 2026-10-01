namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMTarjetaEmbarque_GV42
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.ctrlTarjeta = new PROYECTO_ING_DE_SOFTWARE.CtrlTarjetaEmbarque_GV42();
            this.lblSeccionEtiquetas = new System.Windows.Forms.Label();
            this.lblAyudaEtiquetas = new System.Windows.Forms.Label();
            this.flpEtiquetas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnPdf = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnImprimir = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCerrar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(926, 84);
            this.pnlEncabezado.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(239, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Tarjeta de embarque";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(560, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Pasajero  |  Vuelo AR1302 EZE -> BRC  |  Asiento 12A  |  Check-in en mostrador";
            //
            // pnlContenido
            //
            this.pnlContenido.AutoScroll = true;
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.pnlContenido.Controls.Add(this.ctrlTarjeta);
            this.pnlContenido.Controls.Add(this.lblSeccionEtiquetas);
            this.pnlContenido.Controls.Add(this.lblAyudaEtiquetas);
            this.pnlContenido.Controls.Add(this.flpEtiquetas);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 84);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 16);
            this.pnlContenido.Size = new System.Drawing.Size(926, 724);
            this.pnlContenido.TabIndex = 1;
            //
            // ctrlTarjeta
            //
            this.ctrlTarjeta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlTarjeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ctrlTarjeta.Location = new System.Drawing.Point(24, 20);
            this.ctrlTarjeta.Name = "ctrlTarjeta";
            this.ctrlTarjeta.Size = new System.Drawing.Size(878, 320);
            this.ctrlTarjeta.TabIndex = 0;
            //
            // lblSeccionEtiquetas
            //
            this.lblSeccionEtiquetas.AutoSize = true;
            this.lblSeccionEtiquetas.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionEtiquetas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionEtiquetas.Location = new System.Drawing.Point(22, 356);
            this.lblSeccionEtiquetas.Name = "lblSeccionEtiquetas";
            this.lblSeccionEtiquetas.Size = new System.Drawing.Size(196, 20);
            this.lblSeccionEtiquetas.TabIndex = 1;
            this.lblSeccionEtiquetas.Text = "Etiquetas de equipaje (2)";
            //
            // lblAyudaEtiquetas
            //
            this.lblAyudaEtiquetas.AutoSize = true;
            this.lblAyudaEtiquetas.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyudaEtiquetas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyudaEtiquetas.Location = new System.Drawing.Point(25, 382);
            this.lblAyudaEtiquetas.Name = "lblAyudaEtiquetas";
            this.lblAyudaEtiquetas.Size = new System.Drawing.Size(418, 15);
            this.lblAyudaEtiquetas.TabIndex = 2;
            this.lblAyudaEtiquetas.Text = "Pegá cada etiqueta en su bulto. El pasajero conserva la tarjeta de embarque.";
            //
            // flpEtiquetas
            //
            this.flpEtiquetas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpEtiquetas.AutoScroll = true;
            this.flpEtiquetas.Location = new System.Drawing.Point(24, 406);
            this.flpEtiquetas.Name = "flpEtiquetas";
            this.flpEtiquetas.Size = new System.Drawing.Size(878, 300);
            this.flpEtiquetas.TabIndex = 3;
            this.flpEtiquetas.WrapContents = false;
            //
            // pnlAcciones
            //
            this.pnlAcciones.BackColor = System.Drawing.Color.White;
            this.pnlAcciones.Controls.Add(this.lblAyuda);
            this.pnlAcciones.Controls.Add(this.btnPdf);
            this.pnlAcciones.Controls.Add(this.btnImprimir);
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 808);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(926, 72);
            this.pnlAcciones.TabIndex = 2;
            //
            // lblAyuda
            //
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(24, 18);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(450, 36);
            this.lblAyuda.TabIndex = 0;
            this.lblAyuda.Text = "Al imprimir, la tarjeta y las etiquetas salen en la misma hoja (horizontal).";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnPdf
            //
            this.btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf.FlatAppearance.BorderSize = 0;
            this.btnPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdf.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPdf.Location = new System.Drawing.Point(496, 16);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(150, 40);
            this.btnPdf.TabIndex = 1;
            this.btnPdf.Text = "Guardar PDF";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            //
            // btnImprimir
            //
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.Location = new System.Drawing.Point(656, 16);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(120, 40);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            //
            // btnCerrar
            //
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(786, 16);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(116, 40);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // FRMTarjetaEmbarque_GV42
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(926, 880);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMTarjetaEmbarque_GV42";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tarjeta de embarque";
            this.Load += new System.EventHandler(this.FRMTarjetaEmbarque_GV42_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.CtrlTarjetaEmbarque_GV42 ctrlTarjeta;
        private System.Windows.Forms.Label lblSeccionEtiquetas;
        private System.Windows.Forms.Label lblAyudaEtiquetas;
        private System.Windows.Forms.FlowLayoutPanel flpEtiquetas;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblAyuda;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnPdf;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnImprimir;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCerrar;
    }
}
