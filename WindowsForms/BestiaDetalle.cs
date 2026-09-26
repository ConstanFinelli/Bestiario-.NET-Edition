using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class BestiaDetalle : Form
    {
        private BestiaDTO bestia;
        private FormMode mode;
        private List<CategoriaDTO> categoriasDisponibles = new();

        public BestiaDTO Bestia
        {
            get => bestia;
            set
            {
                bestia = value;
                this.SetBestia();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public BestiaDetalle()
        {
            InitializeComponent();
            ConfigurarCombos();
        }

        public BestiaDetalle(FormMode mode, BestiaDTO bestia) : this()
        {
            Init(mode, bestia);
        }

        private void ConfigurarCombos()
        {
            this.peligrosidadComboBox.Items.Clear();
            this.peligrosidadComboBox.Items.AddRange(new object[] { "Baja", "Media", "Alta", "Muy Alta", "Extrema" });

            this.estadoComboBox.Items.Clear();
            this.estadoComboBox.Items.AddRange(new object[] { "pendiente", "aprobado" });
        }

        private async void Init(FormMode mode, BestiaDTO bestia)
        {
            try
            {
                DeshabilitarControles();
                await CargarCategorias();
                this.Mode = mode;
                this.Bestia = bestia;
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

        private async Task CargarCategorias()
        {
            try
            {
                var result = await CategoriaApiClient.GetAllAsync();
                categoriasDisponibles = result?.ToList() ?? new List<CategoriaDTO>();

                this.categoriasCheckedListBox.Items.Clear();
                foreach (var cat in categoriasDisponibles)
                {
                    this.categoriasCheckedListBox.Items.Add(new CategoriaCheckItem { Id = cat.Id, Nombre = cat.Nombre });
                }
            }
            catch
            {
                categoriasDisponibles = new List<CategoriaDTO>();
            }
        }

        private void SetBestia()
        {
            this.idTextBox.Text = this.Bestia.Id.ToString();
            this.nombreTextBox.Text = this.Bestia.Nombre;
            this.peligrosidadComboBox.SelectedItem = string.IsNullOrEmpty(this.Bestia.Peligrosidad) ? "Media" : this.Bestia.Peligrosidad;
            this.estadoComboBox.SelectedItem = string.IsNullOrEmpty(this.Bestia.Estado) ? "pendiente" : this.Bestia.Estado;

            // Marcar categorías asignadas
            for (int i = 0; i < this.categoriasCheckedListBox.Items.Count; i++)
            {
                if (this.categoriasCheckedListBox.Items[i] is CategoriaCheckItem item)
                {
                    bool isChecked = this.Bestia.CategoriasIds.Contains(item.Id);
                    this.categoriasCheckedListBox.SetItemChecked(i, isChecked);
                }
            }
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                this.Text = "Agregar Bestia";
                this.peligrosidadComboBox.SelectedIndex = 1; // Media
                this.estadoComboBox.SelectedIndex = 0; // pendiente
            }

            if (Mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                this.Text = "Actualizar Bestia";
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateBestia())
            {
                try
                {
                    DeshabilitarControles();

                    this.Bestia.Nombre = nombreTextBox.Text.Trim();
                    this.Bestia.Peligrosidad = peligrosidadComboBox.SelectedItem?.ToString() ?? "Media";
                    this.Bestia.Estado = estadoComboBox.SelectedItem?.ToString() ?? "pendiente";

                    // Extraer categorías seleccionadas
                    this.Bestia.CategoriasIds.Clear();
                    for (int i = 0; i < categoriasCheckedListBox.CheckedItems.Count; i++)
                    {
                        if (categoriasCheckedListBox.CheckedItems[i] is CategoriaCheckItem item)
                        {
                            this.Bestia.CategoriasIds.Add(item.Id);
                        }
                    }

                    if (this.Mode == FormMode.Update)
                    {
                        await BestiaApiClient.UpdateAsync(this.Bestia);
                    }
                    else
                    {
                        await BestiaApiClient.AddAsync(this.Bestia);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar bestia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool ValidateBestia()
        {
            bool isValid = true;
            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(peligrosidadComboBox, string.Empty);

            if (string.IsNullOrWhiteSpace(this.nombreTextBox.Text))
            {
                isValid = false;
                errorProvider.SetError(nombreTextBox, "El Nombre es requerido");
            }

            if (peligrosidadComboBox.SelectedItem == null)
            {
                isValid = false;
                errorProvider.SetError(peligrosidadComboBox, "Debe seleccionar la peligrosidad");
            }

            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            nombreTextBox.Enabled = false;
            peligrosidadComboBox.Enabled = false;
            estadoComboBox.Enabled = false;
            categoriasCheckedListBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            nombreTextBox.Enabled = true;
            peligrosidadComboBox.Enabled = true;
            estadoComboBox.Enabled = true;
            categoriasCheckedListBox.Enabled = true;
        }

        private class CategoriaCheckItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;

            public override string ToString() => Nombre;
        }
    }
}
