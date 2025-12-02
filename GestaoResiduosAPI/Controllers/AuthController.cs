using GestaoResiduosAPI.Models;
using GestaoResiduosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            if (usuario.Username == "admin" && usuario.Password == "admin")
            {
                var token = _authService.GerarToken(usuario);
                return Ok(new { token });
            }

            return Unauthorized("Credenciais inválidas.");
        }
    }
}
