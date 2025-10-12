namespace forms.Request;

public class DropdownRequest : BaseQuestionRequest
{
    public List<OptionRequest>? Options { get; set; }
    public bool? MultiSelect { get; set; }
}

public class OptionRequest
{
    public string? Id { get; set; }
    public string? Label { get; set; }
    public string? Value { get; set; }
}