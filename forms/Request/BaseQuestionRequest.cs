using forms.Enum;

namespace forms.Request;

public abstract class BaseQuestionRequest
{
    public QuestionType? Type { get; set; }
    public string? QuestionText { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Placeholder { get; set; }
    public bool? Required { get; set; } = false;
    public int? Order { get; set; }
    public object? DefaultValue { get; set; }
    public object? Answers { get; set; }
}