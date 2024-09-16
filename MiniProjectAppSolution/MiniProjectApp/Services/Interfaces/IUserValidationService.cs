using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IUserValidationService
    {
        public Task<VerifyDueReturnDTO> VerifyDue(int userId);
        public Task<UserStatusDTO> VerifyUserPaidFine(int UserId);
        public Task<List<User>> GetAllUsers();
    }
}
