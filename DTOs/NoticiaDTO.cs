using System.Text.RegularExpressions;

namespace DTOs
{
    public class NoticiaDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        //public Investigador Publicador {get; set;}
    }
}