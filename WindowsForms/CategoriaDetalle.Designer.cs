using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsForms
{
    partial class CategoriaDetalle
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
            components = new System.ComponentModel.Container();
            nombreTextBox = new TextBox();
            nombreLabel = new Label();
            aceptarButton = new Button();
            errorProvider = new ErrorProvider(components);
            cancelarButton = new Button();
            descripcionLabel = new Label();
            descripcionTextBox = new TextBox();
            descripcionTextBox = new TextBox();
            idLabel = new Label();
            idTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(243, 109);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(200, 39);
            nombreTextBox.TabIndex = 0;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(44, 109);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(102, 32);
            nombreLabel.TabIndex = 1;
            nombreLabel.Text = "Nombre";
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(444, 480);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(150, 46);
            aceptarButton.TabIndex = 2;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(614, 480);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(150, 46);
            cancelarButton.TabIndex = 3;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // descripcionLabel
            // 
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(44, 176);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(102, 32);
            descripcionLabel.TabIndex = 5;
            descripcionLabel.Text = "descripcion";
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Location = new Point(243, 176);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.Size = new Size(200, 39);
            descripcionTextBox.TabIndex = 1;
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(44, 33);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(34, 32);
            idLabel.TabIndex = 11;
            idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(243, 33);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(200, 39);
            idTextBox.TabIndex = 0;
            idTextBox.TabStop = false;
            // 
            // CategoriaDetalle
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 570);
            Controls.Add(idLabel);
            Controls.Add(idTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(nombreLabel);
            Controls.Add(nombreTextBox);
            Name = "CategoriaDetalle";
            Text = "Categoria";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nombreTextBox;
        private Label nombreLabel;
        private Button aceptarButton;
        private ErrorProvider errorProvider;
        private Button cancelarButton;
        private Label descripcionLabel;
        private TextBox descripcionTextBox;
        private Label idLabel;
        private TextBox idTextBox;
    }
}