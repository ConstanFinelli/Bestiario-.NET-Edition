namespace WindowsForms
{
    partial class RegistroLista
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
            filtroPanel = new Panel();
            bestiasComboBox = new ComboBox();
            filtroLabel = new Label();
            registrosDataGridView = new DataGridView();
            aprobarButton = new Button();
            eliminarButton = new Button();
            actualizarButton = new Button();
            agregarButton = new Button();
            filtroPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).BeginInit();
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
            filtroPanel.Size = new Size(810, 40);
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
            // registrosDataGridView
            // 
            registrosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            registrosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            registrosDataGridView.Location = new Point(12, 58);
            registrosDataGridView.MultiSelect = false;
            registrosDataGridView.Name = "registrosDataGridView";
            registrosDataGridView.RowHeadersWidth = 30;
            registrosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            registrosDataGridView.Size = new Size(810, 290);
            registrosDataGridView.TabIndex = 1;
            registrosDataGridView.SelectionChanged += registrosDataGridView_SelectionChanged;
            registrosDataGridView.DoubleClick += actualizarButton_Click;
            // 
            // aprobarButton
            // 
            aprobarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            aprobarButton.ForeColor = Color.DarkGreen;
            aprobarButton.Location = new Point(12, 360);
            aprobarButton.Name = "aprobarButton";
            aprobarButton.Size = new Size(130, 26);
            aprobarButton.TabIndex = 2;
            aprobarButton.Text = "✔ Aprobar Registro";
            aprobarButton.UseVisualStyleBackColor = true;
            aprobarButton.Click += aprobarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(510, 358);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(95, 28);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "🗑️ Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Location = new Point(612, 358);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(105, 28);
            actualizarButton.TabIndex = 4;
            actualizarButton.Text = "✏️ Ver / Editar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(724, 358);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(98, 28);
            agregarButton.TabIndex = 5;
            agregarButton.Text = "➕ Nuevo";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // RegistroLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 400);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(aprobarButton);
            Controls.Add(registrosDataGridView);
            Controls.Add(filtroPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RegistroLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestiario - Registros de Avistamientos";
            Load += RegistroLista_Load;
            filtroPanel.ResumeLayout(false);
            filtroPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel filtroPanel;
        private ComboBox bestiasComboBox;
        private Label filtroLabel;
        private DataGridView registrosDataGridView;
        private Button aprobarButton;
        private Button eliminarButton;
        private Button actualizarButton;
        private Button agregarButton;
    }
}
