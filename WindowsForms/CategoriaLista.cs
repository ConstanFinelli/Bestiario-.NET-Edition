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
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                Width = 200
            });

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 250
            });

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaisNombre",
                HeaderText = "País",
                DataPropertyName = "PaisNombre",
                Width = 150
            });

            this.categoriasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAlta",
                HeaderText = "Fecha Alta",
                DataPropertyName = "FechaAlta",
                Width = 250,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm:ss" }
            });
        }

        private async void Categorias_Load(object sender, EventArgs e)
        {
            await ConfigureButtonPermissions();
            await this.GetByCriteriaAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            CategoriaDTO clienteNuevo = new CategoriaDTO();
            CategoriaDetalle clienteDetalle = new CategoriaDetalle(FormMode.Add, clienteNuevo);

            clienteDetalle.ShowDialog();

            await this.GetByCriteriaAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();

                Guid id = this.SelectedItem().Id;
                CategoriaDTO cliente = await CategoriaApiClient.GetAsync(id);

                CategoriaDetalle clienteDetalle = new CategoriaDetalle(FormMode.Update, cliente);
                clienteDetalle.ShowDialog();

                await this.GetByCriteriaAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            CategoriaDTO cliente = this.SelectedItem();

            var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente {cliente.Nombre} {cliente.Apellido} ({cliente.Email})?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await CategoriaApiClient.DeleteAsync(cliente.Id);
                    await this.GetByCriteriaAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                bool canUpdate = actualizarButton.Tag is bool updatePermission && updatePermission;
                bool canDelete = eliminarButton.Tag is bool deletePermission && deletePermission;

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
            CategoriaDTO cliente;

            cliente = (CategoriaDTO)categoriasDataGridView.SelectedRows[0].DataBoundItem;

            return cliente;
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

