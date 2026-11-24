namespace WindowsForms
{
    partial class NoticiaLista
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
            noticiaGridView = new DataGridView();
            eliminarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)noticiaGridView).BeginInit();
            SuspendLayout();
            noticiaGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            noticiaGridView.Location = new Point(43, 37);
            noticiaGridView.Name = "noticiaGridView";
            noticiaGridView.RowHeadersWidth = 25;
            noticiaGridView.Size = new Size(714, 305);
            noticiaGridView.TabIndex = 2;
            eliminarButton.Location = new Point(520, 348);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 23);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            agregarButton.Location = new Point(682, 348);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 23);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            actualizarButton.Location = new Point(601, 348);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 23);
            actualizarButton.TabIndex = 5;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 392);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(eliminarButton);
            Controls.Add(noticiaGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "NoticiaLista";
            Text = "NoticiaLista";
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