using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class Purchase
    {

        [Key]
        public int PurchaseId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double Amount { get; set; }


    }
}
