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
    public partial class NoticiaLista : Form
    {
        public NoticiaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.noticiaGridView.AutoGenerateColumns = false;

            this.noticiaGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 150
            });
            this.noticiaGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo",
                Width = 180
            });
            this.noticiaGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaPublicacion",
                HeaderText = "Fecha Publicacion",
                DataPropertyName = "FechaPublicacion",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm:ss" },
                Width = 130
            });
            this.noticiaGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Contenido",
                HeaderText = "Contenido",
                DataPropertyName = "Contenido",
                Width = 250
            });
            this.noticiaGridView.ReadOnly = true;
            this.noticiaGridView.AllowUserToAddRows = false;
            this.noticiaGridView.AllowUserToDeleteRows = false;
            this.noticiaGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.noticiaGridView.MultiSelect = false;
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void NoticiaLista_Load(object sender, EventArgs e)
        {
            await this.GetByCriteriaAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            NoticiaDTO noticiaNueva = new NoticiaDTO();
            NoticiaDetalle noticiaDetalle = new NoticiaDetalle(FormMode.Add, noticiaNueva);

            noticiaDetalle.ShowDialog();

            await this.GetByCriteriaAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();

                Guid id = this.SelectedItem().Id;
                NoticiaDTO noticia = await NoticiaApiClient.GetAsync(id);

                NoticiaDetalle noticiaDetalle = new NoticiaDetalle(FormMode.Update, noticia);
                noticiaDetalle.ShowDialog();

                await this.GetByCriteriaAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            NoticiaDTO noticia = this.SelectedItem();

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la noticia?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await NoticiaApiClient.DeleteAsync(noticia.Id);
                    await this.GetByCriteriaAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                this.noticiaGridView.DataSource = null;

                IEnumerable<NoticiaDTO> noticias;
               
                noticias = await NoticiaApiClient.GetAllAsync();
                

                this.noticiaGridView.DataSource = noticias;

                // Solo manejar Enabled/Disabled si los botones son visibles (tienen permisos)
                bool canUpdate = actualizarButton.Tag is bool updatePermission ? updatePermission : true;
                bool canDelete = eliminarButton.Tag is bool deletePermission ? deletePermission : true;

                if (this.noticiaGridView.Rows.Count > 0)
                {
                    this.noticiaGridView.Rows[0].Selected = true;

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
                MessageBox.Show($"Error al cargar noticias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private NoticiaDTO SelectedItem()
        {
            NoticiaDTO noticia;

            noticia = (NoticiaDTO)noticiaGridView.SelectedRows[0].DataBoundItem;
            return noticia;
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
            noticiaGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            buscarButton.Enabled = true;
            buscarTextBox.Enabled = true;
            agregarButton.Enabled = true;
            noticiaGridView.Enabled = true;
            // actualizar y eliminar se habilitan según permisos y datos en GetByCriteriaAndLoad
        }
    }
}
