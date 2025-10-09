using AutoMapper;
using forms.Dto;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class UserQuery
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserQuery(IMapper mapper, IUserService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public string Hello() => "Hello Ram!";

    public IEnumerable<UserDto> IndexUser()
    {
        var users = _service.Index();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public UserDto FetchUser(long id)
    {
        var user = _service.Fetch(id);
        return _mapper.Map<UserDto>(user);
    }
}