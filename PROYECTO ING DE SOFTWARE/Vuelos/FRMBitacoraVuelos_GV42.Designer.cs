namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMBitacoraVuelos_GV42
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTarjetaGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.dgvCambios = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colCodigoVuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAerolinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuerta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostoKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSeccionLista = new System.Windows.Forms.Label();
            this.pnlSeparadorArriba = new System.Windows.Forms.Panel();
            this.pnlFiltros = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblFechaIni = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.cmbCodigo = new System.Windows.Forms.ComboBox();
            this.cmbNombre = new System.Windows.Forms.ComboBox();
            this.dtIni = new System.Windows.Forms.DateTimePicker();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.chkSoloCambios = new System.Windows.Forms.CheckBox();
            this.flpBotonesFiltro = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAplicar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnLimpiar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlSeparadorAbajo = new System.Windows.Forms.Panel();
            this.pnlBotonera = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalir = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnActivar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlTarjetaGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.tlpFiltros.SuspendLayout();
            this.flpBotonesFiltro.SuspendLayout();
            this.pnlBotonera.SuspendLayout();
            this.flpAcciones.SuspendLayout();
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
            this.pnlEncabezado.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(360, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bitácora de cambios de vuelos";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(330, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Historial de versiones de cada vuelo (Vuelo_C)";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlTarjetaGrilla);
            this.pnlContenido.Controls.Add(this.pnlSeparadorArriba);
            this.pnlContenido.Controls.Add(this.pnlFiltros);
            this.pnlContenido.Controls.Add(this.pnlSeparadorAbajo);
            this.pnlContenido.Controls.Add(this.pnlBotonera);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 1;
            //
            // pnlTarjetaGrilla
            //
            this.pnlTarjetaGrilla.Controls.Add(this.dgvCambios);
            this.pnlTarjetaGrilla.Controls.Add(this.lblSeccionLista);
            this.pnlTarjetaGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaGrilla.Location = new System.Drawing.Point(24, 136);
            this.pnlTarjetaGrilla.Name = "pnlTarjetaGrilla";
            this.pnlTarjetaGrilla.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.pnlTarjetaGrilla.Size = new System.Drawing.Size(952, 328);
            this.pnlTarjetaGrilla.TabIndex = 1;
            //
            // dgvCambios
            //
            this.dgvCambios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigoVuelo,
            this.colFecha,
            this.colHora,
            this.colNombre,
            this.colAerolinea,
            this.colSalida,
            this.colLlegada,
            this.colPuerta,
            this.colCostoKilo,
            this.colBaja,
            this.colAct});
            this.dgvCambios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCambios.Location = new System.Drawing.Point(16, 44);
            this.dgvCambios.Name = "dgvCambios";
            this.dgvCambios.Size = new System.Drawing.Size(920, 268);
            this.dgvCambios.TabIndex = 1;
            this.dgvCambios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCambios_CellFormatting);
            //
            // colCodigoVuelo
            //
            this.colCodigoVuelo.DataPropertyName = "CodigoVuelo";
            this.colCodigoVuelo.FillWeight = 62F;
            this.colCodigoVuelo.HeaderText = "Cod. vuelo";
            this.colCodigoVuelo.Name = "colCodigoVuelo";
            this.colCodigoVuelo.ReadOnly = true;
            //
            // colFecha
            //
            this.colFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.colFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.colFecha.FillWeight = 68F;
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            //
            // colHora
            //
            this.colHora.DataPropertyName = "Hora";
            this.colHora.FillWeight = 42F;
            this.colHora.HeaderText = "Hora";
            this.colHora.Name = "colHora";
            this.colHora.ReadOnly = true;
            //
            // colNombre
            //
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.FillWeight = 72F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colAerolinea
            //
            this.colAerolinea.DataPropertyName = "Aerolinea";
            this.colAerolinea.FillWeight = 110F;
            this.colAerolinea.HeaderText = "Aerolínea";
            this.colAerolinea.Name = "colAerolinea";
            this.colAerolinea.ReadOnly = true;
            //
            // colSalida
            //
            this.colSalida.DataPropertyName = "Salida";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            this.colSalida.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSalida.FillWeight = 98F;
            this.colSalida.HeaderText = "Salida";
            this.colSalida.Name = "colSalida";
            this.colSalida.ReadOnly = true;
            //
            // colLlegada
            //
            this.colLlegada.DataPropertyName = "Llegada";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm";
            this.colLlegada.DefaultCellStyle = dataGridViewCellStyle3;
            this.colLlegada.FillWeight = 98F;
            this.colLlegada.HeaderText = "Llegada";
            this.colLlegada.Name = "colLlegada";
            this.colLlegada.ReadOnly = true;
            //
            // colPuerta
            //
            this.colPuerta.DataPropertyName = "Puerta";
            this.colPuerta.FillWeight = 48F;
            this.colPuerta.HeaderText = "Puerta";
            this.colPuerta.Name = "colPuerta";
            this.colPuerta.ReadOnly = true;
            //
            // colCostoKilo
            //
            this.colCostoKilo.DataPropertyName = "CostoKilo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.colCostoKilo.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCostoKilo.FillWeight = 70F;
            this.colCostoKilo.HeaderText = "$/kg exceso";
            this.colCostoKilo.Name = "colCostoKilo";
            this.colCostoKilo.ReadOnly = true;
            //
            // colBaja
            //
            this.colBaja.DataPropertyName = "Baja";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBaja.DefaultCellStyle = dataGridViewCellStyle5;
            this.colBaja.FillWeight = 40F;
            this.colBaja.HeaderText = "Baja";
            this.colBaja.Name = "colBaja";
            this.colBaja.ReadOnly = true;
            //
            // colAct
            //
            this.colAct.DataPropertyName = "Act";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAct.DefaultCellStyle = dataGridViewCellStyle6;
            this.colAct.FillWeight = 36F;
            this.colAct.HeaderText = "Act.";
            this.colAct.Name = "colAct";
            this.colAct.ReadOnly = true;
            //
            // lblSeccionLista
            //
            this.lblSeccionLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionLista.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionLista.Location = new System.Drawing.Point(16, 12);
            this.lblSeccionLista.Name = "lblSeccionLista";
            this.lblSeccionLista.Size = new System.Drawing.Size(920, 32);
            this.lblSeccionLista.TabIndex = 0;
            this.lblSeccionLista.Text = "Historial de cambios";
            //
            // pnlSeparadorArriba
            //
            this.pnlSeparadorArriba.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparadorArriba.Location = new System.Drawing.Point(24, 120);
            this.pnlSeparadorArriba.Name = "pnlSeparadorArriba";
            this.pnlSeparadorArriba.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparadorArriba.TabIndex = 3;
            //
            // pnlFiltros
            //
            this.pnlFiltros.AcentoSuperior = true;
            this.pnlFiltros.Controls.Add(this.tlpFiltros);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(24, 20);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(20, 16, 20, 14);
            this.pnlFiltros.Size = new System.Drawing.Size(952, 126);
            this.pnlFiltros.TabIndex = 0;
            //
            // tlpFiltros
            //
            this.tlpFiltros.ColumnCount = 5;
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tlpFiltros.Controls.Add(this.lblCodigo, 0, 0);
            this.tlpFiltros.Controls.Add(this.lblNombre, 1, 0);
            this.tlpFiltros.Controls.Add(this.lblFechaIni, 2, 0);
            this.tlpFiltros.Controls.Add(this.lblFechaFin, 3, 0);
            this.tlpFiltros.Controls.Add(this.cmbCodigo, 0, 1);
            this.tlpFiltros.Controls.Add(this.cmbNombre, 1, 1);
            this.tlpFiltros.Controls.Add(this.dtIni, 2, 1);
            this.tlpFiltros.Controls.Add(this.dtFin, 3, 1);
            this.tlpFiltros.Controls.Add(this.flpBotonesFiltro, 4, 1);
            this.tlpFiltros.Controls.Add(this.chkSoloCambios, 0, 2);
            this.tlpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFiltros.Location = new System.Drawing.Point(20, 16);
            this.tlpFiltros.Name = "tlpFiltros";
            this.tlpFiltros.RowCount = 3;
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFiltros.Size = new System.Drawing.Size(912, 96);
            this.tlpFiltros.TabIndex = 0;
            //
            // lblCodigo
            //
            this.lblCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCodigo.Location = new System.Drawing.Point(0, 3);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(71, 17);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Cod. vuelo";
            //
            // lblNombre
            //
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblNombre.Location = new System.Drawing.Point(143, 3);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(97, 17);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre (ruta)";
            //
            // lblFechaIni
            //
            this.lblFechaIni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFechaIni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblFechaIni.Location = new System.Drawing.Point(325, 3);
            this.lblFechaIni.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblFechaIni.Name = "lblFechaIni";
            this.lblFechaIni.Size = new System.Drawing.Size(65, 17);
            this.lblFechaIni.TabIndex = 2;
            this.lblFechaIni.Text = "Fecha ini.";
            //
            // lblFechaFin
            //
            this.lblFechaFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblFechaFin.Location = new System.Drawing.Point(488, 3);
            this.lblFechaFin.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(63, 17);
            this.lblFechaFin.TabIndex = 3;
            this.lblFechaFin.Text = "Fecha fin";
            //
            // cmbCodigo
            //
            this.cmbCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCodigo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCodigo.FormattingEnabled = true;
            this.cmbCodigo.Location = new System.Drawing.Point(0, 33);
            this.cmbCodigo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
            this.cmbCodigo.Name = "cmbCodigo";
            this.cmbCodigo.Size = new System.Drawing.Size(127, 25);
            this.cmbCodigo.TabIndex = 4;
            //
            // cmbNombre
            //
            this.cmbNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNombre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNombre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNombre.FormattingEnabled = true;
            this.cmbNombre.Location = new System.Drawing.Point(143, 33);
            this.cmbNombre.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
            this.cmbNombre.Name = "cmbNombre";
            this.cmbNombre.Size = new System.Drawing.Size(166, 25);
            this.cmbNombre.TabIndex = 5;
            //
            // dtIni
            //
            this.dtIni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtIni.Checked = false;
            this.dtIni.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtIni.Location = new System.Drawing.Point(325, 33);
            this.dtIni.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
            this.dtIni.Name = "dtIni";
            this.dtIni.ShowCheckBox = true;
            this.dtIni.Size = new System.Drawing.Size(147, 25);
            this.dtIni.TabIndex = 6;
            //
            // dtFin
            //
            this.dtFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFin.Checked = false;
            this.dtFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(488, 33);
            this.dtFin.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
            this.dtFin.Name = "dtFin";
            this.dtFin.ShowCheckBox = true;
            this.dtFin.Size = new System.Drawing.Size(148, 25);
            this.dtFin.TabIndex = 7;
            //
            // chkSoloCambios
            //
            this.chkSoloCambios.AutoSize = true;
            this.tlpFiltros.SetColumnSpan(this.chkSoloCambios, 3);
            this.chkSoloCambios.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkSoloCambios.Location = new System.Drawing.Point(0, 74);
            this.chkSoloCambios.Margin = new System.Windows.Forms.Padding(0, 4, 3, 0);
            this.chkSoloCambios.Name = "chkSoloCambios";
            this.chkSoloCambios.Size = new System.Drawing.Size(230, 21);
            this.chkSoloCambios.TabIndex = 9;
            this.chkSoloCambios.Text = "Solo vuelos con cambios";
            this.chkSoloCambios.UseVisualStyleBackColor = true;
            //
            // flpBotonesFiltro
            //
            this.flpBotonesFiltro.Controls.Add(this.btnAplicar);
            this.flpBotonesFiltro.Controls.Add(this.btnLimpiar);
            this.flpBotonesFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotonesFiltro.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotonesFiltro.Location = new System.Drawing.Point(652, 22);
            this.flpBotonesFiltro.Margin = new System.Windows.Forms.Padding(0);
            this.flpBotonesFiltro.Name = "flpBotonesFiltro";
            this.flpBotonesFiltro.Size = new System.Drawing.Size(270, 48);
            this.flpBotonesFiltro.TabIndex = 8;
            this.flpBotonesFiltro.WrapContents = false;
            //
            // btnAplicar
            //
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.Location = new System.Drawing.Point(140, 3);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(120, 38);
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
            this.btnLimpiar.Location = new System.Drawing.Point(10, 3);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 38);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // pnlSeparadorAbajo
            //
            this.pnlSeparadorAbajo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparadorAbajo.Location = new System.Drawing.Point(24, 464);
            this.pnlSeparadorAbajo.Name = "pnlSeparadorAbajo";
            this.pnlSeparadorAbajo.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparadorAbajo.TabIndex = 4;
            //
            // pnlBotonera
            //
            this.pnlBotonera.Controls.Add(this.lblAyuda);
            this.pnlBotonera.Controls.Add(this.flpAcciones);
            this.pnlBotonera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotonera.Location = new System.Drawing.Point(24, 480);
            this.pnlBotonera.Name = "pnlBotonera";
            this.pnlBotonera.Padding = new System.Windows.Forms.Padding(20, 12, 20, 14);
            this.pnlBotonera.Size = new System.Drawing.Size(952, 70);
            this.pnlBotonera.TabIndex = 2;
            //
            // lblAyuda
            //
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(20, 12);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(632, 44);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "El registro resaltado (Act. = 1) es la versión vigente del vuelo. Activar vuelve el vuelo a la versión seleccionada.";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flpAcciones
            //
            this.flpAcciones.Controls.Add(this.btnSalir);
            this.flpAcciones.Controls.Add(this.btnActivar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpAcciones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpAcciones.Location = new System.Drawing.Point(652, 12);
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.Size = new System.Drawing.Size(280, 44);
            this.flpAcciones.TabIndex = 0;
            this.flpAcciones.WrapContents = false;
            //
            // btnSalir
            //
            this.btnSalir.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Location = new System.Drawing.Point(150, 2);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(130, 40);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // btnActivar
            //
            this.btnActivar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Advertencia;
            this.btnActivar.FlatAppearance.BorderSize = 0;
            this.btnActivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnActivar.Location = new System.Drawing.Point(10, 2);
            this.btnActivar.Margin = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(130, 40);
            this.btnActivar.TabIndex = 0;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = false;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            //
            // FRMBitacoraVuelos_GV42
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMBitacoraVuelos_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitácora de cambios de vuelos";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlTarjetaGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.tlpFiltros.ResumeLayout(false);
            this.tlpFiltros.PerformLayout();
            this.flpBotonesFiltro.ResumeLayout(false);
            this.pnlBotonera.ResumeLayout(false);
            this.flpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjetaGrilla;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvCambios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigoVuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAerolinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLlegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuerta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostoKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAct;
        private System.Windows.Forms.Label lblSeccionLista;
        private System.Windows.Forms.Panel pnlSeparadorArriba;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlFiltros;
        private System.Windows.Forms.TableLayoutPanel tlpFiltros;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblFechaIni;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.ComboBox cmbCodigo;
        private System.Windows.Forms.ComboBox cmbNombre;
        private System.Windows.Forms.DateTimePicker dtIni;
        private System.Windows.Forms.DateTimePicker dtFin;
        private System.Windows.Forms.CheckBox chkSoloCambios;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesFiltro;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAplicar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnLimpiar;
        private System.Windows.Forms.Panel pnlSeparadorAbajo;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlBotonera;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnSalir;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnActivar;
    }
}
