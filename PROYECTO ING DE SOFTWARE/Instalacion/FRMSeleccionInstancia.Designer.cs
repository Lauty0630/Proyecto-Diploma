namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMSeleccionInstancia
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
            this.pnlCard = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblInstancia = new System.Windows.Forms.Label();
            this.cboInstancias = new System.Windows.Forms.ComboBox();
            this.btnDetectar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnContinuar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(620, 80);
            this.pnlEncabezado.TabIndex = 3;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(240, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configurar conexión a la base de datos";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(300, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Configuración inicial de FLY SAFE";
            // 
            // pnlCard
            // 
            this.pnlCard.AcentoSuperior = true;
            this.pnlCard.Controls.Add(this.lblEstado);
            this.pnlCard.Controls.Add(this.btnDetectar);
            this.pnlCard.Controls.Add(this.cboInstancias);
            this.pnlCard.Controls.Add(this.lblInstancia);
            this.pnlCard.Controls.Add(this.lblDescripcion);
            this.pnlCard.Location = new System.Drawing.Point(24, 100);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(572, 206);
            this.pnlCard.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDescripcion.Location = new System.Drawing.Point(22, 22);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(528, 42);
            this.lblDescripcion.TabIndex = 0;
            this.lblDescripcion.Text = "Elegí la instancia de SQL Server donde querés instalar / conectarte a la base.";
            // 
            // lblInstancia
            // 
            this.lblInstancia.AutoSize = true;
            this.lblInstancia.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblInstancia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblInstancia.Location = new System.Drawing.Point(22, 72);
            this.lblInstancia.Name = "lblInstancia";
            this.lblInstancia.Size = new System.Drawing.Size(70, 17);
            this.lblInstancia.TabIndex = 1;
            this.lblInstancia.Text = "Instancia:";
            // 
            // cboInstancias
            // 
            this.cboInstancias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstancias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboInstancias.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboInstancias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.cboInstancias.FormattingEnabled = true;
            this.cboInstancias.Location = new System.Drawing.Point(24, 98);
            this.cboInstancias.Name = "cboInstancias";
            this.cboInstancias.Size = new System.Drawing.Size(398, 25);
            this.cboInstancias.TabIndex = 2;
            // 
            // btnDetectar
            // 
            this.btnDetectar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnDetectar.FlatAppearance.BorderSize = 0;
            this.btnDetectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetectar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDetectar.Location = new System.Drawing.Point(434, 92);
            this.btnDetectar.Name = "btnDetectar";
            this.btnDetectar.Size = new System.Drawing.Size(116, 38);
            this.btnDetectar.TabIndex = 3;
            this.btnDetectar.Text = "Detectar";
            this.btnDetectar.UseVisualStyleBackColor = false;
            this.btnDetectar.Click += new System.EventHandler(this.btnDetectar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblEstado.Location = new System.Drawing.Point(24, 142);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(526, 40);
            this.lblEstado.TabIndex = 4;
            // 
            // btnContinuar
            // 
            this.btnContinuar.FlatAppearance.BorderSize = 0;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinuar.Location = new System.Drawing.Point(330, 326);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(146, 40);
            this.btnContinuar.TabIndex = 1;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(486, 326);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FRMSeleccionInstancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(620, 386);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMSeleccionInstancia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración inicial - Base de datos";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCard;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblInstancia;
        private System.Windows.Forms.ComboBox cboInstancias;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnDetectar;
        private System.Windows.Forms.Label lblEstado;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnContinuar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
    }
}
