using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

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

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FechaPublicacion)
                    .IsRequired();

                /*entity.Navigation(e => e.Publicador)
                    .HasField("_publicador");
                    
                entity.HasOne(e => e.Publicador)
                    .WithMany()
                    .HasForeignKey(e => e.PublicadorId); */
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasIndex(e => e.Nombre)
                    .IsUnique();

                // Datos iniciales
                entity.HasData(
                    new { Id = Guid.NewGuid(), Nombre = "Artropodo", Descripcion = "Invertebrados dotados de un esqueleto externo"},
                    new { Id = Guid.NewGuid(), Nombre = "Anfibio", Descripcion = "Animal que vive partes de su vida en agua y tierra"},
                    new { Id = Guid.NewGuid(), Nombre = "Aereo", Descripcion = "Puede volar"},
                    new { Id = Guid.NewGuid(), Nombre = "Bipedo", Descripcion = "Animal que se mueve en dos patas" },
                    new { Id = Guid.NewGuid(), Nombre = "Cuadrupedo", Descripcion = "Ser vivo que se mueve en cuatro patas" }
                );
            });
        }
    }
}