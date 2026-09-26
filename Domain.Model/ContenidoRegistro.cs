using System;

namespace Domain.Model
{
    public class ContenidoRegistro
    {
        public Guid IdBestia { get; set; }
        public int NroRegistro { get; set; }
        public int NroContenido { get; set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Contenido { get; private set; } = string.Empty;

        public Registro? Registro { get; set; }

        public ContenidoRegistro() { }

        public ContenidoRegistro(Guid idBestia, int nroRegistro, int nroContenido, string titulo, string contenido)
        {
            IdBestia = idBestia;
            NroRegistro = nroRegistro;
            NroContenido = nroContenido;
            SetTitulo(titulo);
            SetContenido(contenido);
        }

        public ContenidoRegistro(int nroContenido, string titulo, string contenido)
        {
            NroContenido = nroContenido;
            SetTitulo(titulo);
            SetContenido(contenido);
        }

        public void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("El título no puede estar vacío");
            }
            Titulo = titulo.Trim();
        }

        public void SetContenido(string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
            {
                throw new ArgumentException("El contenido no puede estar vacío");
            }
            Contenido = contenido.Trim();
        }
    }
}
