namespace MiniProjectApp.Exceptions
{
    public class NoFeedbackException : Exception
    {
        string message;

        public NoFeedbackException(int bookId)
        {
            message = $"No feedback for the Book with Id {bookId}";
        }

        public override string Message => message;

    }
}
