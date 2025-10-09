using System.Net;
using forms.Dto;
using forms.Exception;

namespace forms.GraphQL;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not DisprzException ex)
        {
            return ErrorBuilder.FromError(error)
                .SetMessage("An unexpected error occurred.")
                .SetCode("UNEXPECTED_ERROR")
                .Build();
        }

        var code = ex.StatusCode switch
        {
            HttpStatusCode.Conflict => "EMAIL_ALREADY_EXISTS",
            HttpStatusCode.BadRequest => "BAD_REQUEST",
            HttpStatusCode.NotFound => "NOT_FOUND",
            HttpStatusCode.Unauthorized => "UNAUTHORIZED",
            HttpStatusCode.Forbidden => "FORBIDDEN",
            _ => "DISPRZ_ERROR"
        };
        return ErrorBuilder.FromError(error)
            .SetMessage(ex.Message)
            .SetCode(code)
            .SetExtension("statusCode", (int)ex.StatusCode)
            .RemoveExtension("stackTrace")
            .Build();
    }
}