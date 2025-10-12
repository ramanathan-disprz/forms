namespace forms.Request;

public class LongAnswerRequest : BaseQuestionRequest
{
    public int? MaxLength { get; set; }
    public int? MinLength { get; set; }
}