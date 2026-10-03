namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMAltaVuelo_GV42
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
            this.pnlTarjeta = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblAerolinea = new System.Windows.Forms.Label();
            this.txtCodigo = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.cmbAerolinea = new System.Windows.Forms.ComboBox();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.cmbDestino = new System.Windows.Forms.ComboBox();
            this.lblSalida = new System.Windows.Forms.Label();
            this.lblLlegada = new System.Windows.Forms.Label();
            this.dtSalida = new System.Windows.Forms.DateTimePicker();
            this.dtLlegada = new System.Windows.Forms.DateTimePicker();
            this.lblPuerta = new System.Windows.Forms.Label();
            this.lblCosto = new System.Windows.Forms.Label();
            this.txtPuerta = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.numCosto = new System.Windows.Forms.NumericUpDown();
            this.lblSeccionPrecios = new System.Windows.Forms.Label();
            this.lblPrecioEconomica = new System.Windows.Forms.Label();
            this.lblPrecioEjecutiva = new System.Windows.Forms.Label();
            this.numPrecioEconomica = new System.Windows.Forms.NumericUpDown();
            this.numPrecioEjecutiva = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioPrimera = new System.Windows.Forms.Label();
            this.numPrecioPrimera = new System.Windows.Forms.NumericUpDown();
            this.lblMapa = new System.Windows.Forms.Label();
            this.flpBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCrear = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlTarjeta.SuspendLayout();
            this.tlpCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioEconomica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioEjecutiva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioPrimera)).BeginInit();
            this.flpBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(680, 80);
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
            this.lblTitulo.Size = new System.Drawing.Size(150, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Nuevo vuelo";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(400, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Cargá los datos del vuelo y el precio base de cada clase";
            //
            // pnlContenido
            //
            this.pnlContenido.Controls.Add(this.pnlTarjeta);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(680, 510);
            this.pnlContenido.TabIndex = 1;
            //
            // pnlTarjeta
            //
            this.pnlTarjeta.AcentoSuperior = true;
            this.pnlTarjeta.Controls.Add(this.flpBotones);
            this.pnlTarjeta.Controls.Add(this.tlpCampos);
            this.pnlTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjeta.Location = new System.Drawing.Point(24, 20);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(20, 14, 20, 12);
            this.pnlTarjeta.Size = new System.Drawing.Size(632, 470);
            this.pnlTarjeta.TabIndex = 0;
            //
            // tlpCampos
            //
            this.tlpCampos.ColumnCount = 2;
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCampos.Controls.Add(this.lblCodigo, 0, 0);
            this.tlpCampos.Controls.Add(this.lblAerolinea, 1, 0);
            this.tlpCampos.Controls.Add(this.txtCodigo, 0, 1);
            this.tlpCampos.Controls.Add(this.cmbAerolinea, 1, 1);
            this.tlpCampos.Controls.Add(this.lblOrigen, 0, 2);
            this.tlpCampos.Controls.Add(this.lblDestino, 1, 2);
            this.tlpCampos.Controls.Add(this.cmbOrigen, 0, 3);
            this.tlpCampos.Controls.Add(this.cmbDestino, 1, 3);
            this.tlpCampos.Controls.Add(this.lblSalida, 0, 4);
            this.tlpCampos.Controls.Add(this.lblLlegada, 1, 4);
            this.tlpCampos.Controls.Add(this.dtSalida, 0, 5);
            this.tlpCampos.Controls.Add(this.dtLlegada, 1, 5);
            this.tlpCampos.Controls.Add(this.lblPuerta, 0, 6);
            this.tlpCampos.Controls.Add(this.lblCosto, 1, 6);
            this.tlpCampos.Controls.Add(this.txtPuerta, 0, 7);
            this.tlpCampos.Controls.Add(this.numCosto, 1, 7);
            this.tlpCampos.Controls.Add(this.lblSeccionPrecios, 0, 8);
            this.tlpCampos.Controls.Add(this.lblPrecioEconomica, 0, 9);
            this.tlpCampos.Controls.Add(this.lblPrecioEjecutiva, 1, 9);
            this.tlpCampos.Controls.Add(this.numPrecioEconomica, 0, 10);
            this.tlpCampos.Controls.Add(this.numPrecioEjecutiva, 1, 10);
            this.tlpCampos.Controls.Add(this.lblPrecioPrimera, 0, 11);
            this.tlpCampos.Controls.Add(this.numPrecioPrimera, 0, 12);
            this.tlpCampos.Controls.Add(this.lblMapa, 1, 12);
            this.tlpCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCampos.Location = new System.Drawing.Point(20, 14);
            this.tlpCampos.Name = "tlpCampos";
            this.tlpCampos.RowCount = 13;
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCampos.Size = new System.Drawing.Size(592, 382);
            this.tlpCampos.TabIndex = 0;
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
            this.lblCodigo.Size = new System.Drawing.Size(60, 17);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código";
            //
            // lblAerolinea
            //
            this.lblAerolinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAerolinea.AutoSize = true;
            this.lblAerolinea.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAerolinea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblAerolinea.Location = new System.Drawing.Point(296, 3);
            this.lblAerolinea.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblAerolinea.Name = "lblAerolinea";
            this.lblAerolinea.Size = new System.Drawing.Size(60, 17);
            this.lblAerolinea.TabIndex = 1;
            this.lblAerolinea.Text = "Aerolínea";
            //
            // txtCodigo
            //
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCodigo.Location = new System.Drawing.Point(0, 27);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(276, 26);
            this.txtCodigo.TabIndex = 2;
            //
            // cmbAerolinea
            //
            this.cmbAerolinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAerolinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAerolinea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAerolinea.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAerolinea.FormattingEnabled = true;
            this.cmbAerolinea.Location = new System.Drawing.Point(296, 27);
            this.cmbAerolinea.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.cmbAerolinea.Name = "cmbAerolinea";
            this.cmbAerolinea.Size = new System.Drawing.Size(276, 25);
            this.cmbAerolinea.TabIndex = 3;
            //
            // lblOrigen
            //
            this.lblOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblOrigen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblOrigen.Location = new System.Drawing.Point(0, 61);
            this.lblOrigen.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(60, 17);
            this.lblOrigen.TabIndex = 4;
            this.lblOrigen.Text = "Origen";
            //
            // lblDestino
            //
            this.lblDestino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDestino.AutoSize = true;
            this.lblDestino.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDestino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblDestino.Location = new System.Drawing.Point(296, 61);
            this.lblDestino.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(60, 17);
            this.lblDestino.TabIndex = 5;
            this.lblDestino.Text = "Destino";
            //
            // cmbOrigen
            //
            this.cmbOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(0, 85);
            this.cmbOrigen.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(276, 25);
            this.cmbOrigen.TabIndex = 6;
            //
            // cmbDestino
            //
            this.cmbDestino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestino.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDestino.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDestino.FormattingEnabled = true;
            this.cmbDestino.Location = new System.Drawing.Point(296, 85);
            this.cmbDestino.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.cmbDestino.Name = "cmbDestino";
            this.cmbDestino.Size = new System.Drawing.Size(276, 25);
            this.cmbDestino.TabIndex = 7;
            //
            // lblSalida
            //
            this.lblSalida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSalida.AutoSize = true;
            this.lblSalida.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSalida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSalida.Location = new System.Drawing.Point(0, 119);
            this.lblSalida.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblSalida.Name = "lblSalida";
            this.lblSalida.Size = new System.Drawing.Size(60, 17);
            this.lblSalida.TabIndex = 8;
            this.lblSalida.Text = "Salida";
            //
            // lblLlegada
            //
            this.lblLlegada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLlegada.AutoSize = true;
            this.lblLlegada.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLlegada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblLlegada.Location = new System.Drawing.Point(296, 119);
            this.lblLlegada.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblLlegada.Name = "lblLlegada";
            this.lblLlegada.Size = new System.Drawing.Size(60, 17);
            this.lblLlegada.TabIndex = 9;
            this.lblLlegada.Text = "Llegada";
            //
            // dtSalida
            //
            this.dtSalida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtSalida.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtSalida.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSalida.Location = new System.Drawing.Point(0, 143);
            this.dtSalida.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.dtSalida.Name = "dtSalida";
            this.dtSalida.Size = new System.Drawing.Size(276, 25);
            this.dtSalida.TabIndex = 10;
            //
            // dtLlegada
            //
            this.dtLlegada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtLlegada.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtLlegada.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLlegada.Location = new System.Drawing.Point(296, 143);
            this.dtLlegada.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.dtLlegada.Name = "dtLlegada";
            this.dtLlegada.Size = new System.Drawing.Size(276, 25);
            this.dtLlegada.TabIndex = 11;
            //
            // lblPuerta
            //
            this.lblPuerta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPuerta.AutoSize = true;
            this.lblPuerta.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPuerta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPuerta.Location = new System.Drawing.Point(0, 177);
            this.lblPuerta.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPuerta.Name = "lblPuerta";
            this.lblPuerta.Size = new System.Drawing.Size(60, 17);
            this.lblPuerta.TabIndex = 12;
            this.lblPuerta.Text = "Puerta";
            //
            // lblCosto
            //
            this.lblCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCosto.AutoSize = true;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCosto.Location = new System.Drawing.Point(296, 177);
            this.lblCosto.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(60, 17);
            this.lblCosto.TabIndex = 13;
            this.lblCosto.Text = "Costo por kilo de exceso ($)";
            //
            // txtPuerta
            //
            this.txtPuerta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPuerta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPuerta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPuerta.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPuerta.Location = new System.Drawing.Point(0, 201);
            this.txtPuerta.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.txtPuerta.MaxLength = 10;
            this.txtPuerta.Name = "txtPuerta";
            this.txtPuerta.Size = new System.Drawing.Size(276, 26);
            this.txtPuerta.TabIndex = 14;
            //
            // numCosto
            //
            this.numCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numCosto.DecimalPlaces = 2;
            this.numCosto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCosto.Location = new System.Drawing.Point(296, 201);
            this.numCosto.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.numCosto.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCosto.Name = "numCosto";
            this.numCosto.Size = new System.Drawing.Size(276, 25);
            this.numCosto.TabIndex = 15;
            this.numCosto.ThousandsSeparator = true;
            //
            // lblSeccionPrecios
            //
            this.lblSeccionPrecios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSeccionPrecios.AutoSize = true;
            this.tlpCampos.SetColumnSpan(this.lblSeccionPrecios, 2);
            this.lblSeccionPrecios.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblSeccionPrecios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblSeccionPrecios.Location = new System.Drawing.Point(0, 242);
            this.lblSeccionPrecios.Margin = new System.Windows.Forms.Padding(0, 0, 3, 4);
            this.lblSeccionPrecios.Name = "lblSeccionPrecios";
            this.lblSeccionPrecios.Size = new System.Drawing.Size(190, 20);
            this.lblSeccionPrecios.TabIndex = 16;
            this.lblSeccionPrecios.Text = "Precio base por clase ($)";
            //
            // lblPrecioEconomica
            //
            this.lblPrecioEconomica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrecioEconomica.AutoSize = true;
            this.lblPrecioEconomica.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPrecioEconomica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPrecioEconomica.Location = new System.Drawing.Point(0, 269);
            this.lblPrecioEconomica.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPrecioEconomica.Name = "lblPrecioEconomica";
            this.lblPrecioEconomica.Size = new System.Drawing.Size(60, 17);
            this.lblPrecioEconomica.TabIndex = 17;
            this.lblPrecioEconomica.Text = "Económica";
            //
            // lblPrecioEjecutiva
            //
            this.lblPrecioEjecutiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrecioEjecutiva.AutoSize = true;
            this.lblPrecioEjecutiva.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPrecioEjecutiva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPrecioEjecutiva.Location = new System.Drawing.Point(296, 269);
            this.lblPrecioEjecutiva.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPrecioEjecutiva.Name = "lblPrecioEjecutiva";
            this.lblPrecioEjecutiva.Size = new System.Drawing.Size(60, 17);
            this.lblPrecioEjecutiva.TabIndex = 18;
            this.lblPrecioEjecutiva.Text = "Ejecutiva";
            //
            // numPrecioEconomica
            //
            this.numPrecioEconomica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPrecioEconomica.DecimalPlaces = 2;
            this.numPrecioEconomica.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPrecioEconomica.Location = new System.Drawing.Point(0, 293);
            this.numPrecioEconomica.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.numPrecioEconomica.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPrecioEconomica.Name = "numPrecioEconomica";
            this.numPrecioEconomica.Size = new System.Drawing.Size(276, 25);
            this.numPrecioEconomica.TabIndex = 19;
            this.numPrecioEconomica.ThousandsSeparator = true;
            //
            // numPrecioEjecutiva
            //
            this.numPrecioEjecutiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPrecioEjecutiva.DecimalPlaces = 2;
            this.numPrecioEjecutiva.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPrecioEjecutiva.Location = new System.Drawing.Point(296, 293);
            this.numPrecioEjecutiva.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.numPrecioEjecutiva.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPrecioEjecutiva.Name = "numPrecioEjecutiva";
            this.numPrecioEjecutiva.Size = new System.Drawing.Size(276, 25);
            this.numPrecioEjecutiva.TabIndex = 20;
            this.numPrecioEjecutiva.ThousandsSeparator = true;
            //
            // lblPrecioPrimera
            //
            this.lblPrecioPrimera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrecioPrimera.AutoSize = true;
            this.lblPrecioPrimera.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPrecioPrimera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblPrecioPrimera.Location = new System.Drawing.Point(0, 327);
            this.lblPrecioPrimera.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lblPrecioPrimera.Name = "lblPrecioPrimera";
            this.lblPrecioPrimera.Size = new System.Drawing.Size(60, 17);
            this.lblPrecioPrimera.TabIndex = 21;
            this.lblPrecioPrimera.Text = "Primera clase";
            //
            // numPrecioPrimera
            //
            this.numPrecioPrimera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPrecioPrimera.DecimalPlaces = 2;
            this.numPrecioPrimera.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPrecioPrimera.Location = new System.Drawing.Point(0, 351);
            this.numPrecioPrimera.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.numPrecioPrimera.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPrecioPrimera.Name = "numPrecioPrimera";
            this.numPrecioPrimera.Size = new System.Drawing.Size(276, 25);
            this.numPrecioPrimera.TabIndex = 22;
            this.numPrecioPrimera.ThousandsSeparator = true;
            //
            // lblMapa
            //
            this.lblMapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMapa.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblMapa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblMapa.Location = new System.Drawing.Point(296, 324);
            this.lblMapa.Margin = new System.Windows.Forms.Padding(0);
            this.lblMapa.Name = "lblMapa";
            this.lblMapa.Size = new System.Drawing.Size(296, 36);
            this.lblMapa.TabIndex = 23;
            this.lblMapa.Text = "Mapa estándar: 120 asientos.";
            this.lblMapa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flpBotones
            //
            this.flpBotones.Controls.Add(this.btnCrear);
            this.flpBotones.Controls.Add(this.btnCancelar);
            this.flpBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpBotones.Location = new System.Drawing.Point(20, 406);
            this.flpBotones.Name = "flpBotones";
            this.flpBotones.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flpBotones.Size = new System.Drawing.Size(592, 52);
            this.flpBotones.TabIndex = 1;
            this.flpBotones.WrapContents = false;
            //
            // btnCrear
            //
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCrear.Location = new System.Drawing.Point(432, 11);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(160, 38);
            this.btnCrear.TabIndex = 0;
            this.btnCrear.Text = "Crear vuelo";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(292, 11);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 38);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            //
            // FRMAltaVuelo_GV42
            //
            this.AcceptButton = this.btnCrear;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(680, 590);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMAltaVuelo_GV42";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo vuelo";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlTarjeta.ResumeLayout(false);
            this.tlpCampos.ResumeLayout(false);
            this.tlpCampos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioEconomica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioEjecutiva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioPrimera)).EndInit();
            this.flpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjeta;
        private System.Windows.Forms.TableLayoutPanel tlpCampos;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblAerolinea;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtCodigo;
        private System.Windows.Forms.ComboBox cmbAerolinea;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.ComboBox cmbDestino;
        private System.Windows.Forms.Label lblSalida;
        private System.Windows.Forms.Label lblLlegada;
        private System.Windows.Forms.DateTimePicker dtSalida;
        private System.Windows.Forms.DateTimePicker dtLlegada;
        private System.Windows.Forms.Label lblPuerta;
        private System.Windows.Forms.Label lblCosto;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtPuerta;
        private System.Windows.Forms.NumericUpDown numCosto;
        private System.Windows.Forms.Label lblSeccionPrecios;
        private System.Windows.Forms.Label lblPrecioEconomica;
        private System.Windows.Forms.Label lblPrecioEjecutiva;
        private System.Windows.Forms.NumericUpDown numPrecioEconomica;
        private System.Windows.Forms.NumericUpDown numPrecioEjecutiva;
        private System.Windows.Forms.Label lblPrecioPrimera;
        private System.Windows.Forms.NumericUpDown numPrecioPrimera;
        private System.Windows.Forms.Label lblMapa;
        private System.Windows.Forms.FlowLayoutPanel flpBotones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCrear;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
    }
}
