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
    public partial class NoticiaLista : Form
    {
        private List<NoticiaDTO> _todasNoticias = new();
        private readonly Dictionary<Guid, Panel> _cardPanels = new();
        private NoticiaDTO? _selectedNoticia;
        private CheckBox _desdeCheckBox = null!;
        private DateTimePicker _desdePicker = null!;
        private CheckBox _hastaCheckBox = null!;
        private DateTimePicker _hastaPicker = null!;

        public NoticiaLista()
        {
            InitializeComponent();
            this.noticiasFlowPanel.BackColor = ThemeHelper.GridSurface;
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            InicializarFiltroFecha();
        }

        private void InicializarFiltroFecha()
        {
            var filtroPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Location = new Point(16, 8),
                Size = new Size(828, 34),
                BackColor = ThemeHelper.Bone
            };

            _desdeCheckBox = new CheckBox
            {
                Text = "Desde:",
                AutoSize = true,
                Margin = new Padding(0, 6, 4, 0)
            };
            _desdeCheckBox.CheckedChanged += (_, _) =>
            {
                _desdePicker.Enabled = _desdeCheckBox.Checked;
                AplicarFiltroFecha();
            };

            _desdePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 120,
                Margin = new Padding(0, 4, 16, 0),
                Enabled = false
            };
            _desdePicker.ValueChanged += (_, _) => { if (_desdeCheckBox.Checked) AplicarFiltroFecha(); };

            _hastaCheckBox = new CheckBox
            {
                Text = "Hasta:",
                AutoSize = true,
                Margin = new Padding(0, 6, 4, 0)
            };
            _hastaCheckBox.CheckedChanged += (_, _) =>
            {
                _hastaPicker.Enabled = _hastaCheckBox.Checked;
                AplicarFiltroFecha();
            };

            _hastaPicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 120,
                Margin = new Padding(0, 4, 16, 0),
                Enabled = false
            };
            _hastaPicker.ValueChanged += (_, _) => { if (_hastaCheckBox.Checked) AplicarFiltroFecha(); };

            var limpiarBtn = new Button
            {
                Text = "Limpiar",
                Size = new Size(80, 26),
                Margin = new Padding(0, 2, 0, 0),
                UseVisualStyleBackColor = true
            };
            limpiarBtn.Click += (_, _) =>
            {
                _desdeCheckBox.Checked = false;
                _hastaCheckBox.Checked = false;
                AplicarFiltroFecha();
            };

            var volverBtn = new Button
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(90, this.eliminarButton.Height),
                Height = this.eliminarButton.Height,
                Padding = new Padding(8, 0, 8, 0),
                Location = new Point(this.noticiasFlowPanel.Left, this.eliminarButton.Top),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            ThemeHelper.StyleBackButton(volverBtn);
            volverBtn.Click += (_, _) => this.Close();
            this.Controls.Add(volverBtn);
            volverBtn.BringToFront();

            filtroPanel.Controls.AddRange(new Control[]
            {
                _desdeCheckBox, _desdePicker,
                _hastaCheckBox, _hastaPicker,
                limpiarBtn
            });
            this.Controls.Add(filtroPanel);
        }

        private async void NoticiaLista_Load(object sender, EventArgs e)
        {
            await this.GetCategoriesAndLoad();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            NoticiaDTO noticiaNueva = new NoticiaDTO();
            using var noticiaDetalle = new NoticiaDetalle(FormMode.Add, noticiaNueva);

            ThemeHelper.ShowSingleWindow(this, noticiaDetalle);

            await this.GetCategoriesAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            if (_selectedNoticia == null) return;

            try
            {
                DeshabilitarControles();

                Guid id = _selectedNoticia.Id;
                NoticiaDTO noticia = await NoticiaApiClient.GetAsync(id);

                using var noticiaDetalle = new NoticiaDetalle(FormMode.Update, noticia);
                ThemeHelper.ShowSingleWindow(this, noticiaDetalle);

                await this.GetCategoriesAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            if (_selectedNoticia == null) return;

            NoticiaDTO noticia = _selectedNoticia;

            var result = MessageBox.Show($"¿Está seguro que desea eliminar la noticia?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeshabilitarControles();
                    await NoticiaApiClient.DeleteAsync(noticia.Id);
                    await this.GetCategoriesAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private async Task GetCategoriesAndLoad()
        {
            try
            {
                DeshabilitarControles();
                _selectedNoticia = null;
                _cardPanels.Clear();
                noticiasFlowPanel.Controls.Clear();

                var noticias = await NoticiaApiClient.GetAllAsync();
                _todasNoticias = noticias?.ToList() ?? new List<NoticiaDTO>();

                AplicarFiltroFecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar noticias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void AplicarFiltroFecha()
        {
            IEnumerable<NoticiaDTO> query = _todasNoticias;
            if (_desdeCheckBox.Checked)
            {
                DateTime desde = _desdePicker.Value.Date;
                query = query.Where(n => n.FechaPublicacion.Date >= desde);
            }
            if (_hastaCheckBox.Checked)
            {
                DateTime hasta = _hastaPicker.Value.Date;
                query = query.Where(n => n.FechaPublicacion.Date <= hasta);
            }

            var filtradas = query
                .OrderByDescending(n => n.FechaPublicacion)
                .ToList();

            RenderizarTarjetas(filtradas);
            SeleccionarNoticia(filtradas.FirstOrDefault());
        }

        private void RenderizarTarjetas(List<NoticiaDTO> noticias)
        {
            noticiasFlowPanel.SuspendLayout();
            noticiasFlowPanel.Controls.Clear();
            _cardPanels.Clear();

            if (!noticias.Any())
            {
                var emptyLabel = new Label
                {
                    Text = "No hay noticias disponibles para el filtro seleccionado.",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = ThemeHelper.TextMuted,
                    BackColor = Color.Transparent,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(noticiasFlowPanel.ClientSize.Width - 24, 120)
                };
                noticiasFlowPanel.Controls.Add(emptyLabel);
                noticiasFlowPanel.ResumeLayout();
                return;
            }

            const int columns = 3;
            const int cardMargin = 4;
            int availableWidth = noticiasFlowPanel.ClientSize.Width - noticiasFlowPanel.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 4;
            int cardWidth = Math.Max(220, (availableWidth - (columns * cardMargin * 2)) / columns);
            int innerWidth = cardWidth - 20;

            var cards = new List<Panel>(noticias.Count);

            foreach (var noticia in noticias)
            {
                var card = CrearTarjetaNoticia(noticia, cardWidth, innerWidth, cardMargin);
                cards.Add(card);
                _cardPanels[noticia.Id] = card;
            }

            // Equalize height per row of 3 cards so rows align cleanly while showing full content
            for (int i = 0; i < cards.Count; i += columns)
            {
                int rowCount = Math.Min(columns, cards.Count - i);
                int maxHeight = 110;
                for (int j = 0; j < rowCount; j++)
                {
                    maxHeight = Math.Max(maxHeight, cards[i + j].Height);
                }
                for (int j = 0; j < rowCount; j++)
                {
                    cards[i + j].Height = maxHeight;
                }
            }

            noticiasFlowPanel.Controls.AddRange(cards.ToArray());
            noticiasFlowPanel.ResumeLayout();
        }

        private Panel CrearTarjetaNoticia(NoticiaDTO noticia, int cardWidth, int innerWidth, int cardMargin)
        {
            var card = new Panel
            {
                Width = cardWidth,
                BackColor = ThemeHelper.Parchment,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(cardMargin),
                Padding = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = noticia
            };

            card.Paint += (s, e) =>
            {
                if (s is Panel p && p.Tag is NoticiaDTO n && _selectedNoticia?.Id == n.Id)
                {
                    using var pen = new Pen(ThemeHelper.OuterBrown, 2);
                    e.Graphics.DrawRectangle(pen, 0, 0, p.ClientSize.Width - 1, p.ClientSize.Height - 1);
                }
            };

            var fechaLabel = new Label
            {
                Text = noticia.FechaPublicacion.ToString("dd/MM/yyyy"),
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = ThemeHelper.TextMuted,
                BackColor = Color.Transparent,
                AutoSize = true,
                TextAlign = ContentAlignment.TopRight
            };
            int fechaWidth = fechaLabel.PreferredWidth + 6;
            int tituloMaxWidth = Math.Max(80, innerWidth - fechaWidth);

            var tituloLabel = new Label
            {
                Text = noticia.Titulo,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark,
                BackColor = Color.Transparent,
                AutoSize = true,
                MaximumSize = new Size(tituloMaxWidth, 0),
                Location = new Point(0, 0)
            };

            int headerHeight = Math.Max(tituloLabel.PreferredHeight, fechaLabel.PreferredHeight) + 4;
            fechaLabel.Location = new Point(innerWidth - fechaLabel.PreferredWidth, 1);

            var topRow = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(innerWidth, headerHeight),
                BackColor = Color.Transparent
            };
            topRow.Controls.Add(tituloLabel);
            topRow.Controls.Add(fechaLabel);

            var divider = new Panel
            {
                Location = new Point(10, topRow.Bottom + 4),
                Size = new Size(innerWidth, 1),
                BackColor = ThemeHelper.ParchmentBorder
            };

            var contenidoLabel = new Label
            {
                Text = noticia.Contenido,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = ThemeHelper.TextBody,
                BackColor = Color.Transparent,
                AutoSize = true,
                MaximumSize = new Size(innerWidth, 0),
                Location = new Point(10, divider.Bottom + 6)
            };

            card.Height = contenidoLabel.Top + contenidoLabel.PreferredHeight + 12;

            card.Controls.Add(topRow);
            card.Controls.Add(divider);
            card.Controls.Add(contenidoLabel);

            void OnClick(object? sender, EventArgs e) => SeleccionarNoticia(noticia);
            void OnDoubleClick(object? sender, EventArgs e)
            {
                SeleccionarNoticia(noticia);
                if (EsInvestigador() && actualizarButton.Enabled)
                {
                    actualizarButton_Click(this, EventArgs.Empty);
                }
            }

            foreach (Control ctrl in new Control[] { card, topRow, tituloLabel, fechaLabel, divider, contenidoLabel })
            {
                ctrl.Click += OnClick;
                ctrl.DoubleClick += OnDoubleClick;
            }

            return card;
        }

        private void SeleccionarNoticia(NoticiaDTO? noticia)
        {
            _selectedNoticia = noticia;

            foreach (var kvp in _cardPanels)
            {
                bool isSelected = noticia != null && kvp.Key == noticia.Id;
                kvp.Value.BackColor = isSelected ? ThemeHelper.ParchmentHeader : ThemeHelper.Parchment;
                kvp.Value.Invalidate();
            }

            ActualizarEstadoBotones();
        }

        private static bool EsInvestigador()
        {
            return LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
        }

        private void ActualizarEstadoBotones()
        {
            bool esInvestigador = EsInvestigador();
            bool canUpdate = esInvestigador && (actualizarButton.Tag is bool updatePermission ? updatePermission : true);
            bool canDelete = esInvestigador && (eliminarButton.Tag is bool deletePermission ? deletePermission : true);

            if (_selectedNoticia != null)
            {
                if (canDelete) this.eliminarButton.Enabled = true;
                if (canUpdate) this.actualizarButton.Enabled = true;
            }
            else
            {
                this.eliminarButton.Enabled = false;
                this.actualizarButton.Enabled = false;
            }
        }

        private void DeshabilitarControles()
        {
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            noticiasFlowPanel.Enabled = false;
        }

        private void HabilitarControles()
        {
            agregarButton.Enabled = EsInvestigador();
            noticiasFlowPanel.Enabled = true;
            ActualizarEstadoBotones();
        }
    }
}
