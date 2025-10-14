using forms.Model;

namespace forms.Service.Interface;

public interface IAdminService
{
    IEnumerable<Admin> GetAll();

    Admin Fetch(long userId);
}