using Microsoft.EntityFrameworkCore;
using prototipoGestao.Models;

namespace prototipoGestao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //config basica do db
        public DbSet<Dispositivo> Dispositivos => Set<Dispositivo>();
        public DbSet<Despesa> Despesas => Set<Despesa>();
        
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

            b.Entity<Despesa>(e =>
            {
                e.ToTable("Despesas"); //iindica a table pra onde isso aqui vai no sql
                e.HasKey(x => x.Id); //torna isso aqui a pk
                e.Property(x => x.Nome).IsRequired(); //is required quer dizer que o campo é obrigatorio, mas pk e data nn precisa
                e.Property(x => x.Data).IsRequired();
                e.Property(x => x.Categoria).IsRequired(false); // se nn for requirido deixa assim pra ser explicito
                e.Property(x => x.Valor).HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired(); //define o tipo default value e se e requerido
                e.Property(x => x.Imposto).HasColumnType("decimal(18,2)").HasDefaultValue(0).IsRequired();
            });
            //vai servir pra adicionar as propiedades da tabela
        }
    }


}
