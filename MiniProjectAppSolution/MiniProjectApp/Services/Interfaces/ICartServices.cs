using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface ICartServices
    {

        public Task<Cart> AddItemToCart(int userId, int bookId, int quantity);

        public Task<Cart> RemoveItemFromCart(int userId, int BookId);

        public Task<int> CheckoutCart(int userId);

        public Task<ViewCartDTO> GetCartItems(int userId);

        public Task<List<RentCart>> GetRentCartItems(int userId);
        public Task<List<SuperRentCart>> GetSuperRentCartItems(int userId);




    }
}
