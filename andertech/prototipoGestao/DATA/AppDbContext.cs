using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using prototipoGestao.Models;
namespace prototipoGestao.Data;
using prototipoGestao.Models;


public class AppDbContext : DbContext //classe que herda o DbContext pra lidar com os dados depois, db vindo EFC

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {} //config base do dbcontext
    public DbSet<Dispositivo> Dispositivos => Set<Dispositivo>(); //cria uma tabela chamada dispo baseada na classe dispositivos

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Dispositivo>(e =>
        {
            e.ToTable("Dispositivos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Fabricante).IsRequired();
            e.Property(x => x.Empresa).IsRequired();
            e.Property(x => x.Local).IsRequired();
            e.Property(x => x.Tipo).IsRequired();
            e.Property(x => x.Ativo).HasDefaultValue(true);
        });
    } //config do banco
}