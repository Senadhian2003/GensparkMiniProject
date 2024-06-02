using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IUserValidationService
    {
        public Task VerifyDue(int userId);
        public Task<UserStatusDTO> VerifyUserPaidFine(int UserId);
    }
}
