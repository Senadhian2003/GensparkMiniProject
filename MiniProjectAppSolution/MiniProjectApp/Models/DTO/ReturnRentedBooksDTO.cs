namespace MiniProjectApp.Models.DTO
{
    public class ReturnRentedBooksDTO
    {
        public int UserId { get; set; }
        public List<int> BookIds { get; set; }
    }
}
