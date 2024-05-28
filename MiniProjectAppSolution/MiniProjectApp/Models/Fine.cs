using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class Fine
    {

        public int RentId { get; set; }
        [ForeignKey(nameof(RentId))]

        [JsonIgnore]
        public Rent Rent { get; set; }

        public int NumberOfBooksFined { get; set; }

        public double FineAmount { get; set; }

        public DateTime FinePaidDate { get; set; }

       

    }
}
