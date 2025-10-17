namespace forms.Dto.FormSubmission;

public class FormSubmissionDto
{
    public string? Id { get; set; }
    public string? FormId { get; set; }
    public long? UserId { get; set; }
    public DateTime? SubmittedAt { get; set; }
    
    public IEnumerable<FormAnswerDto>? Answers { get; set; } 
}