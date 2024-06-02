using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class Fine
    {
        [Key]
        [ForeignKey(nameof(Rent))]
        public int RentId { get; set; }

        [JsonIgnore]
        public Rent Rent { get; set; }

        public int UserId { get; set; }

        public int NumberOfBooksFined { get; set; }

        public int NumbeOfBooksPaidFine { get; set; }

        public double FineAmount { get; set; }

        public double FinePending { get; set; }

        public string Status { get; set; }

        public DateTime? FinePaidDate { get; set; }

        


    }
}
