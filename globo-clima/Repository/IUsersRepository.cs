using globo_clima.Models;

namespace globo_clima.Repository
{
    public interface IUsersRepository
    {
        Task<bool> CreateUserAsync(UserModel user);
    }
}