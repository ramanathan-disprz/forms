using System.Text.Json;
using AutoMapper;
using forms.Model;
using forms.Repository.Interfaces;
using forms.Request;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _log;
    private readonly IUserRepository _repository;

    public UserService(IMapper mapper, ILogger<UserService> log, IUserRepository repository)
    {
        _log = log;
        _mapper = mapper;
        _repository = repository;
    }

    public IEnumerable<User> Index()
    {
        _log.LogInformation("Find all users");
        return _repository.FindAll();
    }

    public User Fetch(long id)
    {
        _log.LogInformation("Find user with id : {userId}", id);
        return _repository.FindOrThrow(id);
    }

    public User Update(long id, UserRequest request)
    {
        _log.LogInformation("Updating user with id: {UserId} " +
                            "and request: {userRequest}", id, JsonSerializer.Serialize(request));
        var user = Fetch(id);
        user = _mapper.Map(request, user);
        return _repository.Update(user);
    }

    public void Delete(long id)
    {
        _log.LogInformation("Delete user with id : {userId}", id);
        var user = Fetch(id);
        _repository.Delete(user);
    }
}