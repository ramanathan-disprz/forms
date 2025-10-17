namespace forms.Dto.FormSubmission;

public class FormAnswerDto
{
    public long? Id { get; set; }
    public long? SubmissionId { get; set; }
    public string? QuestionId { get; set; }
    public string? QuestionType { get; set; }
    public string? ValueText { get; set; }
    public string? ValueJson { get; set; }
}