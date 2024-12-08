using globo_clima.Models;

namespace globo_clima.Repository
{
    public interface IAuthRepository
    {
        Task<UserModel?> GetUserByLoginAsync(LoginModel login);
    }
}