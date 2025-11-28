using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosAPI.ViewModels
{
    public class RotaOtimizadaRequest
    {
        [Required]
        public List<PontoRotaViewModel> Pontos { get; set; }
    }

    public class PontoRotaViewModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
