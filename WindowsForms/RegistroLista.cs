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
    public partial class RegistroLista : Form
    {
        private Guid? idBestiaInicial;
        private List<BestiaDTO> bestias = new();
        private List<RegistroDTO> todosRegistros = new();

        public RegistroLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
            AppTheme.ApplyFormTheme(this);
        }

        public RegistroLista(Guid idBestia) : this()
        {
            idBestiaInicial = idBestia;
        }

        private void ConfigurarColumnas()
        {
            this.registrosDataGridView.AutoGenerateColumns = false;

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Bestia",
                HeaderText = "Bestia",
                DataPropertyName = "BestiaNombre",
                Width = 140
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroRegistro",
                HeaderText = "Nro.",
                DataPropertyName = "NroRegistro",
                Width = 60
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 90
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Publicador",
                HeaderText = "Publicado por",
                DataPropertyName = "CorreoPublicador",
                Width = 150
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Aprobador",
                HeaderText = "Aprobado por",
                DataPropertyName = "NombreInvestigadorAprobador",
                Width = 140
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadContenidos",
                HeaderText = "Secciones",
                DataPropertyName = "CantidadContenidos",
                Width = 70
            });

            this.registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAprobacion",
                HeaderText = "Fecha Aprobación",
                DataPropertyName = "FechaAprobacionTexto",
                Width = 130
            });

            this.registrosDataGridView.ReadOnly = true;
            this.registrosDataGridView.AllowUserToAddRows = false;
            this.registrosDataGridView.AllowUserToDeleteRows = false;
            this.registrosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.registrosDataGridView.MultiSelect = false;
        }

        private async void RegistroLista_Load(object sender, EventArgs e)
        {
            await CargarBestiasFiltro();
            await CargarRegistros();
        }

        private async Task CargarBestiasFiltro()
        {
            try
            {
                var beasts = await BestiaApiClient.GetAllAsync();
                if (LoginForm.UsuarioLogueado?.TipoUsuario == "Lector")
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

        private async Task CargarRegistros()
        {
            try
            {
                DeshabilitarControles();
                this.registrosDataGridView.DataSource = null;
                todosRegistros.Clear();

                foreach (var b in bestias)
                {
                    var regs = await RegistroApiClient.GetByBestiaAsync(b.Id);
                    if (regs != null)
                    {
                        todosRegistros.AddRange(regs);
                    }
                }

                if (LoginForm.UsuarioLogueado?.TipoUsuario == "Lector")
                {
                    todosRegistros = todosRegistros.Where(r => r.Estado != null && r.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }

                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar registros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            IEnumerable<RegistroDTO> filtrados = todosRegistros;
            if (filtroId != Guid.Empty)
            {
                filtrados = filtrados.Where(r => r.IdBestia == filtroId);
            }

            if (LoginForm.UsuarioLogueado?.TipoUsuario == "Lector")
            {
                filtrados = filtrados.Where(r => r.Estado != null && r.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase));
            }

            var vmList = filtrados.Select(r => new RegistroVM
            {
                IdBestia = r.IdBestia,
                NroRegistro = r.NroRegistro,
                FechaAprobacion = r.FechaAprobacion,
                FechaBaja = r.FechaBaja,
                IdUsuarioPublicador = r.IdUsuarioPublicador,
                CorreoPublicador = r.CorreoPublicador ?? "-",
                IdInvestigadorAprobador = r.IdInvestigadorAprobador,
                NombreInvestigadorAprobador = r.NombreInvestigadorAprobador ?? "-",
                Estado = r.Estado,
                Contenidos = r.Contenidos,
                BestiaNombre = bestias.FirstOrDefault(b => b.Id == r.IdBestia)?.Nombre ?? r.IdBestia.ToString().Substring(0, 8),
                CantidadContenidos = r.Contenidos.Count,
                FechaAprobacionTexto = r.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-"
            }).ToList();

            this.registrosDataGridView.DataSource = vmList;

            ActualizarEstadoBotones();
        }

        private void bestiasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void registrosDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool hayFilas = this.registrosDataGridView.SelectedRows.Count > 0;
            this.eliminarButton.Enabled = hayFilas;
            this.actualizarButton.Enabled = hayFilas;

            if (hayFilas)
            {
                var vm = (RegistroVM)registrosDataGridView.SelectedRows[0].DataBoundItem;
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

            var nuevoRegistro = new RegistroDTO
            {
                IdBestia = bestiaPredeterminada,
                Estado = "pendiente",
                Contenidos = new List<ContenidoRegistroDTO>()
            };

            RegistroDetalle detalleForm = new RegistroDetalle(FormMode.Add, nuevoRegistro, bestias);
            detalleForm.ShowDialog();

            await CargarRegistros();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (this.registrosDataGridView.SelectedRows.Count == 0) return;

            try
            {
                DeshabilitarControles();
                var vm = (RegistroVM)registrosDataGridView.SelectedRows[0].DataBoundItem;

                var fresco = await RegistroApiClient.GetAsync(vm.IdBestia, vm.NroRegistro);
                if (fresco != null)
                {
                    RegistroDetalle detalleForm = new RegistroDetalle(FormMode.Update, fresco, bestias);
                    detalleForm.ShowDialog();
                    await CargarRegistros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalle del registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (this.registrosDataGridView.SelectedRows.Count == 0) return;

            var vm = (RegistroVM)registrosDataGridView.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar el registro #{vm.NroRegistro} de la bestia '{vm.BestiaNombre}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await RegistroApiClient.DeleteAsync(vm.IdBestia, vm.NroRegistro);
                    await CargarRegistros();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async void aprobarButton_Click(object sender, EventArgs e)
        {
            if (this.registrosDataGridView.SelectedRows.Count == 0) return;

            var vm = (RegistroVM)registrosDataGridView.SelectedRows[0].DataBoundItem;

            try
            {
                DeshabilitarControles();
                var idInvestigador = LoginForm.UsuarioLogueado?.Id ?? Guid.Parse("99999999-9999-9999-9999-999999999999");
                await RegistroApiClient.AprobarAsync(vm.IdBestia, vm.NroRegistro, idInvestigador);
                MessageBox.Show($"Registro #{vm.NroRegistro} aprobado con éxito.", "Aprobación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            registrosDataGridView.Enabled = false;
            bestiasComboBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            agregarButton.Enabled = true;
            registrosDataGridView.Enabled = true;
            bestiasComboBox.Enabled = true;
            ActualizarEstadoBotones();
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }

        private class RegistroVM : RegistroDTO
        {
            public string BestiaNombre { get; set; } = string.Empty;
            public int CantidadContenidos { get; set; }
            public string FechaAprobacionTexto { get; set; } = string.Empty;
        }
    }
}
