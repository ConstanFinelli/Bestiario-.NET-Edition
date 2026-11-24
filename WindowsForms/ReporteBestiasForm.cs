using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ReporteBestiasForm : Form
    {
        private ReporteEstadisticasBestiasDTO? estadisticas;

        public ReporteBestiasForm()
        {
            InitializeComponent();
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            tipoGraficoComboBox.Items.AddRange(new object[]
            {
                "Por Peligrosidad",
                "Por Categoría"
            });
            tipoGraficoComboBox.SelectedIndex = 0;

            resumenDataGridView.AutoGenerateColumns = false;
            resumenDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Grupo",
                DataPropertyName = "Nombre",
                Width = 220
            });
            resumenDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 90
            });
            resumenDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Aprobadas",
                HeaderText = "Aprobadas",
                DataPropertyName = "Aprobadas",
                Width = 100
            });
            resumenDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pendientes",
                HeaderText = "Pendientes",
                DataPropertyName = "Pendientes",
                Width = 100
            });

            resumenDataGridView.ReadOnly = true;
            resumenDataGridView.AllowUserToAddRows = false;
            resumenDataGridView.AllowUserToDeleteRows = false;
            resumenDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resumenDataGridView.MultiSelect = false;
        }

        private async void ReporteBestiasForm_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void tipoGraficoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        private async Task CargarDatos()
        {
            try
            {
                actualizarButton.Enabled = false;
                tipoGraficoComboBox.Enabled = false;
                resumenLabel.Text = "Consultando estadísticas desde SQL Server (ADO.NET)...";

                estadisticas = await ReporteApiClient.GetEstadisticasBestiasAsync();
                ActualizarVista();
            }
            catch (Exception ex)
            {
                resumenLabel.Text = "Error al cargar estadísticas.";
                MessageBox.Show($"Error al obtener estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                actualizarButton.Enabled = true;
                tipoGraficoComboBox.Enabled = true;
            }
        }

        private void ActualizarVista()
        {
            if (estadisticas == null) return;

            resumenLabel.Text =
                $"Total Bestias: {estadisticas.TotalBestias}   |   " +
                $"Aprobadas: {estadisticas.TotalAprobadas}   |   " +
                $"Pendientes: {estadisticas.TotalPendientes}   |   " +
                $"Registros: {estadisticas.TotalRegistros}   |   " +
                $"Evidencias: {estadisticas.TotalEvidencias}";

            bool porCategoria = tipoGraficoComboBox.SelectedIndex == 1;
            var rows = porCategoria
                ? estadisticas.PorCategoria.Select(c => new ResumenRow
                {
                    Nombre = c.Categoria,
                    Total = c.CantidadBestias,
                    Aprobadas = c.Aprobadas,
                    Pendientes = c.Pendientes
                }).ToList()
                : estadisticas.PorPeligrosidad.Select(p => new ResumenRow
                {
                    Nombre = p.Peligrosidad,
                    Total = p.Cantidad,
                    Aprobadas = p.Aprobadas,
                    Pendientes = p.Pendientes
                }).ToList();

            resumenDataGridView.DataSource = rows;
            chartPictureBox.Invalidate();
        }

        private void chartPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (estadisticas == null)
            {
                DrawCenteredText(g, "Sin datos cargados.", chartPictureBox.ClientRectangle);
                return;
            }

            bool porCategoria = tipoGraficoComboBox.SelectedIndex == 1;
            var items = porCategoria
                ? estadisticas.PorCategoria.Select(c => new ResumenRow
                {
                    Nombre = c.Categoria,
                    Total = c.CantidadBestias,
                    Aprobadas = c.Aprobadas,
                    Pendientes = c.Pendientes
                }).ToList()
                : estadisticas.PorPeligrosidad.Select(p => new ResumenRow
                {
                    Nombre = p.Peligrosidad,
                    Total = p.Cantidad,
                    Aprobadas = p.Aprobadas,
                    Pendientes = p.Pendientes
                }).ToList();

            if (items.Count == 0)
            {
                DrawCenteredText(g, "No hay registros suficientes para graficar.", chartPictureBox.ClientRectangle);
                return;
            }

            int width = chartPictureBox.Width;
            int height = chartPictureBox.Height;
            int marginLeft = 52;
            int marginRight = 24;
            int marginTop = 58;
            int marginBottom = 56;
            int plotW = Math.Max(100, width - marginLeft - marginRight);
            int plotH = Math.Max(80, height - marginTop - marginBottom);

            using var titleFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            using var axisFont = new Font("Segoe UI", 8.5f);
            using var boldLabelFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
            using var mutedBrush = new SolidBrush(Color.FromArgb(100, 100, 100));
            using var gridPen = new Pen(Color.FromArgb(228, 232, 236), 1f);
            using var axisPen = new Pen(Color.FromArgb(120, 120, 120), 1.2f);
            using var aprobBrush = new SolidBrush(porCategoria ? Color.FromArgb(13, 110, 253) : Color.FromArgb(25, 135, 84));
            using var pendBrush = new SolidBrush(Color.FromArgb(255, 193, 7));

            string titulo = porCategoria ? "Bestias por Categoría" : "Bestias por Peligrosidad";
            g.DrawString(titulo, titleFont, textBrush, marginLeft, 8);

            int legendX = Math.Max(marginLeft + 180, width - 210);
            g.FillRectangle(aprobBrush, legendX, 11, 12, 12);
            g.DrawString("Aprobadas", axisFont, textBrush, legendX + 16, 10);
            g.FillRectangle(pendBrush, legendX + 95, 11, 12, 12);
            g.DrawString("Pendientes", axisFont, textBrush, legendX + 111, 10);

            int maxVal = Math.Max(1, items.Max(x => x.Total));
            int steps = Math.Min(5, maxVal);
            if (steps < 1) steps = 1;

            for (int i = 0; i <= steps; i++)
            {
                float ratio = (float)i / steps;
                int y = marginTop + plotH - (int)(plotH * ratio);
                g.DrawLine(gridPen, marginLeft, y, marginLeft + plotW, y);
                int tickVal = (int)Math.Round(maxVal * ratio);
                var tickSize = g.MeasureString(tickVal.ToString(), axisFont);
                g.DrawString(tickVal.ToString(), axisFont, mutedBrush, marginLeft - tickSize.Width - 6, y - tickSize.Height / 2);
            }

            g.DrawLine(axisPen, marginLeft, marginTop, marginLeft, marginTop + plotH);
            g.DrawLine(axisPen, marginLeft, marginTop + plotH, marginLeft + plotW, marginTop + plotH);

            float slotW = (float)plotW / items.Count;
            float barW = Math.Min(64f, slotW * 0.55f);

            using var centerFormat = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                float x = marginLeft + i * slotW + (slotW - barW) / 2f;
                float hAprob = (float)item.Aprobadas / maxVal * plotH;
                float hPend = (float)item.Pendientes / maxVal * plotH;
                float yAprob = marginTop + plotH - hAprob;
                float yPend = yAprob - hPend;

                if (item.Aprobadas > 0)
                {
                    g.FillRectangle(aprobBrush, x, yAprob, barW, hAprob);
                }
                if (item.Pendientes > 0)
                {
                    g.FillRectangle(pendBrush, x, yPend, barW, hPend);
                }

                float centerX = x + barW / 2f;
                g.DrawString(item.Total.ToString(), boldLabelFont, textBrush, centerX, yPend - 17, centerFormat);

                var labelRect = new RectangleF(marginLeft + i * slotW, marginTop + plotH + 6, slotW, 38);
                g.DrawString(item.Nombre, axisFont, textBrush, labelRect, centerFormat);
            }
        }

        private void chartPictureBox_Resize(object sender, EventArgs e)
        {
            chartPictureBox.Invalidate();
        }

        private static void DrawCenteredText(Graphics g, string text, Rectangle bounds)
        {
            using var font = new Font("Segoe UI", 9.5f);
            using var brush = new SolidBrush(Color.Gray);
            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(text, font, brush, bounds, sf);
        }

        private class ResumenRow
        {
            public string Nombre { get; set; } = string.Empty;
            public int Total { get; set; }
            public int Aprobadas { get; set; }
            public int Pendientes { get; set; }
        }
    }
}
