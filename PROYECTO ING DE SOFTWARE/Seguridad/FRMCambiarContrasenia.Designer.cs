namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMCambiarContrasenia
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
            this.pnlCard = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuario = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContrasenia = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNuevaconstrasenia = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblAyudaNueva = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfirmarContrasenia = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.btnAceptar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
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
            this.pnlEncabezado.Size = new System.Drawing.Size(900, 80);
            this.pnlEncabezado.TabIndex = 1;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(245, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Cambiar Contraseña";
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
            this.lblSubtitulo.Text = "Mantené tu cuenta protegida con una contraseña segura";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlCard);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(900, 500);
            this.pnlContenido.TabIndex = 0;
            //
            // pnlCard
            //
            this.pnlCard.AcentoSuperior = true;
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlCard.Controls.Add(this.btnAceptar);
            this.pnlCard.Controls.Add(this.txtConfirmarContrasenia);
            this.pnlCard.Controls.Add(this.label4);
            this.pnlCard.Controls.Add(this.lblAyudaNueva);
            this.pnlCard.Controls.Add(this.txtNuevaconstrasenia);
            this.pnlCard.Controls.Add(this.label3);
            this.pnlCard.Controls.Add(this.txtContrasenia);
            this.pnlCard.Controls.Add(this.label2);
            this.pnlCard.Controls.Add(this.txtUsuario);
            this.pnlCard.Controls.Add(this.label1);
            this.pnlCard.Controls.Add(this.lblSeccion);
            this.pnlCard.Location = new System.Drawing.Point(210, 24);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(480, 440);
            this.pnlCard.TabIndex = 0;
            //
            // lblSeccion
            //
            this.lblSeccion.AutoSize = true;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccion.Location = new System.Drawing.Point(26, 24);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Size = new System.Drawing.Size(150, 20);
            this.lblSeccion.TabIndex = 0;
            this.lblSeccion.Text = "Datos de acceso";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label1.Location = new System.Drawing.Point(28, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario";
            //
            // txtUsuario
            //
            this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtUsuario.Location = new System.Drawing.Point(28, 84);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(424, 26);
            this.txtUsuario.TabIndex = 2;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label2.Location = new System.Drawing.Point(28, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña actual";
            //
            // txtContrasenia
            //
            this.txtContrasenia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContrasenia.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtContrasenia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtContrasenia.Location = new System.Drawing.Point(28, 148);
            this.txtContrasenia.Name = "txtContrasenia";
            this.txtContrasenia.PasswordChar = '●';
            this.txtContrasenia.Size = new System.Drawing.Size(424, 26);
            this.txtContrasenia.TabIndex = 4;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label3.Location = new System.Drawing.Point(28, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nueva contraseña";
            //
            // txtNuevaconstrasenia
            //
            this.txtNuevaconstrasenia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevaconstrasenia.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNuevaconstrasenia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtNuevaconstrasenia.Location = new System.Drawing.Point(28, 212);
            this.txtNuevaconstrasenia.Name = "txtNuevaconstrasenia";
            this.txtNuevaconstrasenia.PasswordChar = '●';
            this.txtNuevaconstrasenia.Size = new System.Drawing.Size(424, 26);
            this.txtNuevaconstrasenia.TabIndex = 6;
            //
            // lblAyudaNueva
            //
            this.lblAyudaNueva.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyudaNueva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyudaNueva.Location = new System.Drawing.Point(28, 242);
            this.lblAyudaNueva.Name = "lblAyudaNueva";
            this.lblAyudaNueva.Size = new System.Drawing.Size(424, 32);
            this.lblAyudaNueva.TabIndex = 7;
            this.lblAyudaNueva.Text = "Entre 6 y 50 caracteres, con al menos una letra y un número.";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label4.Location = new System.Drawing.Point(28, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Confirmar contraseña";
            //
            // txtConfirmarContrasenia
            //
            this.txtConfirmarContrasenia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmarContrasenia.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtConfirmarContrasenia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtConfirmarContrasenia.Location = new System.Drawing.Point(28, 304);
            this.txtConfirmarContrasenia.Name = "txtConfirmarContrasenia";
            this.txtConfirmarContrasenia.PasswordChar = '●';
            this.txtConfirmarContrasenia.Size = new System.Drawing.Size(424, 26);
            this.txtConfirmarContrasenia.TabIndex = 9;
            //
            // btnAceptar
            //
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAceptar.Location = new System.Drawing.Point(292, 366);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(160, 42);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            //
            // FRMCambiarContrasenia
            //
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(900, 580);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMCambiarContrasenia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Contraseña";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCard;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAyudaNueva;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtUsuario;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtContrasenia;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtNuevaconstrasenia;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtConfirmarContrasenia;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAceptar;
    }
}
