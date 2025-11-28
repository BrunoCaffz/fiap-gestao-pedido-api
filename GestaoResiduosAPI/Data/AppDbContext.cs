using GestaoResiduosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabelas do banco
        public DbSet<Coleta> Coletas { get; set; }
        public DbSet<PontoColeta> PontosColeta { get; set; }
        public DbSet<Residuo> Residuos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Coletor> Coletores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aqui no futuro podemos configurar tabelas, chaves, índices e relacionamentos
        }
    }
}
