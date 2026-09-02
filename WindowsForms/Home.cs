using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoriaLista categoriasForm = new CategoriaLista();
            categoriasForm.ShowDialog();
        }

        private void NoticiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
           NoticiaLista noticiaForm = new NoticiaLista();
            noticiasForm.ShowDialog();
        }
    }
}
}
