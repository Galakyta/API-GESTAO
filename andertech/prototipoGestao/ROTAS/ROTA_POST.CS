using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using prototipoGestao.Data;
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost("/dispositivos", async Task<Results<Created<Dispositivo>, BadRequest<string>>>
            (AppDbContext db, Dispositivo payload) =>
        {
            if (string.IsNullOrWhiteSpace(payload.Fabricante))
                return TypedResults.BadRequest("Campo 'Fabricante' é obrigatório.");
            if (string.IsNullOrWhiteSpace(payload.Empresa))
                return TypedResults.BadRequest("Campo 'Empresa' é obrigatório.");
            if (string.IsNullOrWhiteSpace(payload.Local))
                return TypedResults.BadRequest("Campo 'Local' é obrigatório.");

            if (payload.Id == Guid.Empty)
                payload.Id = Guid.NewGuid();

            db.Dispositivos.Add(payload);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/dispositivos/{payload.Id}", payload);
        });
    }
}
