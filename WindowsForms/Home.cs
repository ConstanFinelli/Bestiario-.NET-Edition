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
            AppTheme.ApplyFormTheme(this);
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

            // Tomo medieval encuadernado en cuero marrón (#8E6E53) con ribetes dorados
            using var leatherBrush = new SolidBrush(AppTheme.Primary);

            // Cubierta del libro con esquinas suavemente redondeadas
            var bookRect = new Rectangle(6, 4, 36, 40);
            using var path = new GraphicsPath();
            int radius = 4;
            path.AddArc(bookRect.X, bookRect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bookRect.Right - radius * 2, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bookRect.X, bookRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.FillPath(leatherBrush, path);

            // Borde exterior dorado
            using var goldPen = new Pen(AppTheme.GoldLight, 1.5f);
            e.Graphics.DrawPath(goldPen, path);

            // Cinta marcapáginas dorada/ámbar
            using var ribbonBrush = new SolidBrush(AppTheme.Gold);
            Point[] ribbon = {
                new Point(20, 4),
                new Point(28, 4),
                new Point(28, 18),
                new Point(24, 14),
                new Point(20, 18)
            };
            e.Graphics.FillPolygon(ribbonBrush, ribbon);

            // Hojas pergamino inferiores
            using var parchmentPen = new Pen(AppTheme.Secondary, 2.5f);
            e.Graphics.DrawLine(parchmentPen, 10, 36, 38, 36);
        }
    }
}
