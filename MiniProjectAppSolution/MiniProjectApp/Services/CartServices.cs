using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Services.Interfaces;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Services
{
    public class CartServices : ICartServices
    {

        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, SalesStock> _saleStockRepository;
        private readonly ICompositeKeyRepository<int, Cart> _CartRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int, Sale> _saleRepository;
        private readonly ICompositeKeyRepository<int, SaleDetail> _saleDetailRepository;
        private readonly IRepository<int,UserCredential> _userCredentialRepository;

        public CartServices(IRepository<int, User> userRepository,IRepository<int,UserCredential> userCredentialRepository, ICompositeKeyRepository<int, Cart> CartRepository, IRepository<int, SalesStock> saleStockRepository, ITransactionRepository transactionRepository, IRepository<int, Sale> saleRepository, ICompositeKeyRepository<int, SaleDetail> saleDetailRepository)
        {
            _userRepository = userRepository;
            _CartRepository = CartRepository;
            _saleStockRepository = saleStockRepository;
            _transactionRepository = transactionRepository;
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _userCredentialRepository = userCredentialRepository;
        }

        //public async Task<bool> CheckUserStatus(int userId)
        //{
        //    UserCredential userCredential = await _userCredentialRepository.GetByKey(userId);

        //    if (userCredential.Status == "Disabled")
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        public async Task<Cart> RemoveItemFromCart(int userId, int BookId)
        {
            //if (await CheckUserStatus(userId))
            //{
            //    throw new FineNotPaidException();
            //}
            Cart cartItem = await _CartRepository.DeleteByKey(userId, BookId);
            return cartItem;
        }

        public async Task<Cart> AddItemToCart(int userId, int bookId, int quantity)
        {
            //if (await CheckUserStatus(userId))
            //{
            //    throw new FineNotPaidException();
            //}

            User user = await _userRepository.GetByKey(userId);

            //if (user == null)
            //{
            //    throw new ElementNotFoundException("User");
            //}

            SalesStock saleItem = await _saleStockRepository.GetByKey(bookId);

            if (saleItem == null)
            {
                throw new ElementNotFoundException("Book");
            }

            if (saleItem.QuantityInStock < quantity)
            {
                throw new OutOfStockException(bookId , saleItem.QuantityInStock);

            }
            Cart cartItem;

            cartItem = await _CartRepository.GetByKey(userId, bookId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                cartItem.Price = saleItem.PricePerBook;
                await _CartRepository.Update(cartItem);
                return cartItem;
            }

            cartItem = new Cart();

            cartItem.UserId = userId;
            cartItem.BookId = bookId;
            cartItem.Quantity = quantity;
            cartItem.Price = saleItem.PricePerBook;
            await _CartRepository.Add(cartItem);
            return cartItem;
        }

        public async Task<Sale> CheckoutCart(int userId)
        {
            //if (await CheckUserStatus(userId))
            //{
            //    throw new FineNotPaidException();
            //}

            try
            {

                await _transactionRepository.BeginTransactionAsync();

                User user = await _userRepository.GetByKey(userId);
                double total = 0;
                var cartItems = user.CartItems.ToList();
                int numberOfBooks = 0;
                if (cartItems.Count == 0)
                {
                    throw new EmptyListException("Cart");
                }


                Sale sale = new Sale();
                sale.UserId = user.Id;
                sale.DateOfSale = DateTime.Now;
                await _saleRepository.Add(sale);

                foreach (var item in cartItems)
                {
                    var book = await _saleStockRepository.GetByKey(item.BookId);

                    //if(book == null)
                    //{
                    //    throw new ElementNotFoundException("Book");
                    //}

                    if ( book.QuantityInStock < item.Quantity)
                    {
                        throw new OutOfStockException( item.BookId, book.QuantityInStock );
                    }


                    total += (item.Price * item.Quantity);

                    SaleDetail saleDetail = new SaleDetail
                    {
                        SaleId = sale.SaleId,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    await _saleDetailRepository.Add(saleDetail);
                    numberOfBooks++;
                    // Update stock

                    book.QuantityInStock -= item.Quantity;
                    await _saleStockRepository.Update(book);
                    await _CartRepository.DeleteByKey(item.UserId, item.BookId);
                }

                sale.Total = total;
                sale.NoOfBooks = numberOfBooks;
                if(user.Role=="Premium User")
                {
                    sale.Discount = 0.4*total; 
                    sale.FinalAmount = total - (0.4*total);
                }
                else
                {
                    sale.FinalAmount = total;
                }
                //await _saleRepository.Update(sale);
                await _transactionRepository.CommitTransactionAsync();
                return sale;
            }
            catch (Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;
            }



            
        }

        public async Task<ViewCartDTO> GetCartItems(int userId)
        {
            User user = await _userRepository.GetByKey(userId);

            //var cartItems = user.CartItems.ToList();
            var cartItems = await _CartRepository.GetAll();

            var userCartItems = cartItems.Where(ci=>ci.UserId == userId).ToList();  

            if (userCartItems.Count > 0)
            {
                ViewCartDTO result = MapCartItemsToViewCartDTO(user.Role, userCartItems);

                return result;
            }

            throw new EmptyListException("Cart");
        }

        public async Task<List<RentCart>> GetRentCartItems(int userId)
        {
            User user = await _userRepository.GetByKey(userId);

            List<RentCart> rentCartItems = user.RentCartItems.ToList();

            if (rentCartItems.Count > 0)
            {

                return rentCartItems.ToList();
            }

            throw new EmptyListException("Rent Cart");
        }

        public async Task<List<SuperRentCart>> GetSuperRentCartItems(int userId)
        {
            User user = await _userRepository.GetByKey(userId);
            if (user.Role != "Premium User")
            {
                throw new NotPremiumUserException();
            }

            List<SuperRentCart> superRentCartItems = user.SuperRentCartItems.ToList();

            if (superRentCartItems.Count > 0)
            {

                return superRentCartItems.ToList();
            }

            throw new EmptyListException("Super Rent Cart");
        }

        public ViewCartDTO MapCartItemsToViewCartDTO(string role, List<Cart> cartItems)
        {
            double amount = 0;
            List<CartItemDTO> cartItemDTOs = new List<CartItemDTO>();
            ViewCartDTO result = new ViewCartDTO();
            foreach (Cart cartItem in cartItems)
            {
                CartItemDTO dto = new CartItemDTO();

                dto.BookId = cartItem.BookId;
                dto.Quantity = cartItem.Quantity;
                dto.Price = cartItem.Price;
                dto.BookName = cartItem.Book.Title;
                dto.Image = cartItem.Book.Image;
                dto.AuthorName = cartItem.Book.Author.AuthorName;
                cartItemDTOs.Add(dto);
                amount += (dto.Price * dto.Quantity);

            }
            if (role == "User")
            {
                result.Total = amount;
            }
            else
            {
                result.Total =amount - (amount *0.4);
                result.discount = amount * 0.4;
            }
           
            result.Items = cartItemDTOs;

            return result;


        }



    }
}
