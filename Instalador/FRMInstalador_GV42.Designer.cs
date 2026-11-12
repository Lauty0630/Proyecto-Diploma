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

        // Asistente de varias pantallas (una por panel "pag..."), con la cabecera arriba y los botones abajo.
        // Solo está acá lo fijo: las opciones de "Datos iniciales" cambian según lo que haya en la instancia
        // y se crean en tiempo de ejecución (FRMInstalador_GV42.MostrarOpciones).
        private void InitializeComponent()
        {
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.lblIcono = new System.Windows.Forms.Label();
            this.lblTituloPagina = new System.Windows.Forms.Label();
            this.lblSubtituloPagina = new System.Windows.Forms.Label();
            this.lineaCabecera = new System.Windows.Forms.Panel();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pagCarpeta = new System.Windows.Forms.Panel();
            this.lblCarpetaInfo = new System.Windows.Forms.Label();
            this.lblCarpetaIndicacion = new System.Windows.Forms.Label();
            this.txtCarpeta = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.lblEspacio = new System.Windows.Forms.Label();
            this.pagBase = new System.Windows.Forms.Panel();
            this.lblInstancia = new System.Windows.Forms.Label();
            this.cmbInstancia = new System.Windows.Forms.ComboBox();
            this.btnDetectar = new System.Windows.Forms.Button();
            this.lblNombreBD = new System.Windows.Forms.Label();
            this.btnProbar = new System.Windows.Forms.Button();
            this.lblPrueba = new System.Windows.Forms.Label();
            this.pagDatos = new System.Windows.Forms.Panel();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.grpAdministrador = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.pagTareas = new System.Windows.Forms.Panel();
            this.lblTareas = new System.Windows.Forms.Label();
            this.lblAccesos = new System.Windows.Forms.Label();
            this.chkAccesoDirecto = new System.Windows.Forms.CheckBox();
            this.pagListo = new System.Windows.Forms.Panel();
            this.lblListo = new System.Windows.Forms.Label();
            this.txtResumen = new System.Windows.Forms.TextBox();
            this.pagProgreso = new System.Windows.Forms.Panel();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.txtProgreso = new System.Windows.Forms.TextBox();
            this.chkEjecutar = new System.Windows.Forms.CheckBox();
            this.lineaPie = new System.Windows.Forms.Panel();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.btnAtras = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlCabecera.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pagCarpeta.SuspendLayout();
            this.pagBase.SuspendLayout();
            this.pagDatos.SuspendLayout();
            this.grpAdministrador.SuspendLayout();
            this.pagTareas.SuspendLayout();
            this.pagListo.SuspendLayout();
            this.pagProgreso.SuspendLayout();
            this.pnlPie.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCabecera
            // 
            this.pnlCabecera.BackColor = System.Drawing.Color.White;
            this.pnlCabecera.Controls.Add(this.lblIcono);
            this.pnlCabecera.Controls.Add(this.lblTituloPagina);
            this.pnlCabecera.Controls.Add(this.lblSubtituloPagina);
            this.pnlCabecera.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Size = new System.Drawing.Size(640, 64);
            this.pnlCabecera.TabIndex = 0;
            // 
            // lblIcono
            // 
            this.lblIcono.Font = new System.Drawing.Font("Segoe UI Symbol", 26F);
            this.lblIcono.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblIcono.Location = new System.Drawing.Point(562, 6);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Size = new System.Drawing.Size(60, 52);
            this.lblIcono.TabIndex = 2;
            this.lblIcono.Text = "✈";
            this.lblIcono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloPagina
            // 
            this.lblTituloPagina.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloPagina.Location = new System.Drawing.Point(20, 12);
            this.lblTituloPagina.Name = "lblTituloPagina";
            this.lblTituloPagina.Size = new System.Drawing.Size(530, 18);
            this.lblTituloPagina.TabIndex = 0;
            this.lblTituloPagina.Text = "Título";
            // 
            // lblSubtituloPagina
            // 
            this.lblSubtituloPagina.Location = new System.Drawing.Point(34, 32);
            this.lblSubtituloPagina.Name = "lblSubtituloPagina";
            this.lblSubtituloPagina.Size = new System.Drawing.Size(516, 30);
            this.lblSubtituloPagina.TabIndex = 1;
            this.lblSubtituloPagina.Text = "Subtítulo";
            // 
            // lineaCabecera
            // 
            this.lineaCabecera.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lineaCabecera.Location = new System.Drawing.Point(0, 64);
            this.lineaCabecera.Name = "lineaCabecera";
            this.lineaCabecera.Size = new System.Drawing.Size(640, 1);
            this.lineaCabecera.TabIndex = 1;
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.White;
            this.pnlContenido.Controls.Add(this.pagCarpeta);
            this.pnlContenido.Controls.Add(this.pagBase);
            this.pnlContenido.Controls.Add(this.pagDatos);
            this.pnlContenido.Controls.Add(this.pagTareas);
            this.pnlContenido.Controls.Add(this.pagListo);
            this.pnlContenido.Controls.Add(this.pagProgreso);
            this.pnlContenido.Location = new System.Drawing.Point(0, 65);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(640, 379);
            this.pnlContenido.TabIndex = 2;
            // 
            // pagCarpeta
            // 
            this.pagCarpeta.Controls.Add(this.lblCarpetaInfo);
            this.pagCarpeta.Controls.Add(this.lblCarpetaIndicacion);
            this.pagCarpeta.Controls.Add(this.txtCarpeta);
            this.pagCarpeta.Controls.Add(this.btnExaminar);
            this.pagCarpeta.Controls.Add(this.lblEspacio);
            this.pagCarpeta.Location = new System.Drawing.Point(0, 0);
            this.pagCarpeta.Name = "pagCarpeta";
            this.pagCarpeta.Size = new System.Drawing.Size(640, 379);
            this.pagCarpeta.TabIndex = 0;
            // 
            // lblCarpetaInfo
            // 
            this.lblCarpetaInfo.Location = new System.Drawing.Point(30, 24);
            this.lblCarpetaInfo.Name = "lblCarpetaInfo";
            this.lblCarpetaInfo.Size = new System.Drawing.Size(580, 20);
            this.lblCarpetaInfo.TabIndex = 0;
            this.lblCarpetaInfo.Text = "El programa instalará FLY SAFE en la siguiente carpeta.";
            // 
            // lblCarpetaIndicacion
            // 
            this.lblCarpetaIndicacion.Location = new System.Drawing.Point(30, 64);
            this.lblCarpetaIndicacion.Name = "lblCarpetaIndicacion";
            this.lblCarpetaIndicacion.Size = new System.Drawing.Size(580, 36);
            this.lblCarpetaIndicacion.TabIndex = 1;
            this.lblCarpetaIndicacion.Text = "Para continuar, haga clic en Siguiente. Si desea seleccionar una carpeta diferente, haga clic en Examinar.";
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.Location = new System.Drawing.Point(30, 108);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.Size = new System.Drawing.Size(450, 23);
            this.txtCarpeta.TabIndex = 2;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(490, 106);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(90, 27);
            this.btnExaminar.TabIndex = 3;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // lblEspacio
            // 
            this.lblEspacio.Location = new System.Drawing.Point(30, 340);
            this.lblEspacio.Name = "lblEspacio";
            this.lblEspacio.Size = new System.Drawing.Size(580, 20);
            this.lblEspacio.TabIndex = 4;
            // 
            // pagBase
            // 
            this.pagBase.Controls.Add(this.lblInstancia);
            this.pagBase.Controls.Add(this.cmbInstancia);
            this.pagBase.Controls.Add(this.btnDetectar);
            this.pagBase.Controls.Add(this.lblNombreBD);
            this.pagBase.Controls.Add(this.btnProbar);
            this.pagBase.Controls.Add(this.lblPrueba);
            this.pagBase.Location = new System.Drawing.Point(0, 0);
            this.pagBase.Name = "pagBase";
            this.pagBase.Size = new System.Drawing.Size(640, 379);
            this.pagBase.TabIndex = 1;
            this.pagBase.Visible = false;
            // 
            // lblInstancia
            // 
            this.lblInstancia.Location = new System.Drawing.Point(30, 24);
            this.lblInstancia.Name = "lblInstancia";
            this.lblInstancia.Size = new System.Drawing.Size(500, 18);
            this.lblInstancia.TabIndex = 0;
            this.lblInstancia.Text = "Servidor / instancia de SQL Server:";
            // 
            // cmbInstancia
            // 
            this.cmbInstancia.Location = new System.Drawing.Point(30, 46);
            this.cmbInstancia.Name = "cmbInstancia";
            this.cmbInstancia.Size = new System.Drawing.Size(430, 23);
            this.cmbInstancia.TabIndex = 1;
            // 
            // btnDetectar
            // 
            this.btnDetectar.Location = new System.Drawing.Point(470, 44);
            this.btnDetectar.Name = "btnDetectar";
            this.btnDetectar.Size = new System.Drawing.Size(90, 27);
            this.btnDetectar.TabIndex = 2;
            this.btnDetectar.Text = "Detectar";
            this.btnDetectar.UseVisualStyleBackColor = true;
            this.btnDetectar.Click += new System.EventHandler(this.btnDetectar_Click);
            // 
            // lblNombreBD
            // 
            this.lblNombreBD.Location = new System.Drawing.Point(30, 90);
            this.lblNombreBD.Name = "lblNombreBD";
            this.lblNombreBD.Size = new System.Drawing.Size(530, 18);
            this.lblNombreBD.TabIndex = 3;
            this.lblNombreBD.Text = "Nombre de la base de datos: Gestion Usuario (autenticación de Windows)";
            // 
            // btnProbar
            // 
            this.btnProbar.Location = new System.Drawing.Point(30, 125);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(130, 27);
            this.btnProbar.TabIndex = 4;
            this.btnProbar.Text = "Probar conexión";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // lblPrueba
            // 
            this.lblPrueba.Location = new System.Drawing.Point(30, 162);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(580, 54);
            this.lblPrueba.TabIndex = 5;
            // 
            // pagDatos
            // 
            this.pagDatos.Controls.Add(this.txtBackup);
            this.pagDatos.Controls.Add(this.btnBackup);
            this.pagDatos.Controls.Add(this.grpAdministrador);
            this.pagDatos.Location = new System.Drawing.Point(0, 0);
            this.pagDatos.Name = "pagDatos";
            this.pagDatos.Size = new System.Drawing.Size(640, 379);
            this.pagDatos.TabIndex = 2;
            this.pagDatos.Visible = false;
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(50, 0);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.ReadOnly = true;
            this.txtBackup.Size = new System.Drawing.Size(450, 23);
            this.txtBackup.TabIndex = 0;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(510, 0);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(90, 25);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Elegir...";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // grpAdministrador
            // 
            this.grpAdministrador.Controls.Add(this.lblUsuario);
            this.grpAdministrador.Controls.Add(this.txtUsuario);
            this.grpAdministrador.Controls.Add(this.lblContrasena);
            this.grpAdministrador.Controls.Add(this.txtContrasena);
            this.grpAdministrador.Location = new System.Drawing.Point(30, 300);
            this.grpAdministrador.Name = "grpAdministrador";
            this.grpAdministrador.Size = new System.Drawing.Size(580, 68);
            this.grpAdministrador.TabIndex = 2;
            this.grpAdministrador.TabStop = false;
            this.grpAdministrador.Text = "Autorización de un administrador del sistema (se borran datos)";
            this.grpAdministrador.Visible = false;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Location = new System.Drawing.Point(15, 32);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(60, 20);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(80, 29);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(170, 23);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblContrasena
            // 
            this.lblContrasena.Location = new System.Drawing.Point(285, 32);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(80, 20);
            this.lblContrasena.TabIndex = 2;
            this.lblContrasena.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(370, 29);
            this.txtContrasena.MaxLength = 50;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(180, 23);
            this.txtContrasena.TabIndex = 3;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // pagTareas
            // 
            this.pagTareas.Controls.Add(this.lblTareas);
            this.pagTareas.Controls.Add(this.lblAccesos);
            this.pagTareas.Controls.Add(this.chkAccesoDirecto);
            this.pagTareas.Location = new System.Drawing.Point(0, 0);
            this.pagTareas.Name = "pagTareas";
            this.pagTareas.Size = new System.Drawing.Size(640, 379);
            this.pagTareas.TabIndex = 3;
            this.pagTareas.Visible = false;
            // 
            // lblTareas
            // 
            this.lblTareas.Location = new System.Drawing.Point(30, 24);
            this.lblTareas.Name = "lblTareas";
            this.lblTareas.Size = new System.Drawing.Size(580, 36);
            this.lblTareas.TabIndex = 0;
            this.lblTareas.Text = "Seleccione las tareas adicionales que desea que se realicen durante la instalación de FLY SAFE y haga clic en Siguiente.";
            // 
            // lblAccesos
            // 
            this.lblAccesos.Location = new System.Drawing.Point(30, 78);
            this.lblAccesos.Name = "lblAccesos";
            this.lblAccesos.Size = new System.Drawing.Size(580, 18);
            this.lblAccesos.TabIndex = 1;
            this.lblAccesos.Text = "Accesos directos adicionales:";
            // 
            // chkAccesoDirecto
            // 
            this.chkAccesoDirecto.AutoSize = true;
            this.chkAccesoDirecto.Checked = true;
            this.chkAccesoDirecto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAccesoDirecto.Location = new System.Drawing.Point(45, 102);
            this.chkAccesoDirecto.Name = "chkAccesoDirecto";
            this.chkAccesoDirecto.Size = new System.Drawing.Size(250, 19);
            this.chkAccesoDirecto.TabIndex = 2;
            this.chkAccesoDirecto.Text = "Crear un acceso directo en el escritorio";
            this.chkAccesoDirecto.UseVisualStyleBackColor = true;
            // 
            // pagListo
            // 
            this.pagListo.Controls.Add(this.lblListo);
            this.pagListo.Controls.Add(this.txtResumen);
            this.pagListo.Location = new System.Drawing.Point(0, 0);
            this.pagListo.Name = "pagListo";
            this.pagListo.Size = new System.Drawing.Size(640, 379);
            this.pagListo.TabIndex = 4;
            this.pagListo.Visible = false;
            // 
            // lblListo
            // 
            this.lblListo.Location = new System.Drawing.Point(30, 16);
            this.lblListo.Name = "lblListo";
            this.lblListo.Size = new System.Drawing.Size(580, 36);
            this.lblListo.TabIndex = 0;
            this.lblListo.Text = "Haga clic en Instalar para continuar, o en Atrás si desea revisar o cambiar la configuración.";
            // 
            // txtResumen
            // 
            this.txtResumen.BackColor = System.Drawing.SystemColors.Control;
            this.txtResumen.Location = new System.Drawing.Point(30, 62);
            this.txtResumen.Multiline = true;
            this.txtResumen.Name = "txtResumen";
            this.txtResumen.ReadOnly = true;
            this.txtResumen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResumen.Size = new System.Drawing.Size(580, 300);
            this.txtResumen.TabIndex = 1;
            // 
            // pagProgreso
            // 
            this.pagProgreso.Controls.Add(this.lblProgreso);
            this.pagProgreso.Controls.Add(this.pbProgreso);
            this.pagProgreso.Controls.Add(this.txtProgreso);
            this.pagProgreso.Controls.Add(this.chkEjecutar);
            this.pagProgreso.Location = new System.Drawing.Point(0, 0);
            this.pagProgreso.Name = "pagProgreso";
            this.pagProgreso.Size = new System.Drawing.Size(640, 379);
            this.pagProgreso.TabIndex = 5;
            this.pagProgreso.Visible = false;
            // 
            // lblProgreso
            // 
            this.lblProgreso.Location = new System.Drawing.Point(30, 16);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(580, 20);
            this.lblProgreso.TabIndex = 0;
            this.lblProgreso.Text = "Por favor espere mientras se instala FLY SAFE en su sistema.";
            // 
            // pbProgreso
            // 
            this.pbProgreso.Location = new System.Drawing.Point(30, 44);
            this.pbProgreso.MarqueeAnimationSpeed = 30;
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(580, 18);
            this.pbProgreso.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbProgreso.TabIndex = 1;
            // 
            // txtProgreso
            // 
            this.txtProgreso.Location = new System.Drawing.Point(30, 76);
            this.txtProgreso.Multiline = true;
            this.txtProgreso.Name = "txtProgreso";
            this.txtProgreso.ReadOnly = true;
            this.txtProgreso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProgreso.Size = new System.Drawing.Size(580, 254);
            this.txtProgreso.TabIndex = 2;
            // 
            // chkEjecutar
            // 
            this.chkEjecutar.AutoSize = true;
            this.chkEjecutar.Checked = true;
            this.chkEjecutar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEjecutar.Location = new System.Drawing.Point(30, 342);
            this.chkEjecutar.Name = "chkEjecutar";
            this.chkEjecutar.Size = new System.Drawing.Size(250, 19);
            this.chkEjecutar.TabIndex = 3;
            this.chkEjecutar.Text = "Ejecutar FLY SAFE al finalizar";
            this.chkEjecutar.UseVisualStyleBackColor = true;
            this.chkEjecutar.Visible = false;
            // 
            // lineaPie
            // 
            this.lineaPie.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lineaPie.Location = new System.Drawing.Point(0, 444);
            this.lineaPie.Name = "lineaPie";
            this.lineaPie.Size = new System.Drawing.Size(640, 1);
            this.lineaPie.TabIndex = 3;
            // 
            // pnlPie
            // 
            this.pnlPie.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPie.Controls.Add(this.btnAtras);
            this.pnlPie.Controls.Add(this.btnSiguiente);
            this.pnlPie.Controls.Add(this.btnCancelar);
            this.pnlPie.Location = new System.Drawing.Point(0, 445);
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Size = new System.Drawing.Size(640, 55);
            this.pnlPie.TabIndex = 4;
            // 
            // btnAtras
            // 
            this.btnAtras.Location = new System.Drawing.Point(360, 14);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(80, 27);
            this.btnAtras.TabIndex = 0;
            this.btnAtras.Text = "< Atrás";
            this.btnAtras.UseVisualStyleBackColor = true;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(450, 14);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(80, 27);
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = "Siguiente >";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(540, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 27);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FRMInstalador_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 500);
            this.Controls.Add(this.pnlCabecera);
            this.Controls.Add(this.lineaCabecera);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.lineaPie);
            this.Controls.Add(this.pnlPie);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FRMInstalador_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalar - FLY SAFE";
            this.Load += new System.EventHandler(this.FRMInstalador_GV42_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMInstalador_GV42_FormClosing);
            this.pnlCabecera.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pagCarpeta.ResumeLayout(false);
            this.pagCarpeta.PerformLayout();
            this.pagBase.ResumeLayout(false);
            this.pagDatos.ResumeLayout(false);
            this.pagDatos.PerformLayout();
            this.grpAdministrador.ResumeLayout(false);
            this.grpAdministrador.PerformLayout();
            this.pagTareas.ResumeLayout(false);
            this.pagTareas.PerformLayout();
            this.pagListo.ResumeLayout(false);
            this.pagListo.PerformLayout();
            this.pagProgreso.ResumeLayout(false);
            this.pagProgreso.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.Label lblIcono;
        private System.Windows.Forms.Label lblTituloPagina;
        private System.Windows.Forms.Label lblSubtituloPagina;
        private System.Windows.Forms.Panel lineaCabecera;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pagCarpeta;
        private System.Windows.Forms.Label lblCarpetaInfo;
        private System.Windows.Forms.Label lblCarpetaIndicacion;
        private System.Windows.Forms.TextBox txtCarpeta;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label lblEspacio;
        private System.Windows.Forms.Panel pagBase;
        private System.Windows.Forms.Label lblInstancia;
        private System.Windows.Forms.ComboBox cmbInstancia;
        private System.Windows.Forms.Button btnDetectar;
        private System.Windows.Forms.Label lblNombreBD;
        private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.Label lblPrueba;
        private System.Windows.Forms.Panel pagDatos;
        private System.Windows.Forms.TextBox txtBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.GroupBox grpAdministrador;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Panel pagTareas;
        private System.Windows.Forms.Label lblTareas;
        private System.Windows.Forms.Label lblAccesos;
        private System.Windows.Forms.CheckBox chkAccesoDirecto;
        private System.Windows.Forms.Panel pagListo;
        private System.Windows.Forms.Label lblListo;
        private System.Windows.Forms.TextBox txtResumen;
        private System.Windows.Forms.Panel pagProgreso;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.ProgressBar pbProgreso;
        private System.Windows.Forms.TextBox txtProgreso;
        private System.Windows.Forms.CheckBox chkEjecutar;
        private System.Windows.Forms.Panel lineaPie;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnCancelar;
    }
}
