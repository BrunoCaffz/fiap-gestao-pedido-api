namespace GestaoResiduosAPI.ViewModels
{
    public class ColetaDetalheViewModel
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public double PesoKg { get; set; }

        public string Residuo { get; set; }
        public string PontoColeta { get; set; }
        public double LimiteKgPonto { get; set; }

        public string Veiculo { get; set; }
        public string Coletor { get; set; }

        public bool ExcedeuLimite { get; set; }
    }
}