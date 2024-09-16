using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IBookServices
    {

        public Task<List<SalesStock>> GetCurrentSaleBooks();

        public Task<Feedback> GiveFeedback(GiveFeedback dto, int userId);

        public Task<ViewFeedbackDTO> GetFeedbackItems(int BookId);

        public Task<List<RentStock>> GetCurrentRentBooks();

        public Task<List<Book>> GetAllBookDetails();

        public Task<Author> AddNewAuthor(AddNewAuthorDTO authDTO);

        public Task<List<Author>> GetAllAuthors();

        public Task<List<string>> GetUniqueCategories();

        public Task<Publisher> AddNewPublisher(AddNewPublisherDTO publisherDTO);
        public Task<List<Publisher>> GetALlPublishers();

        public Task<Book> AddNewBook(AddNewBookDTO bookDTO);
        public Task<SalesStock> ViewSaleBookDetail(int bookId);
    }
}
