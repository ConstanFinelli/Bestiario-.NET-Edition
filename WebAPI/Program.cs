using Application.Services;
using Data;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TPIContext>();

// Categorias y Noticias
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();

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

app.MapCategoriaEndpoints();
app.MapNoticiaEndpoints();
app.MapBestiaEndpoints();
app.MapRegistroEndpoints();
app.MapUsuarioEndpoints();

app.Run();