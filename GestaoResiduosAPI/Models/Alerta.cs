using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class Alerta
    {
        public int Id { get; set; }

        [Required]
        public int PontoColetaId { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        public PontoColeta PontoColeta { get; set; }
    }
}
