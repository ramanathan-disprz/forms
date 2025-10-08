using System.Net;

namespace forms.Exception;

public class ConflictException : DisprzException
{
    public ConflictException()
        : base("Conflict", HttpStatusCode.Conflict)
    {
    }

    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }

    public ConflictException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.Conflict)
    {
    }
}