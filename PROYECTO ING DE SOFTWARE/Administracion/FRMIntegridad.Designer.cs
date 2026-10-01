namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMIntegridad
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
            this.pnlIcono = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblIcono = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlCard = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblTablas = new System.Windows.Forms.Label();
            this.lstTablas = new System.Windows.Forms.ListBox();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnBackup = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnRestore = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlIcono.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlEncabezado.ColorFin = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.pnlEncabezado.ColorInicio = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.pnlIcono);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.MostrarDecoracion = false;
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(620, 84);
            this.pnlEncabezado.TabIndex = 2;
            //
            // pnlIcono
            //
            this.pnlIcono.BackColor = System.Drawing.Color.White;
            this.pnlIcono.ColorBorde = System.Drawing.Color.White;
            this.pnlIcono.Controls.Add(this.lblIcono);
            this.pnlIcono.Location = new System.Drawing.Point(24, 16);
            this.pnlIcono.Name = "pnlIcono";
            this.pnlIcono.Padding = new System.Windows.Forms.Padding(0);
            this.pnlIcono.Radio = 24;
            this.pnlIcono.Size = new System.Drawing.Size(50, 52);
            this.pnlIcono.TabIndex = 0;
            //
            // lblIcono
            //
            this.lblIcono.BackColor = System.Drawing.Color.Transparent;
            this.lblIcono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcono.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblIcono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblIcono.Location = new System.Drawing.Point(0, 0);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblIcono.Size = new System.Drawing.Size(50, 52);
            this.lblIcono.TabIndex = 0;
            this.lblIcono.Text = "!";
            this.lblIcono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(88, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(300, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Integridad comprometida";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(91, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(150, 19);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Control de Integridad";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlCard);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 84);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(20, 16, 20, 4);
            this.pnlContenido.Size = new System.Drawing.Size(620, 326);
            this.pnlContenido.TabIndex = 0;
            //
            // pnlCard
            //
            this.pnlCard.Controls.Add(this.lstTablas);
            this.pnlCard.Controls.Add(this.lblTablas);
            this.pnlCard.Controls.Add(this.lblMensaje);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(20, 16);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(580, 306);
            this.pnlCard.TabIndex = 0;
            //
            // lblMensaje
            //
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblMensaje.Location = new System.Drawing.Point(20, 18);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(540, 44);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Se detectaron alteraciones en la base de datos realizadas desde fuera de la aplicación.";
            //
            // lblTablas
            //
            this.lblTablas.AutoSize = true;
            this.lblTablas.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTablas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTablas.Location = new System.Drawing.Point(18, 72);
            this.lblTablas.Name = "lblTablas";
            this.lblTablas.Size = new System.Drawing.Size(130, 20);
            this.lblTablas.TabIndex = 1;
            this.lblTablas.Text = "Tablas afectadas:";
            //
            // lstTablas
            //
            this.lstTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTablas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.lstTablas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTablas.Font = new System.Drawing.Font("Consolas", 10F);
            this.lstTablas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lstTablas.IntegralHeight = false;
            this.lstTablas.ItemHeight = 15;
            this.lstTablas.Location = new System.Drawing.Point(20, 100);
            this.lstTablas.Name = "lstTablas";
            this.lstTablas.Size = new System.Drawing.Size(540, 184);
            this.lstTablas.TabIndex = 2;
            //
            // pnlBotones
            //
            this.pnlBotones.Controls.Add(this.flpBotones);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Location = new System.Drawing.Point(0, 410);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Padding = new System.Windows.Forms.Padding(16, 8, 16, 12);
            this.pnlBotones.Size = new System.Drawing.Size(620, 66);
            this.pnlBotones.TabIndex = 1;
            //
            // flpBotones
            //
            this.flpBotones.Controls.Add(this.btnCancelar);
            this.flpBotones.Controls.Add(this.btnBackup);
            this.flpBotones.Controls.Add(this.btnRestore);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Location = new System.Drawing.Point(16, 8);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.Size = new System.Drawing.Size(588, 46);
            this.flpBotones.TabIndex = 0;
            this.flpBotones.WrapContents = false;
            //
            // btnCancelar
            //
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(458, 3);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(127, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // btnBackup
            //
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackup.Location = new System.Drawing.Point(278, 3);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(167, 40);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Restore";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            //
            // btnRestore
            //
            this.btnRestore.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Advertencia;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.Location = new System.Drawing.Point(98, 3);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(167, 40);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "Recalcular";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // FRMIntegridad
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(620, 476);
            this.ControlBox = false;
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMIntegridad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control de Integridad";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlIcono.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlIcono;
        private System.Windows.Forms.Label lblIcono;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCard;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblTablas;
        private System.Windows.Forms.ListBox lstTablas;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnBackup;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnRestore;
    }
}
