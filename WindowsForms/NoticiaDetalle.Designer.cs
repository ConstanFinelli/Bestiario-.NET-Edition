namespace WindowsForms
{
    partial class NoticiaDetalle
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
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
            label5 = new Label();
            cancelarButton = new Button();
            SuspendLayout();
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(161, 266);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(75, 23);
            aceptarButton.TabIndex = 0;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += button1_Click;
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(67, 71);
            idTextBox.Name = "idTextBox";
            idTextBox.Size = new Size(169, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TextChanged += idTextBox_TextChanged;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(108, 53);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(18, 15);
            idLabel.TabIndex = 2;
            idLabel.Text = "ID";
            idLabel.Click += label1_Click;
            // 
            // tituloLabel
            // 
            tituloLabel.AutoSize = true;
            tituloLabel.Location = new Point(108, 100);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(37, 15);
            tituloLabel.TabIndex = 4;
            tituloLabel.Text = "Título";
            tituloLabel.Click += label2_Click;
            // 
            // tituloTextBox
            // 
            tituloTextBox.Location = new Point(67, 118);
            tituloTextBox.Name = "tituloTextBox";
            tituloTextBox.Size = new Size(169, 23);
            tituloTextBox.TabIndex = 3;
            tituloTextBox.TextChanged += this.tituloTextBox_TextChanged;
            // 
            // contenidoLabel
            // 
            contenidoLabel.AutoSize = true;
            contenidoLabel.Location = new Point(108, 152);
            contenidoLabel.Name = "contenidoLabel";
            contenidoLabel.Size = new Size(63, 15);
            contenidoLabel.TabIndex = 6;
            contenidoLabel.Text = "Contenido";
            contenidoLabel.Click += this.contenidoLabel_Click;
            // 
            // contenidoTextBox
            // 
            contenidoTextBox.Location = new Point(67, 170);
            contenidoTextBox.Name = "contenidoTextBox";
            contenidoTextBox.Size = new Size(169, 23);
            contenidoTextBox.TabIndex = 5;
            contenidoTextBox.TextChanged += this.contenidoTextBox_TextChanged;
            // 
            // fechaPublicacionLabel
            // 
            fechaPublicacionLabel.AutoSize = true;
            fechaPublicacionLabel.Location = new Point(108, 200);
            fechaPublicacionLabel.Name = "fechaPublicacionLabel";
            fechaPublicacionLabel.Size = new Size(103, 15);
            fechaPublicacionLabel.TabIndex = 8;
            fechaPublicacionLabel.Text = "Fecha Publicación";
            fechaPublicacionLabel.Click += this.fechaPublicacionLabel_Click;
            // 
            // fechaPublicacionTextBox
            // 
            fechaPublicacionTextBox.Location = new Point(67, 218);
            fechaPublicacionTextBox.Name = "fechaPublicacionTextBox";
            fechaPublicacionTextBox.Size = new Size(169, 23);
            fechaPublicacionTextBox.TabIndex = 7;
            fechaPublicacionTextBox.TextChanged += this.fechaPublicacionTextBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(108, 23);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 9;
            label5.Text = "Crear Noticia";
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(67, 266);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(75, 23);
            cancelarButton.TabIndex = 10;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            // 
            // NoticiaDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 301);
            Controls.Add(cancelarButton);
            Controls.Add(label5);
            Controls.Add(fechaPublicacionLabel);
            Controls.Add(fechaPublicacionTextBox);
            Controls.Add(contenidoLabel);
            Controls.Add(contenidoTextBox);
            Controls.Add(tituloLabel);
            Controls.Add(tituloTextBox);
            Controls.Add(idLabel);
            Controls.Add(idTextBox);
            Controls.Add(aceptarButton);
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
        private Label label5;
        private Button cancelarButton;
    }
}
