using System.Net;

namespace forms.Exception;

public class MalformedJwtException : DisprzException
{
    public MalformedJwtException()
        : base("Malformed JWT token", HttpStatusCode.Unauthorized)
    {
    }

    public MalformedJwtException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }

    public MalformedJwtException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.Unauthorized)
    {
    }
}