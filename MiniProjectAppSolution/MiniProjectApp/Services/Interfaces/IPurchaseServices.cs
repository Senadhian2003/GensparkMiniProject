using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IPurchaseServices
    {
        public Task<int> PurchaseBooksForLibrary(PurchaseBooksForLibraryDTO dto);
        public Task<List<Purchase>> ViewPurchase();

        public Task<Purchase> ViewPurchaseDetails(int purchaseId);

    }
}
