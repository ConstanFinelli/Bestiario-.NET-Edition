namespace WindowsForms
{
    partial class LoginForm
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
            components = new System.ComponentModel.Container();
            headerPanel = new Panel();
            headerLabel = new Label();
            loginButton = new Button();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            errorProvider = new ErrorProvider(components);
            cancelButton = new Button();
            registerLectorLinkLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(142, 110, 83);
            headerPanel.Controls.Add(headerLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(450, 44);
            headerPanel.TabIndex = 7;
            // 
            // headerLabel
            // 
            headerLabel.Dock = DockStyle.Fill;
            headerLabel.Font = new Font("Georgia", 11.25F, FontStyle.Bold);
            headerLabel.ForeColor = Color.White;
            headerLabel.Location = new Point(0, 0);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(450, 44);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "📜 BESTIARIO - INICIAR SESIÓN";
            headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.FromArgb(217, 189, 98);
            loginButton.FlatAppearance.BorderColor = Color.FromArgb(201, 150, 12);
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            loginButton.ForeColor = Color.FromArgb(44, 62, 80);
            loginButton.Location = new Point(244, 180);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(130, 32);
            loginButton.TabIndex = 4;
            loginButton.Text = "Iniciar sesión";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            usernameLabel.ForeColor = Color.FromArgb(44, 62, 80);
            usernameLabel.Location = new Point(74, 58);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(177, 15);
            usernameLabel.TabIndex = 0;
            usernameLabel.Text = "✉️ Correo electrónico o Usuario";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            passwordLabel.ForeColor = Color.FromArgb(44, 62, 80);
            passwordLabel.Location = new Point(74, 116);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(88, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "🔐 Contraseña";
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.White;
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            usernameTextBox.Location = new Point(74, 78);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(300, 23);
            usernameTextBox.TabIndex = 1;
            usernameTextBox.Text = "admin";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = Color.White;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            passwordTextBox.Location = new Point(74, 136);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 3;
            passwordTextBox.Text = "password";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = Color.FromArgb(142, 110, 83);
            cancelButton.FlatAppearance.BorderColor = Color.FromArgb(110, 83, 61);
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Location = new Point(74, 180);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(130, 32);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += cancelButton_Click;
            // 
            // registerLectorLinkLabel
            // 
            registerLectorLinkLabel.ActiveLinkColor = Color.FromArgb(201, 150, 12);
            registerLectorLinkLabel.AutoSize = true;
            registerLectorLinkLabel.Font = new Font("Segoe UI", 9F);
            registerLectorLinkLabel.LinkColor = Color.FromArgb(142, 110, 83);
            registerLectorLinkLabel.Location = new Point(105, 226);
            registerLectorLinkLabel.Name = "registerLectorLinkLabel";
            registerLectorLinkLabel.Size = new Size(240, 15);
            registerLectorLinkLabel.TabIndex = 6;
            registerLectorLinkLabel.TabStop = true;
            registerLectorLinkLabel.Text = "¿No tienes cuenta? Registrarse como Lector";
            registerLectorLinkLabel.LinkClicked += registerLectorLinkLabel_LinkClicked;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 241, 234);
            ClientSize = new Size(450, 260);
            Controls.Add(headerPanel);
            Controls.Add(registerLectorLinkLabel);
            Controls.Add(cancelButton);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(loginButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestiario - Iniciar Sesión";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel headerPanel;
        private Label headerLabel;
        private Button loginButton;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private ErrorProvider errorProvider;
        private Button cancelButton;
        private LinkLabel registerLectorLinkLabel;
    }
}