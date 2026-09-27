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
            noticiaGridView = new DataGridView();
            eliminarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)noticiaGridView).BeginInit();
            SuspendLayout();
            // 
            // noticiaGridView
            // 
            noticiaGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            noticiaGridView.Location = new Point(43, 37);
            noticiaGridView.Name = "noticiaGridView";
            noticiaGridView.RowHeadersWidth = 25;
            noticiaGridView.Size = new Size(714, 305);
            noticiaGridView.TabIndex = 2;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(520, 348);
            // eliminarButton
            // 
            eliminarButton.Location = new Point(475, 348);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(95, 28);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "🗑️ Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Location = new Point(580, 348);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(95, 28);
            actualizarButton.TabIndex = 5;
            actualizarButton.Text = "✏️ Editar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(685, 348);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(95, 28);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "➕ Redactar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // NoticiaLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 392);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(eliminarButton);
            Controls.Add(noticiaGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "NoticiaLista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bestiario - Noticias";
            Load += NoticiaLista_Load;
            ((System.ComponentModel.ISupportInitialize)noticiaGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView noticiaGridView;
        private Button eliminarButton;
        private Button agregarButton;
        private Button actualizarButton;
    }
}