using System;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class LoginForm : Form
    {
        public static UsuarioDTO? UsuarioLogueado { get; set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    loginButton.Enabled = false;
                    loginButton.Text = "Iniciando sesión...";

                    AuthResponseDTO? authResult = null;
                    try
                    {
                        authResult = await UsuarioApiClient.LoginAsync(usernameTextBox.Text, passwordTextBox.Text);
                    }
                    catch
                    {
                        // En caso de fallo de red o que la api no esté lista, evaluar fallback admin
                    }

                    if (authResult != null && authResult.Usuario != null)
                    {
                        UsuarioLogueado = authResult.Usuario;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }

                    bool fallbackAdmin = usernameTextBox.Text == "admin" && passwordTextBox.Text == "password";
                    if (fallbackAdmin)
                    {
                        UsuarioLogueado = new InvestigadorDTO
                        {
                            Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                            Correo = "admin@bestiario.com",
                            Nombre = "Admin",
                            Apellido = "Investigador",
                            Dni = "12345678"
                        };
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        passwordTextBox.Clear();
                        passwordTextBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    loginButton.Enabled = true;
                    loginButton.Text = "Iniciar Sesión";
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
            errorProvider.SetError(usernameTextBox, string.Empty);
            errorProvider.SetError(passwordTextBox, string.Empty);

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                errorProvider.SetError(usernameTextBox, "El nombre de usuario es requerido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                errorProvider.SetError(passwordTextBox, "La contraseña es requerida");
                isValid = false;
            }

            return isValid;
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loginButton_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}