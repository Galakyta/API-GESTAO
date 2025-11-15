using Microsoft.EntityFrameworkCore;
using prototipoGestao.Models;

namespace prototipoGestao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //config basica do db
        public DbSet<Dispositivo> Dispositivos => Set<Dispositivo>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Dispositivo>(e =>
            {
                e.ToTable("Dispositivos");
                e.HasKey(x => x.Id);
                //isso aq e o pao com manteiga pra definir as keys, o id vai ser o identificador
                e.Property(x => x.Fabricante).IsRequired();
                e.Property(x => x.Empresa).IsRequired();
                e.Property(x => x.Local).IsRequired();
                e.Property(x => x.Tipo).IsRequired();
                //na pratica da par mudar esse empresa is required pra n precisar, maaaaaaas eu recomendo so colocar "propio" se for assim
                e.Property(x => x.Ativo).HasDefaultValue(true);

                // Novas propriedades financeiras
                e.Property(x => x.Lucro)
                    .HasColumnType("decimal(18,2)")  // define precisão e escala
                    .HasDefaultValue(0);

                e.Property(x => x.CustoDeOperacao)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
            });
        }
    }
}
