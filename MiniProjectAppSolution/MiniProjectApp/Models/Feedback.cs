using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniProjectApp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        [JsonIgnore]
        public Book Book { get; set; }
        
        public string FeedbackHeading { get; set; }

        public string Message { get; set; }

        public double Rating { get; set; }

        public DateTime FeedbackDate { get; set; }


    }
}
