namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMReinstalador_GV42
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
            this.pnlCard = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.rbRestaurar = new System.Windows.Forms.RadioButton();
            this.txtArchivo = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.btnElegirArchivo = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.rbReinstalar = new System.Windows.Forms.RadioButton();
            this.lblAdvertencia = new System.Windows.Forms.Label();
            this.btnEjecutar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
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
            this.lblTitulo.Text = "Reinstalador";
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
            this.lblSubtitulo.Text = "Recuperar la base de datos de FLY SAFE";
            // 
            // pnlCard
            // 
            this.pnlCard.AcentoSuperior = true;
            this.pnlCard.Controls.Add(this.lblAdvertencia);
            this.pnlCard.Controls.Add(this.rbReinstalar);
            this.pnlCard.Controls.Add(this.btnElegirArchivo);
            this.pnlCard.Controls.Add(this.txtArchivo);
            this.pnlCard.Controls.Add(this.rbRestaurar);
            this.pnlCard.Controls.Add(this.lblOperacion);
            this.pnlCard.Controls.Add(this.txtContrasena);
            this.pnlCard.Controls.Add(this.lblContrasena);
            this.pnlCard.Controls.Add(this.txtUsuario);
            this.pnlCard.Controls.Add(this.lblUsuario);
            this.pnlCard.Controls.Add(this.lblAdmin);
            this.pnlCard.Location = new System.Drawing.Point(24, 100);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(572, 372);
            this.pnlCard.TabIndex = 0;
            // 
            // lblAdmin
            // 
            this.lblAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblAdmin.Location = new System.Drawing.Point(22, 20);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(528, 40);
            this.lblAdmin.TabIndex = 0;
            this.lblAdmin.Text = "Ingresá un usuario administrador para autorizar la operación.";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblUsuario.Location = new System.Drawing.Point(22, 66);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(80, 17);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtUsuario.Location = new System.Drawing.Point(22, 88);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(250, 26);
            this.txtUsuario.TabIndex = 2;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblContrasena.Location = new System.Drawing.Point(296, 66);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(80, 17);
            this.lblContrasena.TabIndex = 3;
            this.lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            this.txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtContrasena.Location = new System.Drawing.Point(296, 88);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '●';
            this.txtContrasena.Size = new System.Drawing.Size(254, 26);
            this.txtContrasena.TabIndex = 4;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblOperacion.Location = new System.Drawing.Point(22, 134);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(160, 17);
            this.lblOperacion.TabIndex = 5;
            this.lblOperacion.Text = "¿Qué querés hacer?";
            // 
            // rbRestaurar
            // 
            this.rbRestaurar.AutoSize = true;
            this.rbRestaurar.Checked = true;
            this.rbRestaurar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.rbRestaurar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.rbRestaurar.Location = new System.Drawing.Point(22, 160);
            this.rbRestaurar.Name = "rbRestaurar";
            this.rbRestaurar.Size = new System.Drawing.Size(180, 23);
            this.rbRestaurar.TabIndex = 6;
            this.rbRestaurar.TabStop = true;
            this.rbRestaurar.Text = "Restaurar un backup (.bak)";
            this.rbRestaurar.UseVisualStyleBackColor = true;
            this.rbRestaurar.CheckedChanged += new System.EventHandler(this.opcion_CheckedChanged);
            // 
            // txtArchivo
            // 
            this.txtArchivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArchivo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtArchivo.Location = new System.Drawing.Point(42, 190);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.ReadOnly = true;
            this.txtArchivo.Size = new System.Drawing.Size(458, 26);
            this.txtArchivo.TabIndex = 7;
            // 
            // btnElegirArchivo
            // 
            this.btnElegirArchivo.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnElegirArchivo.FlatAppearance.BorderSize = 0;
            this.btnElegirArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnElegirArchivo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnElegirArchivo.Location = new System.Drawing.Point(508, 188);
            this.btnElegirArchivo.Name = "btnElegirArchivo";
            this.btnElegirArchivo.Size = new System.Drawing.Size(42, 30);
            this.btnElegirArchivo.TabIndex = 8;
            this.btnElegirArchivo.Text = "...";
            this.btnElegirArchivo.UseVisualStyleBackColor = false;
            this.btnElegirArchivo.Click += new System.EventHandler(this.btnElegirArchivo_Click);
            // 
            // rbReinstalar
            // 
            this.rbReinstalar.AutoSize = true;
            this.rbReinstalar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.rbReinstalar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.rbReinstalar.Location = new System.Drawing.Point(22, 236);
            this.rbReinstalar.Name = "rbReinstalar";
            this.rbReinstalar.Size = new System.Drawing.Size(180, 23);
            this.rbReinstalar.TabIndex = 9;
            this.rbReinstalar.Text = "Reinstalar la base limpia";
            this.rbReinstalar.UseVisualStyleBackColor = true;
            this.rbReinstalar.CheckedChanged += new System.EventHandler(this.opcion_CheckedChanged);
            // 
            // lblAdvertencia
            // 
            this.lblAdvertencia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAdvertencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblAdvertencia.Location = new System.Drawing.Point(22, 276);
            this.lblAdvertencia.Name = "lblAdvertencia";
            this.lblAdvertencia.Size = new System.Drawing.Size(528, 80);
            this.lblAdvertencia.TabIndex = 10;
            this.lblAdvertencia.Text = "La base vuelve al estado del backup elegido: se pierde todo lo cargado después.";
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Peligro;
            this.btnEjecutar.FlatAppearance.BorderSize = 0;
            this.btnEjecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjecutar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnEjecutar.Location = new System.Drawing.Point(352, 488);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(120, 40);
            this.btnEjecutar.TabIndex = 1;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = false;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(482, 488);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(114, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FRMReinstalador_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(620, 546);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMReinstalador_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reinstalador";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCard;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Label lblUsuario;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtContrasena;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.RadioButton rbRestaurar;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtArchivo;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnElegirArchivo;
        private System.Windows.Forms.RadioButton rbReinstalar;
        private System.Windows.Forms.Label lblAdvertencia;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnEjecutar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
    }
}
