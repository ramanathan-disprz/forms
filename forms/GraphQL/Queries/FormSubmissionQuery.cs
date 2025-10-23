using AutoMapper;
using forms.AWS;
using forms.Dto.FormSubmission;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class FormSubmissionQuery
{
    private readonly IMapper _mapper;
    private readonly AmazonS3Helper _s3Helper;
    private readonly IFormSubmissionService _service;

    public FormSubmissionQuery(IMapper mapper,
        AmazonS3Helper s3Helper,
        IFormSubmissionService service)
    {
        _mapper = mapper;
        _s3Helper = s3Helper;
        _service = service;
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
        return _s3Helper.GeneratePreSignedDownloadUrl(key);
    }
}