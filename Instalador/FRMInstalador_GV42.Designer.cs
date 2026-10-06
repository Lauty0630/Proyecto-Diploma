namespace Instalador
{
    partial class FRMInstalador_GV42
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.grpDestino = new System.Windows.Forms.GroupBox();
            this.txtCarpeta = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.grpBase = new System.Windows.Forms.GroupBox();
            this.lblInstancia = new System.Windows.Forms.Label();
            this.cmbInstancia = new System.Windows.Forms.ComboBox();
            this.btnDetectar = new System.Windows.Forms.Button();
            this.rbPreparar = new System.Windows.Forms.RadioButton();
            this.rbRestaurar = new System.Windows.Forms.RadioButton();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.rbLimpia = new System.Windows.Forms.RadioButton();
            this.grpAdministrador = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.chkAccesoDirecto = new System.Windows.Forms.CheckBox();
            this.txtProgreso = new System.Windows.Forms.TextBox();
            this.btnInstalar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpDestino.SuspendLayout();
            this.grpBase.SuspendLayout();
            this.grpAdministrador.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(600, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Instalador de FLY SAFE";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = false;
            this.lblEstado.Location = new System.Drawing.Point(22, 52);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(600, 20);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Estado";
            // 
            // grpDestino
            // 
            this.grpDestino.Controls.Add(this.txtCarpeta);
            this.grpDestino.Controls.Add(this.btnExaminar);
            this.grpDestino.Location = new System.Drawing.Point(20, 82);
            this.grpDestino.Name = "grpDestino";
            this.grpDestino.Size = new System.Drawing.Size(600, 62);
            this.grpDestino.TabIndex = 2;
            this.grpDestino.TabStop = false;
            this.grpDestino.Text = "1. Carpeta de instalación";
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.Location = new System.Drawing.Point(15, 25);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.Size = new System.Drawing.Size(460, 23);
            this.txtCarpeta.TabIndex = 3;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(485, 23);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(100, 27);
            this.btnExaminar.TabIndex = 4;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // grpBase
            // 
            this.grpBase.Controls.Add(this.lblInstancia);
            this.grpBase.Controls.Add(this.cmbInstancia);
            this.grpBase.Controls.Add(this.btnDetectar);
            this.grpBase.Controls.Add(this.rbPreparar);
            this.grpBase.Controls.Add(this.rbRestaurar);
            this.grpBase.Controls.Add(this.txtBackup);
            this.grpBase.Controls.Add(this.btnBackup);
            this.grpBase.Controls.Add(this.rbLimpia);
            this.grpBase.Controls.Add(this.grpAdministrador);
            this.grpBase.Location = new System.Drawing.Point(20, 152);
            this.grpBase.Name = "grpBase";
            this.grpBase.Size = new System.Drawing.Size(600, 270);
            this.grpBase.TabIndex = 5;
            this.grpBase.TabStop = false;
            this.grpBase.Text = "2. Base de datos";
            // 
            // lblInstancia
            // 
            this.lblInstancia.AutoSize = false;
            this.lblInstancia.Location = new System.Drawing.Point(15, 28);
            this.lblInstancia.Name = "lblInstancia";
            this.lblInstancia.Size = new System.Drawing.Size(150, 20);
            this.lblInstancia.TabIndex = 6;
            this.lblInstancia.Text = "Instancia de SQL Server:";
            // 
            // cmbInstancia
            // 
            this.cmbInstancia.Location = new System.Drawing.Point(170, 25);
            this.cmbInstancia.Name = "cmbInstancia";
            this.cmbInstancia.Size = new System.Drawing.Size(305, 23);
            this.cmbInstancia.TabIndex = 7;
            // 
            // btnDetectar
            // 
            this.btnDetectar.Location = new System.Drawing.Point(485, 23);
            this.btnDetectar.Name = "btnDetectar";
            this.btnDetectar.Size = new System.Drawing.Size(100, 27);
            this.btnDetectar.TabIndex = 8;
            this.btnDetectar.Text = "Detectar";
            this.btnDetectar.UseVisualStyleBackColor = true;
            this.btnDetectar.Click += new System.EventHandler(this.btnDetectar_Click);
            // 
            // rbPreparar
            // 
            this.rbPreparar.AutoSize = true;
            this.rbPreparar.Location = new System.Drawing.Point(18, 62);
            this.rbPreparar.Name = "rbPreparar";
            this.rbPreparar.Size = new System.Drawing.Size(560, 22);
            this.rbPreparar.TabIndex = 9;
            this.rbPreparar.Text = "Preparar la base: crearla si no existe, o actualizarla conservando los datos";
            this.rbPreparar.UseVisualStyleBackColor = true;
            this.rbPreparar.CheckedChanged += new System.EventHandler(this.rbOpcion_CheckedChanged);
            // 
            // rbRestaurar
            // 
            this.rbRestaurar.AutoSize = true;
            this.rbRestaurar.Location = new System.Drawing.Point(18, 90);
            this.rbRestaurar.Name = "rbRestaurar";
            this.rbRestaurar.Size = new System.Drawing.Size(560, 22);
            this.rbRestaurar.TabIndex = 10;
            this.rbRestaurar.Text = "Restaurar un backup (.bak)";
            this.rbRestaurar.UseVisualStyleBackColor = true;
            this.rbRestaurar.CheckedChanged += new System.EventHandler(this.rbOpcion_CheckedChanged);
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(38, 116);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.Size = new System.Drawing.Size(437, 23);
            this.txtBackup.TabIndex = 11;
            this.txtBackup.ReadOnly = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(485, 114);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(100, 27);
            this.btnBackup.TabIndex = 12;
            this.btnBackup.Text = "Elegir...";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // rbLimpia
            // 
            this.rbLimpia.AutoSize = true;
            this.rbLimpia.Location = new System.Drawing.Point(18, 148);
            this.rbLimpia.Name = "rbLimpia";
            this.rbLimpia.Size = new System.Drawing.Size(560, 22);
            this.rbLimpia.TabIndex = 13;
            this.rbLimpia.Text = "Reinstalar la base limpia (se borran todos los datos)";
            this.rbLimpia.UseVisualStyleBackColor = true;
            this.rbLimpia.CheckedChanged += new System.EventHandler(this.rbOpcion_CheckedChanged);
            // 
            // grpAdministrador
            // 
            this.grpAdministrador.Controls.Add(this.lblUsuario);
            this.grpAdministrador.Controls.Add(this.txtUsuario);
            this.grpAdministrador.Controls.Add(this.lblContrasena);
            this.grpAdministrador.Controls.Add(this.txtContrasena);
            this.grpAdministrador.Location = new System.Drawing.Point(18, 178);
            this.grpAdministrador.Name = "grpAdministrador";
            this.grpAdministrador.Size = new System.Drawing.Size(567, 80);
            this.grpAdministrador.TabIndex = 14;
            this.grpAdministrador.TabStop = false;
            this.grpAdministrador.Text = "Autorización de un administrador del sistema (si la base actual se puede leer)";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = false;
            this.lblUsuario.Location = new System.Drawing.Point(15, 35);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(60, 20);
            this.lblUsuario.TabIndex = 15;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(80, 32);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(170, 23);
            this.txtUsuario.TabIndex = 16;
            this.txtUsuario.MaxLength = 50;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = false;
            this.lblContrasena.Location = new System.Drawing.Point(275, 35);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(80, 20);
            this.lblContrasena.TabIndex = 17;
            this.lblContrasena.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(360, 32);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(170, 23);
            this.txtContrasena.TabIndex = 18;
            this.txtContrasena.MaxLength = 50;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // chkAccesoDirecto
            // 
            this.chkAccesoDirecto.AutoSize = true;
            this.chkAccesoDirecto.Location = new System.Drawing.Point(22, 432);
            this.chkAccesoDirecto.Name = "chkAccesoDirecto";
            this.chkAccesoDirecto.Size = new System.Drawing.Size(400, 22);
            this.chkAccesoDirecto.TabIndex = 19;
            this.chkAccesoDirecto.Text = "Crear un acceso directo en el escritorio";
            this.chkAccesoDirecto.UseVisualStyleBackColor = true;
            this.chkAccesoDirecto.Checked = true;
            this.chkAccesoDirecto.CheckState = System.Windows.Forms.CheckState.Checked;
            // 
            // txtProgreso
            // 
            this.txtProgreso.Location = new System.Drawing.Point(20, 462);
            this.txtProgreso.Name = "txtProgreso";
            this.txtProgreso.Size = new System.Drawing.Size(600, 130);
            this.txtProgreso.TabIndex = 20;
            this.txtProgreso.Multiline = true;
            this.txtProgreso.ReadOnly = true;
            this.txtProgreso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // btnInstalar
            // 
            this.btnInstalar.Location = new System.Drawing.Point(380, 604);
            this.btnInstalar.Name = "btnInstalar";
            this.btnInstalar.Size = new System.Drawing.Size(115, 32);
            this.btnInstalar.TabIndex = 21;
            this.btnInstalar.Text = "Instalar";
            this.btnInstalar.UseVisualStyleBackColor = true;
            this.btnInstalar.Click += new System.EventHandler(this.btnInstalar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(505, 604);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(115, 32);
            this.btnSalir.TabIndex = 22;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FRMInstalador_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 650);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.grpDestino);
            this.Controls.Add(this.grpBase);
            this.Controls.Add(this.chkAccesoDirecto);
            this.Controls.Add(this.txtProgreso);
            this.Controls.Add(this.btnInstalar);
            this.Controls.Add(this.btnSalir);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FRMInstalador_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLY SAFE - Instalación";
            this.Load += new System.EventHandler(this.FRMInstalador_GV42_Load);
            this.grpDestino.ResumeLayout(false);
            this.grpDestino.PerformLayout();
            this.grpBase.ResumeLayout(false);
            this.grpBase.PerformLayout();
            this.grpAdministrador.ResumeLayout(false);
            this.grpAdministrador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox grpDestino;
        private System.Windows.Forms.TextBox txtCarpeta;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.GroupBox grpBase;
        private System.Windows.Forms.Label lblInstancia;
        private System.Windows.Forms.ComboBox cmbInstancia;
        private System.Windows.Forms.Button btnDetectar;
        private System.Windows.Forms.RadioButton rbPreparar;
        private System.Windows.Forms.RadioButton rbRestaurar;
        private System.Windows.Forms.TextBox txtBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.RadioButton rbLimpia;
        private System.Windows.Forms.GroupBox grpAdministrador;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.CheckBox chkAccesoDirecto;
        private System.Windows.Forms.TextBox txtProgreso;
        private System.Windows.Forms.Button btnInstalar;
        private System.Windows.Forms.Button btnSalir;
    }
}
