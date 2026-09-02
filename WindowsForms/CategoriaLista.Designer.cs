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
            categoriasDataGridView.Location = new Point(21, 42);
            categoriasDataGridView.Margin = new Padding(2, 1, 2, 1);
            categoriasDataGridView.MultiSelect = false;
            categoriasDataGridView.Name = "categoriasDataGridView";
            categoriasDataGridView.ReadOnly = true;
            categoriasDataGridView.RowHeadersWidth = 82;
            categoriasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            categoriasDataGridView.Size = new Size(751, 244);
            categoriasDataGridView.TabIndex = 0;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(691, 300);
            agregarButton.Margin = new Padding(2, 1, 2, 1);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(81, 22);
            agregarButton.TabIndex = 1;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(506, 300);
            eliminarButton.Margin = new Padding(2, 1, 2, 1);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(81, 22);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(598, 300);
            actualizarButton.Margin = new Padding(2, 1, 2, 1);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(81, 22);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(21, 18);
            buscarTextBox.Margin = new Padding(2, 1, 2, 1);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Buscar por nombre o descripcion";
            buscarTextBox.Size = new Size(217, 23);
            buscarTextBox.TabIndex = 4;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(242, 18);
            buscarButton.Margin = new Padding(2, 1, 2, 1);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(65, 18);
            buscarButton.TabIndex = 5;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // CategoriaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 336);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(agregarButton);
            Controls.Add(categoriasDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 1, 2, 1);
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