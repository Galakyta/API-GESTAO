using prototipoGestao.Data;
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        // esse post e pra itens unitarios e vai servir pra ir um por um, mas eu tie q adicionar um bglh dps pra colocar o banco q eu fiz em outra aba rodar por aqui e enviar pro banco
        app.MapPost("/dispositivos", async (Dispositivo novo, AppDbContext db) =>
        {
            if (novo.Id == Guid.Empty) novo.Id = Guid.NewGuid();
            db.Dispositivos.Add(novo);
            await db.SaveChangesAsync();
            return Results.Created($"/dispositivos/{novo.Id}", novo);
        });

        // esse e so um post com for que deixa adicionar varios itens ao mesmo tempo pra nn precisar adicionar um por um mas sem usar seed, se precisar codar pra ele se auto seedar
        //dps vai ter q mudar e resolver
        app.MapPost("/dispositivos/bulk", async (List<Dispositivo> itens, AppDbContext db) =>
        {
            foreach (var d in itens)
                if (d.Id == Guid.Empty) d.Id = Guid.NewGuid(); //funcao especifica pros id
            await db.Dispositivos.AddRangeAsync(itens);
            await db.SaveChangesAsync();
            return Results.Created("/dispositivos", new { inseridos = itens.Count }); //se der certo vai mostarar quantos foram inseridos mas talvez o contador sasia com -1 do q tem de vdd
        });
    }
}
