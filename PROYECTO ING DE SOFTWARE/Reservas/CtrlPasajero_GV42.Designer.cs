namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class CtrlPasajero_GV42
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

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTarjeta = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.tlpCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtDni = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.txtNombre = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.txtApellido = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.txtEmail = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.txtTelefono = new PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42();
            this.lblNacimiento = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblAsistencia = new System.Windows.Forms.Label();
            this.dtNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblTipoValor = new System.Windows.Forms.Label();
            this.cmbAsistencia = new System.Windows.Forms.ComboBox();
            this.pnlTarjeta.SuspendLayout();
            this.tlpCampos.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.Controls.Add(this.tlpCampos);
            this.pnlTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjeta.Location = new System.Drawing.Point(0, 0);
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.Padding = new System.Windows.Forms.Padding(16, 8, 16, 10);
            this.pnlTarjeta.Radio = 10;
            this.pnlTarjeta.Size = new System.Drawing.Size(900, 150);
            this.pnlTarjeta.TabIndex = 0;
            // 
            // tlpCampos
            // 
            this.tlpCampos.Controls.Add(this.lblTitulo, 0, 0);
            this.tlpCampos.Controls.Add(this.lblDni, 0, 1);
            this.tlpCampos.Controls.Add(this.lblNombre, 1, 1);
            this.tlpCampos.Controls.Add(this.lblApellido, 2, 1);
            this.tlpCampos.Controls.Add(this.lblEmail, 3, 1);
            this.tlpCampos.Controls.Add(this.lblTelefono, 4, 1);
            this.tlpCampos.Controls.Add(this.txtDni, 0, 2);
            this.tlpCampos.Controls.Add(this.txtNombre, 1, 2);
            this.tlpCampos.Controls.Add(this.txtApellido, 2, 2);
            this.tlpCampos.Controls.Add(this.txtEmail, 3, 2);
            this.tlpCampos.Controls.Add(this.txtTelefono, 4, 2);
            this.tlpCampos.Controls.Add(this.lblNacimiento, 0, 3);
            this.tlpCampos.Controls.Add(this.lblTipo, 2, 3);
            this.tlpCampos.Controls.Add(this.lblAsistencia, 3, 3);
            this.tlpCampos.Controls.Add(this.dtNacimiento, 0, 4);
            this.tlpCampos.Controls.Add(this.lblTipoValor, 2, 4);
            this.tlpCampos.Controls.Add(this.cmbAsistencia, 3, 4);
            this.tlpCampos.ColumnCount = 5;
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tlpCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCampos.Location = new System.Drawing.Point(16, 8);
            this.tlpCampos.Name = "tlpCampos";
            this.tlpCampos.RowCount = 5;
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpCampos.SetColumnSpan(this.lblTitulo, 5);
            this.tlpCampos.SetColumnSpan(this.lblNacimiento, 2);
            this.tlpCampos.SetColumnSpan(this.dtNacimiento, 2);
            this.tlpCampos.SetColumnSpan(this.lblAsistencia, 2);
            this.tlpCampos.SetColumnSpan(this.cmbAsistencia, 2);
            this.tlpCampos.Size = new System.Drawing.Size(868, 132);
            this.tlpCampos.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(868, 26);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Pasajero 1";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDni
            // 
            this.lblDni.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblDni.AutoSize = true;
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblDni.Location = new System.Drawing.Point(0, 28);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(60, 15);
            this.lblDni.TabIndex = 1;
            this.lblDni.Text = "DNI";
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblNombre.Location = new System.Drawing.Point(130, 28);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(60, 15);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre";
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblApellido.Location = new System.Drawing.Point(303, 28);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(60, 15);
            this.lblApellido.TabIndex = 3;
            this.lblApellido.Text = "Apellido";
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblEmail.Location = new System.Drawing.Point(476, 28);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(60, 15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // lblTelefono
            // 
            this.lblTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTelefono.Location = new System.Drawing.Point(710, 28);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(60, 15);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Teléfono";
            // 
            // txtDni
            // 
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDni.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtDni.Location = new System.Drawing.Point(0, 49);
            this.txtDni.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.txtDni.MaxLength = 8;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(118, 26);
            this.txtDni.TabIndex = 6;
            this.txtDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDni_KeyPress);
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNombre.Location = new System.Drawing.Point(130, 49);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(161, 26);
            this.txtNombre.TabIndex = 7;
            // 
            // txtApellido
            // 
            this.txtApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtApellido.Location = new System.Drawing.Point(303, 49);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.txtApellido.MaxLength = 60;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(161, 26);
            this.txtApellido.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtEmail.Location = new System.Drawing.Point(476, 49);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(222, 26);
            this.txtEmail.TabIndex = 9;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTelefono.Location = new System.Drawing.Point(710, 49);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(146, 26);
            this.txtTelefono.TabIndex = 10;
            // 
            // lblNacimiento
            // 
            this.lblNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblNacimiento.AutoSize = true;
            this.lblNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblNacimiento.Location = new System.Drawing.Point(0, 84);
            this.lblNacimiento.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblNacimiento.Name = "lblNacimiento";
            this.lblNacimiento.Size = new System.Drawing.Size(120, 15);
            this.lblNacimiento.TabIndex = 11;
            this.lblNacimiento.Text = "Fecha de nacimiento";
            // 
            // lblTipo
            // 
            this.lblTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblTipo.Location = new System.Drawing.Point(303, 84);
            this.lblTipo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(100, 15);
            this.lblTipo.TabIndex = 12;
            this.lblTipo.Text = "Tipo de pasajero";
            // 
            // lblAsistencia
            // 
            this.lblAsistencia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblAsistencia.AutoSize = true;
            this.lblAsistencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAsistencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblAsistencia.Location = new System.Drawing.Point(476, 84);
            this.lblAsistencia.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
            this.lblAsistencia.Name = "lblAsistencia";
            this.lblAsistencia.Size = new System.Drawing.Size(120, 15);
            this.lblAsistencia.TabIndex = 13;
            this.lblAsistencia.Text = "Asistencia especial";
            // 
            // dtNacimiento
            // 
            this.dtNacimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtNacimiento.Checked = false;
            this.dtNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNacimiento.Location = new System.Drawing.Point(0, 103);
            this.dtNacimiento.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.dtNacimiento.Name = "dtNacimiento";
            this.dtNacimiento.ShowCheckBox = true;
            this.dtNacimiento.Size = new System.Drawing.Size(170, 25);
            this.dtNacimiento.TabIndex = 14;
            this.dtNacimiento.ValueChanged += new System.EventHandler(this.dtNacimiento_ValueChanged);
            // 
            // lblTipoValor
            // 
            this.lblTipoValor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblTipoValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTipoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTipoValor.Location = new System.Drawing.Point(303, 105);
            this.lblTipoValor.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.lblTipoValor.Name = "lblTipoValor";
            this.lblTipoValor.Size = new System.Drawing.Size(161, 22);
            this.lblTipoValor.TabIndex = 15;
            this.lblTipoValor.Text = "-";
            this.lblTipoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAsistencia
            // 
            this.cmbAsistencia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cmbAsistencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAsistencia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAsistencia.FormattingEnabled = true;
            this.cmbAsistencia.Location = new System.Drawing.Point(476, 103);
            this.cmbAsistencia.Margin = new System.Windows.Forms.Padding(0, 3, 12, 0);
            this.cmbAsistencia.Name = "cmbAsistencia";
            this.cmbAsistencia.Size = new System.Drawing.Size(380, 25);
            this.cmbAsistencia.TabIndex = 16;
            this.cmbAsistencia.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmbAsistencia_Format);
            // 
            // CtrlPasajero_GV42
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlTarjeta);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CtrlPasajero_GV42";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.Size = new System.Drawing.Size(900, 158);
            this.tlpCampos.ResumeLayout(false);
            this.tlpCampos.PerformLayout();
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlTarjeta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlTarjeta;
        private System.Windows.Forms.TableLayoutPanel tlpCampos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefono;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtDni;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtNombre;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtApellido;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtEmail;
        private PROYECTO_ING_DE_SOFTWARE.TextBoxModerno_GV42 txtTelefono;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblAsistencia;
        private System.Windows.Forms.DateTimePicker dtNacimiento;
        private System.Windows.Forms.Label lblTipoValor;
        private System.Windows.Forms.ComboBox cmbAsistencia;
    }
}
