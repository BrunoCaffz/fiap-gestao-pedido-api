namespace GestaoResiduosAPI.ViewModels
{
    public class ColetaListViewModel
    {
        public int Id { get; set; }
        public string Residuo { get; set; }
        public string Ponto { get; set; }
        public string Veiculo { get; set; }
        public string Coletor { get; set; }
        public double PesoKg { get; set; }
        public DateTime DataHora { get; set; }
    }
}
