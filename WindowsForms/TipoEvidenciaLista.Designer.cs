namespace WindowsForms
{
    partial class TipoEvidenciaLista
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
            tiposDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)tiposDataGridView).BeginInit();
            SuspendLayout();
            // 
            // tiposDataGridView
            // 
            tiposDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tiposDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tiposDataGridView.Location = new Point(12, 12);
            tiposDataGridView.MultiSelect = false;
            tiposDataGridView.Name = "tiposDataGridView";
            tiposDataGridView.RowHeadersWidth = 30;
            tiposDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tiposDataGridView.Size = new Size(640, 290);
            tiposDataGridView.TabIndex = 0;
            tiposDataGridView.DoubleClick += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(390, 315);
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
            actualizarButton.Location = new Point(476, 315);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(84, 26);
            actualizarButton.TabIndex = 2;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(566, 315);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(86, 26);
            agregarButton.TabIndex = 3;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // TipoEvidenciaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 355);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(tiposDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "TipoEvidenciaLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tipos de Evidencia";
            Load += TipoEvidenciaLista_Load;
            ((System.ComponentModel.ISupportInitialize)tiposDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView tiposDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
    }
}

