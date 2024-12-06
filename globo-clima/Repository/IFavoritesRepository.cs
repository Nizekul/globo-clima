using globo_clima.Models;

namespace globo_clima.Repository
{
    public interface IFavoritesRepository
    {
        Task<List<FavoritesModel>> GetAllFavoritesAsync();
    }
}