using prototipoGestao.Data;//usings pra ligar com os namespace
using prototipoGestao.Models;
using Microsoft.EntityFrameworkCore;

namespace prototipoGestao.Rotas;

public static class Rota_GET_LUCRO
{
    public static void MapGetLucroRoutes(this WebApplication app)
    {
        // isso aq e pra puxar das despesas
        app.MapGet("/dispositivos/LucroLiquido", async (AppDbContext db) =>
        {
            var lista = await db.Dispositivos.Select(d => new
            {
                d.Id,
                d.Fabricante,
                d.Ativo,
                d.Lucro,
                d.CustoDeOperacao,
                LucroLiquido = d.Lucro - d.CustoDeOperacao
            }).ToListAsync();
            return Results.Ok(lista);
        });

    }
}
