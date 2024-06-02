using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class RentCart
    {

        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public User User { get; set; }

        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

        public int RentId { get; set; }

        public DateTime DueDate { get; set; }

        public int IsFined { get; set; }


    }
}
