namespace MaxiCrush.MAUI.Exceptions
{
    public class InternetException : Exception
    {
        public InternetException() : base()
        {
        }

        public InternetException(string message) : base(message)
        {
        }

        public InternetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
