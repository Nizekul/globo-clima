using globo_clima.Models;

namespace globo_clima.Services
{
    public interface IFavoritesService
    {
        Task<List<FavoriteModel>> GetFavoritesByUserIdAsync(Guid userId);

        Task<bool> CreateFavorites(FavoriteModel favorites);

        Task<bool> DeleteFavorites(Guid favorites, Guid userID);

    }
}