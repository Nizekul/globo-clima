using globo_clima.Models;
using globo_clima.Repository;

namespace globo_clima.Services
{
    public class UserService
    {
        private UsersRepository _userRepository;

        public UserService(UsersRepository mangaRepository, WeatherService weatherService)
        {
            _userRepository = mangaRepository;
        }

        public Task<bool> CreateUserAsync(UserModel User)
        {
            return _userRepository.CreateUserAsync(User);
        }

    }
}
