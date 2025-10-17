using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using forms.Dto;
using forms.Dto.FormAuthoring;
using forms.Dto.FormSubmission;
using forms.Model;
using forms.Model.FormAuthoring;
using forms.Model.FormSubmission;
using forms.Request;
using forms.Request.FormAuthoring;
using forms.Request.FormSubmission;

namespace forms.Mapping;

[ExcludeFromCodeCoverage]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserRequest, User>(); // request → entity
        CreateMap<User, UserDto>(); // entity → response
        CreateMap<UserRequest, User>() // merge request and entity 
            .ForMember(dest => dest.Password,
                opt => opt.Ignore())
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
                opt => opt.Ignore()
            );

        // Submission mappings
        CreateMap<FormSubmissionRequest, FormSubmission>(); // request → entity
        CreateMap<FormSubmission, FormSubmissionDto>(); // entity → response

        // Answer mappings
        CreateMap<FormAnswerRequest, FormAnswer>(); // request → entity
        CreateMap<FormAnswer, FormAnswerDto>(); // entity → response

        //Submission Detail
        CreateMap<FormSubmissionDetail, FormSubmissionDetailDto>();
    }
}