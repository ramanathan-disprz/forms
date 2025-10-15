using AutoMapper;
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

    public FormQuestion Fetch(string id)
    {
        _log.LogInformation("Fetch question with id : {id}", id);
        return _repository.FindOrThrow(id);
    }

    public FormQuestion Create(FormQuestionRequest request)
    {
        _log.LogInformation("Create question for the form with id {Id} ", request.FormId);
        var question =_questionMapper.Map(request);
        return _repository.Create(question);
    }

    public IEnumerable<FormQuestion> CreateMany(IEnumerable<FormQuestionRequest> requests)
    {
        _log.LogInformation("Create {Count} questions for form id {FormId}", requests.Count(),
            requests.First().FormId);
        var questions = _questionMapper.Map(requests);
        return _repository.CreateMany(questions);
    }

    public FormQuestion Update(string id, FormQuestionRequest request)
    {
        _log.LogInformation("Update question with id: {formId} ", id);
        var question = Fetch(id);
        question = _questionMapper.Merge(question, request);
        return _repository.Update(id, question);
    }

    public IEnumerable<FormQuestion> UpdateMany(IEnumerable<FormQuestionRequest> requests)
    {
        _log.LogInformation("Update {Count} questions for form id {FormId}", requests.Count(),
            requests.First().FormId);
        var questions = _questionMapper.Merge(requests);
        return _repository.UpdateMany(questions, q => q.Id);
    }
}