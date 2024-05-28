using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class RentStock
    {

        [Key]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public double RentPerBook { get; set; }
        public int QuantityInStock { get; set; }
      


    }
}
