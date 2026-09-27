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
            SuspendLayout();
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(171, 18);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(105, 15);
            emailLabel.TabIndex = 0;
            emailLabel.Text = "Correo electrónico";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(74, 36);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.PlaceholderText = "ejemplo@correo.com";
            emailTextBox.Size = new Size(300, 23);
            emailTextBox.TabIndex = 1;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(190, 72);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Contraseña";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(74, 90);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 3;
            // 
            // confirmPasswordLabel
            // 
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Location = new Point(164, 126);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Size = new Size(122, 15);
            confirmPasswordLabel.TabIndex = 4;
            confirmPasswordLabel.Text = "Confirmar contraseña";
            // 
            // confirmPasswordTextBox
            // 
            confirmPasswordTextBox.Location = new Point(74, 144);
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
            notificacionesCheckBox.Location = new Point(115, 182);
            notificacionesCheckBox.Name = "notificacionesCheckBox";
            notificacionesCheckBox.Size = new Size(223, 19);
            notificacionesCheckBox.TabIndex = 6;
            notificacionesCheckBox.Text = "Recibir notificaciones del Bestiario";
            notificacionesCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(74, 218);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(124, 26);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancelar";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(244, 218);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(130, 26);
            registerButton.TabIndex = 8;
            registerButton.Text = "Crear y Entrar";
            registerButton.UseVisualStyleBackColor = true;
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
            ClientSize = new Size(450, 265);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
