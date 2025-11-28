using GestaoResiduosAPI.Services;
using GestaoResiduosAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColetasController : ControllerBase
    {
        private readonly ColetaService _coletaService;

        public ColetasController(ColetaService coletaService)
        {
            _coletaService = coletaService;
        }

        // POST /coletas
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ColetaCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coleta = await _coletaService.RegistrarColetaAsync(model);

            return CreatedAtAction(nameof(Obter), new { id = coleta.Id }, coleta);
        }

        // GET /coletas/{id}
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            return Ok(new { message = "Endpoint de detalhe ainda não implementado." });
        }

        // GET /coletas?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (dados, totalItems) = await _coletaService.ListarAsync(page, pageSize);

            return Ok(new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                data = dados
            });
        }
    }
}
