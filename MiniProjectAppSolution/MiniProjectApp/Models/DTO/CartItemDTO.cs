namespace MiniProjectApp.Models.DTO
{
    public class CartItemDTO
    {
        public int BookId { get; set; }

        public byte[]? Image { get; set; }
        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

    }
}
