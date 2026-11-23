using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class Home : Form
    {
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel listoStatusLabel = null!;
        private ToolStripStatusLabel apiStatusLabel = null!;
        private ToolStripLabel usuarioMenuLabel = null!;
        private ToolStripMenuItem iniciarSesionMenuItem = null!;
        private ToolStripMenuItem cerrarSesionMenuItem = null!;
        private ToolStripMenuItem adminToolStripMenuItem = null!;
        private ToolStripMenuItem solicitudesToolStripMenuItem = null!;
        private Button adminButton = null!;
        private Button solicitudesButton = null!;
        private Button reportesButton = null!;
        private Panel upperBoxContainer = null!;
        private Panel ultimasNoticiasContainer = null!;
        private TableLayoutPanel ultimasNoticiasGrid = null!;
        private Label ultimasNoticiasStatusLabel = null!;
        private Label ultimasNoticiasCountLabel = null!;

        public Home()
        {
            InitializeComponent();
            InicializarBarraSesion();
            InicializarAccesoAdmin();
            InicializarUltimasNoticias();
            AplicarEsteticaHome();
            ActualizarEstadoSesion();
            Shown += async (_, _) => await CargarUltimasNoticiasAsync();
        }

        private void InicializarAccesoAdmin()
        {
            reportesToolStripMenuItem.DropDownItems.Clear();
            reportesToolStripMenuItem.Click += (_, _) =>
            {
                using var repForm = new ReportesForm();
                repForm.ShowDialog();
            };

            solicitudesToolStripMenuItem = new ToolStripMenuItem("Solicitudes");
            solicitudesToolStripMenuItem.Click += (_, _) => NavigateToForm(new SolicitudesInvestigadorForm());
            menuStrip1.Items.Add(solicitudesToolStripMenuItem);

            adminToolStripMenuItem = new ToolStripMenuItem("Admin");
            adminToolStripMenuItem.Click += (_, _) => NavigateToForm(new AdminDashboardForm());
            menuStrip1.Items.Add(adminToolStripMenuItem);

            // Controles de sesión alineados a la derecha en el MenuStrip (como en Web)
            iniciarSesionMenuItem = new ToolStripMenuItem("Iniciar sesión")
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.FromArgb(13, 110, 253),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            iniciarSesionMenuItem.Click += IniciarSesionToolButton_Click;

            cerrarSesionMenuItem = new ToolStripMenuItem("Cerrar sesión")
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.FromArgb(220, 53, 69),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            cerrarSesionMenuItem.Click += CerrarSesionToolButton_Click;

            usuarioMenuLabel = new ToolStripLabel("Modo Invitado")
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.DimGray
            };

            menuStrip1.Items.Add(iniciarSesionMenuItem);
            menuStrip1.Items.Add(cerrarSesionMenuItem);
            menuStrip1.Items.Add(usuarioMenuLabel);

            solicitudesButton = new Button
            {
                Text = "Solicitudes",
                Font = noticiasButton.Font,
                UseVisualStyleBackColor = true
            };
            solicitudesButton.Click += (_, _) => NavigateToForm(new SolicitudesInvestigadorForm());
            cardPanel.Controls.Add(solicitudesButton);

            adminButton = new Button
            {
                Text = "Admin",
                Font = noticiasButton.Font,
                UseVisualStyleBackColor = true
            };
            adminButton.Click += (_, _) => NavigateToForm(new AdminDashboardForm());
            cardPanel.Controls.Add(adminButton);

            reportesButton = new Button
            {
                Text = "Reportes",
                Font = noticiasButton.Font,
                UseVisualStyleBackColor = true
            };
            reportesButton.Click += (_, _) => NavigateToForm(new ReportesForm());
            cardPanel.Controls.Add(reportesButton);
        }

        private void InicializarBarraSesion()
        {
            this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height + 24);

            statusStrip = new StatusStrip
            {
                SizingGrip = false,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            listoStatusLabel = new ToolStripStatusLabel("Listo")
            {
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            apiStatusLabel = new ToolStripStatusLabel("Conectado a WebAPI: 5076")
            {
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.DimGray
            };

            statusStrip.Items.AddRange(new ToolStripItem[]
            {
                listoStatusLabel,
                apiStatusLabel
            });

            this.Controls.Add(statusStrip);
        }

        private void InicializarUltimasNoticias()
        {
            ClientSize = new Size(760, 600);

            upperBoxContainer = new Panel
            {
                BackColor = ThemeHelper.Bone,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(14),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Controls.Remove(cardPanel);
            upperBoxContainer.Controls.Add(cardPanel);
            Controls.Add(upperBoxContainer);

            ultimasNoticiasContainer = new Panel
            {
                BackColor = ThemeHelper.Bone,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 28,
                BackColor = ThemeHelper.Bone
            };

            var headerLabel = new Label
            {
                Text = "Últimas Noticias",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark,
                AutoSize = true,
                Location = new Point(0, 5)
            };

            var divider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = ThemeHelper.BoneBorder
            };

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(divider);

            ultimasNoticiasStatusLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Cargando últimas noticias...",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = ThemeHelper.TextMuted,
                TextAlign = ContentAlignment.MiddleCenter
            };

            ultimasNoticiasGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = ThemeHelper.Bone,
                Visible = false
            };
            ultimasNoticiasGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            ultimasNoticiasGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            ultimasNoticiasGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            ultimasNoticiasGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var bodyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.Bone
            };
            bodyPanel.Controls.Add(ultimasNoticiasGrid);
            bodyPanel.Controls.Add(ultimasNoticiasStatusLabel);

            var footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 26,
                BackColor = ThemeHelper.Bone
            };

            var footerDivider = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = ThemeHelper.BoneBorder
            };

            ultimasNoticiasCountLabel = new Label
            {
                Text = "Mostrando 0 noticia(s) reciente(s)",
                Font = new Font("Segoe UI", 8.25F),
                ForeColor = ThemeHelper.TextMuted,
                AutoSize = true,
                Location = new Point(0, 6)
            };

            var irListadoLink = new LinkLabel
            {
                Text = "Ir al listado completo →",
                Font = new Font("Segoe UI", 8.25F, FontStyle.Regular),
                LinkColor = ThemeHelper.OuterBrownDark,
                ActiveLinkColor = ThemeHelper.OuterBrown,
                VisitedLinkColor = ThemeHelper.OuterBrownDark,
                LinkBehavior = LinkBehavior.HoverUnderline,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            irListadoLink.Location = new Point(footerPanel.Width - irListadoLink.PreferredWidth, 6);
            irListadoLink.Click += NoticiasToolStripMenuItem_Click;

            footerPanel.Controls.Add(ultimasNoticiasCountLabel);
            footerPanel.Controls.Add(irListadoLink);
            footerPanel.Controls.Add(footerDivider);

            ultimasNoticiasContainer.Controls.Add(bodyPanel);
            ultimasNoticiasContainer.Controls.Add(footerPanel);
            ultimasNoticiasContainer.Controls.Add(headerPanel);
            Controls.Add(ultimasNoticiasContainer);

            AjustarLayoutUltimasNoticias();
        }

        private void AplicarEsteticaHome()
        {
            BackColor = ThemeHelper.OuterBrown;
            ForeColor = ThemeHelper.TextDark;

            menuStrip1.BackColor = ThemeHelper.MenuBone;
            menuStrip1.ForeColor = ThemeHelper.TextDark;

            statusStrip.BackColor = ThemeHelper.OuterBrown;
            statusStrip.ForeColor = ThemeHelper.Bone;
            listoStatusLabel.ForeColor = ThemeHelper.Bone;
            apiStatusLabel.ForeColor = ThemeHelper.Bone;

            cardPanel.BackColor = ThemeHelper.Parchment;
            titleLabel.ForeColor = ThemeHelper.TextDark;
            subtitleLabel.ForeColor = ThemeHelper.TextBody;

            ThemeHelper.ApplyToControls(upperBoxContainer.Controls);
            ThemeHelper.ApplyToControls(ultimasNoticiasContainer.Controls);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AjustarLayoutUltimasNoticias();
        }

        private void AjustarLayoutUltimasNoticias()
        {
            int menuBottom = menuStrip1.Bottom > 0 ? menuStrip1.Bottom : 24;
            int sideMargin = 30;
            int upperTop = menuBottom + 14;
            int upperPadding = 14;

            upperBoxContainer.Bounds = new Rectangle(
                sideMargin,
                upperTop,
                ClientSize.Width - (sideMargin * 2),
                cardPanel.Height + (upperPadding * 2));
            cardPanel.Location = new Point(
                Math.Max(upperPadding, (upperBoxContainer.ClientSize.Width - cardPanel.Width) / 2),
                upperPadding);

            int top = upperBoxContainer.Bottom + 14;
            int statusHeight = statusStrip.Height > 0 ? statusStrip.Height : 24;
            int bottomMargin = 14;
            int minNoticiasHeight = 180;
            int availableHeight = ClientSize.Height - statusHeight - bottomMargin - top;

            if (availableHeight < minNoticiasHeight)
            {
                ClientSize = new Size(ClientSize.Width, top + minNoticiasHeight + bottomMargin + statusHeight);
                availableHeight = minNoticiasHeight;
            }

            ultimasNoticiasContainer.Bounds = new Rectangle(sideMargin, top, ClientSize.Width - (sideMargin * 2), availableHeight);
        }

        private async Task CargarUltimasNoticiasAsync()
        {
            try
            {
                ultimasNoticiasGrid.Visible = false;
                ultimasNoticiasStatusLabel.ForeColor = Color.DimGray;
                ultimasNoticiasStatusLabel.Text = "Cargando últimas noticias...";
                ultimasNoticiasStatusLabel.Visible = true;

                var todas = await NoticiaApiClient.GetAllAsync();
                var ultimas = (todas ?? Enumerable.Empty<NoticiaDTO>())
                    .OrderByDescending(n => n.FechaPublicacion)
                    .Take(3)
                    .ToList();

                if (!ultimas.Any())
                {
                    ultimasNoticiasCountLabel.Text = "Mostrando 0 noticia(s) reciente(s)";
                    ultimasNoticiasStatusLabel.Text = "No hay noticias publicadas aún.";
                    return;
                }

                ultimasNoticiasGrid.SuspendLayout();
                ultimasNoticiasGrid.Controls.Clear();

                for (int i = 0; i < ultimas.Count; i++)
                {
                    var noticia = ultimas[i];
                    var card = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = ThemeHelper.Parchment,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(i == 0 ? 0 : 4, 0, i == 2 ? 0 : 4, 0),
                        Padding = new Padding(8)
                    };

                    var topRow = new Panel
                    {
                        Dock = DockStyle.Top,
                        Height = 22,
                        BackColor = ThemeHelper.Parchment
                    };

                    var fechaLabel = new Label
                    {
                        Text = noticia.FechaPublicacion.ToString("dd/MM/yyyy"),
                        Font = new Font("Segoe UI", 7.5F),
                        ForeColor = ThemeHelper.TextMuted,
                        Dock = DockStyle.Right,
                        AutoSize = true,
                        TextAlign = ContentAlignment.TopRight
                    };

                    var tituloLabel = new Label
                    {
                        Text = noticia.Titulo,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = ThemeHelper.TextDark,
                        Dock = DockStyle.Fill,
                        AutoEllipsis = true
                    };

                    topRow.Controls.Add(tituloLabel);
                    topRow.Controls.Add(fechaLabel);

                    var contenidoLabel = new Label
                    {
                        Text = noticia.Contenido,
                        Font = new Font("Segoe UI", 8.5F),
                        ForeColor = ThemeHelper.TextBody,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(0, 4, 0, 0),
                        AutoEllipsis = true
                    };

                    card.Controls.Add(contenidoLabel);
                    card.Controls.Add(topRow);
                    ultimasNoticiasGrid.Controls.Add(card, i, 0);
                }

                ultimasNoticiasGrid.ResumeLayout();
                ultimasNoticiasCountLabel.Text = $"Mostrando {ultimas.Count} noticia(s) reciente(s)";
                ultimasNoticiasStatusLabel.Visible = false;
                ultimasNoticiasGrid.Visible = true;
            }
            catch (Exception ex)
            {
                ultimasNoticiasGrid.Visible = false;
                ultimasNoticiasStatusLabel.ForeColor = Color.DarkRed;
                ultimasNoticiasStatusLabel.Text = $"No se pudieron cargar las noticias: {ex.Message}";
                ultimasNoticiasStatusLabel.Visible = true;
            }
        }

        private void ActualizarEstadoSesion()
        {
            var user = LoginForm.UsuarioLogueado;
            bool esInvestigador = user is InvestigadorDTO || user?.TipoUsuario == "Investigador";

            // Toda opción gestionada desde Admin o dentro de Bestias/Registros desaparece del inicio
            categoriasToolStripMenuItem.Visible = false;
            registrosToolStripMenuItem.Visible = false;
            habitatsToolStripMenuItem.Visible = false;
            tiposEvidenciaToolStripMenuItem.Visible = false;
            usuariosToolStripMenuItem.Visible = false;
            evidenciasToolStripMenuItem.Visible = false;
            comentariosToolStripMenuItem.Visible = false;

            reportesToolStripMenuItem.Visible = esInvestigador;
            adminToolStripMenuItem.Visible = esInvestigador;
            solicitudesToolStripMenuItem.Visible = user != null;
            solicitudesToolStripMenuItem.Text = esInvestigador ? "Solicitudes" : "Volverse Investigador";

            categoriasButton.Visible = false;
            registrosButton.Visible = false;
            habitatsButton.Visible = false;
            tiposEvidenciaButton.Visible = false;
            usuariosButton.Visible = false;
            evidenciasButton.Visible = false;
            comentariosButton.Visible = false;

            reporteEstadisticasButton.Visible = false;
            reporteRegistrosButton.Visible = false;
            reportesButton.Visible = esInvestigador;
            adminButton.Visible = esInvestigador;
            solicitudesButton.Visible = user != null;
            solicitudesButton.Text = esInvestigador ? "Solicitudes" : "Volverse Investigador";

            int yRow2 = noticiasButton.Bottom + Math.Max(10, (reporteEstadisticasButton.Top - noticiasButton.Bottom - noticiasButton.Height) / 2);
            if (esInvestigador)
            {
                solicitudesButton.Size = noticiasButton.Size;
                solicitudesButton.Location = new Point(noticiasButton.Left, yRow2);
                adminButton.Size = bestiasButton.Size;
                adminButton.Location = new Point(bestiasButton.Left, yRow2);

                int wRep = bestiasButton.Right - noticiasButton.Left;
                reportesButton.Size = new Size(wRep, noticiasButton.Height);
                reportesButton.Location = new Point((cardPanel.ClientSize.Width - wRep) / 2, reporteEstadisticasButton.Top);
            }
            else if (user != null)
            {
                int w = (bestiasButton.Right - noticiasButton.Left);
                solicitudesButton.Size = new Size(w, noticiasButton.Height);
                solicitudesButton.Location = new Point((cardPanel.ClientSize.Width - w) / 2, yRow2);
            }

            if (user == null)
            {
                usuarioMenuLabel.Text = "Modo Invitado";
                iniciarSesionMenuItem.Visible = true;
                cerrarSesionMenuItem.Visible = false;
            }
            else
            {
                string rol = esInvestigador ? "Investigador" : "Lector";
                usuarioMenuLabel.Text = $"Usuario: {user.Correo} ({rol})";
                iniciarSesionMenuItem.Visible = false;
                cerrarSesionMenuItem.Visible = true;
            }
        }

        private void IniciarSesionToolButton_Click(object? sender, EventArgs e)
        {
            using var login = new LoginForm();
            ThemeHelper.ShowSingleWindow(this, login);
            ActualizarEstadoSesion();
        }

        private void CerrarSesionToolButton_Click(object? sender, EventArgs e)
        {
            LoginForm.UsuarioLogueado = null;
            BaseApiClient.ClearAuthToken();
            ActualizarEstadoSesion();
        }

        private void NavigateToForm(Form form, Func<Task>? onReturn = null)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormClosed += async (_, _) =>
            {
                if (!this.IsDisposed)
                {
                    this.Show();
                    this.Activate();
                    if (onReturn != null)
                    {
                        await onReturn();
                    }
                }
                form.Dispose();
            };
            this.Hide();
            form.Show();
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new CategoriaLista());
        }

        private void NoticiasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            NavigateToForm(new NoticiaLista(), CargarUltimasNoticiasAsync);
        }

        private void BestiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new BestiaLista());
        }

        private void RegistrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new RegistroLista());
        }

        private void HabitatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new HabitatLista());
        }

        private void TiposEvidenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new TipoEvidenciaLista());
        }

        private void EvidenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new EvidenciaLista());
        }

        private void ComentariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new ComentarioLista());
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new UsuarioLista());
        }

        private void EstadisticasBestiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new ReportesForm(0));
        }

        private void RegistrosPorBestiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToForm(new ReportesForm(1));
        }

        private void IconPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Color del libro unificado con el tema (#8E6E53)
            using var bookBrush = new SolidBrush(ThemeHelper.OuterBrown);

            // Cuerpo del libro con esquinas suavemente redondeadas
            var bookRect = new Rectangle(6, 4, 36, 40);
            using var path = new GraphicsPath();
            int radius = 4;
            path.AddArc(bookRect.X, bookRect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bookRect.X, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.FillPath(bookBrush, path);

            // Cinta de marcador blanca
            using var whiteBrush = new SolidBrush(Color.White);
            Point[] ribbon = {
                new Point(20, 4),
                new Point(28, 4),
                new Point(28, 18),
                new Point(24, 14),
                new Point(20, 18)
            };
            e.Graphics.FillPolygon(whiteBrush, ribbon);

            // Pliegue inferior de páginas
            using var pagePen = new Pen(Color.White, 2.5f);
            e.Graphics.DrawLine(pagePen, 10, 36, 38, 36);
        }
    }
}
