using forms.Model.FormSubmission;

namespace forms.Dto.FormSubmission;

public class FormSubmissionDetailDto
{
    public FormSubmissionDto? Submission { get; set; }
    public IEnumerable<FormAnswerDto>? Answers { get; set; }
}