namespace MiniProjectApp.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }

        public DateTime DateOfSale { get; set; }

        public double Amount { get; set; }

    }
}
