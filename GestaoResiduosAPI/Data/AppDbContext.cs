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

        public DbSet<Coleta> Coletas { get; set; }
        public DbSet<PontoColeta> PontosColeta { get; set; }
        public DbSet<Residuo> Residuos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Coletor> Coletores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Residuo>().HasData(
                new Residuo
                {
                    Id = 1,
                    Tipo = "Plástico",
                    Descricao = "Resíduo plástico em geral"
                }
            );

            modelBuilder.Entity<Veiculo>().HasData(
                new Veiculo
                {
                    Id = 1,
                    Placa = "ABC-1234",
                    Modelo = "Caminhão 3/4",   // <-- preenche o required
                    Ativo = true               // opcional, mas já deixa explícito
                }
            );

            modelBuilder.Entity<Coletor>().HasData(
                new Coletor
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Ativo = true
                }
            );

            modelBuilder.Entity<PontoColeta>().HasData(
                new PontoColeta
                {
                    Id = 1,
                    Nome = "Ponto Central",
                    LimiteKg = 100,
                    Latitude = 0,   // default, ok
                    Longitude = 0   // default, ok
                }
            );
        }
    }
}
