namespace MiniProjectApp.Exceptions
{
    public class FineNotPaidException : Exception
    {

        string message;

        public FineNotPaidException()
        {
            message = "The user has not paid the due fine. Pay the fine to perform any operation";
        }

        public override string Message => message;

    }
}
