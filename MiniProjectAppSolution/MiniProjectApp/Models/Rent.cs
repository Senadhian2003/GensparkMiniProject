using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }


        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        
        public User User { get; set; }

        public string CartType { get; set; }

        public DateTime DateOfRent { get; set; }

        public DateTime DueDate { get; set; }

        public int BooksRented { get; set; }

        public int BooksToBeReturned { get; set; }

        public string Progress { get; set; }

        public double Amount { get; set; }

        public List<RentDetail>? RentDetailsList { get; set; }

    }
}
