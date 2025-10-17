using AutoMapper;
using forms.Request.FormSubmission;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class FormSubmissionMutation
{
    private readonly IMapper _mapper;
    private readonly IFormSubmissionService _service;
    private readonly ILogger<FormSubmissionMutation> _log;

    public FormSubmissionMutation(IMapper mapper,
        IFormSubmissionService service,
        ILogger<FormSubmissionMutation> log)
    {
        _mapper = mapper;
        _service = service;
        _log = log;
    }

    public bool SubmitForm(FormSubmissionRequest request)
    {
        _service.SubmitForm(request);
        return true;
    }

    public bool DeleteFormSubmission(long id)
    {
        _service.Delete(id);
        return true;
    }
}