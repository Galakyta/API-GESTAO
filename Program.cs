using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using prototipoGestao.Data;
using prototipoGestao.Rotas;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("dev", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

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

// procura automaticamente index.html em wwwroot
app.UseDefaultFiles();

// habilita servir HTML / CSS / JS da pasta wwwroot
app.UseStaticFiles();

// SUAS ROTAS DA API
app.MapGetRoutes();
app.MapPostRoutes();
app.MapPutRoutes();
app.MapDeleteRoutes();

app.Run();



// Se quiser servir o index.html ao acessar /
app.MapGet("/", async context =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});

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


app.Run();
