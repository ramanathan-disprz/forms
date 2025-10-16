using AutoMapper;
using forms.Service.Interface;

namespace forms.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class QuestionMutation
{
    private readonly IMapper _mapper;
    private readonly IQuestionService _service;

    public QuestionMutation(IMapper mapper, IQuestionService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [GraphQLName("deleteQuestion")]
    public bool DeleteQuestion(string id)
    {
        _service.Delete(id);
        return true;
    }
}