using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models
{
    public class Sale
    {
        [Key] 
        public int SaleId { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public DateTime DateOfSale { get; set; }

        public double Amount { get; set; }

    }
}
