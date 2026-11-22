using System;

namespace Domain.Model
{
    public class SolicitudInvestigador
    {
        public Guid Id { get; set; }
        public Guid IdLector { get; set; }
        public string Dni { get; private set; } = string.Empty;
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Motivo { get; private set; } = string.Empty;
        public string Estado { get; private set; } = "pendiente"; // "pendiente" | "aprobada" | "rechazada"
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public Guid? IdInvestigadorResolutor { get; set; }

        public Usuario? Lector { get; set; }
        public Investigador? InvestigadorResolutor { get; set; }

        public SolicitudInvestigador() { }

        public SolicitudInvestigador(Guid idLector, string dni, string nombre, string apellido, string motivo)
            : this(Guid.NewGuid(), idLector, dni, nombre, apellido, motivo, "pendiente", DateTime.Now)
        {
        }

        public SolicitudInvestigador(Guid id, Guid idLector, string dni, string nombre, string apellido, string motivo, string estado = "pendiente", DateTime? fechaSolicitud = null)
        {
            if (idLector == Guid.Empty)
            {
                throw new ArgumentException("El identificador del lector es obligatorio.");
            }

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            IdLector = idLector;
            SetDni(dni);
            SetNombre(nombre);
            SetApellido(apellido);
            SetMotivo(motivo);
            SetEstado(estado);
            FechaSolicitud = fechaSolicitud ?? DateTime.Now;
        }

        public void SetDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("El DNI no puede estar vacío.");
            }
            var trimmed = dni.Trim();
            if (trimmed.Length > 20)
            {
                throw new ArgumentException("El DNI no puede superar los 20 caracteres.");
            }
            Dni = trimmed;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            var trimmed = nombre.Trim();
            if (trimmed.Length > 50)
            {
                throw new ArgumentException("El nombre no puede superar los 50 caracteres.");
            }
            Nombre = trimmed;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido no puede estar vacío.");
            }
            var trimmed = apellido.Trim();
            if (trimmed.Length > 50)
            {
                throw new ArgumentException("El apellido no puede superar los 50 caracteres.");
            }
            Apellido = trimmed;
        }

        public void SetMotivo(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
            {
                throw new ArgumentException("El motivo de la solicitud no puede estar vacío.");
            }
            var trimmed = motivo.Trim();
            if (trimmed.Length > 500)
            {
                throw new ArgumentException("El motivo no puede superar los 500 caracteres.");
            }
            Motivo = trimmed;
        }

        public void SetEstado(string estado)
        {
            var est = estado?.Trim().ToLowerInvariant();
            if (est != "pendiente" && est != "aprobada" && est != "rechazada")
            {
                throw new ArgumentException("El estado debe ser 'pendiente', 'aprobada' o 'rechazada'.");
            }
            Estado = est;
        }

        public void Aprobar(Guid idInvestigador)
        {
            if (Estado != "pendiente")
            {
                throw new ArgumentException("Solo se pueden aprobar solicitudes en estado 'pendiente'.");
            }
            if (idInvestigador == Guid.Empty)
            {
                throw new ArgumentException("El investigador resolutor es inválido.");
            }
            Estado = "aprobada";
            FechaResolucion = DateTime.Now;
            IdInvestigadorResolutor = idInvestigador;
        }

        public void Rechazar(Guid idInvestigador)
        {
            if (Estado != "pendiente")
            {
                throw new ArgumentException("Solo se pueden rechazar solicitudes en estado 'pendiente'.");
            }
            if (idInvestigador == Guid.Empty)
            {
                throw new ArgumentException("El investigador resolutor es inválido.");
            }
            Estado = "rechazada";
            FechaResolucion = DateTime.Now;
            IdInvestigadorResolutor = idInvestigador;
        }
    }
}
