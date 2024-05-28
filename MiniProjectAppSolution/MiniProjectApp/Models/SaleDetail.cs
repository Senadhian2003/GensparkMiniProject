using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class SaleDetail
    {
        [Key, Column(Order = 0)]
        public int SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        [JsonIgnore]
        public Sale Sale { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        public double Quantity { get; set; }

        public double Price { get; set; }
    }
}
