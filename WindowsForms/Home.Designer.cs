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
            noticiasToolStripMenuItem = new ToolStripMenuItem();
            bestiasToolStripMenuItem = new ToolStripMenuItem();
            reportesToolStripMenuItem = new ToolStripMenuItem();
            estadisticasBestiasToolStripMenuItem = new ToolStripMenuItem();
            registrosPorBestiaToolStripMenuItem = new ToolStripMenuItem();
            cardPanel = new Panel();
            iconPictureBox = new PictureBox();
            titleLabel = new Label();
            subtitleLabel = new Label();
            noticiasButton = new Button();
            bestiasButton = new Button();
            menuStrip1.SuspendLayout();
            cardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { noticiasToolStripMenuItem, bestiasToolStripMenuItem, reportesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(3, 1, 0, 1);
            menuStrip1.Size = new Size(646, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
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
            // reportesToolStripMenuItem
            // 
            reportesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { estadisticasBestiasToolStripMenuItem, registrosPorBestiaToolStripMenuItem });
            reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            reportesToolStripMenuItem.Size = new Size(68, 22);
            reportesToolStripMenuItem.Text = "Reportes";
            // 
            // estadisticasBestiasToolStripMenuItem
            // 
            estadisticasBestiasToolStripMenuItem.Name = "estadisticasBestiasToolStripMenuItem";
            estadisticasBestiasToolStripMenuItem.Size = new Size(235, 22);
            estadisticasBestiasToolStripMenuItem.Text = "Estadísticas de Bestias (Gráfico)";
            estadisticasBestiasToolStripMenuItem.Click += EstadisticasBestiasToolStripMenuItem_Click;
            // 
            // registrosPorBestiaToolStripMenuItem
            // 
            registrosPorBestiaToolStripMenuItem.Name = "registrosPorBestiaToolStripMenuItem";
            registrosPorBestiaToolStripMenuItem.Size = new Size(235, 22);
            registrosPorBestiaToolStripMenuItem.Text = "Registros por Bestia";
            registrosPorBestiaToolStripMenuItem.Click += RegistrosPorBestiaToolStripMenuItem_Click;
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.White;
            cardPanel.BorderStyle = BorderStyle.FixedSingle;
            cardPanel.Controls.Add(iconPictureBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(subtitleLabel);
            cardPanel.Controls.Add(noticiasButton);
            cardPanel.Controls.Add(bestiasButton);
            cardPanel.Location = new Point(43, 50);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(560, 290);
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
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 385);
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
        private ToolStripMenuItem noticiasToolStripMenuItem;
        private ToolStripMenuItem bestiasToolStripMenuItem;
        private ToolStripMenuItem reportesToolStripMenuItem;
        private ToolStripMenuItem estadisticasBestiasToolStripMenuItem;
        private ToolStripMenuItem registrosPorBestiaToolStripMenuItem;
        private Panel cardPanel;
        private PictureBox iconPictureBox;
        private Label titleLabel;
        private Label subtitleLabel;
        private Button noticiasButton;
        private Button bestiasButton;
    }
}