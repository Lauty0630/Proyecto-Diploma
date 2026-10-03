namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMAyuda_GV42
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
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTema = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.lblTemaTitulo = new System.Windows.Forms.Label();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.pnlArbol = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tvTemas = new System.Windows.Forms.TreeView();
            this.lblTemas = new System.Windows.Forms.Label();
            this.pnlBotonera = new System.Windows.Forms.Panel();
            this.btnExportar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCerrar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlTema.SuspendLayout();
            this.pnlArbol.SuspendLayout();
            this.pnlBotonera.SuspendLayout();
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
            this.pnlEncabezado.TabIndex = 10;
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
            this.lblTitulo.Text = "Ayuda";
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
            this.lblSubtitulo.Text = "Elegí un tema del árbol. Con F1 se abre la ayuda de la pantalla en la que estás.";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.pnlTema);
            this.pnlContenido.Controls.Add(this.pnlSeparador);
            this.pnlContenido.Controls.Add(this.pnlArbol);
            this.pnlContenido.Controls.Add(this.pnlBotonera);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 16);
            this.pnlContenido.Size = new System.Drawing.Size(900, 520);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlTema
            // 
            this.pnlTema.AcentoSuperior = true;
            this.pnlTema.Controls.Add(this.txtTexto);
            this.pnlTema.Controls.Add(this.lblTemaTitulo);
            this.pnlTema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTema.Location = new System.Drawing.Point(320, 20);
            this.pnlTema.Name = "pnlTema";
            this.pnlTema.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlTema.Size = new System.Drawing.Size(556, 428);
            this.pnlTema.TabIndex = 1;
            // 
            // txtTexto
            // 
            this.txtTexto.BackColor = System.Drawing.Color.White;
            this.txtTexto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTexto.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtTexto.Location = new System.Drawing.Point(20, 52);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ReadOnly = true;
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTexto.Size = new System.Drawing.Size(516, 360);
            this.txtTexto.TabIndex = 1;
            this.txtTexto.TabStop = false;
            // 
            // lblTemaTitulo
            // 
            this.lblTemaTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTemaTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTemaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTemaTitulo.Location = new System.Drawing.Point(20, 16);
            this.lblTemaTitulo.Name = "lblTemaTitulo";
            this.lblTemaTitulo.Size = new System.Drawing.Size(516, 36);
            this.lblTemaTitulo.TabIndex = 0;
            this.lblTemaTitulo.Text = "Tema";
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSeparador.Location = new System.Drawing.Point(304, 20);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(16, 428);
            this.pnlSeparador.TabIndex = 3;
            // 
            // pnlArbol
            // 
            this.pnlArbol.Controls.Add(this.tvTemas);
            this.pnlArbol.Controls.Add(this.lblTemas);
            this.pnlArbol.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlArbol.Location = new System.Drawing.Point(24, 20);
            this.pnlArbol.Name = "pnlArbol";
            this.pnlArbol.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.pnlArbol.Size = new System.Drawing.Size(280, 428);
            this.pnlArbol.TabIndex = 0;
            // 
            // tvTemas
            // 
            this.tvTemas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvTemas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTemas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tvTemas.FullRowSelect = true;
            this.tvTemas.HideSelection = false;
            this.tvTemas.ItemHeight = 26;
            this.tvTemas.Location = new System.Drawing.Point(16, 42);
            this.tvTemas.Name = "tvTemas";
            this.tvTemas.ShowLines = false;
            this.tvTemas.Size = new System.Drawing.Size(248, 370);
            this.tvTemas.TabIndex = 1;
            this.tvTemas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTemas_AfterSelect);
            // 
            // lblTemas
            // 
            this.lblTemas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTemas.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTemas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTemas.Location = new System.Drawing.Point(16, 12);
            this.lblTemas.Name = "lblTemas";
            this.lblTemas.Size = new System.Drawing.Size(248, 30);
            this.lblTemas.TabIndex = 0;
            this.lblTemas.Text = "Temas";
            // 
            // pnlBotonera
            // 
            this.pnlBotonera.Controls.Add(this.btnExportar);
            this.pnlBotonera.Controls.Add(this.btnCerrar);
            this.pnlBotonera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotonera.Location = new System.Drawing.Point(24, 448);
            this.pnlBotonera.Name = "pnlBotonera";
            this.pnlBotonera.Size = new System.Drawing.Size(852, 56);
            this.pnlBotonera.TabIndex = 2;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportar.Location = new System.Drawing.Point(558, 14);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(170, 40);
            this.btnExportar.TabIndex = 0;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(738, 14);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(114, 40);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FRMAyuda_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(760, 480);
            this.Name = "FRMAyuda_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlTema.ResumeLayout(false);
            this.pnlTema.PerformLayout();
            this.pnlArbol.ResumeLayout(false);
            this.pnlBotonera.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTema;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblTemaTitulo;
        private System.Windows.Forms.Panel pnlSeparador;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlArbol;
        private System.Windows.Forms.TreeView tvTemas;
        private System.Windows.Forms.Label lblTemas;
        private System.Windows.Forms.Panel pnlBotonera;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnExportar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCerrar;
    }
}
