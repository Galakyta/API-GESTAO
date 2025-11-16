using prototipoGestao.Data; //usings pra ligar com os namespace
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_PUT_Despesas
{
    public static void MapDespesasPutRoutes(this WebApplication app)
    {
        //isso aqui vai server pra atualizar qualquer campo pelo id, tmbm n e pra funcionar sem o id

        //nn trocar nesse /despesas o guid por id dnv, depois vem o tipo,nunca atualizar a pk
        app.MapPut("/despesas/{id:int}", async (int id, Despesa atualizado, AppDbContext db) =>
        {
            var d = await db.Despesas.FindAsync(id);
            if (d is null) return Results.NotFound();


            
            //se nn tiver atualizando confere isso
            d.Nome = atualizado.Nome;
            d.Categoria = atualizado.Categoria;
            d.Data = atualizado.Data;
            d.Valor= atualizado.Valor;
            d.Imposto= atualizado.Imposto;
            //if (string.IsNullOrWhiteSpace(atualizado.Nome))
            //return Results.BadRequest("Nome é obrigatório");

            //if (atualizado.Valor < 0)
            //return Results.BadRequest("Valor não pode ser negativo");

            await db.SaveChangesAsync();
            return Results.Ok(d);
        });
    }
}
