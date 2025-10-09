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

        // Add root mutation type
        builder.AddMutationType<Mutation>();

        // Add extensions
        builder.AddTypeExtension<AuthMutation>();
        builder.AddTypeExtension<UserMutation>();

        return builder;
    }
}