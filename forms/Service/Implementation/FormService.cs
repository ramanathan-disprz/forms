using System.Text.Json;
using AutoMapper;
using forms.Mapping;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request;
using forms.Request.FormAuthoring;
using forms.Service.Interface;

namespace forms.Service.Implementation;

public class FormService : IFormService
{
    private readonly IMapper _mapper;
     
    private readonly ILogger<FormService> _log;
    private readonly IFormRepository _repository;

    public FormService(IMapper mapper, ILogger<FormService> log, IFormRepository repository)
    {
        _log = log;
        _mapper = mapper;
        _repository = repository;
    }

    public Form Fetch(string id)
    {
        _log.LogInformation("Find form with id : {formId}", id);
        return _repository.FindOrThrow(id);
    }

    public Form Create(FormRequest request)
    {
        _log.LogInformation("Create form : {request}",
            JsonSerializer.Serialize(request));
        var form = FormMapper.MapToForm(request);
        return _repository.Create(form);
    }

    public Form UpdateForm(string id, FormRequest request)
    {
        _log.LogInformation("Updating form with id: {formId} " +
                            "and request: {request}", id, JsonSerializer.Serialize(request));

        var form = Fetch(id);
        form = _mapper.Map(request, form);
        return _repository.Update(id, form);
    }
}