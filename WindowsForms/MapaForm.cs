using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class MapaForm : Form
    {
        public MapaForm()
        {
            InitializeComponent();
            AppTheme.ApplyFormTheme(this);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.headerLabel = new Label();
            this.infoPanel = new Panel();
            this.titleLabel = new Label();
            this.alertLabel = new Label();
            this.descLabel1 = new Label();
            this.descLabel2 = new Label();
            this.descLabel3 = new Label();
            this.volverButton = new Button();
            this.mapCanvas = new PictureBox();

            // Header Panel
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 48;
            this.headerPanel.BackColor = AppTheme.Primary;
            this.headerPanel.Controls.Add(this.headerLabel);

            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.Font = new Font("Georgia", 12F, FontStyle.Bold);
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Text = "🗺️ MAPA DE HÁBITATS Y AVISTAMIENTOS";
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Map Canvas (Simulación visual de mapa cartográfico)
            this.mapCanvas.Dock = DockStyle.Fill;
            this.mapCanvas.BackColor = Color.FromArgb(220, 214, 198);
            this.mapCanvas.Paint += MapCanvas_Paint;

            // Info Panel (Lateral / Inferior)
            this.infoPanel.Dock = DockStyle.Right;
            this.infoPanel.Width = 380;
            this.infoPanel.BackColor = AppTheme.Secondary;
            this.infoPanel.Padding = new Padding(20);

            this.titleLabel.Font = new Font("Georgia", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = AppTheme.TextColor;
            this.titleLabel.Text = "Hábitats de Bestias";
            this.titleLabel.Location = new Point(20, 25);
            this.titleLabel.Size = new Size(340, 30);

            this.alertLabel.BackColor = Color.FromArgb(255, 251, 235);
            this.alertLabel.ForeColor = Color.FromArgb(146, 64, 14);
            this.alertLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.alertLabel.Text = "⚠️ Aún no implementado en .NET";
            this.alertLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.alertLabel.Location = new Point(20, 65);
            this.alertLabel.Size = new Size(340, 38);
            this.alertLabel.BorderStyle = BorderStyle.FixedSingle;

            this.descLabel1.Font = new Font("Segoe UI", 9.5F);
            this.descLabel1.ForeColor = AppTheme.TextColor;
            this.descLabel1.Text = "En este mapa interactivo se visualizarán las ubicaciones geográficas de las bestias enigmáticas registradas por la comunidad.";
            this.descLabel1.Location = new Point(20, 115);
            this.descLabel1.Size = new Size(340, 50);

            this.descLabel2.Font = new Font("Segoe UI", 9.5F);
            this.descLabel2.ForeColor = AppTheme.TextColor;
            this.descLabel2.Text = "Estas ubicaciones son determinadas con testimonios y registros contrastados por investigadores del Bestiario.";
            this.descLabel2.Location = new Point(20, 175);
            this.descLabel2.Size = new Size(340, 50);

            this.descLabel3.Font = new Font("Segoe UI", 9.5F);
            this.descLabel3.ForeColor = AppTheme.TextColor;
            this.descLabel3.Text = "El sistema de georreferenciación de hábitats estará disponible en una próxima actualización del servicio.";
            this.descLabel3.Location = new Point(20, 235);
            this.descLabel3.Size = new Size(340, 50);

            this.volverButton.Text = "Volver al Inicio";
            this.volverButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.volverButton.Location = new Point(20, 310);
            this.volverButton.Size = new Size(340, 36);
            this.volverButton.Click += (s, e) => this.Close();

            this.infoPanel.Controls.Add(this.titleLabel);
            this.infoPanel.Controls.Add(this.alertLabel);
            this.infoPanel.Controls.Add(this.descLabel1);
            this.infoPanel.Controls.Add(this.descLabel2);
            this.infoPanel.Controls.Add(this.descLabel3);
            this.infoPanel.Controls.Add(this.volverButton);

            // Form properties
            this.ClientSize = new Size(950, 560);
            this.Controls.Add(this.mapCanvas);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.headerPanel);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bestiario - Mapa de Hábitats";
        }

        private void MapCanvas_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibujar rejilla de coordenadas cartográficas de estilo medieval
            using var gridPen = new Pen(Color.FromArgb(200, 190, 170), 1f) { DashStyle = DashStyle.Dash };
            for (int x = 40; x < mapCanvas.Width; x += 60)
            {
                g.DrawLine(gridPen, x, 0, x, mapCanvas.Height);
            }
            for (int y = 40; y < mapCanvas.Height; y += 60)
            {
                g.DrawLine(gridPen, 0, y, mapCanvas.Width, y);
            }

            // Marcadores ficticios con estilo medieval
            DrawMapMarker(g, mapCanvas.Width / 3, mapCanvas.Height / 3, "🐉 Lago Ness", "Tierras Altas de Escocia");
            DrawMapMarker(g, (int)(mapCanvas.Width * 0.6), (int)(mapCanvas.Height * 0.45), "🌲 Pie Grande", "Bosques del Pacífico");
            DrawMapMarker(g, (int)(mapCanvas.Width * 0.45), (int)(mapCanvas.Height * 0.65), "🏜️ Chupacabras", "Región Centroamericana");

            // Rosa de los vientos estilizada
            using var roseBrush = new SolidBrush(Color.FromArgb(120, AppTheme.Primary));
            using var font = new Font("Georgia", 9F, FontStyle.Bold);
            g.DrawString("N", font, roseBrush, mapCanvas.Width - 50, 20);
            g.DrawString("S", font, roseBrush, mapCanvas.Width - 50, 60);
            g.DrawString("O", font, roseBrush, mapCanvas.Width - 70, 40);
            g.DrawString("E", font, roseBrush, mapCanvas.Width - 30, 40);
        }

        private void DrawMapMarker(Graphics g, int x, int y, string title, string sub)
        {
            using var pinBrush = new SolidBrush(AppTheme.Danger);
            g.FillEllipse(pinBrush, x - 6, y - 6, 12, 12);
            using var goldPen = new Pen(AppTheme.GoldLight, 2f);
            g.DrawEllipse(goldPen, x - 6, y - 6, 12, 12);

            using var textBrush = new SolidBrush(AppTheme.TextColor);
            using var titleFont = new Font("Georgia", 9F, FontStyle.Bold);
            using var subFont = new Font("Segoe UI", 8F);
            g.DrawString(title, titleFont, textBrush, x + 10, y - 10);
            g.DrawString(sub, subFont, Brushes.Gray, x + 10, y + 5);
        }

        private Panel headerPanel = null!;
        private Label headerLabel = null!;
        private Panel infoPanel = null!;
        private Label titleLabel = null!;
        private Label alertLabel = null!;
        private Label descLabel1 = null!;
        private Label descLabel2 = null!;
        private Label descLabel3 = null!;
        private Button volverButton = null!;
        private PictureBox mapCanvas = null!;
    }
}
