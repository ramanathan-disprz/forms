using forms.Dto.FormAuthoring;
using forms.Mapping;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class QuestionQuery
{
    private readonly IQuestionService _service;
    private readonly QuestionMapper _mapper;

    public QuestionQuery(IQuestionService service, QuestionMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [GraphQLName("indexQuestionsByFormId")]
    public IEnumerable<QuestionDto> IndexQuestionsByFormId(string formId)
    {
        var questions = _service.IndexByFormId(formId);
        return _mapper.Map(questions);
    }

    [GraphQLName("fetchQuestion")]
    public QuestionDto FetchQuestion(string id)
    {
        var question = _service.Fetch(id);
        return _mapper.Map(question);
    }
}