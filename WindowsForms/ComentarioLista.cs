using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ComentarioLista : Form
    {
        private List<BestiaDTO> bestias = new();
        private List<ComentarioDTO> todosComentarios = new();

        public ComentarioLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.comentariosDataGridView.AutoGenerateColumns = false;

            this.comentariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Bestia",
                HeaderText = "Bestia",
                DataPropertyName = "BestiaTexto",
                Width = 140
            });

            this.comentariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo",
                Width = 170
            });

            this.comentariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Autor",
                HeaderText = "Autor",
                DataPropertyName = "CorreoUsuarioTexto",
                Width = 150
            });

            this.comentariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 85
            });

            this.comentariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAprobacion",
                HeaderText = "Fecha Aprobación",
                DataPropertyName = "FechaAprobacionTexto",
                Width = 130
            });

            this.comentariosDataGridView.ReadOnly = true;
            this.comentariosDataGridView.AllowUserToAddRows = false;
            this.comentariosDataGridView.AllowUserToDeleteRows = false;
            this.comentariosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.comentariosDataGridView.MultiSelect = false;
        }

        private async void ComentarioLista_Load(object sender, EventArgs e)
        {
            await CargarBestiasFiltro();
            await CargarComentarios();
        }

        private async Task CargarBestiasFiltro()
        {
            try
            {
                var beasts = await BestiaApiClient.GetAllAsync();
                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                if (!esInvestigador)
                {
                    beasts = beasts?.Where(b => b.Estado != null && b.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                bestias = beasts?.ToList() ?? new List<BestiaDTO>();

                this.bestiasComboBox.Items.Clear();
                this.bestiasComboBox.Items.Add(new BestiaComboItem { Id = Guid.Empty, Nombre = "-- Todas las Bestias --" });
                foreach (var b in bestias)
                {
                    this.bestiasComboBox.Items.Add(new BestiaComboItem { Id = b.Id, Nombre = $"{b.Nombre} ({b.Peligrosidad})" });
                }
                this.bestiasComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bestias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarComentarios()
        {
            try
            {
                DeshabilitarControles();
                this.comentariosDataGridView.DataSource = null;

                var result = await ComentarioApiClient.GetAllAsync();
                todosComentarios = result?.ToList() ?? new List<ComentarioDTO>();

                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                if (!esInvestigador)
                {
                    todosComentarios = todosComentarios.Where(c => c.Estado != null && c.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }

                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar comentarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void AplicarFiltro()
        {
            var selectedItem = this.bestiasComboBox.SelectedItem as BestiaComboItem;
            Guid filtroId = selectedItem?.Id ?? Guid.Empty;

            IEnumerable<ComentarioDTO> filtrados = todosComentarios;
            if (filtroId != Guid.Empty)
            {
                filtrados = filtrados.Where(c => c.IdBestia == filtroId);
            }

            var vmList = filtrados.Select(c => new ComentarioVM
            {
                Id = c.Id,
                IdBestia = c.IdBestia,
                BestiaNombre = c.BestiaNombre,
                IdUsuario = c.IdUsuario,
                CorreoUsuario = c.CorreoUsuario,
                Titulo = c.Titulo,
                Contenido = c.Contenido,
                Estado = c.Estado,
                FechaAprobacion = c.FechaAprobacion,
                BestiaTexto = c.BestiaNombre ?? bestias.FirstOrDefault(b => b.Id == c.IdBestia)?.Nombre ?? "-",
                CorreoUsuarioTexto = c.CorreoUsuario ?? "-",
                FechaAprobacionTexto = c.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-"
            }).ToList();

            this.comentariosDataGridView.DataSource = vmList;
            ActualizarEstadoBotones();
        }

        private void bestiasComboBox_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltro();
        private void comentariosDataGridView_SelectionChanged(object sender, EventArgs e) => ActualizarEstadoBotones();

        private void ActualizarEstadoBotones()
        {
            bool estaLogueado = LoginForm.UsuarioLogueado != null;
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayFilas = this.comentariosDataGridView.SelectedRows.Count > 0;

            this.agregarButton.Enabled = estaLogueado;
            this.eliminarButton.Enabled = esInvestigador && hayFilas;
            this.actualizarButton.Enabled = hayFilas;
            this.actualizarButton.Text = esInvestigador ? "Ver / Editar" : "Ver Detalle";

            if (hayFilas && esInvestigador)
            {
                var vm = (ComentarioVM)comentariosDataGridView.SelectedRows[0].DataBoundItem;
                this.aprobarButton.Enabled = vm.Estado == "pendiente";
            }
            else
            {
                this.aprobarButton.Enabled = false;
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var selectedItem = this.bestiasComboBox.SelectedItem as BestiaComboItem;
            Guid bestiaPredeterminada = selectedItem != null && selectedItem.Id != Guid.Empty
                ? selectedItem.Id
                : (bestias.FirstOrDefault()?.Id ?? Guid.Empty);

            var nuevo = new ComentarioDTO
            {
                IdBestia = bestiaPredeterminada,
                IdUsuario = LoginForm.UsuarioLogueado?.Id ?? Guid.Empty,
                Estado = "pendiente"
            };

            ComentarioDetalle detalle = new ComentarioDetalle(FormMode.Add, nuevo, bestias);
            detalle.ShowDialog();
            await CargarComentarios();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.comentariosDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                var vm = (ComentarioVM)comentariosDataGridView.SelectedRows[0].DataBoundItem;
                var fresco = await ComentarioApiClient.GetAsync(vm.Id);
                if (fresco != null)
                {
                    ComentarioDetalle detalle = new ComentarioDetalle(FormMode.Update, fresco, bestias);
                    detalle.ShowDialog();
                    await CargarComentarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.comentariosDataGridView.SelectedRows.Count == 0) return;
            var vm = (ComentarioVM)comentariosDataGridView.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar el comentario '{vm.Titulo}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await ComentarioApiClient.DeleteAsync(vm.Id);
                    await CargarComentarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async void aprobarButton_Click(object sender, EventArgs e)
        {
            if (this.comentariosDataGridView.SelectedRows.Count == 0) return;
            var vm = (ComentarioVM)comentariosDataGridView.SelectedRows[0].DataBoundItem;

            try
            {
                DeshabilitarControles();
                await ComentarioApiClient.AprobarAsync(vm.Id);
                MessageBox.Show($"Comentario '{vm.Titulo}' aprobado con éxito.", "Aprobación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarComentarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            aprobarButton.Enabled = false;
            comentariosDataGridView.Enabled = false;
            bestiasComboBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            comentariosDataGridView.Enabled = true;
            bestiasComboBox.Enabled = true;
            ActualizarEstadoBotones();
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }

        private class ComentarioVM : ComentarioDTO
        {
            public string BestiaTexto { get; set; } = string.Empty;
            public string CorreoUsuarioTexto { get; set; } = string.Empty;
            public string FechaAprobacionTexto { get; set; } = string.Empty;
        }
    }
}
