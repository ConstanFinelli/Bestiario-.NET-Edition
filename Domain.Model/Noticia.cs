namespace Domain.Model
{
    public class Noticia
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Guid? PublicadorId { get; set; }
        public Investigador? Publicador { get; set; }

        public Noticia() { }

        public Noticia(Guid inId, string inTitulo, string inContenido, DateTime inFechaPublicacion, Guid? inPublicadorId = null)
        {
            Id = inId;
            Titulo = inTitulo;
            Contenido = inContenido;
            FechaPublicacion = inFechaPublicacion;
            PublicadorId = inPublicadorId;
        }

        public Noticia(Guid id)
        {
            Id = id;
        }

        public Noticia(string inTitulo, string inContenido, DateTime inFechaPublicacion, Guid? inPublicadorId = null)
        {
            Titulo = inTitulo;
            Contenido = inContenido;
            FechaPublicacion = inFechaPublicacion;
            PublicadorId = inPublicadorId;
        }

        public void SetTitulo(string inTitulo) { 
            if(inTitulo.Length == 0)
            {
                throw new ArgumentException("El titulo no puede ser una cadena de carácteres vacíos");
            }
            Titulo = inTitulo;
        }

        public void SetContenido (string inContenido)
        {
            if (inContenido.Length == 0)
            {
                throw new ArgumentException("El contenido no puede ser una cadena de carácteres vacíos");
            }
            Contenido = inContenido;
        }





    }
}
