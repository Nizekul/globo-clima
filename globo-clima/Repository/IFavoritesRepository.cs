using globo_clima.Models;

namespace globo_clima.Repository
{
    public interface IFavoritesRepository
    {
        Task<List<FavoriteModel>> GetAllFavoritesAsync();

        Task<bool> CreateFavoriteAsync(FavoriteModel favorite);

        Task<bool> DeleteFavoritesAsync(Guid favoriteId, Guid userID);
    }
}