using forms.GraphQL.Queries;
using HotChocolate.Execution.Configuration;

namespace forms.Utils;

public static class QueryCollection
{
    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder)
    {
        // Register query as SCOPED service first
        builder.Services.AddScoped<UserQuery>();
        
        
        // Then add as query type
        builder.AddQueryType<UserQuery>();
        
        return builder;
    }
}