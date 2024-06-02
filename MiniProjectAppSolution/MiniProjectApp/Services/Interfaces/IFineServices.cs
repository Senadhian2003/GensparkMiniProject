using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IFineServices
    {

        public Task<List<Fine>> ViewFines(int UserId);
        public Task<List<Fine>> ViewUnPaidFines(int UserId);

        public Task<Fine> PayFine(int RentId, int UserId);

    }
}
