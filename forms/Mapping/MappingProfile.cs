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
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserRequest, User>(); // request → entity
        CreateMap<User, UserDto>(); // entity → response
        CreateMap<UserRequest, User>() // merge request and entity 
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        // Form mappings
        CreateMap<FormRequest, Form>() // merge request and entity 
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}