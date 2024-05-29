namespace MiniProjectApp.Exceptions
{
    public class NoBooksProvidedException : Exception
    {

        string message;

        public NoBooksProvidedException() 
        {
            message = "Provide atleast one book id to perform the following operation";
        }

        public override string Message => message;


    }
}
