using GestaoResiduosAPI.Services;
using GestaoResiduosAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly RotaService _rotaService;

        public RotaController(RotaService rotaService)
        {
            _rotaService = rotaService;
        }

        // POST /rota
        [HttpPost]
        public IActionResult Gerar([FromBody] RotaOtimizadaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rota = _rotaService.GerarRotaOtimizada(request);
            return Ok(rota);
        }
    }
}
