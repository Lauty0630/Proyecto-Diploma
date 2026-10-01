namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMConsultarReservas_GV42
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
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.pnlSeparadorAbajo = new System.Windows.Forms.Panel();
            this.pnlAcciones = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.pnlSeparadorArriba = new System.Windows.Forms.Panel();
            this.pnlBusqueda = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBusqueda = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.btnBuscar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.lblAyudaBusqueda = new System.Windows.Forms.Label();
            this.dgvReservas = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colReserva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAyudaAcciones = new System.Windows.Forms.Label();
            this.btnVerBoletos = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCheckIn = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlGrilla.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
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
            this.lblTitulo.Size = new System.Drawing.Size(252, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Consultar reservas";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(357, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Buscá una reserva para ver sus boletos o cancelarla";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.pnlGrilla);
            this.pnlContenido.Controls.Add(this.pnlSeparadorAbajo);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlSeparadorArriba);
            this.pnlContenido.Controls.Add(this.pnlBusqueda);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 1;
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.Controls.Add(this.dgvReservas);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Location = new System.Drawing.Point(24, 128);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(12, 12, 12, 14);
            this.pnlGrilla.Size = new System.Drawing.Size(952, 334);
            this.pnlGrilla.TabIndex = 2;
            // 
            // pnlSeparadorAbajo
            // 
            this.pnlSeparadorAbajo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSeparadorAbajo.Location = new System.Drawing.Point(24, 462);
            this.pnlSeparadorAbajo.Name = "pnlSeparadorAbajo";
            this.pnlSeparadorAbajo.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparadorAbajo.TabIndex = 3;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnCheckIn);
            this.pnlAcciones.Controls.Add(this.lblAyudaAcciones);
            this.pnlAcciones.Controls.Add(this.btnVerBoletos);
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(24, 478);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(952, 72);
            this.pnlAcciones.TabIndex = 4;
            // 
            // pnlSeparadorArriba
            // 
            this.pnlSeparadorArriba.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparadorArriba.Location = new System.Drawing.Point(24, 112);
            this.pnlSeparadorArriba.Name = "pnlSeparadorArriba";
            this.pnlSeparadorArriba.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparadorArriba.TabIndex = 1;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.lblBuscar);
            this.pnlBusqueda.Controls.Add(this.txtBusqueda);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.lblAyudaBusqueda);
            this.pnlBusqueda.AcentoSuperior = true;
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(24, 20);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(952, 92);
            this.pnlBusqueda.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblBuscar.Location = new System.Drawing.Point(24, 18);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(360, 17);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar por número, DNI o apellido del cliente";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusqueda.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtBusqueda.Location = new System.Drawing.Point(24, 42);
            this.txtBusqueda.MaxLength = 60;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(340, 26);
            this.txtBusqueda.TabIndex = 1;
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Location = new System.Drawing.Point(376, 38);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(110, 34);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblAyudaBusqueda
            // 
            this.lblAyudaBusqueda.AutoSize = true;
            this.lblAyudaBusqueda.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyudaBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyudaBusqueda.Location = new System.Drawing.Point(500, 47);
            this.lblAyudaBusqueda.Name = "lblAyudaBusqueda";
            this.lblAyudaBusqueda.Size = new System.Drawing.Size(258, 15);
            this.lblAyudaBusqueda.TabIndex = 3;
            this.lblAyudaBusqueda.Text = "Sin texto se muestran las últimas reservas.";
            // 
            // dgvReservas
            // 
            this.dgvReservas.AllowUserToAddRows = false;
            this.dgvReservas.AllowUserToDeleteRows = false;
            this.dgvReservas.AllowUserToResizeRows = false;
            this.dgvReservas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservas.BackgroundColor = System.Drawing.Color.White;
            this.dgvReservas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReservas.ColumnHeadersHeight = 38;
            this.dgvReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReservas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReserva,
            this.colDni,
            this.colCliente,
            this.colVuelo,
            this.colRuta,
            this.colSalida,
            this.colClase,
            this.colEstado,
            this.colImporte});
            this.dgvReservas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReservas.Location = new System.Drawing.Point(12, 12);
            this.dgvReservas.MultiSelect = false;
            this.dgvReservas.Name = "dgvReservas";
            this.dgvReservas.ReadOnly = true;
            this.dgvReservas.RowHeadersVisible = false;
            this.dgvReservas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvReservas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservas.Size = new System.Drawing.Size(928, 308);
            this.dgvReservas.TabIndex = 0;
            this.dgvReservas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReservas_CellFormatting);
            this.dgvReservas.SelectionChanged += new System.EventHandler(this.dgvReservas_SelectionChanged);
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            // 
            // colReserva
            // 
            this.colReserva.DataPropertyName = "NumeroReserva";
            this.colReserva.FillWeight = 90F;
            this.colReserva.HeaderText = "Reserva";
            this.colReserva.Name = "colReserva";
            this.colReserva.ReadOnly = true;
            // 
            // colDni
            // 
            this.colDni.DataPropertyName = "DniCliente";
            this.colDni.FillWeight = 80F;
            this.colDni.HeaderText = "DNI cliente";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            // 
            // colCliente
            // 
            this.colCliente.DataPropertyName = "Cliente";
            this.colCliente.FillWeight = 130F;
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.Name = "colCliente";
            this.colCliente.ReadOnly = true;
            // 
            // colVuelo
            // 
            this.colVuelo.DataPropertyName = "Vuelo";
            this.colVuelo.FillWeight = 70F;
            this.colVuelo.HeaderText = "Vuelo";
            this.colVuelo.Name = "colVuelo";
            this.colVuelo.ReadOnly = true;
            // 
            // colRuta
            // 
            this.colRuta.DataPropertyName = "Ruta";
            this.colRuta.FillWeight = 180F;
            this.colRuta.HeaderText = "Ruta";
            this.colRuta.Name = "colRuta";
            this.colRuta.ReadOnly = true;
            // 
            // colSalida
            // 
            this.colSalida.DataPropertyName = "Salida";
            this.colSalida.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSalida.FillWeight = 100F;
            this.colSalida.HeaderText = "Salida";
            this.colSalida.Name = "colSalida";
            this.colSalida.ReadOnly = true;
            // 
            // colClase
            // 
            this.colClase.DataPropertyName = "Clase";
            this.colClase.FillWeight = 80F;
            this.colClase.HeaderText = "Clase";
            this.colClase.Name = "colClase";
            this.colClase.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.FillWeight = 100F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // colImporte
            // 
            this.colImporte.DataPropertyName = "ImporteTotal";
            this.colImporte.DefaultCellStyle = dataGridViewCellStyle2;
            this.colImporte.FillWeight = 90F;
            this.colImporte.HeaderText = "Importe";
            this.colImporte.Name = "colImporte";
            this.colImporte.ReadOnly = true;
            // 
            // lblAyudaAcciones
            // 
            this.lblAyudaAcciones.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Italic);
            this.lblAyudaAcciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblAyudaAcciones.Location = new System.Drawing.Point(24, 15);
            this.lblAyudaAcciones.Name = "lblAyudaAcciones";
            this.lblAyudaAcciones.Size = new System.Drawing.Size(330, 40);
            this.lblAyudaAcciones.TabIndex = 0;
            this.lblAyudaAcciones.Text = "Seleccioná una reserva para ver sus boletos o cancelarla.";
            this.lblAyudaAcciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVerBoletos
            // 
            this.btnVerBoletos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerBoletos.FlatAppearance.BorderSize = 0;
            this.btnVerBoletos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerBoletos.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnVerBoletos.Location = new System.Drawing.Point(528, 15);
            this.btnVerBoletos.Name = "btnVerBoletos";
            this.btnVerBoletos.Size = new System.Drawing.Size(150, 40);
            this.btnVerBoletos.TabIndex = 1;
            this.btnVerBoletos.Text = "Ver boletos";
            this.btnVerBoletos.UseVisualStyleBackColor = false;
            this.btnVerBoletos.Click += new System.EventHandler(this.btnVerBoletos_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Peligro;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(688, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(240, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar reserva seleccionada";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckIn.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Exito;
            this.btnCheckIn.FlatAppearance.BorderSize = 0;
            this.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.Location = new System.Drawing.Point(368, 15);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(150, 40);
            this.btnCheckIn.TabIndex = 3;
            this.btnCheckIn.Text = "Hacer check-in";
            this.toolTip.SetToolTip(this.btnCheckIn, "El check-in se habilita de 48 hs a 60 minutos antes de la salida (reservas confirmadas).");
            this.btnCheckIn.UseVisualStyleBackColor = false;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // FRMConsultarReservas_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMConsultarReservas_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultar reservas";
            this.Load += new System.EventHandler(this.FRMConsultarReservas_GV42_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlGrilla.ResumeLayout(false);
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlGrilla;
        private System.Windows.Forms.Panel pnlSeparadorAbajo;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlAcciones;
        private System.Windows.Forms.Panel pnlSeparadorArriba;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlBusqueda;
        private System.Windows.Forms.Label lblBuscar;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtBusqueda;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnBuscar;
        private System.Windows.Forms.Label lblAyudaBusqueda;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvReservas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReserva;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImporte;
        private System.Windows.Forms.Label lblAyudaAcciones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnVerBoletos;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCheckIn;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
