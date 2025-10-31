using forms.Dto.FormAuthoring;
using forms.Model.FormAuthoring;
using forms.Request.FormAuthoring;

namespace forms.Mapping;

public interface IQuestionMapper
{
    Question Map(QuestionRequest request);
    IEnumerable<Question> Map(IEnumerable<QuestionRequest> requests);

    QuestionDto Map(Question question);

    public IEnumerable<QuestionDto> Map(IEnumerable<Question> questions);

    Question Merge(Question question, QuestionRequest request);

    IEnumerable<Question> Merge(IEnumerable<QuestionRequest> requests);
}