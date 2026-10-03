namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMIniciarSesion
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
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblEslogan = new System.Windows.Forms.Label();
            this.lnkEspanol = new System.Windows.Forms.LinkLabel();
            this.lnkReinstalador = new System.Windows.Forms.LinkLabel();
            this.lblSeparadorIdioma = new System.Windows.Forms.Label();
            this.lnkIngles = new System.Windows.Forms.LinkLabel();
            this.pnlCard = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogIn = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContrasena = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.btnIngresar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.tlpRegistro = new System.Windows.Forms.TableLayoutPanel();
            this.lnkRegistro = new System.Windows.Forms.LinkLabel();
            this.pnlEncabezado.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.tlpRegistro.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lnkReinstalador);
            this.pnlEncabezado.Controls.Add(this.lnkIngles);
            this.pnlEncabezado.Controls.Add(this.lblSeparadorIdioma);
            this.pnlEncabezado.Controls.Add(this.lnkEspanol);
            this.pnlEncabezado.Controls.Add(this.lblEslogan);
            this.pnlEncabezado.Controls.Add(this.lblMarca);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(480, 150);
            this.pnlEncabezado.TabIndex = 1;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.BackColor = System.Drawing.Color.Transparent;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(30, 44);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(150, 45);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "FLY SAFE";
            // 
            // lblEslogan
            // 
            this.lblEslogan.AutoSize = true;
            this.lblEslogan.BackColor = System.Drawing.Color.Transparent;
            this.lblEslogan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEslogan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblEslogan.Location = new System.Drawing.Point(34, 92);
            this.lblEslogan.Name = "lblEslogan";
            this.lblEslogan.Size = new System.Drawing.Size(196, 19);
            this.lblEslogan.TabIndex = 1;
            this.lblEslogan.Text = "Sistema de reservas de vuelos";
            // 
            // lnkReinstalador
            // 
            this.lnkReinstalador.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkReinstalador.AutoSize = true;
            this.lnkReinstalador.BackColor = System.Drawing.Color.Transparent;
            this.lnkReinstalador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lnkReinstalador.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkReinstalador.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.lnkReinstalador.Location = new System.Drawing.Point(32, 12);
            this.lnkReinstalador.Name = "lnkReinstalador";
            this.lnkReinstalador.Size = new System.Drawing.Size(74, 15);
            this.lnkReinstalador.TabIndex = 9;
            this.lnkReinstalador.TabStop = true;
            this.lnkReinstalador.Text = "Reinstalador";
            this.lnkReinstalador.Visible = false;
            this.lnkReinstalador.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReinstalador_LinkClicked);
            // 
            // lnkEspanol
            // 
            this.lnkEspanol.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkEspanol.AutoSize = true;
            this.lnkEspanol.BackColor = System.Drawing.Color.Transparent;
            this.lnkEspanol.DisabledLinkColor = System.Drawing.Color.White;
            this.lnkEspanol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lnkEspanol.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkEspanol.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.lnkEspanol.Location = new System.Drawing.Point(398, 12);
            this.lnkEspanol.Name = "lnkEspanol";
            this.lnkEspanol.Size = new System.Drawing.Size(22, 15);
            this.lnkEspanol.TabIndex = 6;
            this.lnkEspanol.TabStop = true;
            this.lnkEspanol.Text = "ES";
            this.lnkEspanol.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEspanol_LinkClicked);
            // 
            // lblSeparadorIdioma
            // 
            this.lblSeparadorIdioma.AutoSize = true;
            this.lblSeparadorIdioma.BackColor = System.Drawing.Color.Transparent;
            this.lblSeparadorIdioma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSeparadorIdioma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.lblSeparadorIdioma.Location = new System.Drawing.Point(421, 12);
            this.lblSeparadorIdioma.Name = "lblSeparadorIdioma";
            this.lblSeparadorIdioma.Size = new System.Drawing.Size(10, 15);
            this.lblSeparadorIdioma.TabIndex = 7;
            this.lblSeparadorIdioma.Text = "|";
            // 
            // lnkIngles
            // 
            this.lnkIngles.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkIngles.AutoSize = true;
            this.lnkIngles.BackColor = System.Drawing.Color.Transparent;
            this.lnkIngles.DisabledLinkColor = System.Drawing.Color.White;
            this.lnkIngles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lnkIngles.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkIngles.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.lnkIngles.Location = new System.Drawing.Point(432, 12);
            this.lnkIngles.Name = "lnkIngles";
            this.lnkIngles.Size = new System.Drawing.Size(22, 15);
            this.lnkIngles.TabIndex = 8;
            this.lnkIngles.TabStop = true;
            this.lnkIngles.Text = "EN";
            this.lnkIngles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIngles_LinkClicked);
            // 
            // pnlCard
            // 
            this.pnlCard.AcentoSuperior = true;
            this.pnlCard.Controls.Add(this.tlpRegistro);
            this.pnlCard.Controls.Add(this.btnIngresar);
            this.pnlCard.Controls.Add(this.txtContrasena);
            this.pnlCard.Controls.Add(this.label2);
            this.pnlCard.Controls.Add(this.txtLogIn);
            this.pnlCard.Controls.Add(this.label1);
            this.pnlCard.Controls.Add(this.lblSubtitulo);
            this.pnlCard.Controls.Add(this.lblTitulo);
            this.pnlCard.Location = new System.Drawing.Point(32, 174);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(416, 372);
            this.pnlCard.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitulo.Location = new System.Drawing.Point(26, 26);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(150, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Iniciar Sesión";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(240, 17);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Ingresá tus credenciales para continuar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label1.Location = new System.Drawing.Point(28, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // txtLogIn
            // 
            this.txtLogIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogIn.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtLogIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtLogIn.Location = new System.Drawing.Point(28, 126);
            this.txtLogIn.Name = "txtLogIn";
            this.txtLogIn.Size = new System.Drawing.Size(360, 26);
            this.txtLogIn.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label2.Location = new System.Drawing.Point(28, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtContrasena.Location = new System.Drawing.Point(28, 190);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '●';
            this.txtContrasena.Size = new System.Drawing.Size(360, 26);
            this.txtContrasena.TabIndex = 5;
            // 
            // btnIngresar
            // 
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.Location = new System.Drawing.Point(28, 244);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(360, 42);
            this.btnIngresar.TabIndex = 6;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // tlpRegistro
            // 
            this.tlpRegistro.ColumnCount = 1;
            this.tlpRegistro.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRegistro.Controls.Add(this.lnkRegistro, 0, 0);
            this.tlpRegistro.Location = new System.Drawing.Point(28, 304);
            this.tlpRegistro.Name = "tlpRegistro";
            this.tlpRegistro.RowCount = 1;
            this.tlpRegistro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRegistro.Size = new System.Drawing.Size(360, 40);
            this.tlpRegistro.TabIndex = 7;
            // 
            // lnkRegistro
            // 
            this.lnkRegistro.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lnkRegistro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkRegistro.AutoSize = true;
            this.lnkRegistro.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lnkRegistro.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkRegistro.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lnkRegistro.Location = new System.Drawing.Point(46, 11);
            this.lnkRegistro.Name = "lnkRegistro";
            this.lnkRegistro.Size = new System.Drawing.Size(268, 17);
            this.lnkRegistro.TabIndex = 0;
            this.lnkRegistro.TabStop = true;
            this.lnkRegistro.Text = "¿Sos cliente y no tenés cuenta? Registrate";
            this.lnkRegistro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegistro_LinkClicked);
            // 
            // FRMIniciarSesion
            // 
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(480, 572);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FRMIniciarSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.tlpRegistro.ResumeLayout(false);
            this.tlpRegistro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblEslogan;
        private System.Windows.Forms.LinkLabel lnkEspanol;
        private System.Windows.Forms.LinkLabel lnkReinstalador;
        private System.Windows.Forms.Label lblSeparadorIdioma;
        private System.Windows.Forms.LinkLabel lnkIngles;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCard;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label label1;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtLogIn;
        private System.Windows.Forms.Label label2;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtContrasena;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnIngresar;
        private System.Windows.Forms.TableLayoutPanel tlpRegistro;
        private System.Windows.Forms.LinkLabel lnkRegistro;
    }
}
