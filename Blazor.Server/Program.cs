using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<Blazor.Server.Services.AuthService>();
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Server.Circuits.CircuitHandler, Blazor.Server.Services.AuthCircuitHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapGet("/descargar-evidencia", async (string url, string? nombre) =>
{
    if (string.IsNullOrWhiteSpace(url) ||
        !Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
        uri.Scheme != Uri.UriSchemeHttps ||
        !uri.Host.EndsWith("cloudinary.com", StringComparison.OrdinalIgnoreCase))
    {
        return Results.BadRequest("URL de evidencia no válida.");
    }

    using var http = new HttpClient();
    var response = await http.GetAsync(uri);
    if (!response.IsSuccessStatusCode)
    {
        return Results.StatusCode((int)response.StatusCode);
    }

    var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
    var fileName = string.IsNullOrWhiteSpace(nombre)
        ? Path.GetFileName(uri.LocalPath)
        : nombre;
    if (string.IsNullOrWhiteSpace(fileName))
    {
        fileName = "evidencia";
    }

    var bytes = await response.Content.ReadAsByteArrayAsync();
    return Results.File(bytes, contentType, fileName);
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
