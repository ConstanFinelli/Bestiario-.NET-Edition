using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Registro
    {
        public Guid IdBestia { get; set; }
        public int NroRegistro { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public Guid IdUsuarioPublicador { get; set; }
        public Guid? IdInvestigadorAprobador { get; set; }
        public string Estado { get; private set; } = "pendiente"; // "pendiente" | "aprobado"

        public Bestia? Bestia { get; set; }
        public Usuario? UsuarioPublicador { get; set; }
        public Investigador? InvestigadorAprobador { get; set; }
        public ICollection<ContenidoRegistro> Contenidos { get; set; } = new List<ContenidoRegistro>();

        public Registro() { }

        public Registro(Guid idBestia, int nroRegistro, Guid idUsuarioPublicador, string estado = "pendiente")
        {
            IdBestia = idBestia;
            NroRegistro = nroRegistro;
            IdUsuarioPublicador = idUsuarioPublicador;
            SetEstado(estado);
        }

        public Registro(Guid idBestia, int nroRegistro, Guid idUsuarioPublicador, Guid? idInvestigadorAprobador, DateTime? fechaAprobacion, DateTime? fechaBaja, string estado)
        {
            IdBestia = idBestia;
            NroRegistro = nroRegistro;
            IdUsuarioPublicador = idUsuarioPublicador;
            IdInvestigadorAprobador = idInvestigadorAprobador;
            FechaAprobacion = fechaAprobacion;
            FechaBaja = fechaBaja;
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

        public void Aprobar(Guid idInvestigador)
        {
            IdInvestigadorAprobador = idInvestigador;
            FechaAprobacion = DateTime.Now;
            Estado = "aprobado";
        }

        public void DarDeBaja()
        {
            FechaBaja = DateTime.Now;
        }

        public void AgregarContenido(string titulo, string contenido)
        {
            int siguienteNro = Contenidos.Count > 0 ? Contenidos.Max(c => c.NroContenido) + 1 : 1;
            var item = new ContenidoRegistro(IdBestia, NroRegistro, siguienteNro, titulo, contenido);
            Contenidos.Add(item);
        }
    }
}
