using globo_clima.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace globo_clima.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly WeatherService _weatherService;    

        public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory, WeatherService weatherService)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _weatherService = weatherService;
        }

        [HttpGet("City")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetWeatherByCity([FromQuery] string weatherCity)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherByCityAsync(weatherCity);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados do clima");
                return StatusCode(500, "Erro interno ao buscar dados do clima.");
            }
        }

        [HttpGet("Coordinates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetWeatherByCoodinates([FromQuery] string lat, [FromQuery] string lon)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados do clima");
                return StatusCode(500, "Erro interno ao buscar dados do clima.");
            }
        }

        [HttpGet("Favorites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetFavorites([FromQuery] List<string> codes)
        {
            try
            {
                var countriesData = await _weatherService.GetFavoritesAsync(codes);
                return Ok(countriesData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter Favoritos");
                return StatusCode(500, "Erro interno ao buscar Favoritos.");
            }
        }


    }
}
