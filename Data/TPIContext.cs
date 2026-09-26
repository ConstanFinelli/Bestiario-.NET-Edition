using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Bestia> Bestias { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<ContenidoRegistro> ContenidoRegistros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Investigador> Investigadores { get; set; }
        public DbSet<Lector> Lectores { get; set; }

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
            // Entidad Bestia (Relación N:M con Categoria)
            // ==========================================
            modelBuilder.Entity<Bestia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Peligrosidad).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);

                entity.HasMany(b => b.Categorias)
                    .WithMany(c => c.Bestias)
                    .UsingEntity(j => j.ToTable("BestiaCategorias"));

                entity.HasData(
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000001"), Nombre = "Grifo Real", Peligrosidad = "Alta", Estado = "aprobado" },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000002"), Nombre = "Basilisco Menor", Peligrosidad = "Extrema", Estado = "aprobado" },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000003"), Nombre = "Manticora de las Dunas", Peligrosidad = "Muy Alta", Estado = "pendiente" }
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
                        Estado = "aprobado"
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
        }
    }
}