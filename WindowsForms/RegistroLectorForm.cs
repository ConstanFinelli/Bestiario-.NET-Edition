using System;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class RegistroLectorForm : Form
    {
        public RegistroLectorForm()
        {
            InitializeComponent();
            AppTheme.ApplyFormTheme(this);
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    registerButton.Enabled = false;
                    registerButton.Text = "Creando cuenta...";

                    var lectorDto = new LectorDTO
                    {
                        Correo = emailTextBox.Text.Trim(),
                        Contrasenia = passwordTextBox.Text,
                        RecibirNotificaciones = notificacionesCheckBox.Checked
                    };

                    var created = await UsuarioApiClient.AddLectorAsync(lectorDto);
                    if (created != null && created.Id != Guid.Empty)
                    {
                        // Iniciar sesión automáticamente con el nuevo usuario
                        var authResult = await UsuarioApiClient.LoginAsync(emailTextBox.Text.Trim(), passwordTextBox.Text);
                        if (authResult != null && authResult.Usuario != null)
                        {
                            LoginForm.UsuarioLogueado = authResult.Usuario;
                            MessageBox.Show($"¡Cuenta de Lector creada exitosamente!\nBienvenido al Bestiario, {authResult.Usuario.Correo}.",
                                "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo crear la cuenta de lector:\n{ex.Message}", "Error de Registro",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    registerButton.Enabled = true;
                    registerButton.Text = "Crear y Entrar";
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            errorProvider.SetError(emailTextBox, string.Empty);
            errorProvider.SetError(passwordTextBox, string.Empty);
            errorProvider.SetError(confirmPasswordTextBox, string.Empty);

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || !emailTextBox.Text.Contains("@"))
            {
                errorProvider.SetError(emailTextBox, "Ingrese un correo electrónico válido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                errorProvider.SetError(passwordTextBox, "La contraseña es requerida");
                isValid = false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                errorProvider.SetError(confirmPasswordTextBox, "Las contraseñas no coinciden");
                isValid = false;
            }

            return isValid;
        }
    }
}
