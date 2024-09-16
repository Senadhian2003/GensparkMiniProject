using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.Services.Interfaces
{
    public interface IRentServices
    {
        public Task<ReturnRentBooksDTO> AddBooksToRent(RentBooksDTO dto);
        public Task<ReturnRentedBooksCountDTO> ReturnRentedBooks(ReturnRentedBooksDTO dto);

      

    }
}
