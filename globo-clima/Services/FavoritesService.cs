using globo_clima.Models;
using globo_clima.Repository;

namespace globo_clima.Services
{
    public class FavoritesService
    {
        private FavoritesRepository _favoritesRepository;
        private WeatherService _weatherService;

        public FavoritesService(FavoritesRepository mangaRepository, WeatherService weatherService)
        {
            _favoritesRepository = mangaRepository;
            _weatherService = weatherService;
        }

        public async Task<List<FavoritesModel>> GetAllFavoritesAsync()
        {
            var favorites = await _favoritesRepository.GetAllFavoritesAsync();

            if (favorites == null || favorites.Count == 0)
            {
                return new List<FavoritesModel>();
            }

            foreach (var favorite in favorites)
            {
                if (favorite.Lat != null && favorite.Lon != null)
                {
                    try
                    {
                        var weatherInfo = await _weatherService.GetWeatherByCoordinatesAsync(favorite.Lat, favorite.Lon);
                        favorite.TempC = ((weatherInfo.Main.Temp - 32) * 5 / 9).ToString("F2");
                        favorite.TempF = (weatherInfo.Main.Temp).ToString("F2");
                    }
                    catch
                    {
                        return new List<FavoritesModel>();
                    }
                }
            }

            return favorites;
        }

        public Task<bool> CreateFavorites(FavoritesModel favorites)
        {
            return _favoritesRepository.CreateFavoriteAsync(favorites);
        }

        public Task<bool> DeleteFavorites(Guid favorites, Guid userID)
        {
            return _favoritesRepository.DeleteFavoritesAsync(favorites, userID);
        }

        public Task<bool> UpdateFavoriteAsync(List<FavoritesModel> favorites, Guid userID)
        {
            return _favoritesRepository.UpdateFavoritesAsync(favorites, userID);
        }

    }
}
