using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Placa { get; set; }

        [MaxLength(40)]
        public string Modelo { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
