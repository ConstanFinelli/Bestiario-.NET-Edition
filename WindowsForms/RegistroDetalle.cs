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
    public partial class RegistroDetalle : Form
    {
        private RegistroDTO registro = null!;
        private FormMode mode;
        private List<BestiaDTO> bestias = new();
        private BindingList<ContenidoRegistroDTO> contenidosBindingList = new();
        private string? _selectedImagePath;
        private Button seleccionarImagenButton = null!;
        private Label imagenNombreLabel = null!;

        public RegistroDTO Registro
        {
            get => registro;
            set
            {
                registro = value;
                this.SetRegistro();
            }
        }

        public FormMode Mode
        {
            get => mode;
            set => SetFormMode(value);
        }

        public RegistroDetalle()
        {
            InitializeComponent();
            ConfigurarColumnasContenidos();
            InitializeImagenPicker();
        }

        private void InitializeImagenPicker()
        {
            seleccionarImagenButton = new Button
            {
                Text = "Imagen...",
                Size = new Size(90, 26),
                Location = new Point(12, aceptarButton.Top)
            };
            seleccionarImagenButton.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog
                {
                    Title = "Seleccionar imagen de presentación",
                    Filter = "Imágenes (*.jpg;*.jpeg;*.png;*.webp;*.gif)|*.jpg;*.jpeg;*.png;*.webp;*.gif"
                };
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    _selectedImagePath = ofd.FileName;
                    imagenNombreLabel.Text = System.IO.Path.GetFileName(_selectedImagePath);
                }
            };

            imagenNombreLabel = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(260, 0),
                Location = new Point(108, aceptarButton.Top + 5),
                ForeColor = Color.DimGray,
                Text = "Sin imagen seleccionada"
            };

            this.Controls.Add(seleccionarImagenButton);
            this.Controls.Add(imagenNombreLabel);
        }

        public RegistroDetalle(FormMode mode, RegistroDTO registro, List<BestiaDTO> bestias) : this()
        {
            this.bestias = bestias;
            CargarComboBestias();
            Init(mode, registro);
        }

        private void ConfigurarColumnasContenidos()
        {
            this.contenidosDataGridView.AutoGenerateColumns = false;

            this.contenidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroContenido",
                HeaderText = "#",
                DataPropertyName = "NroContenido",
                Width = 40
            });

            this.contenidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo",
                Width = 160
            });

            this.contenidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Contenido",
                HeaderText = "Contenido",
                DataPropertyName = "Contenido",
                Width = 320
            });

            this.contenidosDataGridView.ReadOnly = true;
            this.contenidosDataGridView.AllowUserToAddRows = false;
            this.contenidosDataGridView.AllowUserToDeleteRows = false;
            this.contenidosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.contenidosDataGridView.MultiSelect = false;
        }

        private void CargarComboBestias()
        {
            this.bestiaComboBox.Items.Clear();
            foreach (var b in bestias)
            {
                this.bestiaComboBox.Items.Add(new BestiaComboItem { Id = b.Id, Nombre = $"{b.Nombre} ({b.Peligrosidad})" });
            }
        }

        private void Init(FormMode mode, RegistroDTO registro)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Registro = registro;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void SetRegistro()
        {
            // Seleccionar bestia en combobox
            for (int i = 0; i < this.bestiaComboBox.Items.Count; i++)
            {
                if (this.bestiaComboBox.Items[i] is BestiaComboItem item && item.Id == this.Registro.IdBestia)
                {
                    this.bestiaComboBox.SelectedIndex = i;
                    break;
                }
            }

            this.nroRegistroTextBox.Text = this.Registro.NroRegistro.ToString();
            this.estadoComboBox.SelectedItem = string.IsNullOrEmpty(this.Registro.Estado) ? "pendiente" : this.Registro.Estado;
            this.publicadorTextBox.Text = this.Registro.CorreoPublicador ?? "(Se asignará usuario logueado)";
            this.aprobadorTextBox.Text = this.Registro.NombreInvestigadorAprobador ?? "-";
            this.fechaAprobacionTextBox.Text = this.Registro.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-";

            // Cargar detalle de contenidos
            contenidosBindingList = new BindingList<ContenidoRegistroDTO>(this.Registro.Contenidos.ToList());
            this.contenidosDataGridView.DataSource = contenidosBindingList;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                this.Text = "Agregar Registro (Maestro / Detalle)";
                this.nroRegistroLabel.Visible = false;
                this.nroRegistroTextBox.Visible = false;
                this.publicadorLabel.Visible = false;
                this.publicadorTextBox.Visible = false;
                this.aprobadorLabel.Visible = false;
                this.aprobadorTextBox.Visible = false;
                this.fechaAprobacionLabel.Visible = false;
                this.fechaAprobacionTextBox.Visible = false;
                this.bestiaComboBox.Enabled = true;
                this.estadoComboBox.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Actualizar Registro (Maestro / Detalle)";
                this.nroRegistroLabel.Visible = true;
                this.nroRegistroTextBox.Visible = true;
                this.publicadorLabel.Visible = true;
                this.publicadorTextBox.Visible = true;
                this.aprobadorLabel.Visible = true;
                this.aprobadorTextBox.Visible = true;
                this.fechaAprobacionLabel.Visible = true;
                this.fechaAprobacionTextBox.Visible = true;
                this.bestiaComboBox.Enabled = false;
            }
        }

        private void agregarContenidoButton_Click(object sender, EventArgs e)
        {
            string titulo = this.nuevoTituloTextBox.Text.Trim();
            string contenido = this.nuevoContenidoTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("El título de la sección de contenido es requerido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nuevoTituloTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contenido))
            {
                MessageBox.Show("El contenido de la sección es requerido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nuevoContenidoTextBox.Focus();
                return;
            }

            int proximoNro = contenidosBindingList.Any() ? contenidosBindingList.Max(c => c.NroContenido) + 1 : 1;

            contenidosBindingList.Add(new ContenidoRegistroDTO
            {
                IdBestia = this.Registro.IdBestia,
                NroRegistro = this.Registro.NroRegistro,
                NroContenido = proximoNro,
                Titulo = titulo,
                Contenido = contenido
            });

            this.nuevoTituloTextBox.Clear();
            this.nuevoContenidoTextBox.Clear();
            this.nuevoTituloTextBox.Focus();
        }

        private void quitarContenidoButton_Click(object sender, EventArgs e)
        {
            if (this.contenidosDataGridView.SelectedRows.Count == 0) return;

            var item = (ContenidoRegistroDTO)this.contenidosDataGridView.SelectedRows[0].DataBoundItem;
            contenidosBindingList.Remove(item);

            // Re-numerar correlativo 1..N
            for (int i = 0; i < contenidosBindingList.Count; i++)
            {
                contenidosBindingList[i].NroContenido = i + 1;
            }

            this.contenidosDataGridView.Refresh();
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateRegistro()) return;

            string? uploadedPublicId = null;
            try
            {
                DeshabilitarControles();

                if (!string.IsNullOrWhiteSpace(_selectedImagePath) && System.IO.File.Exists(_selectedImagePath))
                {
                    string ext = System.IO.Path.GetExtension(_selectedImagePath).ToLowerInvariant();
                    string contentType = ext switch
                    {
                        ".png" => "image/png",
                        ".webp" => "image/webp",
                        ".gif" => "image/gif",
                        _ => "image/jpeg"
                    };

                    await using var stream = System.IO.File.OpenRead(_selectedImagePath);
                    uploadedPublicId = await CloudinaryApiClient.UploadImageAsync(
                        stream,
                        System.IO.Path.GetFileName(_selectedImagePath),
                        contentType);
                    if (!string.IsNullOrWhiteSpace(uploadedPublicId))
                    {
                        this.Registro.MainPicture = uploadedPublicId;
                    }
                }

                var selectedBestia = this.bestiaComboBox.SelectedItem as BestiaComboItem;
                bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
                this.Registro.IdBestia = selectedBestia!.Id;
                this.Registro.Estado = esInvestigador
                    ? (this.estadoComboBox.SelectedItem?.ToString() ?? "pendiente")
                    : "pendiente";
                this.Registro.Contenidos = contenidosBindingList.ToList();

                foreach (var c in this.Registro.Contenidos)
                {
                    c.IdBestia = this.Registro.IdBestia;
                    c.NroRegistro = this.Registro.NroRegistro;
                }

                if (this.Mode == FormMode.Add)
                {
                    this.Registro.IdUsuarioPublicador = LoginForm.UsuarioLogueado?.Id ?? Guid.Parse("99999999-9999-9999-9999-999999999999");
                    await RegistroApiClient.AddAsync(this.Registro);
                }
                else
                {
                    await RegistroApiClient.UpdateAsync(this.Registro);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(uploadedPublicId))
                {
                    try { await CloudinaryApiClient.DeleteImageAsync(uploadedPublicId); } catch { }
                }
                MessageBox.Show($"Error al guardar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateRegistro()
        {
            errorProvider.SetError(bestiaComboBox, string.Empty);

            if (this.bestiaComboBox.SelectedItem == null)
            {
                errorProvider.SetError(bestiaComboBox, "Debe seleccionar una bestia");
                return false;
            }

            if (!contenidosBindingList.Any())
            {
                MessageBox.Show("Debe incluir al menos una sección de contenido en el registro.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            bestiaComboBox.Enabled = false;
            estadoComboBox.Enabled = false;
            agregarContenidoButton.Enabled = false;
            quitarContenidoButton.Enabled = false;
            seleccionarImagenButton.Enabled = false;
        }

        private void HabilitarControles()
        {
            bool esInvestigador = LoginForm.UsuarioLogueado is InvestigadorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Investigador";
            bool esLector = LoginForm.UsuarioLogueado is LectorDTO || LoginForm.UsuarioLogueado?.TipoUsuario == "Lector";
            bool puedeEditar = esInvestigador || (esLector && Mode == FormMode.Add);

            aceptarButton.Visible = puedeEditar;
            aceptarButton.Enabled = puedeEditar;
            aceptarButton.Text = (!esInvestigador && esLector) ? "Proponer" : "Aceptar";
            cancelarButton.Enabled = true;
            cancelarButton.Text = puedeEditar ? "Cancelar" : "Cerrar";
            bestiaComboBox.Enabled = puedeEditar && Mode == FormMode.Add;
            estadoComboBox.Enabled = esInvestigador;
            agregarContenidoButton.Enabled = puedeEditar;
            quitarContenidoButton.Enabled = puedeEditar;
            nuevoTituloTextBox.Enabled = puedeEditar;
            nuevoContenidoTextBox.Enabled = puedeEditar;
            seleccionarImagenButton.Visible = puedeEditar;
            seleccionarImagenButton.Enabled = puedeEditar;
            imagenNombreLabel.Visible = puedeEditar;
            if (!puedeEditar)
            {
                this.Text = "Detalle de Registro";
            }
            else if (!esInvestigador && esLector)
            {
                this.Text = "Proponer Registro (Maestro / Detalle)";
            }
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }
    }
}
