using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class TipoEvidenciaLista : Form
    {
        public TipoEvidenciaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.tiposDataGridView.AutoGenerateColumns = false;

            this.tiposDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 100
            });

            this.tiposDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 300
            });

            this.tiposDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ResourceType",
                HeaderText = "ResourceType",
                DataPropertyName = "ResourceType",
                Width = 180
            });

            this.tiposDataGridView.ReadOnly = true;
            this.tiposDataGridView.AllowUserToAddRows = false;
            this.tiposDataGridView.AllowUserToDeleteRows = false;
            this.tiposDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.tiposDataGridView.MultiSelect = false;
        }

        private async void TipoEvidenciaLista_Load(object sender, EventArgs e)
        {
            await this.GetTiposAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            TipoEvidenciaDTO nuevo = new TipoEvidenciaDTO();
            TipoEvidenciaDetalle detalle = new TipoEvidenciaDetalle(FormMode.Add, nuevo);
            detalle.ShowDialog();
            await this.GetTiposAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.tiposDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                Guid id = this.SelectedItem().Id;
                TipoEvidenciaDTO? tipo = await TipoEvidenciaApiClient.GetAsync(id);
                if (tipo != null)
                {
                    TipoEvidenciaDetalle detalle = new TipoEvidenciaDetalle(FormMode.Update, tipo);
                    detalle.ShowDialog();
                    await this.GetTiposAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar tipo de evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.tiposDataGridView.SelectedRows.Count == 0) return;

            TipoEvidenciaDTO tipo = this.SelectedItem();
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el tipo de evidencia '{tipo.Descripcion}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await TipoEvidenciaApiClient.DeleteAsync(tipo.Id);
                    await this.GetTiposAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar tipo de evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async Task GetTiposAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.tiposDataGridView.DataSource = null;

                var result = await TipoEvidenciaApiClient.GetAllAsync();
                this.tiposDataGridView.DataSource = result?.ToList() ?? new List<TipoEvidenciaDTO>();

                if (this.tiposDataGridView.Rows.Count > 0)
                {
                    this.tiposDataGridView.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private TipoEvidenciaDTO SelectedItem()
        {
            return (TipoEvidenciaDTO)tiposDataGridView.SelectedRows[0].DataBoundItem;
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            tiposDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayFilas = tiposDataGridView.Rows.Count > 0;
            agregarButton.Enabled = esInvestigador;
            tiposDataGridView.Enabled = true;
            actualizarButton.Enabled = esInvestigador && hayFilas;
            eliminarButton.Enabled = esInvestigador && hayFilas;
        }
    }
}
