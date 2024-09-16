using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class RentDetail
    {

        [Key, Column(Order = 0)]
        public int RentId { get; set; }
        [ForeignKey(nameof(RentId))]
        [JsonIgnore]
        public Rent Rent { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        public double Price { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string? status { get; set; }



    }
}
