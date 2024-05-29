namespace MiniProjectApp.Exceptions
{
    public class BookNotAvailabeForThisOperation : Exception
    {
        string message;

        public BookNotAvailabeForThisOperation(string data, int bookId)
        {
            message = $"The book with id {bookId} is not avalable for {data}";
        }

        public override string Message => message;
    }
}
