namespace forms.Request;

public class DatePickerRequest : BaseQuestionRequest
{
    public string? MinDate { get; set; }
    public string? MaxDate { get; set; }
}