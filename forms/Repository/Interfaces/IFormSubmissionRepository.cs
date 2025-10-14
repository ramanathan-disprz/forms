using forms.Model.FormSubmission;
using forms.Repository.SQLRepository;

namespace forms.Repository.Interfaces;

public interface IFormSubmissionRepository : ISQLRepository<FormSubmission>
{
}