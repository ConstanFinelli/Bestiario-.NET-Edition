using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class HabitatDetalle : Form
    {
        private HabitatDTO habitat = new();
        private FormMode mode;
        private BindingList<CaracteristicaHabitatDTO> caracteristicasBindingList = new();

        public HabitatDTO Habitat
        {
            get => habitat;
            set
            {
                habitat = value;
                this.SetHabitat();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public HabitatDetalle()
        {
            InitializeComponent();
            ConfigurarColumnasCaracteristicas();
        }

        public HabitatDetalle(FormMode mode, HabitatDTO habitat) : this()
        {
            Init(mode, habitat);
        }

        private void ConfigurarColumnasCaracteristicas()
        {
            this.caracteristicasDataGridView.AutoGenerateColumns = false;

            this.caracteristicasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroCaracteristica",
                HeaderText = "#",
                DataPropertyName = "NroCaracteristica",
                Width = 45
            });

            this.caracteristicasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 460
            });

            this.caracteristicasDataGridView.ReadOnly = true;
            this.caracteristicasDataGridView.AllowUserToAddRows = false;
            this.caracteristicasDataGridView.AllowUserToDeleteRows = false;
            this.caracteristicasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.caracteristicasDataGridView.MultiSelect = false;
        }

        private void Init(FormMode mode, HabitatDTO habitat)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Habitat = habitat;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del hábitat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void SetHabitat()
        {
            this.idTextBox.Text = this.Habitat.Id.ToString();
            this.nombreTextBox.Text = this.Habitat.Nombre;
            this.localizacionTextBox.Text = this.Habitat.Localizacion;
            this.latitudTextBox.Text = this.Habitat.Latitud.ToString(CultureInfo.InvariantCulture);
            this.longitudTextBox.Text = this.Habitat.Longitud.ToString(CultureInfo.InvariantCulture);

            caracteristicasBindingList = new BindingList<CaracteristicaHabitatDTO>(
                this.Habitat.Caracteristicas.Select(c => new CaracteristicaHabitatDTO
                {
                    IdHabitat = c.IdHabitat,
                    NroCaracteristica = c.NroCaracteristica,
                    Descripcion = c.Descripcion
                }).ToList());
            this.caracteristicasDataGridView.DataSource = caracteristicasBindingList;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;
            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                this.Text = "Agregar Hábitat (Maestro / Detalle)";
            }
            else
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                this.Text = "Actualizar Hábitat (Maestro / Detalle)";
            }
        }

        private void agregarCaracteristicaButton_Click(object sender, EventArgs e)
        {
            string desc = this.nuevaCaracteristicaTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("La descripción de la característica es requerida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nuevaCaracteristicaTextBox.Focus();
                return;
            }

            int proximoNro = caracteristicasBindingList.Any() ? caracteristicasBindingList.Max(c => c.NroCaracteristica) + 1 : 1;
            caracteristicasBindingList.Add(new CaracteristicaHabitatDTO
            {
                IdHabitat = this.Habitat.Id,
                NroCaracteristica = proximoNro,
                Descripcion = desc
            });

            this.nuevaCaracteristicaTextBox.Clear();
            this.nuevaCaracteristicaTextBox.Focus();
        }

        private void quitarCaracteristicaButton_Click(object sender, EventArgs e)
        {
            if (this.caracteristicasDataGridView.SelectedRows.Count == 0) return;

            var item = (CaracteristicaHabitatDTO)this.caracteristicasDataGridView.SelectedRows[0].DataBoundItem;
            caracteristicasBindingList.Remove(item);

            for (int i = 0; i < caracteristicasBindingList.Count; i++)
            {
                caracteristicasBindingList[i].NroCaracteristica = i + 1;
            }

            this.caracteristicasDataGridView.Refresh();
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateHabitat(out double lat, out double lon)) return;

            try
            {
                DeshabilitarControles();

                this.Habitat.Nombre = nombreTextBox.Text.Trim();
                this.Habitat.Localizacion = localizacionTextBox.Text.Trim();
                this.Habitat.Latitud = lat;
                this.Habitat.Longitud = lon;
                this.Habitat.Caracteristicas = caracteristicasBindingList.ToList();

                if (this.Mode == FormMode.Update)
                {
                    await HabitatApiClient.UpdateAsync(this.Habitat);
                }
                else
                {
                    await HabitatApiClient.AddAsync(this.Habitat);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar hábitat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateHabitat(out double latitud, out double longitud)
        {
            bool isValid = true;
            latitud = 0;
            longitud = 0;

            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(localizacionTextBox, string.Empty);
            errorProvider.SetError(latitudTextBox, string.Empty);
            errorProvider.SetError(longitudTextBox, string.Empty);

            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                errorProvider.SetError(nombreTextBox, "El nombre es requerido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(localizacionTextBox.Text))
            {
                errorProvider.SetError(localizacionTextBox, "La localización es requerida");
                isValid = false;
            }

            if (!double.TryParse(latitudTextBox.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out latitud)
                || latitud < -90 || latitud > 90)
            {
                errorProvider.SetError(latitudTextBox, "Ingrese una latitud válida (-90 a 90)");
                isValid = false;
            }

            if (!double.TryParse(longitudTextBox.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out longitud)
                || longitud < -180 || longitud > 180)
            {
                errorProvider.SetError(longitudTextBox, "Ingrese una longitud válida (-180 a 180)");
                isValid = false;
            }

            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            nombreTextBox.Enabled = false;
            localizacionTextBox.Enabled = false;
            latitudTextBox.Enabled = false;
            longitudTextBox.Enabled = false;
            nuevaCaracteristicaTextBox.Enabled = false;
            agregarCaracteristicaButton.Enabled = false;
            quitarCaracteristicaButton.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            aceptarButton.Visible = esInvestigador;
            aceptarButton.Enabled = esInvestigador;
            cancelarButton.Enabled = true;
            cancelarButton.Text = esInvestigador ? "Cancelar" : "Cerrar";
            nombreTextBox.Enabled = esInvestigador;
            localizacionTextBox.Enabled = esInvestigador;
            latitudTextBox.Enabled = esInvestigador;
            longitudTextBox.Enabled = esInvestigador;
            nuevaCaracteristicaTextBox.Enabled = esInvestigador;
            agregarCaracteristicaButton.Enabled = esInvestigador;
            quitarCaracteristicaButton.Enabled = esInvestigador;
            if (!esInvestigador)
            {
                this.Text = "Detalle de Hábitat";
            }
        }
    }
}
