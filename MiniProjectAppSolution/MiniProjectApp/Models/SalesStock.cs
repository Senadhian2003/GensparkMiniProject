using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class SalesStock
    {
        [Key]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public double PricePerBook { get; set; }
        public int QuantityInStock { get; set; }
        public double Amount => PricePerBook * QuantityInStock;
    }
}
