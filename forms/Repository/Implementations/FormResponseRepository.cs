using forms.Data;
using forms.Model;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class FormResponseRepository : SQLRepository.SQLRepository<FormResponse>, IFormResponseRepository
{
    private readonly ApplicationDbContext _context;

    public FormResponseRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}