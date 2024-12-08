using Amazon.DynamoDBv2.Model;
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
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserModel user)
        {
            try
            {
                var userResult = await _userService.CreateUserAsync(user);

                if (userResult)
                {
                    return Ok(userResult);
                }

                return BadRequest(new { message = "Erro ao criar o usuário. Tente novamente mais tarde." });
            }
            catch (ReplicatedWriteConflictException ex)
            {
                return Conflict(new { message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro inesperado. Tente novamente mais tarde.", details = ex.Message });
            }
        }

    }
}
