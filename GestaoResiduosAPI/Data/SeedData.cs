using GestaoResiduosAPI.Models;

namespace GestaoResiduosAPI.Data
{
    public static class SeedData
    {
        public static void Seed(AppDbContext db)
        {
            if (!db.Residuos.Any())
            {
                db.Residuos.Add(new Residuo
                {
                    Id = 1,
                    Tipo = "Plástico",
                    Descricao = "Resíduo plástico em geral"
                });
            }

            if (!db.Veiculos.Any())
            {
                db.Veiculos.Add(new Veiculo
                {
                    Id = 1,
                    Placa = "ABC-1234",
                    Modelo = "Caminhão 3/4",
                    Ativo = true
                });
            }

            if (!db.Coletores.Any())
            {
                db.Coletores.Add(new Coletor
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Ativo = true
                });
            }

            if (!db.PontosColeta.Any())
            {
                db.PontosColeta.Add(new PontoColeta
                {
                    Id = 1,
                    Nome = "Ponto Central",
                    LimiteKg = 100,
                    Latitude = 0,
                    Longitude = 0
                });
            }

            db.SaveChanges();
        }
    }
}