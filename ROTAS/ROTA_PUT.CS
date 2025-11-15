using prototipoGestao.Data; //usings pra ligar com os namespace
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_PUT
{
    public static void MapPutRoutes(this WebApplication app)
    {
        //isso aqui vai server pra atualizar qualquer campo pelo id, tmbm n e pra funcionar sem o id
        app.MapPut("/dispositivos/{id:guid}", async (Guid id, Dispositivo atualizado, AppDbContext db) =>
        {
            var d = await db.Dispositivos.FindAsync(id);
            if (d is null) return Results.NotFound();


            
            //se nn tiver atualizando confere isso
            d.Fabricante = atualizado.Fabricante;
            d.Empresa = atualizado.Empresa;
            d.Local = atualizado.Local;
            d.Tipo = atualizado.Tipo;
            d.Ativo = atualizado.Ativo;
            d.Lucro = atualizado.Lucro;
            d.CustoDeOperacao = atualizado.CustoDeOperacao;

            await db.SaveChangesAsync();
            return Results.Ok(d);
        });
    }
}
