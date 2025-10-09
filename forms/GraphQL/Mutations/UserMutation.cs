using AutoMapper;
using forms.Dto;
using forms.Request;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class UserMutation
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserMutation(IMapper mapper, IUserService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public UserDto UpdateUser(long id, UserRequest request)
    {
        var user = _service.Update(id, request);
        return _mapper.Map<UserDto>(user);
    }

    public void DeleteUser(long id)
    {
        _service.Delete(id);
    }
}