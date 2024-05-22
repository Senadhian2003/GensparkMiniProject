using Microsoft.EntityFrameworkCore.Storage;

namespace MiniProjectApp.Models
{
    public class Fine
    {
        public int RentId { get; set; }

        public double FineAmount { get; set; }

        public DateTime FinePaidDate { get; set; }

        public string Status { get; set;}

    }
}
