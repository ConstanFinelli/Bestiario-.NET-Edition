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
            habitatsToolStripMenuItem = new ToolStripMenuItem();
            tiposEvidenciaToolStripMenuItem = new ToolStripMenuItem();
            evidenciasToolStripMenuItem = new ToolStripMenuItem();
            comentariosToolStripMenuItem = new ToolStripMenuItem();
            cardPanel = new Panel();
            iconPictureBox = new PictureBox();
            titleLabel = new Label();
            subtitleLabel = new Label();
            categoriasButton = new Button();
            noticiasButton = new Button();
            bestiasButton = new Button();
            registrosButton = new Button();
            habitatsButton = new Button();
            tiposEvidenciaButton = new Button();
            evidenciasButton = new Button();
            comentariosButton = new Button();
            menuStrip1.SuspendLayout();
            cardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { categoriasToolStripMenuItem, noticiasToolStripMenuItem, bestiasToolStripMenuItem, registrosToolStripMenuItem, habitatsToolStripMenuItem, tiposEvidenciaToolStripMenuItem, evidenciasToolStripMenuItem, comentariosToolStripMenuItem });
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
            // habitatsToolStripMenuItem
            // 
            habitatsToolStripMenuItem.Name = "habitatsToolStripMenuItem";
            habitatsToolStripMenuItem.Size = new Size(65, 22);
            habitatsToolStripMenuItem.Text = "Hábitats";
            habitatsToolStripMenuItem.Click += HabitatsToolStripMenuItem_Click;
            // 
            // tiposEvidenciaToolStripMenuItem
            // 
            tiposEvidenciaToolStripMenuItem.Name = "tiposEvidenciaToolStripMenuItem";
            tiposEvidenciaToolStripMenuItem.Size = new Size(105, 22);
            tiposEvidenciaToolStripMenuItem.Text = "Tipos Evidencia";
            tiposEvidenciaToolStripMenuItem.Click += TiposEvidenciaToolStripMenuItem_Click;
            // 
            // evidenciasToolStripMenuItem
            // 
            evidenciasToolStripMenuItem.Name = "evidenciasToolStripMenuItem";
            evidenciasToolStripMenuItem.Size = new Size(80, 22);
            evidenciasToolStripMenuItem.Text = "Evidencias";
            evidenciasToolStripMenuItem.Click += EvidenciasToolStripMenuItem_Click;
            // 
            // comentariosToolStripMenuItem
            // 
            comentariosToolStripMenuItem.Name = "comentariosToolStripMenuItem";
            comentariosToolStripMenuItem.Size = new Size(85, 22);
            comentariosToolStripMenuItem.Text = "Comentarios";
            comentariosToolStripMenuItem.Click += ComentariosToolStripMenuItem_Click;
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.White;
            cardPanel.BorderStyle = BorderStyle.FixedSingle;
            cardPanel.Controls.Add(iconPictureBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(subtitleLabel);
            cardPanel.Controls.Add(categoriasButton);
            cardPanel.Controls.Add(noticiasButton);
            cardPanel.Controls.Add(bestiasButton);
            cardPanel.Controls.Add(registrosButton);
            cardPanel.Controls.Add(habitatsButton);
            cardPanel.Controls.Add(tiposEvidenciaButton);
            cardPanel.Controls.Add(evidenciasButton);
            cardPanel.Controls.Add(comentariosButton);
            cardPanel.Location = new Point(43, 50);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(560, 275);
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
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            titleLabel.Location = new Point(10, 72);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(540, 25);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Bestiario - .NET Edition";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Font = new Font("Segoe UI", 9F);
            subtitleLabel.ForeColor = Color.FromArgb(85, 85, 85);
            subtitleLabel.Location = new Point(10, 102);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(540, 36);
            subtitleLabel.TabIndex = 2;
            subtitleLabel.Text = "Seleccione una opción del menú superior para comenzar a gestionar el catálogo del bestiario.";
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // categoriasButton
            // 
            categoriasButton.Font = new Font("Segoe UI", 9F);
            categoriasButton.Location = new Point(25, 150);
            categoriasButton.Name = "categoriasButton";
            categoriasButton.Size = new Size(120, 28);
            categoriasButton.TabIndex = 3;
            categoriasButton.Text = "Categorias";
            categoriasButton.UseVisualStyleBackColor = true;
            categoriasButton.Click += CategoriasToolStripMenuItem_Click;
            // 
            // noticiasButton
            // 
            noticiasButton.Font = new Font("Segoe UI", 9F);
            noticiasButton.Location = new Point(155, 150);
            noticiasButton.Name = "noticiasButton";
            noticiasButton.Size = new Size(120, 28);
            noticiasButton.TabIndex = 4;
            noticiasButton.Text = "Noticias";
            noticiasButton.UseVisualStyleBackColor = true;
            noticiasButton.Click += NoticiasToolStripMenuItem_Click;
            // 
            // bestiasButton
            // 
            bestiasButton.Font = new Font("Segoe UI", 9F);
            bestiasButton.Location = new Point(285, 150);
            bestiasButton.Name = "bestiasButton";
            bestiasButton.Size = new Size(120, 28);
            bestiasButton.TabIndex = 5;
            bestiasButton.Text = "Bestias";
            bestiasButton.UseVisualStyleBackColor = true;
            bestiasButton.Click += BestiasToolStripMenuItem_Click;
            // 
            // registrosButton
            // 
            registrosButton.Font = new Font("Segoe UI", 9F);
            registrosButton.Location = new Point(415, 150);
            registrosButton.Name = "registrosButton";
            registrosButton.Size = new Size(120, 28);
            registrosButton.TabIndex = 6;
            registrosButton.Text = "Registros";
            registrosButton.UseVisualStyleBackColor = true;
            registrosButton.Click += RegistrosToolStripMenuItem_Click;
            // 
            // habitatsButton
            // 
            habitatsButton.Font = new Font("Segoe UI", 9F);
            habitatsButton.Location = new Point(25, 190);
            habitatsButton.Name = "habitatsButton";
            habitatsButton.Size = new Size(120, 28);
            habitatsButton.TabIndex = 7;
            habitatsButton.Text = "Hábitats";
            habitatsButton.UseVisualStyleBackColor = true;
            habitatsButton.Click += HabitatsToolStripMenuItem_Click;
            // 
            // tiposEvidenciaButton
            // 
            tiposEvidenciaButton.Font = new Font("Segoe UI", 9F);
            tiposEvidenciaButton.Location = new Point(155, 190);
            tiposEvidenciaButton.Name = "tiposEvidenciaButton";
            tiposEvidenciaButton.Size = new Size(120, 28);
            tiposEvidenciaButton.TabIndex = 8;
            tiposEvidenciaButton.Text = "Tipos Evidencia";
            tiposEvidenciaButton.UseVisualStyleBackColor = true;
            tiposEvidenciaButton.Click += TiposEvidenciaToolStripMenuItem_Click;
            // 
            // evidenciasButton
            // 
            evidenciasButton.Font = new Font("Segoe UI", 9F);
            evidenciasButton.Location = new Point(285, 190);
            evidenciasButton.Name = "evidenciasButton";
            evidenciasButton.Size = new Size(120, 28);
            evidenciasButton.TabIndex = 9;
            evidenciasButton.Text = "Evidencias";
            evidenciasButton.UseVisualStyleBackColor = true;
            evidenciasButton.Click += EvidenciasToolStripMenuItem_Click;
            // 
            // comentariosButton
            // 
            comentariosButton.Font = new Font("Segoe UI", 9F);
            comentariosButton.Location = new Point(415, 190);
            comentariosButton.Name = "comentariosButton";
            comentariosButton.Size = new Size(120, 28);
            comentariosButton.TabIndex = 10;
            comentariosButton.Text = "Comentarios";
            comentariosButton.UseVisualStyleBackColor = true;
            comentariosButton.Click += ComentariosToolStripMenuItem_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 370);
            Controls.Add(cardPanel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 1, 2, 1);
            MaximizeBox = false;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Windows Forms";
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
        private ToolStripMenuItem habitatsToolStripMenuItem;
        private ToolStripMenuItem tiposEvidenciaToolStripMenuItem;
        private ToolStripMenuItem evidenciasToolStripMenuItem;
        private ToolStripMenuItem comentariosToolStripMenuItem;
        private Panel cardPanel;
        private PictureBox iconPictureBox;
        private Label titleLabel;
        private Label subtitleLabel;
        private Button categoriasButton;
        private Button noticiasButton;
        private Button bestiasButton;
        private Button registrosButton;
        private Button habitatsButton;
        private Button tiposEvidenciaButton;
        private Button evidenciasButton;
        private Button comentariosButton;
    }
}