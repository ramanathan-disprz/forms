using AutoMapper;
using forms.Dto.FormSubmission;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class FormSubmissionQuery
{
    private readonly IMapper _mapper;
    private readonly IFormSubmissionService _service;

    public FormSubmissionQuery(IMapper mapper, IFormSubmissionService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [GraphQLName("fetchFormSubmission")]
    public FormSubmissionDetailDto FetchFormSubmission(long id, bool includeAnswers = false)
    {
        var submissionDetail = _service.Fetch(id, includeAnswers);
        return _mapper.Map<FormSubmissionDetailDto>(submissionDetail);
    }
}