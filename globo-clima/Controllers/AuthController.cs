using globo_clima.Models;
using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var user = await _authService.GetUserAsync(login);

                if (user == null)
                {
                    return Unauthorized("Usuário ou senha inválidos.");
                }

                return Ok(new
                {
                    name = user.Name,
                    id = user.Id
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar login: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

    }
}
