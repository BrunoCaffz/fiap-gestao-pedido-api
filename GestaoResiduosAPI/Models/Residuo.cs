using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.Models
{
    public class Residuo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } 

        [MaxLength(200)]
        public string Descricao { get; set; }
    }
}
