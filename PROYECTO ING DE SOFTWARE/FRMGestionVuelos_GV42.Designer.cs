namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMGestionVuelos_GV42
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
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlTarjetaGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.dgvVuelos = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAerolinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuerta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostoKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSeccionVuelos = new System.Windows.Forms.Label();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.pnlEditor = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnBaja = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnReactivar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.tlpCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblAerolinea = new System.Windows.Forms.Label();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.txtCodigo = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.cmbAerolinea = new System.Windows.Forms.ComboBox();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.cmbDestino = new System.Windows.Forms.ComboBox();
            this.lblSalida = new System.Windows.Forms.Label();
            this.lblLlegada = new System.Windows.Forms.Label();
            this.lblPuerta = new System.Windows.Forms.Label();
            this.lblCosto = new System.Windows.Forms.Label();
            this.dtSalida = new System.Windows.Forms.DateTimePicker();
            this.dtLlegada = new System.Windows.Forms.DateTimePicker();
            this.txtPuerta = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.numCosto = new System.Windows.Forms.NumericUpDown();
            this.lblSeccionEditor = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlTarjetaGrilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVuelos)).BeginInit();
            this.pnlEditor.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.flpBotones.SuspendLayout();
            this.tlpCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(215, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de vuelos";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(391, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Modificá los datos de un vuelo, dalo de baja o reactivalo";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlTarjetaGrilla);
            this.pnlContenido.Controls.Add(this.pnlSeparador);
            this.pnlContenido.Controls.Add(this.pnlEditor);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 1;
            //
            // pnlTarjetaGrilla
            //
            this.pnlTarjetaGrilla.Controls.Add(this.dgvVuelos);
            this.pnlTarjetaGrilla.Controls.Add(this.lblSeccionVuelos);
            this.pnlTarjetaGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaGrilla.Location = new System.Drawing.Point(24, 20);
            this.pnlTarjetaGrilla.Name = "pnlTarjetaGrilla";
            this.pnlTarjetaGrilla.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.pnlTarjetaGrilla.Size = new System.Drawing.Size(952, 282);
            this.pnlTarjetaGrilla.TabIndex = 0;
            //
            // dgvVuelos
            //
            this.dgvVuelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colAerolinea,
            this.colRuta,
            this.colSalida,
            this.colLlegada,
            this.colPuerta,
            this.colCostoKilo,
            this.colEstado});
            this.dgvVuelos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVuelos.Location = new System.Drawing.Point(16, 44);
            this.dgvVuelos.Name = "dgvVuelos";
            this.dgvVuelos.Size = new System.Drawing.Size(920, 222);
            this.dgvVuelos.TabIndex = 1;
            this.dgvVuelos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVuelos_CellFormatting);
            this.dgvVuelos.SelectionChanged += new System.EventHandler(this.dgvVuelos_SelectionChanged);
            //
            // colCodigo
            //
            this.colCodigo.DataPropertyName = "Codigo";
            this.colCodigo.FillWeight = 60F;
            this.colCodigo.HeaderText = "Vuelo";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            //
            // colAerolinea
            //
            this.colAerolinea.DataPropertyName = "Aerolinea";
            this.colAerolinea.FillWeight = 110F;
            this.colAerolinea.HeaderText = "Aerolínea";
            this.colAerolinea.Name = "colAerolinea";
            this.colAerolinea.ReadOnly = true;
            //
            // colRuta
            //
            this.colRuta.DataPropertyName = "Ruta";
            this.colRuta.FillWeight = 80F;
            this.colRuta.HeaderText = "Ruta";
            this.colRuta.Name = "colRuta";
            this.colRuta.ReadOnly = true;
            //
            // colSalida
            //
            this.colSalida.DataPropertyName = "Salida";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colSalida.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSalida.FillWeight = 100F;
            this.colSalida.HeaderText = "Salida";
            this.colSalida.Name = "colSalida";
            this.colSalida.ReadOnly = true;
            //
            // colLlegada
            //
            this.colLlegada.DataPropertyName = "Llegada";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            this.colLlegada.DefaultCellStyle = dataGridViewCellStyle2;
            this.colLlegada.FillWeight = 100F;
            this.colLlegada.HeaderText = "Llegada";
            this.colLlegada.Name = "colLlegada";
            this.colLlegada.ReadOnly = true;
            //
            // colPuerta
            //
            this.colPuerta.DataPropertyName = "Puerta";
            this.colPuerta.FillWeight = 50F;
            this.colPuerta.HeaderText = "Puerta";
            this.colPuerta.Name = "colPuerta";
            this.colPuerta.ReadOnly = true;
            //
            // colCostoKilo
            //
            this.colCostoKilo.DataPropertyName = "CostoKilo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.colCostoKilo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colCostoKilo.FillWeight = 70F;
            this.colCostoKilo.HeaderText = "$/kg exceso";
            this.colCostoKilo.Name = "colCostoKilo";
            this.colCostoKilo.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.FillWeight = 70F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // lblSeccionVuelos
            //
            this.lblSeccionVuelos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionVuelos.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionVuelos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionVuelos.Location = new System.Drawing.Point(16, 12);
            this.lblSeccionVuelos.Name = "lblSeccionVuelos";
            this.lblSeccionVuelos.Size = new System.Drawing.Size(920, 32);
            this.lblSeccionVuelos.TabIndex = 0;
            this.lblSeccionVuelos.Text = "Vuelos programados";
            //
            // pnlSeparador
            //
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparador.Location = new System.Drawing.Point(24, 302);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparador.TabIndex = 1;
            //
            // pnlEditor
            //
            this.pnlEditor.AcentoSuperior = true;
            this.pnlEditor.Controls.Add(this.pnlAcciones);
            this.pnlEditor.Controls.Add(this.tlpCampos);
            this.pnlEditor.Controls.Add(this.lblSeccionEditor);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEditor.Enabled = false;
            this.pnlEditor.Location = new System.Drawing.Point(24, 318);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new System.Windows.Forms.Padding(20, 14, 20, 12);
            this.pnlEditor.Size = new System.Drawing.Size(952, 232);
            this.pnlEditor.TabIndex = 2;
            //
            // pnlAcciones
            //
            this.pnlAcciones.Controls.Add(this.lblAyuda);
            this.pnlAcciones.Controls.Add(this.flpBotones);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAcciones.Location = new System.Drawing.Point(20, 160);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(912, 60);
            this.pnlAcciones.TabIndex = 2;
            //
            // lblAyuda
            //
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(0, 0);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(402, 60);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Cada cambio queda registrado en la bitácora de vuelos (Vuelo_C).\nLa baja es lógica: el vuelo no se elimina, solo deja de ofrecerse.";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flpBotones
            //
            this.flpBotones.Controls.Add(this.btnGuardar);
            this.flpBotones.Controls.Add(this.btnBaja);
            this.flpBotones.Controls.Add(this.btnReactivar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Location = new System.Drawing.Point(402, 0);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpBotones.Size = new System.Drawing.Size(510, 60);
            this.flpBotones.TabIndex = 0;
            this.flpBotones.WrapContents = false;
            //
            // btnGuardar
            //
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(340, 11);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(170, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnBaja
            //
            this.btnBaja.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Peligro;
            this.btnBaja.FlatAppearance.BorderSize = 0;
            this.btnBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaja.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBaja.Location = new System.Drawing.Point(180, 11);
            this.btnBaja.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.Size = new System.Drawing.Size(150, 40);
            this.btnBaja.TabIndex = 1;
            this.btnBaja.Text = "Dar de baja";
            this.btnBaja.UseVisualStyleBackColor = false;
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            //
            // btnReactivar
            //
            this.btnReactivar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Exito;
            this.btnReactivar.FlatAppearance.BorderSize = 0;
            this.btnReactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReactivar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnReactivar.Location = new System.Drawing.Point(20, 11);
            this.btnReactivar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(150, 40);
            this.btnReactivar.TabIndex = 2;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = false;
            this.btnReactivar.Click += new System.EventHandler(this.btnReactivar_Click);
            //
            // tlpCampos
            //
            this.tlpCampos.ColumnCount = 4;
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpCampos.Controls.Add(this.lblCodigo, 0, 0);
            this.tlpCampos.Controls.Add(this.lblAerolinea, 1, 0);
            this.tlpCampos.Controls.Add(this.lblOrigen, 2, 0);
            this.tlpCampos.Controls.Add(this.lblDestino, 3, 0);
            this.tlpCampos.Controls.Add(this.txtCodigo, 0, 1);
            this.tlpCampos.Controls.Add(this.cmbAerolinea, 1, 1);
            this.tlpCampos.Controls.Add(this.cmbOrigen, 2, 1);
            this.tlpCampos.Controls.Add(this.cmbDestino, 3, 1);
            this.tlpCampos.Controls.Add(this.lblSalida, 0, 2);
            this.tlpCampos.Controls.Add(this.lblLlegada, 1, 2);
            this.tlpCampos.Controls.Add(this.lblPuerta, 2, 2);
            this.tlpCampos.Controls.Add(this.lblCosto, 3, 2);
            this.tlpCampos.Controls.Add(this.dtSalida, 0, 3);
            this.tlpCampos.Controls.Add(this.dtLlegada, 1, 3);
            this.tlpCampos.Controls.Add(this.txtPuerta, 2, 3);
            this.tlpCampos.Controls.Add(this.numCosto, 3, 3);
            this.tlpCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCampos.Location = new System.Drawing.Point(20, 44);
            this.tlpCampos.Name = "tlpCampos";
            this.tlpCampos.RowCount = 4;
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.Size = new System.Drawing.Size(912, 116);
            this.tlpCampos.TabIndex = 1;
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
            this.lblCodigo.Size = new System.Drawing.Size(52, 17);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código";
            //
            // lblAerolinea
            //
            this.lblAerolinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAerolinea.AutoSize = true;
            this.lblAerolinea.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAerolinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblAerolinea.Location = new System.Drawing.Point(182, 3);
            this.lblAerolinea.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblAerolinea.Name = "lblAerolinea";
            this.lblAerolinea.Size = new System.Drawing.Size(68, 17);
            this.lblAerolinea.TabIndex = 1;
            this.lblAerolinea.Text = "Aerolínea";
            //
            // lblOrigen
            //
            this.lblOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOrigen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblOrigen.Location = new System.Drawing.Point(400, 3);
            this.lblOrigen.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(50, 17);
            this.lblOrigen.TabIndex = 2;
            this.lblOrigen.Text = "Origen";
            //
            // lblDestino
            //
            this.lblDestino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDestino.AutoSize = true;
            this.lblDestino.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDestino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblDestino.Location = new System.Drawing.Point(655, 3);
            this.lblDestino.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(55, 17);
            this.lblDestino.TabIndex = 3;
            this.lblDestino.Text = "Destino";
            //
            // txtCodigo
            //
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCodigo.Location = new System.Drawing.Point(0, 27);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(164, 26);
            this.txtCodigo.TabIndex = 4;
            //
            // cmbAerolinea
            //
            this.cmbAerolinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAerolinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAerolinea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAerolinea.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAerolinea.FormattingEnabled = true;
            this.cmbAerolinea.Location = new System.Drawing.Point(182, 28);
            this.cmbAerolinea.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.cmbAerolinea.Name = "cmbAerolinea";
            this.cmbAerolinea.Size = new System.Drawing.Size(200, 25);
            this.cmbAerolinea.TabIndex = 5;
            //
            // cmbOrigen
            //
            this.cmbOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(400, 28);
            this.cmbOrigen.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(237, 25);
            this.cmbOrigen.TabIndex = 6;
            //
            // cmbDestino
            //
            this.cmbDestino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestino.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDestino.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDestino.FormattingEnabled = true;
            this.cmbDestino.Location = new System.Drawing.Point(655, 28);
            this.cmbDestino.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.cmbDestino.Name = "cmbDestino";
            this.cmbDestino.Size = new System.Drawing.Size(257, 25);
            this.cmbDestino.TabIndex = 7;
            //
            // lblSalida
            //
            this.lblSalida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSalida.AutoSize = true;
            this.lblSalida.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSalida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSalida.Location = new System.Drawing.Point(0, 61);
            this.lblSalida.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblSalida.Name = "lblSalida";
            this.lblSalida.Size = new System.Drawing.Size(45, 17);
            this.lblSalida.TabIndex = 8;
            this.lblSalida.Text = "Salida";
            //
            // lblLlegada
            //
            this.lblLlegada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLlegada.AutoSize = true;
            this.lblLlegada.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLlegada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblLlegada.Location = new System.Drawing.Point(182, 61);
            this.lblLlegada.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblLlegada.Name = "lblLlegada";
            this.lblLlegada.Size = new System.Drawing.Size(55, 17);
            this.lblLlegada.TabIndex = 9;
            this.lblLlegada.Text = "Llegada";
            //
            // lblPuerta
            //
            this.lblPuerta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPuerta.AutoSize = true;
            this.lblPuerta.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPuerta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPuerta.Location = new System.Drawing.Point(400, 61);
            this.lblPuerta.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPuerta.Name = "lblPuerta";
            this.lblPuerta.Size = new System.Drawing.Size(49, 17);
            this.lblPuerta.TabIndex = 10;
            this.lblPuerta.Text = "Puerta";
            //
            // lblCosto
            //
            this.lblCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCosto.Location = new System.Drawing.Point(655, 61);
            this.lblCosto.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(190, 17);
            this.lblCosto.TabIndex = 11;
            this.lblCosto.Text = "Costo por kilo de exceso ($)";
            //
            // dtSalida
            //
            this.dtSalida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtSalida.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtSalida.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSalida.Location = new System.Drawing.Point(0, 86);
            this.dtSalida.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.dtSalida.Name = "dtSalida";
            this.dtSalida.Size = new System.Drawing.Size(164, 25);
            this.dtSalida.TabIndex = 12;
            //
            // dtLlegada
            //
            this.dtLlegada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtLlegada.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtLlegada.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLlegada.Location = new System.Drawing.Point(182, 86);
            this.dtLlegada.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.dtLlegada.Name = "dtLlegada";
            this.dtLlegada.Size = new System.Drawing.Size(200, 25);
            this.dtLlegada.TabIndex = 13;
            //
            // txtPuerta
            //
            this.txtPuerta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPuerta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPuerta.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPuerta.Location = new System.Drawing.Point(400, 85);
            this.txtPuerta.Margin = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.txtPuerta.MaxLength = 10;
            this.txtPuerta.Name = "txtPuerta";
            this.txtPuerta.Size = new System.Drawing.Size(237, 26);
            this.txtPuerta.TabIndex = 14;
            //
            // numCosto
            //
            this.numCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numCosto.DecimalPlaces = 2;
            this.numCosto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCosto.Location = new System.Drawing.Point(655, 86);
            this.numCosto.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.numCosto.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCosto.Name = "numCosto";
            this.numCosto.Size = new System.Drawing.Size(257, 25);
            this.numCosto.TabIndex = 15;
            this.numCosto.ThousandsSeparator = true;
            //
            // lblSeccionEditor
            //
            this.lblSeccionEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionEditor.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionEditor.Location = new System.Drawing.Point(20, 14);
            this.lblSeccionEditor.Name = "lblSeccionEditor";
            this.lblSeccionEditor.Size = new System.Drawing.Size(912, 30);
            this.lblSeccionEditor.TabIndex = 0;
            this.lblSeccionEditor.Text = "Datos del vuelo seleccionado";
            //
            // FRMGestionVuelos_GV42
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMGestionVuelos_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de vuelos";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlTarjetaGrilla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVuelos)).EndInit();
            this.pnlEditor.ResumeLayout(false);
            this.pnlAcciones.ResumeLayout(false);
            this.flpBotones.ResumeLayout(false);
            this.tlpCampos.ResumeLayout(false);
            this.tlpCampos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjetaGrilla;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvVuelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAerolinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLlegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuerta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostoKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.Label lblSeccionVuelos;
        private System.Windows.Forms.Panel pnlSeparador;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlEditor;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnGuardar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnBaja;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnReactivar;
        private System.Windows.Forms.TableLayoutPanel tlpCampos;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblAerolinea;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.Label lblDestino;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtCodigo;
        private System.Windows.Forms.ComboBox cmbAerolinea;
        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.ComboBox cmbDestino;
        private System.Windows.Forms.Label lblSalida;
        private System.Windows.Forms.Label lblLlegada;
        private System.Windows.Forms.Label lblPuerta;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.DateTimePicker dtSalida;
        private System.Windows.Forms.DateTimePicker dtLlegada;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtPuerta;
        private System.Windows.Forms.NumericUpDown numCosto;
        private System.Windows.Forms.Label lblSeccionEditor;
    }
}
