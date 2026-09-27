namespace WindowsForms
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            categoriasToolStripMenuItem = new ToolStripMenuItem();
            noticiasToolStripMenuItem = new ToolStripMenuItem();
            bestiasToolStripMenuItem = new ToolStripMenuItem();
            registrosToolStripMenuItem = new ToolStripMenuItem();
            cardPanel = new Panel();
            iconPictureBox = new PictureBox();
            titleLabel = new Label();
            subtitleLabel = new Label();
            categoriasButton = new Button();
            noticiasButton = new Button();
            bestiasButton = new Button();
            registrosButton = new Button();
            menuStrip1.SuspendLayout();
            cardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { categoriasToolStripMenuItem, noticiasToolStripMenuItem, bestiasToolStripMenuItem, registrosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(3, 1, 0, 1);
            menuStrip1.Size = new Size(606, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // categoriasToolStripMenuItem
            // 
            categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            categoriasToolStripMenuItem.Size = new Size(75, 22);
            categoriasToolStripMenuItem.Text = "Categorias";
            categoriasToolStripMenuItem.Click += CategoriasToolStripMenuItem_Click;
            // 
            // noticiasToolStripMenuItem
            // 
            noticiasToolStripMenuItem.Name = "noticiasToolStripMenuItem";
            noticiasToolStripMenuItem.Size = new Size(62, 22);
            noticiasToolStripMenuItem.Text = "Noticias";
            noticiasToolStripMenuItem.Click += NoticiasToolStripMenuItem_Click;
            // 
            // bestiasToolStripMenuItem
            // 
            bestiasToolStripMenuItem.Name = "bestiasToolStripMenuItem";
            bestiasToolStripMenuItem.Size = new Size(55, 22);
            bestiasToolStripMenuItem.Text = "Bestias";
            bestiasToolStripMenuItem.Click += BestiasToolStripMenuItem_Click;
            // 
            // registrosToolStripMenuItem
            // 
            registrosToolStripMenuItem.Name = "registrosToolStripMenuItem";
            registrosToolStripMenuItem.Size = new Size(67, 22);
            registrosToolStripMenuItem.Text = "Registros";
            registrosToolStripMenuItem.Click += RegistrosToolStripMenuItem_Click;
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.FromArgb(250, 248, 245);
            cardPanel.BorderStyle = BorderStyle.FixedSingle;
            cardPanel.Controls.Add(iconPictureBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(subtitleLabel);
            cardPanel.Controls.Add(categoriasButton);
            cardPanel.Controls.Add(noticiasButton);
            cardPanel.Controls.Add(bestiasButton);
            cardPanel.Controls.Add(registrosButton);
            cardPanel.Location = new Point(40, 50);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(560, 250);
            cardPanel.TabIndex = 1;
            // 
            // iconPictureBox
            // 
            iconPictureBox.Location = new Point(256, 16);
            iconPictureBox.Name = "iconPictureBox";
            iconPictureBox.Size = new Size(48, 48);
            iconPictureBox.TabIndex = 0;
            iconPictureBox.TabStop = false;
            iconPictureBox.Paint += IconPictureBox_Paint;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Georgia", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(44, 62, 80);
            titleLabel.Location = new Point(10, 70);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(540, 26);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Bestiario - Edición de Escritorio";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Font = new Font("Segoe UI", 9.5F);
            subtitleLabel.ForeColor = Color.FromArgb(85, 85, 85);
            subtitleLabel.Location = new Point(15, 100);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(530, 48);
            subtitleLabel.TabIndex = 2;
            subtitleLabel.Text = "¿Quiénes somos? En Bestiario nos dedicamos a recopilar, clasificar y registrar información sobre criaturas enigmáticas y fenómenos misteriosos.";
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // categoriasButton
            // 
            categoriasButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            categoriasButton.Location = new Point(30, 175);
            categoriasButton.Name = "categoriasButton";
            categoriasButton.Size = new Size(115, 34);
            categoriasButton.TabIndex = 3;
            categoriasButton.Text = "📜 Categorías";
            categoriasButton.UseVisualStyleBackColor = true;
            categoriasButton.Click += CategoriasToolStripMenuItem_Click;
            // 
            // noticiasButton
            // 
            noticiasButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            noticiasButton.Location = new Point(160, 175);
            noticiasButton.Name = "noticiasButton";
            noticiasButton.Size = new Size(115, 34);
            noticiasButton.TabIndex = 4;
            noticiasButton.Text = "📰 Noticias";
            noticiasButton.UseVisualStyleBackColor = true;
            noticiasButton.Click += NoticiasToolStripMenuItem_Click;
            // 
            // bestiasButton
            // 
            bestiasButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            bestiasButton.Location = new Point(290, 175);
            bestiasButton.Name = "bestiasButton";
            bestiasButton.Size = new Size(115, 34);
            bestiasButton.TabIndex = 5;
            bestiasButton.Text = "🐉 Bestias";
            bestiasButton.UseVisualStyleBackColor = true;
            bestiasButton.Click += BestiasToolStripMenuItem_Click;
            // 
            // registrosButton
            // 
            registrosButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            registrosButton.Location = new Point(420, 175);
            registrosButton.Name = "registrosButton";
            registrosButton.Size = new Size(115, 34);
            registrosButton.TabIndex = 6;
            registrosButton.Text = "📋 Registros";
            registrosButton.UseVisualStyleBackColor = true;
            registrosButton.Click += RegistrosToolStripMenuItem_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 340);
            Controls.Add(cardPanel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 1, 2, 1);
            MaximizeBox = false;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestiario - Edición de Escritorio";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            cardPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem noticiasToolStripMenuItem;
        private ToolStripMenuItem bestiasToolStripMenuItem;
        private ToolStripMenuItem registrosToolStripMenuItem;
        private Panel cardPanel;
        private PictureBox iconPictureBox;
        private Label titleLabel;
        private Label subtitleLabel;
        private Button categoriasButton;
        private Button noticiasButton;
        private Button bestiasButton;
        private Button registrosButton;
    }
}