using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class Purchase
    {

        [Key]
        public int PurchaseId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }

        public List<PurchaseDetail>? PurchaseDetailsList { get; set; }
    }
}
