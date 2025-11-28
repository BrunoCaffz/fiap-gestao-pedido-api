using GestaoResiduosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly AlertaService _alertaService;

        public AlertasController(AlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        // GET /alertas
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var alertas = await _alertaService.ListarAsync();
            return Ok(alertas);
        }
    }
}
