using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class PurchaseDetail
    {
        [Key, Column(Order = 0)]
        public int PurchaseId { get; set; }
        [ForeignKey(nameof(PurchaseId))]
        [JsonIgnore]
        public Purchase Purchase { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

        public int Quantity { get; set; }  

        public double PricePerBook { get; set; }

        public double TotalPrice => PricePerBook * Quantity;
    }
}
