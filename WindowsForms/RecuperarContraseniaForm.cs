using System;
using System.Drawing;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public class RecuperarContraseniaForm : Form
    {
        private readonly Panel paso1Panel;
        private readonly Panel paso2Panel;

        // Paso 1
        private readonly TextBox paso1CorreoTextBox;
        private readonly Button enviarCodigoButton;
        private readonly Button paso1VolverButton;
        private readonly LinkLabel yaTengoCodigoLinkLabel;

        // Paso 2
        private readonly TextBox paso2CorreoTextBox;
        private readonly TextBox codigoTextBox;
        private readonly TextBox nuevaPasswordTextBox;
        private readonly TextBox confirmarPasswordTextBox;
        private readonly Button restablecerButton;
        private readonly Button paso2VolverButton;
        private readonly LinkLabel volverLoginLinkLabel;

        private readonly Label statusLabel;

        public string? CorreoRestablecido { get; private set; }

        public RecuperarContraseniaForm(string? correoInicial = null)
        {
            Text = "Recuperar contraseña";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(440, 320);
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);

            string correoDefault = !string.IsNullOrWhiteSpace(correoInicial) && correoInicial.Contains('@')
                ? correoInicial.Trim()
                : string.Empty;

            statusLabel = new Label
            {
                Location = new Point(25, 12),
                Size = new Size(390, 32),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            Controls.Add(statusLabel);

            // ================= PASO 1 =================
            paso1Panel = new Panel
            {
                Location = new Point(0, 48),
                Size = new Size(440, 260)
            };

            var infoLabel = new Label
            {
                Location = new Point(30, 8),
                Size = new Size(380, 36),
                Text = "Ingresá tu correo electrónico y te enviaremos un código de verificación para restablecer tu contraseña.",
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var paso1CorreoLabel = new Label
            {
                Location = new Point(70, 56),
                Size = new Size(300, 18),
                Text = "Correo electrónico",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            paso1CorreoTextBox = new TextBox
            {
                Location = new Point(70, 78),
                Size = new Size(300, 23),
                TextAlign = HorizontalAlignment.Center,
                PlaceholderText = "ejemplo@correo.com",
                Text = correoDefault
            };
            paso1CorreoTextBox.KeyPress += (_, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    _ = EnviarCodigoAsync();
                }
            };

            paso1VolverButton = new Button
            {
                Location = new Point(70, 125),
                Size = new Size(125, 28),
                Text = "Volver",
                UseVisualStyleBackColor = true
            };
            paso1VolverButton.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            enviarCodigoButton = new Button
            {
                Location = new Point(225, 125),
                Size = new Size(145, 28),
                Text = "Enviar código",
                UseVisualStyleBackColor = true
            };
            enviarCodigoButton.Click += async (_, _) => await EnviarCodigoAsync();

            yaTengoCodigoLinkLabel = new LinkLabel
            {
                Location = new Point(70, 174),
                Size = new Size(300, 20),
                Text = "Ya tengo un código de verificación",
                TextAlign = ContentAlignment.MiddleCenter
            };
            yaTengoCodigoLinkLabel.LinkClicked += (_, _) => MostrarPaso2(paso1CorreoTextBox.Text.Trim());

            paso1Panel.Controls.AddRange(new Control[]
            {
                infoLabel, paso1CorreoLabel, paso1CorreoTextBox,
                paso1VolverButton, enviarCodigoButton, yaTengoCodigoLinkLabel
            });

            // ================= PASO 2 =================
            paso2Panel = new Panel
            {
                Location = new Point(0, 48),
                Size = new Size(440, 268),
                Visible = false
            };

            var paso2CorreoLabel = new Label
            {
                Location = new Point(70, 0),
                Size = new Size(300, 16),
                Text = "Correo electrónico",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            paso2CorreoTextBox = new TextBox
            {
                Location = new Point(70, 18),
                Size = new Size(300, 23),
                TextAlign = HorizontalAlignment.Center,
                PlaceholderText = "ejemplo@correo.com",
                Text = correoDefault
            };

            var codigoLabel = new Label
            {
                Location = new Point(70, 48),
                Size = new Size(300, 16),
                Text = "Código de verificación (6 dígitos)",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            codigoTextBox = new TextBox
            {
                Location = new Point(130, 66),
                Size = new Size(180, 23),
                MaxLength = 6,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                PlaceholderText = "000000"
            };

            var nuevaPassLabel = new Label
            {
                Location = new Point(70, 96),
                Size = new Size(300, 16),
                Text = "Nueva contraseña",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            nuevaPasswordTextBox = new TextBox
            {
                Location = new Point(70, 114),
                Size = new Size(300, 23),
                PasswordChar = '*',
                TextAlign = HorizontalAlignment.Center
            };

            var confirmarPassLabel = new Label
            {
                Location = new Point(70, 144),
                Size = new Size(300, 16),
                Text = "Confirmar nueva contraseña",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            confirmarPasswordTextBox = new TextBox
            {
                Location = new Point(70, 162),
                Size = new Size(300, 23),
                PasswordChar = '*',
                TextAlign = HorizontalAlignment.Center
            };
            confirmarPasswordTextBox.KeyPress += (_, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    _ = RestablecerAsync();
                }
            };

            paso2VolverButton = new Button
            {
                Location = new Point(70, 198),
                Size = new Size(115, 28),
                Text = "Volver",
                UseVisualStyleBackColor = true
            };
            paso2VolverButton.Click += (_, _) => MostrarPaso1();

            restablecerButton = new Button
            {
                Location = new Point(195, 198),
                Size = new Size(175, 28),
                Text = "Restablecer contraseña",
                UseVisualStyleBackColor = true
            };
            restablecerButton.Click += async (_, _) => await RestablecerAsync();

            volverLoginLinkLabel = new LinkLabel
            {
                Location = new Point(70, 236),
                Size = new Size(300, 20),
                Text = "Volver al inicio de sesión",
                TextAlign = ContentAlignment.MiddleCenter
            };
            volverLoginLinkLabel.LinkClicked += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            paso2Panel.Controls.AddRange(new Control[]
            {
                paso2CorreoLabel, paso2CorreoTextBox,
                codigoLabel, codigoTextBox,
                nuevaPassLabel, nuevaPasswordTextBox,
                confirmarPassLabel, confirmarPasswordTextBox,
                paso2VolverButton, restablecerButton, volverLoginLinkLabel
            });

            Controls.Add(paso1Panel);
            Controls.Add(paso2Panel);
        }

        private void MostrarStatus(string mensaje, bool esError)
        {
            statusLabel.Text = mensaje;
            statusLabel.ForeColor = esError ? Color.DarkRed : Color.DarkGreen;
            statusLabel.BackColor = esError ? Color.FromArgb(248, 215, 218) : Color.FromArgb(212, 237, 218);
            statusLabel.Visible = true;
        }

        private void LimpiarStatus()
        {
            statusLabel.Visible = false;
            statusLabel.Text = string.Empty;
        }

        private void MostrarPaso1()
        {
            LimpiarStatus();
            Text = "Recuperar contraseña";
            paso2Panel.Visible = false;
            paso1Panel.Visible = true;
            paso1CorreoTextBox.Focus();
        }

        private void MostrarPaso2(string? correo = null)
        {
            Text = "Restablecer contraseña";
            if (!string.IsNullOrWhiteSpace(correo))
            {
                paso2CorreoTextBox.Text = correo.Trim();
            }
            paso1Panel.Visible = false;
            paso2Panel.Visible = true;
            codigoTextBox.Focus();
        }

        private async System.Threading.Tasks.Task EnviarCodigoAsync()
        {
            string correo = paso1CorreoTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            {
                MostrarStatus("Ingrese un correo electrónico válido.", true);
                paso1CorreoTextBox.Focus();
                return;
            }

            try
            {
                enviarCodigoButton.Enabled = false;
                enviarCodigoButton.Text = "Enviando...";
                paso1VolverButton.Enabled = false;
                LimpiarStatus();

                await UsuarioApiClient.SolicitarRecuperacionAsync(correo);
                MostrarPaso2(correo);
                MostrarStatus("Si el correo está registrado, enviamos un código de 6 dígitos a tu casilla.", false);
            }
            catch (Exception ex)
            {
                MostrarStatus(ex.Message, true);
            }
            finally
            {
                enviarCodigoButton.Enabled = true;
                enviarCodigoButton.Text = "Enviar código";
                paso1VolverButton.Enabled = true;
            }
        }

        private async System.Threading.Tasks.Task RestablecerAsync()
        {
            string correo = paso2CorreoTextBox.Text.Trim();
            string codigo = codigoTextBox.Text.Trim();
            string nuevaPass = nuevaPasswordTextBox.Text;
            string confirmarPass = confirmarPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            {
                MostrarStatus("Ingrese un correo electrónico válido.", true);
                paso2CorreoTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MostrarStatus("Ingrese el código de verificación recibido.", true);
                codigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(nuevaPass))
            {
                MostrarStatus("Ingrese la nueva contraseña.", true);
                nuevaPasswordTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(confirmarPass) || nuevaPass != confirmarPass)
            {
                MostrarStatus("Las contraseñas no coinciden.", true);
                confirmarPasswordTextBox.Focus();
                return;
            }

            try
            {
                restablecerButton.Enabled = false;
                restablecerButton.Text = "Guardando...";
                paso2VolverButton.Enabled = false;
                LimpiarStatus();

                await UsuarioApiClient.RestablecerContraseniaAsync(correo, codigo, nuevaPass);
                CorreoRestablecido = correo;
                MessageBox.Show(
                    "Contraseña restablecida correctamente. Ya podés iniciar sesión.",
                    "Contraseña restablecida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarStatus(ex.Message, true);
            }
            finally
            {
                restablecerButton.Enabled = true;
                restablecerButton.Text = "Restablecer contraseña";
                paso2VolverButton.Enabled = true;
            }
        }
    }
}
