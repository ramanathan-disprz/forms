using forms.Enum;
using forms.Model.FormAuthoring;

namespace forms.Request.FormAuthoring;

public class FormRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long? PublishedBy { get; set; }
    public DateTime? PublishedDate { get; set; }
    public FormStatus? FormStatus { get; set; } = Enum.FormStatus.Draft;
    public FormViewStatus? FormViewStatus { get; set; } = Enum.FormViewStatus.Enabled;
    public int? QuestionLimit { get; set; } = 20;
    public bool? AllowMultipleResponses { get; set; } = false;
    
    // questions
    public IEnumerable<QuestionRequest>? Questions { get; set; }
}