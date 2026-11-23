using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DTOs;

namespace WindowsForms
{
    public class AdminDashboardForm : Form
    {
        private Panel _leftMenuPanel = null!;
        private Panel _rightContentPanel = null!;
        private Button _btnCategorias = null!;
        private Button _btnHabitats = null!;
        private Button _btnUsuarios = null!;
        private Button _btnTiposEvidencia = null!;
        private Form? _currentEmbeddedForm;

        public AdminDashboardForm()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO
                || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";

            if (!esInvestigador)
            {
                MessageBox.Show("Acceso denegado. Solo los investigadores pueden acceder al menú de Administrador.",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Load += (_, _) => Close();
                return;
            }

            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);

            ConfigurarFormulario();
            ConstruirLayout();
        }

        private void ConfigurarFormulario()
        {
            Text = "Menú de Administrador — Bestiario";
            ClientSize = new Size(1020, 560);
            MinimumSize = new Size(900, 500);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = ThemeHelper.Bone;
            Font = new Font("Segoe UI", 9.5F);
        }

        private void ConstruirLayout()
        {
            _leftMenuPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 210,
                BackColor = ThemeHelper.MenuBone,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var menuTitle = new Label
            {
                Text = "Gestión CRUD",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark
            };

            _btnCategorias = CrearBotonMenu("Categorías");
            _btnCategorias.Click += (_, _) => CargarSeccion(new CategoriaLista(), _btnCategorias);

            _btnHabitats = CrearBotonMenu("Hábitats");
            _btnHabitats.Click += (_, _) => CargarSeccion(new HabitatLista(), _btnHabitats);

            _btnUsuarios = CrearBotonMenu("Usuarios");
            _btnUsuarios.Click += (_, _) => CargarSeccion(new UsuarioLista(), _btnUsuarios);

            _btnTiposEvidencia = CrearBotonMenu("Tipos de Evidencia");
            _btnTiposEvidencia.Click += (_, _) => CargarSeccion(new TipoEvidenciaLista(), _btnTiposEvidencia);

            _leftMenuPanel.Controls.Add(_btnTiposEvidencia);
            _leftMenuPanel.Controls.Add(Espaciador());
            _leftMenuPanel.Controls.Add(_btnUsuarios);
            _leftMenuPanel.Controls.Add(Espaciador());
            _leftMenuPanel.Controls.Add(_btnHabitats);
            _leftMenuPanel.Controls.Add(Espaciador());
            _leftMenuPanel.Controls.Add(_btnCategorias);
            _leftMenuPanel.Controls.Add(menuTitle);

            var volverBtn = CrearBotonMenu("<- Volver");
            ThemeHelper.StyleBackButton(volverBtn);
            volverBtn.TextAlign = ContentAlignment.MiddleCenter;
            volverBtn.Padding = new Padding(0);
            volverBtn.Dock = DockStyle.Bottom;
            volverBtn.Click += (_, _) => Close();
            _leftMenuPanel.Controls.Add(volverBtn);

            _rightContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.Bone,
                Padding = new Padding(12)
            };

            MostrarBienvenida();

            Controls.Add(_rightContentPanel);
            Controls.Add(_leftMenuPanel);
        }

        private static Button CrearBotonMenu(string texto)
        {
            var btn = new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                BackColor = ThemeHelper.Parchment,
                ForeColor = ThemeHelper.TextDark,
                UseVisualStyleBackColor = false,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderColor = ThemeHelper.ParchmentBorder;
            btn.FlatAppearance.MouseOverBackColor = ThemeHelper.ParchmentHeader;
            return btn;
        }

        private static Panel Espaciador() => new Panel { Dock = DockStyle.Top, Height = 6, BackColor = ThemeHelper.MenuBone };

        private void MostrarBienvenida()
        {
            _rightContentPanel.Controls.Clear();

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.Bone,
                ColumnCount = 1,
                RowCount = 5
            };
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            var gearBox = new PictureBox
            {
                Size = new Size(48, 48),
                Anchor = AnchorStyles.None,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 10)
            };
            gearBox.Paint += GearPictureBox_Paint;

            var titleLabel = new Label
            {
                Text = "Bienvenido al menú de Admin",
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = ThemeHelper.TextDark,
                Margin = new Padding(0, 0, 0, 6)
            };

            var subtitleLabel = new Label
            {
                Text = "Seleccione una opción para gestionar en el menú de la izquierda.",
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeHelper.TextMuted
            };

            container.Controls.Add(gearBox, 0, 1);
            container.Controls.Add(titleLabel, 0, 2);
            container.Controls.Add(subtitleLabel, 0, 3);

            _rightContentPanel.Controls.Add(container);
        }

        private static void GearPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            float cx = 24f;
            float cy = 24f;
            int teeth = 8;
            float outerR = 20f;
            float innerR = 14.5f;
            float holeR = 6.5f;

            using var path = new GraphicsPath(FillMode.Alternate);
            var pts = new PointF[teeth * 4];
            double step = (2.0 * Math.PI) / teeth;

            for (int i = 0; i < teeth; i++)
            {
                double baseAngle = i * step;
                double a1 = baseAngle - step * 0.28;
                double a2 = baseAngle - step * 0.16;
                double a3 = baseAngle + step * 0.16;
                double a4 = baseAngle + step * 0.28;

                pts[i * 4 + 0] = new PointF(cx + innerR * (float)Math.Cos(a1), cy + innerR * (float)Math.Sin(a1));
                pts[i * 4 + 1] = new PointF(cx + outerR * (float)Math.Cos(a2), cy + outerR * (float)Math.Sin(a2));
                pts[i * 4 + 2] = new PointF(cx + outerR * (float)Math.Cos(a3), cy + outerR * (float)Math.Sin(a3));
                pts[i * 4 + 3] = new PointF(cx + innerR * (float)Math.Cos(a4), cy + innerR * (float)Math.Sin(a4));
            }

            path.AddPolygon(pts);
            path.AddEllipse(cx - holeR, cy - holeR, holeR * 2, holeR * 2);

            using var gearBrush = new SolidBrush(ThemeHelper.OuterBrown);
            e.Graphics.FillPath(gearBrush, path);
        }

        private void CargarSeccion(Form childForm, Button activeButton)
        {
            _currentEmbeddedForm?.Close();
            _currentEmbeddedForm?.Dispose();
            _rightContentPanel.Controls.Clear();

            foreach (Control c in _leftMenuPanel.Controls)
            {
                if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", 9.5F, btn == activeButton ? FontStyle.Bold : FontStyle.Regular);
                    btn.BackColor = btn == activeButton ? ThemeHelper.ParchmentHeader : ThemeHelper.Parchment;
                }
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            _currentEmbeddedForm = childForm;
            _rightContentPanel.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
