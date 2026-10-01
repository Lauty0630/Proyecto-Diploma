namespace PROYECTO_ING_DE_SOFTWARE
{
    partial class FRMBackupManual
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
            this.pnlInfo = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.tlpAcciones = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCrear = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblCrearTitulo = new System.Windows.Forms.Label();
            this.lblCrearDesc = new System.Windows.Forms.Label();
            this.btnCrear = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlRestaurar = new PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42();
            this.lblRestaurarTitulo = new System.Windows.Forms.Label();
            this.lblRestaurarDesc = new System.Windows.Forms.Label();
            this.btnRestaurar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlBotonera = new System.Windows.Forms.Panel();
            this.btnCerrar = new PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42();
            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.tlpAcciones.SuspendLayout();
            this.pnlCrear.SuspendLayout();
            this.pnlRestaurar.SuspendLayout();
            this.pnlBotonera.SuspendLayout();
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
            this.lblTitulo.Text = "Gestión de Backups";
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
            this.lblSubtitulo.Text = "Copias de seguridad de la base de datos";
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.tlpAcciones);
            this.pnlContenido.Controls.Add(this.pnlSeparador);
            this.pnlContenido.Controls.Add(this.pnlInfo);
            this.pnlContenido.Controls.Add(this.pnlBotonera);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 80);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContenido.Size = new System.Drawing.Size(1000, 570);
            this.pnlContenido.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.AcentoSuperior = true;
            this.pnlInfo.Controls.Add(this.lblDescripcion);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(24, 20);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(24, 20, 24, 16);
            this.pnlInfo.Size = new System.Drawing.Size(952, 92);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 20);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(904, 56);
            this.lblDescripcion.TabIndex = 0;
            this.lblDescripcion.Text = "Además del backup automático cada 3 horas, podés generar uno manual en cualquier momento o restaurar la base desde un archivo .bak.";
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparador.Location = new System.Drawing.Point(24, 112);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(952, 16);
            this.pnlSeparador.TabIndex = 1;
            // 
            // tlpAcciones
            // 
            this.tlpAcciones.ColumnCount = 2;
            this.tlpAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAcciones.Controls.Add(this.pnlCrear, 0, 0);
            this.tlpAcciones.Controls.Add(this.pnlRestaurar, 1, 0);
            this.tlpAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpAcciones.Location = new System.Drawing.Point(24, 128);
            this.tlpAcciones.Name = "tlpAcciones";
            this.tlpAcciones.RowCount = 1;
            this.tlpAcciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAcciones.Size = new System.Drawing.Size(952, 236);
            this.tlpAcciones.TabIndex = 2;
            // 
            // pnlCrear
            // 
            this.pnlCrear.Controls.Add(this.btnCrear);
            this.pnlCrear.Controls.Add(this.lblCrearDesc);
            this.pnlCrear.Controls.Add(this.lblCrearTitulo);
            this.pnlCrear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCrear.Location = new System.Drawing.Point(0, 0);
            this.pnlCrear.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlCrear.Name = "pnlCrear";
            this.pnlCrear.Size = new System.Drawing.Size(468, 236);
            this.pnlCrear.TabIndex = 0;
            // 
            // lblCrearTitulo
            // 
            this.lblCrearTitulo.AutoSize = true;
            this.lblCrearTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblCrearTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblCrearTitulo.Location = new System.Drawing.Point(22, 22);
            this.lblCrearTitulo.Name = "lblCrearTitulo";
            this.lblCrearTitulo.Size = new System.Drawing.Size(160, 20);
            this.lblCrearTitulo.TabIndex = 0;
            this.lblCrearTitulo.Text = "Backup manual";
            // 
            // lblCrearDesc
            // 
            this.lblCrearDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCrearDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCrearDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblCrearDesc.Location = new System.Drawing.Point(24, 54);
            this.lblCrearDesc.Name = "lblCrearDesc";
            this.lblCrearDesc.Size = new System.Drawing.Size(420, 90);
            this.lblCrearDesc.TabIndex = 1;
            this.lblCrearDesc.Text = "Genera una copia completa de la base de datos en este momento. Podés usarla más adelante para restaurar el sistema.";
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCrear.Location = new System.Drawing.Point(24, 166);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(260, 42);
            this.btnCrear.TabIndex = 2;
            this.btnCrear.Text = "Generar Backup";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // pnlRestaurar
            // 
            this.pnlRestaurar.Controls.Add(this.btnRestaurar);
            this.pnlRestaurar.Controls.Add(this.lblRestaurarDesc);
            this.pnlRestaurar.Controls.Add(this.lblRestaurarTitulo);
            this.pnlRestaurar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRestaurar.Location = new System.Drawing.Point(484, 0);
            this.pnlRestaurar.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.pnlRestaurar.Name = "pnlRestaurar";
            this.pnlRestaurar.Size = new System.Drawing.Size(468, 236);
            this.pnlRestaurar.TabIndex = 1;
            // 
            // lblRestaurarTitulo
            // 
            this.lblRestaurarTitulo.AutoSize = true;
            this.lblRestaurarTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRestaurarTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.lblRestaurarTitulo.Location = new System.Drawing.Point(22, 22);
            this.lblRestaurarTitulo.Name = "lblRestaurarTitulo";
            this.lblRestaurarTitulo.Size = new System.Drawing.Size(160, 20);
            this.lblRestaurarTitulo.TabIndex = 0;
            this.lblRestaurarTitulo.Text = "Restaurar base de datos";
            // 
            // lblRestaurarDesc
            // 
            this.lblRestaurarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRestaurarDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRestaurarDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblRestaurarDesc.Location = new System.Drawing.Point(24, 54);
            this.lblRestaurarDesc.Name = "lblRestaurarDesc";
            this.lblRestaurarDesc.Size = new System.Drawing.Size(420, 90);
            this.lblRestaurarDesc.TabIndex = 1;
            this.lblRestaurarDesc.Text = "Reemplaza la base actual por la de un archivo .bak. Se pierden los cambios posteriores y la aplicación se cierra al terminar.";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestaurar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Advertencia;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestaurar.Location = new System.Drawing.Point(24, 166);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(260, 42);
            this.btnRestaurar.TabIndex = 2;
            this.btnRestaurar.Text = "Elegir archivo y restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // pnlBotonera
            // 
            this.pnlBotonera.Controls.Add(this.btnCerrar);
            this.pnlBotonera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotonera.Location = new System.Drawing.Point(24, 494);
            this.pnlBotonera.Name = "pnlBotonera";
            this.pnlBotonera.Size = new System.Drawing.Size(952, 56);
            this.pnlBotonera.TabIndex = 3;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Estilo = PROYECTO_ING_DE_SOFTWARE.EstiloBoton_GV42.Secundario;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(812, 8);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(140, 40);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FRMBackupManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FRMBackupManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Backups";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.tlpAcciones.ResumeLayout(false);
            this.pnlCrear.ResumeLayout(false);
            this.pnlCrear.PerformLayout();
            this.pnlRestaurar.ResumeLayout(false);
            this.pnlRestaurar.PerformLayout();
            this.pnlBotonera.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PROYECTO_ING_DE_SOFTWARE.PanelEncabezado_GV42 pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlInfo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Panel pnlSeparador;
        private System.Windows.Forms.TableLayoutPanel tlpAcciones;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlCrear;
        private System.Windows.Forms.Label lblCrearTitulo;
        private System.Windows.Forms.Label lblCrearDesc;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCrear;
        private PROYECTO_ING_DE_SOFTWARE.PanelTarjeta_GV42 pnlRestaurar;
        private System.Windows.Forms.Label lblRestaurarTitulo;
        private System.Windows.Forms.Label lblRestaurarDesc;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnRestaurar;
        private System.Windows.Forms.Panel pnlBotonera;
        private PROYECTO_ING_DE_SOFTWARE.BotonModerno_GV42 btnCerrar;
    }
}
