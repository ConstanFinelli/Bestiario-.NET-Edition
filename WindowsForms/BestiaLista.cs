using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class BestiaLista : Form
    {
        private static readonly HttpClient _httpClient = new();
        private List<BestiaVM> _todasBestias = new();
        private ComboBox _categoriaComboBox = null!;
        private ComboBox _habitatComboBox = null!;
        private Button _verPendientesButton = null!;
        private bool _cargandoFiltro;

        public BestiaLista()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            InicializarFiltros();
            ConfigurarColumnas();
            this.bestiasDataGridView.CellDoubleClick += async (_, _) => await AbrirUltimoRegistroSeleccionadoAsync();
        }

        private void InicializarFiltros()
        {
            this.bestiasDataGridView.Top += 34;
            this.bestiasDataGridView.Height -= 34;

            var filtroPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Location = new Point(12, 8),
                Size = new Size(760, 32),
                BackColor = ThemeHelper.Bone
            };

            var lblCat = new Label
            {
                Text = "Categoría:",
                AutoSize = true,
                Margin = new Padding(0, 6, 6, 0),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            _categoriaComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 2, 18, 0),
                Width = 200
            };
            _categoriaComboBox.SelectedIndexChanged += (_, _) =>
            {
                if (!_cargandoFiltro) AplicarFiltros();
            };

            var lblHab = new Label
            {
                Text = "Hábitat:",
                AutoSize = true,
                Margin = new Padding(0, 6, 6, 0),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            _habitatComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 2, 0, 0),
                Width = 200
            };
            _habitatComboBox.SelectedIndexChanged += (_, _) =>
            {
                if (!_cargandoFiltro) AplicarFiltros();
            };

            filtroPanel.Controls.Add(lblCat);
            filtroPanel.Controls.Add(_categoriaComboBox);
            filtroPanel.Controls.Add(lblHab);
            filtroPanel.Controls.Add(_habitatComboBox);
            this.Controls.Add(filtroPanel);

            int btnY = this.verRegistrosButton.Top;
            int btnHeight = this.verRegistrosButton.Height;

            var volverBtn = new Button
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(90, btnHeight),
                Height = btnHeight,
                Padding = new Padding(8, 0, 8, 0),
                Location = new Point(12, btnY),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            ThemeHelper.StyleBackButton(volverBtn);
            volverBtn.Click += (_, _) => this.Close();
            this.Controls.Add(volverBtn);
            volverBtn.BringToFront();

            this.verRegistrosButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.verRegistrosButton.Location = new Point(this.eliminarButton.Left - this.verRegistrosButton.Width - 6, btnY);
            this.verRegistrosButton.BringToFront();

            _verPendientesButton = new Button
            {
                Text = "Ver registro pendientes",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(155, btnHeight),
                Height = btnHeight,
                Padding = new Padding(8, 0, 8, 0),
                Location = new Point(volverBtn.Right + 8, btnY),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                UseVisualStyleBackColor = true,
                Visible = false
            };
            _verPendientesButton.Click += VerPendientesButton_Click;
            this.Controls.Add(_verPendientesButton);
            _verPendientesButton.BringToFront();
        }

        private void VerPendientesButton_Click(object? sender, EventArgs e)
        {
            using var pendientesForm = new RegistrosPendientesForm(_todasBestias.Cast<BestiaDTO>().ToList());
            ThemeHelper.ShowSingleWindow(this, pendientesForm);
        }

        private void ConfigurarColumnas()
        {
            this.bestiasDataGridView.AutoGenerateColumns = false;
            this.bestiasDataGridView.RowTemplate.Height = 52;

            this.bestiasDataGridView.Columns.Add(new DataGridViewImageColumn
            {
                Name = "Foto",
                HeaderText = "Foto",
                DataPropertyName = "Foto",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 56,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    NullValue = null,
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(2)
                }
            });

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
                Name = "Habitat",
                HeaderText = "Hábitat",
                DataPropertyName = "HabitatTexto",
                Width = 140
            });

            this.bestiasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categorias",
                HeaderText = "Categorías",
                DataPropertyName = "CategoriasTexto",
                Width = 180
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
            using var bestiaDetalle = new BestiaDetalle(FormMode.Add, bestiaNueva);

            ThemeHelper.ShowSingleWindow(this, bestiaDetalle);

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
                    using var bestiaDetalle = new BestiaDetalle(FormMode.Update, bestia);
                    ThemeHelper.ShowSingleWindow(this, bestiaDetalle);
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

        private async void verRegistrosButton_Click(object sender, EventArgs e)
        {
            if (this.bestiasDataGridView.SelectedRows.Count == 0) return;

            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

            if (esInvestigador)
            {
                BestiaDTO bestia = this.SelectedItem();
                using var registrosForm = new RegistroLista(bestia.Id);
                ThemeHelper.ShowSingleWindow(this, registrosForm);
                return;
            }

            await AbrirUltimoRegistroSeleccionadoAsync();
        }

        private async Task AbrirUltimoRegistroSeleccionadoAsync()
        {
            if (this.bestiasDataGridView.SelectedRows.Count == 0) return;
            BestiaDTO bestia = this.SelectedItem();

            try
            {
                DeshabilitarControles();
                var vigente = await RegistroApiClient.GetVigenteAsync(bestia.Id);
                if (vigente == null)
                {
                    if (LoginForm.UsuarioLogueado != null)
                    {
                        var resp = MessageBox.Show(
                            $"Aún no existen registros aprobados para '{bestia.Nombre}'. ¿Desea proponer el primer registro?",
                            "Sin registros aprobados",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (resp == DialogResult.Yes)
                        {
                            bool esInv = LoginForm.UsuarioLogueado is InvestigadorDTO
                                || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                            var nuevo = new RegistroDTO
                            {
                                IdBestia = bestia.Id,
                                Estado = esInv ? "aprobado" : "pendiente",
                                Contenidos = new List<ContenidoRegistroDTO>()
                            };
                            using var det = new RegistroDetalle(FormMode.Add, nuevo, new List<BestiaDTO> { bestia });
                            ThemeHelper.ShowSingleWindow(this, det);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aún no existen registros aprobados para esta bestia.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                using var enciclopedia = new RegistroEnciclopediaForm(vigente, bestia, new List<BestiaDTO> { bestia });
                ThemeHelper.ShowSingleWindow(this, enciclopedia);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar registro de la bestia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async Task GetBestiasAndLoad()
        {
            try
            {
                DeshabilitarControles();
                this.bestiasDataGridView.DataSource = null;

                var cats = await CategoriaApiClient.GetAllAsync();
                var habs = await HabitatApiClient.GetAllAsync();
                Guid? prevCatId = (_categoriaComboBox.SelectedItem as FiltroItem)?.Id;
                Guid? prevHabId = (_habitatComboBox.SelectedItem as FiltroItem)?.Id;

                _cargandoFiltro = true;
                try
                {
                    _categoriaComboBox.Items.Clear();
                    _categoriaComboBox.Items.Add(new FiltroItem { Id = null, Nombre = "-- Todas --" });
                    int selectedCatIdx = 0;
                    if (cats != null)
                    {
                        foreach (var c in cats.OrderBy(x => x.Nombre))
                        {
                            int idx = _categoriaComboBox.Items.Add(new FiltroItem { Id = c.Id, Nombre = c.Nombre });
                            if (prevCatId.HasValue && prevCatId.Value == c.Id)
                            {
                                selectedCatIdx = idx;
                            }
                        }
                    }
                    _categoriaComboBox.SelectedIndex = selectedCatIdx;

                    _habitatComboBox.Items.Clear();
                    _habitatComboBox.Items.Add(new FiltroItem { Id = null, Nombre = "-- Todos --" });
                    int selectedHabIdx = 0;
                    if (habs != null)
                    {
                        foreach (var h in habs.OrderBy(x => x.Nombre))
                        {
                            int idx = _habitatComboBox.Items.Add(new FiltroItem { Id = h.Id, Nombre = h.Nombre });
                            if (prevHabId.HasValue && prevHabId.Value == h.Id)
                            {
                                selectedHabIdx = idx;
                            }
                        }
                    }
                    _habitatComboBox.SelectedIndex = selectedHabIdx;
                }
                finally
                {
                    _cargandoFiltro = false;
                }

                var result = await BestiaApiClient.GetAllAsync();
                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                if (!esInvestigador)
                {
                    result = result?.Where(b => b.Estado != null && b.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)).ToList();
                }

                _todasBestias = result?.Select(b => new BestiaVM
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Peligrosidad = b.Peligrosidad,
                    Estado = b.Estado,
                    IdHabitat = b.IdHabitat,
                    HabitatNombre = b.HabitatNombre,
                    ImagenUrl = b.ImagenUrl,
                    HabitatTexto = string.IsNullOrEmpty(b.HabitatNombre) ? "-" : b.HabitatNombre,
                    CategoriasIds = b.CategoriasIds,
                    CategoriasNombres = b.CategoriasNombres,
                    CategoriasTexto = b.CategoriasNombres.Any() ? string.Join(", ", b.CategoriasNombres) : "-"
                }).ToList() ?? new List<BestiaVM>();

                AplicarFiltros();
                await CargarFotosAsync(_todasBestias);
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

        private void AplicarFiltros()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            Guid? catId = (_categoriaComboBox.SelectedItem as FiltroItem)?.Id;
            Guid? habId = (_habitatComboBox.SelectedItem as FiltroItem)?.Id;

            IEnumerable<BestiaVM> query = _todasBestias;
            if (catId.HasValue)
            {
                query = query.Where(b => b.CategoriasIds.Contains(catId.Value));
            }
            if (habId.HasValue)
            {
                query = query.Where(b => b.IdHabitat == habId.Value);
            }

            this.bestiasDataGridView.DataSource = query.ToList();

            if (this.bestiasDataGridView.Rows.Count > 0)
            {
                this.bestiasDataGridView.Rows[0].Selected = true;
                this.eliminarButton.Enabled = esInvestigador;
                this.actualizarButton.Enabled = esInvestigador;
                this.verRegistrosButton.Enabled = true;
            }
            else
            {
                this.eliminarButton.Enabled = false;
                this.actualizarButton.Enabled = false;
                this.verRegistrosButton.Enabled = false;
            }
        }

        private class FiltroItem
        {
            public Guid? Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
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
                IdHabitat = vm.IdHabitat,
                HabitatNombre = vm.HabitatNombre,
                ImagenUrl = vm.ImagenUrl,
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
            _verPendientesButton.Enabled = false;
            bestiasDataGridView.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            agregarButton.Visible = esInvestigador;
            actualizarButton.Visible = esInvestigador;
            eliminarButton.Visible = esInvestigador;
            _verPendientesButton.Visible = esInvestigador;
            _verPendientesButton.Enabled = esInvestigador;
            agregarButton.Enabled = esInvestigador;
            bestiasDataGridView.Enabled = true;
            if (bestiasDataGridView.Rows.Count > 0)
            {
                actualizarButton.Enabled = esInvestigador;
                eliminarButton.Enabled = esInvestigador;
                verRegistrosButton.Enabled = true;
            }
        }

        private async Task CargarFotosAsync(List<BestiaVM> items)
        {
            var urls = items
                .Select(i => i.ImagenUrl)
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (!urls.Any()) return;

            var cache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
            var tasks = urls.Select(async url =>
            {
                try
                {
                    var bytes = await _httpClient.GetByteArrayAsync(url);
                    lock (cache)
                    {
                        cache[url!] = bytes;
                    }
                }
                catch
                {
                }
            });

            await Task.WhenAll(tasks);

            if (IsDisposed) return;

            foreach (var item in items)
            {
                if (!string.IsNullOrWhiteSpace(item.ImagenUrl) && cache.TryGetValue(item.ImagenUrl, out var bytes))
                {
                    try
                    {
                        using var ms = new MemoryStream(bytes);
                        using var img = Image.FromStream(ms);
                        item.Foto = new Bitmap(img);
                    }
                    catch
                    {
                    }
                }
            }

            bestiasDataGridView.Refresh();
        }

        private class BestiaVM : BestiaDTO
        {
            public Image? Foto { get; set; }
            public string CategoriasTexto { get; set; } = string.Empty;
            public string HabitatTexto { get; set; } = string.Empty;
        }
    }
}
