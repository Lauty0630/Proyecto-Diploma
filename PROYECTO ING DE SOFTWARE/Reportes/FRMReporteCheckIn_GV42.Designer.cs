namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMReporteCheckIn_GV42
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTarjetaGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.dgvReporte = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colVuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumeroReserva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPasajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCheckIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuerta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoraLimite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSeccionLista = new System.Windows.Forms.Label();
            this.pnlSeparadorArriba = new System.Windows.Forms.Panel();
            this.pnlFiltros = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.chkFecha = new System.Windows.Forms.CheckBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblVuelo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblAsistencia = new System.Windows.Forms.Label();
            this.lblPasajero = new System.Windows.Forms.Label();
            this.txtVuelo = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.cboAsistencia = new System.Windows.Forms.ComboBox();
            this.txtPasajero = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.flpBotonesFiltro = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAplicar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnLimpiar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlSeparadorAbajo = new System.Windows.Forms.Panel();
            this.pnlResumen = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblResumen = new System.Windows.Forms.Label();
            this.lblResumenTitulo = new System.Windows.Forms.Label();
            this.pnlExportar = new System.Windows.Forms.Panel();
            this.btnExportar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlTarjetaGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.tlpFiltros.SuspendLayout();
            this.flpBotonesFiltro.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.pnlExportar.SuspendLayout();
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
            this.lblTitulo.Size = new System.Drawing.Size(236, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reporte de check-in";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(578, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "RFN 2 – Check-in: pasajeros presentes, pendientes y ausentes de cada vuelo.";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlTarjetaGrilla);
            this.pnlContenido.Controls.Add(this.pnlSeparadorArriba);
            this.pnlContenido.Controls.Add(this.pnlFiltros);
            this.pnlContenido.Controls.Add(this.pnlSeparadorAbajo);
            this.pnlContenido.Controls.Add(this.pnlResumen);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 1;
            //
            // pnlTarjetaGrilla
            //
            this.pnlTarjetaGrilla.Controls.Add(this.dgvReporte);
            this.pnlTarjetaGrilla.Controls.Add(this.lblSeccionLista);
            this.pnlTarjetaGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaGrilla.Location = new System.Drawing.Point(24, 192);
            this.pnlTarjetaGrilla.Name = "pnlTarjetaGrilla";
            this.pnlTarjetaGrilla.Padding = new System.Windows.Forms.Padding(16, 10, 16, 16);
            this.pnlTarjetaGrilla.Size = new System.Drawing.Size(952, 226);
            this.pnlTarjetaGrilla.TabIndex = 1;
            //
            // dgvReporte
            //
            this.dgvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVuelo,
            this.colOrigen,
            this.colDestino,
            this.colSalida,
            this.colNumeroReserva,
            this.colPasajero,
            this.colDni,
            this.colAsistencia,
            this.colEstado,
            this.colFechaCheckIn,
            this.colAsiento,
            this.colClase,
            this.colUbicacion,
            this.colTarjeta,
            this.colPuerta,
            this.colHoraLimite});
            this.dgvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporte.Location = new System.Drawing.Point(16, 40);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvReporte.Size = new System.Drawing.Size(920, 170);
            this.dgvReporte.TabIndex = 1;
            this.dgvReporte.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReporte_CellFormatting);
            //
            // colVuelo
            //
            this.colVuelo.DataPropertyName = "Vuelo";
            this.colVuelo.HeaderText = "Vuelo";
            this.colVuelo.Name = "colVuelo";
            this.colVuelo.ReadOnly = true;
            //
            // colOrigen
            //
            this.colOrigen.DataPropertyName = "Origen";
            this.colOrigen.HeaderText = "Origen";
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            //
            // colDestino
            //
            this.colDestino.DataPropertyName = "Destino";
            this.colDestino.HeaderText = "Destino";
            this.colDestino.Name = "colDestino";
            this.colDestino.ReadOnly = true;
            //
            // colSalida
            //
            this.colSalida.DataPropertyName = "Salida";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colSalida.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSalida.HeaderText = "Salida";
            this.colSalida.Name = "colSalida";
            this.colSalida.ReadOnly = true;
            //
            // colNumeroReserva
            //
            this.colNumeroReserva.DataPropertyName = "NumeroReserva";
            this.colNumeroReserva.HeaderText = "N° reserva";
            this.colNumeroReserva.Name = "colNumeroReserva";
            this.colNumeroReserva.ReadOnly = true;
            //
            // colPasajero
            //
            this.colPasajero.DataPropertyName = "Pasajero";
            this.colPasajero.HeaderText = "Pasajero";
            this.colPasajero.Name = "colPasajero";
            this.colPasajero.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.DataPropertyName = "Dni";
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colAsistencia
            //
            this.colAsistencia.DataPropertyName = "Asistencia";
            this.colAsistencia.HeaderText = "Asistencia";
            this.colAsistencia.Name = "colAsistencia";
            this.colAsistencia.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Check-in";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // colFechaCheckIn
            //
            this.colFechaCheckIn.DataPropertyName = "FechaCheckIn";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            this.colFechaCheckIn.DefaultCellStyle = dataGridViewCellStyle2;
            this.colFechaCheckIn.HeaderText = "Fecha check-in";
            this.colFechaCheckIn.Name = "colFechaCheckIn";
            this.colFechaCheckIn.ReadOnly = true;
            //
            // colAsiento
            //
            this.colAsiento.DataPropertyName = "Asiento";
            this.colAsiento.HeaderText = "Asiento";
            this.colAsiento.Name = "colAsiento";
            this.colAsiento.ReadOnly = true;
            //
            // colClase
            //
            this.colClase.DataPropertyName = "Clase";
            this.colClase.HeaderText = "Clase";
            this.colClase.Name = "colClase";
            this.colClase.ReadOnly = true;
            //
            // colUbicacion
            //
            this.colUbicacion.DataPropertyName = "Ubicacion";
            this.colUbicacion.HeaderText = "Ubicación";
            this.colUbicacion.Name = "colUbicacion";
            this.colUbicacion.ReadOnly = true;
            //
            // colTarjeta
            //
            this.colTarjeta.DataPropertyName = "Tarjeta";
            this.colTarjeta.HeaderText = "Tarjeta de embarque";
            this.colTarjeta.Name = "colTarjeta";
            this.colTarjeta.ReadOnly = true;
            //
            // colPuerta
            //
            this.colPuerta.DataPropertyName = "Puerta";
            this.colPuerta.HeaderText = "Puerta";
            this.colPuerta.Name = "colPuerta";
            this.colPuerta.ReadOnly = true;
            //
            // colHoraLimite
            //
            this.colHoraLimite.DataPropertyName = "HoraLimite";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm";
            this.colHoraLimite.DefaultCellStyle = dataGridViewCellStyle3;
            this.colHoraLimite.HeaderText = "Hora límite embarque";
            this.colHoraLimite.Name = "colHoraLimite";
            this.colHoraLimite.ReadOnly = true;
            //
            // lblSeccionLista
            //
            this.lblSeccionLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionLista.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionLista.Location = new System.Drawing.Point(16, 10);
            this.lblSeccionLista.Name = "lblSeccionLista";
            this.lblSeccionLista.Size = new System.Drawing.Size(920, 30);
            this.lblSeccionLista.TabIndex = 0;
            this.lblSeccionLista.Text = "Pasajeros por vuelo";
            //
            // pnlSeparadorArriba
            //
            this.pnlSeparadorArriba.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparadorArriba.Location = new System.Drawing.Point(24, 176);
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
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(20, 14, 20, 12);
            this.pnlFiltros.Size = new System.Drawing.Size(952, 156);
            this.pnlFiltros.TabIndex = 0;
            //
            // tlpFiltros
            //
            this.tlpFiltros.ColumnCount = 5;
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tlpFiltros.Controls.Add(this.chkFecha, 0, 1);
            this.tlpFiltros.Controls.Add(this.lblDesde, 1, 0);
            this.tlpFiltros.Controls.Add(this.lblHasta, 2, 0);
            this.tlpFiltros.Controls.Add(this.lblAyuda, 3, 0);
            this.tlpFiltros.Controls.Add(this.dtpDesde, 1, 1);
            this.tlpFiltros.Controls.Add(this.dtpHasta, 2, 1);
            this.tlpFiltros.Controls.Add(this.lblVuelo, 0, 2);
            this.tlpFiltros.Controls.Add(this.lblEstado, 1, 2);
            this.tlpFiltros.Controls.Add(this.lblAsistencia, 2, 2);
            this.tlpFiltros.Controls.Add(this.lblPasajero, 3, 2);
            this.tlpFiltros.Controls.Add(this.txtVuelo, 0, 3);
            this.tlpFiltros.Controls.Add(this.cboEstado, 1, 3);
            this.tlpFiltros.Controls.Add(this.cboAsistencia, 2, 3);
            this.tlpFiltros.Controls.Add(this.txtPasajero, 3, 3);
            this.tlpFiltros.Controls.Add(this.flpBotonesFiltro, 4, 3);
            this.tlpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFiltros.Location = new System.Drawing.Point(20, 14);
            this.tlpFiltros.Name = "tlpFiltros";
            this.tlpFiltros.RowCount = 4;
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpFiltros.Size = new System.Drawing.Size(912, 130);
            this.tlpFiltros.TabIndex = 0;
            //
            // chkFecha
            //
            this.chkFecha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkFecha.AutoSize = true;
            this.chkFecha.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.chkFecha.Location = new System.Drawing.Point(0, 32);
            this.chkFecha.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.chkFecha.Name = "chkFecha";
            this.chkFecha.Size = new System.Drawing.Size(132, 21);
            this.chkFecha.TabIndex = 0;
            this.chkFecha.Text = "Fecha de salida";
            this.chkFecha.UseVisualStyleBackColor = true;
            this.chkFecha.CheckedChanged += new System.EventHandler(this.chkFecha_CheckedChanged);
            //
            // lblDesde
            //
            this.lblDesde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDesde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblDesde.Location = new System.Drawing.Point(147, 5);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(45, 17);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde";
            //
            // lblHasta
            //
            this.lblHasta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblHasta.Location = new System.Drawing.Point(294, 5);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(42, 17);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta";
            //
            // lblAyuda
            //
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(441, 0);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0);
            this.lblAyuda.Name = "lblAyuda";
            this.tlpFiltros.SetColumnSpan(this.lblAyuda, 2);
            this.tlpFiltros.SetRowSpan(this.lblAyuda, 2);
            this.lblAyuda.Size = new System.Drawing.Size(471, 62);
            this.lblAyuda.TabIndex = 3;
            this.lblAyuda.Text = "Enter en Vuelo o Pasajero aplica los filtros. El PDF incluye exactamente los pasajeros que se ven en la grilla.";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dtpDesde
            //
            this.dtpDesde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(147, 30);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(133, 25);
            this.dtpDesde.TabIndex = 5;
            //
            // dtpHasta
            //
            this.dtpHasta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(294, 30);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(133, 25);
            this.dtpHasta.TabIndex = 6;
            //
            // lblVuelo
            //
            this.lblVuelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVuelo.AutoSize = true;
            this.lblVuelo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblVuelo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblVuelo.Location = new System.Drawing.Point(0, 65);
            this.lblVuelo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblVuelo.Name = "lblVuelo";
            this.lblVuelo.Size = new System.Drawing.Size(41, 17);
            this.lblVuelo.TabIndex = 7;
            this.lblVuelo.Text = "Vuelo";
            //
            // lblEstado
            //
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblEstado.Location = new System.Drawing.Point(147, 65);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 17);
            this.lblEstado.TabIndex = 8;
            this.lblEstado.Text = "Check-in";
            //
            // lblAsistencia
            //
            this.lblAsistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAsistencia.AutoSize = true;
            this.lblAsistencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAsistencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblAsistencia.Location = new System.Drawing.Point(294, 65);
            this.lblAsistencia.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblAsistencia.Name = "lblAsistencia";
            this.lblAsistencia.Size = new System.Drawing.Size(48, 17);
            this.lblAsistencia.TabIndex = 9;
            this.lblAsistencia.Text = "Asistencia";
            //
            // lblPasajero
            //
            this.lblPasajero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPasajero.AutoSize = true;
            this.lblPasajero.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPasajero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPasajero.Location = new System.Drawing.Point(441, 65);
            this.lblPasajero.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPasajero.Name = "lblPasajero";
            this.tlpFiltros.SetColumnSpan(this.lblPasajero, 2);
            this.lblPasajero.Size = new System.Drawing.Size(222, 17);
            this.lblPasajero.TabIndex = 10;
            this.lblPasajero.Text = "Pasajero (DNI, nombre o apellido)";
            //
            // txtVuelo
            //
            this.txtVuelo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVuelo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVuelo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVuelo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtVuelo.Location = new System.Drawing.Point(0, 94);
            this.txtVuelo.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.txtVuelo.MaxLength = 20;
            this.txtVuelo.Name = "txtVuelo";
            this.txtVuelo.Size = new System.Drawing.Size(133, 26);
            this.txtVuelo.TabIndex = 11;
            this.txtVuelo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFiltro_KeyDown);
            //
            // cboEstado
            //
            this.cboEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(147, 95);
            this.cboEstado.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(133, 25);
            this.cboEstado.TabIndex = 12;
            //
            // cboAsistencia
            //
            this.cboAsistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAsistencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAsistencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAsistencia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboAsistencia.FormattingEnabled = true;
            this.cboAsistencia.Location = new System.Drawing.Point(294, 95);
            this.cboAsistencia.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.cboAsistencia.Name = "cboAsistencia";
            this.cboAsistencia.Size = new System.Drawing.Size(133, 25);
            this.cboAsistencia.TabIndex = 13;
            //
            // txtPasajero
            //
            this.txtPasajero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasajero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasajero.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPasajero.Location = new System.Drawing.Point(441, 94);
            this.txtPasajero.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.txtPasajero.MaxLength = 60;
            this.txtPasajero.Name = "txtPasajero";
            this.txtPasajero.Size = new System.Drawing.Size(217, 26);
            this.txtPasajero.TabIndex = 14;
            this.toolTip1.SetToolTip(this.txtPasajero, "Busca por DNI, nombre o apellido del pasajero.");
            this.txtPasajero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFiltro_KeyDown);
            //
            // flpBotonesFiltro
            //
            this.flpBotonesFiltro.Controls.Add(this.btnAplicar);
            this.flpBotonesFiltro.Controls.Add(this.btnLimpiar);
            this.flpBotonesFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBotonesFiltro.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotonesFiltro.Location = new System.Drawing.Point(672, 84);
            this.flpBotonesFiltro.Margin = new System.Windows.Forms.Padding(0);
            this.flpBotonesFiltro.Name = "flpBotonesFiltro";
            this.flpBotonesFiltro.Size = new System.Drawing.Size(260, 46);
            this.flpBotonesFiltro.TabIndex = 15;
            this.flpBotonesFiltro.WrapContents = false;
            //
            // btnAplicar
            //
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.Location = new System.Drawing.Point(125, 3);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(115, 38);
            this.btnAplicar.TabIndex = 0;
            this.btnAplicar.Text = "Aplicar";
            this.toolTip1.SetToolTip(this.btnAplicar, "Aplica los filtros y actualiza el reporte.");
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Location = new System.Drawing.Point(0, 3);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(115, 38);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.toolTip1.SetToolTip(this.btnLimpiar, "Quita todos los filtros y muestra todos los vuelos.");
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // pnlSeparadorAbajo
            //
            this.pnlSeparadorAbajo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparadorAbajo.Location = new System.Drawing.Point(24, 418);
            this.pnlSeparadorAbajo.Name = "pnlSeparadorAbajo";
            this.pnlSeparadorAbajo.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparadorAbajo.TabIndex = 4;
            //
            // pnlResumen
            //
            this.pnlResumen.Controls.Add(this.lblResumen);
            this.pnlResumen.Controls.Add(this.lblResumenTitulo);
            this.pnlResumen.Controls.Add(this.pnlExportar);
            this.pnlResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResumen.Location = new System.Drawing.Point(24, 434);
            this.pnlResumen.Name = "pnlResumen";
            this.pnlResumen.Padding = new System.Windows.Forms.Padding(20, 10, 20, 12);
            this.pnlResumen.Size = new System.Drawing.Size(952, 116);
            this.pnlResumen.TabIndex = 2;
            //
            // lblResumen
            //
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblResumen.Location = new System.Drawing.Point(20, 36);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(732, 68);
            this.lblResumen.TabIndex = 1;
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblResumenTitulo
            //
            this.lblResumenTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumenTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblResumenTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblResumenTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblResumenTitulo.Name = "lblResumenTitulo";
            this.lblResumenTitulo.Size = new System.Drawing.Size(732, 26);
            this.lblResumenTitulo.TabIndex = 0;
            this.lblResumenTitulo.Text = "Resumen";
            //
            // pnlExportar
            //
            this.pnlExportar.Controls.Add(this.btnExportar);
            this.pnlExportar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlExportar.Location = new System.Drawing.Point(752, 10);
            this.pnlExportar.Name = "pnlExportar";
            this.pnlExportar.Size = new System.Drawing.Size(180, 94);
            this.pnlExportar.TabIndex = 2;
            //
            // btnExportar
            //
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportar.Location = new System.Drawing.Point(20, 28);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(160, 40);
            this.btnExportar.TabIndex = 0;
            this.btnExportar.Text = "Exportar PDF";
            this.toolTip1.SetToolTip(this.btnExportar, "Guarda en PDF los pasajeros que se ven en la grilla.");
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            //
            // FRMReporteCheckIn_GV42
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMReporteCheckIn_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de check-in";
            this.Load += new System.EventHandler(this.FRMReporteCheckIn_GV42_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlTarjetaGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.tlpFiltros.ResumeLayout(false);
            this.tlpFiltros.PerformLayout();
            this.flpBotonesFiltro.ResumeLayout(false);
            this.pnlResumen.ResumeLayout(false);
            this.pnlExportar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjetaGrilla;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvReporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroReserva;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPasajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsistencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCheckIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuerta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoraLimite;
        private System.Windows.Forms.Label lblSeccionLista;
        private System.Windows.Forms.Panel pnlSeparadorArriba;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlFiltros;
        private System.Windows.Forms.TableLayoutPanel tlpFiltros;
        private System.Windows.Forms.CheckBox chkFecha;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblVuelo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblAsistencia;
        private System.Windows.Forms.Label lblPasajero;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtVuelo;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.ComboBox cboAsistencia;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtPasajero;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesFiltro;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAplicar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnLimpiar;
        private System.Windows.Forms.Panel pnlSeparadorAbajo;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlResumen;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label lblResumenTitulo;
        private System.Windows.Forms.Panel pnlExportar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnExportar;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
