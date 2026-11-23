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
        private FlowLayoutPanel _evidenciasCardsPanel = null!;
        private Button? _proponerEvidenciaBtn;
        private List<EvidenciaDTO> _evidenciasActuales = new();
        private Label _comentariosLabel = null!;
        private FlowLayoutPanel _comentariosCardsPanel = null!;
        private TextBox? _comentarioTextoBox;
        private Button? _publicarComentarioBtn;
        private List<ComentarioDTO> _comentariosActuales = new();

        public bool SeEdito { get; private set; }

        public RegistroEnciclopediaForm(RegistroDTO registro, BestiaDTO? bestia, List<BestiaDTO> bestias)
        {
            _registro = registro;
            _bestia = bestia;
            _bestias = bestias;

            ConfigurarFormulario();
            ConstruirContenido();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
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
                Width = 95,
                Height = 28,
                Dock = DockStyle.Left
            };
            ThemeHelper.StyleBackButton(cerrarBtn);
            cerrarBtn.Click += (_, _) => Close();
            bottomPanel.Controls.Add(cerrarBtn);

            if (esInvestigador)
            {
                bool estaPendiente = _registro.Estado != null && _registro.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase);
                if (estaPendiente)
                {
                    var aprobarBtn = new Button
                    {
                        Text = "✔ Aprobar",
                        Width = 105,
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
                        Text = "✕ Eliminar (Pendiente)",
                        Width = 160,
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
                        Text = "↩ Dejar Pendiente",
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        MinimumSize = new Size(155, 28),
                        Height = 28,
                        Padding = new Padding(8, 0, 8, 0),
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
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    MinimumSize = new Size(190, 28),
                    Height = 28,
                    Padding = new Padding(8, 0, 8, 0),
                    Dock = DockStyle.Right
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
                    ThemeHelper.ShowSingleWindow(this, propuestaForm);
                    SeEdito = true;
                    Close();
                };
                bottomPanel.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                bottomPanel.Controls.Add(proponerBtn);
            }

            // Encabezado con nombre de la bestia
            var headerLabel = new Label
            {
                Text = _bestia?.Nombre ?? "Bestia",
                Dock = DockStyle.Top,
                Height = 46,
                Font = new Font("Georgia", 18F, FontStyle.Bold),
                BackColor = ThemeHelper.OuterBrown,
                ForeColor = Color.White,
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

            // Panel inferior de evidencias (tarjetas con vista previa scrolleable como en Web)
            var evidenciasPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 250,
                Padding = new Padding(16, 6, 16, 6)
            };

            var evidenciasHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 28
            };

            _evidenciasLabel = new Label
            {
                Text = "Evidencias registradas (cargando...)",
                Dock = DockStyle.Fill,
                Font = new Font("Georgia", 10F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark,
                TextAlign = ContentAlignment.MiddleLeft
            };
            evidenciasHeader.Controls.Add(_evidenciasLabel);

            if (hayUsuario)
            {
                _proponerEvidenciaBtn = new Button
                {
                    Text = "+ Proponer nueva evidencia",
                    Width = 185,
                    Height = 24,
                    Dock = DockStyle.Right,
                    UseVisualStyleBackColor = true
                };
                _proponerEvidenciaBtn.Click += async (_, _) => await ProponerEvidenciaAsync();
                evidenciasHeader.Controls.Add(_proponerEvidenciaBtn);
            }

            _evidenciasCardsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = ThemeHelper.Bone,
                Padding = new Padding(0, 4, 0, 0)
            };

            evidenciasPanel.Controls.Add(_evidenciasCardsPanel);
            evidenciasPanel.Controls.Add(evidenciasHeader);

            Panel? comentariosPanel = null;
            if (estaAprobado)
            {
                comentariosPanel = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = hayUsuario ? 245 : 155,
                    Padding = new Padding(16, 4, 16, 6)
                };

                var topDivider = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 1,
                    BackColor = Color.FromArgb(189, 182, 167)
                };

                _comentariosLabel = new Label
                {
                    Text = "Comentarios (cargando...)",
                    Dock = DockStyle.Top,
                    Height = 26,
                    Padding = new Padding(0, 4, 0, 0),
                    Font = new Font("Georgia", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(43, 38, 32)
                };

                _comentariosCardsPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    BackColor = ThemeHelper.Bone,
                    Padding = new Padding(0, 4, 0, 4)
                };
                _comentariosCardsPanel.SizeChanged += (_, _) => RenderizarComentarios();

                comentariosPanel.Controls.Add(_comentariosCardsPanel);

                if (hayUsuario)
                {
                    var addPanel = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 96,
                        BackColor = Color.FromArgb(239, 236, 227),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(8)
                    };

                    var addTitleLabel = new Label
                    {
                        Text = "Agregar comentario",
                        Dock = DockStyle.Top,
                        Height = 18,
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                        ForeColor = ThemeHelper.TextDark
                    };

                    _comentarioTextoBox = new TextBox
                    {
                        Multiline = true,
                        ScrollBars = ScrollBars.Vertical,
                        PlaceholderText = "Escriba su comentario...",
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    var publishRow = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 30,
                        BackColor = Color.FromArgb(239, 236, 227),
                        Padding = new Padding(0, 4, 0, 0)
                    };

                    _publicarComentarioBtn = new Button
                    {
                        Text = "Publicar comentario",
                        Dock = DockStyle.Right,
                        Width = 140,
                        Height = 26,
                        UseVisualStyleBackColor = true
                    };
                    _publicarComentarioBtn.Click += async (_, _) => await PublicarComentarioAsync();

                    publishRow.Controls.Add(_publicarComentarioBtn);
                    addPanel.Controls.Add(_comentarioTextoBox);
                    addPanel.Controls.Add(publishRow);
                    addPanel.Controls.Add(addTitleLabel);
                    comentariosPanel.Controls.Add(addPanel);
                }

                comentariosPanel.Controls.Add(_comentariosLabel);
                comentariosPanel.Controls.Add(topDivider);
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
                _evidenciasCardsPanel.SuspendLayout();
                _evidenciasCardsPanel.Controls.Clear();

                if (!_evidenciasActuales.Any())
                {
                    _evidenciasCardsPanel.Controls.Add(new Label
                    {
                        Text = "No hay evidencias registradas para esta bestia.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = ThemeHelper.TextMuted,
                        Padding = new Padding(4, 8, 4, 8)
                    });
                    _evidenciasCardsPanel.ResumeLayout();
                    return;
                }

                foreach (var ev in _evidenciasActuales)
                {
                    _evidenciasCardsPanel.Controls.Add(CrearTarjetaEvidencia(ev, esInvestigador));
                }

                _evidenciasCardsPanel.ResumeLayout();
            }
            catch
            {
                _evidenciasActuales = new List<EvidenciaDTO>();
                _evidenciasLabel.Text = "Evidencias registradas (no disponibles)";
            }
        }

        private Panel CrearTarjetaEvidencia(EvidenciaDTO ev, bool esInvestigador)
        {
            var card = new Panel
            {
                Width = 395,
                Height = 205,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 10, 10),
                Padding = new Padding(8)
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 22,
                BackColor = Color.White
            };

            bool aprobada = string.Equals(ev.Estado, "aprobado", StringComparison.OrdinalIgnoreCase);
            var estadoBadge = new Label
            {
                Text = ev.Estado ?? "-",
                AutoSize = true,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                BackColor = aprobada ? Color.FromArgb(25, 135, 84) : Color.FromArgb(255, 193, 7),
                ForeColor = aprobada ? Color.White : Color.Black,
                Padding = new Padding(4, 1, 4, 1),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var tituloLbl = new Label
            {
                Text = $"Evidencia #{ev.NroEvidencia} — {ev.TipoEvidenciaDescripcion ?? "Archivo"}",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            header.Controls.Add(tituloLbl);
            header.Controls.Add(estadoBadge);

            var footer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 32,
                BackColor = Color.White
            };

            var fechaLbl = new Label
            {
                Text = $"Obtenida: {ev.FechaObtencion:dd/MM/yyyy}",
                Dock = DockStyle.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = ThemeHelper.TextMuted,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 5, 0, 0)
            };

            var accionesFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.White
            };

            if (!string.IsNullOrWhiteSpace(ev.FileUrl))
            {
                var descargarBtn = new Button
                {
                    Text = "Descargar",
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    MinimumSize = new Size(76, 26),
                    Padding = new Padding(6, 2, 6, 2),
                    Font = new Font("Segoe UI", 8F),
                    BackColor = ThemeHelper.Parchment,
                    ForeColor = ThemeHelper.TextDark,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2, 2, 0, 0)
                };
                descargarBtn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
                descargarBtn.Click += async (_, _) => await DescargarEvidenciaAsync(ev);
                accionesFlow.Controls.Add(descargarBtn);
            }

            if (esInvestigador)
            {
                if (string.Equals(ev.Estado, "pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    var aprobarBtn = new Button
                    {
                        Text = "Aprobar",
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        MinimumSize = new Size(66, 26),
                        Padding = new Padding(6, 2, 6, 2),
                        Font = new Font("Segoe UI", 8F),
                        BackColor = ThemeHelper.Parchment,
                        ForeColor = Color.DarkGreen,
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(2, 2, 0, 0)
                    };
                    aprobarBtn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
                    aprobarBtn.Click += async (_, _) => await AprobarEvidenciaAsync(ev);
                    accionesFlow.Controls.Add(aprobarBtn);
                }

                var eliminarBtn = new Button
                {
                    Text = "Eliminar",
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    MinimumSize = new Size(66, 26),
                    Padding = new Padding(6, 2, 6, 2),
                    Font = new Font("Segoe UI", 8F),
                    BackColor = ThemeHelper.Parchment,
                    ForeColor = Color.DarkRed,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2, 2, 0, 0)
                };
                eliminarBtn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
                eliminarBtn.Click += async (_, _) => await EliminarEvidenciaAsync(ev);
                accionesFlow.Controls.Add(eliminarBtn);
            }

            footer.Controls.Add(fechaLbl);
            footer.Controls.Add(accionesFlow);

            var rType = (ev.TipoEvidenciaResourceType ?? string.Empty).Trim().ToLowerInvariant();
            Control bodyControl;
            if (!string.IsNullOrWhiteSpace(ev.FileUrl) && rType == "image")
            {
                var scrollPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = ThemeHelper.GridSurface,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 4, 0, 4)
                };
                var picBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Location = new Point(0, 0),
                    Cursor = Cursors.Hand
                };
                picBox.DoubleClick += (_, _) => MostrarVisorImagenEvidencia(ev);
                scrollPanel.Controls.Add(picBox);
                try { picBox.LoadAsync(ev.FileUrl); } catch { }
                bodyControl = scrollPanel;
            }
            else
            {
                var placeholderPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = ThemeHelper.GridSurface,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 4, 0, 4),
                    Padding = new Padding(8)
                };

                var msgLbl = new Label
                {
                    Text = "Previsualización no disponible",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                    ForeColor = ThemeHelper.TextMuted,
                    BackColor = ThemeHelper.GridSurface,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                placeholderPanel.Controls.Add(msgLbl);

                if (!string.IsNullOrWhiteSpace(ev.FileUrl))
                {
                    var btnBar = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 34,
                        BackColor = ThemeHelper.GridSurface
                    };
                    var abrirBtn = new Button
                    {
                        Text = "Abrir en navegador",
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        MinimumSize = new Size(145, 28),
                        Padding = new Padding(8, 2, 8, 2),
                        Location = new Point(0, 3),
                        BackColor = ThemeHelper.Parchment,
                        ForeColor = ThemeHelper.TextDark,
                        FlatStyle = FlatStyle.Flat
                    };
                    abrirBtn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
                    abrirBtn.Click += (_, _) => AbrirEnNavegador(ev.FileUrl);
                    btnBar.Controls.Add(abrirBtn);
                    placeholderPanel.Controls.Add(btnBar);
                }

                bodyControl = placeholderPanel;
            }

            card.Controls.Add(bodyControl);
            card.Controls.Add(footer);
            card.Controls.Add(header);
            return card;
        }

        private static void AbrirEnNavegador(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir la evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DescargarEvidenciaAsync(EvidenciaDTO ev)
        {
            if (string.IsNullOrWhiteSpace(ev.FileUrl)) return;

            try
            {
                var uri = new Uri(ev.FileUrl);
                string ext = Path.GetExtension(uri.LocalPath);
                string suggestedName = $"evidencia_{ev.NroEvidencia}{(string.IsNullOrWhiteSpace(ext) ? string.Empty : ext)}";

                using var sfd = new SaveFileDialog
                {
                    FileName = suggestedName,
                    Filter = "Todos los archivos|*.*"
                };
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                using var http = new HttpClient();
                var bytes = await http.GetByteArrayAsync(uri);
                await File.WriteAllBytesAsync(sfd.FileName, bytes);
                MessageBox.Show("Evidencia descargada correctamente.", "Descarga completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo descargar la evidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarVisorImagenEvidencia(EvidenciaDTO evidencia)
        {
            using var imgDlg = new Form
            {
                Text = $"Evidencia #{evidencia.NroEvidencia} — {evidencia.TipoEvidenciaDescripcion ?? "Imagen"}",
                Size = new Size(760, 560),
                MinimumSize = new Size(480, 360),
                StartPosition = FormStartPosition.CenterParent
            };

            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8)
            };

            var picBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(0, 0)
            };
            scrollPanel.Controls.Add(picBox);

            var bottomBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 42,
                Padding = new Padding(10, 7, 10, 7)
            };

            var cerrarBtn = new Button
            {
                Width = 92,
                Height = 26,
                Dock = DockStyle.Left
            };
            ThemeHelper.StyleBackButton(cerrarBtn);
            cerrarBtn.Click += (_, _) => imgDlg.Close();

            var abrirNavegadorBtn = new Button
            {
                Text = "Abrir en navegador",
                Width = 140,
                Height = 26,
                Dock = DockStyle.Right
            };
            abrirNavegadorBtn.Click += (_, _) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(evidencia.FileUrl!)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo abrir en navegador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            bottomBar.Controls.Add(cerrarBtn);
            bottomBar.Controls.Add(abrirNavegadorBtn);

            imgDlg.Controls.Add(scrollPanel);
            imgDlg.Controls.Add(bottomBar);
            imgDlg.Load += (_, _) =>
            {
                ThemeHelper.ApplyTheme(imgDlg);
                scrollPanel.BackColor = ThemeHelper.GridSurface;
                try { picBox.LoadAsync(evidencia.FileUrl); } catch { }
            };

            ThemeHelper.ShowSingleWindow(this, imgDlg);
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

                var btnEnviar = new Button { Text = "Enviar evidencia", Location = new Point(195, 140), Width = 120, Height = 28 };
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
                if (ThemeHelper.ShowSingleWindow(this, dlg) != DialogResult.OK || string.IsNullOrWhiteSpace(selectedPath))
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

        private async Task AprobarEvidenciaAsync(EvidenciaDTO sel)
        {
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

        private async Task EliminarEvidenciaAsync(EvidenciaDTO sel)
        {
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
                RenderizarComentarios();
            }
            catch (Exception ex)
            {
                _comentariosLabel.Text = $"Comentarios (error: {ex.Message})";
            }
        }

        private void RenderizarComentarios()
        {
            if (_comentariosCardsPanel == null) return;

            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

            _comentariosCardsPanel.SuspendLayout();
            _comentariosCardsPanel.Controls.Clear();

            int cardWidth = Math.Max(280, _comentariosCardsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);
            int innerWidth = cardWidth - 20;

            if (!_comentariosActuales.Any())
            {
                var emptyLabel = new Label
                {
                    Text = "No hay comentarios para esta bestia.",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = ThemeHelper.TextMuted,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Padding = new Padding(2, 4, 2, 4)
                };
                _comentariosCardsPanel.Controls.Add(emptyLabel);
                _comentariosCardsPanel.ResumeLayout();
                return;
            }

            foreach (var com in _comentariosActuales)
            {
                string correo = string.IsNullOrWhiteSpace(com.CorreoUsuario) ? "Usuario" : com.CorreoUsuario;

                var card = new Panel
                {
                    Width = cardWidth,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10, 8, 10, 8),
                    Margin = new Padding(0, 0, 0, 6)
                };

                var headerRow = new Panel
                {
                    Location = new Point(10, 8),
                    Size = new Size(innerWidth, 28),
                    BackColor = Color.White
                };

                var autorLabel = new Label
                {
                    Text = correo,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = ThemeHelper.TextDark,
                    BackColor = Color.White,
                    AutoSize = true,
                    Location = new Point(0, 2)
                };

                var fechaLabel = new Label
                {
                    Text = $"- Publicado el {com.FechaPublicacion:dd/MM/yyyy HH:mm}",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = ThemeHelper.TextMuted,
                    BackColor = Color.White,
                    AutoSize = true,
                    Location = new Point(autorLabel.PreferredWidth + 4, 3)
                };

                headerRow.Controls.Add(autorLabel);
                headerRow.Controls.Add(fechaLabel);

                if (esInvestigador)
                {
                    var eliminarBtn = new Button
                    {
                        Text = "Eliminar",
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        MinimumSize = new Size(68, 26),
                        Padding = new Padding(6, 2, 6, 2),
                        Dock = DockStyle.Right,
                        Font = new Font("Segoe UI", 8F),
                        ForeColor = Color.DarkRed,
                        BackColor = ThemeHelper.Parchment,
                        FlatStyle = FlatStyle.Flat,
                        UseVisualStyleBackColor = false,
                        Cursor = Cursors.Hand
                    };
                    eliminarBtn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
                    eliminarBtn.Click += async (_, _) => await EliminarComentarioAsync(com);
                    headerRow.Controls.Add(eliminarBtn);
                }

                var contenidoLabel = new Label
                {
                    Text = com.Contenido,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(46, 46, 46),
                    BackColor = Color.White,
                    AutoSize = true,
                    MaximumSize = new Size(innerWidth, 0),
                    Location = new Point(10, headerRow.Bottom + 4)
                };

                card.Height = contenidoLabel.Top + contenidoLabel.PreferredHeight + 10;
                card.Controls.Add(headerRow);
                card.Controls.Add(contenidoLabel);

                _comentariosCardsPanel.Controls.Add(card);
            }

            _comentariosCardsPanel.ResumeLayout();
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

        private async Task EliminarComentarioAsync(ComentarioDTO com)
        {
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
