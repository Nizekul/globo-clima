using globo_clima.Models;

namespace globo_clima.Services
{
    public interface IWeatherService
    {
        Task<object> GetWeatherByCityAsync(string weatherCity);

        Task<WeatherModel> GetWeatherByCoordinatesAsync(string lat, string lon);

        Task<WeatherModel> GetFavoritesAsync(List<string> codes);

    }
}