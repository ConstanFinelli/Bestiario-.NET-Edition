using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public class RegistrosPendientesForm : Form
    {
        private readonly List<BestiaDTO> _bestias;
        private List<RegistroDTO> _pendientes = new();
        private ComboBox _filtroBestiaCombo = null!;
        private Button _quitarFiltroBtn = null!;
        private DataGridView _grid = null!;
        private Button _examinarBtn = null!;
        private Button _aceptarBtn = null!;
        private Button _rechazarBtn = null!;
        private Label _statusLabel = null!;

        public RegistrosPendientesForm(List<BestiaDTO> bestias)
        {
            _bestias = bestias ?? new List<BestiaDTO>();
            ConfigurarFormulario();
            ConstruirContenido();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            this.Load += async (_, _) => await CargarPendientesAsync();
        }

        private void ConfigurarFormulario()
        {
            Text = "Registros Pendientes";
            ClientSize = new Size(760, 440);
            MinimumSize = new Size(640, 380);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 9F);
        }

        private void ConstruirContenido()
        {
            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(12, 8, 12, 6),
                BackColor = ThemeHelper.Parchment
            };

            var lblFiltro = new Label
            {
                Text = "Filtrar por Bestia:",
                AutoSize = true,
                Location = new Point(12, 13),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            _filtroBestiaCombo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(132, 10),
                Width = 250
            };
            _filtroBestiaCombo.Items.Add(new BestiaFiltroItem { Id = null, Nombre = "-- Todas las Bestias --" });
            foreach (var b in _bestias.OrderBy(x => x.Nombre))
            {
                _filtroBestiaCombo.Items.Add(new BestiaFiltroItem { Id = b.Id, Nombre = b.Nombre });
            }
            _filtroBestiaCombo.SelectedIndex = 0;
            _filtroBestiaCombo.SelectedIndexChanged += (_, _) => AplicarFiltro();

            _quitarFiltroBtn = new Button
            {
                Text = "Quitar Filtro",
                Location = new Point(392, 9),
                Size = new Size(92, 24),
                Visible = false,
                UseVisualStyleBackColor = true
            };
            _quitarFiltroBtn.Click += (_, _) => _filtroBestiaCombo.SelectedIndex = 0;

            filterPanel.Controls.Add(lblFiltro);
            filterPanel.Controls.Add(_filtroBestiaCombo);
            filterPanel.Controls.Add(_quitarFiltroBtn);

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersWidth = 28
            };
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Bestia", HeaderText = "Bestia", DataPropertyName = "BestiaNombre", Width = 180 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nro", HeaderText = "Nro.", DataPropertyName = "NroTexto", Width = 65 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Publicador", HeaderText = "Publicador", DataPropertyName = "CorreoPublicador", Width = 240 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Secciones", HeaderText = "Secc.", DataPropertyName = "Secciones", Width = 65 });
            _grid.SelectionChanged += (_, _) => ActualizarBotones();
            _grid.CellDoubleClick += async (_, _) => await ExaminarSeleccionadoAsync();

            var gridWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 8, 12, 8)
            };
            gridWrapper.Controls.Add(_grid);

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                Padding = new Padding(12, 8, 12, 10)
            };

            _statusLabel = new Label
            {
                Text = "Cargando...",
                AutoSize = true,
                Location = new Point(110, 15),
                ForeColor = ThemeHelper.TextMuted
            };

            _examinarBtn = new Button { Text = "Examinar", Size = new Size(88, 28), Location = new Point(372, 10), Enabled = false, UseVisualStyleBackColor = true };
            _examinarBtn.Click += async (_, _) => await ExaminarSeleccionadoAsync();

            _aceptarBtn = new Button { Text = "Aceptar", Size = new Size(84, 28), Location = new Point(466, 10), ForeColor = Color.DarkGreen, Enabled = false, UseVisualStyleBackColor = true };
            _aceptarBtn.Click += async (_, _) => await AceptarSeleccionadoAsync();

            _rechazarBtn = new Button { Text = "Rechazar", Size = new Size(84, 28), Location = new Point(556, 10), ForeColor = Color.DarkRed, Enabled = false, UseVisualStyleBackColor = true };
            _rechazarBtn.Click += async (_, _) => await RechazarSeleccionadoAsync();

            var cerrarBtn = new Button { Size = new Size(90, 28), Location = new Point(12, 10) };
            ThemeHelper.StyleBackButton(cerrarBtn);
            cerrarBtn.Click += (_, _) => Close();

            bottomPanel.Controls.Add(_statusLabel);
            bottomPanel.Controls.Add(_examinarBtn);
            bottomPanel.Controls.Add(_aceptarBtn);
            bottomPanel.Controls.Add(_rechazarBtn);
            bottomPanel.Controls.Add(cerrarBtn);

            Controls.Add(gridWrapper);
            Controls.Add(filterPanel);
            Controls.Add(bottomPanel);
        }

        private async Task CargarPendientesAsync()
        {
            try
            {
                _statusLabel.Text = "Cargando registros pendientes...";
                DeshabilitarAcciones();

                var acumulados = new List<RegistroDTO>();
                foreach (var b in _bestias)
                {
                    var regs = await RegistroApiClient.GetByBestiaAsync(b.Id);
                    if (regs != null)
                    {
                        acumulados.AddRange(regs.Where(r => r.Estado != null && r.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase)));
                    }
                }

                _pendientes = acumulados
                    .OrderBy(r => _bestias.FirstOrDefault(b => b.Id == r.IdBestia)?.Nombre)
                    .ThenBy(r => r.NroRegistro)
                    .ToList();

                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar registros pendientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "Error al cargar pendientes.";
            }
        }

        private void AplicarFiltro()
        {
            Guid? filtroId = (_filtroBestiaCombo.SelectedItem as BestiaFiltroItem)?.Id;
            _quitarFiltroBtn.Visible = filtroId.HasValue;

            IEnumerable<RegistroDTO> query = _pendientes;
            if (filtroId.HasValue)
            {
                query = query.Where(r => r.IdBestia == filtroId.Value);
            }

            var vms = query.Select(r => new PendienteVM
            {
                Registro = r,
                BestiaNombre = _bestias.FirstOrDefault(b => b.Id == r.IdBestia)?.Nombre ?? r.IdBestia.ToString().Substring(0, 8),
                NroTexto = $"#{r.NroRegistro}",
                CorreoPublicador = string.IsNullOrWhiteSpace(r.CorreoPublicador) ? "-" : r.CorreoPublicador,
                Secciones = r.Contenidos?.Count ?? 0
            }).ToList();

            _grid.DataSource = vms;
            if (_grid.Rows.Count > 0)
            {
                _grid.Rows[0].Selected = true;
            }

            _statusLabel.Text = vms.Count == 0
                ? (filtroId.HasValue ? "Sin pendientes para esta bestia." : "No hay registros pendientes.")
                : $"{vms.Count} registro(s) pendiente(s)";

            ActualizarBotones();
        }

        private RegistroDTO? GetSelectedRegistro()
        {
            if (_grid.SelectedRows.Count == 0) return null;
            return (_grid.SelectedRows[0].DataBoundItem as PendienteVM)?.Registro;
        }

        private void DeshabilitarAcciones()
        {
            _examinarBtn.Enabled = false;
            _aceptarBtn.Enabled = false;
            _rechazarBtn.Enabled = false;
        }

        private void ActualizarBotones()
        {
            bool hasSelection = GetSelectedRegistro() != null;
            _examinarBtn.Enabled = hasSelection;
            _aceptarBtn.Enabled = hasSelection;
            _rechazarBtn.Enabled = hasSelection;
        }

        private async Task ExaminarSeleccionadoAsync()
        {
            var reg = GetSelectedRegistro();
            if (reg == null) return;

            var bestia = _bestias.FirstOrDefault(b => b.Id == reg.IdBestia);
            using var enciclopedia = new RegistroEnciclopediaForm(reg, bestia, _bestias);
            ThemeHelper.ShowSingleWindow(this, enciclopedia);
            await CargarPendientesAsync();
        }

        private async Task AceptarSeleccionadoAsync()
        {
            var reg = GetSelectedRegistro();
            if (reg == null || LoginForm.UsuarioLogueado == null) return;

            try
            {
                DeshabilitarAcciones();
                await RegistroApiClient.AprobarAsync(reg.IdBestia, reg.NroRegistro, LoginForm.UsuarioLogueado.Id);
                await CargarPendientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarBotones();
            }
        }

        private async Task RechazarSeleccionadoAsync()
        {
            var reg = GetSelectedRegistro();
            if (reg == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea rechazar y eliminar el registro #{reg.NroRegistro}?",
                "Confirmar rechazo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                DeshabilitarAcciones();
                await RegistroApiClient.DeleteAsync(reg.IdBestia, reg.NroRegistro);
                await CargarPendientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarBotones();
            }
        }

        private class BestiaFiltroItem
        {
            public Guid? Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }

        private class PendienteVM
        {
            public RegistroDTO Registro { get; set; } = null!;
            public string BestiaNombre { get; set; } = string.Empty;
            public string NroTexto { get; set; } = string.Empty;
            public string CorreoPublicador { get; set; } = string.Empty;
            public int Secciones { get; set; }
        }
    }
}
