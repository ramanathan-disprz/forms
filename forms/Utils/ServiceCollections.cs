using System.Diagnostics.CodeAnalysis;
using forms.Service.Implementation;
using forms.Service.Interface;

namespace forms.Utils;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}