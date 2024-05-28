using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.BussinessLogics
{
    public class AdminServices : IAdminServices
    {

        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, SalesStock> _saleStockRepository;
        private readonly ICompositeKeyRepository<int, Cart> _CartRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int, Sale> _saleRepository;
        private readonly ICompositeKeyRepository<int, SaleDetail> _saleDetailRepository;
        private readonly IRepository<int, Feedback> _feedbackRepository;
        private readonly IRepository<int, Book> _bookRepository;
        private readonly IRepository<int, Purchase> _purchaseRepository;
        private readonly ICompositeKeyRepository<int, PurchaseDetail> _purchaseDetailRepository;
        private readonly IRepository<int, Rent> _rentRepository;
        private readonly ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
        private readonly IRepository<int, RentStock> _rentStockRepository;

        private readonly IRepository<int, UserCredential> _userCredentialRepository;
        public AdminServices(IRepository<int, User> userRepository, ICompositeKeyRepository<int, Cart> CartRepository, IRepository<int, SalesStock> saleStockRepository, ITransactionRepository transactionRepository, IRepository<int, Sale> saleRepository, ICompositeKeyRepository<int, SaleDetail> saleDetailRepository, IRepository<int, Feedback> feedbackRepository, IRepository<int, Book> bookRepository, IRepository<int,Purchase> purchaseRepository, ICompositeKeyRepository<int, PurchaseDetail> purchaseDetailRepository, IRepository<int,UserCredential> userCredentialRepository, IRepository<int,Rent> rentRepository, ICompositeKeyRepository<int,RentDetail> rentDetailRepository, IRepository<int,RentStock> rentStockRepository )
        {

            _userRepository = userRepository;
            _CartRepository = CartRepository;
            _saleStockRepository = saleStockRepository;
            _transactionRepository = transactionRepository;
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _feedbackRepository = feedbackRepository;
            _bookRepository = bookRepository;
            _purchaseRepository = purchaseRepository;
            _purchaseDetailRepository = purchaseDetailRepository;
            _userCredentialRepository = userCredentialRepository;
            _rentRepository = rentRepository;
            _rentDetailRepository = rentDetailRepository;
            _rentStockRepository = rentStockRepository; 
        }

        public async Task<int> PurchaseBooksForLibrary(PurchaseBooksForLibraryDTO dto)
        {
            try {
                Purchase purchase = new Purchase();

                purchase.DateOfPurchase = DateTime.Now;
                await _transactionRepository.BeginTransactionAsync();
                if (dto.Type == "Sale")
                {
                  
                    var items = dto.Items;
                    if (items.Count==0)
                    {
                        throw new EmptyListException("Input Books");
                    }
                    purchase.Type = "Sale";
                   
                    double total = 0;
                    await _purchaseRepository.Add(purchase);

                    foreach (var item in items)
                    {
                        var bookstock = await _saleStockRepository.GetByKey(item.BookId);

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
                    throw new NotImplementedException();
                }

            }catch(Exception ex)
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

            if (purchase==null)
            {
                throw new ElementNotFoundException("Purchase Detail");
            }

            return purchase;
        }


        public async Task<bool> CheckUserStatus(int userId)
        {
            UserCredential userCredential = await _userCredentialRepository.GetByKey(userId);

            if (userCredential.Status == "Disabled")
            {
                return true;
            }

            return false;

        }

        public async Task<bool> VerifyDue(int userId)
        {

            var rents = await _rentRepository.GetAll();

            var userRents = rents.Where(r => r.UserId == userId && r.Progress=="Return pending" && DateTime.Now>r.DueDate);
            bool flag=false;

            if(userRents.Any())
            {
                flag = true;
                foreach (var rent in userRents)
                {
                    
                    rent.Progress = "Fine to be paid";
                    await _rentRepository.Update(rent);
                    var rentDetails = rent.RentDetailsList;

                    foreach (var rentDetail in rentDetails)
                    {

                        if (rentDetail.status != "Returned")
                        {
                            rentDetail.status = "Fine to be paid";
                            await _rentDetailRepository.Update(rentDetail);
                        }


                    }

                }

            }

           return flag;


        }


        public async Task<ReturnRentBooksDTO> AddBooksToRent(RentBooksDTO dto)
        {
            bool fined = await VerifyDue(dto.UserId);

            if (fined || await CheckUserStatus(dto.UserId))
            {
                throw new FineNotPaidException();
            }

            Rent rent = new Rent();

            rent.UserId = dto.UserId;
            rent.DateOfRent = DateTime.Now;
            rent.DueDate = DateTime.Now.AddDays(7);
            rent.Progress = "Return pending";
            rent.BooksToBeReturned = dto.BookIds.Count;
            rent.CartType = "Normal Cart";
            await _rentRepository.Add(rent);
            double total = 0;
            var bookIds = dto.BookIds;

            foreach(var bookId in bookIds)
            {
                var bookStock = await _rentStockRepository.GetByKey(bookId) ;

                if (bookStock.QuantityInStock==0)
                {
                    throw new OutOfStockException();
                }

                bookStock.QuantityInStock -= 1;

                await _rentStockRepository.Update(bookStock);

                RentDetail rentDetail = new RentDetail();

                rentDetail.RentId = rent.RentId;
                rentDetail.BookId = bookId;
                rentDetail.ReturnDate = DateTime.Now.AddDays(7);
                rentDetail.Price = bookStock.RentPerBook;
                rentDetail.status = "Return pending";
                total+= rentDetail.Price;
                await _rentDetailRepository.Add(rentDetail);


            }
            
            rent.Amount = total;
            await _rentRepository.Update(rent);

            ReturnRentBooksDTO result = new ReturnRentBooksDTO();

            result.TotalAmount = total;
            result.BooksCount = dto.BookIds.Count;

            return result;


           
        }



        public async Task<ReturnRentedBooksCountDTO> ReturnRentedBooks(RentBooksDTO dto)
        {

            User user = await _userRepository.GetByKey(dto.UserId);

            var bookIds = dto.BookIds;

            var rents = await _rentRepository.GetAll();

            var userRents = rents.Where(r=>r.UserId==dto.UserId && r.Progress== "Return pending").OrderBy(r=>r.DateOfRent);
            int returnedBooksCount = 0;

            foreach (int bookId in bookIds)
            {
                bool bookFlag = false;
                foreach (var userRent in userRents)
                {

                    var rentDetails = userRent.RentDetailsList;
                    int cnt = 0;
                    bool flag = false;
                    foreach(var rentDetail in rentDetails) 
                    {
                        if(rentDetail.BookId==bookId && rentDetail.status=="Return pending")
                        {
                            rentDetail.status = "Returned";
                            cnt++;
                            returnedBooksCount++;
                            flag = true;
                            await _rentDetailRepository.Update(rentDetail);
                            var rentBookStock = await _rentStockRepository.GetByKey(bookId);
                            rentBookStock.QuantityInStock += 1;
                            await _rentStockRepository.Update(rentBookStock);
                            bookFlag = true;
                        }
                   
                    }

                    if (flag)
                    {
                        userRent.BooksToBeReturned-=cnt;

                        if(userRent.BooksToBeReturned==0)
                        {
                            userRent.Progress = "Returned";
                        }

                        await _rentRepository.Update(userRent);
                    }



                }

                if(!bookFlag)
                {
                    throw new InvalidUserIdOrBookIdException(bookId);
                }


            }

            
                
            

            ReturnRentedBooksCountDTO result = new ReturnRentedBooksCountDTO();
            result.NoOfBooksReturned = returnedBooksCount;
            return result;


          
        }





    }
}
