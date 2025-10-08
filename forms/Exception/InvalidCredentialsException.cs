using System.Net;

namespace forms.Exception;

public class InvalidCredentialsException : DisprzException
{
    public InvalidCredentialsException()
        : base("Invalid credentials", HttpStatusCode.Unauthorized)
    {
    }

    public InvalidCredentialsException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }

    public InvalidCredentialsException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.Unauthorized)
    {
    }
}