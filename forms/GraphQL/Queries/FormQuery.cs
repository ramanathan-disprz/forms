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

    public FormQuery(IMapper mapper, IFormService service)
    {
        _mapper = mapper;
        _service = service;
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
        var form = _service.FetchWithQuestions(id);
        return _mapper.Map<FormWithQuestionsDto>(form);
    }
}