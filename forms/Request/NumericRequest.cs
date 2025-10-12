namespace forms.Request;

public class NumericRequest : BaseQuestionRequest
{
    public int? MinValue { get; set; } = Int32.MinValue;
    public int? MaxValue { get; set; } = Int32.MaxValue;
}