using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Services.Interfaces;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly IRepository<int, SalesStock> _saleStockRepository;
        private readonly IRepository<int, Purchase> _purchaseRepository;
        private readonly ICompositeKeyRepository<int, PurchaseDetail> _purchaseDetailRepository;
        private readonly IRepository<int, Book> _bookRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int, RentStock> _rentStockRepository;
        public PurchaseServices(IRepository<int, SalesStock> saleStockRepository, IRepository<int, Book> bookRepository, IRepository<int, RentStock> rentStockRepository, IRepository<int, Purchase> purchaseRepository, ICompositeKeyRepository<int, PurchaseDetail> purchaseDetailRepository, ITransactionRepository transactionRepository) 
        {
            _saleStockRepository = saleStockRepository;
            _purchaseRepository = purchaseRepository;
            _purchaseDetailRepository = purchaseDetailRepository;
            _bookRepository = bookRepository;
            _transactionRepository = transactionRepository;
            _rentStockRepository = rentStockRepository;
        }



        public async Task<int> PurchaseBooksForLibrary(PurchaseBooksForLibraryDTO dto)
        {
            try
            {
                Purchase purchase = new Purchase();

                purchase.DateOfPurchase = DateTime.Now;
                await _transactionRepository.BeginTransactionAsync();
                if (dto.Type == "Sale")
                {

                    var items = dto.Items;
                    if (items.Count == 0)
                    {
                        throw new EmptyListException("Input Books");
                    }
                    purchase.Type = "Sale";

                    double total = 0;
                    await _purchaseRepository.Add(purchase);

                    foreach (var item in items)
                    {
                        var bookstock = await _saleStockRepository.GetByKey(item.BookId);
                        var book = await _bookRepository.GetByKey(item.BookId);
                        PurchaseDetail detail = new PurchaseDetail();


                        detail.PurchaseId = purchase.PurchaseId;
                        detail.BookId = item.BookId;
                        detail.Quantity = item.Quantity;
                        detail.PricePerBook = item.PricePerBook;

                        await _purchaseDetailRepository.Add(detail);

                        if (bookstock == null)
                        {
                            SalesStock salesStock = new SalesStock();

                            salesStock.BookId = item.BookId;
                            salesStock.QuantityInStock = item.Quantity;
                            salesStock.PricePerBook = item.PricePerBook;

                            await _saleStockRepository.Add(salesStock);

                        }
                        else
                        {
                            bookstock.QuantityInStock += item.Quantity;
                            bookstock.PricePerBook = item.PricePerBook;

                            await _saleStockRepository.Update(bookstock);

                        }


                        total += item.PricePerBook * item.Quantity;
                    }

                    purchase.Amount = total;

                    await _purchaseRepository.Update(purchase);

                    await _transactionRepository.CommitTransactionAsync();
                    return purchase.PurchaseId;


                }
                else
                {
                    var items = dto.Items;
                    if (items.Count == 0)
                    {
                        throw new EmptyListException("Input Books");
                    }
                    purchase.Type = "Rent";

                    double total = 0;
                    await _purchaseRepository.Add(purchase);

                    foreach (var item in items)
                    {
                        var bookstock = await _rentStockRepository.GetByKey(item.BookId);
                        var book = await _bookRepository.GetByKey(item.BookId);
                        PurchaseDetail detail = new PurchaseDetail();


                        detail.PurchaseId = purchase.PurchaseId;
                        detail.BookId = item.BookId;
                        detail.Quantity = item.Quantity;
                        detail.PricePerBook = item.PricePerBook;

                        await _purchaseDetailRepository.Add(detail);

                        if (bookstock == null)
                        {
                            RentStock rentStock = new RentStock();

                            rentStock.BookId = item.BookId;
                            rentStock.QuantityInStock = item.Quantity;
                            rentStock.RentPerBook = item.PricePerBook;

                            await _rentStockRepository.Add(rentStock);

                        }
                        else
                        {
                            bookstock.QuantityInStock += item.Quantity;
                            bookstock.RentPerBook = item.PricePerBook;

                            await _rentStockRepository.Update(bookstock);

                        }


                        total += item.PricePerBook * item.Quantity;
                    }

                    purchase.Amount = total;

                    await _purchaseRepository.Update(purchase);

                    await _transactionRepository.CommitTransactionAsync();
                    return purchase.PurchaseId;




                }

            }
            catch (Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;
            }
        }


        public async Task<List<Purchase>> ViewPurchase()
        {
            var purchases = await _purchaseRepository.GetAll();

            if (purchases.Count() == 0)
            {
                throw new EmptyListException("Purchase");
            }
            return purchases.ToList();
        }

        public async Task<Purchase> ViewPurchaseDetails(int purchaseId)
        {
            var purchase = await _purchaseRepository.GetByKey(purchaseId);

            if (purchase == null)
            {
                throw new ElementNotFoundException("Purchase Detail");
            }

            return purchase;
        }

    }
}
