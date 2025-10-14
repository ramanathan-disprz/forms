namespace forms.Request.FormSubmission;

public class FormSubmissionRequest
{
    public string? FormId { get; set; }
    public long? UserId { get; set; }
    public List<FormAnswerRequest>? Answers { get; set; }
}