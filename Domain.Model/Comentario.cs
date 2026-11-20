using System;

namespace Domain.Model
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public Guid IdBestia { get; set; }
        public Guid IdUsuario { get; set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Contenido { get; private set; } = string.Empty;

        public Bestia? Bestia { get; set; }
        public Usuario? Usuario { get; set; }

        public Comentario() { }

        public Comentario(Guid id, Guid idBestia, Guid idUsuario, string titulo, string contenido)
        {
            Id = id;
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            SetTitulo(titulo);
            SetContenido(contenido);
        }

        public Comentario(Guid idBestia, Guid idUsuario, string titulo, string contenido)
        {
            Id = Guid.NewGuid();
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            SetTitulo(titulo);
            SetContenido(contenido);
        }

        public void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("El título del comentario no puede estar vacío");
            }
            Titulo = titulo.Trim();
        }

        public void SetContenido(string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
            {
                throw new ArgumentException("El contenido del comentario no puede estar vacío");
            }
            Contenido = contenido.Trim();
        }
    }
}
