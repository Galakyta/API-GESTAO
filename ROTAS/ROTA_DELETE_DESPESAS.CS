using prototipoGestao.Data;
using prototipoGestao.Models;//usings pra ligar com os namespace

namespace prototipoGestao.Rotas;

public static class Rota_DELETE_Despesas
{
    public static void MapDespesasDeleteRoutes(this WebApplication app)
    {
        // isso aq e o delete por id pra nn dar ruim e deletar algo sem querer precisa ser bem especifico
        app.MapDelete("/despesas/{id:int}", async (int id, AppDbContext db) =>
        {
            var despesa = await db.Despesas.FindAsync(id);
            if (despesa is null)
                return Results.NotFound();

            db.Despesas.Remove(despesa);
            await db.SaveChangesAsync();
            return Results.NoContent(); // e pra retornar nada se for
        });
    }
}
