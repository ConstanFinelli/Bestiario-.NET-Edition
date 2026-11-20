namespace WindowsForms
{
    partial class ComentarioLista
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
            filtroPanel = new Panel();
            bestiasComboBox = new ComboBox();
            filtroLabel = new Label();
            comentariosDataGridView = new DataGridView();
            eliminarButton = new Button();
            actualizarButton = new Button();
            agregarButton = new Button();
            filtroPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comentariosDataGridView).BeginInit();
            SuspendLayout();
            // 
            // filtroPanel
            // 
            filtroPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtroPanel.BackColor = SystemColors.ControlLight;
            filtroPanel.BorderStyle = BorderStyle.FixedSingle;
            filtroPanel.Controls.Add(bestiasComboBox);
            filtroPanel.Controls.Add(filtroLabel);
            filtroPanel.Location = new Point(12, 10);
            filtroPanel.Name = "filtroPanel";
            filtroPanel.Size = new Size(760, 40);
            filtroPanel.TabIndex = 0;
            // 
            // bestiasComboBox
            // 
            bestiasComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiasComboBox.FormattingEnabled = true;
            bestiasComboBox.Location = new Point(130, 8);
            bestiasComboBox.Name = "bestiasComboBox";
            bestiasComboBox.Size = new Size(300, 23);
            bestiasComboBox.TabIndex = 1;
            bestiasComboBox.SelectedIndexChanged += bestiasComboBox_SelectedIndexChanged;
            // 
            // filtroLabel
            // 
            filtroLabel.AutoSize = true;
            filtroLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtroLabel.Location = new Point(12, 11);
            filtroLabel.Name = "filtroLabel";
            filtroLabel.Size = new Size(106, 15);
            filtroLabel.TabIndex = 0;
            filtroLabel.Text = "Filtrar por Bestia:";
            // 
            // comentariosDataGridView
            // 
            comentariosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comentariosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            comentariosDataGridView.Location = new Point(12, 58);
            comentariosDataGridView.MultiSelect = false;
            comentariosDataGridView.Name = "comentariosDataGridView";
            comentariosDataGridView.RowHeadersWidth = 30;
            comentariosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            comentariosDataGridView.Size = new Size(760, 290);
            comentariosDataGridView.TabIndex = 1;
            comentariosDataGridView.SelectionChanged += comentariosDataGridView_SelectionChanged;
            comentariosDataGridView.DoubleClick += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(490, 360);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(80, 26);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Location = new Point(576, 360);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(100, 26);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Ver / Editar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(682, 360);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(90, 26);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // ComentarioLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 400);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(comentariosDataGridView);
            Controls.Add(filtroPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ComentarioLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Comentarios";
            Load += ComentarioLista_Load;
            filtroPanel.ResumeLayout(false);
            filtroPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)comentariosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel filtroPanel;
        private ComboBox bestiasComboBox;
        private Label filtroLabel;
        private DataGridView comentariosDataGridView;
        private Button eliminarButton;
        private Button actualizarButton;
        private Button agregarButton;
    }
}
