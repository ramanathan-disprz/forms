namespace forms.Model.FormSubmission;

public class FormSubmissionDetail
{
    public FormSubmission? Submission { get; set; }
    public IEnumerable<FormAnswer>? Answers { get; set; }
}