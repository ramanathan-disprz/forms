using forms.GraphQL.Queries;
using HotChocolate.Execution.Configuration;

namespace forms.Utils;

public static class QueryCollection
{
    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder)
    {
        // Register services
        builder.Services.AddScoped<UserQuery>();
        builder.Services.AddScoped<FormQuery>();
        builder.Services.AddScoped<QuestionQuery>();

        // Add root mutation type
        builder.AddQueryType<Query>();

        // Add extensions
        builder.AddTypeExtension<UserQuery>();
        builder.AddTypeExtension<FormQuery>();
        builder.AddTypeExtension<QuestionQuery>();

        return builder;
    }
}