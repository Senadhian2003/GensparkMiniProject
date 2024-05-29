namespace MiniProjectApp.Exceptions
{
    public class DuplicateBooksException : Exception
    {

        string message;

        public DuplicateBooksException()
        {
            message = "User cannot pick same book twice in the same Rent.";
        }

        public override string Message => message;

    }
}
