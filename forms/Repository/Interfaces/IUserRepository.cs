using forms.Model;
using forms.Repository.SQLRepository;

namespace forms.Repository.Interfaces;

public interface IUserRepository : ISQLRepository<User>
{
    bool ExistsByEmail(string? email);

    User? FindByEmail(string? email);

    User FindByEmailOrThrow(string? email);
}