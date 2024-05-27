namespace MiniProjectApp.Models.DTO
{
    public class CartItemDTO
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

    }
}
