using System;
using System.Drawing;
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
            BackColor = Color.FromArgb(240, 240, 240);
            Font = new Font("Segoe UI", 9.5F);
        }

        private void ConstruirLayout()
        {
            _leftMenuPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 210,
                BackColor = Color.FromArgb(242, 242, 242),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var menuTitle = new Label
            {
                Text = "Gestión CRUD",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
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

            _rightContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(12)
            };

            MostrarBienvenida();

            Controls.Add(_rightContentPanel);
            Controls.Add(_leftMenuPanel);
        }

        private static Button CrearBotonMenu(string texto)
        {
            return new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Standard
            };
        }

        private static Panel Espaciador() => new Panel { Dock = DockStyle.Top, Height = 6 };

        private void MostrarBienvenida()
        {
            _rightContentPanel.Controls.Clear();
            var welcome = new Label
            {
                Text = "Bienvenido al menú de Admin\n\nSeleccione una opción para gestionar en el menú de la izquierda.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(70, 70, 70)
            };
            _rightContentPanel.Controls.Add(welcome);
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
                    btn.BackColor = btn == activeButton ? Color.FromArgb(225, 236, 255) : Color.White;
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
