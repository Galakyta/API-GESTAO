using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using prototipoGestao.Data;
using prototipoGestao.Rotas;
using Swashbuckle.AspNetCore.SwaggerUI;




var builder = WebApplication.CreateBuilder(args);

// banco SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Financeira",
        Version = "v1",
        Description = "Gerenciamento de dispositivos e receitas em centros comerciais"
    });
});

var app = builder.Build();

// Swagger em ambiente de dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Financeira v1");
        c.DocExpansion(DocExpansion.None);
    });
}

// Ativa as rotas GET
app.MapGetRoutes();
app.MapPostRoutes();


app.Run();
