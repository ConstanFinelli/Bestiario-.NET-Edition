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

                entity.HasOne(e => e.Publicador)
                    .WithMany()
                    .HasForeignKey(e => e.PublicadorId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Artropodo", Contenido = "Invertebrados dotados de un esqueleto externo", FechaPublicacion = DateTime.Now, PublicadorId = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999") },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Anfibio", Contenido = "Animal que vive partes de su vida en agua y tierra", FechaPublicacion = DateTime.Now, PublicadorId = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999") },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Aereo", Contenido = "Puede volar", FechaPublicacion = DateTime.Now, PublicadorId = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999") },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Bipedo", Contenido = "Animal que se mueve en dos patas", FechaPublicacion = DateTime.Now, PublicadorId = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999") },
                    new { Id = Guid.NewGuid(), Titulo = "NUEVO Cuadrupedo", Contenido = "Ser vivo que se mueve en cuatro patas", FechaPublicacion = DateTime.Now, PublicadorId = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999") }
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
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000001"), Nombre = "Montañas rocosas y acantilados", Localizacion = "Cordillera del Olimpo", Latitud = 40.0856, Longitud = 22.3586 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000002"), Nombre = "Bosques antiguos y densos", Localizacion = "Bosques de Velen", Latitud = 52.2297, Longitud = 21.0122 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000003"), Nombre = "Bosques frondosos y zonas volcánicas", Localizacion = "Bosque Primigenio y Tierras Volcánicas", Latitud = 35.3606, Longitud = 138.7274 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000004"), Nombre = "El Mundo del Revés (dimensión oscura)", Localizacion = "Hawkins (Dimensión Alterna)", Latitud = 39.7684, Longitud = -86.1581 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000005"), Nombre = "Bosques templados y praderas altas", Localizacion = "Tierras Altas de Escocia", Latitud = 56.8198, Longitud = -5.1052 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000006"), Nombre = "Cavernas profundas y minas subterráneas", Localizacion = "Minas de Moria", Latitud = -44.6719, Longitud = 167.9256 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000007"), Nombre = "Pantanos y lagos cenagosos", Localizacion = "Lago Lerna, Argólida", Latitud = 37.5511, Longitud = 22.7167 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000008"), Nombre = "Mazmorras y cavernas subterráneas", Localizacion = "Infraoscuridad Profunda", Latitud = 43.0731, Longitud = -89.4012 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000009"), Nombre = "Tierras heladas y bosques boreales", Localizacion = "Jötunheim Boreal", Latitud = 61.524, Longitud = 8.3125 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000010"), Nombre = "Ruinas antiguas, desiertos y alcantarillas", Localizacion = "Cámara Subterránea Antigua", Latitud = 51.5074, Longitud = -0.1278 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000011"), Nombre = "Regiones volcánicas y laderas montañosas", Localizacion = "Monte Quimera, Licia", Latitud = 36.4314, Longitud = 30.4556 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000012"), Nombre = "Yermos radiactivos y ruinas urbanas", Localizacion = "Yermo Capital", Latitud = 38.9072, Longitud = -77.0369 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000013"), Nombre = "Bosques nevados y taigas", Localizacion = "Bosques Boreales de Ontario", Latitud = 49.2827, Longitud = -84.7495 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000014"), Nombre = "Selvas húmedas y bosques nocturnos", Localizacion = "Selva Inundada", Latitud = 14.5995, Longitud = 120.9842 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000015"), Nombre = "Océanos profundos y mares abiertos", Localizacion = "Mar de Noruega", Latitud = 66.0, Longitud = 5.0 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000016"), Nombre = "Desiertos áridos y selvas remotas", Localizacion = "Meseta Persa", Latitud = 32.6546, Longitud = 51.668 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000017"), Nombre = "Praderas, bosques y llanuras abiertas", Localizacion = "Praderas de Midgar", Latitud = 35.6762, Longitud = 139.6503 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000018"), Nombre = "Criptas, ruinas y cementerios", Localizacion = "Cripta Real de Wyzima", Latitud = 50.0647, Longitud = 19.945 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000019"), Nombre = "Mazmorras, castillos y cámaras del tesoro", Localizacion = "Bóvedas de Waterdeep", Latitud = 47.6062, Longitud = -122.3321 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000020"), Nombre = "Fosas abisales y océanos profundos", Localizacion = "Cráter Oceánico 4546B", Latitud = -11.3733, Longitud = 142.5917 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000021"), Nombre = "Desiertos vastos y llanuras áridas", Localizacion = "Llanuras de Ivalice", Latitud = 23.8859, Longitud = 45.0792 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000022"), Nombre = "Bosques sagrados y santuarios rurales", Localizacion = "Santuarios de Kioto", Latitud = 35.0116, Longitud = 135.7681 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000023"), Nombre = "Picos montañosos y cañones rocosos", Localizacion = "Cañones de Valyria", Latitud = 46.5197, Longitud = 7.9821 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000024"), Nombre = "Cuevas oscuras y bosques sombríos", Localizacion = "Cavernas de Bretaña", Latitud = 48.202, Longitud = -2.9326 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000025"), Nombre = "Instalaciones abandonadas y zonas urbanas en ruinas", Localizacion = "Ciudad 17", Latitud = 42.6977, Longitud = 23.3219 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000026"), Nombre = "Zonas rurales, matorrales y campos de pastoreo", Localizacion = "Campos de Puerto Rico y Norte Argentino", Latitud = 18.2208, Longitud = -66.5901 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000027"), Nombre = "Lagos, ríos y estanques profundos", Localizacion = "Lago Ness, Escocia", Latitud = 57.3229, Longitud = -4.4244 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000028"), Nombre = "Cumbres montañosas y bosques húmedos", Localizacion = "Picos Brumosos", Latitud = 36.2048, Longitud = 138.2529 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000029"), Nombre = "Inframundo y portales subterráneos", Localizacion = "Puertas del Hades, Ténaro", Latitud = 36.3853, Longitud = 22.4828 },
                    new { Id = Guid.Parse("aaaa0000-0000-0000-0000-000000000030"), Nombre = "Cimas montañosas y nubes de tormenta", Localizacion = "Montañas Cascadas", Latitud = 48.7768, Longitud = -121.8145 }
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
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000001"), NroCaracteristica = 1, Descripcion = "Entorno característico de montañas rocosas y acantilados en Cordillera del Olimpo" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000002"), NroCaracteristica = 1, Descripcion = "Entorno característico de bosques antiguos y densos en Bosques de Velen" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000003"), NroCaracteristica = 1, Descripcion = "Entorno característico de bosques frondosos y zonas volcánicas en Bosque Primigenio y Tierras Volcánicas" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000004"), NroCaracteristica = 1, Descripcion = "Entorno característico de el mundo del revés (dimensión oscura) en Hawkins (Dimensión Alterna)" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000005"), NroCaracteristica = 1, Descripcion = "Entorno característico de bosques templados y praderas altas en Tierras Altas de Escocia" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000006"), NroCaracteristica = 1, Descripcion = "Entorno característico de cavernas profundas y minas subterráneas en Minas de Moria" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000007"), NroCaracteristica = 1, Descripcion = "Entorno característico de pantanos y lagos cenagosos en Lago Lerna, Argólida" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000008"), NroCaracteristica = 1, Descripcion = "Entorno característico de mazmorras y cavernas subterráneas en Infraoscuridad Profunda" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000009"), NroCaracteristica = 1, Descripcion = "Entorno característico de tierras heladas y bosques boreales en Jötunheim Boreal" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000010"), NroCaracteristica = 1, Descripcion = "Entorno característico de ruinas antiguas, desiertos y alcantarillas en Cámara Subterránea Antigua" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000011"), NroCaracteristica = 1, Descripcion = "Entorno característico de regiones volcánicas y laderas montañosas en Monte Quimera, Licia" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000012"), NroCaracteristica = 1, Descripcion = "Entorno característico de yermos radiactivos y ruinas urbanas en Yermo Capital" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000013"), NroCaracteristica = 1, Descripcion = "Entorno característico de bosques nevados y taigas en Bosques Boreales de Ontario" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000014"), NroCaracteristica = 1, Descripcion = "Entorno característico de selvas húmedas y bosques nocturnos en Selva Inundada" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000015"), NroCaracteristica = 1, Descripcion = "Entorno característico de océanos profundos y mares abiertos en Mar de Noruega" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000016"), NroCaracteristica = 1, Descripcion = "Entorno característico de desiertos áridos y selvas remotas en Meseta Persa" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000017"), NroCaracteristica = 1, Descripcion = "Entorno característico de praderas, bosques y llanuras abiertas en Praderas de Midgar" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000018"), NroCaracteristica = 1, Descripcion = "Entorno característico de criptas, ruinas y cementerios en Cripta Real de Wyzima" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000019"), NroCaracteristica = 1, Descripcion = "Entorno característico de mazmorras, castillos y cámaras del tesoro en Bóvedas de Waterdeep" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000020"), NroCaracteristica = 1, Descripcion = "Entorno característico de fosas abisales y océanos profundos en Cráter Oceánico 4546B" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000021"), NroCaracteristica = 1, Descripcion = "Entorno característico de desiertos vastos y llanuras áridas en Llanuras de Ivalice" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000022"), NroCaracteristica = 1, Descripcion = "Entorno característico de bosques sagrados y santuarios rurales en Santuarios de Kioto" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000023"), NroCaracteristica = 1, Descripcion = "Entorno característico de picos montañosos y cañones rocosos en Cañones de Valyria" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000024"), NroCaracteristica = 1, Descripcion = "Entorno característico de cuevas oscuras y bosques sombríos en Cavernas de Bretaña" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000025"), NroCaracteristica = 1, Descripcion = "Entorno característico de instalaciones abandonadas y zonas urbanas en ruinas en Ciudad 17" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000026"), NroCaracteristica = 1, Descripcion = "Entorno característico de zonas rurales, matorrales y campos de pastoreo en Campos de Puerto Rico y Norte Argentino" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000027"), NroCaracteristica = 1, Descripcion = "Entorno característico de lagos, ríos y estanques profundos en Lago Ness, Escocia" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000028"), NroCaracteristica = 1, Descripcion = "Entorno característico de cumbres montañosas y bosques húmedos en Picos Brumosos" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000029"), NroCaracteristica = 1, Descripcion = "Entorno característico de inframundo y portales subterráneos en Puertas del Hades, Ténaro" },
                    new { IdHabitat = Guid.Parse("aaaa0000-0000-0000-0000-000000000030"), NroCaracteristica = 1, Descripcion = "Entorno característico de cimas montañosas y nubes de tormenta en Montañas Cascadas" }
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
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000001"), Nombre = "Grifo", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000001") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000002"), Nombre = "Leshen", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000002") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000003"), Nombre = "Rathalos", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000003") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000004"), Nombre = "Demogorgon", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000004") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000005"), Nombre = "Hipogrifo", Peligrosidad = "Media", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000005") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000006"), Nombre = "Balrog", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000006") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000007"), Nombre = "Hidra de Lerna", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000007") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000008"), Nombre = "Beholder (Contemplador)", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000008") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000009"), Nombre = "Fenrir", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000009") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000010"), Nombre = "Basilisco", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000010") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000011"), Nombre = "Quimera", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000011") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000012"), Nombre = "Deathclaw (Sanguinario)", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000012") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000013"), Nombre = "Wendigo", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000013") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000014"), Nombre = "Nargacuga", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000014") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000015"), Nombre = "Kraken", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000015") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000016"), Nombre = "Mantícora", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000016") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000017"), Nombre = "Chocobo", Peligrosidad = "Baja", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000017") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000018"), Nombre = "Striga", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000018") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000019"), Nombre = "Mimic (Mímico)", Peligrosidad = "Media", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000019") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000020"), Nombre = "Leviatán", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000020") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000021"), Nombre = "Behemoth", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000021") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000022"), Nombre = "Kitsune", Peligrosidad = "Media", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000022") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000023"), Nombre = "Wyvern", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000023") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000024"), Nombre = "Cockatrice", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000024") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000025"), Nombre = "Headcrab", Peligrosidad = "Media", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000025") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000026"), Nombre = "Chupacabras", Peligrosidad = "Media", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000026") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000027"), Nombre = "Kelpie", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000027") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000028"), Nombre = "Zinogre", Peligrosidad = "Muy Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000028") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000029"), Nombre = "Cerbero", Peligrosidad = "Extrema", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000029") },
                    new { Id = Guid.Parse("11111111-0000-0000-0000-000000000030"), Nombre = "Thunderbird (Ave del Trueno)", Peligrosidad = "Alta", Estado = "aprobado", IdHabitat = (Guid?)Guid.Parse("aaaa0000-0000-0000-0000-000000000030") }
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
                    new { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Correo = "admin@bestiario.com", Contrasenia = "password", Dni = "12345678", Nombre = "Admin", Apellido = "Investigador" },
                    new { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Correo = "geraldo@kaer.morhen", Contrasenia = "password", Dni = "40112233", Nombre = "Geraldo", Apellido = "Rivia" }
                );
            });

            modelBuilder.Entity<Lector>(entity =>
            {
                entity.HasData(
                    new { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Correo = "lector@bestiario.com", Contrasenia = "password" }
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
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_01_grifo" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_02_leshen" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_03_rathalos" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_04_demogorgon" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_05_hipogrifo" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_06_balrog" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_07_hidra_de_lerna" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_08_beholder" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_09_fenrir" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_10_basilisco" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_11_quimera" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_12_deathclaw" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_13_wendigo" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_14_nargacuga" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_15_kraken" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_16_manticora" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_17_chocobo" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_18_striga" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_19_mimic" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_20_leviatan" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_21_behemoth" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_22_kitsune" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_23_wyvern" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_24_cockatrice" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_25_headcrab" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_26_chupacabras" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_27_kelpie" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_28_zinogre" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_29_cerbero" },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroRegistro = 1, FechaAprobacion = (DateTime?)new DateTime(2026, 1, 15, 10, 0, 0), FechaBaja = (DateTime?)null, IdUsuarioPublicador = Guid.Parse("99999999-9999-9999-9999-999999999999"), IdInvestigadorAprobador = (Guid?)Guid.Parse("99999999-9999-9999-9999-999999999999"), Estado = "aprobado", MainPicture = "bestiario_30_thunderbird" }
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
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Grifo es una criatura perteneciente a Mitología griega, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en montañas rocosas y acantilados (Cordillera del Olimpo)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Leshen es una criatura perteneciente a The Witcher, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en bosques antiguos y densos (Bosques de Velen)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Rathalos es una criatura perteneciente a Monster Hunter, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en bosques frondosos y zonas volcánicas (Bosque Primigenio y Tierras Volcánicas)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Demogorgon es una criatura perteneciente a Stranger Things, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en el mundo del revés (dimensión oscura) (Hawkins (Dimensión Alterna))." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Hipogrifo es una criatura perteneciente a Harry Potter / Mitología, catalogada con peligrosidad media." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en bosques templados y praderas altas (Tierras Altas de Escocia)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Balrog es una criatura perteneciente a El Señor de los Anillos, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en cavernas profundas y minas subterráneas (Minas de Moria)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Hidra de Lerna es una criatura perteneciente a Mitología griega, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en pantanos y lagos cenagosos (Lago Lerna, Argólida)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Beholder (Contemplador) es una criatura perteneciente a Dungeons & Dragons, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en mazmorras y cavernas subterráneas (Infraoscuridad Profunda)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Fenrir es una criatura perteneciente a Mitología nórdica, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en tierras heladas y bosques boreales (Jötunheim Boreal)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Basilisco es una criatura perteneciente a Mitología europea / Harry Potter, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en ruinas antiguas, desiertos y alcantarillas (Cámara Subterránea Antigua)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Quimera es una criatura perteneciente a Mitología griega, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en regiones volcánicas y laderas montañosas (Monte Quimera, Licia)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Deathclaw (Sanguinario) es una criatura perteneciente a Fallout, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en yermos radiactivos y ruinas urbanas (Yermo Capital)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Wendigo es una criatura perteneciente a Folclore algonquino, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en bosques nevados y taigas (Bosques Boreales de Ontario)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Nargacuga es una criatura perteneciente a Monster Hunter, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en selvas húmedas y bosques nocturnos (Selva Inundada)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Kraken es una criatura perteneciente a Mitología escandinava, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en océanos profundos y mares abiertos (Mar de Noruega)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Mantícora es una criatura perteneciente a Mitología persa, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en desiertos áridos y selvas remotas (Meseta Persa)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Chocobo es una criatura perteneciente a Final Fantasy, catalogada con peligrosidad baja." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en praderas, bosques y llanuras abiertas (Praderas de Midgar)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Striga es una criatura perteneciente a The Witcher, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en criptas, ruinas y cementerios (Cripta Real de Wyzima)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Mimic (Mímico) es una criatura perteneciente a Dungeons & Dragons, catalogada con peligrosidad media." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en mazmorras, castillos y cámaras del tesoro (Bóvedas de Waterdeep)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Leviatán es una criatura perteneciente a Mitología bíblica / Subnautica, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en fosas abisales y océanos profundos (Cráter Oceánico 4546B)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Behemoth es una criatura perteneciente a Mitología bíblica / Final Fantasy, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en desiertos vastos y llanuras áridas (Llanuras de Ivalice)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Kitsune es una criatura perteneciente a Mitología japonesa, catalogada con peligrosidad media." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en bosques sagrados y santuarios rurales (Santuarios de Kioto)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Wyvern es una criatura perteneciente a Fantasía medieval, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en picos montañosos y cañones rocosos (Cañones de Valyria)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Cockatrice es una criatura perteneciente a Mitología medieval, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en cuevas oscuras y bosques sombríos (Cavernas de Bretaña)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Headcrab es una criatura perteneciente a Half-Life, catalogada con peligrosidad media." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en instalaciones abandonadas y zonas urbanas en ruinas (Ciudad 17)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Chupacabras es una criatura perteneciente a Folclore latinoamericano, catalogada con peligrosidad media." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en zonas rurales, matorrales y campos de pastoreo (Campos de Puerto Rico y Norte Argentino)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Kelpie es una criatura perteneciente a Folclore celta, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en lagos, ríos y estanques profundos (Lago Ness, Escocia)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Zinogre es una criatura perteneciente a Monster Hunter, catalogada con peligrosidad muy alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en cumbres montañosas y bosques húmedos (Picos Brumosos)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Cerbero es una criatura perteneciente a Mitología griega, catalogada con peligrosidad extrema." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en inframundo y portales subterráneos (Puertas del Hades, Ténaro)." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroRegistro = 1, NroContenido = 1, Titulo = "Origen y Clasificación", Contenido = "Thunderbird (Ave del Trueno) es una criatura perteneciente a Mitología nativa americana, catalogada con peligrosidad alta." },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroRegistro = 1, NroContenido = 2, Titulo = "Hábitat Natural", Contenido = "Habita principalmente en cimas montañosas y nubes de tormenta (Montañas Cascadas)." }
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
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_01_grifo_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_01_grifo_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_01_grifo_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_01_grifo_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000001"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_01_grifo_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_02_leshen_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_02_leshen_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_02_leshen_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_02_leshen_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000002"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_02_leshen_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_03_rathalos_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_03_rathalos_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_03_rathalos_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_03_rathalos_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000003"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_03_rathalos_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_04_demogorgon_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_04_demogorgon_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_04_demogorgon_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_04_demogorgon_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000004"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_04_demogorgon_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_05_hipogrifo_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_05_hipogrifo_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_05_hipogrifo_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_05_hipogrifo_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000005"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_05_hipogrifo_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_06_balrog_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_06_balrog_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_06_balrog_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_06_balrog_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000006"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_06_balrog_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_07_hidra_de_lerna_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_07_hidra_de_lerna_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_07_hidra_de_lerna_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_07_hidra_de_lerna_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000007"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_07_hidra_de_lerna_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_08_beholder_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_08_beholder_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_08_beholder_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_08_beholder_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000008"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_08_beholder_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_09_fenrir_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_09_fenrir_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_09_fenrir_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_09_fenrir_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000009"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_09_fenrir_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_10_basilisco_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_10_basilisco_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_10_basilisco_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_10_basilisco_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000010"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_10_basilisco_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_11_quimera_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_11_quimera_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_11_quimera_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_11_quimera_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000011"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_11_quimera_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_12_deathclaw_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_12_deathclaw_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_12_deathclaw_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_12_deathclaw_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000012"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_12_deathclaw_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_13_wendigo_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_13_wendigo_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_13_wendigo_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_13_wendigo_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000013"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_13_wendigo_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_14_nargacuga_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_14_nargacuga_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_14_nargacuga_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_14_nargacuga_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000014"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_14_nargacuga_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_15_kraken_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_15_kraken_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_15_kraken_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_15_kraken_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000015"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_15_kraken_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_16_manticora_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_16_manticora_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_16_manticora_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_16_manticora_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000016"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_16_manticora_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_17_chocobo_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_17_chocobo_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_17_chocobo_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_17_chocobo_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000017"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_17_chocobo_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_18_striga_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_18_striga_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_18_striga_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_18_striga_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000018"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_18_striga_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_19_mimic_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_19_mimic_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_19_mimic_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_19_mimic_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000019"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_19_mimic_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_20_leviatan_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_20_leviatan_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_20_leviatan_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_20_leviatan_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000020"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_20_leviatan_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_21_behemoth_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_21_behemoth_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_21_behemoth_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_21_behemoth_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000021"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_21_behemoth_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_22_kitsune_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_22_kitsune_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_22_kitsune_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_22_kitsune_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000022"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_22_kitsune_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_23_wyvern_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_23_wyvern_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_23_wyvern_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_23_wyvern_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000023"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_23_wyvern_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_24_cockatrice_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_24_cockatrice_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_24_cockatrice_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_24_cockatrice_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000024"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_24_cockatrice_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_25_headcrab_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_25_headcrab_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_25_headcrab_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_25_headcrab_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000025"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_25_headcrab_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_26_chupacabras_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_26_chupacabras_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_26_chupacabras_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_26_chupacabras_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000026"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_26_chupacabras_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_27_kelpie_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_27_kelpie_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_27_kelpie_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_27_kelpie_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000027"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_27_kelpie_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_28_zinogre_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_28_zinogre_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_28_zinogre_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_28_zinogre_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000028"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_28_zinogre_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_29_cerbero_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_29_cerbero_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_29_cerbero_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_29_cerbero_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000029"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_29_cerbero_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroEvidencia = 1, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 11, 10, 30, 0), FileId = "bestiario_30_thunderbird_ev1", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroEvidencia = 2, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 12, 11, 30, 0), FileId = "bestiario_30_thunderbird_ev2", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroEvidencia = 3, Estado = "aprobado", FechaObtencion = new DateTime(2026, 2, 13, 12, 30, 0), FileId = "bestiario_30_thunderbird_ev3", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroEvidencia = 4, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 14, 13, 30, 0), FileId = "bestiario_30_thunderbird_ev4", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") },
                    new { IdBestia = Guid.Parse("11111111-0000-0000-0000-000000000030"), NroEvidencia = 5, Estado = "pendiente", FechaObtencion = new DateTime(2026, 2, 15, 14, 30, 0), FileId = "bestiario_30_thunderbird_ev5", IdTipoEvidencia = Guid.Parse("bbbb1111-1111-1111-1111-111111111111") }
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