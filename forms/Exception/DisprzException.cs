using System.Net;

namespace forms.Exception;

public class DisprzException : System.Exception
{
    protected DisprzException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }


    protected DisprzException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected DisprzException(string message, System.Exception innerException, HttpStatusCode statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}