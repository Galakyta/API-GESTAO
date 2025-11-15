using prototipoGestao.Data;//usings pra ligar com os namespace

namespace prototipoGestao.Rotas;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        // isso aq e o delete por id pra nn dar ruim e deletar algo sem querer precisa ser bem especifico
        app.MapDelete("/dispositivos/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var dispositivo = await db.Dispositivos.FindAsync(id);
            if (dispositivo is null)
                return Results.NotFound();

            db.Dispositivos.Remove(dispositivo);
            await db.SaveChangesAsync();
            return Results.NoContent(); // e pra retornar nada se for
        });
    }
}
