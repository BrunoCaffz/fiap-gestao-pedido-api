using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class Coletor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
