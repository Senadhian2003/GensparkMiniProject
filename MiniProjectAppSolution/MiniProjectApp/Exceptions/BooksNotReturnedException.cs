namespace MiniProjectApp.Exceptions
{
    public class BooksNotReturnedException : Exception
    {
        string message;
        public BooksNotReturnedException(int bookId) 
        {
            message = $"The user has not returned the book with id {bookId}. Return the book before paying the fine";


        }
        public override string Message => message;

    }
}
