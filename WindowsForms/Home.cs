using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class Home : Form
    {
        private Image? beastBgImage;

        public Home()
        {
            InitializeComponent();
            LoadBackgroundImage();
            AppTheme.ApplyFormTheme(this);
        }

        private void Home_Load(object? sender, EventArgs e)
        {
            ActualizarEstadoNavbar();
            _ = CargarNoticiasAsync();
        }

        private void LoadBackgroundImage()
        {
            try
            {
                string localRes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "beast-bg.jpg");
                if (File.Exists(localRes))
                {
                    beastBgImage = Image.FromFile(localRes);
                    return;
                }

                string docsPath = Path.Combine(Directory.GetCurrentDirectory(), "docsJava", "public", "beast-bg.jpg");
                if (File.Exists(docsPath))
                {
                    beastBgImage = Image.FromFile(docsPath);
                    return;
                }

                string blazorPath = Path.Combine(Directory.GetCurrentDirectory(), "Blazor.Server", "wwwroot", "public", "beast-bg.jpg");
                if (File.Exists(blazorPath))
                {
                    beastBgImage = Image.FromFile(blazorPath);
                }
            }
            catch
            {
                beastBgImage = null;
            }
        }

        private void AboutUsPanel_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (beastBgImage != null)
            {
                g.DrawImage(beastBgImage, 0, 0, aboutUsPanel.Width, aboutUsPanel.Height);
            }
            else
            {
                using var fallbackBrush = new SolidBrush(Color.FromArgb(30, 25, 20));
                g.FillRectangle(fallbackBrush, 0, 0, aboutUsPanel.Width, aboutUsPanel.Height);
            }

            // Capa de tinte semitransparente oscuro (rgba(0,0,0,0.5)) idéntica a home.css (.aboutUs::after)
            using var tintBrush = new SolidBrush(Color.FromArgb(130, 0, 0, 0));
            g.FillRectangle(tintBrush, 0, 0, aboutUsPanel.Width, aboutUsPanel.Height);
        }

        public void ActualizarEstadoNavbar()
        {
            if (LoginForm.UsuarioLogueado != null)
            {
                var user = LoginForm.UsuarioLogueado;
                string rol = user is InvestigadorDTO || user.TipoUsuario == "Investigador" ? "Investigador" : "Lector";
                userStatusLabel.Text = $"👤 {user.Correo} ({rol})";
                userStatusLabel.Visible = true;

                authButton.Text = "Cerrar sesión";
                authButton.BackColor = AppTheme.Danger;
                authButton.ForeColor = Color.White;

                navCandidaturaBtn.Visible = rol == "Lector";
                navAdminBtn.Visible = rol == "Investigador";
            }
            else
            {
                userStatusLabel.Visible = false;
                authButton.Text = "Iniciar sesión";
                authButton.BackColor = AppTheme.GoldLight;
                authButton.ForeColor = AppTheme.TextColor;

                navCandidaturaBtn.Visible = false;
                navAdminBtn.Visible = false;
            }
        }

        private void AuthButton_Click(object? sender, EventArgs e)
        {
            if (LoginForm.UsuarioLogueado == null)
            {
                using var login = new LoginForm();
                if (login.ShowDialog(this) == DialogResult.OK)
                {
                    ActualizarEstadoNavbar();
                }
            }
            else
            {
                LoginForm.UsuarioLogueado = null;
                ActualizarEstadoNavbar();
                MessageBox.Show("Has cerrado sesión exitosamente.", "Sesión cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RefreshNews()
        {
            _ = CargarNoticiasAsync();
        }

        private async Task CargarNoticiasAsync()
        {
            try
            {
                newsFlowPanel.Controls.Clear();

                Label loadingLabel = new Label
                {
                    Text = "Cargando noticias...",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                    ForeColor = AppTheme.TextColor,
                    AutoSize = true,
                    Margin = new Padding(15)
                };
                newsFlowPanel.Controls.Add(loadingLabel);

                var noticias = await NoticiaApiClient.GetAllAsync();
                var list = noticias?.OrderByDescending(n => n.FechaPublicacion).ToList();

                newsFlowPanel.Controls.Clear();

                if (list != null && list.Count > 0)
                {
                    foreach (var noticia in list)
                    {
                        var card = CreateNewsCard(noticia);
                        newsFlowPanel.Controls.Add(card);
                    }
                }
                else
                {
                    Label emptyLabel = new Label
                    {
                        Text = "No hay noticias registradas por el momento.",
                        Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                        ForeColor = Color.DimGray,
                        AutoSize = true,
                        Margin = new Padding(15)
                    };
                    newsFlowPanel.Controls.Add(emptyLabel);
                }
            }
            catch
            {
                newsFlowPanel.Controls.Clear();
                Label errLabel = new Label
                {
                    Text = "No se pudieron cargar las noticias del servidor.",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = AppTheme.Danger,
                    AutoSize = true,
                    Margin = new Padding(15)
                };
                newsFlowPanel.Controls.Add(errLabel);
            }
        }

        private Panel CreateNewsCard(NoticiaDTO noticia)
        {
            Panel card = new Panel
            {
                Width = newsFlowPanel.Width - 30,
                AutoSize = true,
                MinimumSize = new Size(newsFlowPanel.Width - 30, 110),
                BackColor = Color.FromArgb(250, 248, 245),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 14),
                Padding = new Padding(12)
            };

            Label titleLabel = new Label
            {
                Text = noticia.Titulo,
                Font = new Font("Georgia", 11F, FontStyle.Bold),
                ForeColor = AppTheme.TextColor,
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(card.Width - 24, 0),
                Margin = new Padding(0, 0, 0, 4)
            };

            Label dateLabel = new Label
            {
                Text = $"publicado {noticia.FechaPublicacion:dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };

            Label contentLabel = new Label
            {
                Text = noticia.Contenido,
                Font = new Font("Segoe UI", 9F),
                ForeColor = AppTheme.TextColor,
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(card.Width - 24, 0)
            };

            card.Controls.Add(contentLabel);
            card.Controls.Add(dateLabel);
            card.Controls.Add(titleLabel);

            return card;
        }
    }
}
