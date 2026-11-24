namespace WindowsForms
{
    partial class EvidenciaLista
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
            evidenciasDataGridView = new DataGridView();
            aprobarButton = new Button();
            eliminarButton = new Button();
            actualizarButton = new Button();
            agregarButton = new Button();
            filtroPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)evidenciasDataGridView).BeginInit();
            SuspendLayout();
            filtroPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtroPanel.BackColor = SystemColors.ControlLight;
            filtroPanel.BorderStyle = BorderStyle.FixedSingle;
            filtroPanel.Controls.Add(bestiasComboBox);
            filtroPanel.Controls.Add(filtroLabel);
            filtroPanel.Location = new Point(12, 10);
            filtroPanel.Name = "filtroPanel";
            filtroPanel.Size = new Size(810, 40);
            filtroPanel.TabIndex = 0;
            bestiasComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiasComboBox.FormattingEnabled = true;
            bestiasComboBox.Location = new Point(130, 8);
            bestiasComboBox.Name = "bestiasComboBox";
            bestiasComboBox.Size = new Size(300, 23);
            bestiasComboBox.TabIndex = 1;
            bestiasComboBox.SelectedIndexChanged += bestiasComboBox_SelectedIndexChanged;
            filtroLabel.AutoSize = true;
            filtroLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtroLabel.Location = new Point(12, 11);
            filtroLabel.Name = "filtroLabel";
            filtroLabel.Size = new Size(106, 15);
            filtroLabel.TabIndex = 0;
            filtroLabel.Text = "Filtrar por Bestia:";
            evidenciasDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            evidenciasDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            evidenciasDataGridView.Location = new Point(12, 58);
            evidenciasDataGridView.MultiSelect = false;
            evidenciasDataGridView.Name = "evidenciasDataGridView";
            evidenciasDataGridView.RowHeadersWidth = 30;
            evidenciasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            evidenciasDataGridView.Size = new Size(810, 290);
            evidenciasDataGridView.TabIndex = 1;
            evidenciasDataGridView.SelectionChanged += evidenciasDataGridView_SelectionChanged;
            evidenciasDataGridView.DoubleClick += actualizarButton_Click;
            aprobarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            aprobarButton.ForeColor = Color.DarkGreen;
            aprobarButton.Location = new Point(12, 360);
            aprobarButton.Name = "aprobarButton";
            aprobarButton.Size = new Size(135, 26);
            aprobarButton.TabIndex = 2;
            aprobarButton.Text = "✔ Aprobar Evidencia";
            aprobarButton.UseVisualStyleBackColor = true;
            aprobarButton.Click += aprobarButton_Click;
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(540, 360);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(80, 26);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Location = new Point(626, 360);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(100, 26);
            actualizarButton.TabIndex = 4;
            actualizarButton.Text = "Ver / Editar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(732, 360);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(90, 26);
            agregarButton.TabIndex = 5;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 400);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(aprobarButton);
            Controls.Add(evidenciasDataGridView);
            Controls.Add(filtroPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "EvidenciaLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Evidencias de Bestias";
            Load += EvidenciaLista_Load;
            filtroPanel.ResumeLayout(false);
            filtroPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)evidenciasDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel filtroPanel;
        private ComboBox bestiasComboBox;
        private Label filtroLabel;
        private DataGridView evidenciasDataGridView;
        private Button aprobarButton;
        private Button eliminarButton;
        private Button actualizarButton;
        private Button agregarButton;
    }
}

