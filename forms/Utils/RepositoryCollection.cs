using System.Diagnostics.CodeAnalysis;
using forms.Repository.Implementations;
using forms.Repository.Interfaces;

namespace forms.Utils;

[ExcludeFromCodeCoverage]
public static class RepositoryCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection repositories)
    {
        repositories.AddScoped<IUserRepository, UserRepository>();
        return repositories;
    }
}