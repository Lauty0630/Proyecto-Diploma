namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMMenuPrincipalUsuario
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
            this.mnuPrincipal = new PROYECTO_ING_DE_SOFTWARE.MenuModerno_GV42();
            this.idiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espanolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inglesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarClaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblIconoSesion = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuarioActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRolActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEspaciador = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIdiomaActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFechaActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlBienvenida = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblIconoBienvenida = new System.Windows.Forms.Label();
            this.lblSaludo = new System.Windows.Forms.Label();
            this.lblRolBienvenida = new System.Windows.Forms.Label();
            this.lblAyudaBienvenida = new System.Windows.Forms.Label();
            this.lblAccesos = new System.Windows.Forms.Label();
            this.flpAccesos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAccesoCambiarClave = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnAccesoCerrarSesion = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.mnuPrincipal.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlBienvenida.SuspendLayout();
            this.flpAccesos.SuspendLayout();
            this.SuspendLayout();
            //
            // mnuPrincipal
            //
            this.mnuPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.mnuPrincipal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.mnuPrincipal.ForeColor = System.Drawing.Color.White;
            this.mnuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.idiomaToolStripMenuItem,
            this.usuarioToolStripMenuItem});
            this.mnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnuPrincipal.Name = "mnuPrincipal";
            this.mnuPrincipal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.mnuPrincipal.Size = new System.Drawing.Size(1100, 35);
            this.mnuPrincipal.TabIndex = 0;
            this.mnuPrincipal.Text = "mnuPrincipal";
            //
            // idiomaToolStripMenuItem
            //
            this.idiomaToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.idiomaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.espanolToolStripMenuItem,
            this.inglesToolStripMenuItem});
            this.idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            this.idiomaToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.idiomaToolStripMenuItem.Size = new System.Drawing.Size(74, 23);
            this.idiomaToolStripMenuItem.Text = "Idioma";
            //
            // espanolToolStripMenuItem
            //
            this.espanolToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.espanolToolStripMenuItem.Name = "espanolToolStripMenuItem";
            this.espanolToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.espanolToolStripMenuItem.Text = "Español";
            this.espanolToolStripMenuItem.Click += new System.EventHandler(this.espanolToolStripMenuItem_Click);
            //
            // inglesToolStripMenuItem
            //
            this.inglesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.inglesToolStripMenuItem.Name = "inglesToolStripMenuItem";
            this.inglesToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.inglesToolStripMenuItem.Text = "English";
            this.inglesToolStripMenuItem.Click += new System.EventHandler(this.inglesToolStripMenuItem_Click);
            //
            // usuarioToolStripMenuItem
            //
            this.usuarioToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarClaveToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(78, 23);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            //
            // cambiarClaveToolStripMenuItem
            //
            this.cambiarClaveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cambiarClaveToolStripMenuItem.Name = "cambiarClaveToolStripMenuItem";
            this.cambiarClaveToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.cambiarClaveToolStripMenuItem.Text = "Cambiar Clave";
            this.cambiarClaveToolStripMenuItem.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            //
            // logOutToolStripMenuItem
            //
            this.logOutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.logOutToolStripMenuItem.Text = "LogOut";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            //
            // statusBar
            //
            this.statusBar.BackColor = System.Drawing.Color.White;
            this.statusBar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblIconoSesion,
            this.lblUsuarioActual,
            this.lblRolActual,
            this.lblEspaciador,
            this.lblIdiomaActual,
            this.lblFechaActual});
            this.statusBar.Location = new System.Drawing.Point(0, 652);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.statusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.statusBar.Size = new System.Drawing.Size(1100, 28);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 1;
            //
            // lblIconoSesion
            //
            this.lblIconoSesion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblIconoSesion.Name = "lblIconoSesion";
            this.lblIconoSesion.Size = new System.Drawing.Size(14, 17);
            this.lblIconoSesion.Text = "●";
            //
            // lblUsuarioActual
            //
            this.lblUsuarioActual.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuarioActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblUsuarioActual.Name = "lblUsuarioActual";
            this.lblUsuarioActual.Size = new System.Drawing.Size(120, 17);
            this.lblUsuarioActual.Text = "Sesión: (no iniciada)";
            //
            // lblRolActual
            //
            this.lblRolActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblRolActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblRolActual.Margin = new System.Windows.Forms.Padding(12, 2, 0, 2);
            this.lblRolActual.Name = "lblRolActual";
            this.lblRolActual.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblRolActual.Size = new System.Drawing.Size(60, 18);
            this.lblRolActual.Text = "Rol: -";
            //
            // lblEspaciador
            //
            this.lblEspaciador.Name = "lblEspaciador";
            this.lblEspaciador.Size = new System.Drawing.Size(700, 17);
            this.lblEspaciador.Spring = true;
            //
            // lblIdiomaActual
            //
            this.lblIdiomaActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblIdiomaActual.Name = "lblIdiomaActual";
            this.lblIdiomaActual.Size = new System.Drawing.Size(100, 17);
            this.lblIdiomaActual.Text = "Idioma: Español";
            //
            // lblFechaActual
            //
            this.lblFechaActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblFechaActual.Margin = new System.Windows.Forms.Padding(16, 3, 0, 2);
            this.lblFechaActual.Name = "lblFechaActual";
            this.lblFechaActual.Size = new System.Drawing.Size(70, 17);
            this.lblFechaActual.Text = "01/01/2026";
            //
            // pnlContenido
            //
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.pnlContenido.Controls.Add(this.pnlBienvenida);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 35);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1100, 617);
            this.pnlContenido.TabIndex = 2;
            //
            // pnlBienvenida
            //
            this.pnlBienvenida.AcentoSuperior = true;
            this.pnlBienvenida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlBienvenida.Controls.Add(this.flpAccesos);
            this.pnlBienvenida.Controls.Add(this.lblAccesos);
            this.pnlBienvenida.Controls.Add(this.lblAyudaBienvenida);
            this.pnlBienvenida.Controls.Add(this.lblRolBienvenida);
            this.pnlBienvenida.Controls.Add(this.lblSaludo);
            this.pnlBienvenida.Controls.Add(this.lblIconoBienvenida);
            this.pnlBienvenida.Location = new System.Drawing.Point(240, 158);
            this.pnlBienvenida.Name = "pnlBienvenida";
            this.pnlBienvenida.Size = new System.Drawing.Size(620, 300);
            this.pnlBienvenida.TabIndex = 0;
            //
            // lblIconoBienvenida
            //
            this.lblIconoBienvenida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblIconoBienvenida.Font = new System.Drawing.Font("Segoe UI Symbol", 26F);
            this.lblIconoBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblIconoBienvenida.Location = new System.Drawing.Point(28, 30);
            this.lblIconoBienvenida.Name = "lblIconoBienvenida";
            this.lblIconoBienvenida.Size = new System.Drawing.Size(64, 64);
            this.lblIconoBienvenida.TabIndex = 0;
            this.lblIconoBienvenida.Text = "✈";
            this.lblIconoBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSaludo
            //
            this.lblSaludo.AutoSize = true;
            this.lblSaludo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblSaludo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSaludo.Location = new System.Drawing.Point(108, 30);
            this.lblSaludo.Name = "lblSaludo";
            this.lblSaludo.Size = new System.Drawing.Size(164, 32);
            this.lblSaludo.TabIndex = 1;
            this.lblSaludo.Text = "¡Bienvenido!";
            //
            // lblRolBienvenida
            //
            this.lblRolBienvenida.AutoSize = true;
            this.lblRolBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRolBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblRolBienvenida.Location = new System.Drawing.Point(111, 70);
            this.lblRolBienvenida.Name = "lblRolBienvenida";
            this.lblRolBienvenida.Size = new System.Drawing.Size(160, 19);
            this.lblRolBienvenida.TabIndex = 2;
            this.lblRolBienvenida.Text = "Ingresaste como: Usuario";
            //
            // lblAyudaBienvenida
            //
            this.lblAyudaBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAyudaBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblAyudaBienvenida.Location = new System.Drawing.Point(28, 116);
            this.lblAyudaBienvenida.Name = "lblAyudaBienvenida";
            this.lblAyudaBienvenida.Size = new System.Drawing.Size(564, 44);
            this.lblAyudaBienvenida.TabIndex = 3;
            this.lblAyudaBienvenida.Text = "Elegí una opción del menú superior o usá uno de los accesos rápidos.";
            //
            // lblAccesos
            //
            this.lblAccesos.AutoSize = true;
            this.lblAccesos.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblAccesos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblAccesos.Location = new System.Drawing.Point(26, 170);
            this.lblAccesos.Name = "lblAccesos";
            this.lblAccesos.Size = new System.Drawing.Size(124, 20);
            this.lblAccesos.TabIndex = 4;
            this.lblAccesos.Text = "Accesos rápidos";
            //
            // flpAccesos
            //
            this.flpAccesos.Controls.Add(this.btnAccesoCambiarClave);
            this.flpAccesos.Controls.Add(this.btnAccesoCerrarSesion);
            this.flpAccesos.Location = new System.Drawing.Point(24, 204);
            this.flpAccesos.Name = "flpAccesos";
            this.flpAccesos.Size = new System.Drawing.Size(572, 52);
            this.flpAccesos.TabIndex = 5;
            this.flpAccesos.WrapContents = false;
            //
            // btnAccesoCambiarClave
            //
            this.btnAccesoCambiarClave.FlatAppearance.BorderSize = 0;
            this.btnAccesoCambiarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccesoCambiarClave.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccesoCambiarClave.Location = new System.Drawing.Point(3, 3);
            this.btnAccesoCambiarClave.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnAccesoCambiarClave.Name = "btnAccesoCambiarClave";
            this.btnAccesoCambiarClave.Size = new System.Drawing.Size(176, 42);
            this.btnAccesoCambiarClave.TabIndex = 0;
            this.btnAccesoCambiarClave.Text = "Cambiar clave";
            this.btnAccesoCambiarClave.UseVisualStyleBackColor = false;
            this.btnAccesoCambiarClave.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            //
            // btnAccesoCerrarSesion
            //
            this.btnAccesoCerrarSesion.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnAccesoCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnAccesoCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccesoCerrarSesion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccesoCerrarSesion.Location = new System.Drawing.Point(192, 3);
            this.btnAccesoCerrarSesion.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnAccesoCerrarSesion.Name = "btnAccesoCerrarSesion";
            this.btnAccesoCerrarSesion.Size = new System.Drawing.Size(176, 42);
            this.btnAccesoCerrarSesion.TabIndex = 1;
            this.btnAccesoCerrarSesion.Text = "Cerrar sesión";
            this.btnAccesoCerrarSesion.UseVisualStyleBackColor = false;
            this.btnAccesoCerrarSesion.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            //
            // FRMMenuPrincipalUsuario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mnuPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.mnuPrincipal;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FRMMenuPrincipalUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Principal — Usuario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRMMenuPrincipalUsuario_Load);
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlBienvenida.ResumeLayout(false);
            this.pnlBienvenida.PerformLayout();
            this.flpAccesos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.MenuModerno_GV42 mnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem idiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espanolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inglesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarClaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblIconoSesion;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuarioActual;
        private System.Windows.Forms.ToolStripStatusLabel lblRolActual;
        private System.Windows.Forms.ToolStripStatusLabel lblEspaciador;
        private System.Windows.Forms.ToolStripStatusLabel lblIdiomaActual;
        private System.Windows.Forms.ToolStripStatusLabel lblFechaActual;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlBienvenida;
        private System.Windows.Forms.Label lblIconoBienvenida;
        private System.Windows.Forms.Label lblSaludo;
        private System.Windows.Forms.Label lblRolBienvenida;
        private System.Windows.Forms.Label lblAyudaBienvenida;
        private System.Windows.Forms.Label lblAccesos;
        private System.Windows.Forms.FlowLayoutPanel flpAccesos;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAccesoCambiarClave;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAccesoCerrarSesion;
    }
}
