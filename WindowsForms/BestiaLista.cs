using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class BestiaLista : Form
    {
        public BestiaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
            AppTheme.ApplyFormTheme(this);
        }

        private void ConfigurarColumnas()
        {
            this.bestiasDataGridView.AutoGenerateColumns = false;

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 100
            });

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 180
            });

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Peligrosidad",
                HeaderText = "Peligrosidad",
                DataPropertyName = "Peligrosidad",
                Width = 110
            });

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categorias",
                HeaderText = "Categorías",
                DataPropertyName = "CategoriasTexto",
                Width = 220
            });

            this.bestiasDataGridView.ReadOnly = true;
            this.bestiasDataGridView.AllowUserToAddRows = false;
            this.bestiasDataGridView.AllowUserToDeleteRows = false;
            this.bestiasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.bestiasDataGridView.MultiSelect = false;
        }

        private async void Bestias_Load(object sender, EventArgs e)
        {
            await this.GetBestiasAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            BestiaDTO bestiaNueva = new BestiaDTO();
            BestiaDetalle bestiaDetalle = new BestiaDetalle(FormMode.Add, bestiaNueva);

            bestiaDetalle.ShowDialog();

            await this.GetBestiasAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.bestiasDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();

                Guid id = this.SelectedItem().Id;
                BestiaDTO? bestia = await BestiaApiClient.GetAsync(id);

                if (bestia != null)
                {
                    BestiaDetalle bestiaDetalle = new BestiaDetalle(FormMode.Update, bestia);
                    bestiaDetalle.ShowDialog();
                    await this.GetBestiasAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener bestia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.bestiasDataGridView.SelectedRows.Count == 0) return;

            BestiaDTO bestia = this.SelectedItem();

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la bestia '{bestia.Nombre}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await BestiaApiClient.DeleteAsync(bestia.Id);
                    await this.GetBestiasAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar bestia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private void verRegistrosButton_Click(object sender, EventArgs e)
        {
            if (this.bestiasDataGridView.SelectedRows.Count == 0) return;

            BestiaDTO bestia = this.SelectedItem();
            RegistroLista registrosForm = new RegistroLista(bestia.Id);
            registrosForm.ShowDialog();
        }

        private async Task GetBestiasAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.bestiasDataGridView.DataSource = null;

                var result = await BestiaApiClient.GetAllAsync();
                if (LoginForm.UsuarioLogueado?.TipoUsuario == "Lector")
                {
                    result = result?.Where(b => b.Estado != null && b.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var list = result?.Select(b => new BestiaVM
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Peligrosidad = b.Peligrosidad,
                    Estado = b.Estado,
                    CategoriasIds = b.CategoriasIds,
                    CategoriasNombres = b.CategoriasNombres,
                    CategoriasTexto = b.CategoriasNombres.Any() ? string.Join(", ", b.CategoriasNombres) : "-"
                }).ToList() ?? new List<BestiaVM>();

                this.bestiasDataGridView.DataSource = list;

                if (this.bestiasDataGridView.Rows.Count > 0)
                {
                    this.bestiasDataGridView.Rows[0].Selected = true;
                    this.eliminarButton.Enabled = true;
                    this.actualizarButton.Enabled = true;
                    this.verRegistrosButton.Enabled = true;
                }
                else
                {
                    this.eliminarButton.Enabled = false;
                    this.actualizarButton.Enabled = false;
                    this.verRegistrosButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bestias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private BestiaDTO SelectedItem()
        {
            var vm = (BestiaVM)bestiasDataGridView.SelectedRows[0].DataBoundItem;
            return new BestiaDTO
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Peligrosidad = vm.Peligrosidad,
                Estado = vm.Estado,
                CategoriasIds = vm.CategoriasIds,
                CategoriasNombres = vm.CategoriasNombres
            };
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            verRegistrosButton.Enabled = false;
            bestiasDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            agregarButton.Enabled = true;
            bestiasDataGridView.Enabled = true;
            if (bestiasDataGridView.Rows.Count > 0)
            {
                actualizarButton.Enabled = true;
                eliminarButton.Enabled = true;
                verRegistrosButton.Enabled = true;
            }
        }

        private class BestiaVM : BestiaDTO
        {
            public string CategoriasTexto { get; set; } = string.Empty;
        }
    }
}
