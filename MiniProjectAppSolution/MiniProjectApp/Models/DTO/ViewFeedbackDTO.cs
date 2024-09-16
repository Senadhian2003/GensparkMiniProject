namespace MiniProjectApp.Models.DTO
{
    public class ViewFeedbackDTO
    {
        public double AverageRating { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public double FiveStarPercentage { get; set; }
        public double FourStarPercentage { get; set; }
        public double ThreeStarPercentage { get; set; }
        public double TwoStarPercentage { get; set; }
        public double OneStarPercentage { get; set; }

    }
}
