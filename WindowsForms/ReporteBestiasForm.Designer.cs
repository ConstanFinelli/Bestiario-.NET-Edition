namespace WindowsForms
{
    partial class ReporteBestiasForm
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
            topPanel = new Panel();
            resumenLabel = new Label();
            tipoLabel = new Label();
            tipoGraficoComboBox = new ComboBox();
            actualizarButton = new Button();
            chartPictureBox = new PictureBox();
            resumenDataGridView = new DataGridView();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resumenDataGridView).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.Controls.Add(resumenLabel);
            topPanel.Controls.Add(tipoLabel);
            topPanel.Controls.Add(tipoGraficoComboBox);
            topPanel.Controls.Add(actualizarButton);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(784, 68);
            topPanel.TabIndex = 0;
            // 
            // resumenLabel
            // 
            resumenLabel.AutoSize = true;
            resumenLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            resumenLabel.Location = new Point(14, 10);
            resumenLabel.Name = "resumenLabel";
            resumenLabel.Size = new Size(140, 15);
            resumenLabel.TabIndex = 0;
            resumenLabel.Text = "Cargando estadísticas...";
            // 
            // tipoLabel
            // 
            tipoLabel.AutoSize = true;
            tipoLabel.Location = new Point(14, 38);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new Size(96, 15);
            tipoLabel.TabIndex = 1;
            tipoLabel.Text = "Agrupación gráfico:";
            // 
            // tipoGraficoComboBox
            // 
            tipoGraficoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoGraficoComboBox.FormattingEnabled = true;
            tipoGraficoComboBox.Location = new Point(155, 35);
            tipoGraficoComboBox.Name = "tipoGraficoComboBox";
            tipoGraficoComboBox.Size = new Size(190, 23);
            tipoGraficoComboBox.TabIndex = 2;
            tipoGraficoComboBox.SelectedIndexChanged += tipoGraficoComboBox_SelectedIndexChanged;
            // 
            // actualizarButton
            // 
            actualizarButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            actualizarButton.Location = new Point(678, 34);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(92, 25);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // chartPictureBox
            // 
            chartPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartPictureBox.BackColor = Color.White;
            chartPictureBox.BorderStyle = BorderStyle.FixedSingle;
            chartPictureBox.Location = new Point(14, 72);
            chartPictureBox.Name = "chartPictureBox";
            chartPictureBox.Size = new Size(756, 260);
            chartPictureBox.TabIndex = 1;
            chartPictureBox.TabStop = false;
            chartPictureBox.Paint += chartPictureBox_Paint;
            chartPictureBox.Resize += chartPictureBox_Resize;
            // 
            // resumenDataGridView
            // 
            resumenDataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resumenDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resumenDataGridView.Location = new Point(14, 342);
            resumenDataGridView.Name = "resumenDataGridView";
            resumenDataGridView.Size = new Size(756, 150);
            resumenDataGridView.TabIndex = 2;
            // 
            // ReporteBestiasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 506);
            Controls.Add(resumenDataGridView);
            Controls.Add(chartPictureBox);
            Controls.Add(topPanel);
            MinimumSize = new Size(640, 440);
            Name = "ReporteBestiasForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte 1 - Estadísticas de Bestias (ADO.NET + Gráfico)";
            Load += ReporteBestiasForm_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)resumenDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Label resumenLabel;
        private Label tipoLabel;
        private ComboBox tipoGraficoComboBox;
        private Button actualizarButton;
        private PictureBox chartPictureBox;
        private DataGridView resumenDataGridView;
    }
}
