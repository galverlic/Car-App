namespace Car_App.Helpers
{
    // custom exception class for throwing application specific exceptions (e.g. for validation)
    // that can be caught and handled within the application
    public class AppException : Exception
    {
        public int StatusCode { get; set; }

        public AppException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public AppException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(string message)
            : base(message)
        {
            StatusCode = 500; // Default value for internal server error
        }
    }
}