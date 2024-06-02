using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class FineDetail
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

        public DateTime? FinePaidDate { get; set; }

        public double FineAmount { get; set; }

        public string Status { get; set; }



    }
}
