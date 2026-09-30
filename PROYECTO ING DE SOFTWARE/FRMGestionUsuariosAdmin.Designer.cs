namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMGestionUsuariosAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnActivarDesactivar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.rbActivos = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBloqueado = new System.Windows.Forms.TextBox();
            this.txtActivo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.components = new System.ComponentModel.Container();
            this.toolTipAyuda = new System.Windows.Forms.ToolTip(this.components);
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblTituloMensaje = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.lblSeparadorSerializacion = new System.Windows.Forms.Label();
            this.lblTituloSerializacion = new System.Windows.Forms.Label();
            this.btnSerializar = new System.Windows.Forms.Button();
            this.txtRutaSerializar = new System.Windows.Forms.TextBox();
            this.btnUbicacionSerializar = new System.Windows.Forms.Button();
            this.btnDeserializar = new System.Windows.Forms.Button();
            this.txtRutaDeserializar = new System.Windows.Forms.TextBox();
            this.btnUbicacionDeserializar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.dgvUsuarios.Location = new System.Drawing.Point(66, 30);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(495, 150);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.SelectionChanged += new System.EventHandler(this.dgvUsuarios_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label1.Location = new System.Drawing.Point(63, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label2.Location = new System.Drawing.Point(63, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Apellido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label3.Location = new System.Drawing.Point(63, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nombre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label4.Location = new System.Drawing.Point(63, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label5.Location = new System.Drawing.Point(63, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Rol";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label6.Location = new System.Drawing.Point(63, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "UserName";
            // 
            // txtDni
            // 
            this.txtDni.BackColor = System.Drawing.Color.White;
            this.txtDni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDni.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtDni.Location = new System.Drawing.Point(132, 226);
            this.txtDni.MaxLength = 8;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(100, 24);
            this.txtDni.TabIndex = 9;
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.Color.White;
            this.txtApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtApellido.Location = new System.Drawing.Point(132, 259);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(100, 24);
            this.txtApellido.TabIndex = 10;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtNombre.Location = new System.Drawing.Point(132, 292);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 24);
            this.txtNombre.TabIndex = 11;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtEmail.Location = new System.Drawing.Point(132, 325);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 24);
            this.txtEmail.TabIndex = 12;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtUser.Location = new System.Drawing.Point(132, 391);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 24);
            this.txtUser.TabIndex = 14;
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnCrear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCrear.ForeColor = System.Drawing.Color.White;
            this.btnCrear.Location = new System.Drawing.Point(724, 30);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(103, 37);
            this.btnCrear.TabIndex = 17;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnDesbloquear
            // 
            this.btnDesbloquear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnDesbloquear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesbloquear.FlatAppearance.BorderSize = 0;
            this.btnDesbloquear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnDesbloquear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnDesbloquear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesbloquear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDesbloquear.ForeColor = System.Drawing.Color.White;
            this.btnDesbloquear.Location = new System.Drawing.Point(724, 87);
            this.btnDesbloquear.Name = "btnDesbloquear";
            this.btnDesbloquear.Size = new System.Drawing.Size(103, 37);
            this.btnDesbloquear.TabIndex = 18;
            this.btnDesbloquear.Text = "Desbloquear";
            this.btnDesbloquear.UseVisualStyleBackColor = false;
            this.btnDesbloquear.Click += new System.EventHandler(this.btnDesbloquear_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnModificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(724, 143);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(103, 37);
            this.btnModificar.TabIndex = 19;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnActivarDesactivar
            // 
            this.btnActivarDesactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnActivarDesactivar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActivarDesactivar.FlatAppearance.BorderSize = 0;
            this.btnActivarDesactivar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnActivarDesactivar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnActivarDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivarDesactivar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnActivarDesactivar.ForeColor = System.Drawing.Color.White;
            this.btnActivarDesactivar.Location = new System.Drawing.Point(724, 205);
            this.btnActivarDesactivar.Name = "btnActivarDesactivar";
            this.btnActivarDesactivar.Size = new System.Drawing.Size(110, 37);
            this.btnActivarDesactivar.TabIndex = 20;
            this.btnActivarDesactivar.Text = "Activar/Desactivar";
            this.btnActivarDesactivar.UseVisualStyleBackColor = false;
            this.btnActivarDesactivar.Click += new System.EventHandler(this.btnActivarDesactivar_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(56)))), ((int)(((byte)(130)))));
            this.btnAplicar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(724, 266);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(103, 37);
            this.btnAplicar.TabIndex = 21;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnCancelar.Location = new System.Drawing.Point(724, 331);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 37);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnSalir.Location = new System.Drawing.Point(724, 398);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(103, 37);
            this.btnSalir.TabIndex = 23;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // rbActivos
            // 
            this.rbActivos.AutoSize = true;
            this.rbActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbActivos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbActivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.rbActivos.Location = new System.Drawing.Point(368, 229);
            this.rbActivos.Name = "rbActivos";
            this.rbActivos.Size = new System.Drawing.Size(67, 21);
            this.rbActivos.TabIndex = 24;
            this.rbActivos.TabStop = true;
            this.rbActivos.Text = "Activos";
            this.rbActivos.UseVisualStyleBackColor = true;
            this.rbActivos.CheckedChanged += new System.EventHandler(this.rbActivos_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTodos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rbTodos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.rbTodos.Location = new System.Drawing.Point(368, 259);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(62, 21);
            this.rbTodos.TabIndex = 25;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label7.Location = new System.Drawing.Point(63, 424);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Bloqueado";
            // 
            // txtBloqueado
            // 
            this.txtBloqueado.BackColor = System.Drawing.Color.White;
            this.txtBloqueado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBloqueado.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtBloqueado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtBloqueado.Location = new System.Drawing.Point(132, 424);
            this.txtBloqueado.Name = "txtBloqueado";
            this.txtBloqueado.Size = new System.Drawing.Size(100, 24);
            this.txtBloqueado.TabIndex = 27;
            // 
            // txtActivo
            // 
            this.txtActivo.BackColor = System.Drawing.Color.White;
            this.txtActivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActivo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtActivo.Location = new System.Drawing.Point(132, 457);
            this.txtActivo.Name = "txtActivo";
            this.txtActivo.Size = new System.Drawing.Size(100, 24);
            this.txtActivo.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label8.Location = new System.Drawing.Point(63, 457);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "Activo";
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblMensaje.Location = new System.Drawing.Point(368, 324);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(44, 17);
            this.lblMensaje.TabIndex = 30;
            this.lblMensaje.Text = "Modo";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(132, 358);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 31;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(580, 30);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 37);
            this.btnActualizar.TabIndex = 32;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblTituloMensaje
            // 
            this.lblTituloMensaje.AutoSize = true;
            this.lblTituloMensaje.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTituloMensaje.Location = new System.Drawing.Point(368, 356);
            this.lblTituloMensaje.Name = "lblTituloMensaje";
            this.lblTituloMensaje.Size = new System.Drawing.Size(62, 17);
            this.lblTituloMensaje.TabIndex = 33;
            this.lblTituloMensaje.Text = "Mensaje:";
            // 
            // txtMensaje
            // 
            this.txtMensaje.BackColor = System.Drawing.Color.White;
            this.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensaje.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtMensaje.Multiline = true;
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensaje.Location = new System.Drawing.Point(368, 378);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(322, 103);
            this.txtMensaje.TabIndex = 34;
            // 
            // lblSeparadorSerializacion
            // 
            this.lblSeparadorSerializacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparadorSerializacion.Location = new System.Drawing.Point(66, 500);
            this.lblSeparadorSerializacion.Name = "lblSeparadorSerializacion";
            this.lblSeparadorSerializacion.Size = new System.Drawing.Size(768, 2);
            this.lblSeparadorSerializacion.TabIndex = 35;
            // 
            // lblTituloSerializacion
            // 
            this.lblTituloSerializacion.AutoSize = true;
            this.lblTituloSerializacion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloSerializacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTituloSerializacion.Location = new System.Drawing.Point(63, 510);
            this.lblTituloSerializacion.Name = "lblTituloSerializacion";
            this.lblTituloSerializacion.Size = new System.Drawing.Size(128, 17);
            this.lblTituloSerializacion.TabIndex = 36;
            this.lblTituloSerializacion.Text = "Serialización XML";
            // 
            // btnSerializar
            // 
            this.btnSerializar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.btnSerializar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSerializar.FlatAppearance.BorderSize = 0;
            this.btnSerializar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.btnSerializar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.btnSerializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerializar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSerializar.ForeColor = System.Drawing.Color.White;
            this.btnSerializar.Location = new System.Drawing.Point(66, 538);
            this.btnSerializar.Name = "btnSerializar";
            this.btnSerializar.Size = new System.Drawing.Size(150, 37);
            this.btnSerializar.TabIndex = 37;
            this.btnSerializar.Text = "SERIALIZAR";
            this.btnSerializar.UseVisualStyleBackColor = false;
            this.btnSerializar.Click += new System.EventHandler(this.btnSerializar_Click);
            // 
            // txtRutaSerializar
            // 
            this.txtRutaSerializar.BackColor = System.Drawing.Color.White;
            this.txtRutaSerializar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutaSerializar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRutaSerializar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtRutaSerializar.ReadOnly = true;
            this.txtRutaSerializar.Location = new System.Drawing.Point(66, 585);
            this.txtRutaSerializar.Name = "txtRutaSerializar";
            this.txtRutaSerializar.Size = new System.Drawing.Size(262, 23);
            this.txtRutaSerializar.TabIndex = 38;
            // 
            // btnUbicacionSerializar
            // 
            this.btnUbicacionSerializar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(130)))));
            this.btnUbicacionSerializar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacionSerializar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnUbicacionSerializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbicacionSerializar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUbicacionSerializar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnUbicacionSerializar.Location = new System.Drawing.Point(332, 584);
            this.btnUbicacionSerializar.Name = "btnUbicacionSerializar";
            this.btnUbicacionSerializar.Size = new System.Drawing.Size(38, 25);
            this.btnUbicacionSerializar.TabIndex = 39;
            this.btnUbicacionSerializar.Text = "...";
            this.btnUbicacionSerializar.UseVisualStyleBackColor = false;
            this.btnUbicacionSerializar.Click += new System.EventHandler(this.btnUbicacionSerializar_Click);
            // 
            // btnDeserializar
            // 
            this.btnDeserializar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.btnDeserializar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeserializar.FlatAppearance.BorderSize = 0;
            this.btnDeserializar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.btnDeserializar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.btnDeserializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeserializar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDeserializar.ForeColor = System.Drawing.Color.White;
            this.btnDeserializar.Location = new System.Drawing.Point(412, 538);
            this.btnDeserializar.Name = "btnDeserializar";
            this.btnDeserializar.Size = new System.Drawing.Size(150, 37);
            this.btnDeserializar.TabIndex = 40;
            this.btnDeserializar.Text = "DES-SERIALIZAR";
            this.btnDeserializar.UseVisualStyleBackColor = false;
            this.btnDeserializar.Click += new System.EventHandler(this.btnDeserializar_Click);
            // 
            // txtRutaDeserializar
            // 
            this.txtRutaDeserializar.BackColor = System.Drawing.Color.White;
            this.txtRutaDeserializar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutaDeserializar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRutaDeserializar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.txtRutaDeserializar.ReadOnly = true;
            this.txtRutaDeserializar.Location = new System.Drawing.Point(412, 585);
            this.txtRutaDeserializar.Name = "txtRutaDeserializar";
            this.txtRutaDeserializar.Size = new System.Drawing.Size(262, 23);
            this.txtRutaDeserializar.TabIndex = 41;
            // 
            // btnUbicacionDeserializar
            // 
            this.btnUbicacionDeserializar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(130)))));
            this.btnUbicacionDeserializar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacionDeserializar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnUbicacionDeserializar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbicacionDeserializar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUbicacionDeserializar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnUbicacionDeserializar.Location = new System.Drawing.Point(678, 584);
            this.btnUbicacionDeserializar.Name = "btnUbicacionDeserializar";
            this.btnUbicacionDeserializar.Size = new System.Drawing.Size(38, 25);
            this.btnUbicacionDeserializar.TabIndex = 42;
            this.btnUbicacionDeserializar.Text = "...";
            this.btnUbicacionDeserializar.UseVisualStyleBackColor = false;
            this.btnUbicacionDeserializar.Click += new System.EventHandler(this.btnUbicacionDeserializar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(724, 538);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(110, 37);
            this.btnLimpiar.TabIndex = 43;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // toolTipAyuda
            // 
            this.toolTipAyuda.AutoPopDelay = 10000;
            this.toolTipAyuda.InitialDelay = 400;
            this.toolTipAyuda.ReshowDelay = 100;
            this.toolTipAyuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // FRMGestionUsuariosAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 630);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnUbicacionDeserializar);
            this.Controls.Add(this.txtRutaDeserializar);
            this.Controls.Add(this.btnDeserializar);
            this.Controls.Add(this.btnUbicacionSerializar);
            this.Controls.Add(this.txtRutaSerializar);
            this.Controls.Add(this.btnSerializar);
            this.Controls.Add(this.lblTituloSerializacion);
            this.Controls.Add(this.lblSeparadorSerializacion);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblTituloMensaje);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtActivo);
            this.Controls.Add(this.txtBloqueado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rbTodos);
            this.Controls.Add(this.rbActivos);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnActivarDesactivar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnDesbloquear);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvUsuarios);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMGestionUsuariosAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRMPrincipalAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnDesbloquear;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnActivarDesactivar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.RadioButton rbActivos;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBloqueado;
        private System.Windows.Forms.TextBox txtActivo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolTip toolTipAyuda;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblTituloMensaje;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Label lblSeparadorSerializacion;
        private System.Windows.Forms.Label lblTituloSerializacion;
        private System.Windows.Forms.Button btnSerializar;
        private System.Windows.Forms.TextBox txtRutaSerializar;
        private System.Windows.Forms.Button btnUbicacionSerializar;
        private System.Windows.Forms.Button btnDeserializar;
        private System.Windows.Forms.TextBox txtRutaDeserializar;
        private System.Windows.Forms.Button btnUbicacionDeserializar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}