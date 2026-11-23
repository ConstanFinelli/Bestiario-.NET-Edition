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
                var editarBtn = new Button
                {
                    Text = "Editar Registro",
                    Width = 125,
                    Height = 28,
                    Dock = DockStyle.Right,
                    BackColor = Color.FromArgb(13, 110, 253),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                editarBtn.Click += (_, _) =>
                {
                    using var detalle = new RegistroDetalle(FormMode.Update, _registro, _bestias);
                    detalle.ShowDialog();
                    SeEdito = true;
                    Close();
                };
                bottomPanel.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
                bottomPanel.Controls.Add(editarBtn);
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
                Height = 105,
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

            evidenciasPanel.Controls.Add(_evidenciasListBox);
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

                var filtradas = deLaBestia
                    .Where(e => esInvestigador
                        || (e.Estado != null && e.Estado.Equals("aprobado", StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(e => e.NroEvidencia)
                    .ToList();

                _evidenciasLabel.Text = $"Evidencias registradas ({filtradas.Count})";
                _evidenciasListBox.Items.Clear();

                if (!filtradas.Any())
                {
                    _evidenciasListBox.Items.Add("No hay evidencias registradas para esta bestia.");
                    return;
                }

                foreach (var ev in filtradas)
                {
                    string tipo = ev.TipoEvidenciaDescripcion ?? "Evidencia";
                    _evidenciasListBox.Items.Add($"#{ev.NroEvidencia} [{ev.Estado}] {tipo} — {ev.FechaObtencion:dd/MM/yyyy} ({ev.FileId})");
                }
            }
            catch
            {
                _evidenciasLabel.Text = "Evidencias registradas (no disponibles)";
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
