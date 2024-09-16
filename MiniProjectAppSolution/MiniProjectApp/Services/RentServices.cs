using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Services.Interfaces;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Services
{
    public class RentServices : IRentServices
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int, UserCredential> _userCredentialRepository;
        private readonly IRepository<int, Rent> _rentRepository;
        private readonly IRepository<int, RentStock> _rentStockRepository;
        private readonly ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
        private readonly ICompositeKeyRepository<int, RentCart> _rentCartRepository;
        private readonly ICompositeKeyRepository<int, SuperRentCart> _superRentCartRepository;
        public RentServices(IRepository<int, User> userRepository, ITransactionRepository transactionRepository, IRepository<int, UserCredential> userCredentialRepository, IRepository<int, Rent> rentRepository, IRepository<int, RentStock> rentStockRepository, ICompositeKeyRepository<int, RentDetail> rentDetailRepository, ICompositeKeyRepository<int, RentCart> rentCartRepository, ICompositeKeyRepository<int, SuperRentCart> superRentCartRepository) 
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _userCredentialRepository = userCredentialRepository;
            _rentRepository = rentRepository;
            _rentStockRepository = rentStockRepository;
            _rentDetailRepository = rentDetailRepository;
            _rentCartRepository = rentCartRepository;
            _superRentCartRepository = superRentCartRepository;
        }

        public async Task<bool> CheckUserStatus(int userId)
        {
            User user = await _userRepository.GetByKey(userId);

            if (user.Status == "Disabled")
            {
                return true;
            }

            return false;

        }

        public async Task<ReturnRentBooksDTO> AddBooksToRent(RentBooksDTO dto)
        {
            if (await CheckUserStatus(dto.UserId))
            {
                throw new FineNotPaidException();
            }

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


               

                User user = await _userRepository.GetByKey(dto.UserId);

                if (dto.CartType == "Normal Cart")
                {

                    Rent rent = new Rent();

                    rent.UserId = dto.UserId;
                    rent.DateOfRent = DateTime.Now;
                  
                    rent.Progress = "Return pending";
                    rent.BooksRented = dto.BookIds.Count;
                    rent.BooksToBeReturned = dto.BookIds.Count;
                    rent.CartType = "Normal Cart";
                    rent.DueDate = DateTime.Now.AddMinutes(7);
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
                            throw new OutOfStockException(bookId, bookStock.QuantityInStock);
                        }

                        bookStock.QuantityInStock -= 1;

                        await _rentStockRepository.Update(bookStock);

                        RentDetail rentDetail = new RentDetail();

                        rentDetail.RentId = rent.RentId;
                        rentDetail.BookId = bookId;
                        
                        rentDetail.Price = bookStock.RentPerBook;
                        rentDetail.status = "Return pending";
                        total += rentDetail.Price;
                        await _rentDetailRepository.Add(rentDetail);

                        RentCart rentcart = new RentCart();

                        rentcart.UserId = dto.UserId;
                        rentcart.BookId = bookId;
                        rentcart.RentId = rent.RentId;
                        rentcart.RentDate = DateTime.Now;
                        rentcart.DueDate = DateTime.Now.AddMinutes(7);

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
                    if(user.Role!="Premium User")
                    {
                        throw new NotPremiumUserException();
                    }

                    //int ItemsInSuperCart = await GetBooksCountInSuperCart(dto.UserId);
                    int ItemsInSuperCart = user.SuperRentCartItems.Count();

                    if (ItemsInSuperCart + dto.BookIds.Count > 3)
                    {
                        throw new BooksInSuperCartNotReturnedException(ItemsInSuperCart);
                    }

                    Rent rent = new Rent();

                    rent.UserId = dto.UserId;
                    rent.DateOfRent = DateTime.Now;
                  
                    rent.Progress = "Return pending";
                    rent.BooksRented = dto.BookIds.Count;
                    rent.BooksToBeReturned = dto.BookIds.Count;
                    rent.CartType = "Super Cart";
                    rent.DueDate = DateTime.Now.AddMinutes(7);
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
                            throw new OutOfStockException(bookId,bookStock.QuantityInStock);
                        }

                        bookStock.QuantityInStock -= 1;

                        await _rentStockRepository.Update(bookStock);

                        RentDetail rentDetail = new RentDetail();

                        rentDetail.RentId = rent.RentId;
                        rentDetail.BookId = bookId;
                        
                        rentDetail.Price = bookStock.RentPerBook;
                        rentDetail.status = "Return pending";
                        total += rentDetail.Price;
                        await _rentDetailRepository.Add(rentDetail);

                        SuperRentCart superRentcart = new SuperRentCart();

                        superRentcart.UserId = dto.UserId;
                        superRentcart.BookId = bookId;
                        superRentcart.RentId = rent.RentId;
                        superRentcart.RentDate = DateTime.Now;
                        superRentcart.DueDate = DateTime.Now.AddMinutes(7);

                        await _superRentCartRepository.Add(superRentcart);

                    }



                    ReturnRentBooksDTO result = new ReturnRentBooksDTO();

                    result.TotalAmount = 0;
                    result.BooksCount = dto.BookIds.Count;
                    await _transactionRepository.CommitTransactionAsync();
                    return result;




                }




            }
            catch (Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;
            }




        }



        public async Task<ReturnRentedBooksCountDTO> ReturnRentedBooks(ReturnRentedBooksDTO dto)
        {
            try
            {
                await _transactionRepository.BeginTransactionAsync();
                User user = await _userRepository.GetByKey(dto.UserId);

                var bookIds = dto.BookIds;

                //var rents = await _rentRepository.GetAll();

                //var userRents = rents.Where(r => r.UserId == dto.UserId && (r.Progress == "Return pending" || r.Progress == "Fine to be paid")).OrderBy(r => r.DateOfRent);
                int returnedBooksCount = 0;

                

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
                    if (rent.BooksToBeReturned == 0 )
                    {
                        rent.Progress = "Returned";
                    }

                    await _rentRepository.Update(rent);

                    RentDetail rentDetail = await _rentDetailRepository.GetByKey(rentId, bookId);
                    rentDetail.status = "Returned";
                    rentDetail.ReturnDate = DateTime.Now;
                    await _rentDetailRepository.Update(rentDetail);
                    var rentBookStock = await _rentStockRepository.GetByKey(bookId);
                    rentBookStock.QuantityInStock += 1;
                    await _rentStockRepository.Update(rentBookStock);



                    returnedBooksCount++;


                }


                ReturnRentedBooksCountDTO result = new ReturnRentedBooksCountDTO();
                result.NoOfBooksReturned = returnedBooksCount;
                await _transactionRepository.CommitTransactionAsync();
                return result;


            }
            catch (Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;

            }





        }

        public async Task<UserStatusDTO> VerifyUserPaidFine(int UserId)
        {
            User user = await _userRepository.GetByKey(UserId);

            var rents = await _rentRepository.GetAll();
            var fineRents = rents.Where(r => r.UserId == UserId && r.Progress == "Fine to be paid");


            if (fineRents.Any())
            {
                user.Status = "Disabled";
                await _userRepository.Update(user);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Disabled";
                return userStatusDTO;

            }
            else
            {
                user.Status = "Active";
                await _userRepository.Update(user);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Active";
                return userStatusDTO;
            }

        }




    }
}
