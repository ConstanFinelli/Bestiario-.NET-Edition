namespace WindowsForms
{
    partial class NoticiaLista
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
            buscarTextBox = new TextBox();
            buscarButton = new Button();
            noticiaGridView = new DataGridView();
            eliminarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)noticiaGridView).BeginInit();
            SuspendLayout();
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(43, 40);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Buscar por ID o Publicador";
            buscarTextBox.Size = new Size(337, 23);
            buscarTextBox.TabIndex = 0;
            buscarTextBox.Tag = "";
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(385, 40);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(75, 23);
            buscarButton.TabIndex = 1;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            // 
            // noticiaGridView
            // 
            noticiaGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            noticiaGridView.Location = new Point(43, 74);
            noticiaGridView.Name = "noticiaGridView";
            noticiaGridView.RowHeadersWidth = 25;
            noticiaGridView.Size = new Size(714, 268);
            noticiaGridView.TabIndex = 2;
            noticiaGridView.CellContentClick += dataGridView1_CellContentClick;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(520, 348);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(682, 348);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            // 
            // actualizarButton
            // 
            actualizarButton.Location = new Point(601, 348);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 23);
            actualizarButton.TabIndex = 5;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            // 
            // NoticiaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(eliminarButton);
            Controls.Add(noticiaGridView);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Name = "NoticiaLista";
            Text = "NoticiaLista";
            Load += NoticiaLista_Load;
            ((System.ComponentModel.ISupportInitialize)noticiaGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox buscarTextBox;
        private Button buscarButton;
        private DataGridView noticiaGridView;
        private Button eliminarButton;
        private Button agregarButton;
        private Button actualizarButton;
    }
}