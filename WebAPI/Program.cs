using Application.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bestiario API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticación JWT usando el esquema Bearer. Ingrese 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<TPIContext>();

// Token Service
builder.Services.AddScoped<ITokenService, TokenService>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "BestiarioClaveSecretaSuperSeguraParaFirmaJwt2026!#";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "BestiarioAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "BestiarioClients";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Categorias y Noticias
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();

// Habitats, Tipos de Evidencia, Evidencias y Comentarios
builder.Services.AddScoped<IHabitatRepository, HabitatRepository>();
builder.Services.AddScoped<IHabitatService, HabitatService>();
builder.Services.AddScoped<ITipoEvidenciaRepository, TipoEvidenciaRepository>();
builder.Services.AddScoped<ITipoEvidenciaService, TipoEvidenciaService>();
builder.Services.AddScoped<IEvidenciaRepository, EvidenciaRepository>();
builder.Services.AddScoped<IEvidenciaService, EvidenciaService>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

// Bestias, Registros y Usuarios
builder.Services.AddScoped<IBestiaRepository, BestiaRepository>();
builder.Services.AddScoped<IBestiaService, BestiaService>();
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();
builder.Services.AddScoped<IRegistroService, RegistroService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapCategoriaEndpoints();
app.MapNoticiaEndpoints();
app.MapHabitatEndpoints();
app.MapTipoEvidenciaEndpoints();
app.MapEvidenciaEndpoints();
app.MapComentarioEndpoints();
app.MapBestiaEndpoints();
app.MapRegistroEndpoints();
app.MapUsuarioEndpoints();

app.Run();