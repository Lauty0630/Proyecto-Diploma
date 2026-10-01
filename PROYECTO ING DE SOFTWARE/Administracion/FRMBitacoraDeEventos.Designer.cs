namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMBitacoraDeEventos
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
            this.pnlFiltros = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblCriticidad = new System.Windows.Forms.Label();
            this.txtLogin = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.cboEvento = new System.Windows.Forms.ComboBox();
            this.cboCriticidad = new System.Windows.Forms.ComboBox();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.flpAccionesFiltro = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAplicar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnLimpiar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlSeparador1 = new System.Windows.Forms.Panel();
            this.pnlGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.dgvBitacora = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.pnlSeparador2 = new System.Windows.Forms.Panel();
            this.pnlInferior = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellidoUsuario = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.btnImprimir = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.tlpFiltros.SuspendLayout();
            this.flpAccionesFiltro.SuspendLayout();
            this.pnlGrilla.SuspendLayout();
            this.pnlInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 80);
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
            this.lblTitulo.Size = new System.Drawing.Size(240, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bitácora de Eventos";
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
            this.lblSubtitulo.Text = "Consultá y filtrá los eventos registrados en el sistema";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.pnlGrilla);
            this.pnlContenido.Controls.Add(this.pnlSeparador2);
            this.pnlContenido.Controls.Add(this.pnlInferior);
            this.pnlContenido.Controls.Add(this.pnlSeparador1);
            this.pnlContenido.Controls.Add(this.pnlFiltros);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.AcentoSuperior = true;
            this.pnlFiltros.Controls.Add(this.tlpFiltros);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(24, 20);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(16, 14, 16, 12);
            this.pnlFiltros.Size = new System.Drawing.Size(952, 154);
            this.pnlFiltros.TabIndex = 0;
            // 
            // tlpFiltros
            // 
            this.tlpFiltros.BackColor = System.Drawing.Color.White;
            this.tlpFiltros.ColumnCount = 4;
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.Controls.Add(this.lblLogin, 0, 0);
            this.tlpFiltros.Controls.Add(this.lblModulo, 1, 0);
            this.tlpFiltros.Controls.Add(this.lblEvento, 2, 0);
            this.tlpFiltros.Controls.Add(this.lblCriticidad, 3, 0);
            this.tlpFiltros.Controls.Add(this.txtLogin, 0, 1);
            this.tlpFiltros.Controls.Add(this.cboModulo, 1, 1);
            this.tlpFiltros.Controls.Add(this.cboEvento, 2, 1);
            this.tlpFiltros.Controls.Add(this.cboCriticidad, 3, 1);
            this.tlpFiltros.Controls.Add(this.lblFechaInicio, 0, 2);
            this.tlpFiltros.Controls.Add(this.lblFechaFin, 1, 2);
            this.tlpFiltros.Controls.Add(this.dtpFechaInicio, 0, 3);
            this.tlpFiltros.Controls.Add(this.dtpFechaFin, 1, 3);
            this.tlpFiltros.Controls.Add(this.flpAccionesFiltro, 2, 2);
            this.tlpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFiltros.Location = new System.Drawing.Point(16, 14);
            this.tlpFiltros.Name = "tlpFiltros";
            this.tlpFiltros.RowCount = 4;
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpFiltros.SetColumnSpan(this.flpAccionesFiltro, 2);
            this.tlpFiltros.SetRowSpan(this.flpAccionesFiltro, 2);
            this.tlpFiltros.Size = new System.Drawing.Size(920, 128);
            this.tlpFiltros.TabIndex = 0;
            // 
            // lblLogin
            // 
            this.lblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblLogin.Location = new System.Drawing.Point(3, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(224, 24);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Login";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblModulo
            // 
            this.lblModulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModulo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblModulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblModulo.Location = new System.Drawing.Point(233, 0);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(224, 24);
            this.lblModulo.TabIndex = 1;
            this.lblModulo.Text = "Módulo";
            this.lblModulo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblEvento
            // 
            this.lblEvento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEvento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblEvento.Location = new System.Drawing.Point(463, 0);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(224, 24);
            this.lblEvento.TabIndex = 2;
            this.lblEvento.Text = "Evento";
            this.lblEvento.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblCriticidad
            // 
            this.lblCriticidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriticidad.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCriticidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCriticidad.Location = new System.Drawing.Point(693, 0);
            this.lblCriticidad.Name = "lblCriticidad";
            this.lblCriticidad.Size = new System.Drawing.Size(224, 24);
            this.lblCriticidad.TabIndex = 3;
            this.lblCriticidad.Text = "Criticidad";
            this.lblCriticidad.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(3, 60);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(224, 24);
            this.lblFechaInicio.TabIndex = 4;
            this.lblFechaInicio.Text = "Fecha inicio";
            this.lblFechaInicio.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblFechaFin.Location = new System.Drawing.Point(233, 60);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(224, 24);
            this.lblFechaFin.TabIndex = 5;
            this.lblFechaFin.Text = "Fecha fin";
            this.lblFechaFin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtLogin.Location = new System.Drawing.Point(3, 27);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(213, 26);
            this.txtLogin.TabIndex = 6;
            // 
            // cboModulo
            // 
            this.cboModulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboModulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboModulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(233, 27);
            this.cboModulo.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(213, 25);
            this.cboModulo.TabIndex = 7;
            // 
            // cboEvento
            // 
            this.cboEvento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEvento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEvento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.cboEvento.FormattingEnabled = true;
            this.cboEvento.Location = new System.Drawing.Point(463, 27);
            this.cboEvento.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboEvento.Name = "cboEvento";
            this.cboEvento.Size = new System.Drawing.Size(213, 25);
            this.cboEvento.TabIndex = 8;
            // 
            // cboCriticidad
            // 
            this.cboCriticidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboCriticidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCriticidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCriticidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCriticidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.cboCriticidad.FormattingEnabled = true;
            this.cboCriticidad.Location = new System.Drawing.Point(693, 27);
            this.cboCriticidad.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.cboCriticidad.Name = "cboCriticidad";
            this.cboCriticidad.Size = new System.Drawing.Size(213, 25);
            this.cboCriticidad.TabIndex = 9;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.dtpFechaInicio.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpFechaInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(3, 87);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(213, 25);
            this.dtpFechaInicio.TabIndex = 10;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.dtpFechaFin.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpFechaFin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(233, 87);
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(213, 25);
            this.dtpFechaFin.TabIndex = 11;
            // 
            // flpAccionesFiltro
            // 
            this.flpAccionesFiltro.Controls.Add(this.btnAplicar);
            this.flpAccionesFiltro.Controls.Add(this.btnLimpiar);
            this.flpAccionesFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAccionesFiltro.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpAccionesFiltro.Location = new System.Drawing.Point(460, 60);
            this.flpAccionesFiltro.Margin = new System.Windows.Forms.Padding(0);
            this.flpAccionesFiltro.Name = "flpAccionesFiltro";
            this.flpAccionesFiltro.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flpAccionesFiltro.Size = new System.Drawing.Size(460, 60);
            this.flpAccionesFiltro.TabIndex = 12;
            this.flpAccionesFiltro.WrapContents = false;
            // 
            // btnAplicar
            // 
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.Location = new System.Drawing.Point(320, 16);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(140, 40);
            this.btnAplicar.TabIndex = 0;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Location = new System.Drawing.Point(170, 16);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 40);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // pnlSeparador1
            // 
            this.pnlSeparador1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparador1.Location = new System.Drawing.Point(24, 174);
            this.pnlSeparador1.Name = "pnlSeparador1";
            this.pnlSeparador1.Size = new System.Drawing.Size(952, 14);
            this.pnlSeparador1.TabIndex = 1;
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvBitacora);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Location = new System.Drawing.Point(24, 188);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(12, 12, 12, 15);
            this.pnlGrilla.Size = new System.Drawing.Size(952, 258);
            this.pnlGrilla.TabIndex = 2;
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.AllowUserToOrderColumns = false;
            this.dgvBitacora.AllowUserToResizeColumns = false;
            this.dgvBitacora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBitacora.Location = new System.Drawing.Point(12, 12);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBitacora.Size = new System.Drawing.Size(928, 231);
            this.dgvBitacora.TabIndex = 0;
            this.dgvBitacora.SelectionChanged += new System.EventHandler(this.dgvBitacora_SelectionChanged);
            // 
            // pnlSeparador2
            // 
            this.pnlSeparador2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparador2.Location = new System.Drawing.Point(24, 446);
            this.pnlSeparador2.Name = "pnlSeparador2";
            this.pnlSeparador2.Size = new System.Drawing.Size(952, 14);
            this.pnlSeparador2.TabIndex = 3;
            // 
            // pnlInferior
            // 
            this.pnlInferior.Controls.Add(this.btnCancelar);
            this.pnlInferior.Controls.Add(this.btnImprimir);
            this.pnlInferior.Controls.Add(this.txtApellidoUsuario);
            this.pnlInferior.Controls.Add(this.lblApellido);
            this.pnlInferior.Controls.Add(this.txtNombreUsuario);
            this.pnlInferior.Controls.Add(this.lblNombre);
            this.pnlInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInferior.Location = new System.Drawing.Point(24, 460);
            this.pnlInferior.Name = "pnlInferior";
            this.pnlInferior.Size = new System.Drawing.Size(952, 90);
            this.pnlInferior.TabIndex = 4;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblNombre.Location = new System.Drawing.Point(20, 14);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(58, 17);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblApellido.Location = new System.Drawing.Point(254, 14);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(57, 17);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.txtNombreUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtNombreUsuario.Location = new System.Drawing.Point(20, 36);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.ReadOnly = true;
            this.txtNombreUsuario.Size = new System.Drawing.Size(214, 26);
            this.txtNombreUsuario.TabIndex = 1;
            // 
            // txtApellidoUsuario
            // 
            this.txtApellidoUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.txtApellidoUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApellidoUsuario.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtApellidoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtApellidoUsuario.Location = new System.Drawing.Point(254, 36);
            this.txtApellidoUsuario.Name = "txtApellidoUsuario";
            this.txtApellidoUsuario.ReadOnly = true;
            this.txtApellidoUsuario.Size = new System.Drawing.Size(214, 26);
            this.txtApellidoUsuario.TabIndex = 3;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.Location = new System.Drawing.Point(646, 24);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(140, 40);
            this.btnImprimir.TabIndex = 4;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(796, 24);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(136, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Salir";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FRMBitacoraDeEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "FRMBitacoraDeEventos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitácora de Eventos";
            this.Load += new System.EventHandler(this.FRMBitacoraDeEventos_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            this.tlpFiltros.ResumeLayout(false);
            this.tlpFiltros.PerformLayout();
            this.flpAccionesFiltro.ResumeLayout(false);
            this.pnlGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.pnlInferior.ResumeLayout(false);
            this.pnlInferior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlFiltros;
        private System.Windows.Forms.TableLayoutPanel tlpFiltros;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblCriticidad;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtLogin;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.ComboBox cboEvento;
        private System.Windows.Forms.ComboBox cboCriticidad;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.FlowLayoutPanel flpAccionesFiltro;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAplicar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnLimpiar;
        private System.Windows.Forms.Panel pnlSeparador1;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlGrilla;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvBitacora;
        private System.Windows.Forms.Panel pnlSeparador2;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlInferior;
        private System.Windows.Forms.Label lblNombre;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtNombreUsuario;
        private System.Windows.Forms.Label lblApellido;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtApellidoUsuario;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnImprimir;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
    }
}
