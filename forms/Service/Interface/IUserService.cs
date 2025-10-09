using forms.Model;
using forms.Request;

namespace forms.Service.Interface;

public interface IUserService
{
    User Create(UserRequest request);
    
}