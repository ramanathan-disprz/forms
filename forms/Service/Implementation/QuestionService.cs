using forms.Mapping;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request.FormAuthoring;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class QuestionService : IQuestionService
{
    private readonly QuestionMapper _questionMapper;
    private readonly ILogger<QuestionService> _log;
    private readonly IQuestionRepository _repository;

    public QuestionService(QuestionMapper questionMapper,
        ILogger<QuestionService> log, IQuestionRepository repository)
    {
        _questionMapper = questionMapper;
        _log = log;
        _repository = repository;
    }

    public IEnumerable<Question> IndexByFormId(string formId)
    {
        _log.LogInformation("Index questions with form id: {formId}", formId);
        return _repository.IndexByFormId(formId);
    }

    public Question Fetch(string id)
    {
        _log.LogInformation("Fetch question with id : {id}", id);
        return _repository.FindOrThrow(id);
    }

    public Question Create(QuestionRequest request)
    {
        _log.LogInformation("Create question for the form with id {Id} ", request.FormId);
        var question = _questionMapper.Map(request);
        return _repository.Create(question);
    }

    public IEnumerable<Question> CreateMany(IEnumerable<QuestionRequest> requests)
    {
        _log.LogInformation("Create {Count} questions for form id {FormId}", requests.Count(),
            requests.First().FormId);
        var questions = _questionMapper.Map(requests);
        return _repository.CreateMany(questions);
    }

    public Question Update(string id, QuestionRequest request)
    {
        _log.LogInformation("Update question with id: {formId} ", id);
        var question = Fetch(id);
        question = _questionMapper.Merge(question, request);
        return _repository.Update(id, question);
    }

    public IEnumerable<Question> UpdateMany(IEnumerable<QuestionRequest> requests)
    {
        _log.LogInformation("Update {Count} questions for form id {FormId}", requests.Count(),
            requests.First().FormId);
        var questions = _questionMapper.Merge(requests);
        return _repository.UpdateMany(questions, q => q.Id);
    }

    public void Delete(string id)
    {
        _log.LogInformation("Delete question with id : {id}", id);
        _repository.Delete(id);
    }

    public void DeleteByFormId(string formId)
    {
        _log.LogInformation("Delete questions with form id: {formId} ", formId);
        _repository.DeleteByFormId(formId);
    }
}