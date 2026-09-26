using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            app.MapGet("/usuarios", async (IUsuarioService usuarioService) =>
            {
                var dtos = await usuarioService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllUsuarios")
            .Produces<List<UsuarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/usuarios/{id}", async (Guid id, IUsuarioService usuarioService) =>
            {
                var dto = await usuarioService.GetAsync(id);
                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/usuarios/investigadores", async (InvestigadorDTO dto, IUsuarioService usuarioService) =>
            {
                try
                {
                    var result = await usuarioService.AddInvestigadorAsync(dto);
                    return Results.Created($"/usuarios/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddInvestigador")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/usuarios/lectores", async (LectorDTO dto, IUsuarioService usuarioService) =>
            {
                try
                {
                    var result = await usuarioService.AddLectorAsync(dto);
                    return Results.Created($"/usuarios/{result.Id}", result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddLector")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/usuarios/login", async (LoginRequestDTO request, IUsuarioService usuarioService) =>
            {
                var user = await usuarioService.ValidarCredencialesAsync(request.Correo, request.Contrasenia);
                if (user == null)
                    return Results.Unauthorized();

                return Results.Ok(user);
            })
            .WithName("LoginUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
