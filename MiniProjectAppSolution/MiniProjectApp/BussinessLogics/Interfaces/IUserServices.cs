using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.BussinessLogics.Interfaces
{
    public interface IUserServices
    {

        public Task<Cart> AddItemToCart(int userId, int bookId, int quantity);

        public Task<Cart> RemoveItemFromCart(int userId, int BookId);

        public Task<List<SalesStock>> GetCurrentSaleBooks();

        public Task<int> CheckoutCart(int userId);

        public Task<ViewCartDTO> GetCartItems(int userId);

        public Task<List<RentCart>> GetRentCartItems(int userId);
        public Task<List<SuperRentCart>> GetSuperRentCartItems(int userId);

        public Task<Feedback> GiveFeedback(GiveFeedback dto);

        public Task<ViewFeedbackDTO> GetFeedbackItems(int BookId);

        public Task<List<Sale>> ViewOrders(int UserId);

        public Task<List<SaleDetail>> ViewOrderDetail(int saleId);

        

    }
}
