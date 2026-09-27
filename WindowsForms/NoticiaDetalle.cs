using System.Text.RegularExpressions;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public enum FormMode
    {
        Add,
        Update
    }
    public partial class NoticiaDetalle : Form
    {
        public NoticiaDetalle()
        {
            InitializeComponent();
            AppTheme.ApplyFormTheme(this);
        }

        private NoticiaDTO noticia = null!;
        private FormMode mode;

        public NoticiaDTO Noticia
        {
            get { return noticia; }
            set
            {
                noticia = value;
                this.SetNoticia();
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

        public NoticiaDetalle(FormMode mode, NoticiaDTO noticia) : this()
        {
            Init(mode, noticia);
        }

        private async void Init(FormMode mode, NoticiaDTO noticia)
        {
            try
            {
                DeshabilitarControles();
                this.Mode = mode;
                this.Noticia = noticia;
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
            try
            {
                DeshabilitarControles();

                this.Noticia.Titulo = tituloTextBox.Text;
                this.Noticia.Contenido = contenidoTextBox.Text;
                //this.Noticia.Publicador = publicadorTextBox.Text;
                this.Noticia.FechaPublicacion = DateTime.Now;
                if (this.Mode == FormMode.Update)
                {
                    await NoticiaApiClient.UpdateAsync(this.Noticia);
                }
                else
                {
                    await NoticiaApiClient.AddAsync(this.Noticia);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetNoticia()
        {
            this.idTextBox.Text = this.Noticia.Id.ToString();
            this.tituloTextBox.Text = this.Noticia.Titulo;
            this.contenidoTextBox.Text = this.Noticia.Contenido;
            this.fechaPublicacionTextBox.Text = this.Noticia.FechaPublicacion.ToString();
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                fechaPublicacionLabel.Visible = false;
                fechaPublicacionTextBox.Visible = false;
            }

            if (Mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                fechaPublicacionLabel.Visible = true;
                fechaPublicacionTextBox.Visible = true;
            }
        }

        private void DeshabilitarControles()
        {
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            tituloTextBox.Enabled = false;
            contenidoTextBox.Enabled = false;
        }

        private void HabilitarControles()
        {
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            tituloTextBox.Enabled = true;
            contenidoTextBox.Enabled = true;
        }

        private void NoticiaDetalle_Load(object sender, EventArgs e)
        {

        }

        private void fechaPublicacionTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
