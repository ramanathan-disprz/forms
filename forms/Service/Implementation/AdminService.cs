using forms.Model;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class AdminService : IAdminService
{
    private readonly ILogger<AdminService> _log;
    private readonly IAdminRepository _repository;

    public AdminService(ILogger<AdminService> log, IAdminRepository repository)
    {
        _log = log;
        _repository = repository;
    }

    public IEnumerable<Admin> GetAll()
    {
        _log.LogInformation("Find all admins");
        return _repository.FindAll();
    }

    public Admin Fetch(long userId)
    {
        _log.LogInformation("Find admin with user id  : {userId}", userId);
        return _repository.FindOrThrow(userId);
    }
}