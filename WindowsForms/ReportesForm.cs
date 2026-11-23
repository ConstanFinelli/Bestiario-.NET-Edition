using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    public class ReportesForm : Form
    {
        private readonly TabControl _tabControl;
        private readonly ReporteBestiasForm _bestiasForm;
        private readonly ReporteRegistrosForm _registrosForm;

        public ReportesForm(int initialTabIndex = 0)
        {
            this.Text = "Reportes del Bestiario";
            this.Size = new Size(1020, 680);
            this.MinimumSize = new Size(860, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Load += (_, _) => ThemeHelper.ApplyTheme(this);

            _tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(12, 6)
            };

            var tabEstadisticas = new TabPage("Estadísticas con gráficos")
            {
                BackColor = Color.White
            };
            _bestiasForm = new ReporteBestiasForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabEstadisticas.Controls.Add(_bestiasForm);
            _bestiasForm.Show();

            var tabRegistros = new TabPage("Registros por Bestia")
            {
                BackColor = Color.White
            };
            _registrosForm = new ReporteRegistrosForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            tabRegistros.Controls.Add(_registrosForm);
            _registrosForm.Show();

            _tabControl.TabPages.Add(tabEstadisticas);
            _tabControl.TabPages.Add(tabRegistros);

            if (initialTabIndex >= 0 && initialTabIndex < _tabControl.TabPages.Count)
            {
                _tabControl.SelectedIndex = initialTabIndex;
            }

            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            mainPanel.Controls.Add(_tabControl);

            this.Controls.Add(mainPanel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bestiasForm.Dispose();
                _registrosForm.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
