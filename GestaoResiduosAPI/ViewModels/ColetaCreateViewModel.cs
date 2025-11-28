using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.ViewModels
{
    public class ColetaCreateViewModel
    {
        [Required]
        public int ResiduoId { get; set; }

        [Required]
        public int PontoColetaId { get; set; }

        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int ColetorId { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Peso deve ser maior que zero.")]
        public double PesoKg { get; set; }
    }
}
