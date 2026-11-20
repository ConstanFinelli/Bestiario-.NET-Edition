namespace WindowsForms
{
    partial class TipoEvidenciaDetalle
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
            descripcionLabel = new Label();
            descripcionTextBox = new TextBox();
            resourceTypeLabel = new Label();
            resourceTypeComboBox = new ComboBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(20, 18);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(20, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(125, 15);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(260, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(20, 50);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(72, 15);
            descripcionLabel.TabIndex = 2;
            descripcionLabel.Text = "Descripción:";
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(125, 47);
            descripcionTextBox.MaxLength = 80;
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(260, 23);
            descripcionTextBox.TabIndex = 3;
            // 
            // resourceTypeLabel
            // 
            resourceTypeLabel.AutoSize = true;
            resourceTypeLabel.Location = new Point(20, 82);
            resourceTypeLabel.Name = "resourceTypeLabel";
            resourceTypeLabel.Size = new Size(84, 15);
            resourceTypeLabel.TabIndex = 4;
            resourceTypeLabel.Text = "ResourceType:";
            // 
            // resourceTypeComboBox
            // 
            resourceTypeComboBox.FormattingEnabled = true;
            resourceTypeComboBox.Items.AddRange(new object[] { "image", "audio", "video", "document" });
            resourceTypeComboBox.Location = new Point(125, 79);
            resourceTypeComboBox.Name = "resourceTypeComboBox";
            resourceTypeComboBox.Size = new Size(260, 23);
            resourceTypeComboBox.TabIndex = 5;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(220, 125);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(80, 25);
            aceptarButton.TabIndex = 6;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(305, 125);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 25);
            cancelarButton.TabIndex = 7;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // TipoEvidenciaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 168);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(resourceTypeComboBox);
            Controls.Add(resourceTypeLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TipoEvidenciaDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tipo de Evidencia";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label descripcionLabel;
        private TextBox descripcionTextBox;
        private Label resourceTypeLabel;
        private ComboBox resourceTypeComboBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}

