using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models.DTO
{
    public class ReturnCartDTO
    {
    
        public int UserId { get; set; }
     
        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
