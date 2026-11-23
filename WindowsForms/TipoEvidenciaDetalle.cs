using System;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class TipoEvidenciaDetalle : Form
    {
        private TipoEvidenciaDTO tipoEvidencia = new();
        private FormMode mode;

        public TipoEvidenciaDTO TipoEvidencia
        {
            get => tipoEvidencia;
            set
            {
                tipoEvidencia = value;
                this.SetTipoEvidencia();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public TipoEvidenciaDetalle()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
        }

        public TipoEvidenciaDetalle(FormMode mode, TipoEvidenciaDTO tipoEvidencia) : this()
        {
            Init(mode, tipoEvidencia);
        }

        private void Init(FormMode mode, TipoEvidenciaDTO tipoEvidencia)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.TipoEvidencia = tipoEvidencia;
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

        private void SetTipoEvidencia()
        {
            this.idTextBox.Text = this.TipoEvidencia.Id.ToString();
            this.descripcionTextBox.Text = this.TipoEvidencia.Descripcion;
            this.resourceTypeComboBox.Text = string.IsNullOrEmpty(this.TipoEvidencia.ResourceType) ? "image" : this.TipoEvidencia.ResourceType;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;
            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                this.Text = "Agregar Tipo de Evidencia";
            }
            else
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                this.Text = "Actualizar Tipo de Evidencia";
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateTipo()) return;

            try
            {
                DeshabilitarControles();

                this.TipoEvidencia.Descripcion = descripcionTextBox.Text.Trim();
                this.TipoEvidencia.ResourceType = resourceTypeComboBox.Text.Trim();

                if (this.Mode == FormMode.Update)
                {
                    await TipoEvidenciaApiClient.UpdateAsync(this.TipoEvidencia);
                }
                else
                {
                    await TipoEvidenciaApiClient.AddAsync(this.TipoEvidencia);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar tipo de evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e) => this.Close();

        private bool ValidateTipo()
        {
            bool isValid = true;
            errorProvider.SetError(descripcionTextBox, string.Empty);
            errorProvider.SetError(resourceTypeComboBox, string.Empty);

            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                errorProvider.SetError(descripcionTextBox, "La descripción es requerida");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(resourceTypeComboBox.Text))
            {
                errorProvider.SetError(resourceTypeComboBox, "El ResourceType es requerido");
                isValid = false;
            }

            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            descripcionTextBox.Enabled = false;
            resourceTypeComboBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            descripcionTextBox.Enabled = true;
            resourceTypeComboBox.Enabled = true;
        }
    }
}

