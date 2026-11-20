namespace WindowsForms
{
    partial class HabitatDetalle
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
            maestroGroupBox = new GroupBox();
            idLabel = new Label();
            idTextBox = new TextBox();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            localizacionLabel = new Label();
            localizacionTextBox = new TextBox();
            latitudLabel = new Label();
            latitudTextBox = new TextBox();
            longitudLabel = new Label();
            longitudTextBox = new TextBox();
            detalleGroupBox = new GroupBox();
            caracteristicasDataGridView = new DataGridView();
            nuevaCaracteristicaLabel = new Label();
            nuevaCaracteristicaTextBox = new TextBox();
            agregarCaracteristicaButton = new Button();
            quitarCaracteristicaButton = new Button();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            maestroGroupBox.SuspendLayout();
            detalleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)caracteristicasDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // maestroGroupBox
            // 
            maestroGroupBox.Controls.Add(longitudTextBox);
            maestroGroupBox.Controls.Add(longitudLabel);
            maestroGroupBox.Controls.Add(latitudTextBox);
            maestroGroupBox.Controls.Add(latitudLabel);
            maestroGroupBox.Controls.Add(localizacionTextBox);
            maestroGroupBox.Controls.Add(localizacionLabel);
            maestroGroupBox.Controls.Add(nombreTextBox);
            maestroGroupBox.Controls.Add(nombreLabel);
            maestroGroupBox.Controls.Add(idTextBox);
            maestroGroupBox.Controls.Add(idLabel);
            maestroGroupBox.Location = new Point(12, 10);
            maestroGroupBox.Name = "maestroGroupBox";
            maestroGroupBox.Size = new Size(560, 155);
            maestroGroupBox.TabIndex = 0;
            maestroGroupBox.TabStop = false;
            maestroGroupBox.Text = "Datos del Hábitat";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(14, 24);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(20, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(105, 21);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(435, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(14, 54);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(54, 15);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre:";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(105, 51);
            nombreTextBox.MaxLength = 60;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(435, 23);
            nombreTextBox.TabIndex = 3;
            // 
            // localizacionLabel
            // 
            localizacionLabel.AutoSize = true;
            localizacionLabel.Location = new Point(14, 84);
            localizacionLabel.Name = "localizacionLabel";
            localizacionLabel.Size = new Size(76, 15);
            localizacionLabel.TabIndex = 4;
            localizacionLabel.Text = "Localización:";
            // 
            // localizacionTextBox
            // 
            localizacionTextBox.Location = new Point(105, 81);
            localizacionTextBox.MaxLength = 120;
            localizacionTextBox.Name = "localizacionTextBox";
            localizacionTextBox.Size = new Size(435, 23);
            localizacionTextBox.TabIndex = 5;
            // 
            // latitudLabel
            // 
            latitudLabel.AutoSize = true;
            latitudLabel.Location = new Point(14, 114);
            latitudLabel.Name = "latitudLabel";
            latitudLabel.Size = new Size(47, 15);
            latitudLabel.TabIndex = 6;
            latitudLabel.Text = "Latitud:";
            // 
            // latitudTextBox
            // 
            latitudTextBox.Location = new Point(105, 111);
            latitudTextBox.Name = "latitudTextBox";
            latitudTextBox.Size = new Size(140, 23);
            latitudTextBox.TabIndex = 7;
            // 
            // longitudLabel
            // 
            longitudLabel.AutoSize = true;
            longitudLabel.Location = new Point(280, 114);
            longitudLabel.Name = "longitudLabel";
            longitudLabel.Size = new Size(58, 15);
            longitudLabel.TabIndex = 8;
            longitudLabel.Text = "Longitud:";
            // 
            // longitudTextBox
            // 
            longitudTextBox.Location = new Point(350, 111);
            longitudTextBox.Name = "longitudTextBox";
            longitudTextBox.Size = new Size(140, 23);
            longitudTextBox.TabIndex = 9;
            // 
            // detalleGroupBox
            // 
            detalleGroupBox.Controls.Add(quitarCaracteristicaButton);
            detalleGroupBox.Controls.Add(agregarCaracteristicaButton);
            detalleGroupBox.Controls.Add(nuevaCaracteristicaTextBox);
            detalleGroupBox.Controls.Add(nuevaCaracteristicaLabel);
            detalleGroupBox.Controls.Add(caracteristicasDataGridView);
            detalleGroupBox.Location = new Point(12, 175);
            detalleGroupBox.Name = "detalleGroupBox";
            detalleGroupBox.Size = new Size(560, 250);
            detalleGroupBox.TabIndex = 1;
            detalleGroupBox.TabStop = false;
            detalleGroupBox.Text = "Características del Hábitat (Maestro / Detalle)";
            // 
            // caracteristicasDataGridView
            // 
            caracteristicasDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            caracteristicasDataGridView.Location = new Point(14, 22);
            caracteristicasDataGridView.MultiSelect = false;
            caracteristicasDataGridView.Name = "caracteristicasDataGridView";
            caracteristicasDataGridView.RowHeadersWidth = 25;
            caracteristicasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            caracteristicasDataGridView.Size = new Size(532, 135);
            caracteristicasDataGridView.TabIndex = 0;
            // 
            // quitarCaracteristicaButton
            // 
            quitarCaracteristicaButton.Location = new Point(406, 165);
            quitarCaracteristicaButton.Name = "quitarCaracteristicaButton";
            quitarCaracteristicaButton.Size = new Size(140, 25);
            quitarCaracteristicaButton.TabIndex = 1;
            quitarCaracteristicaButton.Text = "Quitar Característica";
            quitarCaracteristicaButton.UseVisualStyleBackColor = true;
            quitarCaracteristicaButton.Click += quitarCaracteristicaButton_Click;
            // 
            // nuevaCaracteristicaLabel
            // 
            nuevaCaracteristicaLabel.AutoSize = true;
            nuevaCaracteristicaLabel.Location = new Point(14, 206);
            nuevaCaracteristicaLabel.Name = "nuevaCaracteristicaLabel";
            nuevaCaracteristicaLabel.Size = new Size(72, 15);
            nuevaCaracteristicaLabel.TabIndex = 2;
            nuevaCaracteristicaLabel.Text = "Descripción:";
            // 
            // nuevaCaracteristicaTextBox
            // 
            nuevaCaracteristicaTextBox.Location = new Point(92, 203);
            nuevaCaracteristicaTextBox.MaxLength = 200;
            nuevaCaracteristicaTextBox.Name = "nuevaCaracteristicaTextBox";
            nuevaCaracteristicaTextBox.Size = new Size(308, 23);
            nuevaCaracteristicaTextBox.TabIndex = 3;
            // 
            // agregarCaracteristicaButton
            // 
            agregarCaracteristicaButton.Location = new Point(406, 202);
            agregarCaracteristicaButton.Name = "agregarCaracteristicaButton";
            agregarCaracteristicaButton.Size = new Size(140, 25);
            agregarCaracteristicaButton.TabIndex = 4;
            agregarCaracteristicaButton.Text = "+ Agregar";
            agregarCaracteristicaButton.UseVisualStyleBackColor = true;
            agregarCaracteristicaButton.Click += agregarCaracteristicaButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(390, 438);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(85, 26);
            aceptarButton.TabIndex = 2;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(487, 438);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(85, 26);
            cancelarButton.TabIndex = 3;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // HabitatDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 478);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(detalleGroupBox);
            Controls.Add(maestroGroupBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HabitatDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Hábitat";
            ((System.ComponentModel.ISupportInitialize)caracteristicasDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox maestroGroupBox;
        private Label idLabel;
        private TextBox idTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label localizacionLabel;
        private TextBox localizacionTextBox;
        private Label latitudLabel;
        private TextBox latitudTextBox;
        private Label longitudLabel;
        private TextBox longitudTextBox;
        private GroupBox detalleGroupBox;
        private DataGridView caracteristicasDataGridView;
        private Label nuevaCaracteristicaLabel;
        private TextBox nuevaCaracteristicaTextBox;
        private Button agregarCaracteristicaButton;
        private Button quitarCaracteristicaButton;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}

