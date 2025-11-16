using prototipoGestao.Data;//usings pra ligar com os namespace
using prototipoGestao.Models;
using Microsoft.EntityFrameworkCore;

namespace prototipoGestao.Rotas;

public static class Rota_GET_Maiores_Despesas
{
    public static void MapGetMaioresDespesasRoutes(this WebApplication app)
    {
        // isso aq e pra puxar das despesas
        app.MapGet("/despesas/maiores", async (AppDbContext db) =>
        {
            var lista = await db.Despesas.ToListAsync();

            var ordenada = lista.OrderByDescending(d => d.Valor).ToList();

            return Results.Ok(ordenada);
        });

    }
}