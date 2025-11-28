using GestaoResiduosAPI.ViewModels;

namespace GestaoResiduosAPI.Services
{
    public class RotaService
    {
        public RotaOtimizadaResponse GerarRotaOtimizada(RotaOtimizadaRequest request)
        {
            var pontos = request.Pontos;

            if (pontos == null || pontos.Count == 0)
                throw new Exception("Nenhum ponto informado.");

            // Nearest Neighbor
            var ordem = new List<int>();
            var restantes = pontos.ToList();

            var atual = restantes.First();
            restantes.Remove(atual);
            ordem.Add(atual.Id);

            while (restantes.Count > 0)
            {
                var maisProximo = restantes
                    .OrderBy(p => Dist(atual, p))
                    .First();

                ordem.Add(maisProximo.Id);
                restantes.Remove(maisProximo);
                atual = maisProximo;
            }

            // Cálculo aproximado de distância total
            double distanciaTotal = 0;
            for (int i = 0; i < ordem.Count - 1; i++)
            {
                var p1 = pontos.First(p => p.Id == ordem[i]);
                var p2 = pontos.First(p => p.Id == ordem[i + 1]);
                distanciaTotal += Dist(p1, p2);
            }

            return new RotaOtimizadaResponse
            {
                OrdemColeta = ordem,
                DistanciaTotalKm = Math.Round(distanciaTotal, 2),
                EstimativaTempoMin = (int)(distanciaTotal * 3) // simples: média 20 km/h
            };
        }

        private double Dist(PontoRotaViewModel a, PontoRotaViewModel b)
        {
            return Math.Sqrt(
                Math.Pow(a.Latitude - b.Latitude, 2) +
                Math.Pow(a.Longitude - b.Longitude, 2)
            ) * 111; // Conversão aproximada para km
        }
    }
}
