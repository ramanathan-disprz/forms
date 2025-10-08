using forms.Model;
using forms.Repository.CrudRepository;

namespace forms.Repository.Interfaces;

public interface IUserRepository : ICrudRepository<User>
{
    bool ExistsByEmail(string? email);

    User? FindByEmail(string? email);

    User FindByEmailOrThrow(string? email);
}