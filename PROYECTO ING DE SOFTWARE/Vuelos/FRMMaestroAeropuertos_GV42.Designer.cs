namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMMaestroAeropuertos_GV42
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
            this.pnlEncabezado = new PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDatos = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpDatos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloDatos = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCiudad = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPais = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblTituloMensaje = new System.Windows.Forms.Label();
            this.txtMensaje = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.tlpDerecha = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGrilla = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpGrilla = new System.Windows.Forms.TableLayoutPanel();
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTituloGrilla = new System.Windows.Forms.Label();
            this.dgvAeropuertos = new PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCiudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPais = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBotonera = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCrear = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnModificar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnEliminar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.flpConfirmacion = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalir = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnCancelar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.btnAplicar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.tlpPrincipal.SuspendLayout();
            this.pnlDatos.SuspendLayout();
            this.tlpDatos.SuspendLayout();
            this.tlpDerecha.SuspendLayout();
            this.pnlGrilla.SuspendLayout();
            this.tlpGrilla.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAeropuertos)).BeginInit();
            this.pnlBotonera.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            this.flpConfirmacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 84);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(29, 48);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Alta, modificación y baja de los aeropuertos que usan los vuelos";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Maestro de aeropuertos";
            // 
            // pnlContenido
            // 
            this.pnlContenido.AutoScroll = true;
            this.pnlContenido.AutoScrollMinSize = new System.Drawing.Size(900, 520);
            this.pnlContenido.Controls.Add(this.tlpPrincipal);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 84);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 566);
            this.pnlContenido.TabIndex = 1;
            // 
            // tlpPrincipal
            // 
            this.tlpPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.Controls.Add(this.pnlDatos, 0, 0);
            this.tlpPrincipal.Controls.Add(this.tlpDerecha, 1, 0);
            this.tlpPrincipal.Controls.Add(this.pnlBotonera, 0, 1);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.RowCount = 2;
            this.tlpPrincipal.TabIndex = 0;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tlpPrincipal.SetColumnSpan(this.pnlBotonera, 2);
            // 
            // pnlDatos
            // 
            this.pnlDatos.AcentoSuperior = true;
            this.pnlDatos.BackColor = System.Drawing.Color.White;
            this.pnlDatos.Controls.Add(this.tlpDatos);
            this.pnlDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatos.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.pnlDatos.Name = "pnlDatos";
            this.pnlDatos.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.pnlDatos.TabIndex = 1;
            // 
            // tlpDatos
            // 
            this.tlpDatos.BackColor = System.Drawing.Color.White;
            this.tlpDatos.ColumnCount = 4;
            this.tlpDatos.Controls.Add(this.lblTituloDatos, 0, 0);
            this.tlpDatos.Controls.Add(this.lblMensaje, 0, 1);
            this.tlpDatos.Controls.Add(this.label1, 0, 2);
            this.tlpDatos.Controls.Add(this.txtCodigo, 1, 2);
            this.tlpDatos.Controls.Add(this.label2, 0, 3);
            this.tlpDatos.Controls.Add(this.txtNombre, 1, 3);
            this.tlpDatos.Controls.Add(this.label3, 0, 4);
            this.tlpDatos.Controls.Add(this.txtCiudad, 1, 4);
            this.tlpDatos.Controls.Add(this.label4, 0, 5);
            this.tlpDatos.Controls.Add(this.txtPais, 1, 5);
            this.tlpDatos.Controls.Add(this.lblTituloMensaje, 0, 6);
            this.tlpDatos.Controls.Add(this.txtMensaje, 0, 7);
            this.tlpDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDatos.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDatos.Name = "tlpDatos";
            this.tlpDatos.RowCount = 8;
            this.tlpDatos.TabIndex = 0;
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDatos.SetColumnSpan(this.lblTituloDatos, 4);
            this.tlpDatos.SetColumnSpan(this.lblMensaje, 4);
            this.tlpDatos.SetColumnSpan(this.txtCodigo, 3);
            this.tlpDatos.SetColumnSpan(this.txtNombre, 3);
            this.tlpDatos.SetColumnSpan(this.txtCiudad, 3);
            this.tlpDatos.SetColumnSpan(this.txtPais, 3);
            this.tlpDatos.SetColumnSpan(this.lblTituloMensaje, 4);
            this.tlpDatos.SetColumnSpan(this.txtMensaje, 4);
            // 
            // lblTituloDatos
            // 
            this.lblTituloDatos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTituloDatos.AutoSize = true;
            this.lblTituloDatos.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTituloDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTituloDatos.Margin = new System.Windows.Forms.Padding(0);
            this.lblTituloDatos.Name = "lblTituloDatos";
            this.lblTituloDatos.TabIndex = 0;
            this.lblTituloDatos.Text = "Datos del aeropuerto";
            // 
            // lblMensaje
            // 
            this.lblMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.lblMensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblMensaje.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "Modo Consulta";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "Código IATA";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtCodigo.MaxLength = 8;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(200, 26);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 26);
            this.txtNombre.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.TabIndex = 6;
            this.label3.Text = "Ciudad";
            // 
            // txtCiudad
            // 
            this.txtCiudad.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCiudad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCiudad.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCiudad.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtCiudad.MaxLength = 60;
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(200, 26);
            this.txtCiudad.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.TabIndex = 8;
            this.label4.Text = "País";
            // 
            // txtPais
            // 
            this.txtPais.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtPais.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPais.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPais.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtPais.MaxLength = 100;
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(200, 26);
            this.txtPais.TabIndex = 9;
            // 
            // lblTituloMensaje
            // 
            this.lblTituloMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTituloMensaje.AutoSize = true;
            this.lblTituloMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTituloMensaje.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblTituloMensaje.Name = "lblTituloMensaje";
            this.lblTituloMensaje.TabIndex = 18;
            this.lblTituloMensaje.Text = "Mensaje:";
            // 
            // txtMensaje
            // 
            this.txtMensaje.BackColor = System.Drawing.Color.White;
            this.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMensaje.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMensaje.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensaje.Size = new System.Drawing.Size(300, 60);
            this.txtMensaje.TabIndex = 19;
            this.txtMensaje.TabStop = false;
            // 
            // tlpDerecha
            // 
            this.tlpDerecha.BackColor = System.Drawing.Color.Transparent;
            this.tlpDerecha.ColumnCount = 1;
            this.tlpDerecha.Controls.Add(this.pnlGrilla, 0, 0);
            this.tlpDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDerecha.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tlpDerecha.Name = "tlpDerecha";
            this.tlpDerecha.RowCount = 1;
            this.tlpDerecha.TabIndex = 0;
            this.tlpDerecha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDerecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            // 
            // pnlGrilla
            // 
            this.pnlGrilla.BackColor = System.Drawing.Color.White;
            this.pnlGrilla.Controls.Add(this.tlpGrilla);
            this.pnlGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrilla.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlGrilla.Name = "pnlGrilla";
            this.pnlGrilla.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlGrilla.TabIndex = 0;
            // 
            // tlpGrilla
            // 
            this.tlpGrilla.BackColor = System.Drawing.Color.White;
            this.tlpGrilla.ColumnCount = 1;
            this.tlpGrilla.Controls.Add(this.flpFiltros, 0, 0);
            this.tlpGrilla.Controls.Add(this.dgvAeropuertos, 0, 1);
            this.tlpGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGrilla.Margin = new System.Windows.Forms.Padding(0);
            this.tlpGrilla.Name = "tlpGrilla";
            this.tlpGrilla.RowCount = 2;
            this.tlpGrilla.TabIndex = 0;
            this.tlpGrilla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGrilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpGrilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            // 
            // flpFiltros
            // 
            this.flpFiltros.BackColor = System.Drawing.Color.White;
            this.flpFiltros.Controls.Add(this.lblTituloGrilla);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiltros.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.TabIndex = 1;
            this.flpFiltros.WrapContents = false;
            // 
            // lblTituloGrilla
            // 
            this.lblTituloGrilla.AutoSize = true;
            this.lblTituloGrilla.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblTituloGrilla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTituloGrilla.Margin = new System.Windows.Forms.Padding(0, 6, 24, 0);
            this.lblTituloGrilla.Name = "lblTituloGrilla";
            this.lblTituloGrilla.TabIndex = 0;
            this.lblTituloGrilla.Text = "Aeropuertos";
            // 
            // dgvAeropuertos
            // 
            this.dgvAeropuertos.AllowUserToAddRows = false;
            this.dgvAeropuertos.AllowUserToDeleteRows = false;
            this.dgvAeropuertos.AllowUserToOrderColumns = false;
            this.dgvAeropuertos.AllowUserToResizeColumns = false;
            this.dgvAeropuertos.AllowUserToResizeRows = false;
            this.dgvAeropuertos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAeropuertos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAeropuertos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAeropuertos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAeropuertos.ColumnHeadersHeight = 38;
            this.dgvAeropuertos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAeropuertos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colNombre,
            this.colCiudad,
            this.colPais});
            this.dgvAeropuertos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAeropuertos.EnableHeadersVisualStyles = false;
            this.dgvAeropuertos.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.dgvAeropuertos.MultiSelect = false;
            this.dgvAeropuertos.Name = "dgvAeropuertos";
            this.dgvAeropuertos.ReadOnly = true;
            this.dgvAeropuertos.RowHeadersVisible = false;
            this.dgvAeropuertos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAeropuertos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAeropuertos.Size = new System.Drawing.Size(500, 200);
            this.dgvAeropuertos.TabIndex = 0;
            this.dgvAeropuertos.SelectionChanged += new System.EventHandler(this.dgvAeropuertos_SelectionChanged);
            // 
            // colCodigo
            // 
            this.colCodigo.DataPropertyName = "CodigoIata";
            this.colCodigo.FillWeight = 60F;
            this.colCodigo.HeaderText = "Código IATA";
            this.colCodigo.MinimumWidth = 84;
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.FillWeight = 200F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 70;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            // 
            // colCiudad
            // 
            this.colCiudad.DataPropertyName = "Ciudad";
            this.colCiudad.FillWeight = 100F;
            this.colCiudad.HeaderText = "Ciudad";
            this.colCiudad.MinimumWidth = 70;
            this.colCiudad.Name = "colCiudad";
            this.colCiudad.ReadOnly = true;
            // 
            // colPais
            // 
            this.colPais.DataPropertyName = "Pais";
            this.colPais.FillWeight = 100F;
            this.colPais.HeaderText = "País";
            this.colPais.MinimumWidth = 90;
            this.colPais.Name = "colPais";
            this.colPais.ReadOnly = true;
            // 
            // pnlBotonera
            // 
            this.pnlBotonera.BackColor = System.Drawing.Color.White;
            this.pnlBotonera.Controls.Add(this.flpAcciones);
            this.pnlBotonera.Controls.Add(this.flpConfirmacion);
            this.pnlBotonera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBotonera.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlBotonera.Name = "pnlBotonera";
            this.pnlBotonera.Padding = new System.Windows.Forms.Padding(12, 14, 12, 14);
            this.pnlBotonera.TabIndex = 2;
            // 
            // flpAcciones
            // 
            this.flpAcciones.BackColor = System.Drawing.Color.White;
            this.flpAcciones.Controls.Add(this.btnCrear);
            this.flpAcciones.Controls.Add(this.btnModificar);
            this.flpAcciones.Controls.Add(this.btnEliminar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpAcciones.Margin = new System.Windows.Forms.Padding(0);
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.TabIndex = 0;
            this.flpAcciones.WrapContents = false;
            // 
            // btnCrear
            // 
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(92, 40);
            this.btnCrear.TabIndex = 0;
            this.btnCrear.Text = "Añadir";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(104, 40);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Peligro;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(104, 40);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // flpConfirmacion
            // 
            this.flpConfirmacion.BackColor = System.Drawing.Color.White;
            this.flpConfirmacion.Controls.Add(this.btnSalir);
            this.flpConfirmacion.Controls.Add(this.btnCancelar);
            this.flpConfirmacion.Controls.Add(this.btnAplicar);
            this.flpConfirmacion.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpConfirmacion.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpConfirmacion.Margin = new System.Windows.Forms.Padding(0);
            this.flpConfirmacion.Name = "flpConfirmacion";
            this.flpConfirmacion.Size = new System.Drawing.Size(312, 40);
            this.flpConfirmacion.TabIndex = 1;
            this.flpConfirmacion.WrapContents = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 40);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(100, 40);
            this.btnAplicar.TabIndex = 0;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // FRMMaestroAeropuertos_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMMaestroAeropuertos_GV42";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maestro de aeropuertos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRMMaestroAeropuertos_GV42_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            this.pnlDatos.ResumeLayout(false);
            this.pnlDatos.PerformLayout();
            this.tlpDatos.ResumeLayout(false);
            this.tlpDatos.PerformLayout();
            this.tlpDerecha.ResumeLayout(false);
            this.tlpDerecha.PerformLayout();
            this.pnlGrilla.ResumeLayout(false);
            this.pnlGrilla.PerformLayout();
            this.tlpGrilla.ResumeLayout(false);
            this.tlpGrilla.PerformLayout();
            this.flpFiltros.ResumeLayout(false);
            this.flpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAeropuertos)).EndInit();
            this.pnlBotonera.ResumeLayout(false);
            this.pnlBotonera.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.flpAcciones.PerformLayout();
            this.flpConfirmacion.ResumeLayout(false);
            this.flpConfirmacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlDatos;
        private System.Windows.Forms.TableLayoutPanel tlpDatos;
        private System.Windows.Forms.Label lblTituloDatos;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label label1;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtCodigo;
        private System.Windows.Forms.Label label2;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtNombre;
        private System.Windows.Forms.Label label3;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtCiudad;
        private System.Windows.Forms.Label label4;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtPais;
        private System.Windows.Forms.Label lblTituloMensaje;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtMensaje;
        private System.Windows.Forms.TableLayoutPanel tlpDerecha;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlGrilla;
        private System.Windows.Forms.TableLayoutPanel tlpGrilla;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Label lblTituloGrilla;
        private PROYECTO_ING_DE_SOFTWARE.GrillaModerna_GV42 dgvAeropuertos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCiudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPais;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlBotonera;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCrear;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnModificar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnEliminar;
        private System.Windows.Forms.FlowLayoutPanel flpConfirmacion;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnSalir;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCancelar;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnAplicar;
    }
}
