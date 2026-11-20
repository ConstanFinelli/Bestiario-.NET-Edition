namespace WindowsForms
{
    partial class EvidenciaDetalle
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
            bestiaLabel = new Label();
            bestiaComboBox = new ComboBox();
            nroLabel = new Label();
            nroTextBox = new TextBox();
            tipoLabel = new Label();
            tipoComboBox = new ComboBox();
            fileIdLabel = new Label();
            fileIdTextBox = new TextBox();
            fechaLabel = new Label();
            fechaObtencionPicker = new DateTimePicker();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // bestiaLabel
            // 
            bestiaLabel.AutoSize = true;
            bestiaLabel.Location = new Point(20, 18);
            bestiaLabel.Name = "bestiaLabel";
            bestiaLabel.Size = new Size(41, 15);
            bestiaLabel.TabIndex = 0;
            bestiaLabel.Text = "Bestia:";
            // 
            // bestiaComboBox
            // 
            bestiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiaComboBox.FormattingEnabled = true;
            bestiaComboBox.Location = new Point(135, 15);
            bestiaComboBox.Name = "bestiaComboBox";
            bestiaComboBox.Size = new Size(270, 23);
            bestiaComboBox.TabIndex = 1;
            // 
            // nroLabel
            // 
            nroLabel.AutoSize = true;
            nroLabel.Location = new Point(20, 50);
            nroLabel.Name = "nroLabel";
            nroLabel.Size = new Size(85, 15);
            nroLabel.TabIndex = 2;
            nroLabel.Text = "Nro. Evidencia:";
            // 
            // nroTextBox
            // 
            nroTextBox.Location = new Point(135, 47);
            nroTextBox.Name = "nroTextBox";
            nroTextBox.ReadOnly = true;
            nroTextBox.Size = new Size(100, 23);
            nroTextBox.TabIndex = 3;
            nroTextBox.TabStop = false;
            // 
            // tipoLabel
            // 
            tipoLabel.AutoSize = true;
            tipoLabel.Location = new Point(20, 82);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new Size(90, 15);
            tipoLabel.TabIndex = 4;
            tipoLabel.Text = "Tipo Evidencia:";
            // 
            // tipoComboBox
            // 
            tipoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoComboBox.FormattingEnabled = true;
            tipoComboBox.Location = new Point(135, 79);
            tipoComboBox.Name = "tipoComboBox";
            tipoComboBox.Size = new Size(270, 23);
            tipoComboBox.TabIndex = 5;
            // 
            // fileIdLabel
            // 
            fileIdLabel.AutoSize = true;
            fileIdLabel.Location = new Point(20, 114);
            fileIdLabel.Name = "fileIdLabel";
            fileIdLabel.Size = new Size(41, 15);
            fileIdLabel.TabIndex = 6;
            fileIdLabel.Text = "FileId:";
            // 
            // fileIdTextBox
            // 
            fileIdTextBox.Location = new Point(135, 111);
            fileIdTextBox.MaxLength = 200;
            fileIdTextBox.Name = "fileIdTextBox";
            fileIdTextBox.Size = new Size(270, 23);
            fileIdTextBox.TabIndex = 7;
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new Point(20, 146);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new Size(99, 15);
            fechaLabel.TabIndex = 8;
            fechaLabel.Text = "Fecha Obtención:";
            // 
            // fechaObtencionPicker
            // 
            fechaObtencionPicker.Format = DateTimePickerFormat.Short;
            fechaObtencionPicker.Location = new Point(135, 143);
            fechaObtencionPicker.Name = "fechaObtencionPicker";
            fechaObtencionPicker.Size = new Size(160, 23);
            fechaObtencionPicker.TabIndex = 9;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(20, 178);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 10;
            estadoLabel.Text = "Estado:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(135, 175);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(160, 23);
            estadoComboBox.TabIndex = 11;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(240, 220);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(80, 25);
            aceptarButton.TabIndex = 12;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(325, 220);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 25);
            cancelarButton.TabIndex = 13;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // EvidenciaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 262);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(estadoComboBox);
            Controls.Add(estadoLabel);
            Controls.Add(fechaObtencionPicker);
            Controls.Add(fechaLabel);
            Controls.Add(fileIdTextBox);
            Controls.Add(fileIdLabel);
            Controls.Add(tipoComboBox);
            Controls.Add(tipoLabel);
            Controls.Add(nroTextBox);
            Controls.Add(nroLabel);
            Controls.Add(bestiaComboBox);
            Controls.Add(bestiaLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EvidenciaDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Evidencia";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label bestiaLabel;
        private ComboBox bestiaComboBox;
        private Label nroLabel;
        private TextBox nroTextBox;
        private Label tipoLabel;
        private ComboBox tipoComboBox;
        private Label fileIdLabel;
        private TextBox fileIdTextBox;
        private Label fechaLabel;
        private DateTimePicker fechaObtencionPicker;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
