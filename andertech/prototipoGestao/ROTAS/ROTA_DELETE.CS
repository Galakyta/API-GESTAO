using prototipoGestao.Data;

namespace prototipoGestao.Rotas;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        // DELETE /dispositivos/{id}
        app.MapDelete("/dispositivos/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var d = await db.Dispositivos.FindAsync(id);
            if (d is null) return Results.NotFound();

            db.Dispositivos.Remove(d);
            await db.SaveChangesAsync();
            return Results.NoContent(); // 204
        });
    }
}
