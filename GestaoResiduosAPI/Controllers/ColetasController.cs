using GestaoResiduosAPI.Services;
using GestaoResiduosAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        public async Task<IActionResult> Obter(int id)
        {
            var coleta = await _coletaService.ObterPorIdAsync(id);

            if (coleta == null)
                return NotFound(new { message = "Coleta não encontrada." });

            return Ok(coleta);
        }

        // GET /coletas?page=1&pageSize=10
        [Authorize]
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

        [Authorize]
        [HttpGet("paginado")]
        public async Task<IActionResult> ListarPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await _coletaService.ListarPaginadoAsync(page, pageSize);
            return Ok(resultado);
        }
    }
}
