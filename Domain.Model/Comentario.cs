using System;

namespace Domain.Model
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public Guid IdBestia { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        public string Contenido { get; private set; } = string.Empty;

        public Bestia? Bestia { get; set; }
        public Usuario? Usuario { get; set; }

        public Comentario() { }

        public Comentario(Guid id, Guid idBestia, Guid idUsuario, string contenido, DateTime? fechaPublicacion = null)
        {
            Id = id;
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            FechaPublicacion = fechaPublicacion ?? DateTime.Now;
            SetContenido(contenido);
        }

        public Comentario(Guid idBestia, Guid idUsuario, string contenido, DateTime? fechaPublicacion = null)
        {
            Id = Guid.NewGuid();
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            FechaPublicacion = fechaPublicacion ?? DateTime.Now;
            SetContenido(contenido);
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
