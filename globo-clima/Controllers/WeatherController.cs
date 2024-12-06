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

        public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        public async Task<IActionResult> GetWeatherByCity([FromQuery] string weatherCity)
        {
            try
            {
                string apiKey = _configuration["Envs:TokenWeather"];
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={weatherCity}&appid={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<object>(jsonResponse);

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
                string concatenatedCodes = string.Join(",", codes);
                string url = $"https://restcountries.com/v3.1/alpha?codes={concatenatedCodes}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var contriesData = JsonSerializer.Deserialize<object>(jsonResponse);

                return Ok(contriesData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter Favoritos");
                return StatusCode(500, "Erro interno ao buscar Favoritos.");
            }
        }


    }
}
