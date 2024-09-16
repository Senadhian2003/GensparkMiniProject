using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjectApp.Models.DTO
{
    public class GiveFeedback
    {
       
        
        public int BookId { get; set; }

        [Required(ErrorMessage = "Heading is required.")]
        public string FeedbackHeading { get; set; }
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rating must be greater than 0 and less than 5.")]
        public double Rating { get; set; }

    }
}
