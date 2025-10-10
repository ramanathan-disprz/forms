using forms.Enum;
using forms.Model.FormAuthoring;

namespace forms.Request;

public class FormRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long? PublishedBy { get; set; }
    public DateTime? PublishedDate { get; set; }
    public FormStatus FormStatus { get; set; }
    public FormViewStatus FormViewStatus { get; set; }
    public int QuestionLimit { get; set; }
    public bool AllowMultipleResponses { get; set; }
    public List<BaseQuestion>? Questions { get; set; }
}