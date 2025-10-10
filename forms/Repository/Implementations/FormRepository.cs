using forms.Data;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class FormRepository : NoSQLRepository.NoSQLRepository<Form>, IFormRepository
{
    private readonly MongoDbContext _context;

    public FormRepository(MongoDbContext context) : base(context)
    {
        _context = context;
    }
}