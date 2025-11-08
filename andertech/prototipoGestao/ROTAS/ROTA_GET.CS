namespace prototipoGestao.Rotas;

using Microsoft.EntityFrameworkCore;
using prototipoGestao.Data;
using prototipoGestao.Models;


public static class Rota_GET
{
    public static void MapGetRoutes(this WebApplication app)  // <- tem "this"
    {
        app.MapGet("/dispositivos", async (AppDbContext db) =>
            Results.Ok(await db.Dispositivos.AsNoTracking().ToListAsync()));

        app.MapGet("/dispositivos/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var d = await db.Dispositivos.FindAsync(id);
            return d is null ? Results.NotFound() : Results.Ok(d);
        });
    }
}
