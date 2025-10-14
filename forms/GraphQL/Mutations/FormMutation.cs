using AutoMapper;
using forms.Dto;
using forms.Dto.FormAuthoring;
using forms.Mapping;
using forms.Request;
using forms.Request.FormAuthoring;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class FormMutation
{
    private readonly IMapper _mapper;
    private readonly IFormService _service;

    public FormMutation(IMapper mapper, IFormService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public FormDto CreateForm(FormRequest request)
    {
        var form = _service.Create(request);
        return FormMapper.MapToDto(form);
    }

    public FormDto UpdateForm(string id, FormRequest request)
    {
        var form = _service.UpdateForm(id, request);
        return FormMapper.MapToDto(form);
    }
}