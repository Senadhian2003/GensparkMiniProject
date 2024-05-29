namespace MiniProjectApp.Exceptions
{
    public class BooksInSuperCartNotReturnedException : Exception
    {
        string message;

        public BooksInSuperCartNotReturnedException(int BooksInCart)
        {
            message = $"The super cart already has {BooksInCart} Books and can contain only 3 items at a time, please return old books to rent new books";
        }

        public override string Message => message;

    }
}
