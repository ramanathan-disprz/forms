using forms.Data;
using forms.Model;
using forms.Model.FormSubmission;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class FormAnswerRepository : SQLRepository.SQLRepository<FormAnswer>, IFormAnswerRepository
{
    private readonly ApplicationDbContext _context;
    public FormAnswerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
       
}