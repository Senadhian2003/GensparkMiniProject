using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }


        public ICollection<RentCart>? RentCartItems { get; set; }
        public ICollection<SuperRentCart>? SuperRentCartItems { get; set; }
        public ICollection<Cart>? CartItems { get; set; }

        



    }
}
