using AutoMapper;
using forms.Dto;
using forms.Service.Interface;

namespace forms.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class FormQuery
{
    private readonly IMapper _mapper;
    private readonly IFormService _service;

    public FormQuery(IMapper mapper, IFormService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public FormDto FetchForm(string id)
    {
        var form = _service.Fetch(id);
        return _mapper.Map<FormDto>(form);
    }
}