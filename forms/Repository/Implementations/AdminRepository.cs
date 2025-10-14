using forms.Data;
using forms.Model;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class AdminRepository : SQLRepository.SQLRepository<Admin>, IAdminRepository
{
    private readonly ApplicationDbContext _context;

    public AdminRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}