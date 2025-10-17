using forms.Model.FormSubmission;
using forms.Request.FormSubmission;

namespace forms.Service.Interface;

public interface IFormSubmissionService
{
    FormSubmissionDetail Fetch(long id,  bool includeAnswers);

    void SubmitForm(FormSubmissionRequest request);

    void Delete(long id);
}