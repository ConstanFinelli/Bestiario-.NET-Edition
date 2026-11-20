using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class UsuarioLista : Form
    {
        public UsuarioLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.usuariosDataGridView.AutoGenerateColumns = false;

            this.usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Correo",
                HeaderText = "Correo",
                DataPropertyName = "Correo",
                Width = 210
            });

            this.usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoUsuario",
                HeaderText = "Tipo",
                DataPropertyName = "TipoUsuario",
                Width = 100
            });

            this.usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCompleto",
                HeaderText = "Nombre y Apellido",
                DataPropertyName = "NombreCompleto",
                Width = 190
            });

            this.usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dni",
                HeaderText = "DNI",
                DataPropertyName = "Dni",
                Width = 100
            });

            this.usuariosDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "RecibirNotificaciones",
                HeaderText = "Notificaciones",
                DataPropertyName = "RecibirNotificaciones",
                Width = 95
            });

            this.usuariosDataGridView.ReadOnly = true;
            this.usuariosDataGridView.AllowUserToAddRows = false;
            this.usuariosDataGridView.AllowUserToDeleteRows = false;
            this.usuariosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.usuariosDataGridView.MultiSelect = false;
        }

        private async void UsuarioLista_Load(object sender, EventArgs e)
        {
            await GetUsuariosAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var nuevo = new InvestigadorDTO();
            var detalle = new UsuarioDetalle(FormMode.Add, nuevo);
            detalle.ShowDialog();
            await GetUsuariosAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.usuariosDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                var vm = (UsuarioVM)usuariosDataGridView.SelectedRows[0].DataBoundItem;
                var usuario = await UsuarioApiClient.GetAsync(vm.Id);
                if (usuario != null)
                {
                    var detalle = new UsuarioDetalle(FormMode.Update, usuario);
                    detalle.ShowDialog();
                    await GetUsuariosAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.usuariosDataGridView.SelectedRows.Count == 0) return;
            var vm = (UsuarioVM)usuariosDataGridView.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar el usuario '{vm.Correo}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await UsuarioApiClient.DeleteAsync(vm.Id);
                    await GetUsuariosAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async Task GetUsuariosAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.usuariosDataGridView.DataSource = null;

                var result = await UsuarioApiClient.GetAllAsync();
                var list = result?.Select(u => new UsuarioVM
                {
                    Id = u.Id,
                    Correo = u.Correo,
                    TipoUsuario = u.TipoUsuario,
                    RecibirNotificaciones = u.RecibirNotificaciones,
                    NombreCompleto = u is InvestigadorDTO inv ? $"{inv.Nombre} {inv.Apellido}".Trim() : "-",
                    Dni = u is InvestigadorDTO inv2 ? inv2.Dni : "-"
                }).ToList() ?? new List<UsuarioVM>();

                this.usuariosDataGridView.DataSource = list;

                if (this.usuariosDataGridView.Rows.Count > 0)
                {
                    this.usuariosDataGridView.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            usuariosDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayFilas = usuariosDataGridView.Rows.Count > 0;
            agregarButton.Enabled = esInvestigador;
            usuariosDataGridView.Enabled = true;
            actualizarButton.Enabled = esInvestigador && hayFilas;
            eliminarButton.Enabled = esInvestigador && hayFilas;
        }

        private class UsuarioVM
        {
            public Guid Id { get; set; }
            public string Correo { get; set; } = string.Empty;
            public string TipoUsuario { get; set; } = string.Empty;
            public string NombreCompleto { get; set; } = string.Empty;
            public string Dni { get; set; } = string.Empty;
            public bool RecibirNotificaciones { get; set; }
        }
    }
}

