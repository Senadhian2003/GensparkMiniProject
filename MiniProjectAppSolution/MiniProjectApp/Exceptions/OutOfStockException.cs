namespace MiniProjectApp.Exceptions
{
    public class OutOfStockException : Exception
    {
        string message;

        public OutOfStockException()
        {
            message = $"There required item is out of stock";
        }

        public OutOfStockException(int data)
        {
            message = $"There required item is less in stock than required quantity by {data} item";
        }

        public override string Message => message;

    }
}
