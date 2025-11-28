namespace GestaoResiduosAPI.ViewModels
{
    public class AlertaViewModel
    {
        public int Id { get; set; }
        public string Ponto { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
    }
}
