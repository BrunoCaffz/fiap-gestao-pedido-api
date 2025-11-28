namespace GestaoResiduosAPI.ViewModels
{
    public class RotaOtimizadaResponse
    {
        public double DistanciaTotalKm { get; set; }
        public int EstimativaTempoMin { get; set; }
        public List<int> OrdemColeta { get; set; }
    }
}
