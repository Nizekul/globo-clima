using globo_clima.Models;
using globo_clima.Repository;
using System.Text.Json;

namespace globo_clima.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<object> GetWeatherByCityAsync(string weatherCity)
        {
            try
            {
                string apiKey = _configuration["Envs:TokenWeather"];
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={weatherCity}&units=imperial&appid={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<object>(jsonResponse);

                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados do clima");
                throw new Exception("Erro interno ao buscar dados do clima.", ex);
            }
        }

        public async Task<WeatherModel> GetWeatherByCoordinatesAsync(string lat, string lon)
        {
            try
            {
                string apiKey = _configuration["Envs:TokenWeather"];
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=imperial&appid={apiKey}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherModel>(jsonResponse);

                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados do clima");
                throw new Exception("Erro interno ao buscar dados do clima.", ex);
            }
        }

        public async Task<WeatherModel> GetFavoritesAsync(List<string> codes)
        {
            try
            {
                string concatenatedCodes = string.Join(",", codes);
                string url = $"https://restcountries.com/v3.1/alpha?codes={concatenatedCodes}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var countriesData = JsonSerializer.Deserialize<WeatherModel>(jsonResponse);

                return countriesData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter Favoritos");
                throw new Exception("Erro interno ao buscar Favoritos.", ex);
            }
        }
    }
}
