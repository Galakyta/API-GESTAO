using prototipoGestao.Data;
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_POST_Despesas
{
    public static void MapDespesaPostRoutes(this WebApplication app)
    {
        // esse post e pra itens unitarios e vai servir pra ir um por um, mas eu tie q adicionar um bglh dps pra colocar o banco q eu fiz em outra aba rodar por aqui e enviar pro banco
        app.MapPost("/despesas", async (Despesa novo, AppDbContext db) =>
        {
            db.Despesas.Add(novo);
            await db.SaveChangesAsync();
            return Results.Created($"/despesas/{novo.Id}", novo);
        });

    }
}
