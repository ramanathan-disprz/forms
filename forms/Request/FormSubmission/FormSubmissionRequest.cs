namespace forms.Request.FormSubmission;

public class FormSubmissionRequest
{
    public string? FormId { get; set; }
    public long? UserId { get; set; }

    public IEnumerable<FormAnswerRequest>? Answers { get; set; }
}