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
        private readonly IRepository<int, RentStock> _rentStockRepository;
        private readonly IRepository<int, Author> _authorRepository;
        private readonly IRepository<int, Publisher> _publisherRepository;

        public BookServices(IRepository<int, User> userRepository,IRepository<int, Feedback> feedbackRepository, IRepository<int, Book> bookRepository, IRepository<int, SalesStock> saleStockRepository,IRepository<int, RentStock> rentStockRepository, IRepository<int,Author> authorRepository, IRepository<int, Publisher> publisherRepository)
        {
            _feedbackRepository = feedbackRepository;
            _bookRepository = bookRepository;
            _saleStockRepository = saleStockRepository;
            _userRepository = userRepository;
            _rentStockRepository = rentStockRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
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

        

        public async Task<Feedback> GiveFeedback(GiveFeedback dto, int userId)
        {
            User user = await _userRepository.GetByKey(userId);
            Book book = await _bookRepository.GetByKey(dto.BookId);

            Feedback feedback = new Feedback
            {
                UserId = userId,
                BookId = dto.BookId,
                FeedbackHeading = dto.FeedbackHeading,
                Message = dto.Message,
                Rating = dto.Rating,
                FeedbackDate = DateTime.Now
            };
            book.AvgRating += dto.Rating;
            book.RatingCount += 1;

            await _bookRepository.Update(book);

            await _feedbackRepository.Add(feedback);

            return feedback;
        }

        public async Task<ViewFeedbackDTO> GetFeedbackItems(int BookId)
        {
            var feedbacks = await _feedbackRepository.GetAll();
            Book book = await _bookRepository.GetByKey(BookId);
            var bookFeedback = feedbacks.Where(f => f.BookId == BookId).ToList();

            if (bookFeedback.Count == 0)
            {
                throw new NoFeedbackException(BookId);
            }

            ViewFeedbackDTO viewFeedbackDTO = new ViewFeedbackDTO();
           

            int totalCount = bookFeedback.Count;
            double totalRating = 0;

            Dictionary<int, int> starCounts = new Dictionary<int, int>
    {
        {1, 0}, {2, 0}, {3, 0}, {4, 0}, {5, 0}
    };

            foreach (var feedback in bookFeedback)
            {
               

                totalRating += feedback.Rating;
                int roundedRating = (int)Math.Round(feedback.Rating);
                roundedRating = Math.Max(1, Math.Min(5, roundedRating)); // Ensure rating is between 1 and 5
                starCounts[roundedRating]++;

               
            }

            viewFeedbackDTO.AverageRating = totalRating / totalCount;
            viewFeedbackDTO.Feedbacks = bookFeedback;

            viewFeedbackDTO.FiveStarPercentage = (double)starCounts[5] / totalCount * 100;
            viewFeedbackDTO.FourStarPercentage = (double)starCounts[4] / totalCount * 100;
            viewFeedbackDTO.ThreeStarPercentage = (double)starCounts[3] / totalCount * 100;
            viewFeedbackDTO.TwoStarPercentage = (double)starCounts[2] / totalCount * 100;
            viewFeedbackDTO.OneStarPercentage = (double)starCounts[1] / totalCount * 100;

            return viewFeedbackDTO;
        }

        public async Task<List<RentStock>> GetCurrentRentBooks()
        {

            var RentItems = await _rentStockRepository.GetAll();

            if (RentItems.Any())
            {
                return RentItems.ToList();
            }

            throw new EmptyListException("Rent Books");
        }

        public async Task<List<Book>> GetAllBookDetails()
        {
            
            var books = await _bookRepository.GetAll();

            if(books.Any())
            {
                return books.ToList();

            }

            throw new EmptyListException("Books");

        }

        public async Task<Author> AddNewAuthor(AddNewAuthorDTO authDTO)
        {

            Author author = new Author()
            {
                AuthorName = authDTO.AuthorName,
                Address = authDTO.Address,
                Phone = authDTO.Phone,
            };
                
            var result = await _authorRepository.Add(author);

            return result;


        }

        public async Task<Publisher> AddNewPublisher(AddNewPublisherDTO publisherDTO)
        {

            Publisher publisher = new Publisher()
            {
                PublisherName = publisherDTO.PublisherName,
                City = publisherDTO.City,
                State = publisherDTO.State,
                Country = publisherDTO.Country,
            };

            var result = await _publisherRepository.Add(publisher);

            return result;


        }

        public async Task<List<Author>> GetAllAuthors()
        {

            var result = await _authorRepository.GetAll();

            return result.ToList();


        }

        public async Task<List<Publisher>> GetALlPublishers()
        {

            var result = await _publisherRepository.GetAll();

            return result.ToList();


        }

        public async Task<List<string>> GetUniqueCategories()
        {

            var result = await _bookRepository.GetAll();
            var uniqueCategories = result.Select(book => book.Category).Distinct().ToList();
            return uniqueCategories.ToList();


        }

        public async Task<Book> AddNewBook(AddNewBookDTO bookDTO)
        {

           Book book = new Book()
            {
               Title = bookDTO.Title,
               Description = bookDTO.Description,
               AuthorId = bookDTO.AuthorId,
               PublisherId = bookDTO.PublisherId,
               Category = bookDTO.Category
            };


            if (bookDTO.BookImage != null && bookDTO.BookImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await bookDTO.BookImage.CopyToAsync(memoryStream);
                    book.Image = memoryStream.ToArray();
                }
            }

            var result = await _bookRepository.Add(book);

            return result;


        }


        public async Task<SalesStock> ViewSaleBookDetail(int bookId)
        {

            
            var result = await _saleStockRepository.GetByKey(bookId);

            return result;


        }


    }
}
