using AutoMapper;
using forms.Dto;
using forms.Dto.FormAuthoring;
using forms.Mapping;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class FormQuery
{
    private readonly IMapper _mapper;
    private readonly IFormService _service;
    private readonly QuestionMapper _questionMapper;

    public FormQuery(IMapper mapper, IFormService service, QuestionMapper questionMapper)
    {
        _mapper = mapper;
        _service = service;
        _questionMapper = questionMapper;
    }

    [GraphQLName("indexForms")]
    public IEnumerable<FormDto> IndexForms()
    {
        var forms = _service.Index();
        return _mapper.Map<IEnumerable<FormDto>>(forms);
    }

    [GraphQLName("fetchForm")]
    public FormDto FetchForm(string id)
    {
        var form = _service.Fetch(id);
        return _mapper.Map<FormDto>(form);
    }

    [GraphQLName("fetchFormWithQuestions")]
    public FormWithQuestionsDto FetchFormWithQuestions(string id)
    {
        var formWithQuestions = _service.FetchWithQuestions(id);
        var form = _mapper.Map<FormWithQuestionsDto>(formWithQuestions)?.form;
        var questions = _questionMapper.Map(formWithQuestions.questions);

        return new FormWithQuestionsDto
        {
            form = form,
            questions = questions
        };
    }
}