using forms.Dto;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

public class UserQuery
{
    private readonly IUserService _userService;

    public UserQuery(IUserService userService)
    {
        _userService = userService;
    }

    public UserDto GetUserById(long id)
    {
        return new UserDto();
    }
}