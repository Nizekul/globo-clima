using globo_clima.Models;
using globo_clima.Repository;

namespace globo_clima.Services
{
    public class AuthService
    {
        private AuthRepository _authRepository;

        public AuthService(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserModel> GetUserAsync(LoginModel login)
        {
            var userDB = await _authRepository.GetUserByLoginAsync(login);

            return userDB;
        }

    }
}
