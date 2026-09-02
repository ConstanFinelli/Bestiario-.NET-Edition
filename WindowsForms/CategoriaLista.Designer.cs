namespace WindowsForms
{
    partial class CategoriaLista
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
            categoriasDataGridView = new DataGridView();
            agregarButton = new Button();
            eliminarButton = new Button();
            actualizarButton = new Button();
            buscarTextBox = new TextBox();
            buscarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)categoriasDataGridView).BeginInit();
            SuspendLayout();
            // 
            // categoriasDataGridView
            // 
            categoriasDataGridView.AllowUserToAddRows = false;
            categoriasDataGridView.AllowUserToDeleteRows = false;
            categoriasDataGridView.AllowUserToOrderColumns = true;
            categoriasDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            categoriasDataGridView.Location = new Point(39, 90);
            categoriasDataGridView.MultiSelect = false;
            categoriasDataGridView.Name = "categoriasDataGridView";
            categoriasDataGridView.ReadOnly = true;
            categoriasDataGridView.RowHeadersWidth = 82;
            categoriasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            categoriasDataGridView.Size = new Size(1395, 520);
            categoriasDataGridView.TabIndex = 0;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(1284, 641);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(150, 46);
            agregarButton.TabIndex = 1;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(940, 641);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(150, 46);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(1111, 641);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(150, 46);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(39, 38);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Buscar por nombre o descripcion";
            buscarTextBox.Size = new Size(400, 39);
            buscarTextBox.TabIndex = 4;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(450, 38);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(120, 39);
            buscarButton.TabIndex = 5;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // CategoriaLista
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1478, 716);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(agregarButton);
            Controls.Add(categoriasDataGridView);
            Name = "CategoriaLista";
            Text = "Categorias";
            Load += Categorias_Load;
            ((System.ComponentModel.ISupportInitialize)categoriasDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView categoriasDataGridView;
        private Button agregarButton;
        private Button eliminarButton;
        private Button actualizarButton;
        private TextBox buscarTextBox;
        private Button buscarButton;
    }
}