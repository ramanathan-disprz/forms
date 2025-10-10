using System.Diagnostics.CodeAnalysis;
using forms.Repository.Implementations;
using forms.Repository.Interfaces;

namespace forms.Utils;

public static class RepositoryCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection repositories)
    {
        repositories.AddScoped<IUserRepository, UserRepository>();
        repositories.AddScoped<IFormRepository, FormRepository>();
        return repositories;
    }
}