using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IBookServices
    {

        public Task<List<SalesStock>> GetCurrentSaleBooks();

        public Task<Feedback> GiveFeedback(GiveFeedback dto);

        public Task<ViewFeedbackDTO> GetFeedbackItems(int BookId);

    }
}
