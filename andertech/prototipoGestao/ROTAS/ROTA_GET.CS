using prototipoGestao.Data;//usings pra ligar com os namespace
using prototipoGestao.Models;
using Microsoft.EntityFrameworkCore;

namespace prototipoGestao.Rotas;

public static class Rota_GET
{
    public static void MapGetRoutes(this WebApplication app)
    {
        // isso aq e pra puxar dos dispositos
        app.MapGet("/dispositivos", async (AppDbContext db) =>
        {
            var lista = await db.Dispositivos.ToListAsync();
            return Results.Ok(lista);
        });

        // aqui tmbm funciona por id
        app.MapGet("/dispositivos/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var d = await db.Dispositivos.FindAsync(id);
            return d is not null ? Results.Ok(d) : Results.NotFound();
        });
    }
}
