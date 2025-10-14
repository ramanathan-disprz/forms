namespace forms.Request.FormSubmission;

public class FormAnswerRequest
{
    public string? QuestionId { get; set; }
    public string? QuestionType { get; set; }
    public string? ValueText { get; set; }
    public string? ValueJson { get; set; }
}