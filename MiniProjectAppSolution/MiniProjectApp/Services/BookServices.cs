using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Services.Interfaces;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Services
{
    public class BookServices : IBookServices
    {

        private readonly IRepository<int, Feedback> _feedbackRepository;
        private readonly IRepository<int, Book> _bookRepository;
        private readonly IRepository<int, SalesStock> _saleStockRepository;
        private readonly IRepository<int, User> _userRepository;

        public BookServices(IRepository<int, User> userRepository,IRepository<int, Feedback> feedbackRepository, IRepository<int, Book> bookRepository, IRepository<int, SalesStock> saleStockRepository)
        {
            _feedbackRepository = feedbackRepository;
            _bookRepository = bookRepository;
            _saleStockRepository = saleStockRepository;
            _userRepository = userRepository;
        }


        public async Task<List<SalesStock>> GetCurrentSaleBooks()
        {

            var saleItems = await _saleStockRepository.GetAll();

            if (saleItems.Any())
            {
                return saleItems.ToList();
            }

            throw new EmptyListException("Sales Books");
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
            Book book = await _bookRepository.GetByKey(BookId);
            var bookFeedback = feedbacks.Where(f => f.BookId == BookId);

            if (bookFeedback.Count() == 0)
            {
                throw new NoFeedbackException(BookId);
            }

            ViewFeedbackDTO viewFeedbackDTO = new ViewFeedbackDTO();
            List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
            int cnt = 0;
            double totalRating = 0;
            foreach (var feedback in bookFeedback)
            {
                FeedbackDTO dto = new FeedbackDTO();

                dto.UserId = feedback.UserId;
                dto.Message = feedback.Message;
                dto.Rating = feedback.Rating;
                totalRating += feedback.Rating;
                cnt++;
                feedbackDTOs.Add(dto);

            }


            viewFeedbackDTO.AverageRating = totalRating / cnt;
            viewFeedbackDTO.feedbacks = feedbackDTOs;

            return viewFeedbackDTO;

        }




    }
}
