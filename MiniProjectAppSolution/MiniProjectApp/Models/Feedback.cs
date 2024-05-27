using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Book Book { get; set; }

        public string Message { get; set; }

        public double Rating { get; set; }


    }
}
