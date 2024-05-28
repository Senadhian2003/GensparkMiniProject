namespace MiniProjectApp.Exceptions
{
    public class InvalidUserIdOrBookIdException : Exception
    {
        string message;

        public InvalidUserIdOrBookIdException(int BookId)
        {
            message = $"The User did not rent the book with id {BookId}. Please provide the correct User Id and Book Id";
        }

        public override string Message => message;

    }
}
