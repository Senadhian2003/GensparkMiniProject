using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IFineServices
    {

        public Task<List<Fine>> ViewFines(int UserId);
        public Task<List<Fine>> ViewUnPaidFines(int UserId);

        public Task<Fine> PayFine(int FineId, int UserId);

        public Task<FineDetail> PayFineForOneBook(int FineId, int BookId);
        public Task<List<Fine>> ViewAllFines();

    }
}
