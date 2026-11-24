namespace WindowsForms
{
    partial class UsuarioDetalle
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
            idLabel = new Label();
            idTextBox = new TextBox();
            tipoLabel = new Label();
            tipoComboBox = new ComboBox();
            correoLabel = new Label();
            correoTextBox = new TextBox();
            contraseniaLabel = new Label();
            contraseniaTextBox = new TextBox();
            dniLabel = new Label();
            dniTextBox = new TextBox();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            apellidoLabel = new Label();
            apellidoTextBox = new TextBox();
            notificacionesCheckBox = new CheckBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            idLabel.AutoSize = true;
            idLabel.Location = new Point(20, 18);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(20, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            idTextBox.Location = new Point(135, 15);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(270, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            tipoLabel.AutoSize = true;
            tipoLabel.Location = new Point(20, 50);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new Size(80, 15);
            tipoLabel.TabIndex = 2;
            tipoLabel.Text = "Tipo Usuario:";
            tipoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoComboBox.FormattingEnabled = true;
            tipoComboBox.Location = new Point(135, 47);
            tipoComboBox.Name = "tipoComboBox";
            tipoComboBox.Size = new Size(180, 23);
            tipoComboBox.TabIndex = 3;
            tipoComboBox.SelectedIndexChanged += tipoComboBox_SelectedIndexChanged;
            correoLabel.AutoSize = true;
            correoLabel.Location = new Point(20, 82);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(46, 15);
            correoLabel.TabIndex = 4;
            correoLabel.Text = "Correo:";
            correoTextBox.Location = new Point(135, 79);
            correoTextBox.MaxLength = 100;
            correoTextBox.Name = "correoTextBox";
            correoTextBox.Size = new Size(270, 23);
            correoTextBox.TabIndex = 5;
            contraseniaLabel.AutoSize = true;
            contraseniaLabel.Location = new Point(20, 114);
            contraseniaLabel.Name = "contraseniaLabel";
            contraseniaLabel.Size = new Size(70, 15);
            contraseniaLabel.TabIndex = 6;
            contraseniaLabel.Text = "Contraseña:";
            contraseniaTextBox.Location = new Point(135, 111);
            contraseniaTextBox.MaxLength = 100;
            contraseniaTextBox.Name = "contraseniaTextBox";
            contraseniaTextBox.PasswordChar = '*';
            contraseniaTextBox.Size = new Size(270, 23);
            contraseniaTextBox.TabIndex = 7;
            dniLabel.AutoSize = true;
            dniLabel.Location = new Point(20, 146);
            dniLabel.Name = "dniLabel";
            dniLabel.Size = new Size(30, 15);
            dniLabel.TabIndex = 8;
            dniLabel.Text = "DNI:";
            dniTextBox.Location = new Point(135, 143);
            dniTextBox.MaxLength = 20;
            dniTextBox.Name = "dniTextBox";
            dniTextBox.Size = new Size(180, 23);
            dniTextBox.TabIndex = 9;
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(20, 178);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(54, 15);
            nombreLabel.TabIndex = 10;
            nombreLabel.Text = "Nombre:";
            nombreTextBox.Location = new Point(135, 175);
            nombreTextBox.MaxLength = 50;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(270, 23);
            nombreTextBox.TabIndex = 11;
            apellidoLabel.AutoSize = true;
            apellidoLabel.Location = new Point(20, 210);
            apellidoLabel.Name = "apellidoLabel";
            apellidoLabel.Size = new Size(54, 15);
            apellidoLabel.TabIndex = 12;
            apellidoLabel.Text = "Apellido:";
            apellidoTextBox.Location = new Point(135, 207);
            apellidoTextBox.MaxLength = 50;
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(270, 23);
            apellidoTextBox.TabIndex = 13;
            notificacionesCheckBox.AutoSize = true;
            notificacionesCheckBox.Location = new Point(135, 240);
            notificacionesCheckBox.Name = "notificacionesCheckBox";
            notificacionesCheckBox.Size = new Size(145, 19);
            notificacionesCheckBox.TabIndex = 14;
            notificacionesCheckBox.Text = "Recibir notificaciones";
            notificacionesCheckBox.UseVisualStyleBackColor = true;
            aceptarButton.Location = new Point(240, 275);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(80, 25);
            aceptarButton.TabIndex = 15;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            cancelarButton.Location = new Point(325, 275);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 25);
            cancelarButton.TabIndex = 16;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            errorProvider.ContainerControl = this;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 315);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(notificacionesCheckBox);
            Controls.Add(apellidoTextBox);
            Controls.Add(apellidoLabel);
            Controls.Add(nombreTextBox);
            Controls.Add(nombreLabel);
            Controls.Add(dniTextBox);
            Controls.Add(dniLabel);
            Controls.Add(contraseniaTextBox);
            Controls.Add(contraseniaLabel);
            Controls.Add(correoTextBox);
            Controls.Add(correoLabel);
            Controls.Add(tipoComboBox);
            Controls.Add(tipoLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UsuarioDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Usuario";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label tipoLabel;
        private ComboBox tipoComboBox;
        private Label correoLabel;
        private TextBox correoTextBox;
        private Label contraseniaLabel;
        private TextBox contraseniaTextBox;
        private Label dniLabel;
        private TextBox dniTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label apellidoLabel;
        private TextBox apellidoTextBox;
        private CheckBox notificacionesCheckBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}

