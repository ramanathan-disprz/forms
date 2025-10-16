namespace forms.Model.FormAuthoring;

public class FormWithQuestions
{
    public Form form { get; set; }
    public IEnumerable<Question> questions { get; set; }
}