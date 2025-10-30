using forms.Enum;

namespace forms.Request.FormSubmission;

public class FormAnswerRequest
{
    public string? QuestionId { get; set; }
    public QuestionType? QuestionType { get; set; }
    public long?  SubmissionId {get; set;}

    // Depending on type, either text or JSON
    public string? ValueText { get; set; }
    public string? ValueJson { get; set; }
}