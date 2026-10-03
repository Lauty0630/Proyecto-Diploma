namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMReporteMillas_GV42
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTarjetaGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.dgvReporte = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPasajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMillasTotales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMillasPeriodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVuelosPeriodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaltan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProyeccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaEstimada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencen30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencen60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSeccionLista = new System.Windows.Forms.Label();
            this.pnlSeparadorArriba = new System.Windows.Forms.Panel();
            this.pnlFiltros = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblPasajero = new System.Windows.Forms.Label();
            this.cboPeriodo = new System.Windows.Forms.ComboBox();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
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
            this.lblTitulo.Text = "Reporte de millas";
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
            this.lblSubtitulo.Text = "Reporte inteligente: perfil de fidelización de cada pasajero y oferta recomendada.";
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
            this.colCuenta,
            this.colPasajero,
            this.colDni,
            this.colCategoria,
            this.colMillasTotales,
            this.colMillasPeriodo,
            this.colVuelosPeriodo,
            this.colFaltan,
            this.colProyeccion,
            this.colFechaEstimada,
            this.colVencen30,
            this.colVencen60});
            this.dgvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporte.Location = new System.Drawing.Point(16, 40);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dgvReporte.Size = new System.Drawing.Size(920, 170);
            this.dgvReporte.TabIndex = 1;
            this.dgvReporte.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReporte_CellFormatting);
            this.dgvReporte.SelectionChanged += new System.EventHandler(this.dgvReporte_SelectionChanged);
            //
            // colCuenta
            //
            this.colCuenta.DataPropertyName = "Cuenta";
            this.colCuenta.HeaderText = "N° de cuenta";
            this.colCuenta.Name = "colCuenta";
            this.colCuenta.ReadOnly = true;
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
            // colCategoria
            //
            this.colCategoria.DataPropertyName = "Categoria";
            this.colCategoria.HeaderText = "Categoría";
            this.colCategoria.Name = "colCategoria";
            this.colCategoria.ReadOnly = true;
            //
            // colMillasTotales
            //
            this.colMillasTotales.DataPropertyName = "MillasTotales";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.colMillasTotales.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMillasTotales.HeaderText = "Millas totales";
            this.colMillasTotales.Name = "colMillasTotales";
            this.colMillasTotales.ReadOnly = true;
            //
            // colMillasPeriodo
            //
            this.colMillasPeriodo.DataPropertyName = "MillasPeriodo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            this.colMillasPeriodo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colMillasPeriodo.HeaderText = "Millas del período";
            this.colMillasPeriodo.Name = "colMillasPeriodo";
            this.colMillasPeriodo.ReadOnly = true;
            //
            // colVuelosPeriodo
            //
            this.colVuelosPeriodo.DataPropertyName = "VuelosPeriodo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colVuelosPeriodo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colVuelosPeriodo.HeaderText = "Vuelos del período";
            this.colVuelosPeriodo.Name = "colVuelosPeriodo";
            this.colVuelosPeriodo.ReadOnly = true;
            //
            // colFaltan
            //
            this.colFaltan.DataPropertyName = "Faltan";
            this.colFaltan.HeaderText = "Para la categoría siguiente";
            this.colFaltan.Name = "colFaltan";
            this.colFaltan.ReadOnly = true;
            //
            // colProyeccion
            //
            this.colProyeccion.DataPropertyName = "Proyeccion";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colProyeccion.DefaultCellStyle = dataGridViewCellStyle4;
            this.colProyeccion.HeaderText = "Proyección 90 días";
            this.colProyeccion.Name = "colProyeccion";
            this.colProyeccion.ReadOnly = true;
            //
            // colFechaEstimada
            //
            this.colFechaEstimada.DataPropertyName = "FechaEstimada";
            this.colFechaEstimada.HeaderText = "Fecha estimada";
            this.colFechaEstimada.Name = "colFechaEstimada";
            this.colFechaEstimada.ReadOnly = true;
            //
            // colVencen30
            //
            this.colVencen30.DataPropertyName = "Vencen30";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            this.colVencen30.DefaultCellStyle = dataGridViewCellStyle5;
            this.colVencen30.HeaderText = "Vencen en 30 días";
            this.colVencen30.Name = "colVencen30";
            this.colVencen30.ReadOnly = true;
            //
            // colVencen60
            //
            this.colVencen60.DataPropertyName = "Vencen60";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colVencen60.DefaultCellStyle = dataGridViewCellStyle6;
            this.colVencen60.HeaderText = "Vencen en 60 días";
            this.colVencen60.Name = "colVencen60";
            this.colVencen60.ReadOnly = true;
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
            this.lblSeccionLista.Text = "Pasajeros del programa de millas";
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
            this.pnlFiltros.Size = new System.Drawing.Size(952, 124);
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
            this.tlpFiltros.Controls.Add(this.lblAyuda, 0, 2);
            this.tlpFiltros.Controls.Add(this.lblPeriodo, 0, 0);
            this.tlpFiltros.Controls.Add(this.lblCategoria, 1, 0);
            this.tlpFiltros.Controls.Add(this.lblPasajero, 2, 0);
            this.tlpFiltros.Controls.Add(this.cboPeriodo, 0, 1);
            this.tlpFiltros.Controls.Add(this.cboCategoria, 1, 1);
            this.tlpFiltros.Controls.Add(this.txtPasajero, 2, 1);
            this.tlpFiltros.Controls.Add(this.flpBotonesFiltro, 4, 1);
            this.tlpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFiltros.Location = new System.Drawing.Point(20, 14);
            this.tlpFiltros.Name = "tlpFiltros";
            this.tlpFiltros.RowCount = 3;
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpFiltros.Size = new System.Drawing.Size(912, 98);
            this.tlpFiltros.TabIndex = 0;
            //
            // lblAyuda
            //
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(441, 0);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0);
            this.lblAyuda.Name = "lblAyuda";
            this.tlpFiltros.SetColumnSpan(this.lblAyuda, 5);
            this.lblAyuda.Size = new System.Drawing.Size(471, 62);
            this.lblAyuda.TabIndex = 3;
            this.lblAyuda.Text = "Las millas se calculan con los vuelos que el pasajero ya voló. Elegí un pasajero para ver su perfil y la oferta recomendada.";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPeriodo
            //
            this.lblPeriodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPeriodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPeriodo.Location = new System.Drawing.Point(147, 65);
            this.lblPeriodo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(40, 17);
            this.lblPeriodo.TabIndex = 8;
            this.lblPeriodo.Text = "Período";
            //
            // lblCategoria
            //
            this.lblCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCategoria.Location = new System.Drawing.Point(294, 65);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(48, 17);
            this.lblCategoria.TabIndex = 9;
            this.lblCategoria.Text = "Categoría";
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
            // cboPeriodo
            //
            this.cboPeriodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPeriodo.FormattingEnabled = true;
            this.cboPeriodo.Location = new System.Drawing.Point(147, 95);
            this.cboPeriodo.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Size = new System.Drawing.Size(133, 25);
            this.cboPeriodo.TabIndex = 12;
            //
            // cboCategoria
            //
            this.cboCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCategoria.FormattingEnabled = true;
            this.cboCategoria.Location = new System.Drawing.Point(294, 95);
            this.cboCategoria.Margin = new System.Windows.Forms.Padding(0, 3, 14, 3);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Size = new System.Drawing.Size(133, 25);
            this.cboCategoria.TabIndex = 13;
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
            this.tlpFiltros.SetColumnSpan(this.txtPasajero, 2);
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
            this.toolTip1.SetToolTip(this.btnLimpiar, "Quita todos los filtros y muestra todos los pasajeros.");
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
            this.pnlResumen.Size = new System.Drawing.Size(952, 196);
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
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.TopLeft;
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
            this.lblResumenTitulo.Text = "Perfil del pasajero y oferta recomendada";
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
            // FRMReporteMillas_GV42
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMReporteMillas_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de millas";
            this.Load += new System.EventHandler(this.FRMReporteMillas_GV42_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPasajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMillasTotales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMillasPeriodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelosPeriodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaltan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProyeccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaEstimada;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencen30;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencen60;
        private System.Windows.Forms.Label lblSeccionLista;
        private System.Windows.Forms.Panel pnlSeparadorArriba;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlFiltros;
        private System.Windows.Forms.TableLayoutPanel tlpFiltros;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblPasajero;
        private System.Windows.Forms.ComboBox cboPeriodo;
        private System.Windows.Forms.ComboBox cboCategoria;
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
