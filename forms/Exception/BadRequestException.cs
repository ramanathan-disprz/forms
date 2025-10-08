using System.Net;

namespace forms.Exception;

public class BadRequestException : DisprzException
{
    public BadRequestException()
        : base("Bad Request", HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.BadRequest)
    {
    }
}