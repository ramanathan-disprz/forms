using AutoMapper;
using forms.Model.FormSubmission;
using forms.Repository.Interfaces;
using forms.Request.FormSubmission;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class FormAnswerService : IFormAnswerService
{
    private readonly IMapper _mapper;
    private readonly ILogger<FormAnswerService> _log;
    private readonly IFormAnswerRepository _repository;

    public FormAnswerService(IMapper mapper, ILogger<FormAnswerService> log, IFormAnswerRepository repository)
    {
        _mapper = mapper;
        _log = log;
        _repository = repository;
    }

    public IEnumerable<FormAnswer> IndexBySubmissionId(long submissionId)
    {
        _log.LogInformation("Index answers with submission id : {id} ", submissionId);
        return _repository.IndexAllBySubmissionId(submissionId);
    }

    public IEnumerable<FormAnswer> CreateMany(IEnumerable<FormAnswerRequest> requests)
    {
        var answers = _mapper.Map<IEnumerable<FormAnswer>>(requests);
        _log.LogInformation("Create {Count} answers for submission id {id}", answers.Count(),
            answers.First().SubmissionId);
        return _repository.CreateMany(answers);
    }
}