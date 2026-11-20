using System;

namespace Domain.Model
{
    public class Evidencia
    {
        public Guid IdBestia { get; set; }
        public int NroEvidencia { get; set; }
        public string Estado { get; private set; } = "pendiente"; // "pendiente" | "aprobado"
        public DateTime FechaObtencion { get; set; }
        public string FileId { get; private set; } = string.Empty;
        public Guid IdTipoEvidencia { get; set; }

        public Bestia? Bestia { get; set; }
        public TipoEvidencia? TipoEvidencia { get; set; }

        public Evidencia() { }

        public Evidencia(Guid idBestia, int nroEvidencia, Guid idTipoEvidencia, DateTime fechaObtencion, string fileId, string estado = "pendiente")
        {
            IdBestia = idBestia;
            NroEvidencia = nroEvidencia;
            IdTipoEvidencia = idTipoEvidencia;
            FechaObtencion = fechaObtencion;
            SetFileId(fileId);
            SetEstado(estado);
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

        public void SetFileId(string fileId)
        {
            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentException("El identificador de archivo (FileId) no puede estar vacío");
            }
            FileId = fileId.Trim();
        }

        public void Aprobar()
        {
            Estado = "aprobado";
        }
    }
}
