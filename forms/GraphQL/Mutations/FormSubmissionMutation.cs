using AutoMapper;
using forms.AWS;
using forms.Request.FormSubmission;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class FormSubmissionMutation
{
    private readonly IMapper _mapper;
    private readonly AmazonS3Helper _s3Helper;
    private readonly IFormSubmissionService _service;
    private readonly ILogger<FormSubmissionMutation> _log;

    public FormSubmissionMutation(IMapper mapper,
        AmazonS3Helper s3Helper,
        IFormSubmissionService service,
        ILogger<FormSubmissionMutation> log)
    {
        _mapper = mapper;
        _s3Helper = s3Helper;
        _service = service;
        _log = log;
    }

    [GraphQLName("generatePreSignedUploadUrl")]
    public string GeneratePreSignedUploadUrl(string fileName, string contentType)
    {
        // later incorporate some logic to make the paths unique
        var key = $"uploads/{fileName}";
        return _s3Helper.GeneratePreSignedUploadUrl(key, contentType);
    }

    [GraphQLName("submitForm")]
    public bool SubmitForm(FormSubmissionRequest request)
    {
        _service.SubmitForm(request);
        return true;
    }

    [GraphQLName("deleteFormSubmission")]
    public bool DeleteFormSubmission(long id)
    {
        _service.Delete(id);
        return true;
    }
}