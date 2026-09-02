using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace WindowsForms
{
    public enum FormMode
    {
        Add,
        Update
    }

    public partial class CategoriaDetalle : Form
    {
        private CategoriaDTO categoria;
        private FormMode mode;

        public CategoriaDTO Categoria
        {
            get { return categoria; }
            set
            {
                categoria = value;
                this.SetCategoria();
            }
        }

        public FormMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                SetFormMode(value);
            }
        }

        public CategoriaDetalle()
        {
            InitializeComponent();
        }

        public CategoriaDetalle(FormMode mode, CategoriaDTO categoria) : this()
        {
            Init(mode, categoria);
        }

        private async void Init(FormMode mode, CategoriaDTO categoria)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Categoria = categoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateCategoria())
            {
                try
                {
                    DeshabilitarControles();

                    this.Categoria.Nombre = nombreTextBox.Text;
                    this.Categoria.Descripcion = descripcionTextBox.Text;

                    if (this.Mode == FormMode.Update)
                    {
                        await CategoriaApiClient.UpdateAsync(this.Categoria);
                    }
                    else
                    {
                        await CategoriaApiClient.AddAsync(this.Categoria);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar categoria: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HabilitarControles();
                }
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetCategoria()
        {
            this.idTextBox.Text = this.Categoria.Id.ToString();
            this.nombreTextBox.Text = this.Categoria.Nombre;
            this.descripcionTextBox.Text = this.Categoria.Descripcion;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
            }

            if (Mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
            }
        }

        private bool ValidateCategoria()
        {
            bool isValid = true;

            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(descripcionTextBox, string.Empty);

            if (this.nombreTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(nombreTextBox, "El Nombre es requerido");
            }

            if (this.descripcionTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(descripcionTextBox, "El descripcion es requerido");
            }
            return isValid;
        }


        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            nombreTextBox.Enabled = false;
            descripcionTextBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            nombreTextBox.Enabled = true;
            descripcionTextBox.Enabled = true;
        }
    }
}
