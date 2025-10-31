using System.Net;

namespace forms.Exception;

public class DisprzException : System.Exception
{
    public DisprzException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public DisprzException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public DisprzException(string message, System.Exception innerException, HttpStatusCode statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}