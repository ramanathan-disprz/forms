using AutoMapper;
using forms.Dto;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

public class UserQuery
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;
    private readonly ILogger<UserQuery> _logger;

    public UserQuery(IMapper mapper, IUserService service, ILogger<UserQuery> logger)
    {
        _mapper = mapper;
        _service = service;
        _logger = logger;
    }

    public string Hello() => "Hello Ram!";

    public IEnumerable<UserDto> IndexUser()
    {
        _logger.LogInformation("Find all users");
        var users = _service.Index();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
    
    
}