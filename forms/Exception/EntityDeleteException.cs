using System.Net;

namespace forms.Exception;

public class EntityDeleteException : DisprzException
{
    public EntityDeleteException()
        : base("Entity deletion failed", HttpStatusCode.InternalServerError)
    {
    }

    public EntityDeleteException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }

    public EntityDeleteException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.InternalServerError)
    {
    }
}