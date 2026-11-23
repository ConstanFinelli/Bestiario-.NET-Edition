using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Habitat> Habitats { get; set; }
        public DbSet<CaracteristicaHabitat> CaracteristicasHabitat { get; set; }
        public DbSet<TipoEvidencia> TiposEvidencia { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Bestia> Bestias { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<ContenidoRegistro> ContenidoRegistros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Investigador> Investigadores { get; set; }
        public DbSet<Lector> Lectores { get; set; }
        public DbSet<SolicitudInvestigador> SolicitudesInvestigador { get; set; }
        public DbSet<TokenRecuperacion> TokensRecuperacion { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        internal TPIContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // Entidad Noticia
            // ==========================================
            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(25);
                entity.Property(e => e.Contenido).IsRequired().HasMaxLength(150);
                entity.Property(e => e.FechaPublicacion).IsRequired();

                entity.HasData(
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Artropodo", Contenido = "Invertebrados dotados de un esqueleto externo", FechaPublicacion = DateTime.Now },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Anfibio", Contenido = "Animal que vive partes de su vida en agua y tierra", FechaPublicacion = DateTime.Now },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Aereo", Contenido = "Puede volar", FechaPublicacion = DateTime.Now },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Bipedo", Contenido = "Animal que se mueve en dos patas", FechaPublicacion = DateTime.Now },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Cuadrupedo", Contenido = "Ser vivo que se mueve en cuatro patas", FechaPublicacion = DateTime.Now }
                );
            });

            // ==========================================
            // Entidad Categoria
            // ==========================================
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.HasData(
                    new { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Nombre = "Artropodo", Descripcion = "Invertebrados dotados de un esqueleto externo" },
                    new { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Nombre = "Anfibio", Descripcion = "Animal que vive partes de su vida en agua y tierra" },
                    new { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Nombre = "Aereo", Descripcion = "Puede volar" },
                    new { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Nombre = "Bipedo", Descripcion = "Animal que se mueve en dos patas" },
                    new { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Nombre = "Cuadrupedo", Descripcion = "Ser vivo que se mueve en cuatro patas" }
                );
            });

            // ==========================================
            // Entidad Habitat y CaracteristicaHabitat (Maestro/Detalle)
            // ==========================================
            modelBuilder.Entity<Habitat>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Id).ValueGeneratedOnAdd();
                entity.Property(h => h.Nombre).IsRequired().HasMaxLength(60);
                entity.Property(h => h.Localizacion).IsRequired().HasMaxLength(120);
                entity.Property(h => h.Latitud).IsRequired();
                entity.Property(h => h.Longitud).IsRequired();

                entity.HasData(
                    new { Id = Guid.Parse("aaaa1111-1111-1111-1111-111111111111"), Nombre = "Cumbres del Norte", Localizacion = "Cordillera Septentrional", Latitud = -32.8895, Longitud = -68.8458 },
                    new { Id = Guid.Parse("aaaa2222-2222-2222-2222-222222222222"), Nombre = "Pantano de Bruma", Localizacion = "Tierras Bajas del Este", Latitud = -34.6037, Longitud = -58.3816 },
                    new { Id = Guid.Parse("aaaa3333-3333-3333-3333-333333333333"), Nombre = "Desierto Escarlata", Localizacion = "Dunas del Oeste", Latitud = -29.4135, Longitud = -66.8558 }
                );
            });

            modelBuilder.Entity<CaracteristicaHabitat>(entity =>
            {
                entity.HasKey(c => new { c.IdHabitat, c.NroCaracteristica });
                entity.Property(c => c.Descripcion).IsRequired().HasMaxLength(200);

                entity.HasOne(c => c.Habitat)
                    .WithMany(h => h.Caracteristicas)
                    .HasForeignKey(c => c.IdHabitat)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new { IdHabitat = Guid.Parse("aaaa1111-1111-1111-1111-111111111111"), NroCaracteristica = 1, Descripcion = "Clima frío de alta montaña con vientos intensos" },
                    new { IdHabitat = Guid.Parse("aaaa1111-1111-1111-1111-111111111111"), NroCaracteristica = 2, Descripcion = "Acantilados rocosos escarpados aptos para anidación" },
                    new { IdHabitat = Guid.Parse("aaaa2222-2222-2222-2222-222222222222"), NroCaracteristica = 1, Descripcion = "Humedad constante superior al 85% y niebla densa" },
                    new { IdHabitat = Guid.Parse("aaaa3333-3333-3333-3333-333333333333"), NroCaracteristica = 1, Descripcion = "Temperaturas extremas diurnas y arena rojiza profunda" }
                );
            });

            // ==========================================
            // Entidad TipoEvidencia
            // ==========================================
            modelBuilder.Entity<TipoEvidencia>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.Descripcion).IsRequired().HasMaxLength(80);
                entity.Property(t => t.ResourceType).IsRequired().HasMaxLength(40);

                entity.HasData(
                    new { Id = Guid.Parse("bbbb1111-1111-1111-1111-111111111111"), Descripcion = "Fotografía de Campo", ResourceType = "image" },
                    new { Id = Guid.Parse("bbbb2222-2222-2222-2222-222222222222"), Descripcion = "Grabación de Audio", ResourceType = "audio" },
                    new { Id = Guid.Parse("bbbb3333-3333-3333-3333-333333333333"), Descripcion = "Video de Avistamiento", ResourceType = "video" }
                );
            });

            // ==========================================
            // Entidad Bestia (Relación N:M con Categoria y N:1 con Habitat)
            // ==========================================
            modelBuilder.Entity<Bestia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Peligrosidad).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                entity.HasOne(b => b.Habitat)
                    .WithMany(h => h.Bestias)
                    .HasForeignKey(b => b.IdHabitat)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(b => b.Categorias)
                    .WithMany(c => c.Bestias)
                    .UsingEntity(j => j.ToTable("BestiaCategorias"));

                entity.HasData(
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000001"), Nombre = "Grifo Real", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa1111-1111-1111-1111-111111111111") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000002"), Nombre = "Basilisco Menor", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa2222-2222-2222-2222-222222222222") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000003"), Nombre = "Manticora de las Dunas", Peligrosidad = "Muy Alta", Estado = "pendiente", IdHabitat = (Guid?)Guid.Parse("aaaa3333-3333-3333-3333-333333333333") }
                );
            });

            // ==========================================
            // Herencia de Usuario (TPH)
            // ==========================================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.Correo).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Correo).IsUnique();
                entity.Property(u => u.Contrasenia).IsRequired().HasMaxLength(100);

                entity.HasDiscriminator<string>("TipoUsuario")
                    .HasValue<Usuario>("Usuario")
                    .HasValue<Investigador>("Investigador")
                    .HasValue<Lector>("Lector");
            });

            modelBuilder.Entity<Investigador>(entity =>
            {
                entity.Property(i => i.Dni).HasMaxLength(20);
                entity.Property(i => i.Nombre).HasMaxLength(50);
                entity.Property(i => i.Apellido).HasMaxLength(50);

                entity.HasData(
                    new { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Correo = "admin@bestiario.com", Contrasenia = "password", RecibirNotificaciones = true, Dni = "12345678", Nombre = "Admin", Apellido = "Investigador" },
                    new { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Correo = "geraldo@kaer.morhen", Contrasenia = "password", RecibirNotificaciones = true, Dni = "40112233", Nombre = "Geraldo", Apellido = "Rivia" }
                );
            });

            modelBuilder.Entity<Lector>(entity =>
            {
                entity.HasData(
                    new { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Correo = "lector@bestiario.com", Contrasenia = "password", RecibirNotificaciones = false }
                );
            });

            // ==========================================
            // Entidad Registro (PK compuesta: IdBestia, NroRegistro)
            // ==========================================
            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(r => new { r.IdBestia, r.NroRegistro });

                entity.Property(r => r.Estado).IsRequired().HasMaxLength(20);
                entity.Property(r => r.MainPicture).HasMaxLength(255);

                // Relación con Bestia
                entity.HasOne(r => r.Bestia)
                    .WithMany(b => b.Registros)
                    .HasForeignKey(r => r.IdBestia)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Usuario Publicador (Unidireccional)
                entity.HasOne(r => r.UsuarioPublicador)
                    .WithMany()
                    .HasForeignKey(r => r.IdUsuarioPublicador)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con Investigador Aprobador (Unidireccional y opcional)
                entity.HasOne(r => r.InvestigadorAprobador)
                    .WithMany()
                    .HasForeignKey(r => r.IdInvestigadorAprobador)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        NroRegistro = 1,
                        FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0),
                        FechaBaja = (DateTime?)null,
                        IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                        IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"),
                        Estado = "aprobado",
                        MainPicture = (string?)null
                    }
                );
            });

            // ==========================================
            // Entidad ContenidoRegistro (Patrón Maestro/Detalle)
            // ==========================================
            modelBuilder.Entity<ContenidoRegistro>(entity =>
            {
                entity.HasKey(c => new { c.IdBestia, c.NroRegistro, c.NroContenido });

                entity.Property(c => c.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Contenido).IsRequired();

                // Relación con Registro (Maestro/Detalle con eliminación en cascada)
                entity.HasOne(c => c.Registro)
                    .WithMany(r => r.Contenidos)
                    .HasForeignKey(c => new { c.IdBestia, c.NroRegistro })
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        NroRegistro = 1,
                        NroContenido = 1,
                        Titulo = "Hábitat Natural",
                        Contenido = "Habita en las cumbres montañosas rocosas y acantilados altos."
                    },
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        NroRegistro = 1,
                        NroContenido = 2,
                        Titulo = "Comportamiento y Caza",
                        Contenido = "Caza en parejas durante el amanecer. Extremadamente territorial."
                    }
                );
            });

            // ==========================================
            // Entidad Evidencia (PK compuesta: IdBestia, NroEvidencia)
            // ==========================================
            modelBuilder.Entity<Evidencia>(entity =>
            {
                entity.HasKey(e => new { e.IdBestia, e.NroEvidencia });

                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FechaObtencion).IsRequired();
                entity.Property(e => e.FileId).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Bestia)
                    .WithMany(b => b.Evidencias)
                    .HasForeignKey(e => e.IdBestia)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.TipoEvidencia)
                    .WithMany()
                    .HasForeignKey(e => e.IdTipoEvidencia)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        NroEvidencia = 1,
                        Estado = "aprobado",
                        FechaObtencion = new DateTime(2026, 2, 10, 14, 30, 0),
                        FileId = "cay8rme2mbquwxiyfuan",
                        IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111")
                    },
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        NroEvidencia = 2,
                        Estado = "pendiente",
                        FechaObtencion = new DateTime(2026, 2, 12, 11, 15, 0),
                        FileId = "cay8rme2mbquwxiyfuan",
                        IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111")
                    },
                    new
                    {
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                        NroEvidencia = 1,
                        Estado = "aprobado",
                        FechaObtencion = new DateTime(2026, 2, 15, 18, 45, 0),
                        FileId = "cay8rme2mbquwxiyfuan",
                        IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111")
                    }
                );
            });

            // ==========================================
            // Entidad Comentario
            // ==========================================
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.FechaPublicacion).IsRequired();
                entity.Property(c => c.Contenido).IsRequired().HasMaxLength(500);

                entity.HasOne(c => c.Bestia)
                    .WithMany(b => b.Comentarios)
                    .HasForeignKey(c => c.IdBestia)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey(c => c.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new
                    {
                        Id = Guid.Parse("cccc1111-1111-1111-1111-111111111111"),
                        IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        IdUsuario = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                        FechaPublicacion = new DateTime(2026, 1, 16, 9, 30, 0),
                        Contenido = "Observé un ejemplar sobrevolando el paso montañoso al amanecer."
                    }
                );
            });

            // ==========================================
            // Entidad SolicitudInvestigador
            // ==========================================
            modelBuilder.Entity<SolicitudInvestigador>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Dni).IsRequired().HasMaxLength(20);
                entity.Property(s => s.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Apellido).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Motivo).IsRequired().HasMaxLength(500);
                entity.Property(s => s.Estado).IsRequired().HasMaxLength(20);
                entity.Property(s => s.FechaSolicitud).IsRequired();

                entity.HasOne(s => s.Lector)
                    .WithMany()
                    .HasForeignKey(s => s.IdLector)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.InvestigadorResolutor)
                    .WithMany()
                    .HasForeignKey(s => s.IdInvestigadorResolutor)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // Entidad TokenRecuperacion
            // ==========================================
            modelBuilder.Entity<TokenRecuperacion>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.CodigoHash).IsRequired().HasMaxLength(64);
                entity.Property(t => t.FechaCreacion).IsRequired();
                entity.Property(t => t.FechaExpiracion).IsRequired();
                entity.Property(t => t.Usado).IsRequired();

                entity.HasOne(t => t.Usuario)
                    .WithMany()
                    .HasForeignKey(t => t.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}