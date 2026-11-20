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
        public string Estado { get; private set; } = "pendiente"; // "pendiente" | "aprobado"
        public DateTime? FechaAprobacion { get; set; }

        public Bestia? Bestia { get; set; }
        public Usuario? Usuario { get; set; }

        public Comentario() { }

        public Comentario(Guid id, Guid idBestia, Guid idUsuario, string titulo, string contenido, string estado = "pendiente", DateTime? fechaAprobacion = null)
        {
            Id = id;
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            SetTitulo(titulo);
            SetContenido(contenido);
            SetEstado(estado);
            FechaAprobacion = fechaAprobacion;
        }

        public Comentario(Guid idBestia, Guid idUsuario, string titulo, string contenido, string estado = "pendiente")
        {
            Id = Guid.NewGuid();
            IdBestia = idBestia;
            IdUsuario = idUsuario;
            SetTitulo(titulo);
            SetContenido(contenido);
            SetEstado(estado);
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

        public void SetEstado(string estado)
        {
            var est = estado?.Trim().ToLowerInvariant();
            if (est != "pendiente" && est != "aprobado")
            {
                throw new ArgumentException("El estado debe ser 'pendiente' o 'aprobado'");
            }
            Estado = est;
        }

        public void Aprobar()
        {
            FechaAprobacion = DateTime.Now;
            Estado = "aprobado";
        }
    }
}

