using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ComentarioDetalle : Form
    {
        private ComentarioDTO comentario = new();
        private FormMode mode;
        private List<BestiaDTO> bestias = new();

        public ComentarioDTO Comentario
        {
            get => comentario;
            set
            {
                comentario = value;
                this.SetComentario();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public ComentarioDetalle()
        {
            InitializeComponent();
            estadoComboBox.Items.AddRange(new object[] { "pendiente", "aprobado" });
        }

        public ComentarioDetalle(FormMode mode, ComentarioDTO comentario, List<BestiaDTO> bestias) : this()
        {
            this.bestias = bestias;
            CargarComboBestias();
            Init(mode, comentario);
        }

        private void CargarComboBestias()
        {
            this.bestiaComboBox.Items.Clear();
            foreach (var b in bestias)
            {
                this.bestiaComboBox.Items.Add(new BestiaComboItem { Id = b.Id, Nombre = $"{b.Nombre} ({b.Peligrosidad})" });
            }
        }

        private void Init(FormMode mode, ComentarioDTO comentario)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Comentario = comentario;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void SetComentario()
        {
            for (int i = 0; i < this.bestiaComboBox.Items.Count; i++)
            {
                if (this.bestiaComboBox.Items[i] is BestiaComboItem item && item.Id == this.Comentario.IdBestia)
                {
                    this.bestiaComboBox.SelectedIndex = i;
                    break;
                }
            }

            this.idTextBox.Text = this.Comentario.Id.ToString();
            this.autorTextBox.Text = this.Comentario.CorreoUsuario ?? LoginForm.UsuarioLogueado?.Correo ?? "-";
            this.tituloTextBox.Text = this.Comentario.Titulo;
            this.contenidoTextBox.Text = this.Comentario.Contenido;
            this.estadoComboBox.SelectedItem = string.IsNullOrEmpty(this.Comentario.Estado) ? "pendiente" : this.Comentario.Estado;
            this.fechaAprobacionTextBox.Text = this.Comentario.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-";
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;
            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                fechaLabel.Visible = false;
                fechaAprobacionTextBox.Visible = false;
                estadoComboBox.SelectedIndex = 0;
                this.Text = "Agregar Comentario";
            }
            else
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                fechaLabel.Visible = true;
                fechaAprobacionTextBox.Visible = true;
                this.Text = "Actualizar Comentario";
            }
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateComentario()) return;

            try
            {
                DeshabilitarControles();

                var selectedBestia = bestiaComboBox.SelectedItem as BestiaComboItem;
                this.Comentario.IdBestia = selectedBestia!.Id;
                this.Comentario.Titulo = tituloTextBox.Text.Trim();
                this.Comentario.Contenido = contenidoTextBox.Text.Trim();
                this.Comentario.Estado = estadoComboBox.SelectedItem?.ToString() ?? "pendiente";

                if (this.Mode == FormMode.Update)
                {
                    await ComentarioApiClient.UpdateAsync(this.Comentario);
                }
                else
                {
                    this.Comentario.IdUsuario = LoginForm.UsuarioLogueado?.Id ?? Guid.Empty;
                    await ComentarioApiClient.AddAsync(this.Comentario);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar comentario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e) => this.Close();

        private bool ValidateComentario()
        {
            bool isValid = true;
            errorProvider.SetError(bestiaComboBox, string.Empty);
            errorProvider.SetError(tituloTextBox, string.Empty);
            errorProvider.SetError(contenidoTextBox, string.Empty);

            if (bestiaComboBox.SelectedItem == null)
            {
                errorProvider.SetError(bestiaComboBox, "Debe seleccionar una bestia");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(tituloTextBox.Text))
            {
                errorProvider.SetError(tituloTextBox, "El título es requerido");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(contenidoTextBox.Text))
            {
                errorProvider.SetError(contenidoTextBox, "El contenido es requerido");
                isValid = false;
            }
            return isValid;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            bestiaComboBox.Enabled = false;
            tituloTextBox.Enabled = false;
            contenidoTextBox.Enabled = false;
            estadoComboBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool puedeGuardar = esInvestigador || (Mode == FormMode.Add && LoginForm.UsuarioLogueado != null);

            aceptarButton.Visible = puedeGuardar;
            aceptarButton.Enabled = puedeGuardar;
            cancelarButton.Enabled = true;
            cancelarButton.Text = puedeGuardar ? "Cancelar" : "Cerrar";

            bestiaComboBox.Enabled = puedeGuardar && Mode == FormMode.Add;
            tituloTextBox.Enabled = puedeGuardar;
            contenidoTextBox.Enabled = puedeGuardar;
            estadoComboBox.Enabled = esInvestigador;
            if (!puedeGuardar) this.Text = "Detalle de Comentario";
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }
    }
}
