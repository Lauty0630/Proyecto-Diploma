namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class CtrlNavegadorPasajero_GV42
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
            this.components = new System.ComponentModel.Container();
            this.lblPasajero = new System.Windows.Forms.Label();
            this.btnAnterior = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnSiguiente = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblPasajero
            // 
            this.lblPasajero.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasajero.AutoEllipsis = true;
            this.lblPasajero.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPasajero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPasajero.Location = new System.Drawing.Point(0, 0);
            this.lblPasajero.Name = "lblPasajero";
            this.lblPasajero.Size = new System.Drawing.Size(422, 30);
            this.lblPasajero.TabIndex = 0;
            this.lblPasajero.Text = "Pasajero 1 de 1";
            this.lblPasajero.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnterior.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnterior.Location = new System.Drawing.Point(430, 1);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(40, 28);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "‹";
            this.toolTip1.SetToolTip(this.btnAnterior, "Pasajero anterior");
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiguiente.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSiguiente.Location = new System.Drawing.Point(476, 1);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(40, 28);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = "›";
            this.toolTip1.SetToolTip(this.btnSiguiente, "Pasajero siguiente");
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // CtrlNavegadorPasajero_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.lblPasajero);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CtrlNavegadorPasajero_GV42";
            this.Size = new System.Drawing.Size(520, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPasajero;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAnterior;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnSiguiente;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
