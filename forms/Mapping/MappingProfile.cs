using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using forms.Dto;
using forms.Dto.FormAuthoring;
using forms.Model;
using forms.Model.FormAuthoring;
using forms.Request;
using forms.Request.FormAuthoring;

namespace forms.Mapping;

[ExcludeFromCodeCoverage]
public class MappingProfile : Profile
{
    private readonly QuestionMapper _questionMapper;

    public MappingProfile(QuestionMapper questionMapper)
    {
        _questionMapper = questionMapper;
        // User mappings
        CreateMap<UserRequest, User>(); // request → entity
        CreateMap<User, UserDto>(); // entity → response
        CreateMap<UserRequest, User>() // merge request and entity 
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        // Form mappings
        CreateMap<FormRequest, Form>(); // request → entity
        CreateMap<Form, FormDto>(); // entity → response
        CreateMap<FormRequest, Form>() // merge request and entity 
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<FormWithQuestions, FormWithQuestionsDto>()
            .ForMember(dest => dest.form,
                opt =>
                    opt.MapFrom(src => src.form)
                    )
            .ForMember(dest => dest.questions,
                opt => 
                    opt.MapFrom(src => 
                     _questionMapper.Map(src.questions)
                     )
                    );
    }
}