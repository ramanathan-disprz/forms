using forms.Model.FormSubmission;
using forms.Request.FormSubmission;

namespace forms.Service.Interface;

public interface IFormAnswerService
{
    IEnumerable<FormAnswer> IndexBySubmissionId(long submissionId);
    IEnumerable<FormAnswer> CreateMany(IEnumerable<FormAnswerRequest> requests);
}