using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using forms.Dto;
using forms.Model;
using forms.Model.FormAuthoring;
using forms.Request;

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
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        // Form mappings
        CreateMap<FormRequest, Form>(); // request → entity
        CreateMap<Form, FormDto>(); // entity → response
        CreateMap<FormRequest, Form>() // merge request and entity 
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}