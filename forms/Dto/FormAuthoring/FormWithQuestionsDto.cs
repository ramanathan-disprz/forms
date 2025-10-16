namespace forms.Dto.FormAuthoring;

public class FormWithQuestionsDto
{
    public FormDto form { get; set; }
    
    public IEnumerable<QuestionDto> questions { get; set; }
}