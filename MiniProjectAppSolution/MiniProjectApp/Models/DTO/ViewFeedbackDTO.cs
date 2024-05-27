namespace MiniProjectApp.Models.DTO
{
    public class ViewFeedbackDTO
    {
        public double AverageRating { get; set; }
        public List<FeedbackDTO> feedbacks { get; set; }
    }
}
