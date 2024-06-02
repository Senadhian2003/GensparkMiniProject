using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface ISaleServices
    {
        public Task<List<Sale>> ViewOrders(int UserId);

        public Task<List<SaleDetail>> ViewOrderDetail(int saleId);

    }
}
