namespace WindowsForms
{
    partial class RegistroDetalle
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
            maestroGroupBox = new GroupBox();
            fechaAprobacionTextBox = new TextBox();
            fechaAprobacionLabel = new Label();
            aprobadorTextBox = new TextBox();
            aprobadorLabel = new Label();
            publicadorTextBox = new TextBox();
            publicadorLabel = new Label();
            estadoComboBox = new ComboBox();
            estadoLabel = new Label();
            nroRegistroTextBox = new TextBox();
            nroRegistroLabel = new Label();
            bestiaComboBox = new ComboBox();
            bestiaLabel = new Label();
            detalleGroupBox = new GroupBox();
            quitarContenidoButton = new Button();
            agregarContenidoButton = new Button();
            nuevoContenidoTextBox = new TextBox();
            nuevoContenidoLabel = new Label();
            nuevoTituloTextBox = new TextBox();
            nuevoTituloLabel = new Label();
            contenidosDataGridView = new DataGridView();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            maestroGroupBox.SuspendLayout();
            detalleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)contenidosDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // maestroGroupBox
            // 
            maestroGroupBox.Controls.Add(fechaAprobacionTextBox);
            maestroGroupBox.Controls.Add(fechaAprobacionLabel);
            maestroGroupBox.Controls.Add(aprobadorTextBox);
            maestroGroupBox.Controls.Add(aprobadorLabel);
            maestroGroupBox.Controls.Add(publicadorTextBox);
            maestroGroupBox.Controls.Add(publicadorLabel);
            maestroGroupBox.Controls.Add(estadoComboBox);
            maestroGroupBox.Controls.Add(estadoLabel);
            maestroGroupBox.Controls.Add(nroRegistroTextBox);
            maestroGroupBox.Controls.Add(nroRegistroLabel);
            maestroGroupBox.Controls.Add(bestiaComboBox);
            maestroGroupBox.Controls.Add(bestiaLabel);
            maestroGroupBox.Location = new Point(12, 10);
            maestroGroupBox.Name = "maestroGroupBox";
            maestroGroupBox.Size = new Size(560, 145);
            maestroGroupBox.TabIndex = 0;
            maestroGroupBox.TabStop = false;
            maestroGroupBox.Text = "Datos Principales (Registro)";
            // 
            // fechaAprobacionTextBox
            // 
            fechaAprobacionTextBox.Location = new Point(380, 110);
            fechaAprobacionTextBox.Name = "fechaAprobacionTextBox";
            fechaAprobacionTextBox.ReadOnly = true;
            fechaAprobacionTextBox.Size = new Size(160, 23);
            fechaAprobacionTextBox.TabIndex = 11;
            fechaAprobacionTextBox.TabStop = false;
            // 
            // fechaAprobacionLabel
            // 
            fechaAprobacionLabel.AutoSize = true;
            fechaAprobacionLabel.Location = new Point(290, 113);
            fechaAprobacionLabel.Name = "fechaAprobacionLabel";
            fechaAprobacionLabel.Size = new Size(84, 15);
            fechaAprobacionLabel.TabIndex = 10;
            fechaAprobacionLabel.Text = "F. Aprobación:";
            // 
            // aprobadorTextBox
            // 
            aprobadorTextBox.Location = new Point(380, 80);
            aprobadorTextBox.Name = "aprobadorTextBox";
            aprobadorTextBox.ReadOnly = true;
            aprobadorTextBox.Size = new Size(160, 23);
            aprobadorTextBox.TabIndex = 9;
            aprobadorTextBox.TabStop = false;
            // 
            // aprobadorLabel
            // 
            aprobadorLabel.AutoSize = true;
            aprobadorLabel.Location = new Point(290, 83);
            aprobadorLabel.Name = "aprobadorLabel";
            aprobadorLabel.Size = new Size(67, 15);
            aprobadorLabel.TabIndex = 8;
            aprobadorLabel.Text = "Aprobador:";
            // 
            // publicadorTextBox
            // 
            publicadorTextBox.Location = new Point(90, 80);
            publicadorTextBox.Name = "publicadorTextBox";
            publicadorTextBox.ReadOnly = true;
            publicadorTextBox.Size = new Size(180, 23);
            publicadorTextBox.TabIndex = 7;
            publicadorTextBox.TabStop = false;
            // 
            // publicadorLabel
            // 
            publicadorLabel.AutoSize = true;
            publicadorLabel.Location = new Point(14, 83);
            publicadorLabel.Name = "publicadorLabel";
            publicadorLabel.Size = new Size(67, 15);
            publicadorLabel.TabIndex = 6;
            publicadorLabel.Text = "Publicador:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Items.AddRange(new object[] { "pendiente", "aprobado" });
            estadoComboBox.Location = new Point(90, 110);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(140, 23);
            estadoComboBox.TabIndex = 5;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(14, 113);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 4;
            estadoLabel.Text = "Estado:";
            // 
            // nroRegistroTextBox
            // 
            nroRegistroTextBox.Location = new Point(380, 22);
            nroRegistroTextBox.Name = "nroRegistroTextBox";
            nroRegistroTextBox.ReadOnly = true;
            nroRegistroTextBox.Size = new Size(80, 23);
            nroRegistroTextBox.TabIndex = 3;
            nroRegistroTextBox.TabStop = false;
            // 
            // nroRegistroLabel
            // 
            nroRegistroLabel.AutoSize = true;
            nroRegistroLabel.Location = new Point(290, 25);
            nroRegistroLabel.Name = "nroRegistroLabel";
            nroRegistroLabel.Size = new Size(79, 15);
            nroRegistroLabel.TabIndex = 2;
            nroRegistroLabel.Text = "Nro. Registro:";
            // 
            // bestiaComboBox
            // 
            bestiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiaComboBox.FormattingEnabled = true;
            bestiaComboBox.Location = new Point(90, 22);
            bestiaComboBox.Name = "bestiaComboBox";
            bestiaComboBox.Size = new Size(180, 23);
            bestiaComboBox.TabIndex = 1;
            // 
            // bestiaLabel
            // 
            bestiaLabel.AutoSize = true;
            bestiaLabel.Location = new Point(14, 25);
            bestiaLabel.Name = "bestiaLabel";
            bestiaLabel.Size = new Size(41, 15);
            bestiaLabel.TabIndex = 0;
            bestiaLabel.Text = "Bestia:";
            // 
            // detalleGroupBox
            // 
            detalleGroupBox.Controls.Add(quitarContenidoButton);
            detalleGroupBox.Controls.Add(agregarContenidoButton);
            detalleGroupBox.Controls.Add(nuevoContenidoTextBox);
            detalleGroupBox.Controls.Add(nuevoContenidoLabel);
            detalleGroupBox.Controls.Add(nuevoTituloTextBox);
            detalleGroupBox.Controls.Add(nuevoTituloLabel);
            detalleGroupBox.Controls.Add(contenidosDataGridView);
            detalleGroupBox.Location = new Point(12, 165);
            detalleGroupBox.Name = "detalleGroupBox";
            detalleGroupBox.Size = new Size(560, 275);
            detalleGroupBox.TabIndex = 1;
            detalleGroupBox.TabStop = false;
            detalleGroupBox.Text = "Detalle de Contenidos (Maestro / Detalle)";
            // 
            // quitarContenidoButton
            // 
            quitarContenidoButton.Location = new Point(410, 165);
            quitarContenidoButton.Name = "quitarContenidoButton";
            quitarContenidoButton.Size = new Size(130, 24);
            quitarContenidoButton.TabIndex = 6;
            quitarContenidoButton.Text = "Quitar Sección";
            quitarContenidoButton.UseVisualStyleBackColor = true;
            quitarContenidoButton.Click += quitarContenidoButton_Click;
            // 
            // agregarContenidoButton
            // 
            agregarContenidoButton.Location = new Point(410, 235);
            agregarContenidoButton.Name = "agregarContenidoButton";
            agregarContenidoButton.Size = new Size(130, 25);
            agregarContenidoButton.TabIndex = 5;
            agregarContenidoButton.Text = "+ Agregar Sección";
            agregarContenidoButton.UseVisualStyleBackColor = true;
            agregarContenidoButton.Click += agregarContenidoButton_Click;
            // 
            // nuevoContenidoTextBox
            // 
            nuevoContenidoTextBox.Location = new Point(80, 235);
            nuevoContenidoTextBox.Name = "nuevoContenidoTextBox";
            nuevoContenidoTextBox.Size = new Size(320, 23);
            nuevoContenidoTextBox.TabIndex = 4;
            // 
            // nuevoContenidoLabel
            // 
            nuevoContenidoLabel.AutoSize = true;
            nuevoContenidoLabel.Location = new Point(14, 238);
            nuevoContenidoLabel.Name = "nuevoContenidoLabel";
            nuevoContenidoLabel.Size = new Size(66, 15);
            nuevoContenidoLabel.TabIndex = 3;
            nuevoContenidoLabel.Text = "Contenido:";
            // 
            // nuevoTituloTextBox
            // 
            nuevoTituloTextBox.Location = new Point(80, 205);
            nuevoTituloTextBox.MaxLength = 100;
            nuevoTituloTextBox.Name = "nuevoTituloTextBox";
            nuevoTituloTextBox.Size = new Size(320, 23);
            nuevoTituloTextBox.TabIndex = 2;
            // 
            // nuevoTituloLabel
            // 
            nuevoTituloLabel.AutoSize = true;
            nuevoTituloLabel.Location = new Point(14, 208);
            nuevoTituloLabel.Name = "nuevoTituloLabel";
            nuevoTituloLabel.Size = new Size(40, 15);
            nuevoTituloLabel.TabIndex = 1;
            nuevoTituloLabel.Text = "Título:";
            // 
            // contenidosDataGridView
            // 
            contenidosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            contenidosDataGridView.Location = new Point(14, 22);
            contenidosDataGridView.MultiSelect = false;
            contenidosDataGridView.Name = "contenidosDataGridView";
            contenidosDataGridView.RowHeadersWidth = 25;
            contenidosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            contenidosDataGridView.Size = new Size(526, 135);
            contenidosDataGridView.TabIndex = 0;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(390, 455);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(85, 25);
            aceptarButton.TabIndex = 2;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(487, 455);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(85, 25);
            cancelarButton.TabIndex = 3;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // RegistroDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 495);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(detalleGroupBox);
            Controls.Add(maestroGroupBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegistroDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registro (Maestro / Detalle)";
            maestroGroupBox.ResumeLayout(false);
            maestroGroupBox.PerformLayout();
            detalleGroupBox.ResumeLayout(false);
            detalleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)contenidosDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox maestroGroupBox;
        private Label bestiaLabel;
        private ComboBox bestiaComboBox;
        private Label nroRegistroLabel;
        private TextBox nroRegistroTextBox;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private Label publicadorLabel;
        private TextBox publicadorTextBox;
        private Label aprobadorLabel;
        private TextBox aprobadorTextBox;
        private Label fechaAprobacionLabel;
        private TextBox fechaAprobacionTextBox;
        private GroupBox detalleGroupBox;
        private DataGridView contenidosDataGridView;
        private Label nuevoTituloLabel;
        private TextBox nuevoTituloTextBox;
        private Label nuevoContenidoLabel;
        private TextBox nuevoContenidoTextBox;
        private Button agregarContenidoButton;
        private Button quitarContenidoButton;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
