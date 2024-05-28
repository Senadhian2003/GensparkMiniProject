using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.BussinessLogics.Interfaces
{
    public interface IAdminServices
    {

        public Task<int> PurchaseBooksForLibrary(PurchaseBooksForLibraryDTO dto);
        public Task<List<Purchase>> ViewPurchase();

        public Task<Purchase> ViewPurchaseDetails(int purchaseId);

        public  Task<ReturnRentBooksDTO> AddBooksToRent(RentBooksDTO dto);
        public Task<ReturnRentedBooksCountDTO> ReturnRentedBooks(RentBooksDTO dto);

    }
}
