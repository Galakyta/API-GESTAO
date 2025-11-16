using prototipoGestao.Data;//usings pra ligar com os namespace
using prototipoGestao.Models;
using Microsoft.EntityFrameworkCore;

namespace prototipoGestao.Rotas;

public static class Rota_GET_RELATORIO
{
    public static void MapGetRelatorioRoutes(this WebApplication app)
    {
        // isso aq e pra puxar das despesas
        app.MapGet("/relatorio", async (AppDbContext db) =>
        {
            
            var despesas = await db.Despesas.ToListAsync();
            var dispositivos = await db.Dispositivos.ToListAsync();
            return Results.Ok(new
            {
            TotalDispositivos = dispositivos.Count,
            TotalDespesas = despesas.Count,
            BalancoGeral =  dispositivos.Sum(d => d.Lucro - d.CustoDeOperacao) - despesas.Sum(d => d.Valor + d.Imposto)
            });
        });
    }
}
