using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.BussinessLogics
{
    public class UserServices : IUserServices
    {

        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, SalesStock> _saleStockRepository;
        private readonly ICompositeKeyRepository<int, Cart> _CartRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int,Sale> _saleRepository;
        private readonly ICompositeKeyRepository<int,SaleDetail> _saleDetailRepository;
        private readonly IRepository<int, Feedback> _feedbackRepository;
        private readonly IRepository<int, Book> _bookRepository;
        public UserServices(IRepository<int, User> userRepository, ICompositeKeyRepository<int, Cart> CartRepository, IRepository<int, SalesStock> saleStockRepository,ITransactionRepository transactionRepository, IRepository<int, Sale> saleRepository, ICompositeKeyRepository<int, SaleDetail> saleDetailRepository, IRepository<int, Feedback> feedbackRepository, IRepository<int,Book> bookRepository)
        {

            _userRepository = userRepository;
            _CartRepository = CartRepository;
            _saleStockRepository = saleStockRepository;
            _transactionRepository = transactionRepository;
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _feedbackRepository = feedbackRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Cart> AddItemToCart(int userId, int bookId, int quantity)
        {
            User user = await _userRepository.GetByKey(userId);

            if(user == null)
            {
                throw new ElementNotFoundException("User");
            }

            SalesStock saleItem = await _saleStockRepository.GetByKey(bookId);

            if (saleItem.QuantityInStock < quantity)
            {
                throw new OutOfStockException(quantity-saleItem.QuantityInStock);

            }
            Cart cartItem;

            cartItem = await _CartRepository.GetByKey(userId, bookId);

            if(cartItem!=null)
            {
                cartItem.Quantity = quantity;
                cartItem.Price = saleItem.PricePerBook * quantity;
                await _CartRepository.Update(cartItem);
                return cartItem;
            }

            cartItem = new Cart();

            cartItem.UserId = userId;
            cartItem.BookId = bookId;
            cartItem.Quantity = quantity;
            cartItem.Price = saleItem.PricePerBook * quantity;
            await _CartRepository.Add(cartItem);
            return cartItem;
        }

        public async Task<int> CheckoutCart(int userId)
        {
            try
            {

                await _transactionRepository.BeginTransactionAsync();

                User user = await _userRepository.GetByKey(userId);
                double total = 0;
                var cartItems = user.CartItems.ToList();

                if (cartItems.Count == 0)
                {
                    throw new EmptyListException("Cart");
                }


                Sale sale = new Sale();
                sale.UserId = user.Id;
                sale.DateOfSale = DateTime.UtcNow;
                

                await _saleRepository.Add(sale);

                foreach (var item in cartItems)
                {
                    var book = await _saleStockRepository.GetByKey(item.BookId);

                    if (book == null || book.QuantityInStock < item.Quantity)
                    {
                        throw new InvalidOperationException($"Book with ID {item.BookId} is out of stock.");
                    }


                    total += item.Price;

                    SaleDetail saleDetail = new SaleDetail
                    {
                        SaleId = sale.SaleId,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    await _saleDetailRepository.Add(saleDetail);

                    // Update stock
                  
                    book.QuantityInStock -= item.Quantity;
                    await _saleStockRepository.Update(book);
                    await _CartRepository.DeleteByKey(item.UserId, item.BookId);
                }
                sale.Amount = total;
                await _saleRepository.Update(sale);
                await _transactionRepository.CommitTransactionAsync();
                return sale.SaleId;
            }
            catch (Exception ex)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw ex;
            }



            throw new NotImplementedException();
        }

        public async Task<ViewCartDTO> GetCartItems(int userId)
        {
            User user = await _userRepository.GetByKey(userId);

            var cartItems = user.CartItems.ToList();

            if(cartItems.Count > 0)
            {
                ViewCartDTO result = MapCartItemsToViewCartDTO(cartItems);

                return result;
            }

            throw new EmptyListException("Cart");
        }

        public ViewCartDTO MapCartItemsToViewCartDTO(List<Cart> cartItems)
        {
            double amount =0;
            List<CartItemDTO> cartItemDTOs = new List<CartItemDTO>();
            ViewCartDTO result = new ViewCartDTO();
            foreach(Cart cartItem in cartItems)
            {
                CartItemDTO dto = new CartItemDTO();

                dto.BookId = cartItem.BookId;
                dto.Quantity = cartItem.Quantity;
                dto.Price = cartItem.Price;
                dto.Book = cartItem.Book;
                cartItemDTOs.Add(dto);
                amount += dto.Price;

            }

            result.Total = amount;
            result.Items = cartItemDTOs;

            return result;


        }


        public async Task<List<SalesStock>> GetCurrentSaleBooks()
        {

            var saleItems = await _saleStockRepository.GetAll();

            if(saleItems.Any())
            {
                return saleItems.ToList();
            }

            throw new EmptyListException("Sales Books");
        }

        public async Task<Cart> RemoveItemFromCart(int userId, int BookId)
        {
            Cart cartItem = await _CartRepository.DeleteByKey(userId, BookId);

            return cartItem;

            
        }

        public async Task<Feedback> GiveFeedback(GiveFeedback dto)
        {
            User user = await _userRepository.GetByKey(dto.UserId);
            Book book = await _bookRepository.GetByKey(dto.BookId);

            Feedback feedback = new Feedback
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                Message = dto.Message,
                Rating = dto.Rating,
            };

            await _feedbackRepository.Add(feedback);

            return feedback;

            
        }

        public async Task<ViewFeedbackDTO> GetFeedbackItems(int BookId)
        {
            var feedbacks = await _feedbackRepository.GetAll();

            var bookFeedback = feedbacks.Where(f=> f.BookId == BookId);

            if(bookFeedback.Count()==0)
            {
                throw new NoFeedbackException(BookId);
            }

            ViewFeedbackDTO viewFeedbackDTO = new ViewFeedbackDTO();
            List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>(); 
            int cnt = 0;
            double totalRating = 0;
            foreach(var feedback in bookFeedback)
            {
                FeedbackDTO dto = new FeedbackDTO();

                dto.UserId = feedback.UserId;
                dto.Message = feedback.Message;
                dto.Rating = feedback.Rating;
                totalRating+= feedback.Rating;
                cnt++;
                feedbackDTOs.Add(dto);

            }


            viewFeedbackDTO.AverageRating = totalRating/cnt;
            viewFeedbackDTO.feedbacks = feedbackDTOs;

            return viewFeedbackDTO;
            throw new NotImplementedException();
        }
    }
}
