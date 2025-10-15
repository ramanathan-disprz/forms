using forms.Repository.Implementations;
using forms.Repository.Interfaces;

namespace forms.Utils;

public static class RepositoryCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection repositories)
    {
        // User Repositories
        repositories.AddScoped<IUserRepository, UserRepository>();
        repositories.AddScoped<IAdminRepository, AdminRepository>();

        repositories.AddScoped<IFormRepository, FormRepository>();
        repositories.AddScoped<IQuestionRepository, QuestionRepository>();

        // Response Repositories
        repositories.AddScoped<IFormSubmissionRepository, FormSubmissionRepository>();
        repositories.AddScoped<IFormAnswerRepository, FormAnswerRepository>();
        return repositories;
    }
}