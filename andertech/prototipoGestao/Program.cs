using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using prototipoGestao.Data;
using prototipoGestao.Rotas;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
//config do sqlite, so conectando o banco com a aplicacao
builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// conversao pros enum funcionarem no json como o id
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("dev", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
//comando pra destravar pra ter acesso completo

// config do swagger pra teste e usar as rotas
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Financeira",
        Version = "v1",
        Description = "ela serve pra gerenciar fontes de renda extra dentro de centros comerciais como shoppings e negocios alternativos dentro de um mercado"
    });
});

var app = builder.Build();

// isso aplica as migrations da databse pro copilador n reclamar
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Financeira v1");
        c.DocExpansion(DocExpansion.None);
    });

    app.UseCors("dev");
}

// isso aqui pega as rota pra execucao
app.MapGetRoutes(); //getter

app.MapPostRoutes(); //post

app.MapPutRoutes(); //setter

app.MapDeleteRoutes(); //delete

app.Run();
