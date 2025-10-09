using AutoMapper;
using forms.Dto;
using forms.Request;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class AuthMutation
{
    private readonly IMapper _mapper;
    private readonly IAuthService _service;
    private readonly ILogger<AuthMutation> _logger;

    public AuthMutation(IMapper mapper, IAuthService authService, ILogger<AuthMutation> logger)
    {
        _mapper = mapper;
        _service = authService;
        _logger = logger;
    }

    public UserDto RegisterUser(UserRequest request)
    {
        var user = _service.Register(request);
        return _mapper.Map<UserDto>(user);
    }
}