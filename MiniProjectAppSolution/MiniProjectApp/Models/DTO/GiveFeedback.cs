using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models.DTO
{
    public class GiveFeedback
    {
        public int UserId { get; set; }
        
        public int BookId { get; set; }
       

        public string Message { get; set; }

        public double Rating { get; set; }

    }
}
