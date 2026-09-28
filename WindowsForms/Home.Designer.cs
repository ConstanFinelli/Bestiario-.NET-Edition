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
            this.navbarPanel = new Panel();
            this.brandLabel = new Label();
            this.navBestiasBtn = new Button();
            this.navMapaBtn = new Button();
            this.navNoticiasBtn = new Button();
            this.navRegistrosBtn = new Button();
            this.navCategoriasBtn = new Button();
            this.navCandidaturaBtn = new Button();
            this.navAdminBtn = new Button();
            this.userStatusLabel = new Label();
            this.authButton = new Button();
            this.footerPanel = new Panel();
            this.footerLabel = new Label();
            this.mainContainerPanel = new Panel();
            this.newsSidebarPanel = new Panel();
            this.newsHeaderLabel = new Label();
            this.newsFlowPanel = new FlowLayoutPanel();
            this.aboutUsPanel = new Panel();
            this.aboutContentPanel = new Panel();
            this.lblAboutTitle1 = new Label();
            this.lblAboutText1 = new Label();
            this.lblAboutTitle2 = new Label();
            this.lblAboutText2 = new Label();
            this.navbarPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.mainContainerPanel.SuspendLayout();
            this.newsSidebarPanel.SuspendLayout();
            this.aboutUsPanel.SuspendLayout();
            this.aboutContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // navbarPanel
            // 
            this.navbarPanel.BackColor = Color.FromArgb(142, 110, 83);
            this.navbarPanel.Controls.Add(this.authButton);
            this.navbarPanel.Controls.Add(this.userStatusLabel);
            this.navbarPanel.Controls.Add(this.navAdminBtn);
            this.navbarPanel.Controls.Add(this.navCandidaturaBtn);
            this.navbarPanel.Controls.Add(this.navCategoriasBtn);
            this.navbarPanel.Controls.Add(this.navRegistrosBtn);
            this.navbarPanel.Controls.Add(this.navNoticiasBtn);
            this.navbarPanel.Controls.Add(this.navMapaBtn);
            this.navbarPanel.Controls.Add(this.navBestiasBtn);
            this.navbarPanel.Controls.Add(this.brandLabel);
            this.navbarPanel.Dock = DockStyle.Top;
            this.navbarPanel.Height = 56;
            this.navbarPanel.Location = new Point(0, 0);
            this.navbarPanel.Name = "navbarPanel";
            this.navbarPanel.Size = new Size(1150, 56);
            this.navbarPanel.TabIndex = 0;
            // 
            // brandLabel
            // 
            this.brandLabel.AutoSize = true;
            this.brandLabel.Cursor = Cursors.Hand;
            this.brandLabel.Font = new Font("Georgia", 16F, FontStyle.Bold);
            this.brandLabel.ForeColor = Color.White;
            this.brandLabel.Location = new Point(16, 14);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Size = new Size(150, 27);
            this.brandLabel.TabIndex = 0;
            this.brandLabel.Text = "🐉 Bestiario";
            this.brandLabel.Click += (s, e) => this.RefreshNews();
            // 
            // navBestiasBtn
            // 
            this.navBestiasBtn.Cursor = Cursors.Hand;
            this.navBestiasBtn.FlatAppearance.BorderSize = 0;
            this.navBestiasBtn.FlatStyle = FlatStyle.Flat;
            this.navBestiasBtn.Font = new Font("Segoe UI", 10.5F);
            this.navBestiasBtn.ForeColor = Color.AliceBlue;
            this.navBestiasBtn.Location = new Point(175, 12);
            this.navBestiasBtn.Name = "navBestiasBtn";
            this.navBestiasBtn.Size = new Size(75, 32);
            this.navBestiasBtn.TabIndex = 1;
            this.navBestiasBtn.Text = "Bestias";
            this.navBestiasBtn.UseVisualStyleBackColor = true;
            this.navBestiasBtn.Click += (s, e) => new BestiaLista().ShowDialog();
            // 
            // navMapaBtn
            // 
            this.navMapaBtn.Cursor = Cursors.Hand;
            this.navMapaBtn.FlatAppearance.BorderSize = 0;
            this.navMapaBtn.FlatStyle = FlatStyle.Flat;
            this.navMapaBtn.Font = new Font("Segoe UI", 10.5F);
            this.navMapaBtn.ForeColor = Color.AliceBlue;
            this.navMapaBtn.Location = new Point(255, 12);
            this.navMapaBtn.Name = "navMapaBtn";
            this.navMapaBtn.Size = new Size(65, 32);
            this.navMapaBtn.TabIndex = 2;
            this.navMapaBtn.Text = "Mapa";
            this.navMapaBtn.UseVisualStyleBackColor = true;
            this.navMapaBtn.Click += (s, e) => new MapaForm().ShowDialog();
            // 
            // navNoticiasBtn
            // 
            this.navNoticiasBtn.Cursor = Cursors.Hand;
            this.navNoticiasBtn.FlatAppearance.BorderSize = 0;
            this.navNoticiasBtn.FlatStyle = FlatStyle.Flat;
            this.navNoticiasBtn.Font = new Font("Segoe UI", 10.5F);
            this.navNoticiasBtn.ForeColor = Color.AliceBlue;
            this.navNoticiasBtn.Location = new Point(325, 12);
            this.navNoticiasBtn.Name = "navNoticiasBtn";
            this.navNoticiasBtn.Size = new Size(80, 32);
            this.navNoticiasBtn.TabIndex = 3;
            this.navNoticiasBtn.Text = "Noticias";
            this.navNoticiasBtn.UseVisualStyleBackColor = true;
            this.navNoticiasBtn.Click += (s, e) => new NoticiaLista().ShowDialog();
            // 
            // navRegistrosBtn
            // 
            this.navRegistrosBtn.Cursor = Cursors.Hand;
            this.navRegistrosBtn.FlatAppearance.BorderSize = 0;
            this.navRegistrosBtn.FlatStyle = FlatStyle.Flat;
            this.navRegistrosBtn.Font = new Font("Segoe UI", 10.5F);
            this.navRegistrosBtn.ForeColor = Color.AliceBlue;
            this.navRegistrosBtn.Location = new Point(410, 12);
            this.navRegistrosBtn.Name = "navRegistrosBtn";
            this.navRegistrosBtn.Size = new Size(85, 32);
            this.navRegistrosBtn.TabIndex = 4;
            this.navRegistrosBtn.Text = "Registros";
            this.navRegistrosBtn.UseVisualStyleBackColor = true;
            this.navRegistrosBtn.Click += (s, e) => new RegistroLista().ShowDialog();
            // 
            // navCategoriasBtn
            // 
            this.navCategoriasBtn.Cursor = Cursors.Hand;
            this.navCategoriasBtn.FlatAppearance.BorderSize = 0;
            this.navCategoriasBtn.FlatStyle = FlatStyle.Flat;
            this.navCategoriasBtn.Font = new Font("Segoe UI", 10.5F);
            this.navCategoriasBtn.ForeColor = Color.AliceBlue;
            this.navCategoriasBtn.Location = new Point(500, 12);
            this.navCategoriasBtn.Name = "navCategoriasBtn";
            this.navCategoriasBtn.Size = new Size(95, 32);
            this.navCategoriasBtn.TabIndex = 5;
            this.navCategoriasBtn.Text = "Categorías";
            this.navCategoriasBtn.UseVisualStyleBackColor = true;
            this.navCategoriasBtn.Click += (s, e) => new CategoriaLista().ShowDialog();
            // 
            // navCandidaturaBtn
            // 
            this.navCandidaturaBtn.Cursor = Cursors.Hand;
            this.navCandidaturaBtn.FlatAppearance.BorderSize = 0;
            this.navCandidaturaBtn.FlatStyle = FlatStyle.Flat;
            this.navCandidaturaBtn.Font = new Font("Segoe UI", 10F);
            this.navCandidaturaBtn.ForeColor = Color.AliceBlue;
            this.navCandidaturaBtn.Location = new Point(600, 12);
            this.navCandidaturaBtn.Name = "navCandidaturaBtn";
            this.navCandidaturaBtn.Size = new Size(160, 32);
            this.navCandidaturaBtn.TabIndex = 6;
            this.navCandidaturaBtn.Text = "Volverse Investigador";
            this.navCandidaturaBtn.UseVisualStyleBackColor = true;
            this.navCandidaturaBtn.Visible = false;
            this.navCandidaturaBtn.Click += (s, e) => new CandidaturaForm().ShowDialog();
            // 
            // navAdminBtn
            // 
            this.navAdminBtn.Cursor = Cursors.Hand;
            this.navAdminBtn.FlatAppearance.BorderSize = 0;
            this.navAdminBtn.FlatStyle = FlatStyle.Flat;
            this.navAdminBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.navAdminBtn.ForeColor = Color.FromArgb(255, 235, 150);
            this.navAdminBtn.Location = new Point(600, 12);
            this.navAdminBtn.Name = "navAdminBtn";
            this.navAdminBtn.Size = new Size(80, 32);
            this.navAdminBtn.TabIndex = 7;
            this.navAdminBtn.Text = "🛡️ Admin";
            this.navAdminBtn.UseVisualStyleBackColor = true;
            this.navAdminBtn.Visible = false;
            this.navAdminBtn.Click += (s, e) => new AdminDashboardForm().ShowDialog();
            // 
            // userStatusLabel
            // 
            this.userStatusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.userStatusLabel.Font = new Font("Segoe UI", 9.5F);
            this.userStatusLabel.ForeColor = Color.WhiteSmoke;
            this.userStatusLabel.Location = new Point(780, 16);
            this.userStatusLabel.Name = "userStatusLabel";
            this.userStatusLabel.Size = new Size(220, 24);
            this.userStatusLabel.TabIndex = 8;
            this.userStatusLabel.Text = "usuario@bestiario.com";
            this.userStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            this.userStatusLabel.Visible = false;
            // 
            // authButton
            // 
            this.authButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.authButton.BackColor = Color.FromArgb(217, 189, 98);
            this.authButton.Cursor = Cursors.Hand;
            this.authButton.FlatAppearance.BorderSize = 0;
            this.authButton.FlatStyle = FlatStyle.Flat;
            this.authButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.authButton.ForeColor = Color.FromArgb(44, 62, 80);
            this.authButton.Location = new Point(1010, 12);
            this.authButton.Name = "authButton";
            this.authButton.Size = new Size(125, 32);
            this.authButton.TabIndex = 9;
            this.authButton.Text = "Iniciar sesión";
            this.authButton.UseVisualStyleBackColor = false;
            this.authButton.Click += new EventHandler(this.AuthButton_Click);
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = Color.FromArgb(142, 110, 83);
            this.footerPanel.Controls.Add(this.footerLabel);
            this.footerPanel.Dock = DockStyle.Bottom;
            this.footerPanel.Height = 32;
            this.footerPanel.Location = new Point(0, 688);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new Size(1150, 32);
            this.footerPanel.TabIndex = 1;
            // 
            // footerLabel
            // 
            this.footerLabel.Dock = DockStyle.Fill;
            this.footerLabel.Font = new Font("Segoe UI", 9F);
            this.footerLabel.ForeColor = Color.White;
            this.footerLabel.Location = new Point(0, 0);
            this.footerLabel.Name = "footerLabel";
            this.footerLabel.Size = new Size(1150, 32);
            this.footerLabel.TabIndex = 0;
            this.footerLabel.Text = "Bestiario © 2026 - Recopilación y Registro de Criaturas Enigmáticas";
            this.footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mainContainerPanel
            // 
            this.mainContainerPanel.Controls.Add(this.aboutUsPanel);
            this.mainContainerPanel.Controls.Add(this.newsSidebarPanel);
            this.mainContainerPanel.Dock = DockStyle.Fill;
            this.mainContainerPanel.Location = new Point(0, 56);
            this.mainContainerPanel.Name = "mainContainerPanel";
            this.mainContainerPanel.Size = new Size(1150, 632);
            this.mainContainerPanel.TabIndex = 2;
            // 
            // newsSidebarPanel
            // 
            this.newsSidebarPanel.BackColor = Color.FromArgb(203, 187, 163);
            this.newsSidebarPanel.Controls.Add(this.newsFlowPanel);
            this.newsSidebarPanel.Controls.Add(this.newsHeaderLabel);
            this.newsSidebarPanel.Dock = DockStyle.Right;
            this.newsSidebarPanel.Location = new Point(760, 0);
            this.newsSidebarPanel.Name = "newsSidebarPanel";
            this.newsSidebarPanel.Padding = new Padding(15);
            this.newsSidebarPanel.Size = new Size(390, 632);
            this.newsSidebarPanel.TabIndex = 1;
            // 
            // newsHeaderLabel
            // 
            this.newsHeaderLabel.Dock = DockStyle.Top;
            this.newsHeaderLabel.Font = new Font("Georgia", 14F, FontStyle.Bold);
            this.newsHeaderLabel.ForeColor = Color.FromArgb(44, 62, 80);
            this.newsHeaderLabel.Location = new Point(15, 15);
            this.newsHeaderLabel.Name = "newsHeaderLabel";
            this.newsHeaderLabel.Size = new Size(360, 38);
            this.newsHeaderLabel.TabIndex = 0;
            this.newsHeaderLabel.Text = "Últimas noticias";
            this.newsHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // newsFlowPanel
            // 
            this.newsFlowPanel.AutoScroll = true;
            this.newsFlowPanel.Dock = DockStyle.Fill;
            this.newsFlowPanel.FlowDirection = FlowDirection.TopDown;
            this.newsFlowPanel.Location = new Point(15, 53);
            this.newsFlowPanel.Name = "newsFlowPanel";
            this.newsFlowPanel.Size = new Size(360, 564);
            this.newsFlowPanel.TabIndex = 1;
            this.newsFlowPanel.WrapContents = false;
            // 
            // aboutUsPanel
            // 
            this.aboutUsPanel.Controls.Add(this.aboutContentPanel);
            this.aboutUsPanel.Dock = DockStyle.Fill;
            this.aboutUsPanel.Location = new Point(0, 0);
            this.aboutUsPanel.Name = "aboutUsPanel";
            this.aboutUsPanel.Padding = new Padding(40);
            this.aboutUsPanel.Size = new Size(760, 632);
            this.aboutUsPanel.TabIndex = 0;
            this.aboutUsPanel.Paint += new PaintEventHandler(this.AboutUsPanel_Paint);
            // 
            // aboutContentPanel
            // 
            this.aboutContentPanel.AutoScroll = true;
            this.aboutContentPanel.BackColor = Color.FromArgb(180, 25, 20, 15);
            this.aboutContentPanel.Controls.Add(this.lblAboutText2);
            this.aboutContentPanel.Controls.Add(this.lblAboutTitle2);
            this.aboutContentPanel.Controls.Add(this.lblAboutText1);
            this.aboutContentPanel.Controls.Add(this.lblAboutTitle1);
            this.aboutContentPanel.Dock = DockStyle.Fill;
            this.aboutContentPanel.Location = new Point(40, 40);
            this.aboutContentPanel.Name = "aboutContentPanel";
            this.aboutContentPanel.Padding = new Padding(30);
            this.aboutContentPanel.Size = new Size(680, 552);
            this.aboutContentPanel.TabIndex = 0;
            // 
            // lblAboutTitle1
            // 
            this.lblAboutTitle1.AutoSize = true;
            this.lblAboutTitle1.Font = new Font("Georgia", 16F, FontStyle.Bold);
            this.lblAboutTitle1.ForeColor = Color.Wheat;
            this.lblAboutTitle1.Location = new Point(25, 25);
            this.lblAboutTitle1.Name = "lblAboutTitle1";
            this.lblAboutTitle1.Size = new Size(205, 27);
            this.lblAboutTitle1.TabIndex = 0;
            this.lblAboutTitle1.Text = "¿Quiénes somos?";
            // 
            // lblAboutText1
            // 
            this.lblAboutText1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblAboutText1.Font = new Font("Segoe UI", 11F);
            this.lblAboutText1.ForeColor = Color.WhiteSmoke;
            this.lblAboutText1.Location = new Point(25, 60);
            this.lblAboutText1.Name = "lblAboutText1";
            this.lblAboutText1.Size = new Size(620, 110);
            this.lblAboutText1.TabIndex = 1;
            this.lblAboutText1.Text = "En Bestiario nos dedicamos a recopilar, analizar y compartir información sobre avistamientos de criaturas enigmáticas alrededor del mundo. Desde leyendas ancestrales hasta testimonios recientes, nuestro objetivo es construir un registro accesible y confiable que acerque a la comunidad a estos fenómenos misteriosos.";
            // 
            // lblAboutTitle2
            // 
            this.lblAboutTitle2.AutoSize = true;
            this.lblAboutTitle2.Font = new Font("Georgia", 16F, FontStyle.Bold);
            this.lblAboutTitle2.ForeColor = Color.Wheat;
            this.lblAboutTitle2.Location = new Point(25, 185);
            this.lblAboutTitle2.Name = "lblAboutTitle2";
            this.lblAboutTitle2.Size = new Size(345, 27);
            this.lblAboutTitle2.TabIndex = 2;
            this.lblAboutTitle2.Text = "¿Que encontrarás en este sitio?";
            // 
            // lblAboutText2
            // 
            this.lblAboutText2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblAboutText2.Font = new Font("Segoe UI", 11F);
            this.lblAboutText2.ForeColor = Color.WhiteSmoke;
            this.lblAboutText2.Location = new Point(25, 220);
            this.lblAboutText2.Name = "lblAboutText2";
            this.lblAboutText2.Size = new Size(620, 130);
            this.lblAboutText2.TabIndex = 3;
            this.lblAboutText2.Text = "Aquí encontrarás reportes de avistamientos, descripciones detalladas, mapas interactivos y artículos de investigación que buscan dar contexto cultural, histórico y científico a cada caso. Creemos que cada relato, ya sea una historia transmitida por generaciones o una experiencia vivida en primera persona, aporta una pieza valiosa al gran rompecabezas de lo desconocido.";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1150, 720);
            this.Controls.Add(this.mainContainerPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.navbarPanel);
            this.MinimumSize = new Size(950, 620);
            this.Name = "Home";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Bestiario - Inicio";
            this.Load += new EventHandler(this.Home_Load);
            this.navbarPanel.ResumeLayout(false);
            this.navbarPanel.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.mainContainerPanel.ResumeLayout(false);
            this.newsSidebarPanel.ResumeLayout(false);
            this.aboutUsPanel.ResumeLayout(false);
            this.aboutContentPanel.ResumeLayout(false);
            this.aboutContentPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel navbarPanel;
        private Label brandLabel;
        private Button navBestiasBtn;
        private Button navMapaBtn;
        private Button navNoticiasBtn;
        private Button navRegistrosBtn;
        private Button navCategoriasBtn;
        private Button navCandidaturaBtn;
        private Button navAdminBtn;
        private Label userStatusLabel;
        private Button authButton;
        private Panel footerPanel;
        private Label footerLabel;
        private Panel mainContainerPanel;
        private Panel newsSidebarPanel;
        private Label newsHeaderLabel;
        private FlowLayoutPanel newsFlowPanel;
        private Panel aboutUsPanel;
        private Panel aboutContentPanel;
        private Label lblAboutTitle1;
        private Label lblAboutText1;
        private Label lblAboutTitle2;
        private Label lblAboutText2;
    }
}