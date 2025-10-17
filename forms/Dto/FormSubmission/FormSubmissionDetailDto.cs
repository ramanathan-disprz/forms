using forms.Model.FormSubmission;

namespace forms.Dto.FormSubmission;

public class FormSubmissionDetailDto
{
    public Model.FormSubmission.FormSubmission? Submission { get; set; }
    public IEnumerable<FormAnswer>? Answers { get; set; }
}