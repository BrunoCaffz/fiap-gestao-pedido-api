using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class Coleta
    {
        public int Id { get; set; }

        [Required]
        public int ResiduoId { get; set; }

        [Required]
        public int PontoColetaId { get; set; }

        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int ColetorId { get; set; }

        [Required]
        public double PesoKg { get; set; }

        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        //Relacionamentos
        public Residuo Residuo { get; set; }
        public PontoColeta PontoColeta { get; set; }
        public Veiculo Veiculo { get; set; }
        public Coletor Coletor { get; set; }
    }
}
