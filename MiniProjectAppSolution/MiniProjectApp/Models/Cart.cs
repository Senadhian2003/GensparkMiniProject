using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models
{
    public class Cart
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))] 
        public Book Book { get; set; }
        public int Quantity { get; set; }   

        public double Price { get; set; }

    }
}
