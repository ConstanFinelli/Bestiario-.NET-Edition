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
    public class SolicitudesInvestigadorForm : Form
    {
        private readonly bool _esInvestigador;
        private readonly bool _esLector;

        private Label? _bannerLabel;
        private TextBox? _nombreTextBox;
        private TextBox? _apellidoTextBox;
        private TextBox? _dniTextBox;
        private TextBox? _motivoTextBox;
        private Button? _enviarButton;

        private ComboBox? _filtroEstadoComboBox;
        private Button? _aprobarButton;
        private Button? _rechazarButton;

        private DataGridView _grid = null!;
        private Label _statusLabel = null!;
        private List<SolicitudInvestigadorDTO> _solicitudes = new();

        public SolicitudesInvestigadorForm()
        {
            var user = LoginForm.UsuarioLogueado;
            _esInvestigador = user is InvestigadorDTO || user?.TipoUsuario == "Investigador";
            _esLector = user != null && !_esInvestigador;

            InitializeUI();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            this.Load += async (_, _) => await CargarDatosAsync();
        }

        private void InitializeUI()
        {
            this.Text = _esInvestigador
                ? "Solicitudes de Lectores para Investigador"
                : "Solicitar Rol de Investigador";
            this.Size = new Size(920, 580);
            this.MinimumSize = new Size(760, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(12, 10, 12, 6)
            };

            if (_esLector)
            {
                _bannerLabel = new Label
                {
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Height = 34,
                    Visible = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 0, 8, 0),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                var formGroup = new GroupBox
                {
                    Text = "Nueva Solicitud para ser Investigador",
                    Dock = DockStyle.Top,
                    Height = 165,
                    Padding = new Padding(10)
                };

                var lblNombre = new Label { Text = "Nombre:", Location = new Point(14, 24), AutoSize = true };
                _nombreTextBox = new TextBox { Location = new Point(14, 42), Width = 240, MaxLength = 50 };

                var lblApellido = new Label { Text = "Apellido:", Location = new Point(270, 24), AutoSize = true };
                _apellidoTextBox = new TextBox { Location = new Point(270, 42), Width = 240, MaxLength = 50 };

                var lblDni = new Label { Text = "DNI:", Location = new Point(526, 24), AutoSize = true };
                _dniTextBox = new TextBox { Location = new Point(526, 42), Width = 180, MaxLength = 20 };

                var lblMotivo = new Label { Text = "Motivo / Experiencia:", Location = new Point(14, 72), AutoSize = true };
                _motivoTextBox = new TextBox
                {
                    Location = new Point(14, 90),
                    Width = 692,
                    Height = 48,
                    Multiline = true,
                    MaxLength = 500,
                    ScrollBars = ScrollBars.Vertical
                };

                _enviarButton = new Button
                {
                    Text = "Enviar Solicitud",
                    Location = new Point(724, 104),
                    Size = new Size(140, 32),
                    UseVisualStyleBackColor = true
                };
                _enviarButton.Click += async (_, _) => await EnviarSolicitudAsync();

                formGroup.Controls.AddRange(new Control[]
                {
                    lblNombre, _nombreTextBox,
                    lblApellido, _apellidoTextBox,
                    lblDni, _dniTextBox,
                    lblMotivo, _motivoTextBox,
                    _enviarButton
                });

                var lblHist = new Label
                {
                    Text = "Historial de mis solicitudes:",
                    Dock = DockStyle.Top,
                    Height = 22,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Padding = new Padding(0, 4, 0, 0)
                };

                topPanel.Controls.Add(lblHist);
                topPanel.Controls.Add(formGroup);
                topPanel.Controls.Add(_bannerLabel);
            }
            else if (_esInvestigador)
            {
                var filterBar = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    Height = 36,
                    FlowDirection = FlowDirection.LeftToRight,
                    Padding = new Padding(0, 4, 0, 0)
                };

                var lblFiltro = new Label
                {
                    Text = "Filtrar por Estado:",
                    AutoSize = true,
                    Margin = new Padding(0, 6, 8, 0),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                _filtroEstadoComboBox = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Width = 170
                };
                _filtroEstadoComboBox.Items.AddRange(new object[]
                {
                    "pendiente",
                    "Todas",
                    "aprobada",
                    "rechazada"
                });
                _filtroEstadoComboBox.SelectedIndex = 0;
                _filtroEstadoComboBox.SelectedIndexChanged += async (_, _) => await CargarDatosAsync();

                var refreshBtn = new Button
                {
                    Text = "Refrescar",
                    Size = new Size(88, 25),
                    Margin = new Padding(10, 0, 0, 0),
                    UseVisualStyleBackColor = true
                };
                refreshBtn.Click += async (_, _) => await CargarDatosAsync();

                filterBar.Controls.AddRange(new Control[] { lblFiltro, _filtroEstadoComboBox, refreshBtn });
                topPanel.Controls.Add(filterBar);
            }

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            _grid.SelectionChanged += (_, _) => ActualizarBotonesInvestigador();

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                Padding = new Padding(12, 8, 12, 8)
            };

            _statusLabel = new Label
            {
                Dock = DockStyle.Left,
                Width = 360,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DimGray,
                Text = "Cargando..."
            };
            bottomPanel.Controls.Add(_statusLabel);

            var cerrarBtn = new Button
            {
                Text = "Cerrar",
                Dock = DockStyle.Right,
                Width = 90,
                UseVisualStyleBackColor = true
            };
            cerrarBtn.Click += (_, _) => this.Close();
            bottomPanel.Controls.Add(cerrarBtn);

            if (_esInvestigador)
            {
                _rechazarButton = new Button
                {
                    Text = "Rechazar Solicitud",
                    Dock = DockStyle.Right,
                    Width = 135,
                    Enabled = false,
                    ForeColor = Color.DarkRed,
                    UseVisualStyleBackColor = true
                };
                _rechazarButton.Click += async (_, _) => await RechazarSeleccionadaAsync();

                _aprobarButton = new Button
                {
                    Text = "Aprobar Solicitud",
                    Dock = DockStyle.Right,
                    Width = 135,
                    Enabled = false,
                    ForeColor = Color.DarkGreen,
                    UseVisualStyleBackColor = true
                };
                _aprobarButton.Click += async (_, _) => await AprobarSeleccionadaAsync();

                bottomPanel.Controls.Add(_rechazarButton);
                bottomPanel.Controls.Add(_aprobarButton);
            }

            var gridHost = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 4, 12, 4)
            };
            gridHost.Controls.Add(_grid);

            this.Controls.Add(gridHost);
            this.Controls.Add(topPanel);
            this.Controls.Add(bottomPanel);
        }

        private async Task CargarDatosAsync()
        {
            if (!_esInvestigador && !_esLector)
            {
                MessageBox.Show("Debe iniciar sesión para acceder a las solicitudes.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                _statusLabel.Text = "Cargando solicitudes...";
                if (_esInvestigador)
                {
                    string? estado = _filtroEstadoComboBox?.SelectedItem?.ToString();
                    if (estado == "Todas") estado = null;
                    _solicitudes = (await SolicitudInvestigadorApiClient.GetAllAsync(estado)).ToList();
                }
                else
                {
                    _solicitudes = (await SolicitudInvestigadorApiClient.GetMisSolicitudesAsync()).ToList();
                    ActualizarEstadoFormularioLector();
                }

                BindGrid();
                _statusLabel.Text = $"{_solicitudes.Count} solicitud(es) encontrada(s).";
                ActualizarBotonesInvestigador();
            }
            catch (Exception ex)
            {
                _statusLabel.Text = "Error al cargar solicitudes.";
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid()
        {
            var rows = _solicitudes.Select(s => new
            {
                s.Id,
                Fecha = s.FechaSolicitud.ToString("dd/MM/yyyy HH:mm"),
                CorreoLector = s.CorreoLector ?? "-",
                Solicitante = $"{s.Nombre} {s.Apellido}",
                s.Dni,
                s.Motivo,
                s.Estado,
                Resolutor = s.NombreInvestigadorResolutor ?? "-"
            }).ToList();

            _grid.DataSource = rows;
            if (_grid.Columns["Id"] != null) _grid.Columns["Id"]!.Visible = false;
            if (_grid.Columns["CorreoLector"] != null) _grid.Columns["CorreoLector"]!.Visible = _esInvestigador;
        }

        private void ActualizarEstadoFormularioLector()
        {
            bool tienePendiente = _solicitudes.Any(s => s.Estado == "pendiente");
            bool tieneAprobada = _solicitudes.Any(s => s.Estado == "aprobada");
            bool bloquear = tienePendiente || tieneAprobada;

            if (_bannerLabel != null)
            {
                if (tieneAprobada)
                {
                    _bannerLabel.Visible = true;
                    _bannerLabel.BackColor = Color.FromArgb(209, 231, 221);
                    _bannerLabel.ForeColor = Color.FromArgb(15, 81, 50);
                    _bannerLabel.Text = "¡Tu solicitud fue aprobada! Cerrá sesión y volvé a iniciar sesión para activar tus permisos de Investigador.";
                }
                else if (tienePendiente)
                {
                    _bannerLabel.Visible = true;
                    _bannerLabel.BackColor = Color.FromArgb(207, 244, 252);
                    _bannerLabel.ForeColor = Color.FromArgb(5, 81, 96);
                    _bannerLabel.Text = "Ya tenés una solicitud en estado pendiente. Un investigador revisará tu postulación pronto.";
                }
                else
                {
                    _bannerLabel.Visible = false;
                }
            }

            if (_nombreTextBox != null) _nombreTextBox.Enabled = !bloquear;
            if (_apellidoTextBox != null) _apellidoTextBox.Enabled = !bloquear;
            if (_dniTextBox != null) _dniTextBox.Enabled = !bloquear;
            if (_motivoTextBox != null) _motivoTextBox.Enabled = !bloquear;
            if (_enviarButton != null) _enviarButton.Enabled = !bloquear;
        }

        private void ActualizarBotonesInvestigador()
        {
            if (!_esInvestigador || _aprobarButton == null || _rechazarButton == null) return;
            var sel = ObtenerSeleccionada();
            bool esPendiente = sel != null && sel.Estado == "pendiente";
            _aprobarButton.Enabled = esPendiente;
            _rechazarButton.Enabled = esPendiente;
        }

        private SolicitudInvestigadorDTO? ObtenerSeleccionada()
        {
            if (_grid.SelectedRows.Count == 0) return null;
            if (_grid.SelectedRows[0].Cells["Id"]?.Value is Guid id)
            {
                return _solicitudes.FirstOrDefault(s => s.Id == id);
            }
            return null;
        }

        private async Task EnviarSolicitudAsync()
        {
            string nombre = _nombreTextBox?.Text.Trim() ?? string.Empty;
            string apellido = _apellidoTextBox?.Text.Trim() ?? string.Empty;
            string dni = _dniTextBox?.Text.Trim() ?? string.Empty;
            string motivo = _motivoTextBox?.Text.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Nombre, Apellido y DNI son obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_enviarButton != null) _enviarButton.Enabled = false;
                await SolicitudInvestigadorApiClient.CrearAsync(new CrearSolicitudInvestigadorDTO
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Dni = dni,
                    Motivo = motivo
                });

                _nombreTextBox?.Clear();
                _apellidoTextBox?.Clear();
                _dniTextBox?.Clear();
                _motivoTextBox?.Clear();

                MessageBox.Show("Solicitud enviada correctamente. Quedará pendiente de aprobación por un investigador.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar solicitud: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActualizarEstadoFormularioLector();
            }
        }

        private async Task AprobarSeleccionadaAsync()
        {
            var sel = ObtenerSeleccionada();
            if (sel == null) return;

            var confirm = MessageBox.Show(
                $"¿Aprobar la solicitud de {sel.Nombre} {sel.Apellido} ({sel.CorreoLector})? Su rol pasará a Investigador.",
                "Confirmar aprobación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await SolicitudInvestigadorApiClient.AprobarAsync(sel.Id);
                MessageBox.Show($"Solicitud aprobada. {sel.CorreoLector} ahora es Investigador.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar solicitud: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RechazarSeleccionadaAsync()
        {
            var sel = ObtenerSeleccionada();
            if (sel == null) return;

            var confirm = MessageBox.Show(
                $"¿Rechazar la solicitud de {sel.Nombre} {sel.Apellido}?",
                "Confirmar rechazo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await SolicitudInvestigadorApiClient.RechazarAsync(sel.Id);
                MessageBox.Show("Solicitud rechazada.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar solicitud: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
