using System.Net;

namespace forms.Exception;

public class EntityUpdateException : DisprzException
{
    public EntityUpdateException()
        : base("Entity update failed", HttpStatusCode.InternalServerError)
    {
    }

    public EntityUpdateException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }

    public EntityUpdateException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.InternalServerError)
    {
    }
}