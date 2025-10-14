using forms.Model;
using forms.Repository.SQLRepository;

namespace forms.Repository.Interfaces;

public interface IAdminRepository : ISQLRepository<Admin>
{
}