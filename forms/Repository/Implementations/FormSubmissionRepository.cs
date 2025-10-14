using forms.Data;
using forms.Model;
using forms.Model.FormSubmission;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class FormSubmissionRepository : SQLRepository.SQLRepository<FormSubmission>, IFormSubmissionRepository
{
    private readonly ApplicationDbContext _context;

    public FormSubmissionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}