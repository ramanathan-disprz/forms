using forms.Enum;

namespace forms.Request.FormAuthoring;

public class QuestionRequest
{
    public string? Id { get; set; }
    public string? FormId { get; set; }
    public QuestionType? Type { get; set; }
    public string? QuestionText { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Placeholder { get; set; }
    public bool? Required { get; set; } = false;
    public int? Order { get; set; }

    /* Short Text and Long Text Questions */
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }

    /* Date Question */
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }

    /* File Question */
    public string[]? AllowedFileTypes { get; set; }
    public long? MaxFileSizeMB { get; set; }
    public long? MaxTotalFileSizeMB { get; set; }
    public int? MaxFiles { get; set; }

    /* Number Question */
    public int? MinValue { get; set; } = Int32.MinValue;
    public int? MaxValue { get; set; } = Int32.MaxValue;

    /* Select Question */
    public List<Option>? Options { get; set; }
    public bool? MultiSelect { get; set; }
}

public class Option
{
    public string? Id { get; set; }
    public string? Label { get; set; }
    public string? Value { get; set; }
}