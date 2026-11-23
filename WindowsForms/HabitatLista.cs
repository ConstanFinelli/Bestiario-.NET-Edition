using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class HabitatLista : Form
    {
        public HabitatLista()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.habitatsDataGridView.AutoGenerateColumns = false;

            this.habitatsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 170
            });

            this.habitatsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Localizacion",
                HeaderText = "Localización",
                DataPropertyName = "Localizacion",
                Width = 210
            });

            this.habitatsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Latitud",
                HeaderText = "Latitud",
                DataPropertyName = "Latitud",
                Width = 90
            });

            this.habitatsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Longitud",
                HeaderText = "Longitud",
                DataPropertyName = "Longitud",
                Width = 90
            });

            this.habitatsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadCaracteristicas",
                HeaderText = "Características",
                DataPropertyName = "CantidadCaracteristicas",
                Width = 110
            });

            this.habitatsDataGridView.ReadOnly = true;
            this.habitatsDataGridView.AllowUserToAddRows = false;
            this.habitatsDataGridView.AllowUserToDeleteRows = false;
            this.habitatsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.habitatsDataGridView.MultiSelect = false;
        }

        private async void HabitatLista_Load(object sender, EventArgs e)
        {
            await this.GetHabitatsAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            HabitatDTO nuevo = new HabitatDTO();
            HabitatDetalle detalle = new HabitatDetalle(FormMode.Add, nuevo);
            detalle.ShowDialog();
            await this.GetHabitatsAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.habitatsDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                Guid id = this.SelectedItem().Id;
                HabitatDTO? habitat = await HabitatApiClient.GetAsync(id);
                if (habitat != null)
                {
                    HabitatDetalle detalle = new HabitatDetalle(FormMode.Update, habitat);
                    detalle.ShowDialog();
                    await this.GetHabitatsAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener hábitat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.habitatsDataGridView.SelectedRows.Count == 0) return;

            HabitatVM habitat = this.SelectedItem();
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el hábitat '{habitat.Nombre}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await HabitatApiClient.DeleteAsync(habitat.Id);
                    await this.GetHabitatsAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar hábitat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async Task GetHabitatsAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.habitatsDataGridView.DataSource = null;

                var result = await HabitatApiClient.GetAllAsync();
                var list = result?.Select(h => new HabitatVM
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    Localizacion = h.Localizacion,
                    Latitud = h.Latitud,
                    Longitud = h.Longitud,
                    Caracteristicas = h.Caracteristicas,
                    CantidadCaracteristicas = h.Caracteristicas.Count
                }).ToList() ?? new List<HabitatVM>();

                this.habitatsDataGridView.DataSource = list;

                if (this.habitatsDataGridView.Rows.Count > 0)
                {
                    this.habitatsDataGridView.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar hábitats: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private HabitatVM SelectedItem()
        {
            return (HabitatVM)habitatsDataGridView.SelectedRows[0].DataBoundItem;
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            habitatsDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayFilas = habitatsDataGridView.Rows.Count > 0;
            agregarButton.Enabled = esInvestigador;
            habitatsDataGridView.Enabled = true;
            actualizarButton.Enabled = hayFilas;
            actualizarButton.Text = esInvestigador ? "Actualizar" : "Ver Detalle";
            eliminarButton.Enabled = esInvestigador && hayFilas;
        }

        private class HabitatVM : HabitatDTO
        {
            public int CantidadCaracteristicas { get; set; }
        }
    }
}

