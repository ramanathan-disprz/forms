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
        builder.Services.AddScoped<FormSubmissionQuery>();

        // Add root mutation type
        builder.AddQueryType<Query>();

        // Add extensions
        builder.AddTypeExtension<UserQuery>();
        builder.AddTypeExtension<FormQuery>();
        builder.AddTypeExtension<QuestionQuery>();
        builder.AddTypeExtension<FormSubmissionQuery>();

        return builder;
    }
}