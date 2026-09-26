using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoriaLista categoriasForm = new CategoriaLista();
            categoriasForm.ShowDialog();
        }

        private void NoticiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoticiaLista noticiasForm = new NoticiaLista();
            noticiasForm.ShowDialog();
        }

        private void BestiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BestiaLista bestiasForm = new BestiaLista();
            bestiasForm.ShowDialog();
        }

        private void RegistrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroLista registrosForm = new RegistroLista();
            registrosForm.ShowDialog();
        }

        private void IconPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Color azul libro idéntico al icono web (#0d6efd)
            using var blueBrush = new SolidBrush(Color.FromArgb(13, 110, 253));

            // Cuerpo del libro con esquinas suavemente redondeadas
            var bookRect = new Rectangle(6, 4, 36, 40);
            using var path = new GraphicsPath();
            int radius = 4;
            path.AddArc(bookRect.X, bookRect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bookRect.X, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.FillPath(blueBrush, path);

            // Cinta de marcador blanca
            using var whiteBrush = new SolidBrush(Color.White);
            Point[] ribbon = {
                new Point(20, 4),
                new Point(28, 4),
                new Point(28, 18),
                new Point(24, 14),
                new Point(20, 18)
            };
            e.Graphics.FillPolygon(whiteBrush, ribbon);

            // Pliegue inferior de páginas
            using var pagePen = new Pen(Color.White, 2.5f);
            e.Graphics.DrawLine(pagePen, 10, 36, 38, 36);
        }
    }
}
