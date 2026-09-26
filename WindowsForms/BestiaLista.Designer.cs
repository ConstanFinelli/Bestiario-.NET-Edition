namespace WindowsForms
{
    partial class BestiaLista
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
            bestiasDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            verRegistrosButton = new Button();
            ((System.ComponentModel.ISupportInitialize)bestiasDataGridView).BeginInit();
            SuspendLayout();
            // 
            // bestiasDataGridView
            // 
            bestiasDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bestiasDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            bestiasDataGridView.Location = new Point(12, 12);
            bestiasDataGridView.MultiSelect = false;
            bestiasDataGridView.Name = "bestiasDataGridView";
            bestiasDataGridView.RowHeadersWidth = 30;
            bestiasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bestiasDataGridView.Size = new Size(760, 330);
            bestiasDataGridView.TabIndex = 0;
            bestiasDataGridView.DoubleClick += actualizarButton_Click;
            // 
            // verRegistrosButton
            // 
            verRegistrosButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            verRegistrosButton.Location = new Point(12, 355);
            verRegistrosButton.Name = "verRegistrosButton";
            verRegistrosButton.Size = new Size(130, 26);
            verRegistrosButton.TabIndex = 4;
            verRegistrosButton.Text = "Ver Registros";
            verRegistrosButton.UseVisualStyleBackColor = true;
            verRegistrosButton.Click += verRegistrosButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Location = new Point(516, 355);
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
            actualizarButton.Location = new Point(602, 355);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(80, 26);
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
            // BestiaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 395);
            Controls.Add(verRegistrosButton);
            Controls.Add(agregarButton);
            Controls.Add(actualizarButton);
            Controls.Add(eliminarButton);
            Controls.Add(bestiasDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BestiaLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestias";
            Load += Bestias_Load;
            ((System.ComponentModel.ISupportInitialize)bestiasDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView bestiasDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button verRegistrosButton;
    }
}
