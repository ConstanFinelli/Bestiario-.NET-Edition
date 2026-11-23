using System;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class UsuarioDetalle : Form
    {
        private UsuarioDTO usuario = new();
        private FormMode mode;

        public UsuarioDTO Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
                SetUsuario();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public UsuarioDetalle()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            tipoComboBox.Items.AddRange(new object[] { "Investigador", "Lector" });
        }

        public UsuarioDetalle(FormMode mode, UsuarioDTO usuario) : this()
        {
            Init(mode, usuario);
        }

        private void Init(FormMode mode, UsuarioDTO usuario)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Usuario = usuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void SetUsuario()
        {
            idTextBox.Text = Usuario.Id.ToString();
            correoTextBox.Text = Usuario.Correo;
            contraseniaTextBox.Text = string.Empty;
            notificacionesCheckBox.Checked = Usuario.RecibirNotificaciones;

            if (Usuario is InvestigadorDTO inv)
            {
                tipoComboBox.SelectedItem = "Investigador";
                dniTextBox.Text = inv.Dni;
                nombreTextBox.Text = inv.Nombre;
                apellidoTextBox.Text = inv.Apellido;
            }
            else
            {
                tipoComboBox.SelectedItem = "Lector";
                dniTextBox.Text = string.Empty;
                nombreTextBox.Text = string.Empty;
                apellidoTextBox.Text = string.Empty;
            }

            ActualizarVisibilidadCamposInvestigador();
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;
            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                tipoComboBox.Enabled = true;
                this.Text = "Agregar Usuario";
            }
            else
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                tipoComboBox.Enabled = false;
                this.Text = "Actualizar Usuario";
            }
        }

        private void tipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadCamposInvestigador();
        }

        private void ActualizarVisibilidadCamposInvestigador()
        {
            bool esInv = tipoComboBox.SelectedItem?.ToString() == "Investigador";
            dniLabel.Visible = esInv;
            dniTextBox.Visible = esInv;
            nombreLabel.Visible = esInv;
            nombreTextBox.Visible = esInv;
            apellidoLabel.Visible = esInv;
            apellidoTextBox.Visible = esInv;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateUsuario()) return;

            try
            {
                DeshabilitarControles();
                bool esInv = tipoComboBox.SelectedItem?.ToString() == "Investigador";

                if (esInv)
                {
                    var invDto = new InvestigadorDTO
                    {
                        Id = Mode == FormMode.Update ? Usuario.Id : Guid.Empty,
                        Correo = correoTextBox.Text.Trim(),
                        Contrasenia = string.IsNullOrWhiteSpace(contraseniaTextBox.Text) ? null : contraseniaTextBox.Text,
                        RecibirNotificaciones = notificacionesCheckBox.Checked,
                        Dni = dniTextBox.Text.Trim(),
                        Nombre = nombreTextBox.Text.Trim(),
                        Apellido = apellidoTextBox.Text.Trim()
                    };

                    if (Mode == FormMode.Add)
                        await UsuarioApiClient.AddInvestigadorAsync(invDto);
                    else
                        await UsuarioApiClient.UpdateInvestigadorAsync(invDto);
                }
                else
                {
                    var lecDto = new LectorDTO
                    {
                        Id = Mode == FormMode.Update ? Usuario.Id : Guid.Empty,
                        Correo = correoTextBox.Text.Trim(),
                        Contrasenia = string.IsNullOrWhiteSpace(contraseniaTextBox.Text) ? null : contraseniaTextBox.Text,
                        RecibirNotificaciones = notificacionesCheckBox.Checked
                    };

                    if (Mode == FormMode.Add)
                        await UsuarioApiClient.AddLectorAsync(lecDto);
                    else
                        await UsuarioApiClient.UpdateLectorAsync(lecDto);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e) => this.Close();

        private bool ValidateUsuario()
        {
            bool isValid = true;
            errorProvider.SetError(correoTextBox, string.Empty);
            errorProvider.SetError(contraseniaTextBox, string.Empty);
            errorProvider.SetError(dniTextBox, string.Empty);
            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(apellidoTextBox, string.Empty);

            if (string.IsNullOrWhiteSpace(correoTextBox.Text) || !correoTextBox.Text.Contains("@"))
            {
                errorProvider.SetError(correoTextBox, "Ingrese un correo electrónico válido");
                isValid = false;
            }

            if (Mode == FormMode.Add && string.IsNullOrWhiteSpace(contraseniaTextBox.Text))
            {
                errorProvider.SetError(contraseniaTextBox, "La contraseña es requerida para nuevos usuarios");
                isValid = false;
            }

            if (tipoComboBox.SelectedItem?.ToString() == "Investigador")
            {
                if (string.IsNullOrWhiteSpace(dniTextBox.Text))
                {
                    errorProvider.SetError(dniTextBox, "El DNI es requerido");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
                {
                    errorProvider.SetError(nombreTextBox, "El nombre es requerido");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(apellidoTextBox.Text))
                {
                    errorProvider.SetError(apellidoTextBox, "El apellido es requerido");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            tipoComboBox.Enabled = false;
            correoTextBox.Enabled = false;
            contraseniaTextBox.Enabled = false;
            notificacionesCheckBox.Enabled = false;
            dniTextBox.Enabled = false;
            nombreTextBox.Enabled = false;
            apellidoTextBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            tipoComboBox.Enabled = Mode == FormMode.Add;
            correoTextBox.Enabled = true;
            contraseniaTextBox.Enabled = true;
            notificacionesCheckBox.Enabled = true;
            dniTextBox.Enabled = true;
            nombreTextBox.Enabled = true;
            apellidoTextBox.Enabled = true;
        }
    }
}

