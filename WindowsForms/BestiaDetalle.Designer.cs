namespace WindowsForms
{
    partial class BestiaDetalle
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
            idLabel = new Label();
            idTextBox = new TextBox();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            peligrosidadLabel = new Label();
            peligrosidadComboBox = new ComboBox();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
            categoriasLabel = new Label();
            categoriasCheckedListBox = new CheckedListBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(24, 18);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(20, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(120, 15);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(260, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(24, 50);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(54, 15);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre:";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(120, 47);
            nombreTextBox.MaxLength = 100;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(260, 23);
            nombreTextBox.TabIndex = 3;
            // 
            // peligrosidadLabel
            // 
            peligrosidadLabel.AutoSize = true;
            peligrosidadLabel.Location = new Point(24, 82);
            peligrosidadLabel.Name = "peligrosidadLabel";
            peligrosidadLabel.Size = new Size(75, 15);
            peligrosidadLabel.TabIndex = 4;
            peligrosidadLabel.Text = "Peligrosidad:";
            // 
            // peligrosidadComboBox
            // 
            peligrosidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            peligrosidadComboBox.FormattingEnabled = true;
            peligrosidadComboBox.Location = new Point(120, 79);
            peligrosidadComboBox.Name = "peligrosidadComboBox";
            peligrosidadComboBox.Size = new Size(160, 23);
            peligrosidadComboBox.TabIndex = 5;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(24, 114);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 6;
            estadoLabel.Text = "Estado:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(120, 111);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(160, 23);
            estadoComboBox.TabIndex = 7;
            // 
            // categoriasLabel
            // 
            categoriasLabel.AutoSize = true;
            categoriasLabel.Location = new Point(24, 146);
            categoriasLabel.Name = "categoriasLabel";
            categoriasLabel.Size = new Size(66, 15);
            categoriasLabel.TabIndex = 8;
            categoriasLabel.Text = "Categorías:";
            // 
            // categoriasCheckedListBox
            // 
            categoriasCheckedListBox.CheckOnClick = true;
            categoriasCheckedListBox.FormattingEnabled = true;
            categoriasCheckedListBox.Location = new Point(120, 146);
            categoriasCheckedListBox.Name = "categoriasCheckedListBox";
            categoriasCheckedListBox.Size = new Size(260, 94);
            categoriasCheckedListBox.TabIndex = 9;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(215, 260);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(80, 25);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(300, 260);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 25);
            cancelarButton.TabIndex = 11;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // BestiaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 300);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(categoriasCheckedListBox);
            Controls.Add(categoriasLabel);
            Controls.Add(estadoComboBox);
            Controls.Add(estadoLabel);
            Controls.Add(peligrosidadComboBox);
            Controls.Add(peligrosidadLabel);
            Controls.Add(nombreTextBox);
            Controls.Add(nombreLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BestiaDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bestia";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label peligrosidadLabel;
        private ComboBox peligrosidadComboBox;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private Label categoriasLabel;
        private CheckedListBox categoriasCheckedListBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
