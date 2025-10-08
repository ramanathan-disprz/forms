using System.Net;

namespace forms.Exception;

public class UnauthorizedAccessException : DisprzException
{
    public UnauthorizedAccessException()
        : base("Unauthorized access", HttpStatusCode.Forbidden)
    {
    }

    public UnauthorizedAccessException(string message)
        : base(message, HttpStatusCode.Forbidden)
    {
    }

    public UnauthorizedAccessException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.Forbidden)
    {
    }
}