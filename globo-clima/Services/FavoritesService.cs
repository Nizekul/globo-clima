using globo_clima.Models;
using globo_clima.Repository;

namespace globo_clima.Services
{
    public class FavoritesService
    {
        private FavoritesRepository _favoritesRepository;

        public FavoritesService(FavoritesRepository mangaRepository)
        {
            _favoritesRepository = mangaRepository;
        }

        public Task<List<FavoritesModel>> GetAllFavoritesAsync()
        {
            return _favoritesRepository.GetAllFavoritesAsync();
        }

        public Task<bool> CreateFavorites(List<FavoritesModel> favorites)
        {
            return _favoritesRepository.CreateFavoritesAsync(favorites);
        }

        public Task<bool> DeleteFavorites(List<Guid> favorites)
        {
            return _favoritesRepository.DeleteFavoritesAsync(favorites);
        }

        public Task<bool> UpdateFavoriteAsync(List<Guid> favorites)
        {
            return _favoritesRepository.DeleteFavoritesAsync(favorites);
        }
        
    }
}
