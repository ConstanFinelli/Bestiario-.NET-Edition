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
        private List<NoticiaDTO> _todasNoticias = new();
        private CheckBox _desdeCheckBox = null!;
        private DateTimePicker _desdePicker = null!;
        private CheckBox _hastaCheckBox = null!;
        private DateTimePicker _hastaPicker = null!;

        public NoticiaLista()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            InicializarFiltroFecha();
            ConfigurarColumnas();
        }

        private void InicializarFiltroFecha()
        {
            this.noticiaGridView.Top += 34;
            this.noticiaGridView.Height -= 34;

            _desdeCheckBox = new CheckBox
            {
                Text = "Desde:",
                AutoSize = true,
                Location = new Point(12, 13)
            };
            _desdeCheckBox.CheckedChanged += (_, _) =>
            {
                _desdePicker.Enabled = _desdeCheckBox.Checked;
                AplicarFiltroFecha();
            };

            _desdePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(78, 10),
                Width = 110,
                Enabled = false
            };
            _desdePicker.ValueChanged += (_, _) => { if (_desdeCheckBox.Checked) AplicarFiltroFecha(); };

            _hastaCheckBox = new CheckBox
            {
                Text = "Hasta:",
                AutoSize = true,
                Location = new Point(204, 13)
            };
            _hastaCheckBox.CheckedChanged += (_, _) =>
            {
                _hastaPicker.Enabled = _hastaCheckBox.Checked;
                AplicarFiltroFecha();
            };

            _hastaPicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(268, 10),
                Width = 110,
                Enabled = false
            };
            _hastaPicker.ValueChanged += (_, _) => { if (_hastaCheckBox.Checked) AplicarFiltroFecha(); };

            var limpiarBtn = new Button
            {
                Text = "Limpiar",
                Size = new Size(76, 24),
                Location = new Point(392, 9),
                UseVisualStyleBackColor = true
            };
            limpiarBtn.Click += (_, _) =>
            {
                _desdeCheckBox.Checked = false;
                _hastaCheckBox.Checked = false;
                AplicarFiltroFecha();
            };

            this.Controls.AddRange(new Control[]
            {
                _desdeCheckBox, _desdePicker,
                _hastaCheckBox, _hastaPicker,
                limpiarBtn
            });
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

        private async void NoticiaLista_Load(object sender, EventArgs e)
        {
            await this.GetCategoriesAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            NoticiaDTO noticiaNueva = new NoticiaDTO();
            NoticiaDetalle noticiaDetalle = new NoticiaDetalle(FormMode.Add, noticiaNueva);

            noticiaDetalle.ShowDialog();

            await this.GetCategoriesAndLoad();
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

                await this.GetCategoriesAndLoad();
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
                    await this.GetCategoriesAndLoad();
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

        private async Task GetCategoriesAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.noticiaGridView.DataSource = null;

                var noticias = await NoticiaApiClient.GetAllAsync();
                _todasNoticias = noticias?.ToList() ?? new List<NoticiaDTO>();

                AplicarFiltroFecha();
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

        private void AplicarFiltroFecha()
        {
            IEnumerable<NoticiaDTO> query = _todasNoticias;
            if (_desdeCheckBox.Checked)
            {
                DateTime desde = _desdePicker.Value.Date;
                query = query.Where(n => n.FechaPublicacion.Date >= desde);
            }
            if (_hastaCheckBox.Checked)
            {
                DateTime hasta = _hastaPicker.Value.Date;
                query = query.Where(n => n.FechaPublicacion.Date <= hasta);
            }

            this.noticiaGridView.DataSource = query.ToList();

            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool canUpdate = esInvestigador && (actualizarButton.Tag is bool updatePermission ? updatePermission : true);
            bool canDelete = esInvestigador && (eliminarButton.Tag is bool deletePermission ? deletePermission : true);

            if (this.noticiaGridView.Rows.Count > 0)
            {
                this.noticiaGridView.Rows[0].Selected = true;

                if (canDelete) this.eliminarButton.Enabled = true;
                if (canUpdate) this.actualizarButton.Enabled = true;
            }
            else
            {
                this.eliminarButton.Enabled = false;
                this.actualizarButton.Enabled = false;
            }
        }

        private NoticiaDTO SelectedItem()
        {
            NoticiaDTO noticia;

            noticia = (NoticiaDTO)noticiaGridView.SelectedRows[0].DataBoundItem;
            return noticia;
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            noticiaGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            agregarButton.Enabled = esInvestigador;
            noticiaGridView.Enabled = true;
        }
    }
}
