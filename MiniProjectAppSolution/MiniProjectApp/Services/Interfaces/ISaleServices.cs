using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface ISaleServices
    {
        public Task<List<Sale>> ViewOrders(int UserId);

        public Task<List<SaleDetail>> ViewOrderDetail(int saleId);

        public Task<List<Rent>> ViewRents(int UserId);

        public Task<List<RentDetail>> ViewRentDetail(int rentId);
        public Task<List<Rent>> ViewAllRents();

    }
}
