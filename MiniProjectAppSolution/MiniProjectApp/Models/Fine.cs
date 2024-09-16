using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class Fine
    {
        [Key]
        public int FineId { get; set; }

        public int RentId { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public int NumberOfBooksFined { get; set; }

        public int NumbeOfBooksToPayFine { get; set; }

        public double FineAmount { get; set; }

        public double FinePending { get; set; }

        public string Status { get; set; }

        public DateTime? RentDate { get; set; }


        public List<FineDetail>? FineDetailsList { get; set; }

    }
}
