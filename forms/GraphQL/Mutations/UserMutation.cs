using AutoMapper;
using forms.Dto;
using forms.Request;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

public class UserMutation
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserMutation(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _service = userService;
    }

    public UserDto CreateUser(UserRequest request)
    {
        var user = _service.Create(request);
        return _mapper.Map<UserDto>(user);
    }
}