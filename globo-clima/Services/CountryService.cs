using System.Text.Json;
using globo_clima.Models;

namespace globo_clima.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CountryService> _logger;
        private readonly WeatherService _weatherService;

        public CountryService(HttpClient httpClient, IConfiguration configuration, ILogger<CountryService> logger, WeatherService weatherService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _weatherService = weatherService;
        }

        public async Task<List<CountryModel>> GetAllCountriesAsync(int page, int itemsPerPage)
        {
            try
            {
                string url = $"https://restcountries.com/v3.1/all?fields=name,capital,latlng,cca2";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var countriesData = JsonSerializer.Deserialize<List<CountryModel>>(jsonResponse);

                //var paginatedCountries = countriesData
                //.Skip(page * itemsPerPage)
                //.Take(itemsPerPage)
                //.ToList();

                //var tasks = paginatedCountries.Select(async country =>
                //{
                //    if (country.LatLng != null && country.LatLng.Count >= 2)
                //    {
                //        try
                //        {
                //            var weatherInfo = await _weatherService.GetWeatherByCoordinatesAsync(
                //                country.LatLng[0].ToString(),
                //                country.LatLng[1].ToString()
                //            );

                //            country.TempC = ((weatherInfo.Main.Temp - 32) * 5 / 9).ToString("F2");
                //            country.TempF = weatherInfo.Main.Temp.ToString("F2");
                //        }
                //        catch (Exception ex)
                //        {
                //            _logger.LogError(ex, $"Erro ao buscar clima para {country.Name.Common}");
                //        }
                //    }
                //});

                //await Task.WhenAll(tasks);
                return countriesData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados dos países");
                throw new Exception("Erro interno ao buscar dados dos países.", ex);
            }
        }

        public async Task<List<CountryModel>> GetWeatherForCurrentPageAsync(List<CountryModel> allCountries, int currentPage, int itemsPerPage)
        {
            var paginatedCountries = allCountries
                .Skip(currentPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            var tasks = paginatedCountries.Select(async country =>
            {
                if (country.LatLng != null && country.LatLng.Count >= 2)
                {
                    try
                    {
                        var weatherInfo = await _weatherService.GetWeatherByCoordinatesAsync(
                            country.LatLng[0].ToString(),
                            country.LatLng[1].ToString()
                        );

                        country.TempC = ((weatherInfo.Main.Temp - 32) * 5 / 9).ToString("F2");
                        country.TempF = weatherInfo.Main.Temp.ToString("F2");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao buscar clima para {country.Name.Common}");
                    }
                }
            });

            await Task.WhenAll(tasks);
            return paginatedCountries;
        }
    }
}