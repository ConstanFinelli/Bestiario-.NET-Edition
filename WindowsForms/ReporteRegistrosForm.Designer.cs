namespace WindowsForms
{
    partial class ReporteRegistrosForm
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
            filterPanel = new Panel();
            bestiaLabel = new Label();
            bestiaComboBox = new ComboBox();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
            desdeCheckBox = new CheckBox();
            desdeDateTimePicker = new DateTimePicker();
            hastaCheckBox = new CheckBox();
            hastaDateTimePicker = new DateTimePicker();
            buscarButton = new Button();
            limpiarButton = new Button();
            exportarCsvButton = new Button();
            registrosDataGridView = new DataGridView();
            bottomPanel = new Panel();
            totalesLabel = new Label();
            filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // filterPanel
            // 
            filterPanel.Controls.Add(bestiaLabel);
            filterPanel.Controls.Add(bestiaComboBox);
            filterPanel.Controls.Add(estadoLabel);
            filterPanel.Controls.Add(estadoComboBox);
            filterPanel.Controls.Add(desdeCheckBox);
            filterPanel.Controls.Add(desdeDateTimePicker);
            filterPanel.Controls.Add(hastaCheckBox);
            filterPanel.Controls.Add(hastaDateTimePicker);
            filterPanel.Controls.Add(buscarButton);
            filterPanel.Controls.Add(limpiarButton);
            filterPanel.Controls.Add(exportarCsvButton);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Location = new Point(0, 0);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(924, 72);
            filterPanel.TabIndex = 0;
            // 
            // bestiaLabel
            // 
            bestiaLabel.AutoSize = true;
            bestiaLabel.Location = new Point(12, 14);
            bestiaLabel.Name = "bestiaLabel";
            bestiaLabel.Size = new Size(41, 15);
            bestiaLabel.TabIndex = 0;
            bestiaLabel.Text = "Bestia:";
            // 
            // bestiaComboBox
            // 
            bestiaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            bestiaComboBox.FormattingEnabled = true;
            bestiaComboBox.Location = new Point(60, 10);
            bestiaComboBox.Name = "bestiaComboBox";
            bestiaComboBox.Size = new Size(200, 23);
            bestiaComboBox.TabIndex = 1;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(272, 14);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 2;
            estadoLabel.Text = "Estado:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(323, 10);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(130, 23);
            estadoComboBox.TabIndex = 3;
            // 
            // desdeCheckBox
            // 
            desdeCheckBox.AutoSize = true;
            desdeCheckBox.Location = new Point(12, 43);
            desdeCheckBox.Name = "desdeCheckBox";
            desdeCheckBox.Size = new Size(62, 19);
            desdeCheckBox.TabIndex = 4;
            desdeCheckBox.Text = "Desde:";
            desdeCheckBox.UseVisualStyleBackColor = true;
            desdeCheckBox.CheckedChanged += desdeCheckBox_CheckedChanged;
            // 
            // desdeDateTimePicker
            // 
            desdeDateTimePicker.Enabled = false;
            desdeDateTimePicker.Format = DateTimePickerFormat.Short;
            desdeDateTimePicker.Location = new Point(78, 41);
            desdeDateTimePicker.Name = "desdeDateTimePicker";
            desdeDateTimePicker.Size = new Size(115, 23);
            desdeDateTimePicker.TabIndex = 5;
            // 
            // hastaCheckBox
            // 
            hastaCheckBox.AutoSize = true;
            hastaCheckBox.Location = new Point(210, 43);
            hastaCheckBox.Name = "hastaCheckBox";
            hastaCheckBox.Size = new Size(59, 19);
            hastaCheckBox.TabIndex = 6;
            hastaCheckBox.Text = "Hasta:";
            hastaCheckBox.UseVisualStyleBackColor = true;
            hastaCheckBox.CheckedChanged += hastaCheckBox_CheckedChanged;
            // 
            // hastaDateTimePicker
            // 
            hastaDateTimePicker.Enabled = false;
            hastaDateTimePicker.Format = DateTimePickerFormat.Short;
            hastaDateTimePicker.Location = new Point(273, 41);
            hastaDateTimePicker.Name = "hastaDateTimePicker";
            hastaDateTimePicker.Size = new Size(115, 23);
            hastaDateTimePicker.TabIndex = 7;
            // 
            // buscarButton
            // 
            buscarButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buscarButton.Location = new Point(628, 22);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(88, 28);
            buscarButton.TabIndex = 8;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // limpiarButton
            // 
            limpiarButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            limpiarButton.Location = new Point(722, 22);
            limpiarButton.Name = "limpiarButton";
            limpiarButton.Size = new Size(88, 28);
            limpiarButton.TabIndex = 9;
            limpiarButton.Text = "Limpiar";
            limpiarButton.UseVisualStyleBackColor = true;
            limpiarButton.Click += limpiarButton_Click;
            // 
            // exportarCsvButton
            // 
            exportarCsvButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exportarCsvButton.Location = new Point(816, 22);
            exportarCsvButton.Name = "exportarCsvButton";
            exportarCsvButton.Size = new Size(96, 28);
            exportarCsvButton.TabIndex = 10;
            exportarCsvButton.Text = "Exportar CSV";
            exportarCsvButton.UseVisualStyleBackColor = true;
            exportarCsvButton.Click += exportarCsvButton_Click;
            // 
            // registrosDataGridView
            // 
            registrosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            registrosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            registrosDataGridView.Location = new Point(12, 76);
            registrosDataGridView.Name = "registrosDataGridView";
            registrosDataGridView.Size = new Size(900, 344);
            registrosDataGridView.TabIndex = 1;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(totalesLabel);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 426);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(924, 34);
            bottomPanel.TabIndex = 2;
            // 
            // totalesLabel
            // 
            totalesLabel.AutoSize = true;
            totalesLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            totalesLabel.Location = new Point(12, 9);
            totalesLabel.Name = "totalesLabel";
            totalesLabel.Size = new Size(120, 15);
            totalesLabel.TabIndex = 0;
            totalesLabel.Text = "Total de registros: 0";
            // 
            // ReporteRegistrosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 460);
            Controls.Add(registrosDataGridView);
            Controls.Add(bottomPanel);
            Controls.Add(filterPanel);
            MinimumSize = new Size(740, 380);
            Name = "ReporteRegistrosForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte 2 - Registros por Bestia (ADO.NET)";
            Load += ReporteRegistrosForm_Load;
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).EndInit();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel filterPanel;
        private Label bestiaLabel;
        private ComboBox bestiaComboBox;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private CheckBox desdeCheckBox;
        private DateTimePicker desdeDateTimePicker;
        private CheckBox hastaCheckBox;
        private DateTimePicker hastaDateTimePicker;
        private Button buscarButton;
        private Button limpiarButton;
        private Button exportarCsvButton;
        private DataGridView registrosDataGridView;
        private Panel bottomPanel;
        private Label totalesLabel;
    }
}
