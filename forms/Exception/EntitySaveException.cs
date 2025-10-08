using System.Net;

namespace forms.Exception;

public class EntitySaveException : DisprzException
{
    public EntitySaveException()
        : base("Entity save failed", HttpStatusCode.InternalServerError)
    {
    }

    public EntitySaveException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }

    public EntitySaveException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.InternalServerError)
    {
    }
}