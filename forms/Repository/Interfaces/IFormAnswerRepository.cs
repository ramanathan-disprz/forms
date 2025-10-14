using forms.Model;
using forms.Model.FormSubmission;
using forms.Repository.SQLRepository;

namespace forms.Repository.Interfaces;

public interface IFormAnswerRepository : ISQLRepository<FormAnswer>
{
}