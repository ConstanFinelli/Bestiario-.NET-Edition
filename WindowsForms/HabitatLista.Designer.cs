namespace WindowsForms
{
    partial class HabitatLista
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
            habitatsDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)habitatsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // habitatsDataGridView
            // 
            habitatsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            habitatsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            habitatsDataGridView.Location = new Point(12, 12);
            habitatsDataGridView.MultiSelect = false;
            habitatsDataGridView.Name = "habitatsDataGridView";
            habitatsDataGridView.RowHeadersWidth = 30;
            habitatsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            habitatsDataGridView.Size = new Size(760, 330);
            habitatsDataGridView.TabIndex = 0;
            habitatsDataGridView.DoubleClick += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(504, 355);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(80, 26);
            eliminarButton.TabIndex = 1;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Location = new Point(590, 355);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(92, 26);
            actualizarButton.TabIndex = 2;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(688, 355);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(84, 26);
            agregarButton.TabIndex = 3;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // HabitatLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 395);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(habitatsDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "HabitatLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hábitats";
            Load += HabitatLista_Load;
            ((System.ComponentModel.ISupportInitialize)habitatsDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView habitatsDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
    }
}
