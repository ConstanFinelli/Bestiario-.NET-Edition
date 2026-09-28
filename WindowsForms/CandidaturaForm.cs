using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class CandidaturaForm : Form
    {
        public CandidaturaForm()
        {
            InitializeComponent();
            AppTheme.ApplyFormTheme(this);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.headerLabel = new Label();
            this.alertLabel = new Label();
            this.nombreLabel = new Label();
            this.nombreTextBox = new TextBox();
            this.apellidoLabel = new Label();
            this.apellidoTextBox = new TextBox();
            this.dniLabel = new Label();
            this.dniTextBox = new TextBox();
            this.cancelarButton = new Button();
            this.guardarButton = new Button();

            // Header Panel
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 44;
            this.headerPanel.BackColor = AppTheme.Primary;
            this.headerPanel.Controls.Add(this.headerLabel);

            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.Font = new Font("Georgia", 11.25F, FontStyle.Bold);
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Text = "📜 PRESENTAR CANDIDATURA A INVESTIGADOR";
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Alert Label
            this.alertLabel.BackColor = Color.FromArgb(255, 251, 235);
            this.alertLabel.ForeColor = Color.FromArgb(146, 64, 14);
            this.alertLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.alertLabel.Text = "⚠️ Aún no implementado en .NET";
            this.alertLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.alertLabel.Location = new Point(50, 60);
            this.alertLabel.Size = new Size(350, 36);
            this.alertLabel.BorderStyle = BorderStyle.FixedSingle;

            // Form inputs
            this.nombreLabel.Text = "Nombre:";
            this.nombreLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.nombreLabel.ForeColor = AppTheme.TextColor;
            this.nombreLabel.Location = new Point(50, 110);
            this.nombreLabel.Size = new Size(100, 20);

            this.nombreTextBox.Location = new Point(50, 130);
            this.nombreTextBox.Size = new Size(350, 24);
            this.nombreTextBox.PlaceholderText = "Ej: Juan";

            this.apellidoLabel.Text = "Apellido:";
            this.apellidoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.apellidoLabel.ForeColor = AppTheme.TextColor;
            this.apellidoLabel.Location = new Point(50, 165);
            this.apellidoLabel.Size = new Size(100, 20);

            this.apellidoTextBox.Location = new Point(50, 185);
            this.apellidoTextBox.Size = new Size(350, 24);
            this.apellidoTextBox.PlaceholderText = "Ej: Pérez";

            this.dniLabel.Text = "DNI / Identificación:";
            this.dniLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dniLabel.ForeColor = AppTheme.TextColor;
            this.dniLabel.Location = new Point(50, 220);
            this.dniLabel.Size = new Size(150, 20);

            this.dniTextBox.Location = new Point(50, 240);
            this.dniTextBox.Size = new Size(350, 24);
            this.dniTextBox.PlaceholderText = "Ej: 12345678";

            // Buttons
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.Location = new Point(50, 290);
            this.cancelarButton.Size = new Size(160, 34);
            this.cancelarButton.Click += (s, e) => this.Close();

            this.guardarButton.Text = "Guardar →";
            this.guardarButton.Location = new Point(240, 290);
            this.guardarButton.Size = new Size(160, 34);
            this.guardarButton.Click += GuardarButton_Click;

            this.ClientSize = new Size(450, 350);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.alertLabel);
            this.Controls.Add(this.nombreLabel);
            this.Controls.Add(this.nombreTextBox);
            this.Controls.Add(this.apellidoLabel);
            this.Controls.Add(this.apellidoTextBox);
            this.Controls.Add(this.dniLabel);
            this.Controls.Add(this.dniTextBox);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.guardarButton);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bestiario - Presentar Candidatura";
        }

        private void GuardarButton_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("⚠️ El flujo de aprobación de candidaturas a investigador aún no está habilitado en la API .NET.",
                "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private Panel headerPanel = null!;
        private Label headerLabel = null!;
        private Label alertLabel = null!;
        private Label nombreLabel = null!;
        private TextBox nombreTextBox = null!;
        private Label apellidoLabel = null!;
        private TextBox apellidoTextBox = null!;
        private Label dniLabel = null!;
        private TextBox dniTextBox = null!;
        private Button cancelarButton = null!;
        private Button guardarButton = null!;
    }
}
