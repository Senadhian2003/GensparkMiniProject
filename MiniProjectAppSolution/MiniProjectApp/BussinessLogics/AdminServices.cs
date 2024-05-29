using Microsoft.Extensions.Logging;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using System.Collections.Generic;

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
        private readonly IRepository<int, Fine> _fineRepository;
        private readonly ICompositeKeyRepository<int, RentCart> _rentCartRepository;
        private readonly ICompositeKeyRepository<int, SuperRentCart> _superRentCartRepository;
        public AdminServices(IRepository<int, User> userRepository, ICompositeKeyRepository<int, Cart> CartRepository, IRepository<int, SalesStock> saleStockRepository, ITransactionRepository transactionRepository, IRepository<int, Sale> saleRepository, ICompositeKeyRepository<int, SaleDetail> saleDetailRepository, IRepository<int, Feedback> feedbackRepository, IRepository<int, Book> bookRepository, IRepository<int,Purchase> purchaseRepository, ICompositeKeyRepository<int, PurchaseDetail> purchaseDetailRepository, IRepository<int,UserCredential> userCredentialRepository, IRepository<int,Rent> rentRepository, ICompositeKeyRepository<int,RentDetail> rentDetailRepository, IRepository<int,RentStock> rentStockRepository, IRepository<int,Fine> fineRepository, ICompositeKeyRepository<int, RentCart> rentCartRepository, ICompositeKeyRepository<int, SuperRentCart> superRentCartRepository)
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
            _fineRepository = fineRepository;
            _rentCartRepository = rentCartRepository;
            _superRentCartRepository = superRentCartRepository;

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
                    bool fineCreate = false;
                    rent.Progress = "Fine to be paid";
                    rent.FineAmount = rent.BooksToBeReturned * 5;
                    await _rentRepository.Update(rent);

                    Fine fine = new Fine();

                    fine.RentId = rent.RentId;
                    fine.UserId = rent.UserId;
                    fine.FineAmount = rent.FineAmount;
                    fine.NumberOfBooksFined = rent.BooksToBeReturned;
                    fine.Status = "Fine to be paid";
                    
                    await _fineRepository.Add(fine);

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

        public async Task<int> GetBooksCountInSuperCart(int UserId)
        {
            var rents = await _rentRepository.GetAll();

            var TotalCount = rents.Where(r => r.UserId == UserId && r.CartType == "SuperCart" && r.Progress == "Return pending").GroupBy(r => r.CartType)
                .Select(g =>  g.Sum(r => r.BooksToBeReturned)
                ).FirstOrDefault();

            return TotalCount;
            

        }


        public async Task<ReturnRentBooksDTO> AddBooksToRent(RentBooksDTO dto)
        {
            try
            {
                await _transactionRepository.BeginTransactionAsync();

                if (dto.BookIds.Count() == 0)
                {
                    throw new NoBooksProvidedException();
                }

                List<int> uniqueBooks = dto.BookIds.Distinct().ToList();

                if (uniqueBooks.Count() != dto.BookIds.Count())
                {
                    throw new DuplicateBooksException();
                }


                if (await CheckUserStatus(dto.UserId))
                {
                    throw new FineNotPaidException();
                }

                User user = await _userRepository.GetByKey(dto.UserId);

                if (dto.CartType == "Normal Cart")
                {

                    Rent rent = new Rent();

                    rent.UserId = dto.UserId;
                    rent.DateOfRent = DateTime.Now;
                    rent.DueDate = DateTime.Now.AddMinutes(10);
                    rent.Progress = "Return pending";
                    rent.BooksToBeReturned = dto.BookIds.Count;
                    rent.CartType = "Normal Cart";
                    await _rentRepository.Add(rent);
                    double total = 0;
                    var bookIds = dto.BookIds;

                    foreach (var bookId in bookIds)
                    {
                        var bookStock = await _rentStockRepository.GetByKey(bookId);
                        if (bookStock == null)
                        {
                            throw new BookNotAvailabeForThisOperation("Sale", bookId);
                        }
                        if (bookStock.QuantityInStock == 0)
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
                        total += rentDetail.Price;
                        await _rentDetailRepository.Add(rentDetail);

                        RentCart rentcart = new RentCart();

                        rentcart.UserId = dto.UserId;
                        rentcart.BookId = bookId;
                        rentcart.RentId = rent.RentId;

                        await _rentCartRepository.Add(rentcart);

                    }

                    rent.Amount = total;
                    await _rentRepository.Update(rent);

                    ReturnRentBooksDTO result = new ReturnRentBooksDTO();

                    result.TotalAmount = total;
                    result.BooksCount = dto.BookIds.Count;
                    await _transactionRepository.CommitTransactionAsync();
                    return result;

                }
                else
                {
                    //int ItemsInSuperCart = await GetBooksCountInSuperCart(dto.UserId);
                    int ItemsInSuperCart = user.SuperRentCartItems.Count();

                    if (ItemsInSuperCart + dto.BookIds.Count > 3)
                    {
                        throw new BooksInSuperCartNotReturnedException(ItemsInSuperCart);
                    }

                    Rent rent = new Rent();

                    rent.UserId = dto.UserId;
                    rent.DateOfRent = DateTime.Now;
                    rent.DueDate = DateTime.Now.AddMinutes(10);
                    rent.Progress = "Return pending";
                    rent.BooksToBeReturned = dto.BookIds.Count;
                    rent.CartType = "Super Cart";
                    await _rentRepository.Add(rent);
                    double total = 0;
                    var bookIds = dto.BookIds;

                    foreach (var bookId in bookIds)
                    {
                        var bookStock = await _rentStockRepository.GetByKey(bookId);
                        if (bookStock == null)
                        {
                            throw new BookNotAvailabeForThisOperation("Rent", bookId);
                        }
                        if (bookStock.QuantityInStock == 0)
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
                        total += rentDetail.Price;
                        await _rentDetailRepository.Add(rentDetail);

                        SuperRentCart superRentcart = new SuperRentCart();

                        superRentcart.UserId = dto.UserId;
                        superRentcart.BookId = bookId;
                        superRentcart.RentId = rent.RentId;

                        await _superRentCartRepository.Add(superRentcart);

                    }



                    ReturnRentBooksDTO result = new ReturnRentBooksDTO();

                    result.TotalAmount = 0;
                    result.BooksCount = dto.BookIds.Count;
                    await _transactionRepository.CommitTransactionAsync();
                    return result;




                }




            }catch(Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;
            }




        }


        public async Task<UserStatusDTO> VerifyUserPaidFine(int UserId)
        {
            UserCredential userCredential = await _userCredentialRepository.GetByKey(UserId);

            var rents = await _rentRepository.GetAll();
            var fineRents = rents.Where(r=>r.UserId == UserId && r.Progress== "Fine to be paid");

            
            if (fineRents.Any() )
            {
                userCredential.Status = "Disabled";
                await _userCredentialRepository.Update(userCredential);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Disabled";
               return userStatusDTO;

            }
            else
            {
                userCredential.Status = "Active";
                await _userCredentialRepository.Update(userCredential);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Active";
                return userStatusDTO;
            }
           
        }


        public async Task<ReturnRentedBooksCountDTO> ReturnRentedBooks(RentBooksDTO dto)
        {
            try 
            {
                await _transactionRepository.BeginTransactionAsync();
                User user = await _userRepository.GetByKey(dto.UserId);

                var bookIds = dto.BookIds;

                var rents = await _rentRepository.GetAll();

                var userRents = rents.Where(r => r.UserId == dto.UserId && (r.Progress == "Return pending" || r.Progress == "Fine to be paid")).OrderBy(r => r.DateOfRent);
                int returnedBooksCount = 0;

                //foreach (int bookId in bookIds)
                //{
                //    bool bookFlag = false;
                //    foreach (var userRent in userRents)
                //    {

                //        var rentDetails = userRent.RentDetailsList;
                //        int cnt = 0;
                //        bool flag = false;
                //        foreach (var rentDetail in rentDetails)
                //        {
                //            if (rentDetail.BookId == bookId && (rentDetail.status == "Return pending" || rentDetail.status == "Fine to be paid"))
                //            {
                //                rentDetail.status = "Returned";
                //                cnt++;
                //                returnedBooksCount++;
                //                flag = true;
                //                await _rentDetailRepository.Update(rentDetail);
                //                var rentBookStock = await _rentStockRepository.GetByKey(bookId);
                //                rentBookStock.QuantityInStock += 1;
                //                await _rentStockRepository.Update(rentBookStock);
                //                bookFlag = true;
                //                break;
                //            }


                //        }

                //        if (flag)
                //        {
                //            userRent.BooksToBeReturned -= cnt;

                //            if (userRent.BooksToBeReturned == 0 && userRent.Progress != "Fine to be paid")
                //            {
                //                userRent.Progress = "Returned";
                //            }

                //            await _rentRepository.Update(userRent);
                //            break;
                //        }



                //    }

                //    if (!bookFlag)
                //    {
                //        throw new InvalidUserIdOrBookIdException(bookId);
                //    }


                //}

                foreach (int bookId in bookIds)
                {

                    int rentId;

                    RentCart rentCartItem = await _rentCartRepository.GetByKey(dto.UserId, bookId);
                    SuperRentCart superRentCartItem = await _superRentCartRepository.GetByKey(dto.UserId, bookId);

                    if (rentCartItem != null)
                    {
                        rentId = rentCartItem.RentId;
                        await _rentCartRepository.DeleteByKey(dto.UserId, bookId);
                    }
                    else if (superRentCartItem != null)
                    {
                        rentId = superRentCartItem.RentId;
                        await _superRentCartRepository.DeleteByKey(dto.UserId, bookId);
                    }
                    else
                    {
                        throw new InvalidUserIdOrBookIdException(bookId);
                    }

                    Rent rent = await _rentRepository.GetByKey(rentId);
                    rent.BooksToBeReturned -= 1;
                    if (rent.BooksToBeReturned == 0 && rent.Progress != "Fine to be paid")
                    {
                        rent.Progress = "Returned";
                    }

                    await _rentRepository.Update(rent);

                    RentDetail rentDetail = await _rentDetailRepository.GetByKey(rentId, bookId);
                    rentDetail.status = "Returned";

                    var rentBookStock = await _rentStockRepository.GetByKey(bookId);
                    rentBookStock.QuantityInStock += 1;
                    await _rentStockRepository.Update(rentBookStock);



                    returnedBooksCount++;


                }


                await VerifyUserPaidFine(dto.UserId);


                ReturnRentedBooksCountDTO result = new ReturnRentedBooksCountDTO();
                result.NoOfBooksReturned = returnedBooksCount;
                await _transactionRepository.CommitTransactionAsync();
                return result;


            }
            catch(Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;

            }





        }


        public async Task<List<Fine>> ViewFines(int UserId)
        {

            var Fines = await _fineRepository.GetAll();

            var userFines = Fines.Where(f=>f.UserId== UserId);

            if (userFines.Any())
            {
                return userFines.ToList();
            }

            throw new EmptyListException("Fine");

           
        }

        public async Task<List<Fine>> ViewUnPaidFines(int UserId)
        {

            var Fines = await _fineRepository.GetAll();

            var userFines = Fines.Where(f => f.UserId == UserId && f.Status== "Fine to be paid");

            if (userFines.Any())
            {
                return userFines.ToList();
            }

            throw new EmptyListException("Fine");


        }

        public async Task<Fine> PayFine(int RentId,int UserId)
        {

            Fine fine = await _fineRepository.GetByKey(RentId);
            if(fine.Status== "Fine paid")
            {
                throw new FineAlreadyPaidException();
            }
            fine.Status = "Fine paid";

            fine.FinePaidDate = DateTime.Now;

            await _fineRepository.Update(fine);


            var rent = await _rentRepository.GetByKey(RentId);

            rent.Progress = "Fine paid";
            rent.BooksToBeReturned = 0;

            await _rentRepository.Update(rent);

            var rentDetails = rent.RentDetailsList.ToList();

            foreach(RentDetail rentDetail in rentDetails)
            {
                if(rentDetail.status=="Fine to be paid")
                {
                    rentDetail.status = "Returned";

                    RentStock bookStock = await _rentStockRepository.GetByKey(rentDetail.BookId);
                    bookStock.QuantityInStock += 1;
                    await _rentStockRepository.Update(bookStock);

                    await _rentDetailRepository.Update(rentDetail);

                    
                    if(rent.CartType=="Super Cart")
                    {
                        await _superRentCartRepository.DeleteByKey(UserId, rentDetail.BookId);

                    }
                    else
                    {
                        await _rentCartRepository.DeleteByKey(UserId,rentDetail.BookId);
                    }

                }
            }
            await VerifyUserPaidFine(UserId);
            return fine;

        }


    }
}
