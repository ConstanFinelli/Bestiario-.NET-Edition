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
    public partial class CategoriaLista : Form
    {
        public CategoriaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.categoriasDataGridView.AutoGenerateColumns = false;

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripcion",
                DataPropertyName = "Descripcion",
                Width = 300
            });

            this.categoriasDataGridView.ReadOnly = true;
            this.categoriasDataGridView.AllowUserToAddRows = false;
            this.categoriasDataGridView.AllowUserToDeleteRows = false;
            this.categoriasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.categoriasDataGridView.MultiSelect = false;
        }

        private async void Categorias_Load(object sender, EventArgs e)
        {
            await this.GetByCriteriaAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            CategoriaDTO categoriaNuevo = new CategoriaDTO();
            CategoriaDetalle categoriaDetalle = new CategoriaDetalle(FormMode.Add, categoriaNuevo);

            categoriaDetalle.ShowDialog();

            await this.GetByCriteriaAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();

                Guid id = this.SelectedItem().Id;
                CategoriaDTO categoria = await CategoriaApiClient.GetAsync(id);

                CategoriaDetalle categoriaDetalle = new CategoriaDetalle(FormMode.Update, categoria);
                categoriaDetalle.ShowDialog();

                await this.GetByCriteriaAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar categoria: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            CategoriaDTO categoria = this.SelectedItem();

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la categoria {categoria.Nombre} {categoria.Descripcion}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await CategoriaApiClient.DeleteAsync(categoria.Id);
                    await this.GetByCriteriaAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar categoria: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async Task GetByCriteriaAndLoad(string texto = "")
        {
            try
            {
                DeshabilitarControles();
                this.categoriasDataGridView.DataSource = null;

                IEnumerable<CategoriaDTO> categorias;
                if (string.IsNullOrWhiteSpace(texto))
                {
                    categorias = await CategoriaApiClient.GetAllAsync();
                }
                else
                {
                    categorias = await CategoriaApiClient.GetByCriteriaAsync(texto);
                }

                this.categoriasDataGridView.DataSource = categorias;

                // Solo manejar Enabled/Disabled si los botones son visibles (tienen permisos)
                bool canUpdate = actualizarButton.Tag is bool updatePermission ? updatePermission : true;
                bool canDelete = eliminarButton.Tag is bool deletePermission ? deletePermission : true;

                if (this.categoriasDataGridView.Rows.Count > 0)
                {
                    this.categoriasDataGridView.Rows[0].Selected = true;

                    // Solo habilitar si tiene permisos Y hay elementos
                    if (canDelete) this.eliminarButton.Enabled = true;
                    if (canUpdate) this.actualizarButton.Enabled = true;
                }
                else
                {
                    // Solo deshabilitar si son visibles
                    if (canDelete) this.eliminarButton.Enabled = false;
                    if (canUpdate) this.actualizarButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private CategoriaDTO SelectedItem()
        {
            CategoriaDTO categoria;

            categoria = (CategoriaDTO)categoriasDataGridView.SelectedRows[0].DataBoundItem;

            return categoria;
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            string texto = this.buscarTextBox.Text.Trim();
            await this.GetByCriteriaAndLoad(texto);
        }

        private void DeshabilitarControles()
        {
            buscarButton.Enabled = false;
            buscarTextBox.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            categoriasDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            buscarButton.Enabled = true;
            buscarTextBox.Enabled = true;
            agregarButton.Enabled = true;
            categoriasDataGridView.Enabled = true;
            // actualizar y eliminar se habilitan según permisos y datos en GetByCriteriaAndLoad
        }

    }
}

