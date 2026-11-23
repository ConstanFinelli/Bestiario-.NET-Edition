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

        public Home()
        {
            InitializeComponent();
            InicializarBarraSesion();
            InicializarAccesoAdmin();
            ActualizarEstadoSesion();
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
            solicitudesToolStripMenuItem.Click += (_, _) =>
            {
                using var solForm = new SolicitudesInvestigadorForm();
                solForm.ShowDialog();
            };
            menuStrip1.Items.Add(solicitudesToolStripMenuItem);

            adminToolStripMenuItem = new ToolStripMenuItem("Admin");
            adminToolStripMenuItem.Click += (_, _) =>
            {
                using var adminForm = new AdminDashboardForm();
                adminForm.ShowDialog();
            };
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
            solicitudesButton.Click += (_, _) =>
            {
                using var solForm = new SolicitudesInvestigadorForm();
                solForm.ShowDialog();
            };
            cardPanel.Controls.Add(solicitudesButton);

            adminButton = new Button
            {
                Text = "Admin",
                Font = noticiasButton.Font,
                UseVisualStyleBackColor = true
            };
            adminButton.Click += (_, _) =>
            {
                using var adminForm = new AdminDashboardForm();
                adminForm.ShowDialog();
            };
            cardPanel.Controls.Add(adminButton);

            reportesButton = new Button
            {
                Text = "Reportes",
                Font = noticiasButton.Font,
                UseVisualStyleBackColor = true
            };
            reportesButton.Click += (_, _) =>
            {
                using var repForm = new ReportesForm();
                repForm.ShowDialog();
            };
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
            login.ShowDialog();
            ActualizarEstadoSesion();
        }

        private void CerrarSesionToolButton_Click(object? sender, EventArgs e)
        {
            LoginForm.UsuarioLogueado = null;
            BaseApiClient.ClearAuthToken();
            ActualizarEstadoSesion();
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoriaLista categoriasForm = new CategoriaLista();
            categoriasForm.ShowDialog();
        }

        private void NoticiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoticiaLista noticiasForm = new NoticiaLista();
            noticiasForm.ShowDialog();
        }

        private void BestiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BestiaLista bestiasForm = new BestiaLista();
            bestiasForm.ShowDialog();
        }

        private void RegistrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroLista registrosForm = new RegistroLista();
            registrosForm.ShowDialog();
        }

        private void HabitatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HabitatLista habitatsForm = new HabitatLista();
            habitatsForm.ShowDialog();
        }

        private void TiposEvidenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoEvidenciaLista tiposForm = new TipoEvidenciaLista();
            tiposForm.ShowDialog();
        }

        private void EvidenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvidenciaLista evidenciasForm = new EvidenciaLista();
            evidenciasForm.ShowDialog();
        }

        private void ComentariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComentarioLista comentariosForm = new ComentarioLista();
            comentariosForm.ShowDialog();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioLista usuariosForm = new UsuarioLista();
            usuariosForm.ShowDialog();
        }

        private void EstadisticasBestiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new ReportesForm(0);
            form.ShowDialog();
        }

        private void RegistrosPorBestiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new ReportesForm(1);
            form.ShowDialog();
        }

        private void IconPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Color azul libro idéntico al icono web (#0d6efd)
            using var blueBrush = new SolidBrush(Color.FromArgb(13, 110, 253));

            // Cuerpo del libro con esquinas suavemente redondeadas
            var bookRect = new Rectangle(6, 4, 36, 40);
            using var path = new GraphicsPath();
            int radius = 4;
            path.AddArc(bookRect.X, bookRect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bookRect.X, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.FillPath(blueBrush, path);

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
