using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MiniProjectApp.Models
{
    public class PurchaseDetail
    {
        public int PurchaseId { get; set; }

        //public Purchase Purchase { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; }    

        public double PricePerBook { get; set; }


    }
}
