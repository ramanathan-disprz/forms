using forms.Mapping;
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

        // Authoring Services
        services.AddScoped<IFormService, FormService>();
        services.AddScoped<IQuestionService, QuestionService>();

        // Submission Services
        services.AddScoped<IFormSubmissionService, FormSubmissionService>();
        services.AddScoped<IFormAnswerService, FormAnswerService>();

        // Mapper
        services.AddScoped<QuestionMapper>();
        
        return services;
    }
}