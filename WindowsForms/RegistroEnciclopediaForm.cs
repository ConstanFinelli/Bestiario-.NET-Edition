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
    public class RegistroEnciclopediaForm : Form
    {
        private RegistroDTO _registro;
        private readonly BestiaDTO? _bestia;
        private readonly List<BestiaDTO> _bestias;
        private Label _evidenciasLabel = null!;
        private ListBox _evidenciasListBox = null!;
        private Button? _abrirEvidenciaBtn;
        private Button? _proponerEvidenciaBtn;
        private Button? _aprobarEvidenciaBtn;
        private Button? _eliminarEvidenciaBtn;
        private List<EvidenciaDTO> _evidenciasActuales = new();
        private Label _comentariosLabel = null!;
        private ListBox _comentariosListBox = null!;
        private TextBox? _comentarioTextoBox;
        private Button? _publicarComentarioBtn;
        private Button? _eliminarComentarioBtn;
        private List<ComentarioDTO> _comentariosActuales = new();

        public bool SeEdito { get; private set; }

        public RegistroEnciclopediaForm(RegistroDTO registro, BestiaDTO? bestia, List<BestiaDTO> bestias)
        {
            _registro = registro;
            _bestia = bestia;
            _bestias = bestias;

            ConfigurarFormulario();
            ConstruirContenido();
            Load += async (_, _) =>
            {
                await CargarEvidenciasAsync();
                if (_registro.Estado != null && _registro.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase))
                {
                    await CargarComentariosAsync();
                }
            };
        }

        private void ConfigurarFormulario()
        {
            string nombreBestia = _bestia?.Nombre ?? "Registro";
            Text = $"Enciclopedia — {nombreBestia}";
            ClientSize = new Size(880, 740);
            MinimumSize = new Size(760, 560);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(246, 243, 235);
            Font = new Font("Segoe UI", 9.5F);
        }

        private void ConstruirContenido()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool hayUsuario = LoginForm.UsuarioLogueado != null;
            bool estaAprobado = _registro.Estado != null
                && _registro.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase);

            // Barra inferior de botones
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                BackColor = Color.FromArgb(236, 232, 222),
                Padding = new Padding(12, 8, 12, 8)
            };

            var cerrarBtn = new Button
            {
                Text = "Cerrar",
                Width = 95,
                Height = 28,
                Dock = DockStyle.Right
            };
            cerrarBtn.Click += (_, _) => Close();
            bottomPanel.Controls.Add(cerrarBtn);

            if (esInvestigador)
            {
                bool estaPendiente = _registro.Estado != null && _registro.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase);
                if (estaPendiente)
                {
                    var aprobarBtn = new Button
                    {
                        Text = "Aprobar",
                        Width = 100,
                        Height = 28,
                        Dock = DockStyle.Right,
                        BackColor = Color.FromArgb(25, 135, 84),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    aprobarBtn.Click += async (_, _) =>
                    {
                        try
                        {
                            aprobarBtn.Enabled = false;
                            await RegistroApiClient.AprobarAsync(_registro.IdBestia, _registro.NroRegistro, LoginForm.UsuarioLogueado!.Id);
                            SeEdito = true;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            aprobarBtn.Enabled = true;
                            MessageBox.Show($"Error al aprobar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    var eliminarRegBtn = new Button
                    {
                        Text = "Eliminar (Pendiente)",
                        Width = 140,
                        Height = 28,
                        Dock = DockStyle.Right,
                        BackColor = Color.FromArgb(220, 53, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    eliminarRegBtn.Click += async (_, _) =>
                    {
                        var confirm = MessageBox.Show(
                            $"¿Eliminar físicamente el Registro #{_registro.NroRegistro}?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                        if (confirm != DialogResult.Yes) return;

                        try
                        {
                            eliminarRegBtn.Enabled = false;
                            await RegistroApiClient.DeleteAsync(_registro.IdBestia, _registro.NroRegistro);
                            SeEdito = true;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            eliminarRegBtn.Enabled = true;
                            MessageBox.Show($"Error al eliminar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    bottomPanel.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                    bottomPanel.Controls.Add(eliminarRegBtn);
                    bottomPanel.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                    bottomPanel.Controls.Add(aprobarBtn);
                }
                else
                {
                    var dejarPendienteBtn = new Button
                    {
                        Text = "Dejar Pendiente",
                        Width = 125,
                        Height = 28,
                        Dock = DockStyle.Right,
                        BackColor = Color.FromArgb(255, 193, 7),
                        ForeColor = Color.Black,
                        FlatStyle = FlatStyle.Flat
                    };
                    dejarPendienteBtn.Click += async (_, _) =>
                    {
                        try
                        {
                            dejarPendienteBtn.Enabled = false;
                            _registro.Estado = "pendiente";
                            _registro.FechaAprobacion = null;
                            _registro.FechaBaja = null;
                            _registro.IdInvestigadorAprobador = null;
                            await RegistroApiClient.UpdateAsync(_registro);
                            SeEdito = true;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            dejarPendienteBtn.Enabled = true;
                            MessageBox.Show($"Error al dejar pendiente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    bottomPanel.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                    bottomPanel.Controls.Add(dejarPendienteBtn);
                }
            }

            if (hayUsuario)
            {
                var proponerBtn = new Button
                {
                    Text = "+ Proponer nuevo registro",
                    Width = 175,
                    Height = 28,
                    Dock = DockStyle.Left
                };
                proponerBtn.Click += (_, _) =>
                {
                    var copia = new RegistroDTO
                    {
                        IdBestia = _registro.IdBestia,
                        NroRegistro = 0,
                        Estado = esInvestigador ? "aprobado" : "pendiente",
                        IdUsuarioPublicador = LoginForm.UsuarioLogueado!.Id,
                        Contenidos = _registro.Contenidos
                            .OrderBy(c => c.NroContenido)
                            .Select(c => new ContenidoRegistroDTO
                            {
                                IdBestia = _registro.IdBestia,
                                NroRegistro = 0,
                                NroContenido = c.NroContenido,
                                Titulo = c.Titulo,
                                Contenido = c.Contenido
                            }).ToList()
                    };
                    using var propuestaForm = new RegistroDetalle(FormMode.Add, copia, _bestias);
                    propuestaForm.ShowDialog();
                    SeEdito = true;
                    Close();
                };
                bottomPanel.Controls.Add(proponerBtn);
            }

            // Encabezado con nombre de la bestia
            var headerLabel = new Label
            {
                Text = _bestia?.Nombre ?? "Bestia",
                Dock = DockStyle.Top,
                Height = 46,
                Font = new Font("Georgia", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(43, 38, 32),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Barra de consulta histórica por fecha
            var fechaPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(235, 230, 218),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(14, 7, 14, 7)
            };
            var fechaLbl = new Label
            {
                Text = "Cargar registro en fecha:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(43, 38, 32),
                Margin = new Padding(0, 6, 8, 0)
            };
            var fechaPicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Width = 195,
                Value = DateTime.Now,
                Margin = new Padding(0, 2, 8, 0)
            };
            var cargarFechaBtn = new Button
            {
                Text = "Cargar",
                Width = 90,
                Height = 29,
                Margin = new Padding(0)
            };
            cargarFechaBtn.Click += async (_, _) =>
            {
                try
                {
                    cargarFechaBtn.Enabled = false;
                    var hist = await RegistroApiClient.GetVigenteAsync(_registro.IdBestia, fechaPicker.Value);
                    if (hist == null)
                    {
                        MessageBox.Show("No existen registros aprobados para esta bestia en esa fecha.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _registro = hist;
                    Controls.Clear();
                    ConstruirContenido();
                    await CargarEvidenciasAsync();
                    await CargarComentariosAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al consultar fecha: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cargarFechaBtn.Enabled = true;
                }
            };
            fechaPanel.Controls.Add(fechaLbl);
            fechaPanel.Controls.Add(fechaPicker);
            fechaPanel.Controls.Add(cargarFechaBtn);

            // Contenedor central (2 columnas)
            var mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(16, 6, 16, 10)
            };
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Columna izquierda: secciones de contenido
            var leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(4, 4, 12, 4)
            };

            var contenidos = _registro.Contenidos.OrderBy(c => c.NroContenido).ToList();
            if (!contenidos.Any())
            {
                leftPanel.Controls.Add(new Label
                {
                    Text = "Este registro aún no posee secciones de contenido.",
                    AutoSize = true,
                    ForeColor = Color.DimGray,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                    Location = new Point(4, 8)
                });
            }
            else
            {
                var flow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false
                };

                foreach (var cont in contenidos)
                {
                    var secTitulo = new Label
                    {
                        Text = cont.Titulo,
                        AutoSize = true,
                        MaximumSize = new Size(490, 0),
                        Font = new Font("Georgia", 11.5F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(44, 39, 33),
                        Margin = new Padding(0, 8, 0, 4)
                    };

                    var secTexto = new Label
                    {
                        Text = cont.Contenido,
                        AutoSize = true,
                        MaximumSize = new Size(490, 0),
                        Font = new Font("Segoe UI", 9.5F),
                        ForeColor = Color.FromArgb(46, 46, 46),
                        Margin = new Padding(0, 0, 0, 12)
                    };

                    flow.Controls.Add(secTitulo);
                    flow.Controls.Add(secTexto);
                }

                leftPanel.Controls.Add(flow);
            }

            // Columna derecha: tarjeta de detalles
            var rightCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(228, 224, 216),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Padding = new Padding(12)
            };

            string categoriasTexto = (_bestia?.CategoriasNombres != null && _bestia.CategoriasNombres.Any())
                ? string.Join(", ", _bestia.CategoriasNombres)
                : "Sin categorías";

            string habitatTexto = string.IsNullOrWhiteSpace(_bestia?.HabitatNombre)
                ? "Sin hábitat asignado"
                : _bestia.HabitatNombre;

            string fechaAprob = _registro.FechaAprobacion.HasValue
                ? _registro.FechaAprobacion.Value.ToString("dd/MM/yyyy HH:mm")
                : "-";

            var infoLabel = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(260, 0),
                ForeColor = Color.FromArgb(43, 38, 32),
                Text =
                    $"DETALLES DE LA BESTIA\n" +
                    $"Nombre: {_bestia?.Nombre ?? "-"}\n" +
                    $"Peligrosidad: {_bestia?.Peligrosidad ?? "-"}\n\n" +
                    $"CATEGORÍAS\n" +
                    $"{categoriasTexto}\n\n" +
                    $"HÁBITAT LOCALIZADO\n" +
                    $"{habitatTexto}\n\n" +
                    $"DETALLES DE REGISTRO\n" +
                    $"Nro. Registro: #{_registro.NroRegistro}\n" +
                    $"Estado: {_registro.Estado}\n" +
                    $"Publicado por: {_registro.CorreoPublicador ?? "N/A"}\n" +
                    $"Aprobado por: {_registro.NombreInvestigadorAprobador ?? "Pendiente"}\n" +
                    $"Aprobación: {fechaAprob}"
            };
            rightCard.Controls.Add(infoLabel);

            if (!string.IsNullOrWhiteSpace(_registro.MainPictureUrl))
            {
                var pictureBox = new PictureBox
                {
                    Dock = DockStyle.Top,
                    Height = 175,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(218, 213, 203),
                    Margin = new Padding(0, 0, 0, 8),
                    Padding = new Padding(2)
                };
                bool triedFallback = false;
                pictureBox.LoadCompleted += (s, ev) =>
                {
                    if ((ev.Error != null || ev.Cancelled) && !triedFallback && !string.IsNullOrWhiteSpace(_registro.DefaultPictureUrl))
                    {
                        triedFallback = true;
                        try { pictureBox.LoadAsync(_registro.DefaultPictureUrl); } catch { }
                    }
                };
                try { pictureBox.LoadAsync(_registro.MainPictureUrl); } catch { }
                rightCard.Controls.Add(pictureBox);
            }

            mainTable.Controls.Add(leftPanel, 0, 0);
            mainTable.Controls.Add(rightCard, 1, 0);

            // Panel inferior de evidencias
            var evidenciasPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 132,
                Padding = new Padding(16, 4, 16, 4)
            };

            _evidenciasLabel = new Label
            {
                Text = "Evidencias registradas (cargando...)",
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Georgia", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(43, 38, 32)
            };

            _evidenciasListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(250, 248, 243)
            };
            _evidenciasListBox.SelectedIndexChanged += (_, _) => ActualizarBotonesEvidencia();
            _evidenciasListBox.DoubleClick += (_, _) => AbrirEvidenciaSeleccionada();

            var evActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 140,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(6, 0, 0, 0)
            };

            _abrirEvidenciaBtn = new Button
            {
                Text = "Ver / Descargar",
                Width = 128,
                Height = 25,
                Enabled = false,
                UseVisualStyleBackColor = true
            };
            _abrirEvidenciaBtn.Click += (_, _) => AbrirEvidenciaSeleccionada();
            evActions.Controls.Add(_abrirEvidenciaBtn);

            if (hayUsuario)
            {
                _proponerEvidenciaBtn = new Button
                {
                    Text = "+ Proponer",
                    Width = 128,
                    Height = 25,
                    UseVisualStyleBackColor = true
                };
                _proponerEvidenciaBtn.Click += async (_, _) => await ProponerEvidenciaAsync();
                evActions.Controls.Add(_proponerEvidenciaBtn);
            }

            if (esInvestigador)
            {
                _aprobarEvidenciaBtn = new Button
                {
                    Text = "Aprobar",
                    Width = 128,
                    Height = 25,
                    Enabled = false,
                    ForeColor = Color.DarkGreen,
                    UseVisualStyleBackColor = true
                };
                _aprobarEvidenciaBtn.Click += async (_, _) => await AprobarEvidenciaSeleccionadaAsync();

                _eliminarEvidenciaBtn = new Button
                {
                    Text = "Eliminar",
                    Width = 128,
                    Height = 25,
                    Enabled = false,
                    ForeColor = Color.DarkRed,
                    UseVisualStyleBackColor = true
                };
                _eliminarEvidenciaBtn.Click += async (_, _) => await EliminarEvidenciaSeleccionadaAsync();

                evActions.Controls.Add(_aprobarEvidenciaBtn);
                evActions.Controls.Add(_eliminarEvidenciaBtn);
            }

            evidenciasPanel.Controls.Add(_evidenciasListBox);
            evidenciasPanel.Controls.Add(evActions);
            evidenciasPanel.Controls.Add(_evidenciasLabel);

            Panel? comentariosPanel = null;
            if (estaAprobado)
            {
                comentariosPanel = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = hayUsuario ? 185 : 115,
                    Padding = new Padding(16, 4, 16, 6)
                };

                _comentariosLabel = new Label
                {
                    Text = "Comentarios (cargando...)",
                    Dock = DockStyle.Top,
                    Height = 22,
                    Font = new Font("Georgia", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(43, 38, 32)
                };

                _comentariosListBox = new ListBox
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(250, 248, 243)
                };

                if (esInvestigador)
                {
                    var delBar = new Panel { Dock = DockStyle.Right, Width = 125, Padding = new Padding(6, 0, 0, 0) };
                    _eliminarComentarioBtn = new Button
                    {
                        Text = "Eliminar comentario",
                        Dock = DockStyle.Top,
                        Height = 28,
                        Enabled = false
                    };
                    _eliminarComentarioBtn.Click += async (_, _) => await EliminarComentarioSeleccionadoAsync();
                    _comentariosListBox.SelectedIndexChanged += (_, _) =>
                    {
                        if (_eliminarComentarioBtn != null)
                        {
                            _eliminarComentarioBtn.Enabled = _comentariosListBox.SelectedIndex >= 0
                                && _comentariosListBox.SelectedIndex < _comentariosActuales.Count;
                        }
                    };
                    delBar.Controls.Add(_eliminarComentarioBtn);
                    comentariosPanel.Controls.Add(delBar);
                }

                comentariosPanel.Controls.Add(_comentariosListBox);

                if (hayUsuario)
                {
                    var addPanel = new Panel { Dock = DockStyle.Bottom, Height = 40, Padding = new Padding(0, 6, 0, 0) };
                    _comentarioTextoBox = new TextBox
                    {
                        PlaceholderText = "Escriba su comentario...",
                        Location = new Point(0, 8),
                        Width = 680
                    };
                    _publicarComentarioBtn = new Button
                    {
                        Text = "Publicar",
                        Location = new Point(690, 6),
                        Width = 110,
                        Height = 26
                    };
                    _publicarComentarioBtn.Click += async (_, _) => await PublicarComentarioAsync();

                    addPanel.Controls.Add(_comentarioTextoBox);
                    addPanel.Controls.Add(_publicarComentarioBtn);
                    comentariosPanel.Controls.Add(addPanel);
                }

                comentariosPanel.Controls.Add(_comentariosLabel);
            }

            Controls.Add(mainTable);
            Controls.Add(evidenciasPanel);
            if (comentariosPanel != null)
            {
                Controls.Add(comentariosPanel);
            }
            Controls.Add(fechaPanel);
            Controls.Add(headerLabel);
            Controls.Add(bottomPanel);
        }

        private async Task CargarEvidenciasAsync()
        {
            try
            {
                var deLaBestia = await EvidenciaApiClient.GetByBestiaAsync(_registro.IdBestia);
                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                    || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

                _evidenciasActuales = deLaBestia
                    .Where(e => esInvestigador
                        || (e.Estado != null && e.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(e => e.NroEvidencia)
                    .ToList();

                _evidenciasLabel.Text = $"Evidencias registradas ({_evidenciasActuales.Count})";
                _evidenciasListBox.Items.Clear();

                if (!_evidenciasActuales.Any())
                {
                    _evidenciasListBox.Items.Add("No hay evidencias registradas para esta bestia.");
                    ActualizarBotonesEvidencia();
                    return;
                }

                foreach (var ev in _evidenciasActuales)
                {
                    string tipo = ev.TipoEvidenciaDescripcion ?? "Evidencia";
                    _evidenciasListBox.Items.Add($"#{ev.NroEvidencia} [{ev.Estado}] {tipo} — {ev.FechaObtencion:dd/MM/yyyy} ({ev.FileId})");
                }
                ActualizarBotonesEvidencia();
            }
            catch
            {
                _evidenciasActuales = new List<EvidenciaDTO>();
                _evidenciasLabel.Text = "Evidencias registradas (no disponibles)";
                ActualizarBotonesEvidencia();
            }
        }

        private EvidenciaDTO? ObtenerEvidenciaSeleccionada()
        {
            int idx = _evidenciasListBox.SelectedIndex;
            if (idx < 0 || idx >= _evidenciasActuales.Count) return null;
            return _evidenciasActuales[idx];
        }

        private void ActualizarBotonesEvidencia()
        {
            var sel = ObtenerEvidenciaSeleccionada();
            if (_abrirEvidenciaBtn != null)
            {
                _abrirEvidenciaBtn.Enabled = sel != null && !string.IsNullOrWhiteSpace(sel.FileUrl);
            }
            if (_aprobarEvidenciaBtn != null)
            {
                _aprobarEvidenciaBtn.Enabled = sel != null && sel.Estado != null
                    && sel.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase);
            }
            if (_eliminarEvidenciaBtn != null)
            {
                _eliminarEvidenciaBtn.Enabled = sel != null;
            }
        }

        private void AbrirEvidenciaSeleccionada()
        {
            var sel = ObtenerEvidenciaSeleccionada();
            if (sel == null || string.IsNullOrWhiteSpace(sel.FileUrl)) return;

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sel.FileUrl)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir la evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ProponerEvidenciaAsync()
        {
            if (LoginForm.UsuarioLogueado == null) return;

            try
            {
                var tipos = (await TipoEvidenciaApiClient.GetAllAsync()).ToList();
                if (!tipos.Any())
                {
                    MessageBox.Show("No hay tipos de evidencia disponibles.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var dlg = new Form
                {
                    Text = "Proponer nueva evidencia",
                    Size = new Size(440, 230),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    StartPosition = FormStartPosition.CenterParent
                };

                var lblTipo = new Label { Text = "Tipo de evidencia:", Location = new Point(16, 18), AutoSize = true };
                var cboTipo = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(140, 15),
                    Width = 265,
                    DisplayMember = "Descripcion",
                    DataSource = tipos
                };

                var lblFecha = new Label { Text = "Fecha obtención:", Location = new Point(16, 54), AutoSize = true };
                var dtpFecha = new DateTimePicker { Location = new Point(140, 50), Width = 265, Format = DateTimePickerFormat.Short };

                string? selectedPath = null;
                var lblArchivo = new Label { Text = "Archivo:", Location = new Point(16, 90), AutoSize = true };
                var lblNombreArch = new Label { Text = "Ningún archivo seleccionado", Location = new Point(236, 90), Width = 175, ForeColor = Color.DimGray };
                var btnArchivo = new Button { Text = "Seleccionar...", Location = new Point(140, 86), Width = 90, Height = 24 };
                btnArchivo.Click += (_, _) =>
                {
                    using var ofd = new OpenFileDialog { Title = "Seleccionar archivo de evidencia" };
                    if (ofd.ShowDialog(dlg) == DialogResult.OK)
                    {
                        selectedPath = ofd.FileName;
                        lblNombreArch.Text = System.IO.Path.GetFileName(selectedPath);
                    }
                };

                var btnEnviar = new Button { Text = "Enviar", Location = new Point(235, 140), Width = 80, Height = 28 };
                var btnCancelar = new Button { Text = "Cancelar", Location = new Point(325, 140), Width = 80, Height = 28, DialogResult = DialogResult.Cancel };
                btnEnviar.Click += (_, _) =>
                {
                    if (string.IsNullOrWhiteSpace(selectedPath))
                    {
                        MessageBox.Show(dlg, "Debe seleccionar un archivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dlg.DialogResult = DialogResult.OK;
                    dlg.Close();
                };

                dlg.Controls.AddRange(new Control[] { lblTipo, cboTipo, lblFecha, dtpFecha, lblArchivo, btnArchivo, lblNombreArch, btnEnviar, btnCancelar });
                if (dlg.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(selectedPath))
                    return;

                var tipoSel = (TipoEvidenciaDTO)cboTipo.SelectedItem!;
                string ext = System.IO.Path.GetExtension(selectedPath).ToLowerInvariant();
                string contentType = ext switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".webp" => "image/webp",
                    ".mp4" => "video/mp4",
                    ".webm" => "video/webm",
                    ".mp3" => "audio/mpeg",
                    ".pdf" => "application/pdf",
                    _ => "application/octet-stream"
                };

                string? uploadedPublicId = null;
                try
                {
                    if (_proponerEvidenciaBtn != null) _proponerEvidenciaBtn.Enabled = false;
                    await using var stream = System.IO.File.OpenRead(selectedPath);
                    uploadedPublicId = await CloudinaryApiClient.UploadImageAsync(
                        stream,
                        System.IO.Path.GetFileName(selectedPath),
                        contentType);

                    if (string.IsNullOrWhiteSpace(uploadedPublicId))
                        throw new Exception("Cloudinary no devolvió un public_id válido.");

                    bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                        || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

                    await EvidenciaApiClient.AddAsync(new EvidenciaDTO
                    {
                        IdBestia = _registro.IdBestia,
                        IdTipoEvidencia = tipoSel.Id,
                        FechaObtencion = dtpFecha.Value,
                        FileId = uploadedPublicId,
                        Estado = esInvestigador ? "aprobado" : "pendiente"
                    });

                    await CargarEvidenciasAsync();
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrWhiteSpace(uploadedPublicId))
                    {
                        try { await CloudinaryApiClient.DeleteImageAsync(uploadedPublicId); } catch { }
                    }
                    MessageBox.Show($"Error al proponer evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (_proponerEvidenciaBtn != null) _proponerEvidenciaBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al preparar propuesta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AprobarEvidenciaSeleccionadaAsync()
        {
            var sel = ObtenerEvidenciaSeleccionada();
            if (sel == null) return;

            try
            {
                await EvidenciaApiClient.AprobarAsync(sel.IdBestia, sel.NroEvidencia);
                await CargarEvidenciasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task EliminarEvidenciaSeleccionadaAsync()
        {
            var sel = ObtenerEvidenciaSeleccionada();
            if (sel == null) return;

            var confirm = MessageBox.Show(
                $"¿Eliminar la evidencia #{sel.NroEvidencia}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await EvidenciaApiClient.DeleteAsync(sel.IdBestia, sel.NroEvidencia);
                await CargarEvidenciasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarComentariosAsync()
        {
            try
            {
                var lista = await ComentarioApiClient.GetByBestiaAsync(_registro.IdBestia);
                _comentariosActuales = lista?.ToList() ?? new List<ComentarioDTO>();

                _comentariosLabel.Text = $"Comentarios ({_comentariosActuales.Count})";
                _comentariosListBox.Items.Clear();

                if (!_comentariosActuales.Any())
                {
                    _comentariosListBox.Items.Add("No hay comentarios para esta bestia.");
                }
                else
                {
                    foreach (var c in _comentariosActuales)
                    {
                        string correo = string.IsNullOrWhiteSpace(c.CorreoUsuario) ? "Usuario" : c.CorreoUsuario;
                        _comentariosListBox.Items.Add($"{correo} - Publicado el {c.FechaPublicacion:dd/MM/yyyy HH:mm}: {c.Contenido}");
                    }
                }

                if (_eliminarComentarioBtn != null)
                {
                    _eliminarComentarioBtn.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _comentariosLabel.Text = $"Comentarios (error: {ex.Message})";
            }
        }

        private async Task PublicarComentarioAsync()
        {
            if (LoginForm.UsuarioLogueado == null || _comentarioTextoBox == null || _publicarComentarioBtn == null)
                return;

            if (string.IsNullOrWhiteSpace(_comentarioTextoBox.Text))
            {
                MessageBox.Show("El contenido del comentario es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _publicarComentarioBtn.Enabled = false;
                var dto = new ComentarioDTO
                {
                    IdBestia = _registro.IdBestia,
                    IdUsuario = LoginForm.UsuarioLogueado.Id,
                    Contenido = _comentarioTextoBox.Text.Trim()
                };

                await ComentarioApiClient.AddAsync(dto);
                _comentarioTextoBox.Clear();
                await CargarComentariosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al publicar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _publicarComentarioBtn.Enabled = true;
            }
        }

        private async Task EliminarComentarioSeleccionadoAsync()
        {
            int idx = _comentariosListBox.SelectedIndex;
            if (idx < 0 || idx >= _comentariosActuales.Count) return;

            var com = _comentariosActuales[idx];
            var confirm = MessageBox.Show(
                "¿Estás seguro que deseas eliminar este comentario?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await ComentarioApiClient.DeleteAsync(com.Id);
                await CargarComentariosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
