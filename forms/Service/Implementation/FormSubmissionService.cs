using AutoMapper;
using forms.Exception;
using forms.Model.FormSubmission;
using forms.Repository.Interfaces;
using forms.Request.FormSubmission;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class FormSubmissionService : IFormSubmissionService
{
    private readonly IMapper _mapper;
    private readonly ILogger<FormSubmissionService> _log;
    private readonly IFormSubmissionRepository _repository;

    private readonly IFormAnswerService _answerService;

    public FormSubmissionService(IMapper mapper,
        ILogger<FormSubmissionService> log,
        IFormSubmissionRepository repository,
        IFormAnswerService answerService)
    {
        _mapper = mapper;
        _log = log;
        _repository = repository;
        _answerService = answerService;
    }

    public FormSubmissionDetail Fetch(long id, bool includeAnswers = false)
    {
        _log.LogInformation("Fetch form submission with id: {Id}", id);

        var submission = _repository.FindOrThrow(id);

        IEnumerable<FormAnswer> answers = Enumerable.Empty<FormAnswer>();

        if (includeAnswers)
        {
            _log.LogInformation("Including answers for submission id: {Id}", id);
            answers = _answerService.IndexBySubmissionId(id);
        }

        return new FormSubmissionDetail
        {
            Submission = submission,
            Answers = answers
        };
    }

    public void SubmitForm(FormSubmissionRequest request)
    {
        _log.LogInformation("Submit Form");
        var submission = _mapper.Map<FormSubmission>(request);
        submission.GenerateId();
        submission = _repository.Create(submission);
        var answers = request.Answers;
        SaveAnswers(submission.Id, answers);
    }

    public void Delete(long id)
    {
        _log.LogInformation("Delete form submission with id  : {id} ", id);
        var submission = Fetch(id).Submission;
        _repository.Delete(submission);
    }

    private void SaveAnswers(long id,
        IEnumerable<FormAnswerRequest>? requests)
    {
        try
        {
            if (requests == null || !requests.Any())
                throw new EntitySaveException($"No answers found for the submission {id}");
            requests.Select(request => request.SubmissionId = id);
            var answers = _answerService.CreateMany(requests);
        }
        catch (System.Exception ex)
        {
            _log.LogError("Exception {ex} occured.", ex.Message);
            _log.LogInformation("Rolling back");
            Delete(id);
        }
    }
}