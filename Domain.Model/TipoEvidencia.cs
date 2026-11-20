using System;

namespace Domain.Model
{
    public class TipoEvidencia
    {
        public Guid Id { get; set; }
        public string Descripcion { get; private set; } = string.Empty;
        public string ResourceType { get; private set; } = string.Empty;

        public TipoEvidencia() { }

        public TipoEvidencia(Guid id, string descripcion, string resourceType)
        {
            Id = id;
            SetDescripcion(descripcion);
            SetResourceType(resourceType);
        }

        public TipoEvidencia(string descripcion, string resourceType)
        {
            Id = Guid.NewGuid();
            SetDescripcion(descripcion);
            SetResourceType(resourceType);
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción no puede estar vacía");
            }
            Descripcion = descripcion.Trim();
        }

        public void SetResourceType(string resourceType)
        {
            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw new ArgumentException("El tipo de recurso (ResourceType) no puede estar vacío");
            }
            ResourceType = resourceType.Trim();
        }
    }
}

