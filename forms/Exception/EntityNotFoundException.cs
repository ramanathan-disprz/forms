using System.Net;

namespace forms.Exception;

public class EntityNotFoundException : DisprzException
{
    public EntityNotFoundException()
        : base("Entity not found", HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(long id)
        : base($"Entity with id {id} not found", HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(string message, System.Exception innerException)
        : base(message, innerException, HttpStatusCode.NotFound)
    {
    }
}