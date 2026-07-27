namespace Domain.Model
{
    public class Noticia
    {
        public Guid Id { get; set; }
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        /*private Investigador _publicador; Aun no se desarrollo la clase investigador
        private Guid _publicadorId;
        public Guid PublicadorId
        {
            get =>  _publicador?.Id ?? _publicadorId;
            private set => _publicadorId = value;
        }
        public Investigador Publicador
        {
             get => _publicador;
            private set 
            {
                _publicador = value;
                if (value != null && _publicadorId != value.Id)
                {
                    _publicadorId = value.Id;
                }
            }
        }
         */

        public Noticia(Guid inId, string inTitulo, string inContenido, DateTime inFechaPublicacion/*, Investigador inPublicador*/)
        {
            Id = inId;
            Titulo = inTitulo;
            Contenido = inContenido;
            FechaPublicacion = inFechaPublicacion;
            //Publicador = inPublicador
        }

        public Noticia(Guid id)
        {
            Id = id;
        }

        public Noticia(string inTitulo, string inContenido, DateTime inFechaPublicacion/*, Investigador inPublicador*/)
        {
            Titulo = inTitulo;
            Contenido = inContenido;
            FechaPublicacion = inFechaPublicacion;
            //Publicador = inPublicador
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
