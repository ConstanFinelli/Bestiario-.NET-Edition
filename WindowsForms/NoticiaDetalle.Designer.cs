namespace WindowsForms
{
    partial class NoticiaDetalle
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
            aceptarButton = new Button();
            idTextBox = new TextBox();
            idLabel = new Label();
            tituloLabel = new Label();
            tituloTextBox = new TextBox();
            contenidoLabel = new Label();
            contenidoTextBox = new TextBox();
            fechaPublicacionLabel = new Label();
            fechaPublicacionTextBox = new TextBox();
            cancelarButton = new Button();
            SuspendLayout();
            aceptarButton.Location = new Point(199, 205);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 0;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            idTextBox.Location = new Point(112, 10);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(169, 23);
            idTextBox.TabIndex = 1;
            idLabel.AutoSize = true;
            idLabel.Location = new Point(87, 13);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(18, 15);
            idLabel.TabIndex = 2;
            idLabel.Text = "ID";
            tituloLabel.AutoSize = true;
            tituloLabel.Location = new Point(71, 42);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(37, 15);
            tituloLabel.TabIndex = 4;
            tituloLabel.Text = "Título";
            tituloTextBox.Location = new Point(111, 39);
            tituloTextBox.Name = "tituloTextBox";
            tituloTextBox.Size = new Size(169, 23);
            tituloTextBox.TabIndex = 3;
            contenidoLabel.AutoSize = true;
            contenidoLabel.Location = new Point(42, 71);
            contenidoLabel.Name = "contenidoLabel";
            contenidoLabel.Size = new Size(63, 15);
            contenidoLabel.TabIndex = 6;
            contenidoLabel.Text = "Contenido";
            contenidoTextBox.Location = new Point(111, 68);
            contenidoTextBox.Multiline = true;
            contenidoTextBox.Name = "contenidoTextBox";
            contenidoTextBox.Size = new Size(170, 102);
            contenidoTextBox.TabIndex = 5;
            fechaPublicacionLabel.AutoSize = true;
            fechaPublicacionLabel.Location = new Point(2, 179);
            fechaPublicacionLabel.Name = "fechaPublicacionLabel";
            fechaPublicacionLabel.Size = new Size(103, 15);
            fechaPublicacionLabel.TabIndex = 8;
            fechaPublicacionLabel.Text = "Fecha Publicación";
            fechaPublicacionLabel.TextAlign = ContentAlignment.TopCenter;
            fechaPublicacionTextBox.Location = new Point(111, 176);
            fechaPublicacionTextBox.Name = "fechaPublicacionTextBox";
            fechaPublicacionTextBox.ReadOnly = true;
            fechaPublicacionTextBox.Size = new Size(169, 23);
            fechaPublicacionTextBox.TabIndex = 7;
            fechaPublicacionTextBox.TextChanged += fechaPublicacionTextBox_TextChanged;
            cancelarButton.Location = new Point(12, 205);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 10;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 236);
            Controls.Add(cancelarButton);
            Controls.Add(fechaPublicacionLabel);
            Controls.Add(fechaPublicacionTextBox);
            Controls.Add(contenidoLabel);
            Controls.Add(contenidoTextBox);
            Controls.Add(tituloLabel);
            Controls.Add(tituloTextBox);
            Controls.Add(idLabel);
            Controls.Add(idTextBox);
            Controls.Add(aceptarButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "NoticiaDetalle";
            Text = "Gestión de Noticias";
            Load += NoticiaDetalle_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button aceptarButton;
        private TextBox idTextBox;
        private Label idLabel;
        private Label tituloLabel;
        private TextBox tituloTextBox;
        private Label contenidoLabel;
        private TextBox contenidoTextBox;
        private Label fechaPublicacionLabel;
        private TextBox fechaPublicacionTextBox;
        private Button cancelarButton;
    }
}
