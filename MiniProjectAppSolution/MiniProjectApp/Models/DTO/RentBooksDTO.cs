namespace MiniProjectApp.Models.DTO
{
    public class RentBooksDTO
    {
        public int UserId { get; set; }
        public string CartType { get; set; } = "Normal Cart";
        public List<int> BookIds { get; set; }

    }
}
