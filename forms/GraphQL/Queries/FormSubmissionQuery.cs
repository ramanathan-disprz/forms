using AutoMapper;
using forms.AWS;
using forms.Dto.FormSubmission;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class FormSubmissionQuery
{
    private readonly IMapper _mapper;
    private readonly IFormSubmissionService _service;

    public FormSubmissionQuery(IMapper mapper,
        IFormSubmissionService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [GraphQLName("indexFormSubmissionByFormId")]
    public IEnumerable<FormSubmissionDto> IndexFormSubmissionByFormId(string formId)
    {
        var submissions = _service.IndexByFormId(formId);
        return _mapper.Map<IEnumerable<FormSubmissionDto>>(submissions);
    }

    [GraphQLName("indexFormSubmissionByUserId")]
    public IEnumerable<FormSubmissionDto> IndexFormSubmissionByUserId(long userId)
    {
        var submissions = _service.IndexByUserId(userId);
        return _mapper.Map<IEnumerable<FormSubmissionDto>>(submissions);
    }

    [GraphQLName("fetchFormSubmission")]
    public FormSubmissionDetailDto FetchFormSubmission(long id, bool includeAnswers = false)
    {
        var submissionDetail = _service.Fetch(id, includeAnswers);
        return _mapper.Map<FormSubmissionDetailDto>(submissionDetail);
    }

    [GraphQLName("generatePreSignedDownloadUrl")]
    public string GeneratePreSignedDownloadUrl(string fileName)
    {
        var key = $"uploads/{fileName}";
        return "";
        // return _s3Helper.GeneratePreSignedDownloadUrl(key);
    }
}