using System.Text.Json;
using AutoMapper;
using forms.Exception;
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

    public User Create(UserRequest request)
    {
        _log.LogInformation("Create new user : {userRequest}", JsonSerializer.Serialize(request));
        ValidateEmail(request.Email);
        var user = _mapper.Map<User>(request);
        user.GenerateId();
        return _repository.Create(user);
    }

    private void ValidateEmail(string? email)
    {
        var existsByEmail = _repository.ExistsByEmail(email);
        if (existsByEmail)
            throw new ConflictException($"User with email : {email} already exists");
    }
}