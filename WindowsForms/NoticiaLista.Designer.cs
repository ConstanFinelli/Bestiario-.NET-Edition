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
            noticiasFlowPanel = new FlowLayoutPanel();
            eliminarButton = new Button();
            agregarButton = new Button();
            actualizarButton = new Button();
            SuspendLayout();
            noticiasFlowPanel.AutoScroll = true;
            noticiasFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            noticiasFlowPanel.WrapContents = true;
            noticiasFlowPanel.BorderStyle = BorderStyle.FixedSingle;
            noticiasFlowPanel.Location = new Point(16, 48);
            noticiasFlowPanel.Name = "noticiasFlowPanel";
            noticiasFlowPanel.Padding = new Padding(8);
            noticiasFlowPanel.Size = new Size(828, 404);
            noticiasFlowPanel.TabIndex = 2;
            eliminarButton.Location = new Point(602, 462);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(75, 25);
            eliminarButton.TabIndex = 3;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            agregarButton.Location = new Point(764, 462);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(75, 25);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            actualizarButton.Location = new Point(683, 462);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(75, 25);
            actualizarButton.TabIndex = 5;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 500);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(eliminarButton);
            Controls.Add(noticiasFlowPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "NoticiaLista";
            Text = "NoticiaLista";
            Load += NoticiaLista_Load;
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel noticiasFlowPanel;
        private Button eliminarButton;
        private Button agregarButton;
        private Button actualizarButton;
    }
}