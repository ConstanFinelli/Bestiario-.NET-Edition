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
            agregarButton.Location = new Point(685, 296);
            agregarButton.Margin = new Padding(2, 1, 2, 1);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(95, 28);
            agregarButton.TabIndex = 1;
            agregarButton.Text = "➕ Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(475, 296);
            eliminarButton.Margin = new Padding(2, 1, 2, 1);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(95, 28);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "🗑️ Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(580, 296);
            actualizarButton.Margin = new Padding(2, 1, 2, 1);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(95, 28);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "✏️ Editar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // CategoriaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 336);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(agregarButton);
            Controls.Add(categoriasDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 1, 2, 1);
            MaximizeBox = false;
            Name = "CategoriaLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestiario - Categorías";
            Load += Categorias_Load;
            ((System.ComponentModel.ISupportInitialize)categoriasDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView categoriasDataGridView;
        private Button agregarButton;
        private Button eliminarButton;
        private Button actualizarButton;
    }
}