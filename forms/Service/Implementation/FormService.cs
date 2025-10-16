using AutoMapper;
using forms.Dto.FormAuthoring;
using forms.Enum;
using forms.Exception;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request.FormAuthoring;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class FormService : IFormService
{
    private readonly IMapper _mapper;
    private readonly ILogger<FormService> _log;
    private readonly IFormRepository _repository;

    private readonly IQuestionService _questionService;

    public FormService(IMapper mapper, ILogger<FormService> log, IFormRepository repository,
        IQuestionService questionService)
    {
        _log = log;
        _mapper = mapper;
        _repository = repository;
        _questionService = questionService;
    }

    public IEnumerable<Form> Index()
    {
        _log.LogInformation("Index forms");
        return _repository.FindAll();
    }

    public Form Fetch(string id)
    {
        _log.LogInformation("Fetch form with id : {formId}", id);
        return _repository.FindOrThrow(id);
    }

    public FormWithQuestions FetchWithQuestions(string id)
    {
        var form = Fetch(id);
        return BuildFormWithQuestions(form);
    }

    public Form Create(FormRequest request)
    {
        _log.LogInformation("Create form");
        var form = _mapper.Map<Form>(request);
        form = _repository.Create(form);
        AddQuestions(form, request);
        return form;
    }

    public Form Update(string id, FormRequest request)
    {
        _log.LogInformation("Update form with id: {formId} ", id);
        var form = Fetch(id);
        ValidateForm(form);
        form = _mapper.Map(request, form);
        UpdateQuestions(form, request);
        return _repository.Update(id, form);
    }

    public void Delete(string id)
    {
        _log.LogInformation("Delete form with id : {formId}", id);
        DeleteQuestions(id);
        _repository.Delete(id);
    }

    private FormWithQuestions BuildFormWithQuestions(Form form)
    {
        var questions = _questionService.IndexByFormId(form.Id);
        return new FormWithQuestions
        {
            form = form,
            questions = questions
        };
    }

    private void CreateFormQuestions(Form form, IEnumerable<QuestionRequest> questionRequests)
    {
        string? formId = form.Id;
        if (formId == null)
            return;
        foreach (var q in questionRequests)
        {
            q.FormId = formId;
        }

        var questions = _questionService.CreateMany(questionRequests);
    }

    private void AddQuestions(Form form, FormRequest request)
    {
        if (request.Questions == null || !request.Questions.Any())
            return;
        CreateFormQuestions(form, request.Questions);
    }

    private void ValidateForm(Form form)
    {
        if (form.FormStatus == FormStatus.Published)
            throw new ConflictException("Form cannot be changed further, Ready to accept responses");
    }

    private void UpdateFormQuestions(Form form, IEnumerable<QuestionRequest> questionRequests)
    {
        string? formId = form.Id;
        if (formId == null)
            return;
        foreach (var q in questionRequests)
        {
            q.FormId = formId;
        }

        var questions = _questionService.UpdateMany(questionRequests);
    }

    private void UpdateQuestions(Form form, FormRequest request)
    {
        if (request.Questions == null || !request.Questions.Any())
            return;

        var updateRequests = request.Questions
            .Where(q => !string.IsNullOrEmpty(q.Id));

        var newRequests = request.Questions
            .Where(q => string.IsNullOrEmpty(q.Id));

        if (updateRequests.Any())
        {
            UpdateFormQuestions(form, updateRequests);
        }

        if (newRequests.Any())
        {
            CreateFormQuestions(form, newRequests);
        }
    }

    private void DeleteQuestions(string id)
    {
        _questionService.DeleteByFormId(id);
    }
}