using forms.Service.Implementation;
using forms.Service.Interface;
using forms.Utils.Security;

namespace forms.Utils;

public static class ServiceCollection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // User Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        services.AddScoped<IFormService, FormService>();

        // Response Services
        services.AddScoped<IFormSubmissionService, FormSubmissionService>();
        return services;
    }
}