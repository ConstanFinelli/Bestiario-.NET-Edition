using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
            AppTheme.ApplyFormTheme(this);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.headerLabel = new Label();
            this.panelCentral = new Panel();
            this.btnBestias = new Button();
            this.btnCategorias = new Button();
            this.btnNoticias = new Button();
            this.btnRegistros = new Button();
            this.btnCandidaturas = new Button();
            this.btnVolver = new Button();

            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 48;
            this.headerPanel.BackColor = AppTheme.Primary;
            this.headerPanel.Controls.Add(this.headerLabel);

            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.Font = new Font("Georgia", 12F, FontStyle.Bold);
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Text = "🛡️ PANEL DE ADMINISTRACIÓN";
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.panelCentral.Dock = DockStyle.Fill;
            this.panelCentral.Padding = new Padding(30);
            this.panelCentral.BackColor = AppTheme.Secondary;

            this.btnBestias.Text = "🐉 Gestionar Bestias";
            this.btnBestias.Location = new Point(50, 40);
            this.btnBestias.Size = new Size(220, 45);
            this.btnBestias.Click += (s, e) => { new BestiaLista().ShowDialog(); };

            this.btnCategorias.Text = "📜 Gestionar Categorías";
            this.btnCategorias.Location = new Point(310, 40);
            this.btnCategorias.Size = new Size(220, 45);
            this.btnCategorias.Click += (s, e) => { new CategoriaLista().ShowDialog(); };

            this.btnNoticias.Text = "📰 Gestionar Noticias";
            this.btnNoticias.Location = new Point(50, 105);
            this.btnNoticias.Size = new Size(220, 45);
            this.btnNoticias.Click += (s, e) => { new NoticiaLista().ShowDialog(); };

            this.btnRegistros.Text = "📋 Gestionar Registros";
            this.btnRegistros.Location = new Point(310, 105);
            this.btnRegistros.Size = new Size(220, 45);
            this.btnRegistros.Click += (s, e) => { new RegistroLista().ShowDialog(); };

            this.btnCandidaturas.Text = "👥 Solicitudes Investigador (Aún no implementado)";
            this.btnCandidaturas.Location = new Point(50, 170);
            this.btnCandidaturas.Size = new Size(480, 45);
            this.btnCandidaturas.Click += (s, e) =>
            {
                MessageBox.Show("Aún no implementado en .NET", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            this.btnVolver.Text = "Volver al Inicio";
            this.btnVolver.Location = new Point(180, 240);
            this.btnVolver.Size = new Size(220, 38);
            this.btnVolver.Click += (s, e) => this.Close();

            this.panelCentral.Controls.Add(this.btnBestias);
            this.panelCentral.Controls.Add(this.btnCategorias);
            this.panelCentral.Controls.Add(this.btnNoticias);
            this.panelCentral.Controls.Add(this.btnRegistros);
            this.panelCentral.Controls.Add(this.btnCandidaturas);
            this.panelCentral.Controls.Add(this.btnVolver);

            this.ClientSize = new Size(580, 360);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bestiario - Panel de Administración";
        }

        private Panel headerPanel = null!;
        private Label headerLabel = null!;
        private Panel panelCentral = null!;
        private Button btnBestias = null!;
        private Button btnCategorias = null!;
        private Button btnNoticias = null!;
        private Button btnRegistros = null!;
        private Button btnCandidaturas = null!;
        private Button btnVolver = null!;
    }
}
