using Application.Services;
using Data;
using System.Linq.Expressions;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TPIContext>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();

var app = builder.Build();

Console.ForegroundColor = ConsoleColor.Red;
Console.ResetColor();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoriaEndpoints();
app.MapNoticiaEndpoints();

app.Run();