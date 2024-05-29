using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.Exceptions
{
    public class FineAlreadyPaidException : Exception
    {
        string message; 

        public FineAlreadyPaidException()
        {
            message = "The fine has already been paid by the user";
        }

        public override string Message => message;

    }
}
