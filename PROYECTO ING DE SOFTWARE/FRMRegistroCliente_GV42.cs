﻿using BE;
using BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROYECTO_ING_DE_SOFTWARE
{
    // El cliente crea su propia cuenta para poder reservar sin pasar por un vendedor (RFN 1,
    // canal Autogestión). Misma estética "tarjeta blanca sobre fondo celeste" que FRMIniciarSesion.
    public class FRMRegistroCliente_GV42 : Form
    {
        private readonly BLLReserva_GV42 _bll = new BLLReserva_GV42();

        private TextBox txtDni, txtNombre, txtApellido, txtEmail, txtTelefono, txtLogin, txtContrasena, txtConfirmar;
        private Button btnRegistrarme;

        public FRMRegistroCliente_GV42()
        {
            ConstruirUI();
        }

        private void ConstruirUI()
        {
            Text = "Crear cuenta de cliente";
            // Antes el alto no alcanzaba: con 8 campos + botón, el contenido terminaba fuera del
            // formulario y "Crear cuenta" quedaba inalcanzable. AutoScroll queda como red de
            // seguridad por si se agrega algún campo más adelante.
            ClientSize = new Size(560, 680);
            BackColor = Tema_GV42.Fondo;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);

            var card = Tema_GV42.CrearCard();
            card.Location = new Point(60, 20);
            card.Size = new Size(440, 640);
            card.AutoScroll = true;
            Controls.Add(card);

            var lblTitulo = new Label
            {
                Text = "Crear cuenta",
                Font = Tema_GV42.FuenteTitulo,
                ForeColor = Tema_GV42.Acento,
                AutoSize = true,
                Location = new Point(30, 20)
            };
            var lblSubtitulo = new Label
            {
                Text = "Registrate para reservar tus propios vuelos",
                Font = Tema_GV42.FuenteSubtitulo,
                ForeColor = Tema_GV42.Texto,
                AutoSize = true,
                Location = new Point(30, 55)
            };
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblSubtitulo);

            int y = 95;
            txtDni       = AgregarCampo(card, "DNI", ref y);
            txtNombre    = AgregarCampo(card, "Nombre", ref y);
            txtApellido  = AgregarCampo(card, "Apellido", ref y);
            txtEmail     = AgregarCampo(card, "Email", ref y);
            txtTelefono  = AgregarCampo(card, "Teléfono", ref y);
            txtLogin     = AgregarCampo(card, "Usuario (login)", ref y);
            txtContrasena = AgregarCampo(card, "Contraseña", ref y, esPassword: true);
            txtConfirmar  = AgregarCampo(card, "Confirmar contraseña", ref y, esPassword: true);

            btnRegistrarme = new Button
            {
                Text = "Crear cuenta",
                Location = new Point(30, y + 10),
                Size = new Size(380, 40)
            };
            Tema_GV42.EstilizarBotonPrimario(btnRegistrarme);
            btnRegistrarme.Click += btnRegistrarme_Click;
            card.Controls.Add(btnRegistrarme);

            AcceptButton = btnRegistrarme;
        }

        private TextBox AgregarCampo(Panel card, string etiqueta, ref int y, bool esPassword = false)
        {
            var lbl = Tema_GV42.CrearLabel(etiqueta);
            lbl.Location = new Point(30, y);
            card.Controls.Add(lbl);

            var txt = Tema_GV42.CrearTextBox();
            txt.Location = new Point(30, y + 20);
            txt.Size = new Size(380, 24);
            if (esPassword) txt.UseSystemPasswordChar = true;
            card.Controls.Add(txt);

            y += 55;
            return txt;
        }

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = new Pasajero_GV42
                {
                    DNI = txtDni.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                _bll.RegistrarClienteAutogestionado(cliente, txtLogin.Text.Trim(), txtContrasena.Text, txtConfirmar.Text);

                MessageBox.Show(
                    "Cuenta creada correctamente. Ya podés iniciar sesión.",
                    "Cuenta creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NegocioException_GV42 ex)
            {
                MessageBox.Show(ex.Message, "No se pudo crear la cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo crear la cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
