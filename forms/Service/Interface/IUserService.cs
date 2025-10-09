using forms.Model;
using forms.Request;

namespace forms.Service.Interface;

public interface IUserService
{
    IEnumerable<User> Index();

    User Fetch(long id);

    User Update(long id, UserRequest request);

    void Delete(long id);
}