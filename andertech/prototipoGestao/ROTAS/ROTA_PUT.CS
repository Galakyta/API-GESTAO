using prototipoGestao.Data;
using prototipoGestao.Models;

namespace prototipoGestao.Rotas;

public static class Rota_PUT
{
    public static void MapPutRoutes(this WebApplication app)
    {
        // PUT /dispositivos/{id}
        app.MapPut("/dispositivos/{id:guid}", async (Guid id, Dispositivo dto, AppDbContext db) =>
        {
            // validações simples
            if (string.IsNullOrWhiteSpace(dto.Fabricante))
                return Results.BadRequest("Campo 'Fabricante' é obrigatório.");
            if (string.IsNullOrWhiteSpace(dto.Empresa))
                return Results.BadRequest("Campo 'Empresa' é obrigatório.");
            if (string.IsNullOrWhiteSpace(dto.Local))
                return Results.BadRequest("Campo 'Local' é obrigatório.");

            var d = await db.Dispositivos.FindAsync(id);
            if (d is null) return Results.NotFound();

            // atualiza campos
            d.Fabricante = dto.Fabricante;
            d.Empresa     = dto.Empresa;
            d.Local       = dto.Local;
            d.Tipo        = dto.Tipo;
            d.Ativo       = dto.Ativo;

            await db.SaveChangesAsync();
            return Results.Ok(d);
        });
    }
}
