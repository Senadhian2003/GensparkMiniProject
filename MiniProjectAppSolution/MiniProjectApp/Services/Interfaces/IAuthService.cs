using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.BussinessLogics.Services
{
    public interface IAuthService
    {

        public Task<LoginReturnDTO> Login(UserLoginDTO loginDTO);
        public Task<User> Register(UserRegisterDTO registerDTO);

        public Task<PremiumUserDTO> UpgradeToPremium(int userId);

    }
}
