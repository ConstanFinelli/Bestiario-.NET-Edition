namespace WindowsForms
{
    partial class RegistroLectorForm
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
            emailLabel = new Label();
            emailTextBox = new TextBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            confirmPasswordLabel = new Label();
            confirmPasswordTextBox = new TextBox();
            notificacionesCheckBox = new CheckBox();
            cancelButton = new Button();
            registerButton = new Button();
            errorProvider = new ErrorProvider(components);
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
            headerPanel.TabIndex = 9;
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
            headerLabel.Text = "📜 BESTIARIO - REGISTRO DE LECTOR";
            headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            emailLabel.ForeColor = Color.FromArgb(44, 62, 80);
            emailLabel.Location = new Point(74, 55);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(123, 15);
            emailLabel.TabIndex = 0;
            emailLabel.Text = "✉️ Correo electrónico";
            // 
            // emailTextBox
            // 
            emailTextBox.BackColor = Color.White;
            emailTextBox.BorderStyle = BorderStyle.FixedSingle;
            emailTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            emailTextBox.Location = new Point(74, 73);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.PlaceholderText = "ejemplo@correo.com";
            emailTextBox.Size = new Size(300, 23);
            emailTextBox.TabIndex = 1;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            passwordLabel.ForeColor = Color.FromArgb(44, 62, 80);
            passwordLabel.Location = new Point(74, 107);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(88, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "🔐 Contraseña";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = Color.White;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            passwordTextBox.Location = new Point(74, 125);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 3;
            // 
            // confirmPasswordLabel
            // 
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            confirmPasswordLabel.ForeColor = Color.FromArgb(44, 62, 80);
            confirmPasswordLabel.Location = new Point(74, 159);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Size = new Size(146, 15);
            confirmPasswordLabel.TabIndex = 4;
            confirmPasswordLabel.Text = "🔐 Confirmar contraseña";
            // 
            // confirmPasswordTextBox
            // 
            confirmPasswordTextBox.BackColor = Color.White;
            confirmPasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            confirmPasswordTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            confirmPasswordTextBox.Location = new Point(74, 177);
            confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            confirmPasswordTextBox.PasswordChar = '*';
            confirmPasswordTextBox.Size = new Size(300, 23);
            confirmPasswordTextBox.TabIndex = 5;
            // 
            // notificacionesCheckBox
            // 
            notificacionesCheckBox.AutoSize = true;
            notificacionesCheckBox.Checked = true;
            notificacionesCheckBox.CheckState = CheckState.Checked;
            notificacionesCheckBox.Font = new Font("Segoe UI", 9F);
            notificacionesCheckBox.ForeColor = Color.FromArgb(44, 62, 80);
            notificacionesCheckBox.Location = new Point(74, 212);
            notificacionesCheckBox.Name = "notificacionesCheckBox";
            notificacionesCheckBox.Size = new Size(207, 19);
            notificacionesCheckBox.TabIndex = 6;
            notificacionesCheckBox.Text = "Recibir notificaciones del Bestiario";
            notificacionesCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = Color.FromArgb(142, 110, 83);
            cancelButton.FlatAppearance.BorderColor = Color.FromArgb(110, 83, 61);
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Location = new Point(74, 246);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(130, 32);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += cancelButton_Click;
            // 
            // registerButton
            // 
            registerButton.BackColor = Color.FromArgb(217, 189, 98);
            registerButton.FlatAppearance.BorderColor = Color.FromArgb(201, 150, 12);
            registerButton.FlatStyle = FlatStyle.Flat;
            registerButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            registerButton.ForeColor = Color.FromArgb(44, 62, 80);
            registerButton.Location = new Point(244, 246);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(130, 32);
            registerButton.TabIndex = 8;
            registerButton.Text = "Crear y Entrar";
            registerButton.UseVisualStyleBackColor = false;
            registerButton.Click += registerButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // RegistroLectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 241, 234);
            ClientSize = new Size(450, 298);
            Controls.Add(headerPanel);
            Controls.Add(registerButton);
            Controls.Add(cancelButton);
            Controls.Add(notificacionesCheckBox);
            Controls.Add(confirmPasswordTextBox);
            Controls.Add(confirmPasswordLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(emailTextBox);
            Controls.Add(emailLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegistroLectorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registro de Lector";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel headerPanel;
        private Label headerLabel;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Label confirmPasswordLabel;
        private TextBox confirmPasswordTextBox;
        private CheckBox notificacionesCheckBox;
        private Button cancelButton;
        private Button registerButton;
        private ErrorProvider errorProvider;
    }
}
