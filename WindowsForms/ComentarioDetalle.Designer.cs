namespace WindowsForms
{
    partial class ComentarioDetalle
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
            bestiaLabel = new Label();
            bestiaComboBox = new ComboBox();
            autorLabel = new Label();
            autorTextBox = new TextBox();
            tituloLabel = new Label();
            tituloTextBox = new TextBox();
            contenidoLabel = new Label();
            contenidoTextBox = new TextBox();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
            fechaLabel = new Label();
            fechaAprobacionTextBox = new TextBox();
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
            idTextBox.Location = new Point(130, 15);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(280, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            // 
            // bestiaLabel
            // 
            bestiaLabel.AutoSize = true;
            bestiaLabel.Location = new Point(20, 50);
            bestiaLabel.Name = "bestiaLabel";
            bestiaLabel.Size = new Size(41, 15);
            bestiaLabel.TabIndex = 2;
            bestiaLabel.Text = "Bestia:";
            // 
            // bestiaComboBox
            // 
            bestiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiaComboBox.FormattingEnabled = true;
            bestiaComboBox.Location = new Point(130, 47);
            bestiaComboBox.Name = "bestiaComboBox";
            bestiaComboBox.Size = new Size(280, 23);
            bestiaComboBox.TabIndex = 3;
            // 
            // autorLabel
            // 
            autorLabel.AutoSize = true;
            autorLabel.Location = new Point(20, 82);
            autorLabel.Name = "autorLabel";
            autorLabel.Size = new Size(40, 15);
            autorLabel.TabIndex = 4;
            autorLabel.Text = "Autor:";
            // 
            // autorTextBox
            // 
            autorTextBox.Location = new Point(130, 79);
            autorTextBox.Name = "autorTextBox";
            autorTextBox.ReadOnly = true;
            autorTextBox.Size = new Size(280, 23);
            autorTextBox.TabIndex = 5;
            autorTextBox.TabStop = false;
            // 
            // tituloLabel
            // 
            tituloLabel.AutoSize = true;
            tituloLabel.Location = new Point(20, 114);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(40, 15);
            tituloLabel.TabIndex = 6;
            tituloLabel.Text = "Título:";
            // 
            // tituloTextBox
            // 
            tituloTextBox.Location = new Point(130, 111);
            tituloTextBox.MaxLength = 100;
            tituloTextBox.Name = "tituloTextBox";
            tituloTextBox.Size = new Size(280, 23);
            tituloTextBox.TabIndex = 7;
            // 
            // contenidoLabel
            // 
            contenidoLabel.AutoSize = true;
            contenidoLabel.Location = new Point(20, 146);
            contenidoLabel.Name = "contenidoLabel";
            contenidoLabel.Size = new Size(66, 15);
            contenidoLabel.TabIndex = 8;
            contenidoLabel.Text = "Contenido:";
            // 
            // contenidoTextBox
            // 
            contenidoTextBox.Location = new Point(130, 143);
            contenidoTextBox.MaxLength = 500;
            contenidoTextBox.Multiline = true;
            contenidoTextBox.Name = "contenidoTextBox";
            contenidoTextBox.Size = new Size(280, 85);
            contenidoTextBox.TabIndex = 9;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(20, 240);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 10;
            estadoLabel.Text = "Estado:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(130, 237);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(150, 23);
            estadoComboBox.TabIndex = 11;
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new Point(20, 272);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new Size(99, 15);
            fechaLabel.TabIndex = 12;
            fechaLabel.Text = "F. Aprobación:";
            // 
            // fechaAprobacionTextBox
            // 
            fechaAprobacionTextBox.Location = new Point(130, 269);
            fechaAprobacionTextBox.Name = "fechaAprobacionTextBox";
            fechaAprobacionTextBox.ReadOnly = true;
            fechaAprobacionTextBox.Size = new Size(280, 23);
            fechaAprobacionTextBox.TabIndex = 13;
            fechaAprobacionTextBox.TabStop = false;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(245, 310);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(80, 25);
            aceptarButton.TabIndex = 14;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(330, 310);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 25);
            cancelarButton.TabIndex = 15;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // ComentarioDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 352);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(fechaAprobacionTextBox);
            Controls.Add(fechaLabel);
            Controls.Add(estadoComboBox);
            Controls.Add(estadoLabel);
            Controls.Add(contenidoTextBox);
            Controls.Add(contenidoLabel);
            Controls.Add(tituloTextBox);
            Controls.Add(tituloLabel);
            Controls.Add(autorTextBox);
            Controls.Add(autorLabel);
            Controls.Add(bestiaComboBox);
            Controls.Add(bestiaLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ComentarioDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Comentario";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label bestiaLabel;
        private ComboBox bestiaComboBox;
        private Label autorLabel;
        private TextBox autorTextBox;
        private Label tituloLabel;
        private TextBox tituloTextBox;
        private Label contenidoLabel;
        private TextBox contenidoTextBox;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private Label fechaLabel;
        private TextBox fechaAprobacionTextBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
