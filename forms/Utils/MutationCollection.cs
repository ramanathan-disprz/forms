using forms.GraphQL.Mutations;
using HotChocolate.Execution.Configuration;

namespace forms.Utils;

public static class MutationCollection
{
    public static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder)
    {
        // Register services
        builder.Services.AddScoped<AuthMutation>();
        builder.Services.AddScoped<UserMutation>();
        builder.Services.AddScoped<FormMutation>();
        builder.Services.AddScoped<QuestionMutation>();
        builder.Services.AddScoped<FormSubmissionMutation>();

        // Add root mutation type
        builder.AddMutationType<Mutation>();

        // Add extensions
        builder.AddTypeExtension<AuthMutation>();
        builder.AddTypeExtension<UserMutation>();
        builder.AddTypeExtension<FormMutation>();
        builder.AddTypeExtension<QuestionMutation>();
        builder.AddTypeExtension<FormSubmissionMutation>();

        return builder;
    }
}