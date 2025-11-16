using prototipoGestao.Data;//usings pra ligar com os namespace
using prototipoGestao.Models;
using Microsoft.EntityFrameworkCore;

namespace prototipoGestao.Rotas;

public static class Rota_GET_Despesa
{
    public static void MapGetDespesasRoutes(this WebApplication app)
    {
        // isso aq e pra puxar das despesas
        app.MapGet("/despesas", async (AppDbContext db) =>
        {
            var lista = await db.Despesas.ToListAsync();
            return Results.Ok(lista);
        });

        // aqui tmbm funciona por id
        app.MapGet("/despesas/{id:int}", async (int id, AppDbContext db) =>
        {
            var d = await db.Despesas.FindAsync(id);
            return d is not null ? Results.Ok(d) : Results.NotFound();
        });
    }
}
