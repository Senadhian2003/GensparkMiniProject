using MiniProjectApp.Models;

namespace MiniProjectApp.BussinessLogics.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
