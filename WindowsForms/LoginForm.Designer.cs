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
            loginButton = new Button();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            errorProvider = new ErrorProvider(components);
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // loginButton
            // 
            loginButton.Location = new Point(252, 154);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(122, 23);
            loginButton.TabIndex = 0;
            loginButton.Text = "Iniciar sesión";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(181, 23);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(109, 15);
            usernameLabel.TabIndex = 1;
            usernameLabel.Text = "Nombre de usuario";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(199, 83);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Contraseña";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(74, 41);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(300, 23);
            usernameTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(74, 101);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 4;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(74, 154);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(124, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 189);
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
    }
}