using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class EvidenciaDetalle : Form
    {
        private EvidenciaDTO evidencia = new();
        private FormMode mode;
        private List<BestiaDTO> bestias = new();
        private List<TipoEvidenciaDTO> tipos = new();

        public EvidenciaDTO Evidencia
        {
            get => evidencia;
            set
            {
                evidencia = value;
                this.SetEvidencia();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public EvidenciaDetalle()
        {
            InitializeComponent();
            estadoComboBox.Items.AddRange(new object[] { "pendiente", "aprobado" });
        }

        public EvidenciaDetalle(FormMode mode, EvidenciaDTO evidencia, List<BestiaDTO> bestias) : this()
        {
            this.bestias = bestias;
            CargarComboBestias();
            Init(mode, evidencia);
        }

        private void CargarComboBestias()
        {
            this.bestiaComboBox.Items.Clear();
            foreach (var b in bestias)
            {
                this.bestiaComboBox.Items.Add(new ComboItem { Id = b.Id, Nombre = $"{b.Nombre} ({b.Peligrosidad})" });
            }
        }

        private async void Init(FormMode mode, EvidenciaDTO evidencia)
        {
            try
            {
                DeshabilitarControles();
                await CargarTiposEvidencia();
                this.Mode = mode;
                this.Evidencia = evidencia;
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

        private async Task CargarTiposEvidencia()
        {
            try
            {
                var result = await TipoEvidenciaApiClient.GetAllAsync();
                tipos = result?.ToList() ?? new List<TipoEvidenciaDTO>();
                this.tipoComboBox.Items.Clear();
                foreach (var t in tipos)
                {
                    this.tipoComboBox.Items.Add(new ComboItem { Id = t.Id, Nombre = $"{t.Descripcion} ({t.ResourceType})" });
                }
            }
            catch
            {
                tipos = new List<TipoEvidenciaDTO>();
            }
        }

        private void SetEvidencia()
        {
            for (int i = 0; i < this.bestiaComboBox.Items.Count; i++)
            {
                if (this.bestiaComboBox.Items[i] is ComboItem item && item.Id == this.Evidencia.IdBestia)
                {
                    this.bestiaComboBox.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < this.tipoComboBox.Items.Count; i++)
            {
                if (this.tipoComboBox.Items[i] is ComboItem item && item.Id == this.Evidencia.IdTipoEvidencia)
                {
                    this.tipoComboBox.SelectedIndex = i;
                    break;
                }
            }

            if (this.tipoComboBox.SelectedIndex < 0 && this.tipoComboBox.Items.Count > 0)
            {
                this.tipoComboBox.SelectedIndex = 0;
            }

            this.nroTextBox.Text = this.Evidencia.NroEvidencia.ToString();
            this.estadoComboBox.SelectedItem = string.IsNullOrEmpty(this.Evidencia.Estado) ? "pendiente" : this.Evidencia.Estado;
            this.fechaObtencionPicker.Value = this.Evidencia.FechaObtencion == default ? DateTime.Now : this.Evidencia.FechaObtencion;
            this.fileIdTextBox.Text = this.Evidencia.FileId;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;
            if (Mode == FormMode.Add)
            {
                this.Text = "Agregar Evidencia";
                nroLabel.Visible = false;
                nroTextBox.Visible = false;
                estadoComboBox.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Actualizar Evidencia";
                nroLabel.Visible = true;
                nroTextBox.Visible = true;
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateEvidencia()) return;

            try
            {
                DeshabilitarControles();

                var selectedBestia = bestiaComboBox.SelectedItem as ComboItem;
                var selectedTipo = tipoComboBox.SelectedItem as ComboItem;
                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

                this.Evidencia.IdBestia = selectedBestia!.Id;
                this.Evidencia.IdTipoEvidencia = selectedTipo!.Id;
                this.Evidencia.Estado = esInvestigador
                    ? (estadoComboBox.SelectedItem?.ToString() ?? "pendiente")
                    : "pendiente";
                this.Evidencia.FechaObtencion = fechaObtencionPicker.Value;
                this.Evidencia.FileId = fileIdTextBox.Text.Trim();

                if (this.Mode == FormMode.Update)
                {
                    await EvidenciaApiClient.UpdateAsync(this.Evidencia);
                }
                else
                {
                    await EvidenciaApiClient.AddAsync(this.Evidencia);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e) => this.Close();

        private bool ValidateEvidencia()
        {
            bool isValid = true;
            errorProvider.SetError(bestiaComboBox, string.Empty);
            errorProvider.SetError(tipoComboBox, string.Empty);
            errorProvider.SetError(fileIdTextBox, string.Empty);

            if (bestiaComboBox.SelectedItem == null)
            {
                errorProvider.SetError(bestiaComboBox, "Debe seleccionar una bestia");
                isValid = false;
            }

            if (tipoComboBox.SelectedItem == null)
            {
                errorProvider.SetError(tipoComboBox, "Debe seleccionar un tipo de evidencia");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(fileIdTextBox.Text))
            {
                errorProvider.SetError(fileIdTextBox, "El FileId es requerido");
                isValid = false;
            }

            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            bestiaComboBox.Enabled = false;
            tipoComboBox.Enabled = false;
            estadoComboBox.Enabled = false;
            fechaObtencionPicker.Enabled = false;
            fileIdTextBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool esLector = LoginForm.UsuarioLogueado is LectorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Lector";
            bool puedeEditar = esInvestigador || (esLector && Mode == FormMode.Add);

            aceptarButton.Visible = puedeEditar;
            aceptarButton.Enabled = puedeEditar;
            aceptarButton.Text = (!esInvestigador && esLector) ? "Proponer" : "Aceptar";
            cancelarButton.Enabled = true;
            cancelarButton.Text = puedeEditar ? "Cancelar" : "Cerrar";
            bestiaComboBox.Enabled = puedeEditar && Mode == FormMode.Add;
            tipoComboBox.Enabled = puedeEditar;
            estadoComboBox.Enabled = esInvestigador;
            fechaObtencionPicker.Enabled = puedeEditar;
            fileIdTextBox.Enabled = puedeEditar;
            if (!puedeEditar) this.Text = "Detalle de Evidencia";
            else if (!esInvestigador && esLector) this.Text = "Proponer Evidencia";
        }

        private class ComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }
    }
}

