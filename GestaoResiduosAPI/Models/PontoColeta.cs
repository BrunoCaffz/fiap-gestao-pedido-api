using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class PontoColeta
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nome { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Limite máximo de lixo antes de gerar alerta
        public double LimiteKg { get; set; }
    }
}
