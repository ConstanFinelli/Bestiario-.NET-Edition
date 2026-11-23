using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class EvidenciaLista : Form
    {
        private Guid? idBestiaInicial;
        private List<BestiaDTO> bestias = new();
        private List<EvidenciaDTO> todasEvidencias = new();

        public EvidenciaLista()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            ConfigurarColumnas();
        }

        public EvidenciaLista(Guid idBestia) : this()
        {
            idBestiaInicial = idBestia;
        }

        private void ConfigurarColumnas()
        {
            this.evidenciasDataGridView.AutoGenerateColumns = false;

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Bestia",
                HeaderText = "Bestia",
                DataPropertyName = "BestiaNombre",
                Width = 140
            });

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroEvidencia",
                HeaderText = "Nro.",
                DataPropertyName = "NroEvidencia",
                Width = 55
            });

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoEvidencia",
                HeaderText = "Tipo Evidencia",
                DataPropertyName = "TipoEvidenciaTexto",
                Width = 170
            });

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileId",
                HeaderText = "FileId",
                DataPropertyName = "FileId",
                Width = 170
            });

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 85
            });

            this.evidenciasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaObtencion",
                HeaderText = "Fecha Obtención",
                DataPropertyName = "FechaObtencionTexto",
                Width = 130
            });

            this.evidenciasDataGridView.ReadOnly = true;
            this.evidenciasDataGridView.AllowUserToAddRows = false;
            this.evidenciasDataGridView.AllowUserToDeleteRows = false;
            this.evidenciasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.evidenciasDataGridView.MultiSelect = false;
        }

        private async void EvidenciaLista_Load(object sender, EventArgs e)
        {
            await CargarBestiasFiltro();
            await CargarEvidencias();
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

                int indexToSelect = 0;
                int curr = 1;
                foreach (var b in bestias)
                {
                    this.bestiasComboBox.Items.Add(new BestiaComboItem { Id = b.Id, Nombre = $"{b.Nombre} ({b.Peligrosidad})" });
                    if (idBestiaInicial.HasValue && b.Id == idBestiaInicial.Value)
                    {
                        indexToSelect = curr;
                    }
                    curr++;
                }

                this.bestiasComboBox.SelectedIndex = indexToSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listado de bestias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarEvidencias()
        {
            try
            {
                DeshabilitarControles();
                this.evidenciasDataGridView.DataSource = null;
                todasEvidencias.Clear();

                foreach (var b in bestias)
                {
                    var items = await EvidenciaApiClient.GetByBestiaAsync(b.Id);
                    if (items != null)
                    {
                        todasEvidencias.AddRange(items);
                    }
                }

                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                if (!esInvestigador)
                {
                    todasEvidencias = todasEvidencias.Where(e => e.Estado != null && e.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }

                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar evidencias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            IEnumerable<EvidenciaDTO> filtradas = todasEvidencias;
            if (filtroId != Guid.Empty)
            {
                filtradas = filtradas.Where(e => e.IdBestia == filtroId);
            }

            var vmList = filtradas.Select(e => new EvidenciaVM
            {
                IdBestia = e.IdBestia,
                NroEvidencia = e.NroEvidencia,
                Estado = e.Estado,
                FechaObtencion = e.FechaObtencion,
                FileId = e.FileId,
                IdTipoEvidencia = e.IdTipoEvidencia,
                TipoEvidenciaDescripcion = e.TipoEvidenciaDescripcion,
                TipoEvidenciaResourceType = e.TipoEvidenciaResourceType,
                BestiaNombre = bestias.FirstOrDefault(b => b.Id == e.IdBestia)?.Nombre ?? e.IdBestia.ToString().Substring(0, 8),
                TipoEvidenciaTexto = string.IsNullOrEmpty(e.TipoEvidenciaDescripcion) ? "-" : $"{e.TipoEvidenciaDescripcion} ({e.TipoEvidenciaResourceType})",
                FechaObtencionTexto = e.FechaObtencion.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            this.evidenciasDataGridView.DataSource = vmList;
            ActualizarEstadoBotones();
        }

        private void bestiasComboBox_SelectedIndexChanged(object sender, EventArgs e) => AplicarFiltro();
        private void evidenciasDataGridView_SelectionChanged(object sender, EventArgs e) => ActualizarEstadoBotones();

        private void ActualizarEstadoBotones()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayFilas = this.evidenciasDataGridView.SelectedRows.Count > 0;
            this.eliminarButton.Enabled = esInvestigador && hayFilas;
            this.actualizarButton.Enabled = hayFilas;
            this.actualizarButton.Text = esInvestigador ? "Ver / Editar" : "Ver Detalle";

            if (hayFilas && esInvestigador)
            {
                var vm = (EvidenciaVM)evidenciasDataGridView.SelectedRows[0].DataBoundItem;
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

            var nueva = new EvidenciaDTO
            {
                IdBestia = bestiaPredeterminada,
                Estado = "pendiente",
                FechaObtencion = DateTime.Now
            };

            using var detalle = new EvidenciaDetalle(FormMode.Add, nueva, bestias);
            ThemeHelper.ShowSingleWindow(this, detalle);
            await CargarEvidencias();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.evidenciasDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                var vm = (EvidenciaVM)evidenciasDataGridView.SelectedRows[0].DataBoundItem;
                var fresca = await EvidenciaApiClient.GetAsync(vm.IdBestia, vm.NroEvidencia);
                if (fresca != null)
                {
                    using var detalle = new EvidenciaDetalle(FormMode.Update, fresca, bestias);
                    ThemeHelper.ShowSingleWindow(this, detalle);
                    await CargarEvidencias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.evidenciasDataGridView.SelectedRows.Count == 0) return;
            var vm = (EvidenciaVM)evidenciasDataGridView.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la evidencia #{vm.NroEvidencia} de '{vm.BestiaNombre}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await EvidenciaApiClient.DeleteAsync(vm.IdBestia, vm.NroEvidencia);
                    await CargarEvidencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async void aprobarButton_Click(object sender, EventArgs e)
        {
            if (this.evidenciasDataGridView.SelectedRows.Count == 0) return;
            var vm = (EvidenciaVM)evidenciasDataGridView.SelectedRows[0].DataBoundItem;

            try
            {
                DeshabilitarControles();
                await EvidenciaApiClient.AprobarAsync(vm.IdBestia, vm.NroEvidencia);
                MessageBox.Show($"Evidencia #{vm.NroEvidencia} aprobada con éxito.", "Aprobación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarEvidencias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            evidenciasDataGridView.Enabled = false;
            bestiasComboBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool esLector = LoginForm.UsuarioLogueado is LectorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Lector";
            agregarButton.Enabled = esInvestigador || esLector;
            agregarButton.Text = esLector ? "Proponer" : "Agregar";
            evidenciasDataGridView.Enabled = true;
            bestiasComboBox.Enabled = true;
            ActualizarEstadoBotones();
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }

        private class EvidenciaVM : EvidenciaDTO
        {
            public string BestiaNombre { get; set; } = string.Empty;
            public string TipoEvidenciaTexto { get; set; } = string.Empty;
            public string FechaObtencionTexto { get; set; } = string.Empty;
        }
    }
}

