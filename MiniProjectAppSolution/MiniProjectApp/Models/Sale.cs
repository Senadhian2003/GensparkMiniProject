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

        public int NoOfBooks { get; set; }

        public double Total { get; set; }

        public double Discount { get; set; }

        public double FinalAmount { get; set; }



        public List<SaleDetail>? SaleDetailList { get; set; }

    }
}
