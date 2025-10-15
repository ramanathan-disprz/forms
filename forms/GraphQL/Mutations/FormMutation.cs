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

    [GraphQLName("createForm")]
    public FormDto CreateForm(FormRequest request)
    {
        var form = _service.Create(request);
        return _mapper.Map<FormDto>(form);
    }

    [GraphQLName("updateForm")]
    public FormDto UpdateForm(string id, FormRequest request)
    {
        var form = _service.Update(id, request);
        return _mapper.Map<FormDto>(form);
    }

    [GraphQLName("deleteForm")]
    public bool DeleteForm(string id)
    {
        _service.Delete(id);
        return true;
    }
}