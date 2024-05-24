namespace MiniProjectApp.Models
{
    public class Feedback
    {

        public int FeedbackId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Message { get; set; }

        public double Rating { get; set; }


    }
}
