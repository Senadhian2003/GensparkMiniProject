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

    }
}
