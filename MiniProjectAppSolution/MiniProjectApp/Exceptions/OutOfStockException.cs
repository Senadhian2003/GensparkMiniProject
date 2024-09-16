namespace MiniProjectApp.Exceptions
{
    public class OutOfStockException : Exception
    {
        string message;

    

        public OutOfStockException(int bookId, int quantity)
        {
            message = $"The required Book with id {bookId} has only {quantity} items in stock. Please reduce the quantity to continue purchase";
        }

   

        public override string Message => message;

    }
}
