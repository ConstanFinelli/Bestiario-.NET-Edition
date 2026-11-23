using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ReporteRegistrosForm : Form
    {
        private List<ReporteRegistrosPorBestiaDTO> resultados = new();

        public ReporteRegistrosForm()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            estadoComboBox.Items.AddRange(new object[]
            {
                "-- Todos --",
                "aprobado",
                "pendiente"
            });
            estadoComboBox.SelectedIndex = 0;

            registrosDataGridView.AutoGenerateColumns = false;
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BestiaNombre",
                HeaderText = "Bestia",
                DataPropertyName = "BestiaNombre",
                Width = 140
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Peligrosidad",
                HeaderText = "Peligrosidad",
                DataPropertyName = "Peligrosidad",
                Width = 90
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HabitatNombre",
                HeaderText = "Hábitat",
                DataPropertyName = "HabitatNombre",
                Width = 130
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroRegistro",
                HeaderText = "Nro.",
                DataPropertyName = "NroRegistro",
                Width = 50
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 80
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAprobacionTexto",
                HeaderText = "Fecha Aprobación",
                DataPropertyName = "FechaAprobacionTexto",
                Width = 120
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PublicadorCorreo",
                HeaderText = "Publicado por",
                DataPropertyName = "PublicadorCorreo",
                Width = 145
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AprobadorNombre",
                HeaderText = "Aprobado por",
                DataPropertyName = "AprobadorNombre",
                Width = 120
            });
            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadContenidos",
                HeaderText = "Secciones",
                DataPropertyName = "CantidadContenidos",
                Width = 70
            });

            registrosDataGridView.ReadOnly = true;
            registrosDataGridView.AllowUserToAddRows = false;
            registrosDataGridView.AllowUserToDeleteRows = false;
            registrosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            registrosDataGridView.MultiSelect = false;
        }

        private async void ReporteRegistrosForm_Load(object sender, EventArgs e)
        {
            await CargarBestiasFiltro();
            await CargarReporte();
        }

        private async Task CargarBestiasFiltro()
        {
            try
            {
                var bestias = await BestiaApiClient.GetAllAsync();
                bestiaComboBox.Items.Clear();
                bestiaComboBox.Items.Add(new BestiaComboItem { Id = Guid.Empty, Nombre = "-- Todas las Bestias --" });

                foreach (var b in (bestias ?? Enumerable.Empty<BestiaDTO>()).OrderBy(x => x.Nombre))
                {
                    bestiaComboBox.Items.Add(new BestiaComboItem { Id = b.Id, Nombre = b.Nombre });
                }
                bestiaComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bestias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarReporte()
        {
            try
            {
                SetControlsEnabled(false);

                var selectedBestia = bestiaComboBox.SelectedItem as BestiaComboItem;
                Guid? idBestia = selectedBestia != null && selectedBestia.Id != Guid.Empty ? selectedBestia.Id : null;
                string? estado = estadoComboBox.SelectedIndex > 0 ? estadoComboBox.SelectedItem?.ToString() : null;
                DateTime? desde = desdeCheckBox.Checked ? desdeDateTimePicker.Value.Date : null;
                DateTime? hasta = hastaCheckBox.Checked ? hastaDateTimePicker.Value.Date : null;

                var res = await ReporteApiClient.GetRegistrosPorBestiaAsync(desde, hasta, estado, idBestia);
                resultados = res?.ToList() ?? new List<ReporteRegistrosPorBestiaDTO>();

                var vmList = resultados.Select(r => new ReporteRegistroVM
                {
                    BestiaNombre = r.BestiaNombre,
                    Peligrosidad = r.Peligrosidad,
                    HabitatNombre = r.HabitatNombre,
                    NroRegistro = r.NroRegistro,
                    Estado = r.Estado,
                    FechaAprobacionTexto = r.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-",
                    PublicadorCorreo = r.PublicadorCorreo,
                    AprobadorNombre = r.AprobadorNombre,
                    CantidadContenidos = r.CantidadContenidos
                }).ToList();

                registrosDataGridView.DataSource = vmList;
                totalesLabel.Text = $"Total de registros: {resultados.Count}   |   Total de secciones: {resultados.Sum(x => x.CantidadContenidos)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            buscarButton.Enabled = enabled;
            limpiarButton.Enabled = enabled;
            exportarCsvButton.Enabled = enabled && resultados.Count > 0;
            bestiaComboBox.Enabled = enabled;
            estadoComboBox.Enabled = enabled;
            desdeCheckBox.Enabled = enabled;
            hastaCheckBox.Enabled = enabled;
            desdeDateTimePicker.Enabled = enabled && desdeCheckBox.Checked;
            hastaDateTimePicker.Enabled = enabled && hastaCheckBox.Checked;
        }

        private void desdeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            desdeDateTimePicker.Enabled = desdeCheckBox.Checked;
        }

        private void hastaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            hastaDateTimePicker.Enabled = hastaCheckBox.Checked;
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await CargarReporte();
        }

        private async void limpiarButton_Click(object sender, EventArgs e)
        {
            if (bestiaComboBox.Items.Count > 0) bestiaComboBox.SelectedIndex = 0;
            estadoComboBox.SelectedIndex = 0;
            desdeCheckBox.Checked = false;
            hastaCheckBox.Checked = false;
            await CargarReporte();
        }

        private void exportarCsvButton_Click(object sender, EventArgs e)
        {
            if (resultados.Count == 0) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"ReporteRegistrosBestias_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine("Bestia,Peligrosidad,Habitat,NroRegistro,Estado,FechaAprobacion,Publicador,Aprobador,Secciones");
            foreach (var r in resultados)
            {
                sb.AppendLine(string.Join(",",
                    Esc(r.BestiaNombre),
                    Esc(r.Peligrosidad),
                    Esc(r.HabitatNombre),
                    r.NroRegistro.ToString(),
                    Esc(r.Estado),
                    Esc(r.FechaAprobacion?.ToString("dd/MM/yyyy HH:mm") ?? "-"),
                    Esc(r.PublicadorCorreo),
                    Esc(r.AprobadorNombre),
                    r.CantidadContenidos.ToString()));
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Reporte exportado correctamente.", "Exportación CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Esc(string? value)
        {
            var s = value ?? string.Empty;
            return $"\"{s.Replace("\"", "\"\"")}\"";
        }

        private class BestiaComboItem
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public override string ToString() => Nombre;
        }

        private class ReporteRegistroVM
        {
            public string BestiaNombre { get; set; } = string.Empty;
            public string Peligrosidad { get; set; } = string.Empty;
            public string HabitatNombre { get; set; } = string.Empty;
            public int NroRegistro { get; set; }
            public string Estado { get; set; } = string.Empty;
            public string FechaAprobacionTexto { get; set; } = string.Empty;
            public string PublicadorCorreo { get; set; } = string.Empty;
            public string AprobadorNombre { get; set; } = string.Empty;
            public int CantidadContenidos { get; set; }
        }
    }
}
