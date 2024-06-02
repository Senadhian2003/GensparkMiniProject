namespace MiniProjectApp.Exceptions
{
    public class NotPremiumUserException : Exception
    {
        string message;

        public NotPremiumUserException()
        {
            message = "You have to be a Premium user to use this feature. Upgrade plan to premium to use this feature and obtain discount on purchases";
        }

        public override string Message => message;

    }
}
