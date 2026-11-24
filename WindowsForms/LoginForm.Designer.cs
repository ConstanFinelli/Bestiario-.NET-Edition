namespace WindowsForms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            loginButton = new Button();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            errorProvider = new ErrorProvider(components);
            cancelButton = new Button();
            registerLinkLabel = new LinkLabel();
            guestLinkLabel = new LinkLabel();
            forgotPasswordLinkLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            loginButton.Location = new Point(252, 176);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(122, 23);
            loginButton.TabIndex = 0;
            loginButton.Text = "Iniciar sesión";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(181, 23);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(109, 15);
            usernameLabel.TabIndex = 1;
            usernameLabel.Text = "Nombre de usuario";
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(199, 83);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Contraseña";
            usernameTextBox.Location = new Point(74, 41);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(300, 23);
            usernameTextBox.TabIndex = 3;
            usernameTextBox.Text = "admin@bestiario.com";
            passwordTextBox.Location = new Point(74, 101);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.Text = "password";
            forgotPasswordLinkLabel.AutoSize = true;
            forgotPasswordLinkLabel.Location = new Point(155, 134);
            forgotPasswordLinkLabel.Name = "forgotPasswordLinkLabel";
            forgotPasswordLinkLabel.Size = new Size(140, 15);
            forgotPasswordLinkLabel.TabIndex = 8;
            forgotPasswordLinkLabel.TabStop = true;
            forgotPasswordLinkLabel.Text = "¿Olvidaste tu contraseña?";
            forgotPasswordLinkLabel.LinkClicked += forgotPasswordLinkLabel_LinkClicked;
            errorProvider.ContainerControl = this;
            cancelButton.Location = new Point(74, 176);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(124, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            registerLinkLabel.AutoSize = true;
            registerLinkLabel.Location = new Point(74, 212);
            registerLinkLabel.Name = "registerLinkLabel";
            registerLinkLabel.Size = new Size(130, 15);
            registerLinkLabel.TabIndex = 6;
            registerLinkLabel.TabStop = true;
            registerLinkLabel.Text = "Registrarse como Lector";
            registerLinkLabel.LinkClicked += registerLinkLabel_LinkClicked;
            guestLinkLabel.AutoSize = true;
            guestLinkLabel.Location = new Point(235, 212);
            guestLinkLabel.Name = "guestLinkLabel";
            guestLinkLabel.Size = new Size(139, 15);
            guestLinkLabel.TabIndex = 7;
            guestLinkLabel.TabStop = true;
            guestLinkLabel.Text = "Continuar como invitado";
            guestLinkLabel.LinkClicked += guestLinkLabel_LinkClicked;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 245);
            Controls.Add(forgotPasswordLinkLabel);
            Controls.Add(guestLinkLabel);
            Controls.Add(registerLinkLabel);
            Controls.Add(cancelButton);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(loginButton);
            Name = "LoginForm";
            Text = "Iniciar sesión";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginButton;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private ErrorProvider errorProvider;
        private Button cancelButton;
        private LinkLabel registerLinkLabel;
        private LinkLabel guestLinkLabel;
        private LinkLabel forgotPasswordLinkLabel;
    }
}