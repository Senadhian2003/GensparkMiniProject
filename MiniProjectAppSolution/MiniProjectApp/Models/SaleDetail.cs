using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class SaleDetail
    {
        [Key, Column(Order = 0)]
        public int SaleId { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }
    }
}
